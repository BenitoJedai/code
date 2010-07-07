using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq;

using jsc.CodeModel;

using ScriptCoreLib;
using ScriptCoreLib.CSharp.Extensions;

namespace jsc.Script.PHP
{

	partial class PHPCompiler
	{

		private void CreateInstructionHandlers()
		{
			CIW[OpCodes.Constrained] =
				e =>
				{
					if (e.i.StackBeforeStrict.Length == 0)
						// throw skip statement instead?
						return;

					EmitFirstOnStack(e);
				};

			CIW[OpCodes.Leave, OpCodes.Leave_S] =
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

			#region starg
			CIW[OpCodes.Starg_S,
				OpCodes.Starg] =
				delegate(CodeEmitArgs e)
				{
					WriteMethodParameterOrSelf(e.i);
					WriteAssignment();
					if (EmitEnumAsStringSafe(e))
						return;

					Emit(e.p, e.FirstOnStack);
				};
			#endregion

			#region passthru

			//CIW[OpCodes.Constrained]=
			//    delegate(CodeEmitArgs e)
			//    {
			//    };

			CIW[OpCodes.Castclass,
				OpCodes.Box,
				OpCodes.Unbox_Any,
				OpCodes.Pop,
				OpCodes.Conv_I4,
				OpCodes.Conv_I8,
				OpCodes.Conv_U,
				OpCodes.Conv_U1,
				OpCodes.Conv_U2,
				OpCodes.Conv_R8] = CodeEmitArgs.DelegateEmitFirstOnStack;


			#region Ldtoken
			CIW[OpCodes.Ldtoken] =
				e =>
				{
					//if (e.i.TargetType == null)
					//    throw new NotSupportedException("ldtoken");

					//WriteDecoratedTypeName(e.i.TargetType);

					var _RuntimeTypeHandle = MySession.ResolveImplementation(typeof(RuntimeTypeHandle));
					var _IntPtr = MySession.ResolveImplementation(typeof(IntPtr));
					var _RuntimeTypeHandle_From_Class = _RuntimeTypeHandle.GetExplicitOperators(null, _RuntimeTypeHandle).Single(i => i.ReturnType == _RuntimeTypeHandle);

					var _TargetType = MySession.ResolveImplementation(e.i.TargetType) ?? e.i.TargetType;

					// typeof(System.__String) or typeof(java.lang.String)
					_TargetType = _TargetType.ToScriptAttributeOrDefault().ImplementationType ?? _TargetType;

					#region _RuntimeTypeHandle_From_Class
					//WriteDecoratedTypeName(_RuntimeTypeHandle);
					//Write("->");
					WriteDecoratedMethodName(_RuntimeTypeHandle_From_Class, false);
					Write("(");

					if (_TargetType.IsGenericParameter)
					{
						// the Type should be passed as an argument?
						throw new NotSupportedException("typeof(T) not supported yet.");
					}

					Write("\"");

					// we shall favor promitives

					// what about native types?
					if (_TargetType.ToScriptAttributeOrDefault().Implements == typeof(string))
					{
						Write("string");
					}
					else
					{
						WriteDecoratedTypeName(
							_TargetType
						);
					}

					Write("\"");

		

					Write(")");
					#endregion
				};
			#endregion

			CIW[OpCodes.Isinst] = delegate(CodeEmitArgs e)
			{
				//throw new NotSupportedException("emitting this opcode isn't directly supported");

				// instanceof 
				//Write("/* instance of */");

				//Write("TryCast(variable, type)");

				// yay, a special mode!

				var InstructionName =
					"_" + GetDecoratedTypeName(e.Method.DeclaringType, false) +
					"_" + e.Method.MetadataToken.ToString("x8") +
					"_" + e.i.Offset.ToString("x8");

				Write(InstructionName);
				Write("(");
				EmitFirstOnStack(e);
				Write(")");


				this.CompileType_WriteAdditionalStaticMembers +=
					delegate
					{
						WriteIdent();
						WriteKeywordSpace(Keywords._function);
						Write(InstructionName);
						Write("(");
						Write("$value");
						Write(")");
						WriteLine();

						using (CreateScope())
						{
							WriteIdent();
							WriteKeywordSpace(Keywords._return);
							Write("$value");
							WriteSpace();
							WriteKeywordSpace(Keywords._instanceof);
							WriteSpace();
							WriteDecoratedTypeName(ResolveImplementation(e.i.TargetType) ?? e.i.TargetType);
							WriteSpace();
							Write("?");
							WriteSpace();
							Write("$value");
							WriteSpace();
							Write(":");
							WriteSpace();
							Write("NULL");
							Write(";");
							WriteLine();
						}
					};
			};


			#endregion

			#region Ret
			CIW[OpCodes.Ret] =
				delegate(CodeEmitArgs e)
				{
					WriteReturn(e.p, e.i);
				};
			#endregion

			#region Ldlen
			CIW[OpCodes.Ldlen] =
				delegate(CodeEmitArgs e)
				{
					Write("count(");

					EmitFirstOnStack(e);

					Write(")");
				};
			#endregion

			#region Ldftn
			CIW[OpCodes.Ldftn,
				OpCodes.Ldvirtftn] =
				delegate(CodeEmitArgs e)
				{
					WriteDecoratedMethodName(e.i.TargetMethod, true);
				};
			#endregion

			#region Ldnull
			CIW[OpCodes.Initobj] =
				delegate(CodeEmitArgs e)
				{
					Write("$_" + e.i.Prev.TargetVariable.LocalIndex);
					WriteAssignment();
					Write("NULL");

				};

			CIW[OpCodes.Ldnull] =
				delegate(CodeEmitArgs e)
				{
					Write("NULL");
				};
			#endregion

			#region Throw
			CIW[OpCodes.Throw] =
				delegate(CodeEmitArgs e)
				{
					Write("throw");
					WriteSpace();

					Emit(e.p, e.FirstOnStack);
				};
			#endregion


			CIW[OpCodes.Br_S,
				OpCodes.Br] =
				delegate(CodeEmitArgs e)
				{
					if (e.i.TargetFlow.Branch == OpCodes.Ret)
					{
						WriteReturn(e.p, e.i.TargetFlow.Branch);
					}
					else Break("invalid br opcode");
				};

			#region fld
			CIW[OpCodes.Ldfld,
				OpCodes.Ldflda] =
				delegate(CodeEmitArgs e)
				{

					Emit(e.p, e.FirstOnStack);
					Write("->");
					WriteDecoratedField(e.i.TargetField, false);
				};

			CIW[OpCodes.Stfld] =
				delegate(CodeEmitArgs e)
				{
					ILFlow.StackItem[] s = e.i.StackBeforeStrict;

					Emit(e.p, s[0]);
					Write("->");
					WriteDecoratedField(e.i.TargetField, false);
					WriteSpace();
					Write("=");
					WriteSpace();
					Emit(e.p, s[1]);
				};
			#endregion


			#region sfld
			CIW[OpCodes.Ldsfld] =
				delegate(CodeEmitArgs e)
				{
					ILFlow.StackItem[] s = e.i.StackBeforeStrict;

					var FieldContext = e.i.TargetField.DeclaringType;

					WriteDecoratedTypeName(ResolveImplementation(FieldContext) ?? FieldContext);
					WriteTypeStaticAccessor();
					WriteDecoratedField(e.i.TargetField, true);
				};

			CIW[OpCodes.Stsfld] =
				delegate(CodeEmitArgs e)
				{
					try
					{
						WriteDecoratedTypeName(e.i.TargetField.DeclaringType);
						WriteTypeStaticAccessor();
						WriteDecoratedField(e.i.TargetField, true);
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


			#region  operands
			CIW[OpCodes.Xor] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "^"); };

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
						Emit(e.p, e.FirstOnStack);
						return;
					}

