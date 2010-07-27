
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using jsc.Languages.JavaScript;
using jsc.Script;
using ScriptCoreLib;
using ScriptCoreLib.CSharp.Extensions;


namespace jsc
{
    using ilbp = ILBlock.Prestatement;
    using ilfsi = ILFlow.StackItem;
    using ili = ILInstruction;

    public delegate void OpCodeHandler(IdentWriter w, ilbp p, ili i, ilfsi[] s);

    partial class IL2ScriptGenerator
    {


        internal static OpCodeHandler[] InternalHandlers;

        public class HandlersWrapper
        {
            public OpCodeHandler this[params OpCode[] i]
            {
                set
                {
                    foreach (OpCode c in i)
                        this[c] = value;
                }
            }

            public OpCodeHandler this[OpCode i]
            {
                get
                {
                    return this[i.Value];
                }
                set
                {
                    this[i.Value] = value;
                }
            }

            public OpCodeHandler this[short i]
            {
                get
                {
                    return IL2ScriptGenerator.InternalHandlers[i & 0x0000ffff];
                }
                set
                {
                    IL2ScriptGenerator.InternalHandlers[i & 0x0000ffff] = value;
                }
            }
        }

        static readonly HandlersWrapper _Handlers = new HandlersWrapper();

        static public HandlersWrapper Handlers
        {
            get { return _Handlers; }
        }

        static IL2ScriptGenerator()
        {
            InternalHandlers = new OpCodeHandler[0xFFFF];

            CreateInstructionHandlers();
        }


        static void OpCodeHandlerLogic(IdentWriter w, ilbp p, ili i, ILBlock.InlineLogic logic)
        {


            if (logic.hint == ILBlock.InlineLogic.SpecialType.AndOperator)
            {
                if (logic.IsNegative)
                    w.Write("!");

                w.Write("(");


                OpCodeHandlerLogic(w, p, i, logic.lhs);

                w.WriteSpace();
                w.Write("&&");
                w.WriteSpace();

                OpCodeHandlerLogic(w, p, i, logic.rhs);

                w.Write(")");

                return;
            }

            if (logic.hint == ILBlock.InlineLogic.SpecialType.OrOperator)
            {
                if (logic.IsNegative)
                    w.Write("!");

                w.Write("(");
                OpCodeHandlerLogic(w, p, i, logic.lhs);

                w.WriteSpace();
                w.Write("||");
                w.WriteSpace();

                OpCodeHandlerLogic(w, p, i, logic.rhs);

                w.Write(")");

                return;
            }

            if (logic.hint == ILBlock.InlineLogic.SpecialType.Value)
            {
                if (logic.IsNegative)
                    w.Write("!");

                OpCodeHandler(w, p, logic.value.SingleStackInstruction, null);

                return;
            }

            if (logic.hint == ILBlock.InlineLogic.SpecialType.IfClause)
            {
                w.Write("(");

                if (logic.IsNegative)
                {
                    w.Write("!");

                }

                w.Write("(");

                if (logic.IfClause.Branch == OpCodes.Brtrue_S
                    || logic.IfClause.Branch == OpCodes.Brfalse_S)
                    OpCodeHandler(w, p, logic.IfClause.Branch, logic.IfClause.Branch.StackBeforeStrict[0]);
                else
                    OpCodeHandler(w, p, logic.IfClause.Branch, null);

                w.Write(")");

                w.WriteSpace();
                w.Write("?");
                w.WriteSpace();

                ILBlock.PrestatementBlock block;

                block = p.Owner.ExtractBlock(
                    /*logic.IsNegative ? logic.IfClause.FFirst :*/ logic.IfClause.BodyFalseFirst,
                    /*logic.IsNegative ? logic.IfClause.FLast :*/ logic.IfClause.BodyFalseLast
                );

                // todo: explicit boolean

                OpCodeHandler(w,
                  block.PrestatementCommands[block.PrestatementCommands.Count - 1],
                  block.PrestatementCommands[block.PrestatementCommands.Count - 1].Instruction,
                  null
                  );



                w.WriteSpace();
                w.Write(":");
                w.WriteSpace();

                block = p.Owner.ExtractBlock(
                    /*!logic.IsNegative ?*/ logic.IfClause.BodyTrueFirst /*: logic.IfClause.TFirst*/,
                    /*!logic.IsNegative ?*/ logic.IfClause.BodyTrueLast /*: logic.IfClause.TLast*/
                );


                OpCodeHandler(w,
                    block.PrestatementCommands[block.PrestatementCommands.Count - 1],
                    block.PrestatementCommands[block.PrestatementCommands.Count - 1].Instruction,
                    null
                    );


                w.Write(")");

                return;
            }

            Debugger.Break();
        }

