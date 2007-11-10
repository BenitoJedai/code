using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Emit;

using ScriptCoreLib;

using jsc.Script;

namespace jsc
{
    using ilbp = ILBlock.Prestatement;
    using ili = ILInstruction;
    using ilfsi = ILFlow.StackItem;



    public class IL2Script
    {




        public static void Emit(IdentWriter w, ILBlock.Prestatement p)
        {
            if (IL2ScriptGenerator.Handlers[p.Instruction.OpCode.Value] == null)
            {
                //Task.Error("unknown opcode  [{0}] in {2}.{1}", p, p.Instruction.OwnerMethod.Name, p.Instruction.OwnerMethod.DeclaringType.FullName);

                jsc.Script.CompilerBase.BreakToDebugger("unknown opcode " + p.Instruction.OpCode.Name + " at " + p.Instruction.OwnerMethod.Name + " + " + string.Format("0x{0:x4}", p.Instruction.Offset));


                //w.WriteCommentLine("prestatement 0x{0:x4} {1}", p.Instruction.Offset, p.Instruction.OpCode.Name);

                throw new InvalidOperationException();

            }
            else
            {
                if (p.Instruction == OpCodes.Nop)
                    return;

                if (p.Next == null &&
                    p.Instruction == OpCodes.Ret)
                {
                    if (p == null)
                    {
                        Task.Error("no prestatement  given");

                        Debugger.Break();
                    }

                    try
                    {
                        if (p.Instruction.NextInstruction == null)
                        {
                            if (p.Instruction.OwnerMethod.IsConstructor)
                                return;


                            if (p.Instruction.OwnerMethod is MethodInfo)
                            {
                                if ((p.Instruction.OwnerMethod as MethodInfo).ReturnType == typeof(void))
                                    return;
                            }
                            else
                            {
                                if (p.Instruction.OwnerMethod.IsStatic)
                                    return;
                            }


                        }

                    }
                    catch
                    {

                        // wtf
                        Debugger.Break();
                    }
                }

                if (p.Instruction.IsLoadInstruction)
                    throw new Exception("a load instruction was selected as prestatement. this is a bug within jsc. " + p.Instruction.Location);

                w.WriteIdent();
                IL2ScriptGenerator.OpCodeHandler(w, p);
                w.WriteLine(";");
            }
        }


        public static void EmitScope(IdentWriter w, ILBlock.PrestatementBlock s, bool can_collapse)
        {
            EmitScope(w, s, can_collapse, null);
        }

        public static void EmitScope(IdentWriter w, ILBlock.PrestatementBlock s, bool can_collapse, Predicate<ILBlock.Prestatement> predicate)
        {

            //if (false && can_collapse && !s.DemandsScope)
            //{
            //    w.Ident++;
            //    EmitPrestatement(w, s.PrestatementCommands[0]);
            //    w.Ident--;
            //}
            //else
            //{
            w.WriteBeginScope();
            EmitPrestatementBlock(w, s, predicate);
            w.WriteEndScope();
            //}
        }



        public static void EmitPrestatementBlock(IdentWriter w, ILBlock.PrestatementBlock s)
        {
            EmitPrestatementBlock(w, s, null);
        }


