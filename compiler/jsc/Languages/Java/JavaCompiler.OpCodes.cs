
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml;
using System.Threading;

using jsc.CodeModel;

using ScriptCoreLib;
using jsc.Script;

namespace jsc.Languages.Java
{

    partial class JavaCompiler
    {
        private void CreateInstructionHandlers()
        {
            Action<CodeEmitArgs> WriteCall_DebugTrace_Assign_Load =
                e =>
                {
                    #region WriteCall_DebugTrace_Assign_Active
                    if (WriteCall_DebugTrace_Assign_Active)
                    {
                        var ok = false;

                        Action<Type> check = t => ok |= (t != null && t.IsValueType);

                        check(e.i.TargetField == null ? null : e.i.TargetField.FieldType);
                        check(e.i.TargetVariable == null ? null : e.i.TargetVariable.LocalType);
                        check(e.i.TargetParameter == null ? null : e.i.TargetParameter.ParameterType);

                        if (ok)
                        {
                            Write(" [ \" + ");
                            WriteCall_DebugTrace_Assign_Active = false;

                            CIW[e.OpCode](e);

                            WriteCall_DebugTrace_Assign_Active = true;
                            Write(" + \" ] ");
                        }
                    }
                    #endregion
                };

            #region elem_ref
            CIW[OpCodes.Ldelem_Ref,
                OpCodes.Ldelem_U1,
                OpCodes.Ldelem_U2,
                OpCodes.Ldelem_I1,
                OpCodes.Ldelem_I4,
                OpCodes.Ldelem_I8,
                OpCodes.Ldelem_R4,
                OpCodes.Ldelem_R8,
				OpCodes.Ldelema,
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
                OpCodes.Stelem_I4,
                OpCodes.Stelem_R4,
                OpCodes.Stelem_R8
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


			CIW[OpCodes.Ldobj] =
				e =>
				{
					ILFlow.StackItem[] s = e.i.StackBeforeStrict;

					Emit(e.p, s[0]);
				
				};

			CIW[OpCodes.Stobj] =
				e =>
				{
					ILFlow.StackItem[] s = e.i.StackBeforeStrict;

					Emit(e.p, s[0]);

					WriteAssignment();

					Emit(e.p, s[1]);
				};
            #endregion

            CIW[OpCodes.Leave,
                OpCodes.Leave_S] = delegate { BreakToDebugger("return from within try block not yet supported"); };


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
                    else Break("invalid br opcode");
                };


            CIW[OpCodes.Neg] =
                delegate(CodeEmitArgs e)
                {
                    Write("(-(");
                    EmitFirstOnStack(e);
                    Write("))");
                };

            #region Unbox_Any
            CIW[OpCodes.Unbox_Any] =
                e =>
                {
                    if (e.i.TargetType == typeof(int))
                    {
                        Write("((Integer)");
                        EmitFirstOnStack(e);
                        Write(").intValue()");

                        return;
                    }

                    if (e.i.TargetType == typeof(long))
                    {
                        Write("((Long)");
                        EmitFirstOnStack(e);
                        Write(").longValue()");

                        return;
                    }

                    if (e.i.TargetType == typeof(double))
                    {
                        Write("((Double)");
                        EmitFirstOnStack(e);
                        Write(").doubleValue()");

                        return;
                    }

                    if (e.i.TargetType == typeof(bool))
                    {
                        Write("((Boolean)");
                        EmitFirstOnStack(e);
                        Write(").booleanValue()");
                        return;
                    }


                    WriteBoxedComment("unbox " + e.i.TargetType.Name);
                    EmitFirstOnStack(e);
                };
            #endregion

            #region passthru

            CIW[
                OpCodes.Pop] = CodeEmitArgs.DelegateEmitFirstOnStack;

            CIW[
                OpCodes.Ldtoken] = delegate(CodeEmitArgs e)
                {
                    if (e.i.TargetType == null)
                        throw new NotSupportedException("ldtoken");

                    WriteDecoratedTypeName(e.i.TargetType);
                };

            CIW[OpCodes.Isinst] = delegate(CodeEmitArgs e)
            {
                throw new NotSupportedException("a custom TryCast is not yet implemented");
            };

            #endregion

            CIW[OpCodes.Dup] = delegate(CodeEmitArgs e) { EmitFirstOnStack(e); };

            #region fld
            CIW[OpCodes.Ldfld] =
                e =>
                {

                    WriteCall_DebugTrace_Assign_Load(e);

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

            CIW[OpCodes.Castclass] =
                    delegate(CodeEmitArgs e)
                    {
                        //EmitFirstOnStack(e);
                        ConvertTypeAndEmit(e, GetDecoratedTypeName(e.i.TargetType, true, false));
                        //Write("((");

                        //WriteDecoratedTypeName(e.i.TargetType);
                        //Write(")");
                        //EmitFirstOnStack(e);
                        //Write(")");

                    };

            CIW[OpCodes.Box] =
                delegate(CodeEmitArgs e)
                {
                    Write("new ");
                    Write(GetDecoratedTypeName(e.i.TargetType, true, false));
                    Write("(");

                    EmitFirstOnStack(e);

                    Write(")");
                };

            #region conv
			CIW[OpCodes.Conv_I1] = e => ConvertTypeAndEmit(e, "byte");
			CIW[OpCodes.Conv_I2] = e => ConvertTypeAndEmit(e, "short");
            CIW[OpCodes.Conv_U2] = e => ConvertTypeAndEmit(e, "char");
            CIW[OpCodes.Conv_I4] = e => ConvertTypeAndEmit(e, "int");

            CIW[OpCodes.Conv_I8] = e => ConvertTypeAndEmit(e, "long");
            CIW[OpCodes.Conv_U8] = e => ConvertTypeAndEmit(e, "long");

            CIW[OpCodes.Conv_R4] = e => ConvertTypeAndEmit(e, "float");
            CIW[OpCodes.Conv_R8] = e => ConvertTypeAndEmit(e, "double");

            CIW[OpCodes.Conv_U1] = e => ConvertTypeAndEmit(e, "byte");
            CIW[OpCodes.Conv_Ovf_I] = e => ConvertTypeAndEmit(e, "int");
            #endregion

            #region Ldlen
            CIW[OpCodes.Ldlen] =
                delegate(CodeEmitArgs e)
                {
                    EmitFirstOnStack(e);

                    Write(".length");
                };
            #endregion

            #region Newarr
            CIW[OpCodes.Newarr] =
                e =>
                {
                    #region CreateArray
                    Action<Action> CreateArray =
                       a =>
                       {
                           WriteKeywordSpace(Keywords._new);
                           WriteDecoratedTypeName(e.i.TargetType);
                           //WriteGenericTypeName(e.i.OwnerMethod.DeclaringType, e.i.TargetType);

                           Write("[]");
                           WriteSpace();

                           if (a == null)
                           {
                               Write("{");
                               Write("}");
                           }
                           else
                           {
                               WriteLine("{");

                               a();

                               WriteIdent();
                               Write("}");
                           }
                       };
                    #endregion


                    #region inline newarr
                    if (e.p != null && e.p.IsValidInlineArrayInit)
                    {
                        CreateArray(
                            delegate
                            {
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
                            }
                        );
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

                            // Conversion To IEnumrable

                            CreateArray(
                                   delegate
                                   {
                                       WriteIdent();

                                       if (Type == typeof(int))
                                       {
                                           var Values = e.i.NextInstruction.NextInstruction.TargetField.GetValue(null).StructAsInt32Array();


                                           for (int i = 0; i < Values.Length; i++)
                                           {
                                               if (i > 0)
                                                   Write(", ");

                                               Write(Values[i].ToString());
                                           }

                                       }
                                       else if (Type == typeof(uint))
                                       {
                                           var Values = e.i.NextInstruction.NextInstruction.TargetField.GetValue(null).StructAsUInt32Array();


                                           for (int i = 0; i < Values.Length; i++)
                                           {
                                               if (i > 0)
                                                   Write(", ");

                                               Write(Values[i].ToString());
                                           }

                                       }
                                       else
                                           throw new NotImplementedException();
                                   }
                           );


                            //Write("[ /* ? */ ]");

                            // todo: implement


                        }
                        else
                        {

                            // Write("[]");
                            // this fix is for javascript too

                            if (e.FirstOnStack.SingleStackInstruction == OpCodes.Ldc_I4_0)
                            {
                                CreateArray(null);
                            }
                            else
                            {
                                WriteKeywordSpace(Keywords._new);
                                // WriteGenericTypeName(e.i.OwnerMethod.DeclaringType, e.i.TargetType);

								var ElementType = e.i.TargetType;
								var ElementRank = 0;

								while (ElementType.IsArray)
								{
									ElementType = ElementType.GetElementType();
									ElementRank++;
								}


								WriteDecoratedTypeName(ElementType);

								for (int i = 0; i <= ElementRank; i++)
								{
									Write("[");
									if (i == 0)
										EmitFirstOnStack(e);
									Write("]");
								}
                           

								

                            }
                        }

                    }

                    //Write("new ");


                    //#region inline newarr
                    //if (e.p.IsValidInlineArrayInit)
                    //{
                    //    WriteDecoratedTypeName(e.i.TargetType);
                    //    WriteLine("[]");
                    //    Ident++;

                    //    using (CreateScope(false))
                    //    {

                    //        ILFlow.StackItem[] _stack = e.p.InlineArrayInitElements;

                    //        for (int si = 0; si < _stack.Length; si++)
                    //        {


                    //            if (si > 0)
                    //            {
                    //                Write(",");
                    //                WriteLine();
                    //            }

                    //            WriteIdent();

                    //            if (_stack[si] == null)
                    //            {
                    //                if (!e.i.TargetType.IsValueType)
                    //                {
                    //                    Write("null");
                    //                }
                    //                else
                    //                {
                    //                    if (e.i.TargetType == typeof(int))
                    //                        Write("0");
                    //                    else if (e.i.TargetType == typeof(sbyte))
                    //                        Write("0");
                    //                    else
                    //                        BreakToDebugger("default for " + e.i.TargetType.FullName + " is unknown");
                    //                }
                    //            }
                    //            else
                    //            {
                    //                Emit(e.p, _stack[si]);
                    //            }

                    //        }

                    //        WriteLine();
                    //    };
                    //    Ident--;
                    //}
                    //#endregion
                    //else
                    //{
                    //    int rank = 0;
                    //    Type type = e.i.TargetType;

                    //    while (type.IsArray)
                    //    {
                    //        type = type.GetElementType();
                    //        rank++;
                    //    }

                    //    WriteDecoratedTypeName(type);
                    //    Write("[");
                    //    EmitFirstOnStack(e);
                    //    Write("]");

                    //    while (rank-- > 0)
                    //    {
                    //        Write("[");
                    //        Write("]");
                    //    }
                    //}
                };
            #endregion

            CIW[OpCodes.Ldnull] =
                delegate(CodeEmitArgs e)
                {
                    Write("null");
                };

            #region Throw
            CIW[OpCodes.Throw] =
                delegate(CodeEmitArgs e)
                {
                    Write("throw");
                    WriteSpace();

                    Emit(e.p, e.FirstOnStack);
                };
            #endregion

            #region Rethrow
            CIW[OpCodes.Rethrow] =
                delegate(CodeEmitArgs e)
                {
                    Write("throw");
                    WriteSpace();
                    WriteExceptionVar();
                };
            #endregion

            #region Ldarg
            CIW[OpCodes.Ldarg_0,
                OpCodes.Ldarg_1,
                OpCodes.Ldarg_2,
                OpCodes.Ldarg_3,
                OpCodes.Ldarg_S,
                OpCodes.Ldarg] =
                e =>
                {
                    WriteCall_DebugTrace_Assign_Load(e);

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

            #region Stsfld
            CIW[OpCodes.Stsfld] =
               delegate(CodeEmitArgs e)
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
                           WriteTypeStaticAccessor();
                       }

					   WriteSafeLiteral(e.i.TargetField.Name);
                       WriteAssignment();

                       if (EmitEnumAsStringSafe(e))
                           return;

                       #region  assign boolean literal
                       if (e.i.TargetField.FieldType == typeof(bool))
                       {
                           if (e.i.StackBeforeStrict[0].StackInstructions.Length == 1)
                           {
                               if (e.i.StackBeforeStrict[0].SingleStackInstruction.TargetInteger != null)
                               {
                                   if (e.i.StackBeforeStrict[0].SingleStackInstruction.TargetInteger == 0)
                                       Write("false");
                                   else
                                       Write("true");

                                   return;
                               }
                           }
                       }
                       #endregion

                       Emit(e.p, e.FirstOnStack);
                   }
                   catch (Exception exc)
                   {
                       throw exc;
                   }
               };
            #endregion


