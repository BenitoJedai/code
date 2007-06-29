using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml;

using IntPtr = global::System.IntPtr;

using ScriptCoreLib;

using jsc.Script;


namespace jsc.Languages.C
{
    class CCompiler : CompilerCLike
    {
        public string HeaderFileName;

        public bool IsHeaderOnlyMode;

        public readonly AssamblyTypeInfo MySession;

        public CCompiler(TextWriter xw, AssamblyTypeInfo xs)
            : base(xw)
        {

            MySession = xs;

            CreateInstructionHandlers();
        }


        private void CreateInstructionHandlers()
        {
            #region Ldftn
            CIW[OpCodes.Ldftn] =
                delegate(CodeEmitArgs e)
                {
                    WriteDecoratedMethodName(e.i.TargetMethod, false);
                };
            #endregion

            CIW[OpCodes.Dup] = delegate(CodeEmitArgs e) { EmitFirstOnStack(e); };
            #region Castclass

            CIW[OpCodes.Castclass] =
                    delegate(CodeEmitArgs e)
                    {
                        //EmitFirstOnStack(e);
                        
                        Type tc = e.i.TargetType;

                        WriteTypeCastAndEmit(e, tc);

                    };
            #endregion

            CIW[OpCodes.Conv_U2] =
               delegate(CodeEmitArgs e) { ConvertTypeAndEmit(e, "unsigned char"); };

            CIW[OpCodes.Conv_I4] =
                delegate(CodeEmitArgs e) { ConvertTypeAndEmit(e, "signed int"); };

            CIW[OpCodes.Conv_I8] =
                delegate(CodeEmitArgs e) { ConvertTypeAndEmit(e, "signed long"); };
            CIW[OpCodes.Conv_U8] =
    delegate(CodeEmitArgs e) { ConvertTypeAndEmit(e, "unsigned long"); };


            CIW[OpCodes.Initobj] =
                delegate(CodeEmitArgs e)
                {
                    if (!e.p.IsInlineAssigment)
                    {
                        WriteVariableName(e.Method.DeclaringType, e.Method, e.i.Prev.TargetVariable);
                        WriteAssignment();
                    }

                    Write("NULL");
                };
            

            CIW[OpCodes.Br_S,
                OpCodes.Br] =
                delegate(CodeEmitArgs e)
                {
                    if (e.i.TargetFlow.Branch == OpCodes.Ret)
                    {
                        // xxx: fix needed

                        WriteReturn(e.p, e.i);
                    }
                    else Break("invalid br opcode");
                };

            #region elem_ref
            CIW[OpCodes.Ldelem_Ref,
                OpCodes.Ldelem_U1,
                OpCodes.Ldelem_U2,
                OpCodes.Ldelem_I1,
                OpCodes.Ldelem_I4,
                OpCodes.Ldelem
                ] =
                delegate(CodeEmitArgs e)
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
                OpCodes.Stelem_I4,
                OpCodes.Stelem
                ] =
                delegate(CodeEmitArgs e)
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

            //#region Ldarg
            //CIW[OpCodes.Ldarg_0,
            //    OpCodes.Ldarg_1,
            //    OpCodes.Ldarg_2,
            //    OpCodes.Ldarg_3,
            //    OpCodes.Ldarg_S,
            //    OpCodes.Ldarg] =
            //    delegate(CodeEmitArgs e)
            //    {
            //        WriteMethodParameterOrSelf(e.i);
            //    };
            //#endregion



            CIW[OpCodes.Ldnull] =
                delegate(CodeEmitArgs e) { Write("NULL"); };

            CIW[OpCodes.Ldlen] =
                delegate(CodeEmitArgs e)
                {
                    CompilerBase.BreakToDebugger("c runtime cannot tell the length of an array");
                };


            CIW[OpCodes.Callvirt] =
                delegate(CodeEmitArgs e)
                {
                    WriteMethodCall(e.p, e.i, e.i.TargetMethod);
                };

            #region Ldarg
            CIW[OpCodes.Ldarg_0,
                OpCodes.Ldarg_1,
                OpCodes.Ldarg_2,
                OpCodes.Ldarg_3,
                OpCodes.Ldarg_S,
                OpCodes.Ldarg] =
                delegate(CodeEmitArgs e)
                {
                    WriteMethodParameterOrSelf(e.i);
                };
            #endregion
            #region passthru