        public static void EmitPrestatementBlock(IdentWriter w, ILBlock.PrestatementBlock s, Predicate<ILBlock.Prestatement> predicate)
        {
            for (int x = 0; x < s.PrestatementCommands.Count; x++)
            {
                ILBlock.Prestatement p = s.PrestatementCommands[x];

                if (predicate != null)
                {
                    if (predicate(p))
                        continue;
                }

                #region try
                if (p.Block != null)
                {


                    if (p.Block.IsTryBlock)
                    {

                        w.WriteIdent();
                        w.WriteLine("try");


                        ILBlock.PrestatementBlock b = p.Block.Prestatements;

                        bool _pop = false;
                        bool _leave = b.Last == OpCodes.Leave_S && b.Last.TargetInstruction == b.OwnerBlock.NextNonClauseBlock.First;

                        EmitScope(w, b.ExtractBlock(_pop ? b.First.Next : b.First, _leave ? b.Last.Prev : b.Last), false);

                        continue;
                    }

                    if (p.Block.IsHandlerBlock)
                    {


                        w.WriteIdent();



                        ILBlock.PrestatementBlock b = p.Block.Prestatements;

                        bool _pop = b.First == OpCodes.Pop && (p.Block.Clause.Flags == ExceptionHandlingClauseOptions.Clause);
                        bool _leave =
                            b.Last == OpCodes.Endfinally
                        ||
                            (b.Last == OpCodes.Leave_S && b.Last.TargetInstruction == b.OwnerBlock.NextNonClauseBlock.First);

                        b = b.ExtractBlock(_pop ? b.First.Next : b.First, _leave ? b.Last.Prev : b.Last);

                        b.RemoveNopOpcodes();


                        //w.WriteHint("commands: " + b.PrestatementCommands.Count);

                        //foreach (ILBlock.Prestatement px in b.PrestatementCommands)
                        //{
                        //    w.WriteHint(px.ToString());
                        //}

                        if (b.PrestatementCommands.Count == 0)
                        {
                            if (p.Block.Clause.Flags == ExceptionHandlingClauseOptions.Finally)
                                w.WriteLine("finally ");
                            else
                                if (p.Block.Clause.Flags == ExceptionHandlingClauseOptions.Clause)
                                {
                                    w.Write("catch (");
                                    w.Helper.DOMWriteCatchParameter();
                                    w.Write(")");
                                }
                                else
                                    Debugger.Break();


                            w.Write("{ }");
                            w.WriteLine();
                        }
                        else
                        {
                            if (p.Block.Clause.Flags == ExceptionHandlingClauseOptions.Finally)
                                w.WriteLine("finally");
                            else
                                if (p.Block.Clause.Flags == ExceptionHandlingClauseOptions.Clause)
                                {
                                    w.Write("catch (");
                                    w.Helper.DOMWriteCatchParameter();
                                    w.Write(")");
                                    w.WriteLine();
                                }
                                else
                                    Debugger.Break();

                            EmitScope(w, b, false);

                        }

                        continue;
                    }


                }
                #endregion


                #region if
                ILIfElseConstruct iif = p.Instruction.InlineIfElseConstruct;

                if (iif != null)
                {
                    w.WriteLine();
                    w.WriteIdent();
                    w.Write("if");
                    w.WriteSpace();


                    if (iif.Branch.IsAnyOpCodeOf(OpCodes.Brfalse_S, OpCodes.Brfalse))
                    {
                        w.Write("(");
                        IL2ScriptGenerator.OpCodeHandler(w, p, iif.Branch, iif.Branch.StackBeforeStrict[0]);
                        w.Write(")");
                    }
                    else
                    {

                        if (iif.Branch.IsAnyOpCodeOf(OpCodes.Brtrue_S, OpCodes.Brtrue))
                        {
                            // fix 2.03.2006
                            w.Write("(");
                            w.Write("!");

                            ILFlow.StackItem expression = iif.Branch.StackBeforeStrict[0];

                            bool compact = false;

                            if (expression.StackInstructions.Length == 1)
                            {
                                if (expression.SingleStackInstruction.TargetVariable != null)
                                    compact = true;
                            }

                            if (compact)
                                IL2ScriptGenerator.OpCodeHandler(w, p, iif.Branch, expression);

                            else
                            {
                                w.Write("(");
                                IL2ScriptGenerator.OpCodeHandler(w, p, iif.Branch, expression);

                                w.Write(")");
                            }

                            w.Write(")");
                            //w.Write("(!(");
                            //IL2ScriptGenerator.OpCodeHandler(w, p, iif.Branch, iif.Branch.StackBeforeStrict[0]);
                            //w.Write("))");
                        }
                        else
                        {
                            Task.Error("if block not detected correctly, opcode was " + iif.Branch.OpCode.Name);
                            Task.Fail(null);


                        }
                    }

                    w.WriteLine();

                    //w.WriteCommentLine(iif.Branch.ToString());

                    EmitScope(w, p.Owner.ExtractBlock(iif.BodyTrueFirst, iif.BodyTrueLast), true);

                    if (iif.HasElseClause)
                    {
                        w.WriteIdent();
                        w.WriteLine("else");

                        EmitScope(w, p.Owner.ExtractBlock(iif.BodyFalseFirst, iif.BodyFalseLast), true);
                    }

                    w.WriteLine();

                    continue;
                }
                #endregion

                #region loop
                ILLoopConstruct loop = p.Instruction.InlineLoopConstruct;

                if (loop != null)
                {
                    if (loop.IsBreak(p.Instruction))
                    {
                        w.WriteIdent();
                        w.WriteLine("break;");
                        continue;
                    }

                    if (loop.IsContinue(p.Instruction))
                    {
                        w.WriteIdent();
                        w.WriteLine("continue;");
                        continue;
                    }

                    w.WriteIdent();
                    w.Write("while");
                    w.WriteSpace();
                    w.Write("(");


                    ILBlock.PrestatementBlock block;


                    block = p.Owner.ExtractBlock(loop.CFirst, loop.CLast);
                    block.IsCompound = true;

                    if (block.LastPrestatement.Instruction.OpCode.FlowControl == FlowControl.Branch)
                    {
                        w.Write("true");
                    }
                    else
                    {

                        IL2ScriptGenerator.OpCodeHandlerArgument(w, block.LastPrestatement);

                    }

                    w.Write(")");
                    w.WriteLine();

                    EmitScope(w, p.Owner.ExtractBlock(loop.BodyFirst, loop.BodyLast), true);

                    continue;
                }
                #endregion

                #region opt-out

                if (p != null && p.Instruction != null)
                {
                    try
                    {
                        if (p.Instruction == OpCodes.Nop)
                            continue;


                        if (p.Instruction.OpCode == OpCodes.Call)
                        {
                            if (p.Instruction.TargetConstructor != null)
                            {
                                if (p.Instruction.TargetConstructor.DeclaringType == typeof(object))
                                    continue;
                            }
                        }


                        if (p.Instruction == OpCodes.Ret)
                        {
                            if (p.Next == null)
                            {
                                if (p.Instruction.StackBeforeStrict.Length == 0)
                                {
                                    continue;
                                }
                            }
                        }
                    }
                    catch
                    {
                        Console.WriteLine("optout failed");
                    }
                }
                #endregion

                #region SupportsForStatements
                /* if (SupportsForStatements)*/
                {
                    if (p.IsValidForStatement)
                    {
                        x += 1;



                        WriteForStatement(w, p);

                        continue;
                    }
                }
                #endregion

                #region newarr
                /* if (SupportsInlineArrayInit) */
                {
                    if (p.IsValidInlineArrayInit)
                    {
                        x += p.InlineArrayInitElementsFound;
                    }
                }
                #endregion

                Emit(w, p);


            }

        }