        public static void OpCodeHandlerArgument(IdentWriter w, ilbp p)
        {
            OpCodeHandlerArgument(w, p, p.Instruction, p.Instruction.StackBeforeStrict[0]);
        }

        static void OpCodeHandlerArgument(IdentWriter w, ilbp p, ili i, ilfsi s)
        {
            if (s.StackInstructions.Length == 1)
            {
                OpCodeHandler(w, p, s.SingleStackInstruction, null);
            }
            else
            {
                OpCodeHandlerLogic(w, p, i, s.InlineLogic(p.Owner.MemoryBy(s)));
            }
        }

        public static void OpCodeHandler(IdentWriter w, ilbp p)
        {
            IL2ScriptGenerator.Handlers[p.Instruction.OpCode.Value](w, p, p.Instruction, p.Instruction.StackBeforeStrict);
        }

        /// <summary>
        /// resolves operand (stackitem) -or- if s is null, resolves raw opcode
        /// </summary>
        /// <param name="w"></param>
        /// <param name="p"></param>
        /// <param name="i"></param>
        /// <param name="s"></param>
        public static void OpCodeHandler(IdentWriter w, ilbp p, ili i, ilfsi s)
        {
            if (s == null)
            {
                if (i == null)
                {
                    w.Write("0 /* null instruction*/");

                    return;
                }

                // if debugger breaks, opcode is missing
                if (Handlers[i] == null)
                {
                    Task.Error("opcode unsupported - {0}", i);
                    Task.Fail(i);

                }
                else
                {


                    Handlers[i](w, p, i, i.StackBeforeStrict);
                }
            }
            else
            {
                OpCodeHandlerArgument(w, p, i, s);
            }
        }





        static void OpCode_call_override(IdentWriter w, ilbp p, ili i, ilfsi[] s, MethodBase m)
        {
            ScriptAttribute sq = ScriptAttribute.OfProvider(m);
            ScriptAttribute sqt = ScriptAttribute.OfProvider(m.DeclaringType);

            if (sqt == null && ScriptAttribute.IsAnonymousType(m.DeclaringType))
                sqt = new ScriptAttribute();

            // definition not found
            if (sqt == null && !m.DeclaringType.IsInterface)
            {
                using (StringWriter sw = new StringWriter())
                {
                    if (m.IsStatic)
                        sw.Write("static ");

                    sw.Write("{0}.{1}",
                        (m.DeclaringType.IsGenericType ?
                        m.DeclaringType.GetGenericTypeDefinition() :
                        m.DeclaringType).FullName, m.Name);
                    sw.Write("(");
                    int pix = 0;
                    foreach (ParameterInfo pi in m.GetParameters())
                    {
                        if (pix++ > 0)
                            sw.Write(", ");

                        sw.Write(pi.ParameterType.FullName);
                    }

                    sw.Write(")");

                    MethodBase x = w.Session.ResolveImplementationTrace(m.DeclaringType, m);

                    if (x == null)
                    {
                        Task.Error("No implementation found for this native method, please implement [{0}]", sw.ToString());
                        Task.Warning("Did you reference ScriptCoreLib via IAssemblyReferenceToken?", sw.ToString());
                    }
                    else
                        Task.Error("method was found, but too late: [{0}]", x.Name);

                    Task.Fail(i);

                    return;
                }
                Debugger.Break();
            }

            if (!m.IsStatic && sq != null && sq.DefineAsStatic)
            {
                w.WriteDecoratedMemberInfo(m);

                w.Write("(");
                OpCodeHandler(w, p, i, s[0]);
                if (s.Length > 1)
                {
                    w.Helper.WriteDelimiter();
                    w.WriteParameters(p, i, s, 1, m);
                }
                w.Write(")");
            }
            else
            {
                if (m.IsStatic)
                {
                    w.WriteDecoratedMemberInfo(m);

                }
                else
                {
                    // target
                    w.WriteHint("impl_type");
                    OpCodeHandler(w, p, i, s[0]);
                    w.Write(".");


                    // method
                    if (sqt != null && (sqt.ExternalTarget != null || sqt.HasNoPrototype))
                    {
                        w.Write(m.Name);
                    }
                    else
                    {
                        if (sq != null && sq.ExternalTarget != null)
                            w.Write(sq.ExternalTarget);
                        else
                            w.WriteDecoratedMemberInfo(m);
                    }
                }

                w.Write("(");
                // arguments
                w.WriteParameters(p, i, s, m.IsStatic ? 0 : 1, m);
                w.Write(")");
            }


        }


        //public static bool IsToString(MethodBase e)
        //{
        //    return e != null && e.Name == "ToString" && !e.IsStatic && e.GetParameters().Length == 0;
        //}