            CIW[

                OpCodes.Unbox_Any,
                OpCodes.Pop,
                OpCodes.Box,
                OpCodes.Isinst] = CodeEmitArgs.DelegateEmitFirstOnStack;

            #endregion

            #region Newobj
            CIW[OpCodes.Newobj] =
                delegate(CodeEmitArgs e)
                {
                    WriteTypeConstruction(e);

                };
            #endregion

            CIW[OpCodes.Newarr] =
                delegate(CodeEmitArgs e)
                {
                    WriteTypeConstruction(e);



                };

            #region fld
            CIW[OpCodes.Ldfld] =
                delegate(CodeEmitArgs e)
                {

                    Emit(e.p, e.FirstOnStack);
                    Write("->");
                    Write(e.i.TargetField.Name);

                };

            CIW[OpCodes.Stfld] =
                delegate(CodeEmitArgs e)
                {
                    ILFlow.StackItem[] s = e.i.StackBeforeStrict;

                    Emit(e.p, s[0]);
                    Write("->");
                    Write(e.i.TargetField.Name);
                    WriteAssignment();


                    #region  assign boolean literal
                    /*
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
                    }  */
                    #endregion

                    Emit(e.p, s[1]);
                };
            #endregion

            #region Ldstr
            CIW[OpCodes.Ldstr] =
                delegate(CodeEmitArgs e)
                {
                    WriteLiteral(e.i.TargetLiteral);
                };
            #endregion


            CIW[OpCodes.Ldsfld] =
                delegate(CodeEmitArgs e)
                {
                    ILFlow.StackItem[] s = e.i.StackBeforeStrict;

                    WriteDecoratedTypeName(e.i.TargetField.DeclaringType);
                    Write("_");
                    Write(e.i.TargetField.Name);
                };

            //#region sfld
            //CIW[OpCodes.Ldsfld] =
            //    delegate(CodeEmitArgs e)
            //    {
            //        ILFlow.StackItem[] s = e.i.StackBeforeStrict;

            //        WriteDecoratedTypeName(e.i.TargetField.DeclaringType);
            //        WriteTypeStaticAccessor();
            //        WriteDecoratedField(e.i.TargetField, true);
            //    };

            //CIW[OpCodes.Stsfld] =
            //    delegate(CodeEmitArgs e)
            //    {
            //        try
            //        {
            //            WriteDecoratedTypeName(e.i.TargetField.DeclaringType);
            //            WriteTypeStaticAccessor();
            //            WriteDecoratedField(e.i.TargetField, true);
            //            WriteAssignment();


            //            if (EmitEnumAsStringSafe(e))
            //                return;

            //            Emit(e.p, e.FirstOnStack);
            //        }
            //        catch (Exception exc)
            //        {
            //            throw exc;
            //        }
            //    };
            //#endregion