        private static void WriteForStatement(IdentWriter w, ILBlock.Prestatement p)
        {
            if (!p.IsValidForStatement)
                CompilerBase.BreakToDebugger("not a valid for statement");

            w.WriteLine();

            //p.Instruction.ToConsole(4);

            w.WriteIdent();
            w.Write("for (");

            IL2ScriptGenerator.OpCodeHandler(w, p);

            w.Write("; ");

            ILLoopConstruct loop = p.Next.Instruction.InlineLoopConstruct;

            ILBlock.PrestatementBlock block = p.Owner.ExtractBlock(loop.CFirst, loop.CLast);

            block.IsCompound = true;

            if (block.LastPrestatement.Instruction.OpCode.FlowControl == FlowControl.Branch)
            {
                w.Write("true");
            }
            else
            {
                IL2ScriptGenerator.OpCodeHandler(w, block.LastPrestatement, block.LastPrestatement.Instruction, block.LastPrestatement.Instruction.StackBeforeStrict[0]);

            }

            w.Write("; ");

            ILBlock.PrestatementBlock wblock = p.Owner.ExtractBlock(loop.BodyFirst, loop.BodyLast);

            IL2ScriptGenerator.OpCodeHandler(w, wblock.LastPrestatement);



            w.WriteLine(")");

            w.WriteBeginScope();

            EmitPrestatementBlock(w, wblock,
                delegate(ILBlock.Prestatement xxp)
                {
                    return xxp == wblock.LastPrestatement;
                }
                );
            w.WriteEndScope();

            w.WriteLine();
        }


        public static void EmitBody(IdentWriter w, MethodBase i, bool define_self)
        {
            if (i.IsAbstract)
            {
                w.Write("/* abstract */");

                return;
            }

            try
            {


                ILBlock xb = new ILBlock(i);


                ScriptAttribute a = ScriptAttribute.OfProvider(i);

                Script.CompilerBase.DebugBreak(a);

                bool _a = DefineLocalVariables(w, i, define_self, false);

                if (_a)
                    w.WriteLine();

                bool _noexeptions = (a != null && a.NoExeptions);

                if (_noexeptions)
                {
                    w.WriteIdent();
                    w.WriteLine("try");
                    w.WriteBeginScope();
                }


                ILBlock.PrestatementBlock b = xb.Prestatements;

                if (b.PrestatementCommands.Count > 0)
                {
                    ILInstruction _base = b.PrestatementCommands[0].Instruction;

                    bool _ctor = i.IsConstructor;
                    bool _ctor_objbase = _ctor && _base == OpCodes.Call && _base.TargetConstructor.DeclaringType == typeof(object);

                    EmitPrestatementBlock(w, b.ExtractBlock(_ctor_objbase ? _base.Next : b.First, b.Last));
                }

                if (_noexeptions)
                {
                    w.WriteEndScope();
                    w.WriteIdent();
                    w.WriteLine("catch (_ne) {}");



                }

            }
            catch (Exception exc)
            {
                Script.CompilerBase.BreakToDebugger("emmiting failed : " + exc.Message + " at " + exc.StackTrace);
            }
        }

        private static bool DefineLocalVariables(IdentWriter w, MethodBase i, bool define_self, bool _a)
        {
            if (define_self && !i.IsStatic)
            {
                //if (xb.MethodMakesUseOfThisReference)
                //{


                w.WriteIdent();
                w.Write("var ");
                w.WriteSelf();
                w.Write(" = ");
                w.Write("this");

                _a = true;

                //}
            }

            IList<LocalVariableInfo> locals = i.GetMethodBody().LocalVariables;

            if (locals.Count > 0)
            {


                if (!_a)
                {
                    w.WriteIdent();
                    w.Write("var ");
                }

                for (int ilocal = 0; ilocal < locals.Count; ilocal++)
                {
                    if (_a || ilocal > 0)
                        w.Write(", ");

                    w.WriteDecorated(i, locals[ilocal]);
                }

                _a = true;
            }

            if (_a)
            {
                w.WriteLine(";");
            }

            return _a;
        }