        static void OpCode_call(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            MethodBase mm = i.ReferencedMethod;

            if (mm.DeclaringType == typeof(System.Runtime.CompilerServices.RuntimeHelpers))
            {
                if (mm.Name == "InitializeArray")
                {
                    throw new SkipThisPrestatementException();
                }
            }

            #region fetch method
            MethodBase m = (MethodBase)i.TargetMethod ?? (MethodBase)i.TargetConstructor;


            if (m == null)
                Debugger.Break();
            #endregion


            if (m.DeclaringType.IsValueType)
            {
                if (m is ConstructorInfo)
                {
                    // fix this call as it shall be a call to new at the moment

                    OpCodeHandler(w, p, i, s[0]);

                    w.Helper.WriteAssignment();

                    ilfsi[] s2 = new ilfsi[s.Length - 1];

                    Array.Copy(s, 1, s2, 0, s2.Length);

                    WriteCreateType(w, p, i, s2,
                        w.Session.ResolveImplementation(m.DeclaringType, m) ?? m
                        );

                    return;
                    // 
                }
            }

            if (mm.DeclaringType.ToScriptAttributeOrDefault().HasNoPrototype)
            {
                // we are calling a native method

                var DefaultMemberAttribute = mm.DeclaringType.GetCustomAttributes<DefaultMemberAttribute>().FirstOrDefault();

                if (DefaultMemberAttribute != null)
                {
                    foreach (var DefaultMember in
                        mm.DeclaringType.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Where(k => k.Name == DefaultMemberAttribute.MemberName)
                        )
                    {


                        if (DefaultMember != null)
                        {
                            var Getter = DefaultMember.GetGetMethod();
                            if (Getter == mm)
                                if (Getter.ToScriptAttributeOrDefault().DefineAsStatic)
                                {
                                    // bail
                                }
                                else
                                {
                                    OpCodeHandler(w, p, i, s[0]);
                                    w.Write("[");
                                    OpCodeHandler(w, p, i, s[1]);
                                    w.Write("]");
                                    return;
                                }

                            var Setter = DefaultMember.GetSetMethod();

                            if (Setter == mm)
                                if (Setter.ToScriptAttributeOrDefault().DefineAsStatic)
                                {
                                    // bail
                                }
                                else
                                {
                                    OpCodeHandler(w, p, i, s[0]);
                                    w.Write("[");
                                    OpCodeHandler(w, p, i, s[1]);
                                    w.Write("]");

                                    w.WriteSpace();
                                    w.Write("=");
                                    w.WriteSpace();

                                    OpCodeHandler(w, p, i, s[2]);
                                    return;
                                }
                        }
                    }
                }
            }


            #region toString
            if (Script.CompilerBase.IsToStringMethod(m))
            {
                w.Write("(");
                OpCodeHandler(w, p, i, s[0]);
                w.Write("+''");
                w.Write(")");

                return;
            }
            #endregion



            var found_implementation = w.Session.ResolveImplementation(m.DeclaringType, m);

            OpCode_call_override(w, p, i, s,
                 found_implementation ?? m
                 );


        }

        static void OpCode_ldstr(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            /*var @break = i.TargetLiteral == "jsc.break";

            if (@break)
                Debugger.Break();*/

            w.WriteLiteral(i.TargetLiteral);
        }

