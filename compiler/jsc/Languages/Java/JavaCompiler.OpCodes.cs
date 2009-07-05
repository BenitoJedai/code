
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
using ScriptCoreLib.CSharp.Extensions;

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

			#region Ldftn
			CIW[OpCodes.Ldftn,
				OpCodes.Ldvirtftn] =
				delegate(CodeEmitArgs e)
				{
					// we must load it as IntPtr
					var _IntPtr = MySession.ResolveImplementation(typeof(IntPtr));

					var _IntPtr_Of = _IntPtr.GetMethods().SingleOrDefault(
						k =>
						{
							if (!k.IsStatic)
								return false;

							if (k.ReturnType != _IntPtr)
								return false;

							var p = k.GetParameters();

							if (p.Length != 3)
								return false;

							if (p[1].ParameterType != typeof(string))
								return false;

							if (!p[2].ParameterType.IsArray)
								return false;

							if (p[2].ParameterType.GetElementType() != p[0].ParameterType)
								return false;

							return true;
						}
					);

					if (_IntPtr_Of == null)
						throw new Exception("OpCodes.Ldftn cannot find IntPtr factory method.");

					var _Method = ResolveImplementationMethod(e.i.TargetMethod.DeclaringType, e.i.TargetMethod) ?? e.i.TargetMethod;

					WriteDecoratedTypeNameOrImplementationTypeName(_IntPtr, false, false);
					Write(".");
					WriteDecoratedMethodName(_IntPtr_Of, false);
					Write("(");


					WriteDecoratedTypeNameOrImplementationTypeName(_Method.DeclaringType, false, true);
					Write(".");
					WriteKeyword(Keywords._class);

					Write(", ");

					WriteQuote();
					WriteDecoratedMethodName(_Method, false);
					WriteQuote();

					Write(", ");

					WriteKeywordSpace(Keywords._new);
					WriteDecoratedTypeNameOrImplementationTypeName(_IntPtr_Of.GetParameters()[0].ParameterType, false, false);
					Write("[");
					Write("]");
					Write("{");

					var _MethodParameters = _Method.GetParameters();
					for (int i = 0; i < _MethodParameters.Length; i++)
					{
						if (i > 0)
							Write(", ");


						WriteDecoratedTypeNameOrImplementationTypeName(_MethodParameters[i].ParameterType, false, true);
						Write(".");
						WriteKeyword(Keywords._class);
					}

					Write("}");
					Write(")");

					//if (_Method.IsStatic)
					//{
					//    WriteDecoratedTypeNameOrImplementationTypeName(_IntPtr, false, false, IsFullyQualifiedNamesRequired(e.Method.DeclaringType, _IntPtr));
					//    Write(".");
					//    WriteDecoratedMethodName(_IntPtr_Function, false);
					//    Write("(");
					//    WriteDecoratedTypeNameOrImplementationTypeName(_Method.DeclaringType, false, false, IsFullyQualifiedNamesRequired(e.Method.DeclaringType, _Method.DeclaringType), WriteDecoratedTypeNameOrImplementationTypeNameMode.IgnoreImplementationType);
					//    Write(".");
					//    WriteDecoratedMethodName(_Method, false);
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
					//        // jsc does not tell us the upper instructrion
					//        // we must  assume that the upper instruction
					//        // takes a stack of two (target, method)

					//        EmitInstruction(e.p, e.i.Prev);
					//        Write(".");

					//        WriteDecoratedMethodName(_Method, false);
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

			#region elem_ref

			#region byte
			CIW[OpCodes.Ldelem_U1] =
				e =>
				{
					ILFlow.StackItem[] s = e.i.StackBeforeStrict;


					Write("(short)");
					Write("(");

					Emit(e.p, s[0]);
					Write("[");
					Emit(e.p, s[1]);
					Write("]");

					// this operator is either 16bit or 32bit, depends on VM
					Write(" & 0xff");
					Write(")");


				};
			#endregion


			#region uint
			CIW[OpCodes.Ldelem_U4] =
				e =>
				{
					ILFlow.StackItem[] s = e.i.StackBeforeStrict;


					Write("(long)");
					Write("(");

					Emit(e.p, s[0]);
					Write("[");
					Emit(e.p, s[1]);
					Write("]");

					Write(" & 0xffffffffL");
					Write(")");


				};
			#endregion

			#region Ldelem
			CIW[OpCodes.Ldelem_Ref,
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
			#endregion


			#region Stelem
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

					if (s[0].SingleStackInstruction.ReferencedType != null)
					{
						var TargetFieldElement = s[0].SingleStackInstruction.ReferencedType.GetElementType();

						if (TargetFieldElement == typeof(byte))
						{
							Write("(byte)");
						}

						if (TargetFieldElement == typeof(int))
							if (s[2].SingleStackInstruction.OpCode == OpCodes.Ldelem_U4)
								Write("(int)");

						if (TargetFieldElement == typeof(uint))
						{
							Write("(int)");
						}


						if (TargetFieldElement == typeof(char))
						{
							Write("(char)");
						}

						if (TargetFieldElement == typeof(ushort))
						{
							Write("(short)");
						}
					}

					Emit(e.p, s[2]);
				};
			#endregion


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

					var ResolvedTargetType = ResolveImplementation(e.i.TargetType);

					if (e.i.TargetType.IsValueType && ResolvedTargetType != null)
					{
						ConvertTypeAndEmit(e, GetDecoratedTypeName(ResolvedTargetType, true, false));
						return;
					}

					WriteBoxedComment("unbox " + e.i.TargetType.Name);
					EmitFirstOnStack(e);
				};
			#endregion

			#region passthru

			CIW[OpCodes.Pop] = CodeEmitArgs.DelegateEmitFirstOnStack;

			#endregion

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
					WriteDecoratedTypeNameOrImplementationTypeName(_RuntimeTypeHandle, false, false);
					Write(".");
					WriteDecoratedMethodName(_RuntimeTypeHandle_From_Class, false);
					Write("(");

					if (_TargetType.IsGenericParameter)
					{
						// the Type should be passed as an argument?
						throw new NotSupportedException("typeof(T) not supported yet.");
					}

					WriteDecoratedTypeNameOrImplementationTypeName(
						_TargetType, false, false
					);

					Write(".");
					WriteKeyword(Keywords._class);

					Write(")");
					#endregion
				};
			#endregion


			#region Isinst
			CIW[OpCodes.Isinst] = delegate(CodeEmitArgs e)
			{
				if (e.i.StackBeforeStrict.Length == 1)
					if (e.i.StackBeforeStrict[0].SingleStackInstruction.IsLoadInstruction)
					{
						// expression is type ? (type)expression : (type)null
						Write("(");

						Write("(");
						EmitFirstOnStack(e);

						WriteSpace();
						WriteKeywordSpace(Keywords._instanceof);
						WriteSpace();

						WriteDecoratedTypeNameOrImplementationTypeName(
							e.i.TargetType, false, false
							//IsFullyQualifiedNamesRequired(e.Method.DeclaringType, e.i.TargetType)
						);

						Write(")");

						WriteSpace();
						Write("?");
						WriteSpace();

						Write("(");
						WriteDecoratedTypeNameOrImplementationTypeName(
							e.i.TargetType, false, false
							//IsFullyQualifiedNamesRequired(e.Method.DeclaringType, e.i.TargetType)
						);
						Write(")");
						EmitFirstOnStack(e);

						WriteSpace();
						Write(":");
						WriteSpace();

						Write("(");
						WriteDecoratedTypeNameOrImplementationTypeName(
							e.i.TargetType, false, false
							//IsFullyQualifiedNamesRequired(e.Method.DeclaringType, e.i.TargetType)
						);
						Write(")");
						WriteKeywordNull();

						Write(")");
						return;
					}

				throw new NotSupportedException("a custom TryCast is not yet implemented");
			};
			#endregion



			CIW[OpCodes.Dup] = delegate(CodeEmitArgs e) { EmitFirstOnStack(e); };

			#region fld
			CIW[OpCodes.Ldfld,
				OpCodes.Ldflda] =
				e =>
				{

					WriteCall_DebugTrace_Assign_Load(e);

					#region byte
					if (e.i.TargetField.FieldType == typeof(byte))
					{
						Write("(short)");
						Write("(");
						Emit(e.p, e.FirstOnStack);
						Write(".");
						WriteSafeLiteral(e.i.TargetField.Name);

						// this operator is either 16bit or 32bit, depends on VM
						Write(" & 0xff");
						Write(")");

						return;
					}
					#endregion

					#region uint
					if (e.i.TargetField.FieldType == typeof(uint))
					{
						Write("(long)");
						Write("(");
						Emit(e.p, e.FirstOnStack);
						Write(".");
						WriteSafeLiteral(e.i.TargetField.Name);

						Write(" & 0xffffffffL");
						Write(")");

						return;
					}
					#endregion

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

					if (e.i.TargetField.FieldType == typeof(byte))
					{
						// we will store ubyte as sbyte
						Write("(byte)");
					}

					if (e.i.TargetField.FieldType == typeof(uint))
					{
						Write("(int)");
					}

					if (e.i.TargetField.FieldType == typeof(ushort))
					{
						Write("(short)");
					}

					Emit(e.p, s[1]);
				};
			#endregion

			CIW[OpCodes.Castclass] =
					delegate(CodeEmitArgs e)
					{
						//EmitFirstOnStack(e);
						var TargetType = this.ResolveImplementation(e.i.TargetType) ?? e.i.TargetType;

						TargetType = TargetType.ToScriptAttributeOrDefault().ImplementationType ?? TargetType;

						ConvertTypeAndEmit(e, GetDecoratedTypeName(TargetType, true, false));
						//Write("((");

						//WriteDecoratedTypeName(e.i.TargetType);
						//Write(")");
						//EmitFirstOnStack(e);
						//Write(")");

					};

			CIW[OpCodes.Box] =
				delegate(CodeEmitArgs e)
				{
					#region byte
					if (e.i.TargetType == typeof(byte))
					{
						// short has 15 unsigned bits, we need 8
						Write("new ");
						Write(GetDecoratedTypeName(typeof(short), true, false));
						Write("(");

						if (e.FirstOnStack.SingleStackInstruction.IsLoadLocal)
						{
							EmitFirstOnStack(e);
						}
						else
						{
							Write("(short)");
							Write("(");
							EmitFirstOnStack(e);

							// this operator is either 16bit or 32bit, depends on VM
							Write(" & 0xff");
							Write(")");
						}

						Write(")");
						return;
					}
					#endregion

					#region uint
					if (e.i.TargetType == typeof(uint))
					{
						// short has 15 unsigned bits, we need 8
						Write("new ");
						Write(GetDecoratedTypeName(typeof(long), true, false));
						Write("(");

						if (e.FirstOnStack.SingleStackInstruction.IsLoadLocal)
						{
							EmitFirstOnStack(e);
						}
						else
						{
							Write("(long)");
							Write("(");
							EmitFirstOnStack(e);

							Write(" & 0xffffffffL");
							Write(")");
						}

						Write(")");
						return;
					}
					#endregion

					if (e.i.TargetType == typeof(IntPtr))
					{
						// IntPtr should never be boxed, because in our implementation
						// we only have classes
						EmitFirstOnStack(e);
					}
					else
					{
						//var TargetType = this.ResolveImplementation(e.i.TargetType) ?? e.i.TargetType;


						Write("new ");
						Write(GetDecoratedTypeName(e.i.TargetType, true, false));
						Write("(");
						EmitFirstOnStack(e);
						Write(")");
					}
				};

			#region conv
			CIW[OpCodes.Conv_I1,
				OpCodes.Conv_U1] = e => ConvertTypeAndEmit(e, "byte");

			CIW[OpCodes.Conv_I4,
				OpCodes.Conv_U,
				OpCodes.Conv_Ovf_I,
				OpCodes.Conv_U4] = e => ConvertTypeAndEmit(e, "int");


			CIW[OpCodes.Conv_I2] = e => ConvertTypeAndEmit(e, "short");
			CIW[OpCodes.Conv_U2] = e => ConvertTypeAndEmit(e, "char");



			CIW[OpCodes.Conv_I8] = e => ConvertTypeAndEmit(e, "long");
			CIW[OpCodes.Conv_U8] = e => ConvertTypeAndEmit(e, "long");

			CIW[OpCodes.Conv_R4] = e => ConvertTypeAndEmit(e, "float");
			CIW[OpCodes.Conv_R8] = e => ConvertTypeAndEmit(e, "double");

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
											else if (e.i.TargetType == typeof(uint))
												Write("0");
											else if (e.i.TargetType == typeof(byte))
												Write("0");
											else if (e.i.TargetType == typeof(ushort))
												Write("0");
											else if (e.i.TargetType == typeof(sbyte))
												Write("0");
											else
												BreakToDebugger("default for " + e.i.TargetType.FullName + " is unknown");
										}
									}
									else
									{
										if (e.i.TargetType == typeof(byte))
										{
											Write("(byte)");
											// bytes should be written in hex
											if (_stack[si].SingleStackInstruction.TargetInteger != null)
											{
												this.Write(
													string.Format("0x{0:x2}", _stack[si].SingleStackInstruction.TargetInteger)
												);
											}
											else
											{
												Emit(e.p, _stack[si]);
											}
										}
										else
										{
											if (e.i.TargetType == typeof(uint))
												Write("(int)");

											if (e.i.TargetType == typeof(ushort))
												Write("(short)");

											Emit(e.p, _stack[si]);
										}
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

											   Write("(int)");
											   Write(Values[i].ToString());
										   }

									   }
									   else if (Type == typeof(byte))
									   {
										   var Values = e.i.NextInstruction.NextInstruction.TargetField.GetValue(null).StructAsByteArray();


										   for (int i = 0; i < Values.Length; i++)
										   {
											   if (i > 0)
												   Write(", ");

											   Write("(byte)");
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


					// loading this pointer
					if (e.i.TargetParameter != null)
					{
						#region byte
						if (e.i.TargetParameter.ParameterType == typeof(byte))
						{
							Write("(short)");
							Write("(");
							WriteMethodParameterOrSelf(e.i);

							// this operator is either 16bit or 32bit, depends on VM
							Write(" & 0xff");
							Write(")");

							return;
						}
						#endregion

						#region uint
						if (e.i.TargetParameter.ParameterType == typeof(uint))
						{
							Write("(long)");
							Write("(");
							WriteMethodParameterOrSelf(e.i);

							// this operator is either 16bit or 32bit, depends on VM
							Write(" & 0xffffffffL");
							Write(")");

							return;
						}
						#endregion
					}

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

					if (e.i.TargetParameter.ParameterType == typeof(byte))
					{
						// we will store ubyte as sbyte
						Write("(byte)");
					}

					if (e.i.TargetParameter.ParameterType == typeof(uint))
					{
						// we will store ubyte as sbyte
						Write("(int)");
					}

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

					   if (e.i.TargetField.FieldType == typeof(byte))
					   {
						   // we will store ubyte as sbyte
						   Write("(byte)");
					   }

					   if (e.i.TargetField.FieldType == typeof(uint))
					   {
						   // we will store ubyte as sbyte
						   Write("(int)");
					   }

					   Emit(e.p, e.FirstOnStack);
				   }
				   catch (Exception exc)
				   {
					   throw exc;
				   }
			   };
			#endregion


			#region Ldsfld
			CIW[OpCodes.Ldsfld] =
				delegate(CodeEmitArgs e)
				{
					ILFlow.StackItem[] s = e.i.StackBeforeStrict;

					#region byte
					if (e.i.TargetField.FieldType == typeof(byte))
					{
						Write("(short)");
						Write("(");
						WriteDecoratedTypeName(e.i.TargetField.DeclaringType);
						WriteTypeStaticAccessor();
						WriteSafeLiteral(e.i.TargetField.Name);

						// this operator is either 16bit or 32bit, depends on VM
						Write(" & 0xff");
						Write(")");

						return;
					}
					#endregion

					#region byte
					if (e.i.TargetField.FieldType == typeof(uint))
					{
						Write("(long)");
						Write("(");
						WriteDecoratedTypeName(e.i.TargetField.DeclaringType);
						WriteTypeStaticAccessor();
						WriteSafeLiteral(e.i.TargetField.Name);

						// this operator is either 16bit or 32bit, depends on VM
						Write(" & 0xffffffL");
						Write(")");

						return;
					}
					#endregion

					WriteDecoratedTypeName(e.i.TargetField.DeclaringType);
					WriteTypeStaticAccessor();
					WriteSafeLiteral(e.i.TargetField.Name);
				};
			#endregion

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
					var AsExtensionMethod = false;

					if (e.i.TargetConstructor.DeclaringType.IsDelegate())
					{
						var TargetIsNotNull = e.i.StackBeforeStrict[0].SingleStackInstruction != OpCodes.Ldnull;
						var TargetMethodIsStatic = e.i.StackBeforeStrict[1].SingleStackInstruction.TargetMethod.IsStatic;

						if (TargetMethodIsStatic)
							if (TargetIsNotNull)
							{
								AsExtensionMethod = true;

							}
					}

					if (AsExtensionMethod)
					{
						Write("(");
						WriteDecoratedTypeNameOrImplementationTypeName(e.i.TargetConstructor.DeclaringType);
						Write(")");
					}

					WriteTypeConstruction(e);


					if (AsExtensionMethod)
					{

						Write(".");
						Write(DelegateImplementationProvider.AsExtensionMethod);
						Write("()");
					}
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

					if (e.i.TargetVariable.LocalType == typeof(byte))
					{
						// we will store ubyte as sbyte
						Write("(byte)");
					}

					if (e.i.TargetVariable.LocalType == typeof(uint))
					{
						// we will store ubyte as sbyte
						Write("(int)");
					}

					if (e.i.TargetVariable.LocalType == typeof(ushort))
					{
						// we will store ubyte as sbyte
						Write("(short)");
					}

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

				   #region IsCompound
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
				   #endregion

				   if (e.i.TargetVariable.LocalType == typeof(byte))
				   {
					   Write("(short)");
					   Write("(");
					   WriteVariableName(e.i.OwnerMethod.DeclaringType, e.i.OwnerMethod, e.i.TargetVariable);

					   // this operator is either 16bit or 32bit, depends on VM
					   Write(" & 0xff");
					   Write(")");
				   }
				   else if (e.i.TargetVariable.LocalType == typeof(uint))
				   {
					   Write("(long)");
					   Write("(");
					   WriteVariableName(e.i.OwnerMethod.DeclaringType, e.i.OwnerMethod, e.i.TargetVariable);

					   Write(" & 0xffffffffL");
					   Write(")");
				   }
				   else
				   {
					   WriteVariableName(e.i.OwnerMethod.DeclaringType, e.i.OwnerMethod, e.i.TargetVariable);
				   }

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
						   // long literal has the suffix L
						   // http://www.java2s.com/Tutorial/Java/0040__Data-Type/Ahexadecimalliteraloftypelong.htm

						   MyWriter.Write(x.Value);
						   MyWriter.Write("L");
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
			// are we missing something important in implementation?
			CIW[OpCodes.Shr,
				OpCodes.Shr_Un] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, ">>"); };

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

			CIW[OpCodes.Or] = delegate(CodeEmitArgs e)
			{
				WriteInlineOperator(e.p, e.i, "|");
			};

			CIW[OpCodes.And] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "&"); };
			CIW[OpCodes.Rem,
				OpCodes.Rem_Un] = delegate(CodeEmitArgs e) { WriteInlineOperator(e.p, e.i, "%"); };
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