        public static void DeclareStaticConstructors(IdentWriter w, Type[] types, bool debug)
        {
            foreach (Type z in types)
            {
                if (!z.IsClass)
                    continue;


                object[] o = z.GetCustomAttributes(typeof(ScriptAttribute), true);

                if (o.Length == 1)
                {
                    ScriptAttribute sa = o[0] as ScriptAttribute;

                    if (!sa.IsDebugCode && debug)
                        continue;
                }
                else
                    continue;

                ConstructorInfo[] ci = z.GetConstructors(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Public);
                foreach (ConstructorInfo zc in ci)
                {

                    ScriptAttribute xsa = ScriptAttribute.Of(z);

                    if (xsa != null && xsa.IsDebugCode && debug)
                        new ILBlock(zc).ToConsole();


                    w.Helper.DOMWriteTerminator(
                        delegate { w.Helper.DOMAnonymousMethodCall(zc); }
                    );



                    w.WriteLine();
                    w.WriteLine();

                }
            }
        }



        public static void DeclareVirtualMethods(IdentWriter w, Type owner)
        {
            // find the virtual name or names

            if (ScriptAttribute.Of(owner).HasNoPrototype)
                return;

            //owner.
            Type[] t = owner.GetInterfaces();

            bool b = false;

            __handler AfterAction = null;

            __handler BeforeAction = delegate
            {
                w.WriteCommentLine(owner.FullName);

                w.WriteIdent();
                w.Write("(function (i)");

                w.WriteBeginScope();

                AfterAction = delegate
                {
                    w.WriteEndScope();

                    w.WriteIdent();
                    w.Write(")(");
                    w.Helper.WritePrototypeAlias(owner);
                    w.WriteLine(");");
                };
            };


            __handler AfterOnce =
                delegate
                {
                    if (AfterAction == null)
                        return;

                    AfterAction();

                    AfterAction = null;
                };

            __handler BeforeOnce =
                delegate
                {
                    if (BeforeAction == null)
                        return;

                    BeforeAction();

                    BeforeAction = null;
                };

            foreach (Type x in t)
            {
                w.WriteCommentLine(x.FullName);

                InterfaceMapping z = owner.GetInterfaceMap(x);

                int ix = 0;

                //w.WriteCommentLine(z.TargetType.FullName);


                foreach (MethodInfo zm in z.TargetMethods)
                {
                    //ScriptAttribute s = ScriptAttribute.OfProvider( zm.DeclaringType);

                    //if (s == null)
                    //{
                    //    if (w.Session.ResolveImplementation(zm.DeclaringType, zm) == null) 
                    //    {
                    //         w.WriteCommentLine(zm.DeclaringType + "." + zm.Name);

                    //        continue;
                    //    }
                    //}

                    BeforeOnce();

                    w.WriteIdent();

                    w.Write("i");
                    w.Helper.WriteAccessor();
                    w.WriteDecoratedMethodName(z.InterfaceMethods[ix]);

                    w.Helper.WriteAssignment();

                    w.Write("i");
                    w.Helper.WriteAccessor();
                    w.WriteDecoratedMethodName(z.TargetMethods[ix]);
                    w.Helper.WriteTerminator();
                    w.WriteLine();

                    //w.Helper.DOMCopyMember(owner, z.InterfaceMethods[ix], z.TargetMethods[ix]);

                    b = true;

                    ix++;
                }





            }



            AfterOnce();

            //if (b)
            //    w.WriteLine();

        }

        public static void OverrideVirtualMember(IdentWriter w, Type z, MethodInfo m)
        {
            w.WriteIdent();


            ParameterInfo[] p = m.GetParameters();



            Type[] pt = new Type[p.Length];

            for (int i = 0; i < p.Length; i++)
                pt[i] = p[i].ParameterType;

            ParameterModifier pm = p.Length > 0 ? new ParameterModifier(p.Length) : new ParameterModifier();

            if (p.Length > 0)
            {
                for (int i = 0; i < p.Length; i++)
                    pm[i] = true;
            }

            Type _base = z;

            while (true)
            {
                _base = _base.BaseType;

                MethodInfo[] xxx = _base.GetMethods();

                MethodInfo v = (MethodInfo)w.Session.ResolveMethod(m as MethodBase, _base, null);


                MethodInfo[] zzz = _base.GetMethods(
                    //                m.Name,
                BindingFlags.ExactBinding
                | BindingFlags.Instance
                | BindingFlags.DeclaredOnly
                | BindingFlags.Public
                | BindingFlags.NonPublic
                    //,
                    //null,
                    //CallingConventions.HasThis,
                    //pt, new ParameterModifier[] { pm }
                );

                if (v != null)
                {
                    w.WriteIdent();
                    w.WriteDecorated(z);
                    w.Write(".prototype.");
                    w.WriteDecoratedMethodName(v);
                    w.WriteHint("virtual override");
                    w.Write(" = ");
                    w.WriteDecorated(z);
                    w.Write(".prototype.");
                    w.WriteDecoratedMethodName(m);
                    w.WriteLine(";");


                    //Task.WriteLine("virtual overload [{0}]", v.Name);

                    return;
                }

                if (_base == typeof(object))
                    break;

            }

            if (Script.CompilerBase.IsToStringMethod(m))
                return;

            Task.Error("override is in effect, base class mehtod should be overridden");
            Task.Error("unable to map override to [{0}.{1}]", m.DeclaringType, m.Name);
            Task.Fail(null);



        }