            CIW[OpCodes.Ldsfld] =
                delegate(CodeEmitArgs e)
                {
                    ILFlow.StackItem[] s = e.i.StackBeforeStrict;

                    WriteDecoratedTypeName(e.i.TargetField.DeclaringType);
                    WriteTypeStaticAccessor();
					WriteSafeLiteral(e.i.TargetField.Name);
                };

            CIW[OpCodes.Callvirt] =
                delegate(CodeEmitArgs e)
                {
                    WriteMethodCall(e.p, e.i, e.i.TargetMethod);
                };

            #region call
            CIW[OpCodes.Call] =
                delegate(CodeEmitArgs e)
                {
                    MethodBase m = e.i.ReferencedMethod;

                    MethodBase mi = MySession.ResolveImplementation(m.DeclaringType, m);

                    if (m.DeclaringType == typeof(System.Runtime.CompilerServices.RuntimeHelpers))
                    {
                        if (m.Name == "InitializeArray")
                        {
                            throw new SkipThisPrestatementException();
                        }
                    }


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



            #region Ret
            CIW[OpCodes.Ret] =
                e =>
                {
                    WriteReturn(e.p, e.i);
                };
            #endregion

            #region Newobj
            CIW[OpCodes.Newobj] =
                e =>
                {
                    WriteTypeConstruction(e);
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
                   WriteCall_DebugTrace_Assign_Load(e);

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


            CIW[OpCodes.Ldstr] =
                e =>
                {
                    if (WriteCall_DebugTrace_Assign_Active)
                        Write("\\\"");
                    else
                        Write("\"");
                    WriteDecoratedLiteralString(e.i.TargetLiteral);

                    if (WriteCall_DebugTrace_Assign_Active)
                        Write("\\\"");
                    else
                        Write("\"");
                };


            #region ldc
            CIW[OpCodes.Ldc_R4] =
                delegate(CodeEmitArgs e)
                {
                    WriteNumeric(e.i.OpParamAsFloat);
                };

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


            #region  operands
			CIW[OpCodes.Not] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "~"); };
			CIW[OpCodes.Xor] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "^"); };
            CIW[OpCodes.Shl] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "<<"); };
            CIW[OpCodes.Shr] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, ">>"); };

            CIW[OpCodes.Clt, OpCodes.Clt_Un, OpCodes.Blt, OpCodes.Blt_S] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "<"); };
            CIW[OpCodes.Cgt, OpCodes.Cgt_Un, OpCodes.Bgt, OpCodes.Bgt_S] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, ">"); };

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
            CIW[OpCodes.Ceq, 
                OpCodes.Beq, 
                OpCodes.Beq_S] =
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

        }


    }
}
