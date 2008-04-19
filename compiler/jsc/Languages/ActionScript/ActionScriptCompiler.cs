using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using ScriptCoreLib;
using System.Reflection.Emit;
using jsc.Script;
using System.Runtime.InteropServices;

namespace jsc.Languages.ActionScript
{
    partial class ActionScriptCompiler : Script.CompilerCLike
    {
        public static string FileExtension = "as";

        public readonly AssamblyTypeInfo MySession;

        public ActionScriptCompiler(TextWriter xw, AssamblyTypeInfo xs)
            : base(xw)
        {
            MySession = xs;

            CreateInstructionHandlers();
        }

        private void GetImportTypesFromMethod(Type t, List<Type> imp, MethodBase v)
        {

            ScriptAttribute vs = ScriptAttribute.OfProvider(v);


            // DebugBreak(vs);

            if (vs != null && vs.DefineAsStatic)
                imp.Add(t);

            DebugBreak(vs);

            //imp.AddRange(GetMethodExceptions(v));

            foreach (ParameterInfo p in v.GetParameters())
            {
                imp.Add(p.ParameterType);
            }

            if (v.IsAbstract)
                return;

            foreach (LocalVariableInfo l in v.GetMethodBody().LocalVariables)
            {
                imp.Add(l.LocalType);
            }

            ILBlock b = new ILBlock(v);

            //for (var i = b.First; i != null; i = i.Next)
            foreach (var i in b.Instructrions)
            {
                if (i == OpCodes.Ldtoken)
                {

                    continue;
                }

                if (i == OpCodes.Ldvirtftn)
                {
                    imp.Add(typeof(IntPtr));
                    continue;
                }

                if (i == OpCodes.Ldftn)
                {
                    imp.Add(typeof(IntPtr));
                    continue;
                }

                if (i == OpCodes.Box)
                {

                    if (i.TargetType.IsGenericParameter)
                    {
                        // http://msdn2.microsoft.com/en-us/library/system.type.getgenericparameterconstraints(VS.80).aspx
                        var c = i.TargetType.GetGenericParameterConstraints().SingleOrDefault();

                        if (c != null)
                            imp.Add(c);
                    }
                    else
                    {
                        imp.Add(i.TargetType);
                    }
                }

                if (i.TargetMethod != null)
                {
                    var attr = i.TargetMethod.ToScriptAttribute();

                    if (attr != null && attr.NotImplementedHere)
                    {
                        var impl = MySession.ResolveImplementation(i.TargetMethod.DeclaringType, i.TargetMethod, AssamblyTypeInfo.ResolveImplementationDirectMode.ResolveNativeImplementationExtension);

                        if (impl != null)
                            imp.Add(impl.DeclaringType);
                    }
                }

                if (i.ReferencedMethod != null)
                {
                    if (!IsTypeOfOperator(i.ReferencedMethod))
                        if (i.ReferencedMethod.DeclaringType != typeof(object))
                        {
                            if (i.ReferencedMethod.DeclaringType == typeof(System.Runtime.CompilerServices.RuntimeHelpers))
                                continue;

                            if (ScriptAttribute.IsCompilerGenerated(i.ReferencedMethod.DeclaringType))
                            {
                                imp.Add(i.ReferencedMethod.DeclaringType);
                                continue;
                            }

                            if (i.ReferencedMethod.DeclaringType.IsInterface)
                            {
                                imp.Add(MySession.ResolveImplementation(i.ReferencedMethod.DeclaringType));
                                continue;
                            }

                            MethodBase method = GetMethodImplementation(MySession, i);
                            var method_attribute = method.ToScriptAttribute();
                            if (method.IsConstructor || method.IsStatic || (method_attribute != null && method_attribute.DefineAsStatic))
                            {
                                imp.Add(method.DeclaringType);
                                continue;
                            }
                        }
                }



                if (i.TargetField != null)
                {
                    if (i.TargetField.IsStatic)
                    {
                        imp.Add(i.TargetField.DeclaringType);
                        continue;
                    }
                }
            }
        }

        private readonly Dictionary<Type, IEnumerable<Type>> GetImportTypes_Cache =
            new Dictionary<Type, IEnumerable<Type>>();