        public static void DeclareMethods(IdentWriter w, MethodBase[] mi)
        {
            #region methods
            foreach (MethodInfo zm in mi)
            {


                ScriptAttribute zsa = ScriptAttribute.Of(zm);

                //bool _define = true; 

                if (zsa != null)
                {

                    if (zsa.HasNoPrototype)
                    {
                        Debugger.Break();
                        continue;
                    }

                    if (zsa.ExternalTarget != null)
                    {
                        Task.WriteLine("method: {0}", zsa.ExternalTarget);
                        continue;
                    }

                }

                Task.Enabled = zsa != null && zsa.IsDebugCode;

                w.WriteCommentLine("{0}.{1}", zm.DeclaringType.FullName, zm.Name);


                if (!zm.IsStatic && zsa != null && zsa.DefineAsStatic)
                {
                    // attach an instance function as static
                    #region DefineAsStatic
                    ParameterInfo[] pi = zm.GetParameters();


                    w.WriteIdent();
                    w.Write("function ");
                    w.WriteDecoratedMethodName(zm);
                    w.Write("(");
                    w.WriteSelf();


                    if (pi.Length > 0)
                    {
                        w.Helper.WriteDelimiter();

                        if (zsa != null && zsa.OptimizedCode != null && !zsa.UseCompilerConstants)
                            w.WriteParameterArray(zm.GetParameters());
                        else
                            w.WriteDecoratedParameterArray(pi);

                    }

                    w.Write(")");

                    if (zsa != null && zsa.OptimizedCode != null)
                    {
                        w.Write(" { ");

                        #region constants

                        string code = DoCompilerConstants(zm, zsa);


                        w.Write(code);
                        #endregion

                        w.WriteLine(" }");
                        w.Helper.WriteTerminator();
                    }
                    else
                    {
                        w.WriteLine();
                        w.WriteBeginScope();
                        IL2Script.EmitBody(w, zm, false);
                        w.EndScopeAndTerminate();
                        w.WriteLine();
                    }
                    #endregion
                }
                else
                {
                    if (SemiInlineWrapperMethod(w, zm, zsa))
                    {
                        // inlined
                    }
                    else
                    {
                        if (zm.IsStatic)
                        {
                            w.WriteIdent();
                            w.Write("function ");
                            w.WriteDecoratedMethodName(zm);
                        }
                        else
                        {
                            ScriptAttribute s = ScriptAttribute.Of(zm.DeclaringType);

                            if (s != null && s.HasNoPrototype)
                                continue;

                            w.WriteIdent();
                            w.Helper.WritePrototypeAlias(zm.DeclaringType);

                            //w.WriteDecorated(zm.DeclaringType);
                            //w.Helper.WriteAccessor();
                            //w.Write(ScriptAttribute.Prototype);
                            w.Helper.WriteAccessor();
                            w.WriteDecoratedMethodName(zm);
                            w.Write(" = function ");
                        }




                        if (zsa != null && zsa.OptimizedCode != null)
                        {
                            w.Write("(");
                            w.WriteParameterArray(zm.GetParameters());
                            w.Write(") { ");

                            #region constants

                            string code = DoCompilerConstants(zm, zsa);

                            w.Write(code);
                            #endregion


                            w.Write(" }");
                            w.Helper.WriteTerminator();
                            w.WriteLine();

                        }
                        else
                        {
                            w.Write("(");
                            w.WriteDecoratedParameterArray(zm.GetParameters());
                            w.Write(")");


                            w.WriteLine();


                            w.WriteBeginScope();


                            if (zm.Name == "GetHashCode" && ScriptAttribute.IsAnonymousType(zm.DeclaringType))
                            {
                                w.WriteIdent();
                                w.WriteLine("throw 'Not implemented';");
                            }
                            else
                            {
                                IL2Script.EmitBody(w, zm, !zm.IsStatic);
                            }


                            w.EndScopeAndTerminate();


                            if (zm.IsVirtual && zm.IsHideBySig)
                            {

                                if ((zm.Attributes & MethodAttributes.VtableLayoutMask) == 0)
                                {
                                    Task.Enabled = true;
                                    OverrideVirtualMember(w, zm.DeclaringType, zm);
                                }

                            }

                            w.WriteLine();
                        }
                    }
                }


            }
            //w.WriteCommentLine("endmethods");

            #endregion
        }

