using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Emit;
using jsc.Script;

namespace jsc.Languages.CSharp2
{
    partial class CSharp2Compiler
    {
        private void CreateInstructionHandlers()
        {
            #region Newarr
            CIW[OpCodes.Newarr] =
                e =>
                {
                    // fixme: new array with size

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

                            // Conversion To IEnumrable

                            if (Type == typeof(int))
                            {
                                var Values = e.i.NextInstruction.NextInstruction.TargetField.GetValue(null).StructAsInt32Array();

                                Write("[");
                                for (int i = 0; i < Values.Length; i++)
                                {
                                    if (i > 0)
                                        Write(", ");

                                    Write(Values[i].ToString());
                                }
                                Write("]");
                            }
                            else if (Type == typeof(uint))
                            {
                                var Values = e.i.NextInstruction.NextInstruction.TargetField.GetValue(null).StructAsUInt32Array();

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
                        {

                            // Write("[]");
                            // this fix is for javascript too

                            if (e.FirstOnStack.SingleStackInstruction == OpCodes.Ldc_I4_0)
                            {
                                Write("[]");
                            }
                            else
                            {
                                Write("new Array(");
                                EmitFirstOnStack(e);
                                Write(")");
                            }
                        }

                    }
                };
            #endregion


            #region Ldftn
            CIW[OpCodes.Ldftn,
                OpCodes.Ldvirtftn] =
                delegate(CodeEmitArgs e)
                {
                    Write("_method_");

                    // we must load it as IntPtr
                    //var _IntPtr = MySession.ResolveImplementation(typeof(IntPtr));
                    //var _Operators = _IntPtr.GetExplicitOperators(null, _IntPtr);

                    //var _IntPtr_string = _Operators.Single(i => i.GetParameters().Single().ParameterType == typeof(string));
                    //var _IntPtr_Function = _Operators.Single(i => i.GetParameters().Single().ParameterType != typeof(string));

                    //var _Method = e.i.TargetMethod;
                    //if (_Method.IsStatic)
                    //{
                    //    WriteDecoratedTypeNameOrImplementationTypeName(_IntPtr, false, false, IsFullyQualifiedNamesRequired(e.Method.DeclaringType, _IntPtr));
                    //    Write(".");
                    //    WriteDecoratedMethodName(_IntPtr_Function, false);
                    //    Write("(");
                    //    WriteDecoratedTypeNameOrImplementationTypeName(_Method.DeclaringType, false, false, IsFullyQualifiedNamesRequired(e.Method.DeclaringType, _Method.DeclaringType));
                    //    Write(".");
                    //    WriteDecoratedMethodName(e.i.TargetMethod, false);
                    //    Write(")");
                    //}
                    //else
                    //{
                    //    if (_Method.DeclaringType == e.Method.DeclaringType)
                    //    {
                    //        WriteDecoratedTypeNameOrImplementationTypeName(_IntPtr, false, false, IsFullyQualifiedNamesRequired(e.Method.DeclaringType, _IntPtr));
                    //        Write(".");
                    //        WriteDecoratedMethodName(_IntPtr_Function, false);
                    //        Write("(");
                    //        WriteDecoratedMethodName(e.i.TargetMethod, false);
                    //        Write(")");
                    //    }
                    //    else
                    //    {
                    //        WriteDecoratedTypeNameOrImplementationTypeName(_IntPtr, false, false, IsFullyQualifiedNamesRequired(e.Method.DeclaringType, _IntPtr));
                    //        Write(".");
                    //        WriteDecoratedMethodName(_IntPtr_string, false);
                    //        Write("(");
                    //        WriteDecoratedMethodName(_Method, true);
                    //        Write(")");
                    //    }
                    //}



                };
            #endregion

            #region conv

            // not supported
            // CIW[OpCodes.Conv_I1] = e => ConvertTypeAndEmit(e, "byte");
            {
                Func<string, CodeInstructionHandler> f = t => e => ConvertTypeAndEmit(e, t);

                CIW[OpCodes.Conv_U] = f("uint"); // char == int
                CIW[OpCodes.Conv_U1] = f("uint"); // char == int
                CIW[OpCodes.Conv_U2] = f("uint"); // char == int
                CIW[OpCodes.Conv_U4] = f("uint"); // char == int

                CIW[OpCodes.Conv_I1] = f("int");
                CIW[OpCodes.Conv_I2] = f("int");
                CIW[OpCodes.Conv_I4] = f("int");

                CIW[OpCodes.Conv_R4] = f("Number");
                CIW[OpCodes.Conv_R8] = f("Number");
                CIW[OpCodes.Conv_I8] = f("Number");
                CIW[OpCodes.Conv_U8] = f("Number");


                CIW[OpCodes.Conv_Ovf_I] = f("int");
            }
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
                OpCodes.Ldelem_R8,
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
                OpCodes.Stelem_I8,
                OpCodes.Stelem_R8,
                OpCodes.Stelem
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


            #region Ldlen
            CIW[OpCodes.Ldlen] =
                e =>
                {
                    EmitFirstOnStack(e);

                    Write(".Length");
                };
            #endregion


            CIW[OpCodes.Initobj] =
                e =>
                {
                    WriteVariableName(e.i.OwnerMethod.DeclaringType, e.i.OwnerMethod, e.i.Prev.TargetVariable);
                    WriteAssignment();

                    WriteKeyword(Keywords._default);
                    Write("(");
                    WriteGenericTypeName(e.i.OwnerMethod.DeclaringType, e.i.Prev.TargetVariable.LocalType);
                    Write(")");
                };

            CIW[OpCodes.Constrained] =
                e =>
                {
                    if (e.i.StackBeforeStrict.Length == 0)
                        // throw skip statement instead?
                        return;

                    EmitFirstOnStack(e);
                };

            CIW[OpCodes.Castclass] =
                 e =>
                 {
                     //if (AutoCastToEnumerator(e.p, e.i.TargetType, e.FirstOnStack))
                     //    return;

                     ConvertTypeAndEmit(e, e.i.TargetType);
                 };

            CIW[OpCodes.Ldnull] = e => WriteKeyword(Keywords._null);


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

            CIW[OpCodes.Throw] =
                e =>
                {
                    WriteKeywordSpace(Keywords._throw);

                    EmitFirstOnStack(e);
                };

            CIW[OpCodes.Newobj] =
                  e =>
                  {
                      WriteTypeConstruction(e);
                  };

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

            #region Ldarg
            CIW[OpCodes.Ldarg_0,
                OpCodes.Ldarg_1,
                OpCodes.Ldarg_2,
                OpCodes.Ldarg_3,
                OpCodes.Ldarg_S,
                OpCodes.Ldarga,
                OpCodes.Ldarga_S,
                OpCodes.Ldarg] =
                e =>
                {
                    WriteMethodParameterOrSelf(e.i);
                };
            #endregion

            #region Ret
            CIW[OpCodes.Ret] =
                e =>
                {
                    WriteReturn(e.p, e.i);
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

            CIW[OpCodes.Pop] = CodeEmitArgs.DelegateEmitFirstOnStack;


            CIW[OpCodes.Callvirt] =
                e =>
                {
                    WriteMethodCall(e.p, e.i, e.i.TargetMethod);
                };


            CIW[OpCodes.Ldstr] =
                e =>
                {
                    WriteQuotedLiteral(e.i.TargetLiteral);
                };

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

                 if (e.FirstOnStack.SingleStackInstruction.ReferencedType == t)
                 {
                     // see: Dictionary<,>.Enumerator

                     EmitFirstOnStack(e);

                     return;
                 }

                 Write("new ");
                 WriteDecoratedTypeNameOrImplementationTypeName(t, false, false, IsFullyQualifiedNamesRequired(e.Method.DeclaringType, t));
                 Write("(");

                 EmitFirstOnStack(e);

                 Write(")");
             };

            CIW[OpCodes.Isinst] =
               e =>
               {
                   //Write("/* is or as */");

                   // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/operators.html#as
                   // http://crawlmsdn.microsoft.com/en-us/library/cscsdfbt.aspx
                   // expression as type
                   // expression is type ? (type)expression : (type)null

                   if (e.i.StackBeforeStrict.Length == 1)
                   {
                       EmitFirstOnStack(e);

                       WriteSpace();
                       Write("as");
                       WriteSpace();

                       WriteDecoratedTypeNameOrImplementationTypeName(
                           e.i.TargetType, false, false,
                           IsFullyQualifiedNamesRequired(e.Method.DeclaringType, e.i.TargetType)
                       );
                   }
                   else
                       throw new NotSupportedException();
               };

            CIW[
                OpCodes.Nop,
                OpCodes.Dup] = e => EmitFirstOnStack(e);

            CIW[OpCodes.Call] =
              e =>
              {
                  DebugBreak(e.i.OwnerMethod.ToScriptAttribute());

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

                  if (m.Name == "op_Implicit" && !m.ToScriptAttributeOrDefault().NotImplementedHere)
                  {
                      // native types cannot have operators defined unless they are using the NotImplementedHere flag
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


            #region operators

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

            CIW[OpCodes.Ceq, OpCodes.Beq, OpCodes.Beq_S] =
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

            {
                Func<string, CodeInstructionHandler> f = t => e => WriteInlineOperator(e.p, e.i, t);

                CIW[OpCodes.Xor] = f("^");
                CIW[OpCodes.Shl] = f("<<");
                CIW[OpCodes.Shr] = f(">>");

                CIW[OpCodes.Clt, OpCodes.Clt_Un] = f("<");
                CIW[OpCodes.Cgt, OpCodes.Cgt_Un] = f(">");


                CIW[OpCodes.Or] = f("|");
                CIW[OpCodes.And] = f("&");
                CIW[OpCodes.Rem] = f("%");
                CIW[OpCodes.Mul] = f("*");
                CIW[OpCodes.Div, OpCodes.Div_Un] = f("/");
                CIW[OpCodes.Bge_S, OpCodes.Bge] = f(">=");
                CIW[OpCodes.Ble_S, OpCodes.Ble] = f("<=");
                CIW[OpCodes.Bne_Un_S, OpCodes.Bne_Un] = f("!=");



            }
            #endregion

        }
    }
}