        private IEnumerable<Type> GetImportTypes(Type t)
        {
            if (GetImportTypes_Cache.ContainsKey(t))
                return GetImportTypes_Cache[t];


            var imp = new List<Type>();

            Type[] tinterfaces = t.GetInterfaces();

            foreach (Type tinterface in tinterfaces)
                imp.Add(tinterface);

            if (t.BaseType == typeof(MulticastDelegate))
                return new List<Type> { 
                    
                    MySession.ResolveImplementation(typeof(IntPtr)) };

            /*
            Type bp = t.BaseType;

            while (bp != typeof(object) &&
                    bp != null)
            {
                imp.Add(bp);
                bp = bp.BaseType;
            }
            */
            foreach (FieldInfo v in this.GetAllFields(t))
            {
                imp.Add(v.FieldType);
            }

            foreach (MethodBase v in GetAllInstanceConstructors(t))
            {


                GetImportTypesFromMethod(t, imp, v);
            }

            foreach (MethodInfo mi in this.GetAllMethods(t))
            {
                if (ScriptAttribute.IsAnonymousType(t))
                {
                    if (mi.Name == "GetHashCode") continue;
                    if (mi.Name == "Equals") continue;
                }

                imp.Add(mi.ReturnParameter.ParameterType);

                MethodBase v = mi;

                GetImportTypesFromMethod(t, imp, v);
            }

            var imp_types = new List<Type>();

            imp.RemoveAll(i => i == null);
            imp.RemoveAll(i => i.IsGenericParameter);

            // todo: import only if used in code...
            imp.Add(GetArrayEnumeratorType());


            while (imp.Count > 0)
            {
                Type p = imp[0];


                // remove duplicates
                imp.RemoveAll(
                     delegate(Type w)
                     {


                         if (w.IsArray && p.IsArray)
                         {
                             return w.GetElementType().GUID == p.GetElementType().GUID;
                         }

                         return w.GUID == p.GUID;
                     }
                 );

                // todo fix additional types handling

                while (p.IsArray)
                {
                    p = p.GetElementType();

                }

                if (p.IsGenericParameter)
                    continue;

                if (p.IsEnum)
                    continue;

                if (p == typeof(object)) continue;
                if (p == typeof(void)) continue;
                if (p == typeof(string)) continue;
                if (p == typeof(int)) continue;
                if (p == typeof(uint)) continue;
                if (p == typeof(short)) continue;
                if (p == typeof(long)) continue;
                if (p == typeof(float)) continue;
                if (p == typeof(double)) continue;
                if (p == typeof(byte)) continue;
                if (p == typeof(sbyte)) continue;
                if (p == typeof(bool)) continue;
                if (p == typeof(char)) continue;

                // is a BCL type
                var a = p.ToScriptAttribute();

                if (a == null)
                {
                    if (ScriptAttribute.IsCompilerGenerated(p))
                    {
                        imp_types.Add(p);

                        continue;
                    }

                    // and has an implementation type
                    var p_impl = MySession.ResolveImplementation(p);

                    if (p_impl == null)
                    {
                        //if (p.IsInterface)
                        //{
                        //    // silently skip this interface
                        //    continue;
                        //}

                        Break("class import: no implementation for " + p.ToString() + " at " + t.FullName);
                    }

                    p = p_impl;
                    // a = ScriptAttribute.Of(p, true);
                }


                imp_types.Add(p);


            }

            imp_types.AddRange(
                from i in imp_types.ToArray()
                let ia = i.ToScriptAttribute()
                where ia != null
                where ia.ImplementationType != null
                select ia.ImplementationType
            );


            return GetImportTypes_Cache[t] = imp_types;
        }

        public void WriteImportTypes(Type z)
        {
            // all field types, return types, parameter types, variable types, statics

            List<Type> t = GetImportTypes(z).ToList();
            List<string> imports = new List<string>();

            /*
            t.RemoveAll(delegate(Type x)
            {
                return IsEmptyImplementationType(x);
            });
            */

            while (t.Count > 0)
            {
                Type p = t[0];

                // optimize me

                t.RemoveAll(
                    delegate(Type x)
                    {
                        return x.GUID == z.GUID || x.GUID == p.GUID;
                    }
                );



                ScriptAttribute a = ScriptAttribute.Of(p, false);

                string n = (p.Namespace == null) ? "" : p.Namespace + ".";



                if (a != null && a.Implements != null && a.ExternalTarget != null)
                {
                    if (p != z)
                    {

                        imports.Add(n + GetDecoratedTypeName(p, false));
                    }

                    imports.Add(GetDecoratedTypeName(p, true));


                }
                else
                {

                    imports.Add(n + GetDecoratedTypeName(p, true));



                }



            }


            imports.Sort(
               delegate(string x, string y)
               {
                   return x.CompareTo(y);
               });

            foreach (string var in imports)
            {

                WriteIdent();

                Write("import ");
                Write(NamespaceFixup(var));
                WriteLine(";");

            }


        }