        private static bool SemiInlineWrapperMethod(IdentWriter w, MethodInfo zm, ScriptAttribute zsa)
        {
            if (!zm.IsStatic)
                return false;

            if (zsa != null && zsa.DefineAsInstance)
                return false;

            if (zsa != null && zsa.OptimizedCode != null)
                return false;

            ILBlock xb = new ILBlock(zm);

            if (xb.Prestatements.PrestatementCommands.Count == 3)
            {
                ILBlock.Prestatement _0 = xb.Prestatements.PrestatementCommands[0];
                ILBlock.Prestatement _1 = xb.Prestatements.PrestatementCommands[1];
                ILBlock.Prestatement _2 = xb.Prestatements.PrestatementCommands[2];

                if (_0.Instruction.OpCode != OpCodes.Nop)
                    goto skip;

                if (!_1.Instruction.IsStoreLocal)
                    goto skip;

                if (_2.Instruction.OpCode != OpCodes.Ret)
                    goto skip;

                // validate that the local variable is the same

                ILInstruction _3 = _2.Instruction.StackBeforeStrict[0].SingleStackInstruction;

                if (!_3.IsLoadLocal)
                    goto skip;

                if (_3.TargetVariable.LocalIndex != _1.Instruction.TargetVariable.LocalIndex)
                    goto skip;

                // now we know that this function returns something

                ILInstruction _4 = _1.Instruction.StackBeforeStrict[0].SingleStackInstruction;

                if (_4.OpCode.FlowControl != FlowControl.Call)
                    goto skip;

                MethodBase _4_Method = _4.TargetMethod;

                if (_4_Method == null)
                    goto skip;

                if (!_4_Method.IsStatic)
                    goto skip;



                // now if it has the same argument types and if we pass the same argument values
                // we can semi-inline it.


                ParameterInfo[] tp = _4_Method.GetParameters();
                ParameterInfo[] mp = zm.GetParameters();

                if (tp.Length != mp.Length)
                    goto skip;

                for (int j = 0; j < tp.Length; j++)
                {
                    if (tp[j].ParameterType != mp[j].ParameterType)
                        goto skip;

                }

                for (int i = 0; i < tp.Length; i++)
                {
                    ParameterInfo v = _4.StackBeforeStrict[i].SingleStackInstruction.TargetParameter;

                    if (v == null)
                        goto skip;

                    if (v.Position != i)
                        goto skip;
                }

                w.WriteIdent();
                w.Write("var ");
                w.WriteDecoratedMethodName(zm);
                w.Write(" = ");


                if (_4_Method.DeclaringType.Assembly == zm.DeclaringType.Assembly)
                {
                    w.Write("function () { ");

                    if (zm.ReturnType != typeof(void))
                    {
                        w.Write("return ");
                    }

                    w.WriteDecoratedMethodName(_4_Method);
                    w.Write(".apply(null, arguments);");
                    w.Write(" }");
                }
                else
                {
                    w.WriteDecoratedMethodName(_4_Method);
                }

                w.WriteLine(";");


                return true;
            }

        skip:

            return false;


        }

        private static string DoCompilerConstants(MethodInfo zm, ScriptAttribute zsa)
        {
            string code = zsa.OptimizedCode;

            if (zsa.UseCompilerConstants)
            {
                code = CompilerBase.ReplaceWithCompilerConstants(code, zm, IdentWriter.GetParameterInfoNameField);
            }
            return code;
        }