            #region  operands
            CIW[OpCodes.Xor] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "^"); };
            CIW[OpCodes.Shl] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "<<"); };
            CIW[OpCodes.Shr] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, ">>"); };
            CIW[OpCodes.Clt] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "<"); };
            CIW[OpCodes.Cgt] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, ">"); };
            CIW[OpCodes.Blt_S] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "<"); };
            CIW[OpCodes.Bgt_S] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, ">"); };
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
                    ILFlow.StackItem[] s = e.i.StackBeforeStrict;

                    bool b = false;

                    if (s[1].SingleStackInstruction == OpCodes.Ldc_I4_0
                        && s[0].SingleStackInstruction.ReferencedType == typeof(bool))
                        b = true;



                    if (s[1].SingleStackInstruction == OpCodes.Ldc_I4_0
                        && s[0].SingleStackInstruction.IsBooleanOpcode)
                        b = true;

                    if (b)
                    {
                        Write("!");
                        Emit(e.p, e.i.StackBeforeStrict[0]);
                    }
                    else
                        WriteInlineOperator(e.p, e.i, "==");
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
               delegate(CodeEmitArgs e)
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


                   if (e.p.Owner.IsCompound)
                   {
                       // redundant to inline?

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

            #region ldc
            CIW[OpCodes.Ldc_R8] =
                delegate(CodeEmitArgs e)
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
               delegate(CodeEmitArgs e)
               {
                   int? n = e.i.TargetInteger;


                   if (n == null)
                       Break("ldc unresolved");

                   MyWriter.Write(n.Value);
               };
            #endregion

            #region Ret
            CIW[OpCodes.Ret] =
                delegate(CodeEmitArgs e)
                {
                    WriteReturn(e.p, e.i);
                };
            #endregion

            #region Stloc
            CIW[OpCodes.Stloc_0,
                OpCodes.Stloc_1,
                OpCodes.Stloc_2,
                OpCodes.Stloc_3,
                OpCodes.Stloc_S,
                OpCodes.Stloc] =
                delegate(CodeEmitArgs e)
                {
                    WriteVariableName(e.i.OwnerMethod.DeclaringType, e.i.OwnerMethod, e.i.TargetVariable);

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

                    WriteAssignment();

                    if (e.i.IsFirstInFlow && e.i.Flow.OwnerBlock.IsHandlerBlock)
                    {
                        WriteExceptionVar();
                        return;
                    }

                    //if (EmitEnumAsStringSafe(e))
                    //    return;

                    //#region  assign boolean literal
                    //if (e.i.TargetVariable.LocalType == typeof(bool))
                    //{
                    //    if (e.i.StackBeforeStrict[0].StackInstructions.Length == 1)
                    //    {
                    //        if (e.i.StackBeforeStrict[0].SingleStackInstruction.TargetInteger != null)
                    //        {
                    //            if (e.i.StackBeforeStrict[0].SingleStackInstruction.TargetInteger == 0)
                    //                WriteKeywordFalse();
                    //            else
                    //                WriteKeywordTrue();

                    //            return;
                    //        }
                    //    }
                    //}
                    //#endregion

                    WriteTypeCastAndEmit(e, e.i.TargetVariable.LocalType);
                };
            #endregion

            #region call
            CIW[OpCodes.Call] =
                delegate(CodeEmitArgs e)
                {
                    MethodBase m = e.i.ReferencedMethod;

                    MethodBase mi = MySession.ResolveImplementation(m.DeclaringType, m);

                    if (mi != null)
                    {
                        WriteMethodCall(e.p, e.i, mi);

                        return;
                    }

                    //if (m.Name == "op_Implicit")
                    //{
                    //    ScriptAttribute sa = ScriptAttribute.Of(m.DeclaringType, false);

                    //    if (sa != null && sa.IsNative)
                    //    {
                    //        // that implicit call is only for to help c# conversions
                    //        // so we must emit first parameter

                    //        EmitFirstOnStack(e);
                    //        return;
                    //    }
                    //}

                    WriteMethodCall(e.p, e.i, m);
                };
            #endregion

        }

        private void WriteTypeCastAndEmit(CodeEmitArgs e, Type tc)
        {
            Write("((");

            Write(GetDecoratedTypeName(tc, true, true));
            Write(")");
            EmitFirstOnStack(e);
            Write(")");
        }

        public override ScriptType GetScriptType()
        {
            return ScriptType.C;
        }

        public override bool CompileType(Type z)
        {
            Console.WriteLine(z.FullName);

            ScriptAttribute za = ScriptAttribute.Of(z);

  
            if (za.Implements == null)
            {
                // not all impl types can have ctors...

                WriteTypeInstanceConstructors(z);
            }


            if (!IsHeaderOnlyMode)
            {
                #region static variables
                FieldInfo[] sfields = GetAllFields(z);
                
                foreach (FieldInfo sfield in sfields)
                {
                    if (!sfield.IsStatic)
                        continue;

                    // constants will be inlined
                    if (sfield.IsLiteral)
                        continue;

                    WriteIdent();

                    Write(GetDecoratedTypeName(sfield.FieldType, false, true));

                    WriteSpace();

                    WriteDecoratedTypeName(z);
                    Write("_");
                    Write(sfield.Name);

                    WriteLine(";");
                }

                WriteLine();
                #endregion
            }

            WriteTypeStaticMethods(z, za);



            WriteTypeInstanceMethods(z, za);

            return true;
        }

        private void WriteTypeInstanceConstructors(Type z)
        {
            ConstructorInfo[] zci = GetAllInstanceConstructors(z);

            foreach (ConstructorInfo zc in zci)
            {
                WriteLine();

                WriteMethodHint(zc);
                WriteMethodSignature(zc, false);

                if (WillEmitMethodBody())
                    WriteMethodBody(zc);

            }

            WriteLine();
        }

        private void WriteTypeDefPrototype(Type e)
        {
            if (IsHeaderOnlyMode)
            {

                ScriptAttribute a = ScriptAttribute.Of(e);


                if (a == null || !a.HasNoPrototype)
                {

                    WriteIdent();
                    Write("typedef struct tag_" + GetDecoratedTypeName(e, false));

                    string _pname = GetPointerName(e);

                    WriteLine(" *" + _pname + ";");
                }
            }
        }


        private void WriteTypeDef(Type e)
        {
            if (!e.IsAbstract)
            {
                ScriptAttribute a = ScriptAttribute.Of(e);

                //if (a.Implements != null)
                //    return;

                WriteLine();

                if (IsHeaderOnlyMode)
                {
                    string _typename = GetDecoratedTypeName(e, false, false);
                    string _pname = GetPointerName(e);

                    #region instance struct

                    if (a == null || !a.HasNoPrototype)
                    {
                        #region typedef
                        WriteIdent();
                        WriteLine("typedef struct tag_" + GetDecoratedTypeName(e, false));

                        using (CreateScope(false))
                        {
                            Stack<Type> u = new Stack<Type>();

                            Type p = e;

                            while (p != typeof(object))
                            {
                                u.Push(p);
                                p = p.BaseType;
                            }

                            while (u.Count > 0)
                            {
                                p = u.Pop();

                                FieldInfo[] fields = GetAllFields(p);

                                if (fields.Length == 0)
                                {
                                    WriteIdent();
                                    WriteLine("void* __dummy;");

                                }
                                else
                                {
                                    foreach (FieldInfo field in fields)
                                    {
                                        if (field.IsStatic)
                                            continue;

                                        WriteIdent();
                                        Write(GetDecoratedTypeName(field.FieldType, false, true));
                                        WriteSpace();
                                        Write(field.Name);
                                        WriteLine(";");
                                    }
                                }
                            }
                        }
                        WriteLine(" " + _typename + ", *" + _pname + ";");
                        #endregion
                    }


                    
                    WriteLine("#define __new_" + _typename + "(count) \\");
                    WriteLine("    (" + _pname + ") malloc(sizeof(" + _typename + ") * count)");
                    
                    WriteLine();

                    #endregion
                }
            }
        }

        public string GetPointerName(Type e)
        {
            

            ScriptAttribute a = ScriptAttribute.Of(e);

            if (a == null || a.PointerName == null)
            {
                string _typename = GetDecoratedTypeName(e, false, false);
                string _pname = "LP" + _typename;

                return _pname;
            }
            else
                return a.PointerName;
            
        }

        public override void WriteTypeConstructionVerified(CodeEmitArgs e, Type mtype, MethodBase m, ScriptAttribute mza)
        {
            string type = GetDecoratedTypeName(mtype, false, false);

            if (e.OpCode == OpCodes.Newobj)
            {
                WriteDecoratedMethodName(m, false);
                Write("(");
            }


            ScriptAttribute a = ScriptAttribute.Of(mtype);

            if (a == null)
            {
                a = ScriptAttribute.Of(ResolveImplementation(mtype));
            }

            if (a == null)
            {
                Write("(" + type + "*) malloc(sizeof(" + type + ")");

                if (e.OpCode == OpCodes.Newarr)
                {
                    Write(" * ");

                    EmitFirstOnStack(e);
                }

                Write(")");
            }
            else
            {
                Write("__new_" + type + "(");
                if (e.OpCode == OpCodes.Newarr)
                {
                    EmitFirstOnStack(e);
                }
                else
                {
                    Write("1");
                }
                Write(")");
            }


            

            if (e.OpCode == OpCodes.Newobj)
            {
                WriteParameters(e.p, m, e.i.StackBeforeStrict, 0, m.GetParameters(), true, ",");

                Write(")");
            }
        }

        public override void WriteTypeSignature(Type z, ScriptAttribute za)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void WriteTypeFields(Type z, ScriptAttribute za)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void WriteTypeFieldModifier(FieldInfo zfn)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void WriteXmlDoc(MethodBase m)
        {
            if (this.XmlDoc != null)
            {
                ParameterInfo[] param = m.GetParameters();

                string MethodSig = "M:" + m.DeclaringType.Namespace + "." + m.DeclaringType.Name + "." + m.Name;

                if (param.Length > 0)
                {
                    int i = 0;
                    MethodSig += "(";

                    foreach (ParameterInfo MethodParam in param)
                    {
                        if (i++ > 0)
                            MethodSig += ",";

                        MethodSig += MethodParam.ParameterType.FullName;
                    }

                    MethodSig += ")";
                }

                XmlNode n = this.XmlDoc.SelectSingleNode(@"//members/member[@name='" + MethodSig + "']");

                if (n != null)
                {
                    string Summary = n["summary"].InnerText.Trim();

                    WriteJavaDoc(Summary);
                }
                else
                {
                 //   WriteJavaDoc(MethodSig);
                }
            }
        }

        public override Predicate<ILBlock.Prestatement> MethodBodyFilter 
        {
            get
            {
                return 
                 delegate(ILBlock.Prestatement p)
                 {
                     // note that instance constructor returns pointer to instance

                     #region remove redundant returns
                     if (p.Instruction != null)
                         if (p.Instruction == OpCodes.Ret)
                             if (p.Instruction.Next == null)
                                 if (p.Instruction.StackBeforeStrict.Length == 0)
                                     if (!p.Instruction.OwnerMethod.IsConstructor)
                                     {
                                         return true;
                                     }
                     #endregion

                     return false;
                 };
            }
        }


        public override void WriteTypeInstanceMethods(Type z, ScriptAttribute za)
        {
            MethodInfo[] mx = GetAllInstanceMethods(z);

            int idx = 0;

            foreach (MethodInfo m in mx)
            {
                ScriptAttribute ma = ScriptAttribute.Of(m);

                bool dStatic = AlwaysDefineAsStatic || (ma != null && ma.DefineAsStatic);

                if (ma != null && (ma.IsNative || ma.ExternalTarget != null))
                    continue;

                if (ma == null && !m.IsStatic && (za.HasNoPrototype))
                    continue;

                if (idx++ > 0)
                    WriteLine();

                WriteMethodHint(m);
                WriteXmlDoc(m);
                WriteMethodSignature(m, dStatic);

                if (WillEmitMethodBody())
                if (!ScriptIsPInvoke(m))
                if (!m.IsAbstract) WriteMethodBody(m, MethodBodyFilter);

            }
        }

       

        public override string GetDecoratedTypeName(Type z, bool bExternalAllowed)
        {
            return GetDecoratedTypeName(z, bExternalAllowed, false);
        }

        public override bool WillEmitMethodBody()
        {
            return !this.IsHeaderOnlyMode;
        }

        public override void WriteMethodSignature(MethodBase m, bool dStatic)
        {
            WriteIdent();

            if (m is MethodInfo)
            {
                MethodInfo mi = m as MethodInfo;

                //WriteDecoratedTypeName(mi.ReturnType);
                Write(GetDecoratedTypeName(mi.ReturnType, true, true));
                //Write(GetDecoratedTypeNameWithinNestedName( mi.ReturnType));
               
            }
            else
            {
                Write(GetDecoratedTypeName(m.DeclaringType, true, true));
            }

            WriteSpace();

            WriteDecoratedMethodName(m, false);

            Write("(");
            WriteMethodParameterList(m);
            Write(")");

            if (!WillEmitMethodBody())
            {
                Write(";");
            }


            WriteLine();
        }

        public override void WriteMethodCallVerified(ILBlock.Prestatement p, ILInstruction i, MethodBase m)
        {
            ScriptAttribute s = ScriptAttribute.Of(m.DeclaringType);

            if (s == null)
            {
                WriteDecoratedMethodName(ResolveImplementationMethod(m.DeclaringType, m), false);
            }
            else
            {
                WriteDecoratedMethodName(m, false);
            }

            int offset = 1;

            if (m.IsStatic)
                offset = 0;

            WriteParameterInfoFromStack(m, p, i.StackBeforeStrict, offset);
        }

        public override void MethodCallParameterTypeCast(ParameterInfo p)
        {
            Write("(");
            Write(GetDecoratedTypeName(p.ParameterType, true, true));
            Write(")");
        }

        public override bool AlwaysDefineAsStatic
        {
            get
            {
                return true;
            }
        }

        public bool HideParameterNameInHeaderFiles = true;

        public override void WriteMethodParameterList(MethodBase m)
        {
            ParameterInfo[] mp = m.GetParameters();

            ScriptAttribute ma = ScriptAttribute.Of(m);

            bool bStatic = (!m.IsStatic && AlwaysDefineAsStatic) || (ma != null && ma.DefineAsStatic);

            if (bStatic)
            {
                if (m.IsStatic)
                {
                    Break("method is already static, but is marked to be declared out of band : " + m.DeclaringType.FullName + "." + m.Name);
                }

                DebugBreak(ma);


                ScriptAttribute sa = ScriptAttribute.Of(m.DeclaringType, false);

                if (sa.Implements == null)
                {
                    Write(GetDecoratedTypeName(m.DeclaringType, true, true));

                }
                else
                {
                    Write(GetDecoratedTypeName(sa.Implements, true, true));
                }

                if (this.IsHeaderOnlyMode)
                {
                    if (!HideParameterNameInHeaderFiles)
                    {
                        WriteSpace();
                        Write("/* ");
                        WriteSelf();
                        Write(" */");
                    }
                }
                else
                {
                    WriteSpace();
                    WriteSelf();
                }
            }
            else
            {
                if (mp.Length == 0)
                    Write("void");
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

                if (za.Implements == null || m.DeclaringType.GUID != p.ParameterType.GUID)
                    Write(GetDecoratedTypeName(p.ParameterType, true, true));
                else
                    Write(GetDecoratedTypeName(za.Implements, true, true));

                if (this.IsHeaderOnlyMode)
                {
                    if (!HideParameterNameInHeaderFiles)
                    {
                        WriteSpace();
                        Write("/* ");
                        Write(p.Name);
                        Write(" */");
                    }
                }
                else
                {
                    WriteSpace();
                    Write(p.Name);
                }

            }
        }

        public override void WriteSelf()
        {
            Write("that");
        }

        public override void EmitPrestatement(ILBlock.Prestatement p)
        {
            WriteIdent();
            //WriteBoxedComment(p.Instruction.Flow.ToString());

            EmitInstruction(p, p.Instruction);
            WriteLine(";");
        }

        

        public override Type[] GetActiveTypes()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public Type ResolveImplementation(Type t)
        {
            return MySession.ResolveImplementation(t); ;
        }

        public override MethodBase ResolveImplementationMethod(Type t, MethodBase m)
        {
            return MySession.ResolveImplementation(t, m); ;
        }

        public override MethodBase ResolveImplementationMethod(Type t, MethodBase m, string alias)
        {
            return MySession.ResolveMethod(m, t, alias);
        }

        public override bool EmitTryBlock(ILBlock.Prestatement p)
        {
            // no exceptions in c, so used only for paired actions

                    BreakToDebugger("no try/catch in c");

              
            return true;
        }

        public override void WriteDecoratedMethodName(MethodBase z, bool q)
        {

            if (q)
                Write("\"");

            ScriptAttribute s = ScriptAttribute.Of(z);

            bool x = true;

            if (s != null)
            {
                if (s.NoDecoration)
                    x = false;
            }

            s = ScriptAttribute.Of(z.DeclaringType);

            if (s != null)
            {
                if (s.IsNative)
                    x = false;
            }

            if (x)
            {
                Write(GetDecoratedTypeName(z.DeclaringType, true, false));
                Write("_");
            }

            Write(z.Name.Replace(".", "_"));

            if (z.Name == "op_Implicit")
            {
                Write("_");
                WriteDecoratedTypeName(((MethodInfo)z).ReturnType);
            }

            if (x && (!z.IsStatic || z.GetParameters().Length > 0))
            {
                if (IsOverloadedMethod(z))
                {
                    Write("_");
                    Write(TokenToString(z.MetadataToken));
                }
            }

            if (q)
                Write("\"");
        }



        public override void WriteDecoratedMethodParameter(ParameterInfo p)
        {
            Write(p.Name);
        }


        public void Compile()
        {
            WriteMachineGeneratedWarning();

            // we will need to specify the headers

            if (IsHeaderOnlyMode)
            {
                #region write include headers
                foreach (Type u in this.MySession.Types)
                {
                    ScriptAttribute s = ScriptAttribute.Of(u);

                    if (s == null)
                        continue;

                    if (s.IsNative)
                        if (s.Header != null)
                        {
                            Write("#include ");
                            Write(s.IsSystemHeader ? '<' : '"');
                            Write(s.Header);
                            Write(s.IsSystemHeader ? '>' : '"');
                            WriteLine();
                        }



                }
                #endregion

            }
            else
            {
                WriteLine("#include \"" + HeaderFileName + "\"");
            }

            foreach (Type u in this.MySession.Types)
            {
                ScriptAttribute s = ScriptAttribute.Of(u);

                // native types are just a placehodlers, so we skip em
                if (s == null || s.IsNative)
                    continue;

                WriteTypeDefPrototype(u);
            }

            foreach (Type u in this.MySession.Types)
            {
                ScriptAttribute s = ScriptAttribute.Of(u);

                // native types are just a placehodlers, so we skip em
                if (s == null || s.IsNative)
                    continue;

                WriteTypeDef(u);
            }

            

            foreach (Type u in this.MySession.Types)
            {
                ScriptAttribute s = ScriptAttribute.Of(u);

                // native types are just a placehodlers, so we skip em
                if (s == null || s.IsNative)
                    continue;

                CompileType(u);
            }

        }


        public override void WriteLocalVariableDefinition(LocalVariableInfo v, MethodBase u)
        {
            WriteIdent();
            Write(GetDecoratedTypeName(v.LocalType, false, true));
            WriteSpace();
            WriteVariableName(u.DeclaringType, u, v);

            WriteLine(";");
        }


        public override bool AlwaysDoTypeCastOnParameters
        {
            get
            {
                return true;
            }
        }

        public override bool IsBooleanSupported
        {
            get
            {
                return false;
            }
        }

        public override bool WillReturnPointerToThisOnConstructorReturn
        {
            get
            {
                return true;
            }
        }


        public string GetDecoratedTypeName(Type z, bool bExternalAllowed, bool bPointer)
        {
            if (z.IsArray)
            {
                return GetDecoratedTypeName(z.GetElementType(), true) + "*";
            }

            if (z == typeof(IntPtr)) return "void*";
            if (z == typeof(object)) return "void*";

            if (z == typeof(bool)) return "int";
            if (z == typeof(int)) return "int";
            if (z == typeof(uint)) return "unsigned int";
            if (z == typeof(void)) return "void";
            if (z == typeof(string)) return "char*";
            if (z == typeof(char)) return "unsigned char";
            if (z == typeof(long)) return "signed long";

            ScriptAttribute t = ScriptAttribute.Of(z);

            if (t != null)
            {
                if (t.HasNoPrototype)
                {
                    if (bPointer)
                        return GetPointerName(z);

                    return z.Name;
                }

                if (t.IsNative)
                    return "void*";

                string u = GetSafeTypeFullname(z);

                if (bPointer)
                    return GetPointerName(z);

                return u;
            }

            if (z.IsGenericParameter)
                return "void*";

            Type impl = ResolveImplementation(z);

            if (impl != null)
                return GetDecoratedTypeName(impl, bExternalAllowed, bPointer);
            //if (z.BaseType == typeof(global::System.MulticastDelegate))
            //    return "void*";

            Script.CompilerBase.BreakToDebugger("type not supported: " + z.FullName);

            return "unknown";
        }

        protected override void WriteTypeCast(Type type)
        {
            ScriptAttribute u = ScriptAttribute.Of(type);

            Type p = type;

            if (u != null && u.Implements != null)
                p = u.Implements;


            Write("(");
            Write(GetDecoratedTypeName(p, true, true));
            Write(")");
        }

        public override bool SupportsForStatements
        {
            get
            {
                return true;
            }
        }

        public override bool SupportsInlineAssigments
        {
            get
            {
                return true;
            }
        }

        public override void WriteTypeConstructionVerified()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }


}