					WriteInlineOperator(e.p, e.i, "-");
				};

			CIW[OpCodes.Dup] = delegate(CodeEmitArgs e) { EmitFirstOnStack(e); };

			CIW[OpCodes.Shl] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "<<"); };
			CIW[OpCodes.Shr,
				OpCodes.Shr_Un] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, ">>"); };
			CIW[OpCodes.And] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "&"); };
			CIW[OpCodes.Or] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "|"); };
			CIW[OpCodes.Rem] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "%"); };
			CIW[OpCodes.Mul] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "*"); };
			CIW[OpCodes.Div] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "/"); };
			CIW[OpCodes.Bge_S,
				OpCodes.Bge] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, ">="); };
			CIW[OpCodes.Ble_S,
				OpCodes.Ble] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "<="); };
			CIW[OpCodes.Bne_Un_S,
				OpCodes.Bne_Un] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "!=="); };
			CIW[OpCodes.Neg] =
				delegate(CodeEmitArgs e)
				{
					Write("-");
					Emit(e.p, e.FirstOnStack);
				};

			CIW[OpCodes.Not] =
				delegate(CodeEmitArgs e)
				{
					Write("~");
					Emit(e.p, e.FirstOnStack);
				};

			CIW[OpCodes.Ceq,
				OpCodes.Beq,
				OpCodes.Beq_S] =
		
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
					{
						//if (e.i.StackBeforeStrict[1].SingleStackInstruction.OpCode == OpCodes.Ldnull)
						WriteInlineOperator(e.p, e.i, "===");
						//else
						//    WriteInlineOperator(e.p, e.i, "==");
					}
				};
			#endregion


			#region Ldarg
			CIW[OpCodes.Ldarg_0,
				OpCodes.Ldarg_1,
				OpCodes.Ldarg_2,
				OpCodes.Ldarg_3,
				OpCodes.Ldarg_S,
				OpCodes.Ldarg,
				OpCodes.Ldarga,
				OpCodes.Ldarga_S
				] =
				delegate(CodeEmitArgs e)
				{

					WriteMethodParameterOrSelf(e.i);
				};
			#endregion

			#region Ldind_Ref
			CIW[OpCodes.Ldind_Ref] =
				delegate(CodeEmitArgs e)
				{
					WriteMethodParameterOrSelf(e.FirstOnStack.SingleStackInstruction);
				};
			#endregion



			#region ldc
			CIW[OpCodes.Ldc_R8] =
				delegate(CodeEmitArgs e)
				{
					WriteNumeric(e.i.OpParamAsDouble);
				};

			CIW[OpCodes.Ldc_I8] =
				delegate(CodeEmitArgs e)
				{
					WriteNumeric(e.i.OpParamAsLong);
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


				OpCodes.Ldc_I4_S] =
			   delegate(CodeEmitArgs e)
			   {
				   int? int32 = e.i.TargetInteger;

				   if (int32 == null)
					   Break("ldc unresolved");

				   MyWriter.Write(int32.Value);
			   };
			#endregion


			#region stloc
			CIW[OpCodes.Stloc_0,
				OpCodes.Stloc_1,
				OpCodes.Stloc_2,
				OpCodes.Stloc_3,
				OpCodes.Stloc_S,
				OpCodes.Stloc] =
				delegate(CodeEmitArgs e)
				{

					Write("$_" + e.i.TargetVariable.LocalIndex);
					WriteAssignment();

					if (e.i.IsFirstInFlow && e.i.Flow.OwnerBlock.IsHandlerBlock)
					{
						WriteExceptionVar();
						return;
					}

					if (EmitEnumAsStringSafe(e))
						return;


					Emit(e.p, e.FirstOnStack);
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
				   if (e.p.Owner.IsCompound)
				   {
					   ILBlock.Prestatement sp = e.p.Owner.SourcePrestatement(e.p, e.i);

					   if (sp != null)
					   {
						   EmitInstruction(sp, sp.Instruction);



						   return;
					   }
				   }

				   Write("$_" + e.i.TargetVariable.LocalIndex);

				   if (e.i.IsInlinePostSub) Write("--");
				   if (e.i.IsInlinePostAdd) Write("++");
			   };
			#endregion


			CIW[OpCodes.Ldstr] =
				delegate(CodeEmitArgs e)
				{
					WriteQuotedLiteral(e.i.TargetLiteral);
				};

			CIW[OpCodes.Newarr] =
				delegate(CodeEmitArgs e)
				{
					Action WriteDefaultElement =
						delegate
						{
							if (!e.i.TargetType.IsValueType)
							{
								Write("NULL");
							}
							else
							{
								if (e.i.TargetType == typeof(int))
									Write("0");
								if (e.i.TargetType == typeof(bool))
									WriteKeywordFalse();
								else if (e.i.TargetType == typeof(sbyte))
									Write("0");
								else if (e.i.TargetType == typeof(byte))
									Write("0");
								else if (e.i.TargetType == typeof(char))
									Write("0");
								else if (e.i.TargetType == typeof(int))
									Write("0");
								else if (e.i.TargetType == typeof(short))
									Write("0");
								else
									BreakToDebugger("default for " + e.i.TargetType.FullName + " is unknown");
							}
						};

					#region inline newarr
					if (e.p.IsValidInlineArrayInit)
					{
						WriteLine("array (");
						Ident++;


						//using (CreateScope(false))
						{

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
									WriteDefaultElement();
								}
								else
								{
									Emit(e.p, _stack[si]);
								}

							}


						};


						WriteLine();

						Ident--;

						WriteIdent();
						Write(")");

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

								Write("array");
								Write("(");
								for (int i = 0; i < Values.Length; i++)
								{
									if (i > 0)
										Write(", ");

									Write(Values[i].ToString());
								}
								Write(")");
							}
							else if (Type == typeof(uint))
							{
								var Values = e.i.NextInstruction.NextInstruction.TargetField.GetValue(null).StructAsUInt32Array();

								Write("array");
								Write("(");
								for (int i = 0; i < Values.Length; i++)
								{
									if (i > 0)
										Write(", ");

									Write(Values[i].ToString());
								}
								Write(")");
							}
							else if (Type == typeof(byte))
							{
								var Values = e.i.NextInstruction.NextInstruction.TargetField.GetValue(null).StructAsByteArray();

								Write("array");
								Write("(");
								for (int i = 0; i < Values.Length; i++)
								{
									if (i > 0)
										Write(", ");

									Write("0x" + Values[i].ToString("x2"));
								}
								Write(")");
							}
							else if (Type == typeof(double))
							{
								var Values = e.i.NextInstruction.NextInstruction.TargetField.GetValue(null).StructAsDoubleArray();

								Write("array");
								Write("(");
								for (int i = 0; i < Values.Length; i++)
								{
									if (i > 0)
										Write(", ");

									WriteNumeric(Values[i]);
								}
								Write(")");
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
								Write("array()");
							}
							else
							{
								// http://ee.php.net/array_fill
								// Warning: array_fill() [function.array-fill]: Number of elements must be positive 

								Write("(($_newarr = ");
								EmitFirstOnStack(e);
								Write(") > 0 ? ");

								Write("array_fill(0");
								Write(", ");
								Write("$_newarr");
								Write(", ");
								WriteDefaultElement();
								Write(")");
								Write(" : ");
								Write("array()");
								Write(")");


							}
						}
					}
				};

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
				OpCodes.Stelem_I8,
				OpCodes.Stelem_R8,
				OpCodes.Stelem
				] =
				delegate(CodeEmitArgs e)
				{
					ILFlow.StackItem[] s = e.i.StackBeforeStrict;

					Emit(e.p, s[0]);
					Write("[");
					Emit(e.p, s[1]);
					Write("]");
					WriteSpace();
					Write("=");
					WriteSpace();
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


			#region Newobj
			CIW[OpCodes.Newobj] =
				delegate(CodeEmitArgs e)
				{
					var TargetConstructor = e.i.TargetConstructor;
					var TargetConstructorDeclaringType = ResolveImplementation(TargetConstructor.DeclaringType) ?? TargetConstructor.DeclaringType;

					var sa = ScriptAttribute.OfProvider(TargetConstructorDeclaringType);

					if (!(sa != null && sa.InternalConstructor))
						if (TargetConstructorDeclaringType.GetConstructors().Length > 1)
						{
							// yay, a special mode!

							var InstructionName =
								"_" + GetDecoratedTypeName(e.Method.DeclaringType, false) +
								"_" + e.Method.MetadataToken.ToString("x8") +
								"_" + e.i.Offset.ToString("x8");

							Write(InstructionName);
							WriteParameterInfoFromStack(TargetConstructor, e.p, e.i.StackBeforeStrict, 0);

							this.CompileType_WriteAdditionalStaticMembers +=
								delegate
								{
									WriteIdent();
									WriteKeywordSpace(Keywords._function);
									Write(InstructionName);
									Write("(");
									WriteMethodParameterList(TargetConstructor);
									Write(")");
									WriteLine();

									using (CreateScope())
									{
										WriteIdent();
										Write("$value");
										WriteAssignment();
										WriteKeywordSpace(Keywords._new);
										WriteDecoratedTypeName(TargetConstructorDeclaringType);
										Write("()");
										Write(";");
										WriteLine();

										WriteIdent();
										WriteKeywordSpace(Keywords._return);
										Write("$value");
										Write("->");
										WriteDecoratedMethodName(
											ResolveImplementationMethod(TargetConstructor.DeclaringType, TargetConstructor) ?? TargetConstructor
											, false
										);
										Write("(");
										WriteMethodParameterList(TargetConstructor);
										Write(")");
										Write(";");
										WriteLine();
									}
								};

							return;
						}

					WriteTypeConstruction(e);

				};
			#endregion

			#region call
			CIW[OpCodes.Callvirt,
				OpCodes.Call] =
				delegate(CodeEmitArgs e)
				{

					MethodBase m = e.i.ReferencedMethod;

					if (m.DeclaringType == typeof(System.Runtime.CompilerServices.RuntimeHelpers))
					{
						if (m.Name == "InitializeArray")
						{
							throw new SkipThisPrestatementException();
						}
					}

					#region multiple constructor support
					var TargetConstructor = e.i.TargetConstructor;
					if (TargetConstructor != null)
					{
						var TargetConstructorDeclaringType = ResolveImplementation(TargetConstructor.DeclaringType) ?? TargetConstructor.DeclaringType;

						Action InvokeBaseMultiConstructor =
							delegate
							{
								WriteSelf();
								Write("->");
								WriteDecoratedMethodName(TargetConstructor, false);
								WriteParameterInfoFromStack(TargetConstructor, e.p, e.i.StackBeforeStrict, 1);
							};

						// we should not call empty base constructors
						// from multiconstructor types

						if (TargetConstructorDeclaringType == e.Method.DeclaringType)
						{
							// current type has multiple constructors...
							InvokeBaseMultiConstructor();
							return;
						}
						else if (TargetConstructorDeclaringType == e.Method.DeclaringType.BaseType)
							if (e.Method.DeclaringType.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Length > 1)
							{
								if (TargetConstructorDeclaringType.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Length == 1)
								{
									this.WriteCommentLine("Empty base constructor call ommited.");

									throw new SkipThisPrestatementException();
								}
								else
								{
									InvokeBaseMultiConstructor();
									return;
								}
							}
							else
							{
								//InvokeBaseMultiConstructor();
								//return;
							}
					}
					#endregion

					if (Script.CompilerBase.IsToStringMethod(m))
					{
						Write("");
						EmitFirstOnStack(e);
						Write("->__toString()");

						return;
					}

					MethodBase mi = MySession.ResolveImplementation(m.DeclaringType, m);

					if (mi != null)
					{
						WriteMethodCall(e.p, e.i, mi);

						return;
					}

					WriteMethodCall(e.p, e.i, m);
				};
			#endregion
		}

	}
}