        public static void DeclareFields(IdentWriter w, FieldInfo[] fi, Type type)
        {


            #region static fields
            foreach (FieldInfo zf in fi)
            {
                if (zf.IsLiteral)
                    continue;

                if (!zf.IsStatic)
                    continue;

                w.WriteIdent();
                w.Write("var ");

                w.WriteDecoratedMemberInfo(zf);
                w.Helper.WriteAssignment();

                DeclareFieldDefaultValue(w, zf);

                w.WriteLine(";");
            }
            #endregion


            //todo: to be optimized to with statement or inline function just like virtual
            // and instead of explicitly setting the constructor one can use the default prototype
            /// of the base$ctor

            int counter = 0;

            __handler Before =
                delegate
                {
                    w.WriteIdent();
                    //w.Write(CopyMembers);
                    //w.Write("(");
                    w.Helper.WritePrototypeAlias(type);
                    w.Helper.WriteAssignment();
                    w.WriteDecorated(type);
                    w.Helper.WriteAccessor();
                    w.Helper.WritePrototype();
                    w.Helper.WriteAssignment();

                    w.WriteBeginScope();

                    // hack
                    counter++;
                    w.WriteIdent();
                    w.Write("constructor: ");
                    w.WriteDecoratedType(type, false);
                    w.WriteLine(",");

                };




            foreach (FieldInfo zf in fi)
            {
                if (zf.IsLiteral)
                    continue;

                if (zf.IsStatic)
                    continue;


                ScriptAttribute s = ScriptAttribute.Of(zf.DeclaringType);

                if (s != null && s.HasNoPrototype)
                    continue;

                if (counter++ > 0)
                    w.WriteLine(",");
                else
                {
                    Before();
                }


                w.WriteIdent();
                w.WriteDecoratedMemberInfo(zf);
                w.Write(": ");
                DeclareFieldDefaultValue(w, zf);


            }

            // instance methods are also fields
            // only declared methods can be considered
            //MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);

            //foreach (MethodInfo v in methods)
            //{
            //    w.WriteCommentLine("" + v);

            //}

            if (counter > 0)
            {
                w.WriteLine();
                w.WriteEndScope();
                w.WriteLine(";");
            }


            // whatif hasnoprototype?
            if (type.IsSerializable)
            {
                #region type.prototype.meta = {};
                w.Helper.WriteOptionalIdent();
                w.Helper.WritePrototypeAlias(type);

                //w.WriteDecorated(type);
                //w.Helper.WriteAccessor();
                //w.Write(ScriptAttribute.Prototype);
                w.Helper.WriteAccessor();
                w.Write(ScriptAttribute.MetadataMember);
                w.Helper.WriteAssignment();
                w.Helper.WriteCreateObject();
                w.Helper.WriteTerminator();
                w.Helper.WriteOptionalNewline();
                #endregion


                #region type.prototype.meta.typename = 'typename';
                w.Helper.WriteOptionalIdent();
                w.Helper.WritePrototypeAlias(type);
                //w.WriteDecorated(type);
                //w.Helper.WriteAccessor();
                //w.Helper.WritePrototype();
                w.Helper.WriteAccessor();
                w.Write(ScriptAttribute.MetadataMember);
                w.Helper.WriteAccessor();
                w.Write(ScriptAttribute.MetadataMemberTypeName);
                w.Helper.WriteAssignment();
                w.Write("'" + type.Name + "'");
                w.Helper.WriteTerminator();
                w.Helper.WriteOptionalNewline();
                #endregion

                #region type.prototype.meta.defaultctor = 'defaultctor';

                MethodBase ctor = type.GetConstructor(Type.EmptyTypes);

                if (ctor != null)
                {
                    // metadata support?

                    w.Helper.WriteOptionalIdent();
                    w.Helper.WritePrototypeAlias(type);

                    //w.WriteDecorated(type);
                    //w.Helper.WriteAccessor();
                    //w.Helper.WritePrototype();
                    w.Helper.WriteAccessor();
                    w.Write(ScriptAttribute.MetadataMember);
                    w.Helper.WriteAccessor();
                    w.Write(ScriptAttribute.MetadataMemberDefaultConstructor);
                    w.Helper.WriteAssignment();
                    w.WriteDecoratedMemberInfo(ctor, true);
                    w.Helper.WriteTerminator();

                    w.Helper.WriteOptionalNewline();
                }

                #endregion

                // usage?
                // !!! xml serialization

                foreach (FieldInfo zf in fi)
                {
                    if (zf.IsLiteral)
                        continue;

                    if (zf.FieldType.IsEnum)
                        continue;

                    if (zf.IsStatic)
                        continue;

                    // TODO: add primitive type constructors
                    // string, number, boolean

                    if (zf.FieldType.IsArray)
                    {
                        #region type.prototype.meta.field = [];
                        w.Helper.WriteOptionalIdent();
                        w.WriteDecorated(type);
                        w.Helper.WriteAccessor();
                        w.Helper.WritePrototype();
                        w.Helper.WriteAccessor();
                        w.Write(ScriptAttribute.MetadataMember);
                        w.Helper.WriteAccessor();
                        w.WriteDecoratedMemberInfo(zf);
                        w.Helper.WriteAssignment();
                        w.Helper.WriteCreateArray();
                        w.Helper.WriteTerminator();
                        w.Helper.WriteOptionalNewline();
                        #endregion

                        continue;
                    }

                    ScriptAttribute sf = ScriptAttribute.Of(zf.FieldType);

                    if (sf != null)
                    {
                        #region type.prototype.meta.field = 'ctor';
                        w.Helper.WriteOptionalIdent();
                        w.WriteDecorated(type);
                        w.Helper.WriteAccessor();
                        w.Helper.WritePrototype();
                        w.Helper.WriteAccessor();
                        w.Write(ScriptAttribute.MetadataMember);
                        w.Helper.WriteAccessor();
                        w.WriteDecoratedMemberInfo(zf);
                        w.Helper.WriteAssignment();
                        w.Write("'");
                        w.Write(zf.FieldType.Name);
                        w.Write("'");
                        w.Helper.WriteTerminator();
                        w.Helper.WriteOptionalNewline();
                        #endregion

                    }

                }

                w.Helper.WriteOptionalNewline();
            }
        }

        private static void DeclareFieldDefaultValue(IdentWriter w, FieldInfo zf)
        {

            if (zf.FieldType == typeof(int))
            {
                w.Write("0");
            }
            else if (zf.FieldType == typeof(long))
            {
                w.Write("0");
            }
            else if (zf.FieldType == typeof(bool))
            {
                w.Write("false");
            }
            else if (zf.FieldType == typeof(string))
            {
                w.Write("null");
            }
            else if (zf.FieldType.IsEnum)
            {
                w.Write("0");
            }
            else if (zf.FieldType is object)
            {
                w.Write("null");
            }
        }


        static Type[] SortTypes(IdentWriter w, Type[] e)
        {
            List<Type> a = new List<Type>(e);

            while (e.Length > 0)
            {
                //Console.WriteLine("---");;
                List<Type> r = new List<Type>();

                foreach (Type v in e)
                {
                    // find my base and align after the base

                    Type b = v.BaseType;

                    if (b == null)
                        continue;

                    if (b.IsGenericType) 
                        b = b.GetGenericTypeDefinition()
                        ;

                    b = w.Session.ResolveImplementation(b) ?? b;

                    int x = a.IndexOf(b);
                    int y = a.IndexOf(v);

                    if (y < x)
                    {
                        //Console.WriteLine("move {0} after {1} at {2}", v, b, x);

                        a.Insert(x + 1, v);
                        a.RemoveAt(y);

                        r.Add(v);
                    }
                }


                e = r.ToArray();

            }


            return a.ToArray();
        }