        private void CreateInstructionHandlers()
        {
            CIW[OpCodes.Neg] =
                delegate(CodeEmitArgs e)
                {
                    Write("(-(");
                    EmitFirstOnStack(e);
                    Write("))");
                };

            CIW[OpCodes.Callvirt] =
                e =>
                {
                    WriteMethodCall(e.p, e.i, e.i.TargetMethod);
                };

            #region call
            CIW[OpCodes.Call] =
                e =>
                {
                    MethodBase m = e.i.ReferencedMethod;


                    if (m.DeclaringType == typeof(System.Runtime.CompilerServices.RuntimeHelpers))
                    {
                        if (m.Name == "InitializeArray")
                        {
                            throw new SkipThisPrestatementException();
                        }
                    }


                    MethodBase mi = MySession.ResolveImplementation(m.DeclaringType, m);

                    if (mi != null)
                    {
                        WriteMethodCall(e.p, e.i, mi);

                        return;
                    }

                    if (m.Name == "op_Implicit")
                    {
                        ScriptAttribute sa = ScriptAttribute.Of(m.DeclaringType, false);

                        if (sa != null && sa.IsNative)
                        {
                            // that implicit call is only for to help c# conversions
                            // so we must emit first parameter

                            EmitFirstOnStack(e);
                            return;
                        }
                    }

                    WriteMethodCall(e.p, e.i, m);
                };
            #endregion

            #region Stloc
            CIW[OpCodes.Stloc_0,
                OpCodes.Stloc_1,
                OpCodes.Stloc_2,
                OpCodes.Stloc_3,
                OpCodes.Stloc_S,
                OpCodes.Stloc] =
                e =>
                {
                    WriteVariableName(e.i.OwnerMethod.DeclaringType, e.i.OwnerMethod, e.i.TargetVariable);

                    #region ++ --
                    if (e.FirstOnStack.StackInstructions.Length == 1)
                    {
                        ILInstruction i = e.FirstOnStack.SingleStackInstruction;

                        if (i == OpCodes.Add)
                        {
                            if (i.StackBeforeStrict[1].SingleStackInstruction.TargetInteger == 1)
                            {
                                if (i.StackBeforeStrict[0].SingleStackInstruction.IsEqualVariable(e.i.TargetVariable))
                                {
                                    Write("++");
                                    return;
                                }
                            }
                        }

                        if (i == OpCodes.Sub)
                        {
                            if (i.StackBeforeStrict[1].SingleStackInstruction.TargetInteger == 1)
                            {
                                if (i.StackBeforeStrict[0].SingleStackInstruction.IsEqualVariable(e.i.TargetVariable))
                                {
                                    Write("--");
                                    return;
                                }
                            }
                        }
                    }
                    #endregion

                    WriteAssignment();

                    if (e.i.IsFirstInFlow && e.i.Flow.OwnerBlock.IsHandlerBlock)
                    {
                        WriteExceptionVar();
                        return;
                    }

                    if (EmitEnumAsStringSafe(e))
                        return;

                    #region  assign boolean literal
                    if (e.i.TargetVariable.LocalType == typeof(bool))
                    {
                        if (e.i.StackBeforeStrict[0].IsSingle)
                        {
                            if (e.i.StackBeforeStrict[0].SingleStackInstruction.TargetInteger != null)
                            {
                                if (e.i.StackBeforeStrict[0].SingleStackInstruction.TargetInteger == 0)
                                    WriteKeywordFalse();
                                else
                                    WriteKeywordTrue();

                                return;
                            }
                        }
                    }
                    #endregion



                    EmitFirstOnStack(e);
                };
            #endregion


            #region Ldloc
            CIW[OpCodes.Ldloc_0,
                OpCodes.Ldloc_1,
                OpCodes.Ldloc_2,
                OpCodes.Ldloc_3,
                OpCodes.Ldloc_S,
                OpCodes.Ldloc,
                OpCodes.Ldloca,
                OpCodes.Ldloca_S] =
               e =>
               {
                   #region inline assigment
                   if (e.i.InlineAssigmentValue != null)
                   {
                       //WriteBoxedComment("inline");

                       Emit(e.i.InlineAssigmentValue,
                           e.i.InlineAssigmentValue.Instruction.StackBeforeStrict[0]);


                       return;
                   }
                   #endregion

                   if (e.p != null)
                       if (e.p.Owner != null)
                           if (e.p.Owner.IsCompound)
                           {
                               ILBlock.Prestatement sp = e.p.Owner.SourcePrestatement(e.p, e.i);

                               if (sp != null)
                               {
                                   EmitInstruction(sp, sp.Instruction);

                                   return;
                               }
                           }

                   WriteVariableName(e.i.OwnerMethod.DeclaringType, e.i.OwnerMethod, e.i.TargetVariable);


                   if (e.i.IsInlinePostSub) Write("--");
                   if (e.i.IsInlinePostAdd) Write("++");
               };
            #endregion

            #region Newobj
            CIW[OpCodes.Newobj] =
                e =>
                {
                    WriteTypeConstruction(e);
                };
            #endregion

            #region fld
            CIW[OpCodes.Ldfld,
                OpCodes.Ldflda] =
                e =>
                {

                    Emit(e.p, e.FirstOnStack);
                    Write(".");
                    WriteSafeLiteral(e.i.TargetField.Name);

                };

            CIW[OpCodes.Stfld] =
                e =>
                {


                    ILFlow.StackItem[] s = e.i.StackBeforeStrict;

                    Emit(e.p, s[0]);
                    Write(".");
                    WriteSafeLiteral(e.i.TargetField.Name);
                    WriteAssignment();

                    #region  assign boolean literal
                    if (e.i.TargetField.FieldType == typeof(bool))
                    {
                        if (e.i.StackBeforeStrict[1].StackInstructions.Length == 1)
                        {
                            if (e.i.StackBeforeStrict[1].SingleStackInstruction.TargetInteger != null)
                            {
                                if (e.i.StackBeforeStrict[1].SingleStackInstruction.TargetInteger == 0)
                                    Write("false");
                                else
                                    Write("true");

                                return;
                            }
                        }
                    }
                    #endregion

                    Emit(e.p, s[1]);
                };
            #endregion

            CIW[OpCodes.Pop] = CodeEmitArgs.DelegateEmitFirstOnStack;


            CIW[OpCodes.Ldstr] =
                e =>
                {
                    WriteQuotedLiteral(e.i.TargetLiteral);
                };


            #region Ldarg
            CIW[OpCodes.Ldarg_0,
                OpCodes.Ldarg_1,
                OpCodes.Ldarg_2,
                OpCodes.Ldarg_3,
                OpCodes.Ldarg_S,
                OpCodes.Ldarg] =
                e =>
                {
                    WriteMethodParameterOrSelf(e.i);
                };
            #endregion

            #region starg
            CIW[OpCodes.Starg_S,
                OpCodes.Starg] =
                e =>
                {
                    WriteMethodParameterOrSelf(e.i);
                    WriteAssignment();
                    if (EmitEnumAsStringSafe(e))
                        return;

                    Emit(e.p, e.FirstOnStack);
                };
            #endregion

            CIW[
                OpCodes.Br_S,
                OpCodes.Br] =
                delegate(CodeEmitArgs e)
                {
                    // adjusted for inline assigment

                    if (e.i.TargetFlow.Branch == OpCodes.Ret)
                    {
                        WriteReturn(e.p, e.i);
                    }
                    else throw new NotSupportedException("invalid br opcode");
                };

            #region Ret
            CIW[OpCodes.Ret] =
                e =>
                {
                    WriteReturn(e.p, e.i);
                };
            #endregion


            #region ldc
            CIW[OpCodes.Ldc_R4] =
                e =>
                {
                    WriteNumeric(e.i.OpParamAsFloat);
                };

            CIW[OpCodes.Ldc_R8] =
                e =>
                {
                    WriteNumeric(e.i.OpParamAsDouble);
                };


            CIW[OpCodes.Ldc_I4,
                OpCodes.Ldc_I4_0,
                OpCodes.Ldc_I4_1,
                OpCodes.Ldc_I4_2,
                OpCodes.Ldc_I4_3,
                OpCodes.Ldc_I4_4,
                OpCodes.Ldc_I4_5,
                OpCodes.Ldc_I4_6,
                OpCodes.Ldc_I4_7,
                OpCodes.Ldc_I4_8,
                OpCodes.Ldc_I4_M1,
                OpCodes.Ldc_I8,

                OpCodes.Ldc_I4_S] =
               e =>
               {
                   int? n = e.i.TargetInteger;

                   if (n == null)
                   {
                       // long fix

                       long? x = e.i.TargetLong;

                       if (x == null)
                       {
                           Break("ldc unresolved");
                       }
                       else
                       {
                           MyWriter.Write(x.Value);
                       }
                   }
                   else
                   {
                       MyWriter.Write(n.Value);
                   }
               };
            #endregion

            #region Ldlen
            CIW[OpCodes.Ldlen] =
                e =>
                {
                    EmitFirstOnStack(e);

                    Write(".length");
                };
            #endregion


            #region operators
            CIW[OpCodes.Xor] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "^"); };
            CIW[OpCodes.Shl] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "<<"); };
            CIW[OpCodes.Shr] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, ">>"); };

            CIW[OpCodes.Clt, OpCodes.Clt_Un] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "<"); };
            CIW[OpCodes.Cgt, OpCodes.Cgt_Un] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, ">"); };

            CIW[OpCodes.Add] =
                delegate(CodeEmitArgs e)
                {
                    if (e.i.IsInlinePrefixOperator(OpCodes.Add))
                    {
                        Write("++");
                        Emit(e.p, e.FirstOnStack);
                        return;
                    }

                    WriteInlineOperator(e.p, e.i, "+");
                };

            CIW[OpCodes.Sub] =
                delegate(CodeEmitArgs e)
                {
                    if (e.i.IsInlinePrefixOperator(OpCodes.Sub))
                    {
                        Write("--");
                        EmitFirstOnStack(e);
                        return;
                    }

                    WriteInlineOperator(e.p, e.i, "-");
                };

            CIW[OpCodes.Or] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "|"); };
            CIW[OpCodes.And] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "&"); };
            CIW[OpCodes.Rem] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "%"); };
            CIW[OpCodes.Mul] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "*"); };
            CIW[OpCodes.Div] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "/"); };
            CIW[OpCodes.Bge_S,
                OpCodes.Bge] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, ">="); };
            CIW[OpCodes.Ble_S,
                OpCodes.Ble] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "<="); };
            CIW[OpCodes.Bne_Un_S,
                OpCodes.Bne_Un] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "!="); };
            CIW[OpCodes.Ceq] =
                delegate(CodeEmitArgs e)
                {
                    if (e.i.IsNegativeOperator)
                    {
                        Write("!");
                        Emit(e.p, e.i.StackBeforeStrict[0]);
                    }
                    else
                        WriteInlineOperator(e.p, e.i, "==");
                };
            #endregion

            #region conv

            // not supported
            // CIW[OpCodes.Conv_I1] = e => ConvertTypeAndEmit(e, "byte");

            CIW[OpCodes.Conv_U2] = e => ConvertTypeAndEmit(e, "uint"); // char == int
            CIW[OpCodes.Conv_I4] = e => ConvertTypeAndEmit(e, "int");

            // CIW[OpCodes.Conv_I8] = e => ConvertTypeAndEmit(e, "long");
            // CIW[OpCodes.Conv_U8] = e => ConvertTypeAndEmit(e, "long");

            CIW[OpCodes.Conv_R4] = e => ConvertTypeAndEmit(e, "Number");
            CIW[OpCodes.Conv_R8] = e => ConvertTypeAndEmit(e, "Number");

            // CIW[OpCodes.Conv_U1] = e => ConvertTypeAndEmit(e, "byte");
            CIW[OpCodes.Conv_Ovf_I] = e => ConvertTypeAndEmit(e, "int");
            #endregion

            CIW[OpCodes.Ldnull] = e => Write("null");


            #region Newarr
            CIW[OpCodes.Newarr] =
                e =>
                {

                    #region inline newarr
                    if (e.p != null && e.p.IsValidInlineArrayInit)
                    {
                        WriteLine("[");
                        Ident++;

                        ILFlow.StackItem[] _stack = e.p.InlineArrayInitElements;

                        for (int si = 0; si < _stack.Length; si++)
                        {


                            if (si > 0)
                            {
                                Write(",");
                                WriteLine();
                            }

                            WriteIdent();

                            if (_stack[si] == null)
                            {
                                if (!e.i.TargetType.IsValueType)
                                {
                                    Write("null");
                                }
                                else
                                {
                                    if (e.i.TargetType == typeof(int))
                                        Write("0");
                                    else if (e.i.TargetType == typeof(sbyte))
                                        Write("0");
                                    else
                                        BreakToDebugger("default for " + e.i.TargetType.FullName + " is unknown");
                                }
                            }
                            else
                            {
                                Emit(e.p, _stack[si]);
                            }

                        }

                        WriteLine();

                        Ident--;
                        WriteIdent();
                        Write("]");
                    }
                    #endregion
                    else
                    {

                        if (e.i.NextInstruction == OpCodes.Dup &&
                            e.i.NextInstruction.NextInstruction == OpCodes.Ldtoken &&
                            e.i.NextInstruction.NextInstruction.NextInstruction == OpCodes.Call)
                        {
                            var Length = (int)e.i.StackBeforeStrict.First().SingleStackInstruction.TargetInteger;
                            var Type = e.i.TargetType;

                            if (Type == typeof(int))
                            {
                                var Values = StructAsInt32Array(e.i.NextInstruction.NextInstruction.TargetField.GetValue(null));

                                Write("[");
                                for (int i = 0; i < Values.Length; i++)
                                {
                                    if (i > 0)
                                        Write(", ");

                                    Write(Values[i].ToString());
                                }
                                Write("]");
                            }
                            else
                                throw new NotImplementedException();



                            //Write("[ /* ? */ ]");

                            // todo: implement


                        }
                        else
                            Write("[]");

                    }
                };
            #endregion


            #region elem_ref
            CIW[OpCodes.Ldelem_Ref,
                OpCodes.Ldelem_U1,
                OpCodes.Ldelem_U2,
                OpCodes.Ldelem_U4,
                OpCodes.Ldelem_I1,
                OpCodes.Ldelem_I2,
                OpCodes.Ldelem_I4,
                OpCodes.Ldelem_I8,
                OpCodes.Ldelem
                ] =
                e =>
                {
                    ILFlow.StackItem[] s = e.i.StackBeforeStrict;

                    Emit(e.p, s[0]);
                    Write("[");
                    Emit(e.p, s[1]);
                    Write("]");
                };

            CIW[OpCodes.Stelem_Ref,
                OpCodes.Stelem_I1,
                OpCodes.Stelem_I2,
                OpCodes.Stelem_I4
                ] =
                e =>
                {
                    ILFlow.StackItem[] s = e.i.StackBeforeStrict;

                    Emit(e.p, s[0]);
                    Write("[");
                    Emit(e.p, s[1]);
                    Write("]");
                    WriteAssignment();

                    Emit(e.p, s[2]);
                };
            #endregion

            CIW[OpCodes.Castclass] = e => ConvertTypeAndEmit(e, e.i.TargetType);


            #region Stsfld
            CIW[OpCodes.Stsfld] =
               e =>
               {
                   try
                   {
                       bool _b_skip_classname = false;

                       if (e.Method.IsStatic && e.Method.MemberType == MemberTypes.Constructor)
                       {
                           if (e.i.TargetField.IsInitOnly)
                           {
                               // javac workaround

                               _b_skip_classname = true;
                           }
                       }

                       if (!_b_skip_classname)
                       {
                           WriteDecoratedTypeName(e.i.TargetField.DeclaringType);
                           Write(".");
                       }

                       WriteSafeLiteral(e.i.TargetField.Name);
                       //Write(e.i.TargetField.Name);
                       WriteAssignment();

                       if (EmitEnumAsStringSafe(e))
                           return;

                       Emit(e.p, e.FirstOnStack);
                   }
                   catch (Exception exc)
                   {
                       throw exc;
                   }
               };
            #endregion


            CIW[OpCodes.Constrained] =
                e =>
                {
                    if (e.i.StackBeforeStrict.Length == 0)
                        return;

                    EmitFirstOnStack(e);
                };

            CIW[OpCodes.Ldsfld] =
                e =>
                {
                    ILFlow.StackItem[] s = e.i.StackBeforeStrict;

                    WriteDecoratedTypeName(e.i.TargetField.DeclaringType);
                    Write(".");
                    WriteSafeLiteral(e.i.TargetField.Name);
                };


            CIW[OpCodes.Dup] = e => EmitFirstOnStack(e);

            CIW[OpCodes.Box] =
                e =>
                {
                    // how do we box a generic type?

                    var t = e.i.TargetType;

                    if (t.IsGenericParameter)
                    {
                        // http://msdn2.microsoft.com/en-us/library/system.type.getgenericparameterconstraints(VS.80).aspx
                        var c = t.GetGenericParameterConstraints().SingleOrDefault();

                        if (c == null)
                        {
                            EmitFirstOnStack(e);
                            return;
                        }
                        else
                        {
                            ConvertTypeAndEmit(e, c);
                            return;
                        }
                    }

                    Write("new ");
                    Write(GetDecoratedTypeName(t, true));
                    Write("(");

                    EmitFirstOnStack(e);

                    Write(")");
                };

            #region Ldftn
            CIW[OpCodes.Ldftn,
                OpCodes.Ldvirtftn] =
                delegate(CodeEmitArgs e)
                {
                    // we must load it as IntPtr
                    var _IntPtr = MySession.ResolveImplementation(typeof(IntPtr));
                    var _Operators = _IntPtr.GetExplicitOperators(null, _IntPtr);

                    var _IntPtr_string = _Operators.Single(i => i.GetParameters().Single().ParameterType == typeof(string));
                    var _IntPtr_Function = _Operators.Single(i => i.GetParameters().Single().ParameterType != typeof(string));

                    var _Method = e.i.TargetMethod;
                    if (_Method.IsStatic)
                    {
                        WriteDecoratedTypeNameOrImplementationTypeName(_IntPtr, false, false, IsFullyQualifiedNamesRequired(e.Method.DeclaringType, _IntPtr));
                        Write(".");
                        WriteDecoratedMethodName(_IntPtr_Function, false);
                        Write("(");
                        WriteDecoratedTypeNameOrImplementationTypeName(_Method.DeclaringType, false, false, IsFullyQualifiedNamesRequired(e.Method.DeclaringType, _Method.DeclaringType));
                        Write(".");
                        WriteDecoratedMethodName(e.i.TargetMethod, false);
                        Write(")");
                    }
                    else
                    {
                        if (_Method.DeclaringType == e.Method.DeclaringType)
                        {
                            WriteDecoratedTypeNameOrImplementationTypeName(_IntPtr, false, false, IsFullyQualifiedNamesRequired(e.Method.DeclaringType, _IntPtr));
                            Write(".");
                            WriteDecoratedMethodName(_IntPtr_Function, false);
                            Write("(");
                            WriteDecoratedMethodName(e.i.TargetMethod, false);
                            Write(")");
                        }
                        else
                        {
                            WriteDecoratedTypeNameOrImplementationTypeName(_IntPtr, false, false, IsFullyQualifiedNamesRequired(e.Method.DeclaringType, _IntPtr));
                            Write(".");
                            WriteDecoratedMethodName(_IntPtr_string, false);
                            Write("(");
                            WriteDecoratedMethodName(_Method, true);
                            Write(")");
                        }
                    }



                };
            #endregion

            #region Throw
            CIW[OpCodes.Throw] =
                e =>
                {
                    Write("throw");
                    WriteSpace();

                    EmitFirstOnStack(e);
                };
            #endregion
        }



        public override void WriteMethodParameterList(MethodBase m)
        {
            WriteMethodParameterList(m, null);
        }

        public void WriteMethodParameterList(MethodBase m, ILFlow.StackItem[] DefaultValues)
        {
            ParameterInfo[] mp = m.GetParameters();

            var ma = m.ToScriptAttribute();


            bool bStatic = (ma != null && ma.DefineAsStatic);

            if (bStatic)
            {
                if (m.IsStatic)
                {
                    Break("method is already static, but is marked to be declared out of band : " + m.DeclaringType.FullName + "." + m.Name);
                }


                DebugBreak(ma);

                // cannot use 'this' on arguments as it is a keyword
                WriteSelf();
                Write(":");
                WriteDecoratedTypeNameOrImplementationTypeName(m.DeclaringType, true, true, IsFullyQualifiedNamesRequired(m.DeclaringType, m.DeclaringType));

                //var sa = ScriptAttribute.Of(m.DeclaringType, false);

                //if (sa.Implements == null)
                //{
                //    WriteDecoratedTypeName(m.DeclaringType);

                //}
                //else
                //{
                //    WriteDecoratedTypeName(sa.Implements);
                //}
            }

            for (int mpi = 0; mpi < mp.Length; mpi++)
            {
                if (mpi > 0 || bStatic)
                {
                    Write(",");
                    WriteSpace();
                }

                ParameterInfo p = mp[mpi];

                ScriptAttribute za = ScriptAttribute.Of(m.DeclaringType, true);

                // nameless params...
                if (string.IsNullOrEmpty(p.Name))
                    Write("_" + mpi);
                else
                    WriteDecoratedMethodParameter(p);

                Write(":");
                WriteDecoratedTypeNameOrImplementationTypeName(p.ParameterType, true, true, IsFullyQualifiedNamesRequired(m.DeclaringType, p.ParameterType));

                if (DefaultValues != null && mpi < DefaultValues.Length)
                {
                    WriteAssignment();
                    EmitInstruction(null, DefaultValues[mpi].SingleStackInstruction);
                }
                /*
                if (za.Implements == null || m.DeclaringType.GUID != p.ParameterType.GUID)
                    WriteDecoratedTypeName(p.ParameterType);
                else
                    WriteDecoratedTypeName(za.Implements);
                */

            }
        }

        public void ConvertTypeAndEmit(CodeEmitArgs e, Type x)
        {
            var r = e.i.StackBeforeStrict.Single().SingleStackInstruction.ReferencedType;
            var ra = r.ToScriptAttribute();

            if (r == x || (ra != null && ra.IsArray))
            {
                EmitFirstOnStack(e);
                return;
            }


            Write("(");
            WriteDecoratedTypeNameOrImplementationTypeName(x, true, true, IsFullyQualifiedNamesRequired(e.Method.DeclaringType, x));
            Write("(");
            EmitFirstOnStack(e);
            Write(")");
            Write(")");
        }

        public override void ConvertTypeAndEmit(CodeEmitArgs e, string x)
        {
            Write("(" + x + "(");
            EmitFirstOnStack(e);
            Write("))");
        }

        Type CachedArrayEnumeratorType;

        public Type GetArrayEnumeratorType()
        {
            return CachedArrayEnumeratorType ?? (CachedArrayEnumeratorType = (from i in MySession.ImplementationTypes
                                                                              let a = i.ToScriptAttribute()
                                                                              where a != null
                                                                              where a.IsArrayEnumerator
                                                                              select i).SingleOrDefault());
        }

        public override void WriteArrayToCustomArrayEnumeratorCast(Type Enumerable, Type ElementType, ILBlock.Prestatement p, ILFlow.StackItem s)
        {
            var x = GetArrayEnumeratorType();
            if (x == null)
                throw new Exception("SZArrayEnumerator is missing");

            var ArrayToEnumerator = x.GetImplicitOperators(null, null).Single();

            WriteDecoratedTypeNameOrImplementationTypeName(x, false, false, IsFullyQualifiedNamesRequired(p.DeclaringMethod.DeclaringType, x));
            Write(".");
            WriteDecoratedMethodName(ArrayToEnumerator, false);
            Write("(");

            Emit(p, s);

            Write(")");

        }

        public override string GetDecoratedMethodParameter(ParameterInfo p)
        {
            return GetSafeLiteral(p.Name);
        }





        public static int[] StructAsInt32Array(object data)
        {
            // http://www.vsj.co.uk/articles/display.asp?id=501

            var size = Marshal.SizeOf(data);
            var buf = Marshal.AllocHGlobal(size);


            Marshal.StructureToPtr(data, buf, false);

            var a = new int[size / sizeof(int)];

            unsafe
            {
                int* p = (int*)buf;
                for (int i = 0; i < a.Length; i++)
                {
                    a[i] = *p;
                    p++;
                }
            }

            Marshal.FreeHGlobal(buf);

            return a;
        }

        public override void WriteDecoratedTypeName(Type context, Type subject)
        {
            // used by OpCodes.Newobj

            WriteDecoratedTypeNameOrImplementationTypeName(subject, false, false, IsFullyQualifiedNamesRequired(context, subject));

        }

        protected override void WriteTypeInstanceConstructors(Type z)
        {
            var zci = GetAllInstanceConstructors(z);

            if (zci.Length > 1)
            {
                // visual basic can have optional parameters on its own, its c# that needs some help
                // as3 does not support method overloading but does support default parameters
                // we need to figure out which ctor is real and which are just sattelites

                var query =
                    from c in zci
                    let b = new ILBlock(c).Prestatements.PrestatementCommands.Where(p => !p.Instruction.IsAnyOpCodeOf(OpCodes.Ret, OpCodes.Nop)).ToArray()
                    where (b.Length == 1 && b[0].Instruction == OpCodes.Call)
                    let i = b[0].Instruction
                    let t = i.TargetConstructor
                    where t != null && zci.Contains(t)
                    // skip the ldarg0/this op
                    select new { ctor = c, target = t, args = i.StackBeforeStrict.Skip(1).ToArray() };

                var cache = query.ToArray();
                var targets = zci.Except(cache.Select(i => i.ctor)).ToArray();


                if (targets.Length != 1)
                    throw new NotSupportedException("Unable to transform overloaded constructors to a single constructor via optional parameters for " + z.FullName);
                    
                var target = targets.Single();


                // step 1
                var ctor = cache.Single(i => i.target == target);
                var args = ctor.args;

                while (true)
                {
                    ctor = cache.SingleOrDefault(i => i.target == ctor.ctor);

                    if (ctor == null)
                        break;

                    args = args.Select((s, i) => s.SingleStackInstruction.TargetParameter == null ? s : ctor.args[i]).ToArray();
                } 
               
                // now we should have one ctor and others that point to them

                WriteMethodSignature(target, false, WriteMethodSignatureMode.Delcaring, args);
                WriteMethodBody(target);

            }
            else
            {
                foreach (var zc in zci)
                {
                    WriteMethodSignature(zc, false);
                    WriteMethodBody(zc);

                }
            }

            WriteLine();
        }
    }


}