        static void OpCode_ldc(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {

            if (i == OpCodes.Ldc_R4)
            {
                w.WriteNumeric(i.OpParamAsFloat);
                return;
            }

            if (i == OpCodes.Ldc_I8)
            {
                w.WriteNumeric((long)i.TargetLong);
                return;
            }

            if (i == OpCodes.Ldc_R8)
            {
                w.WriteNumeric(i.OpParamAsDouble);
                return;
            }

            int? n = i.TargetInteger;

            if (n == null)
            {
                Task.Error("ldc not resolved");
                Debugger.Break();
            }

            w.WriteNumeric(n.Value);
        }

        static void OpCode_br(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            if (i.TargetFlow.Branch == OpCodes.Ret)
            {
                OpCode_ret(w, p, i.TargetFlow.Branch, i.TargetFlow.Branch.StackBeforeStrict);
            }
            else
            {
                Task.Error("logic failure");
                Task.Fail(i);

            }

        }

        static void OpCode_leave(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            ILBlock b = i.Flow.OwnerBlock;

            if (b.Clause == null)
            {
                b = b.Parent;
            }

            if (b.Clause == null)
                Debugger.Break();


            if (b.Clause.Flags == ExceptionHandlingClauseOptions.Clause)
            {
                if (b.NextNonClauseBlock == null)
                    Debugger.Break();



                ILBlock.Prestatement tx = i.IndirectReturnPrestatement;

                if (tx != null)
                {
                    // redirect

                    OpCodeHandler(w, tx);

                    return;
                }


                if (b.NextNonClauseBlock.First == i.TargetInstruction)
                {
                    w.Write("/* leave */");
                    return;
                }


            }

            if (b.Clause.Flags == ExceptionHandlingClauseOptions.Finally)
            {
                ILBlock.Prestatement tx = i.IndirectReturnPrestatement;


                if (tx != null)
                {
                    // redirect

                    OpCodeHandler(w, tx);

                    return;
                }
            }

            // this needs to be fixed!
            throw new NotSupportedException("current OpCodes.Leave cannot be understood at " + i.Location);
        }

        static void OpCode_ret(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            if (s.Length == 0)
                w.Helper.DOMReturn();
            else
                w.Helper.DOMReturnExpression(
                    delegate { OpCodeHandler(w, p, i, s[0]); }
                );

        }

        static void OpCode_ldfld(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            if (i == OpCodes.Ldfld || i == OpCodes.Ldflda)
            {
                OpCodeHandler(w, p, i, s[0]);

            }

            if (i == OpCodes.Ldsfld)
            {
                object[] o = i.TargetField.DeclaringType.GetCustomAttributes(typeof(ScriptAttribute), true);

                if (o.Length == 1)
                {
                    ScriptAttribute sa = o[0] as ScriptAttribute;

                    if (sa.ExternalTarget != null)
                    {
                        Debugger.Break();

                        w.Write(sa.ExternalTarget);

                        goto skip;
                    }

                }

                o = i.TargetField.GetCustomAttributes(typeof(ScriptAttribute), true);

                if (o.Length == 1)
                {
                    ScriptAttribute sa = o[0] as ScriptAttribute;

                    if (sa.ExternalTarget != null)
                    {
                        w.Write(sa.ExternalTarget);

                        return;
                    }


                }


            }

        skip:


            OpCode_DecorateField(w, p, i, s);
        }

        static void OpCode_DecorateField(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            if (i.TargetField == null)
            {
                w.Write("/* bad field */");

                return;
            }

            var sa = i.TargetField.DeclaringType.ToScriptAttributeOrDefault();

            if (sa != null)
            {
                if (sa.HasNoPrototype)
                {
                    if (i.TargetField.IsStatic)
                    {

                        goto skip;
                    }
                    else
                        w.Write(".");


                    w.Write(i.TargetField.Name);

                    return;
                }


            }

            if (!i.TargetField.IsStatic)
                w.Write(".");

        skip:

            var TargetField = i.TargetField;
            var TargetFieldType = TargetField.DeclaringType;
            var TargetFieldTypeImplementation = w.Session.ResolveImplementation(TargetFieldType);

            if (TargetFieldTypeImplementation != null)
                if (TargetFieldType != TargetFieldTypeImplementation)
                {
                    TargetField = TargetFieldTypeImplementation.GetField(TargetField.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

                    if (TargetField == null)
                        throw new Exception("BCL implementation does not implement a field: " + i.TargetField.Name + " at " + TargetFieldType.ToString());
                }

            // static fields live on the global scope
            // yet when they need to honor [Script(Implements=)]



            w.WriteDecoratedMemberInfo(TargetField);


        }

        static void OpCode_stfld(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            if (i == OpCodes.Stfld) OpCodeHandler(w, p, i, s[0]);

            if (i == OpCodes.Stsfld)
            {
                object[] o = i.TargetField.DeclaringType.GetCustomAttributes(typeof(ScriptAttribute), true);

                if (o.Length == 1)
                {
                    ScriptAttribute sa = o[0] as ScriptAttribute;

                    if (sa.ExternalTarget != null)
                    {
                        Debugger.Break();

                        w.Write(sa.ExternalTarget);

                        goto skip;
                    }

                }




            }



        skip:


            OpCode_DecorateField(w, p, i, s);





            w.WriteSpace();
            w.Write("=");
            w.WriteSpace();

            if (OpCodeEmitStringEnum(w, s[s.Length - 1], i.TargetField.FieldType))
                return;

            OpCodeHandler(w, p, i, s[s.Length - 1]);
        }

        internal static bool OpCodeEmitStringEnum(IdentWriter w, ilfsi s, Type type)
        {


            if (type != null && type.IsEnum)
            {
                ScriptAttribute _enum_a = ScriptAttribute.Of(type);

                if (_enum_a != null && _enum_a.IsStringEnum)
                {
                    int? _enum_val = s.SingleStackInstruction.TargetInteger;

                    if (_enum_val != null)
                    {
                        string name = Enum.GetName(type, _enum_val.Value);

                        ScriptAttribute ma = ScriptAttribute.OfTypeMember(type, name);

                        if (ma != null)
                        {
                            if (ma.ExternalTarget != null)
                            {
                                w.WriteLiteral(ma.ExternalTarget);

                                return true;

                            }
                        }


                        w.WriteLiteral(name);


                        return true;
                    }
                }
            }



            return false;
        }

        static void OpCode_ldelem(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            OpCodeHandler(w, p, i, s[0]);
            w.Write("[");
            OpCodeHandler(w, p, i, s[1]);
            w.Write("]");
        }

        static void OpCode_stelem(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            OpCodeHandler(w, p, i, s[0]);
            w.Write("[");
            OpCodeHandler(w, p, i, s[1]);
            w.Write("]");
            w.WriteSpace();
            w.Write("=");
            w.WriteSpace();


            OpCodeHandler(w, p, i, s[2]);
        }


        static void OpCode_stobj(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            OpCodeHandler(w, p, i, s[0]);

            w.Write("=");

            OpCodeHandler(w, p, i, s[1]);
        }

        static void OpCode_ldobj(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            OpCodeHandler(w, p, i, s[0]);
        }
        static void OpCode_ldlen(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            OpCodeHandler(w, p, i, s[0]);
            w.Write(".length");
        }


        static void OpCode_ldnull(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            w.Write("null");
        }


        static void OpCode_ldvirtftn(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            OpCode_ldftn(w, p, i, s);
        }

        static void OpCode_ldtoken(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {

            w.Write("new ");

            w.Helper.WriteWrappedConstructor(

                w.Session.ResolveImplementation(
                typeof(RuntimeTypeHandle)
                ).GetConstructor(new Type[] { typeof(IntPtr) })

                );

            w.Write("(");

            w.Helper.WritePrototypeAlias(

                w.Session.ResolveImplementation(i.TargetType) ?? i.TargetType

                );

            w.Write(")");

        }

        static void OpCode_ldftn(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            var _Method = w.Session.ResolveImplementation(i.TargetMethod.DeclaringType, i.TargetMethod) ?? i.TargetMethod;

            w.WriteDecoratedMemberInfo(_Method, true);
        }




        static void OpCode_initobj(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            // we can only initobj a variable. we cannot init a generic type parameter
            if (i.Prev.TargetVariable == null)
                throw new SkipThisPrestatementException();

            //Script.CompilerBase.WriteVisualStudioMessage(jsc.Script.CompilerBase.MessageType.warning, 1001, "init missing: " + i.Method.DeclaringType.FullName + "." + i.Method.Name);

            w.WriteDecorated(i.OwnerMethod, i.Prev.TargetVariable);


            w.WriteSpace();
            w.Write("=");
            w.WriteSpace();

            if (i.TargetType.IsGenericParameter)
                w.Write("void(0)");
            else
            {
                var type = w.Session.ResolveImplementation(i.TargetType) ?? i.TargetType;

                var ctor = type.GetConstructor(new Type[0]);

                if (ctor == null)
                    CompilerBase.BreakToDebugger("valuetype ctor not found " + i.TargetType.ToString());

                WriteCreateType(w, p, i, new ILFlow.StackItem[0], ctor);

            }


            //Task.Error("default(T) not supported yet");
            //Task.Fail(i);
        }



        private static void WriteCreateType(IdentWriter w, ilbp p, ili i, ilfsi[] s, MethodBase m)
        {
            ScriptAttribute sa =
                ScriptAttribute.IsAnonymousType(m.DeclaringType) ?
                    new ScriptAttribute() :
                    ScriptAttribute.Of(m.DeclaringType, true);

            if (sa == null)
            {
                Script.CompilerBase.BreakToDebugger("ctor not found or no script attribute for type " + m.DeclaringType.FullName + " at " + i.Location);
                return;
            }


            if (sa.HasNoPrototype)
            {

                if (sa.GetConstructorAlias() != null)
                {
                    MethodBase c = w.Session.ResolveMethod(m, m.DeclaringType, sa.GetConstructorAlias());

                    if (c != null)
                    {

                        OpCode_call_override(w, p, i, s, c);

                        return;
                    }
                }




                if (sa.ExternalTarget == null)
                {
                    Task.Error("You tried to instance a class which seems to be marked as native.");

                    Task.Error("type has no callable constructor: [{0}] {1}", m.DeclaringType.FullName, m.ToString());
                    Task.Fail(i);
                }
                else
                    w.Helper.DOMCreateType(sa.ExternalTarget, p, i, s);
            }
            else
            {
                w.Helper.DOMCreateAndInvokeConstructor(
                    m.DeclaringType,
                    m, p, i, s);
            }
        }

        static void OpCode_newarr(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            #region inline newarr
            if (p.IsValidInlineArrayInit)
            {
                w.WriteLine("[");
                w.Ident++;

                ILFlow.StackItem[] _stack = p.InlineArrayInitElements;

                for (int si = 0; si < _stack.Length; si++)
                {


                    if (si > 0)
                    {
                        w.Write(",");
                        w.WriteLine();
                    }

                    w.WriteIdent();

                    if (_stack[si] == null)
                    {
                        if (!i.TargetType.IsValueType)
                        {
                            w.Write("null");
                        }
                        else
                        {
                            // http://stackoverflow.com/questions/325426/c-programmatic-equivalent-of-defaulttype

                            if (i.TargetType.IsEnum)
                                w.Write("" + Activator.CreateInstance(Enum.GetUnderlyingType(i.TargetType)));
                            else
                                w.Write("" + Activator.CreateInstance(i.TargetType));

                            //if (i.TargetType == typeof(double))
                            //    w.Write("0.0");
                            //else if (i.TargetType == typeof(int))
                            //    w.Write("0");
                            //else if (i.TargetType == typeof(sbyte))
                            //    w.Write("0");
                            //else if (i.TargetType == typeof(byte))
                            //    w.Write("0");
                            //else if (i.TargetType.IsEnum)
                            //    w.Write("0");
                            //else
                            //    CompilerBase.BreakToDebugger("default for " + i.TargetType.FullName + " is unknown at " + i.Location);
                        }
                    }
                    else
                    {
                        IL2ScriptGenerator.OpCodeHandler(w, p, i, _stack[si]);

                    }

                }

                w.WriteLine();

                w.Ident--;
                w.WriteIdent();
                w.Write("]");
            }
            #endregion
            else
            {
                if (i.NextInstruction == OpCodes.Dup &&
                                            i.NextInstruction.NextInstruction == OpCodes.Ldtoken &&
                                            i.NextInstruction.NextInstruction.NextInstruction == OpCodes.Call)
                {
                    var Length = (int)i.StackBeforeStrict.First().SingleStackInstruction.TargetInteger;
                    var Type = i.TargetType;

                    // Conversion To IEnumrable

                    if (Type == typeof(int))
                    {
                        var Values = i.NextInstruction.NextInstruction.TargetField.GetValue(null).StructAsInt32Array();

                        w.Write("[");
                        for (int j = 0; j < Values.Length; j++)
                        {
                            if (j > 0)
                                w.Write(", ");

                            w.Write(Values[j].ToString());
                        }
                        w.Write("]");
                    }
                    else if (Type == typeof(uint))
                    {
                        var Values = i.NextInstruction.NextInstruction.TargetField.GetValue(null).StructAsUInt32Array();

                        w.Write("[");
                        for (int j = 0; j < Values.Length; j++)
                        {
                            if (j > 0)
                                w.Write(", ");

                            w.Write(Values[j].ToString());
                        }
                        w.Write("]");
                    }
                    else if (Type == typeof(ushort))
                    {
                        var Values = i.NextInstruction.NextInstruction.TargetField.GetValue(null).StructAsUInt16Array();

                        w.Write("[");
                        for (int j = 0; j < Values.Length; j++)
                        {
                            if (j > 0)
                                w.Write(", ");

                            w.Write(Values[j].ToString());
                        }
                        w.Write("]");
                    }
                    else if (Type == typeof(double))
                    {
                        var Values = i.NextInstruction.NextInstruction.TargetField.GetValue(null).StructAsDoubleArray();

                        w.Write("[");
                        for (int j = 0; j < Values.Length; j++)
                        {
                            if (j > 0)
                                w.Write(", ");

                            w.WriteNumeric(Values[j]);
                        }
                        w.Write("]");
                    }
                    else
                        throw new NotImplementedException(Type.Name);




                    //Write("[ /* ? */ ]");

                    // todo: implement


                }
                else
                {

                    // Write("[]");
                    // this fix is for javascript too

                    if (i.StackBeforeStrict[0].SingleStackInstruction == OpCodes.Ldc_I4_0)
                    {
                        w.Write("[]");
                    }
                    else
                    {
                        w.Write("new Array(");


                        IL2ScriptGenerator.OpCodeHandler(w, p, i, i.StackBeforeStrict[0]);

                        w.Write(")");
                    }
                }
            }



        }

        static void OpCode_rethrow(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            w.Write("throw ");

            w.Helper.DOMWriteCatchParameter();


        }

        static void OpCode_throw(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            if (s.Length == 1)
            {
                w.Write("throw ");

                OpCodeHandler(w, p, i, s[0]);
            }
            else
            {
                Debugger.Break();
            }
        }




        static void OpCode_dup(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            if (s.Length != 1) Debugger.Break();

            OpCodeHandler(w, p, i, s[0]);
        }

        static void OpCode_pop(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            // size optimized

            //w.Write("void (");

            OpCodeHandler(w, p, i, s[0]);

            //w.Write(")");
        }

        static void OpCode_conv(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {

            //if (i == OpCodes.Conv_R8)
            //{

            //    OpCodeHandler(w, p, i, s[0]);

            //    return;
            //}

            //if (i.IsAnyOpCodeOf(OpCodes.Conv_I4, OpCodes.Conv_I8, OpCodes.Conv_U8, OpCodes.Conv_U4))
            //{

            OpCodeHandler(w, p, i, s[0]);

            //    return;
            //}

            //Debugger.Break();
        }

        static void OpCode_endfinally(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            Debugger.Break();
        }

        static void OpCode_castclass(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            // runtime check?

            OpCodeHandler(w, p, i, s[0]);
        }

        static void OpCode_box(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            if (s.Length != 1)
                Debugger.Break();

            Type t = i.TargetType;

            if (t == typeof(bool))
            {
                w.Write("new Boolean");
                w.Write("(");
                OpCodeHandler(w, p, i, s[0]);
                w.Write(")");

                return;
            }

            if (t == typeof(int))
            {
                w.Write("new Number");
                w.Write("(");
                OpCodeHandler(w, p, i, s[0]);
                w.Write(")");

                return;
            }


            if (t == null)
            {
                w.Write("/* null box */ ");
                OpCodeHandler(w, p, i, s[0]);

                return;
            }


            // w.Write("/* box[{0}] */ ", t.UnderlyingSystemType);

            OpCodeHandler(w, p, i, s[0]);
        }

        static void OpCode_donothing(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            //w.Write("/* {0} */", i.ToString());



            if (s.Length == 0)
                return;

            OpCodeHandler(w, p, i, s[0]);
        }




        /// <summary>
        /// defines "lhs op rhs"
        /// </summary>
        /// <param name="w"></param>
        /// <param name="p"></param>
        /// <param name="i"></param>
        /// <param name="lhs"></param>
        /// <param name="op"></param>
        /// <param name="rhs"></param>
        static void OpCode_OperatorHandler(IdentWriter w, ilbp p, ili i, ilfsi lhs, string op, ilfsi rhs)
        {
            OpCodeHandler(w, p, i, lhs);

            w.WriteSpace();
            w.Write(op);
            w.WriteSpace();

            OpCodeHandler(w, p, i, rhs);
        }

        static void OpCode_bne_un(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            OpCode_OperatorHandler(w, p, i, s[0], "!=", s[1]);
        }

        static void OpCode_beq(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            OpCode_OperatorHandler(w, p, i, s[0], "==", s[1]);
        }

        static void OpCode_bgt(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            OpCode_OperatorHandler(w, p, i, s[0], ">", s[1]);
        }

        static void OpCode_blt(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            OpCode_OperatorHandler(w, p, i, s[0], "<", s[1]);
        }

        static void OpCode_ble(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            OpCode_OperatorHandler(w, p, i, s[0], "<=", s[1]);
        }

        static void OpCode_bge(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            OpCode_OperatorHandler(w, p, i, s[0], ">=", s[1]);
        }


        static void OpCode_LogicOperators(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            // catch prefix operators

            if (i.IsInlinePrefixOperator(OpCodes.Add))
            {
                w.Write("++");
                OpCodeHandler(w, p, i, s[0]);
                return;
            }

            if (i.IsInlinePrefixOperator(OpCodes.Sub))
            {
                w.Write("--");
                OpCodeHandler(w, p, i, s[0]);
                return;
            }

            if (i == OpCodes.Not)
            {
                w.Write("~");

                OpCodeHandler(w, p, i, s[0]);


                return;
            }

            if (i == OpCodes.Ceq)
            {
                if (s[1].SingleStackInstruction == OpCodes.Ldc_I4_0)
                {
                    w.Write("!");
                    OpCodeHandler(w, p, i, s[0]);


                    return;
                }
            }

            if (i == OpCodes.Neg)
            {


                w.Write("(-");
                OpCodeHandler(w, p, i, s[0]);

                w.Write(")");

                return;

            }


            if (s[0].SingleStackInstruction.OpCode == OpCodes.Isinst)
                if (i.OpCode == OpCodes.Cgt_Un)
                    if (s[1].SingleStackInstruction.OpCode == OpCodes.Ldnull)
                    {
                        WriteOperatorIs(w, p, i, s[0]);


                        return;
                    }


            w.Write("(");

            OpCodeHandler(w, p, i, s[0]);

            w.WriteSpace();

            if (i.IsAnyOpCodeOf(OpCodes.Div, OpCodes.Div_Un)) w.Write("/");

            if (i == OpCodes.Sub ||
                i == OpCodes.Sub_Ovf) w.Write("-");

            if (i == OpCodes.Add ||
                i == OpCodes.Add_Ovf ||
                i == OpCodes.Add_Ovf_Un) w.Write("+");

            if (i == OpCodes.Mul) w.Write("*");
            if (i == OpCodes.And) w.Write("&");
            if (i == OpCodes.Or) w.Write("|");
            if (i == OpCodes.Xor) w.Write("^");
            if (i == OpCodes.Shl) w.Write("<<");
            if (i == OpCodes.Shr) w.Write(">>");
            if (i == OpCodes.Shr_Un) w.Write(">>");
            if (i == OpCodes.Cgt) w.Write(">");
            if (i == OpCodes.Cgt_Un) w.Write(">");
            if (i == OpCodes.Ceq) w.Write("==");
            if (i == OpCodes.Clt) w.Write("<");
            if (i == OpCodes.Clt_Un) w.Write("<");

            if (i.IsAnyOpCodeOf(OpCodes.Rem, OpCodes.Rem_Un)) w.Write("%");

            w.WriteSpace();

            if (s[0].SingleStackInstruction.TargetField != null)
                if (OpCodeEmitStringEnum(w, s[1], s[0].SingleStackInstruction.TargetField.FieldType))
                {

                    w.Write(")");
                    return;
                }

            #region uint fixup
            if (s[1].SingleStackInstruction.TargetInteger != null)
            {
                // if we are going to AND an uint, the second operator should also be presented
                if (
                    (s[0].SingleStackInstruction.TargetField != null && s[0].SingleStackInstruction.TargetField.FieldType == typeof(uint)) ||
                    (s[0].SingleStackInstruction.TargetParameter != null && s[0].SingleStackInstruction.TargetParameter.ParameterType == typeof(uint))
                    )
                {
                    w.Write((uint)s[1].SingleStackInstruction.TargetInteger);

                    w.Write(")");
                    return;
                }
            }
            #endregion


            OpCodeHandler(w, p, i, s[1]);

            w.Write(")");
        }

        //public static Func<IdentWriter, ilbp, ili, ilfsi[], bool> Override_OpCode_ldarg;

        static void OpCode_ldarg(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            //if (Override_OpCode_ldarg != null)
            //    if (Override_OpCode_ldarg(w, p, i, s))
            //        return;

            if (i.OwnerMethod.IsStatic)
            {
                w.WriteDecoratedParameterInfo(i.TargetParameter);
            }
            else
            {
                if (i == OpCodes.Ldarg_0)
                    w.WriteSelf();
                else
                    w.WriteDecoratedParameterInfo(i.TargetParameter);
            }
        }


        static void OpCode_ldloc(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {


            if (p.Owner.IsCompound)
            {
                ilbp sp = p.Owner.SourcePrestatement(p, i);

                if (sp != null)
                {
                    OpCodeHandlerArgument(w, sp);



                    return;
                }
            }


            w.WriteDecorated(i.OwnerMethod, i.TargetVariable);


            // -- operator?

            if (i.IsInlinePostSub) w.Write("--");
            if (i.IsInlinePostAdd) w.Write("++");
        }


        static void OpCode_stloc(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            // catch prefix operators here

            w.WriteDecorated(i.OwnerMethod, i.TargetVariable);



            if (i.IsInlinePrefixOperatorStatement(OpCodes.Add))
            {
                w.Write("++");
                return;
            }

            if (i.IsInlinePrefixOperatorStatement(OpCodes.Sub))
            {
                w.Write("--");
                return;
            }

            // optimize: g = g + 1 to g += 1
            if (w.OptimizeAssignment(p, i))
                return;



            w.WriteSpace();
            w.Write("=");
            w.WriteSpace();


            if (i.IsFirstInFlow && i.Flow.OwnerBlock.IsHandlerBlock)
                w.Write("__exc");
            else
            {

                if (OpCodeEmitStringEnum(w, s[0], i.TargetVariable.LocalType))
                    return;

                IL2ScriptGenerator.OpCodeHandler(w, p, i, s[0]);

            }
        }

        static void OpCode_starg(IdentWriter w, ilbp p, ili i, ilfsi[] s)
        {
            if (i.TargetParameter == null)
                Debugger.Break();

            w.WriteDecoratedParameterInfo(i.TargetParameter);


            w.WriteSpace();
            w.Write("=");
            w.WriteSpace();

            OpCodeHandler(w, p, i, s[0]);
        }
    }

}