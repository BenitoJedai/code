using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using jsc.Script;
using ScriptCoreLib;
using ScriptCoreLib.CSharp.Extensions;

namespace jsc.Languages.ActionScript
{
	partial class ActionScriptCompiler
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

			CIW[OpCodes.Callvirt] =
				e =>
				{
					WriteMethodCall(e.p, e.i, e.i.TargetMethod);
				};

			#region call
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



					Emit(e.p, e.FirstOnStack, e.i.TargetVariable.LocalType);
				};
			#endregion

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

			#region Newobj
			CIW[OpCodes.Newobj] =
				e =>
				{
					WriteTypeConstruction(e);

					if (e.i.TargetConstructor.DeclaringType.IsDelegate())
					{
						var TargetIsNotNull = e.i.StackBeforeStrict[0].SingleStackInstruction != OpCodes.Ldnull;
						var TargetMethodIsStatic = e.i.StackBeforeStrict[1].SingleStackInstruction.TargetMethod.IsStatic;

						if (TargetMethodIsStatic)
							if (TargetIsNotNull)
							{
								Write(".");
								Write(DelegateImplementationProvider.AsExtensionMethod);
								Write("()");
							}
					}
				};
			#endregion

			#region fld
			CIW[OpCodes.Ldfld,
				OpCodes.Ldflda] =
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

					Emit(e.p, s[1], e.i.TargetField.FieldType);
				};
			#endregion

			CIW[OpCodes.Pop] = CodeEmitArgs.DelegateEmitFirstOnStack;


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

			CIW[OpCodes.Leave,
				OpCodes.Leave_S] =
				e =>
				{
					var b = e.i.Flow.OwnerBlock;

					if (b.Clause == null)
						b = b.Parent;


					if (b.Clause.Flags == ExceptionHandlingClauseOptions.Clause ||
						b.Clause.Flags == ExceptionHandlingClauseOptions.Finally
						)
					{
						var tx = e.i.IndirectReturnPrestatement;
						if (tx != null)
						{
							EmitPrestatement(tx);
							return;
						}

					}

					throw new NotSupportedException("current OpCodes.Leave cannot be understood");
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
					   var TypeExpectedOrDefault =
						((e.TypeExpectedOrDefault != null && e.TypeExpectedOrDefault.IsEnum)
						? Enum.GetUnderlyingType(e.TypeExpectedOrDefault) : null)
						?? e.TypeExpectedOrDefault;

					   if (TypeExpectedOrDefault == typeof(bool))
					   {
						   if (n == 0)
							   WriteKeyword(Keywords._false);
						   else
							   WriteKeyword(Keywords._true);

						   return;
					   }

					   if (e.TypeExpectedOrDefault == typeof(uint))
						   MyWriter.Write((uint)n.Value);
					   else
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

				CIW[OpCodes.Not] = f("~");
				CIW[OpCodes.Xor] = f("^");
				CIW[OpCodes.Shl] = f("<<");
				CIW[OpCodes.Shr,
					OpCodes.Shr_Un] = f(">>");

				CIW[OpCodes.Clt, OpCodes.Clt_Un, OpCodes.Blt_S] = f("<");
				CIW[OpCodes.Cgt, OpCodes.Cgt_Un, OpCodes.Bgt_S] = f(">");

				// sho
				CIW[OpCodes.Or] =
					e =>
					{
						// acctionscript does not accept short-circut or on boolean
						if (e.i.StackBeforeStrict[0].SingleStackInstruction.ReferencedType ==
							e.i.StackBeforeStrict[1].SingleStackInstruction.ReferencedType &&
							e.i.StackBeforeStrict[0].SingleStackInstruction.ReferencedType == typeof(bool))
						{
							WriteInlineOperator(e.p, e.i, "||");
							return;
						}

						WriteInlineOperator(e.p, e.i, "|");
					};

				CIW[OpCodes.And] =
					e =>
					{
						// acctionscript does not accept short-circut or on boolean
						if (e.i.StackBeforeStrict[0].SingleStackInstruction.ReferencedType ==
							e.i.StackBeforeStrict[1].SingleStackInstruction.ReferencedType &&
							e.i.StackBeforeStrict[0].SingleStackInstruction.ReferencedType == typeof(bool))
						{
							WriteInlineOperator(e.p, e.i, "&&");
							return;
						}

						WriteInlineOperator(e.p, e.i, "&");
					};

				CIW[OpCodes.Rem, OpCodes.Rem_Un] = f("%");




				CIW[OpCodes.Mul] =
					e =>
					{
						var a = e.i.StackBeforeStrict[0].SingleStackInstruction.TargetInteger;
						var b = e.i.StackBeforeStrict[1].SingleStackInstruction.TargetInteger;

						Func<int?, int, jsc.ILFlow.StackItem, bool> TryOptimize =
							(z, x, s) =>
							{
								if (z != null)
								{
									if (z == (1 << x))
									{

										Write("(");
										Emit(e.p, s);
										WriteSpace();
										Write("<<");
										WriteSpace();
										Write((x).ToString());
										Write(")");


										return true;
									}


								}
								return false;
							};

						Func<int, bool> TryOptimizeDual =
							x =>
							{
								if (TryOptimize(a, x, e.i.StackBeforeStrict[1]))
									return true;

								if (TryOptimize(b, x, e.i.StackBeforeStrict[0]))
									return true;

								return false;
							};

						for (int i = 2; i < 32; i++)
						{
							if (TryOptimizeDual(i))
								return;
						}




						WriteInlineOperator(e.p, e.i, "*");
					};

				CIW[OpCodes.Div, OpCodes.Div_Un] = f("/");
				CIW[OpCodes.Bge_S, OpCodes.Bge, OpCodes.Bge_Un, OpCodes.Bge_Un_S] = f(">=");
				CIW[OpCodes.Ble_S, OpCodes.Ble, OpCodes.Ble_Un, OpCodes.Ble_Un_S] = f("<=");
				CIW[OpCodes.Bne_Un_S, OpCodes.Bne_Un] = f("!=");



			}
			#endregion

			#region conv

			// not supported
			// CIW[OpCodes.Conv_I1] = e => ConvertTypeAndEmit(e, "byte");
			{
				Func<string, CodeInstructionHandler> f = t => e => ConvertTypeAndEmit(e, t);

				CIW[OpCodes.Conv_U] = f("uint"); // char == int
				CIW[OpCodes.Conv_U1] =
					e =>
					{
						Write("uint(");
						Write("(");

						EmitFirstOnStack(e);

						Write(") & 0xff");
						Write(")");
					};

				CIW[OpCodes.Conv_U2] =
					e =>
					{
						Write("uint(");
						Write("(");

						EmitFirstOnStack(e);

						Write(") & 0xffff");
						Write(")");
					};
				CIW[OpCodes.Conv_U4] = f("uint"); // char == int

				CIW[OpCodes.Conv_I1] =
					e =>
					{
						Write("int(");
						Write("(");

						EmitFirstOnStack(e);

						Write(") & 0xff");
						Write(")");
					};

				CIW[OpCodes.Conv_I2] =
					e =>
					{
						Write("int(");
						Write("(");

						EmitFirstOnStack(e);

						Write(") & 0xffff");
						Write(")");
					};
				CIW[OpCodes.Conv_I4] = f("int");

				CIW[OpCodes.Conv_R4] = f("Number");
				CIW[OpCodes.Conv_R8] = f("Number");
				CIW[OpCodes.Conv_I8] = f("Number");
				CIW[OpCodes.Conv_U8] = f("Number");


				CIW[OpCodes.Conv_Ovf_I] = f("int");
			}
			#endregion

			CIW[OpCodes.Ldnull] = e => Write("null");


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
									var TargetType = e.i.TargetType;

									if (TargetType.IsEnum)
										TargetType = Enum.GetUnderlyingType(TargetType);

									if (TargetType == typeof(int))
										Write("0");
									else if (TargetType == typeof(sbyte))
										Write("0");
									else if (TargetType == typeof(byte))
										Write("0");
									else if (TargetType == typeof(double))
										Write("0");
									else
										BreakToDebugger("default for " + e.i.TargetType.FullName + " is unknown at " + e.i.Location);
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

									Write("uint(");
									Write(Values[i].ToString());
									Write(")");
								}
								Write("]");
							}
							else if (Type == typeof(char))
							{
								var Values = e.i.NextInstruction.NextInstruction.TargetField.GetValue(null).StructAsCharArray();

								Write("[");
								for (int i = 0; i < Values.Length; i++)
								{
									if (i > 0)
										Write(", ");

									// char is translated to int by jsc
									// interesting :)
									Write("int(");
									Write("0x" + ((byte)Values[i]).ToString("x4"));
									Write(")");
								}
								Write("]");
							}
							else if (Type == typeof(byte))
							{
								var Values = e.i.NextInstruction.NextInstruction.TargetField.GetValue(null).StructAsByteArray();

								Write("[");
								for (int i = 0; i < Values.Length; i++)
								{
									if (i > 0)
										Write(", ");

									Write("uint(");
									Write("0x" + ((byte)Values[i]).ToString("x2"));
									Write(")");
								}
								Write("]");
							}
							else if (Type == typeof(double))
							{
								var Values = e.i.NextInstruction.NextInstruction.TargetField.GetValue(null).StructAsDoubleArray();

								Write("[");
								for (int i = 0; i < Values.Length; i++)
								{
									if (i > 0)
										Write(", ");

									WriteNumeric(Values[i]);
								}
								Write("]");
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

			CIW[OpCodes.Stobj] =
				e =>
				{
					ILFlow.StackItem[] s = e.i.StackBeforeStrict;

					Emit(e.p, s[0]);

					WriteAssignment();

					Emit(e.p, s[1]);
				};

			CIW[OpCodes.Ldobj] =
				e =>
				{
					ILFlow.StackItem[] s = e.i.StackBeforeStrict;

					Emit(e.p, s[0]);

				};
			#endregion

			CIW[OpCodes.Castclass] =
				e =>
				{
					if (AutoCastToEnumerator(e.p, e.i.TargetType, e.FirstOnStack))
						return;

					ConvertTypeAndEmit(e, e.i.TargetType);
				};

			// When applied to the boxed form of a value type, the unbox.any instruction extracts the value contained within obj (of type O), and is therefore equivalent to unbox followed by ldobj.
			// When applied to a reference type, the unbox.any instruction has the same effect as castclass  typeTok.
			// Is the same for actionscript...
			CIW[OpCodes.Unbox_Any] = e => ConvertTypeAndEmit(e, e.i.TargetType);


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

					   if (EmitEnumAsStringSafe(e))
						   return;

					   Emit(e.p, e.FirstOnStack, e.i.TargetField.FieldType);
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
						// throw skip statement instead?
						return;

					EmitFirstOnStack(e);
				};

			CIW[OpCodes.Ldsfld] =
				e =>
				{
					ILFlow.StackItem[] s = e.i.StackBeforeStrict;


					var t = e.i.TargetField.DeclaringType;
					WriteDecoratedTypeNameOrImplementationTypeName(t, false, false, IsFullyQualifiedNamesRequired(e.Method.DeclaringType, t), WriteDecoratedTypeNameOrImplementationTypeNameMode.IgnoreImplementationType);
					Write(".");
					WriteSafeLiteral(e.i.TargetField.Name);
				};

			CIW[OpCodes.Isinst] = this.OpCodes_Isinst;


			CIW[
				OpCodes.Nop,
				OpCodes.Dup] = e => EmitFirstOnStack(e);

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

			CIW[OpCodes.Ldtoken] =
				e =>
				{
					var _RuntimeTypeHandle = MySession.ResolveImplementation(typeof(RuntimeTypeHandle));
					var _IntPtr = MySession.ResolveImplementation(typeof(IntPtr));
					var _RuntimeTypeHandle_From_Class = _RuntimeTypeHandle.GetExplicitOperators(null, _RuntimeTypeHandle).Single(i => i.ReturnType == _RuntimeTypeHandle);

					var _TargetType = MySession.ResolveImplementation(e.i.TargetType) ?? e.i.TargetType;


					#region _RuntimeTypeHandle_From_Class
					WriteDecoratedTypeNameOrImplementationTypeName(_RuntimeTypeHandle, false, false, IsFullyQualifiedNamesRequired(e.Method.DeclaringType, _RuntimeTypeHandle));
					Write(".");
					WriteDecoratedMethodName(_RuntimeTypeHandle_From_Class, false);
					Write("(");

					if (_TargetType.IsGenericParameter)
					{
						// the Type should be passed as an argument?
						throw new NotSupportedException("typeof(T) not supported yet.");
					}

					WriteDecoratedTypeNameOrImplementationTypeName(
						_TargetType, false, false,
						IsFullyQualifiedNamesRequired(e.Method.DeclaringType, _TargetType));

					Write(")");
					#endregion
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


					var _Method = ResolveImplementationMethod(e.i.TargetMethod.DeclaringType, e.i.TargetMethod) ?? e.i.TargetMethod;

					if (_Method.IsStatic)
					{
						WriteDecoratedTypeNameOrImplementationTypeName(_IntPtr, false, false, IsFullyQualifiedNamesRequired(e.Method.DeclaringType, _IntPtr));
						Write(".");
						WriteDecoratedMethodName(_IntPtr_Function, false);
						Write("(");
						WriteDecoratedTypeNameOrImplementationTypeName(_Method.DeclaringType, false, false, IsFullyQualifiedNamesRequired(e.Method.DeclaringType, _Method.DeclaringType), WriteDecoratedTypeNameOrImplementationTypeNameMode.IgnoreImplementationType);
						Write(".");
						WriteDecoratedMethodName(_Method, false);
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
							// jsc does not tell us the upper instructrion
							// we must  assume that the upper instruction
							// takes a stack of two (target, method)

							EmitInstruction(e.p, e.i.Prev);
							Write(".");

							WriteDecoratedMethodName(_Method, false);
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

			CIW[OpCodes.Initobj] =
				e =>
				{
					// we can only initobj a variable. we cannot init a generic type parameter
					if (e.i.Prev.TargetVariable == null)
						throw new SkipThisPrestatementException();

					var target = MySession.ResolveImplementation(e.i.TargetType) ?? e.i.TargetType;


					if (target.IsGenericParameter)
					{
						throw new SkipThisPrestatementException();
					}

					WriteVariableName(e.i.OwnerMethod.DeclaringType, e.i.OwnerMethod, e.i.Prev.TargetVariable);
					WriteAssignment();


					WriteKeywordSpace(Keywords._new);
					WriteDecoratedTypeName(e.Method.DeclaringType, target);
					Write("()");
				};

			#region Throw
			CIW[OpCodes.Throw] =
				e =>
				{
					Write("throw");
					WriteSpace();

					EmitFirstOnStack(e);
				};

			CIW[OpCodes.Rethrow] =
				e =>
				{
					// http://livedocs.adobe.com/flex/3/html/help.html?content=11_Handling_errors_08.html

					Write("throw");
					WriteSpace();

					var b = e.i.Flow.OwnerBlock;

					if (b.Clause == null)
						b = b.Parent;

					if (b.Clause.CatchType == typeof(object))
					{
						WriteExceptionVar();
					}
					else
					{
						var set_exc = b.Prestatements.PrestatementCommands[0];
						WriteVariableName(b.OwnerMethod.DeclaringType, b.OwnerMethod, set_exc.Instruction.TargetVariable);
					}

				};

			#endregion
		}


	}


}
