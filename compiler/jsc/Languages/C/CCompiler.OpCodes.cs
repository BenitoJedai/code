using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml;
using System.Linq;

using IntPtr = global::System.IntPtr;

using ScriptCoreLib;

using jsc.Script;
using ScriptCoreLib.CSharp.Extensions;


namespace jsc.Languages.C
{
	partial class CCompiler
	{
		private void CreateInstructionHandlers()
		{
			CIW[OpCodes.Neg] =
				delegate(CodeEmitArgs e)
				{
					Write("(-(");
					EmitFirstOnStack(e);
					Write("))");
				};

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

			CIW[OpCodes.Conv_U] =
			   delegate(CodeEmitArgs e)
			   {
				   if (e.FirstOnStack.SingleStackInstruction.IsAnyOpCodeOf(OpCodes.Ldloca_S, OpCodes.Ldloca))
					   EmitFirstOnStack(e);
				   else
					   ConvertTypeAndEmit(e, "unsigned int");
			   };

			CIW[OpCodes.Conv_U2] =
			   delegate(CodeEmitArgs e) { ConvertTypeAndEmit(e, "unsigned char"); };
			CIW[OpCodes.Conv_U4] =
			   delegate(CodeEmitArgs e) { ConvertTypeAndEmit(e, "unsigned int"); };

			CIW[OpCodes.Conv_I2] =
				delegate(CodeEmitArgs e) { ConvertTypeAndEmit(e, "short int"); };


			CIW[OpCodes.Conv_I4] =
				delegate(CodeEmitArgs e) { ConvertTypeAndEmit(e, "signed int"); };

			CIW[OpCodes.Conv_I8] =
				delegate(CodeEmitArgs e) { ConvertTypeAndEmit(e, "signed long"); };
			CIW[OpCodes.Conv_U8] =
				delegate(CodeEmitArgs e) { ConvertTypeAndEmit(e, "unsigned long"); };
			CIW[OpCodes.Conv_R8] =
					delegate(CodeEmitArgs e) { ConvertTypeAndEmit(e, "double"); };

			CIW[OpCodes.Conv_R_Un] =
				delegate(CodeEmitArgs e) { ConvertTypeAndEmit(e, "float"); };


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
				OpCodes.Ldelem_U4,
				OpCodes.Ldelem_I1,
				OpCodes.Ldelem_I2,
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
					// future revisions could implement virtual arrays

					CompilerBase.BreakToDebugger(
						"C runtime cannot tell the length of an array; " + e.i.Location + " ;");
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
					var Target = e.i.TargetConstructor.DeclaringType;

					if (Target.IsDelegate())
					{
						// this is a breaking change..
						// a native delegate can be defined as nested or not as nested type

						if (Target.ToScriptAttributeOrDefault().IsNative)
						{
							Emit(e.p, e.i.StackBeforeStrict[1]);
							return;
						}
					}

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
					WriteQuotedLiteral(e.i.TargetLiteral);
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

			CIW[OpCodes.Stsfld] =
				delegate(CodeEmitArgs e)
				{
					ILFlow.StackItem[] s = e.i.StackBeforeStrict;

					WriteDecoratedTypeName(e.i.TargetField.DeclaringType);
					Write("_");
					Write(e.i.TargetField.Name);

					WriteAssignment();

					EmitFirstOnStack(e);
				};

			CIW[OpCodes.Ldsflda] =
				delegate(CodeEmitArgs e)
				{
					ILFlow.StackItem[] s = e.i.StackBeforeStrict;

					Write("&");
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
			CIW[OpCodes.Rem,
				OpCodes.Rem_Un] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "%"); };
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
			CIW[
				OpCodes.Ldloca,
				OpCodes.Ldloca_S] =
				 delegate(CodeEmitArgs e)
				 {
					 Write("&");
					 WriteVariableName(e.i.OwnerMethod.DeclaringType, e.i.OwnerMethod, e.i.TargetVariable);

				 };

			CIW[OpCodes.Ldloc_0,
				OpCodes.Ldloc_1,
				OpCodes.Ldloc_2,
				OpCodes.Ldloc_3,
				OpCodes.Ldloc_S,
				OpCodes.Ldloc] =
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

				   #region IsCompound
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
				   #endregion

				   //WriteBoxedComment("ldloc");

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
	}


}