        public static void DeclareTypes(IdentWriter w, Type[] arg_types, bool debug, ScriptAttribute attribute)
        {
            Type[] types = SortTypes(w, arg_types);

            w.Ident++;

            if (attribute.IsCoreLib)
            {
                // declare file scoped inheritance class builder
                w.WriteLine(new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("jsc.Languages.JavaScript.$ctor$.js")).ReadToEnd());
            }

            //Type[] test = {
            //    types[4],
            //    types[0],
            //    w.Session.ResolveImplementation(typeof(System.EventHandler)),
            //    types[1],
            //    w.Session.ResolveImplementation(typeof(System.Delegate)) ,
            //    types[2],
            //    w.Session.ResolveImplementation(typeof(System.MulticastDelegate)),
            //    types[3],
            //};

            //test = SortTypes(w, test);




            int i0 = Array.IndexOf(types, w.Session.ResolveImplementation(typeof(Delegate)));
            int i1 = Array.IndexOf(types, w.Session.ResolveImplementation(typeof(MulticastDelegate)));
            int i2 = Array.IndexOf(types, w.Session.ResolveImplementation(typeof(EventHandler)));

            if (i2 < i0 || i1 < i0)
                throw new Exception();

            foreach (Type z in types)
            {
                Console.WriteLine(z.FullName);
                ////w.WriteCommentLine(z.FullName);

                if (!z.IsClass)
                    continue;

                ScriptAttribute sa = ScriptAttribute.Of(z);


                if (z.BaseType == typeof(System.MulticastDelegate))
                {
                    if (sa != null)
                    {
                        // we have a delegate with a script attribute
                        // now we have to write the implementation for it

                        jsc.Languages.JavaScript.legacy.DelegateImplementationProvider.Write(w, z);
                    }

                    continue;
                }

                #region compilergenerated

                //if (ScriptAttribute.IsCompilerGenerated(z))

                if (sa == null)
                {
                    if (ScriptAttribute.IsAnonymousType(z))
                    {
                        w.WriteIdent();
                        w.WriteCommentLine("Anonymous type");
                    }
                    else
                    {
                        w.WriteCommentLine("Closure type");
                    }

                    w.Helper.DOMDefineNamedType(z);


                    DeclareFields(w, z.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Public), z);
                    DeclareMethods(w, z.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Public));

                    continue;
                }

                #endregion



                if (sa != null)
                {

                    if (!sa.IsDebugCode && debug)
                        continue;

                    Task.Enabled = true;
                    Task.WriteLine("type: [{0}]", z.FullName);


                    //using (new Task("DeclareTypes", z.FullName))
                    //{


                    #region prototype
                    //w.WriteCommentLine("prototype");

                    bool IsStaticClass = z.IsAbstract && z.IsSealed;

                    if (!sa.HasNoPrototype && !IsStaticClass)
                    {

                        Type __inherit_from = z.BaseType;

                        if (__inherit_from == typeof(object) && sa.Implements == null)
                        {
                            __inherit_from = w.Session.ResolveImplementation(__inherit_from);
                        }


                        w.Helper.DOMDefineNamedType(z, __inherit_from);
                        w.Helper.DefineAndAssignPrototype(z);


                    }
                    //w.WriteCommentLine("endprototype");
                    #endregion


                    #region instance fields
                    //w.WriteCommentLine("fields");

                    DeclareFields(w, z.GetFields(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public), z);


                    //w.WriteCommentLine("endfields");
                    #endregion

                    #region instance constructors

                    if (!sa.HasNoPrototype && !IsStaticClass)
                    {
                        w.Helper.DefineTypeInheritanceConstructor(z, z.BaseType);
                    }

                    //if (sa.Implements == null)
                    {
                        ConstructorInfo[] ci = z.GetConstructors(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

                        foreach (ConstructorInfo zc in ci)
                        {


                            if (zc.IsStatic)
                            {
                                continue;
                                //  IL2Script.EmitBody(w, zc);
                            }
                            else
                            {
                                if (sa.HasNoPrototype)
                                {
                                    continue;
                                }


                                ScriptAttribute zsa = ScriptAttribute.Of(zc);

                                Task.Enabled = zsa != null && zsa.IsDebugCode;

                                w.WriteCommentLine(zc.DeclaringType.FullName + "." + zc.Name);

                                w.Helper.DefineTypeMemberMethodHeader(z, zc);

                                w.WriteBeginScope();
                                IL2Script.EmitBody(w, zc, true);
                                w.EndScopeAndTerminate();

                                // alias
                                w.Helper.DefineTypeInheritanceConstructor(z, zc, z.BaseType);





                            }

                            w.WriteLine();

                        }
                    }
                    #endregion



                    //w.WriteCommentLine("methods");
                    MethodInfo[] mi = z.GetMethods(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

                    DeclareMethods(w, mi);



                    DeclareVirtualMethods(w, z);


                    //}
                }



            }

            Console.WriteLine();

            DeclareStaticConstructors(w, types, debug);

            w.Ident--;


        }




    }




}