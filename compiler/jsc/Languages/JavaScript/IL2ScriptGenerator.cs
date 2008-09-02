
using System;

using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;

using ScriptCoreLib;

using jsc.Script;
using jsc.Languages.JavaScript;

namespace jsc
{
	using ilbp = ILBlock.Prestatement;
	using ili = ILInstruction;
	using ilfsi = ILFlow.StackItem;

	public delegate void OpCodeHandler(IdentWriter w, ilbp p, ili i, ilfsi[] s);

	class IL2ScriptGenerator
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

			Handlers[OpCodes.Ret] = new OpCodeHandler(OpCode_ret);
			Handlers[OpCodes.Leave_S, OpCodes.Leave] = new OpCodeHandler(OpCode_leave);

			Handlers[OpCodes.Br_S,
					 OpCodes.Br] = new OpCodeHandler(OpCode_br);

			Handlers[OpCodes.Ldarg_0,
					 OpCodes.Ldarg_1,
					 OpCodes.Ldarg_2,
					 OpCodes.Ldarg_3,
					 OpCodes.Ldarg,
					 OpCodes.Ldarg_S,
					 OpCodes.Ldarga_S,
					 OpCodes.Ldarga] = new OpCodeHandler(OpCode_ldarg);


			Handlers[OpCodes.Starg_S] = new OpCodeHandler(OpCode_starg);

			Handlers[OpCodes.Ldloc_0,
					 OpCodes.Ldloc_1,
					 OpCodes.Ldloc_2,
					 OpCodes.Ldloc_3,
					 OpCodes.Ldloc_S,
					 OpCodes.Ldloca,
					 OpCodes.Ldloca_S,
					 OpCodes.Ldloc] = new OpCodeHandler(OpCode_ldloc);


			Handlers[OpCodes.Stloc_0,
					 OpCodes.Stloc_1,
					 OpCodes.Stloc_2,
					 OpCodes.Stloc_3,
					 OpCodes.Stloc_S,
					 OpCodes.Stloc] = new OpCodeHandler(OpCode_stloc);

			Handlers[OpCodes.Sub,
					 OpCodes.Add,
					 OpCodes.Add_Ovf,
					 OpCodes.Add_Ovf_Un,
					 OpCodes.Div,
					 OpCodes.Div_Un,
					 OpCodes.Mul,
					 OpCodes.Rem,
					 OpCodes.And,
					 OpCodes.Or,
					 OpCodes.Xor,
					 OpCodes.Not,
					 OpCodes.Shl,
					 OpCodes.Shr,
					 OpCodes.Shr_Un,
					 OpCodes.Neg,
					 OpCodes.Ceq,
					 OpCodes.Cgt,
					 OpCodes.Cgt_Un,
					 OpCodes.Clt,
					 OpCodes.Clt_Un
					 ] = new OpCodeHandler(OpCode_LogicOperators);


			Handlers[OpCodes.Ldc_I4,
					 OpCodes.Ldc_I4_S,
					 OpCodes.Ldc_I4_0,
					 OpCodes.Ldc_I4_1,
					 OpCodes.Ldc_I4_2,
					 OpCodes.Ldc_I4_3,
					 OpCodes.Ldc_I4_4,
					 OpCodes.Ldc_I4_5,
					 OpCodes.Ldc_I4_6,
					 OpCodes.Ldc_I4_7,
					 OpCodes.Ldc_I4_8,
					 OpCodes.Ldc_R4,
					 OpCodes.Ldc_R8,
					 OpCodes.Ldc_I8,
					 OpCodes.Ldc_I4_M1] = new OpCodeHandler(OpCode_ldc);

			Handlers[OpCodes.Ldstr] = new OpCodeHandler(OpCode_ldstr);

			Handlers[OpCodes.Call] = new OpCodeHandler(OpCode_call);
			Handlers[OpCodes.Callvirt] = new OpCodeHandler(OpCode_call);


			Handlers[OpCodes.Beq_S] = new OpCodeHandler(OpCode_beq);
			Handlers[OpCodes.Bgt_S] = new OpCodeHandler(OpCode_bgt);
			Handlers[OpCodes.Blt_S] = new OpCodeHandler(OpCode_blt);
			Handlers[OpCodes.Ble_S] = new OpCodeHandler(OpCode_ble);
			Handlers[OpCodes.Bge_S] = new OpCodeHandler(OpCode_bge);
			Handlers[OpCodes.Bne_Un_S] = new OpCodeHandler(OpCode_bne_un);

			// fixme: leace_S my leave function

			Handlers[OpCodes.Box] = new OpCodeHandler(OpCode_box);
			Handlers[OpCodes.Castclass] = new OpCodeHandler(OpCode_castclass);
			Handlers[OpCodes.Endfinally] = new OpCodeHandler(OpCode_endfinally);

			Handlers[OpCodes.Conv_R8,
					OpCodes.Conv_I4,
					OpCodes.Conv_Ovf_I4,
					OpCodes.Conv_Ovf_I4_Un,
					OpCodes.Conv_I8,
					OpCodes.Conv_U8,
					OpCodes.Conv_R4,
					OpCodes.Conv_U4,
					OpCodes.Conv_U2,
					 OpCodes.Conv_U1
					] = new OpCodeHandler(OpCode_conv);

			Handlers[
					 OpCodes.Unbox_Any,



					 OpCodes.Nop,
					 OpCodes.Constrained] = new OpCodeHandler(OpCode_donothing);

			Handlers[OpCodes.Throw] = new OpCodeHandler(OpCode_throw);
			Handlers[OpCodes.Rethrow] = new OpCodeHandler(OpCode_rethrow);
			Handlers[OpCodes.Isinst] = new OpCodeHandler(OpCode_isinst);
			Handlers[OpCodes.Dup] = new OpCodeHandler(OpCode_dup);
			Handlers[OpCodes.Pop] = new OpCodeHandler(OpCode_pop);
			Handlers[OpCodes.Newarr] = new OpCodeHandler(OpCode_newarr);
			Handlers[OpCodes.Newobj] = new OpCodeHandler(OpCode_newobj);
			Handlers[OpCodes.Initobj] = new OpCodeHandler(OpCode_initobj);

			Handlers[OpCodes.Ldlen] = new OpCodeHandler(OpCode_ldlen);
			Handlers[OpCodes.Ldnull] = new OpCodeHandler(OpCode_ldnull);
			Handlers[OpCodes.Ldftn] = new OpCodeHandler(OpCode_ldftn);
			Handlers[OpCodes.Ldvirtftn] = OpCode_ldvirtftn;
			Handlers[OpCodes.Ldtoken] = OpCode_ldtoken;

			Handlers[OpCodes.Stelem,
					 OpCodes.Stelem_Ref,
					 OpCodes.Stelem_I1,
					 OpCodes.Stelem_I4,
					 OpCodes.Stelem_R8,
					 OpCodes.Stelem_I2] = OpCode_stelem;

			Handlers[OpCodes.Stobj] = OpCode_stobj;

			Handlers[OpCodes.Ldobj] = OpCode_ldobj;


			Handlers[OpCodes.Ldelem_Ref,
				OpCodes.Ldelem_U1,
				OpCodes.Ldelem_U2,
				OpCodes.Ldelem_I1,
				OpCodes.Ldelem_I4,
				OpCodes.Ldelem_R8,
				OpCodes.Ldelema,
				OpCodes.Ldelem
					 ] = new OpCodeHandler(OpCode_ldelem);

			Handlers[OpCodes.Stfld,
					 OpCodes.Stsfld] = new OpCodeHandler(OpCode_stfld);

			Handlers[OpCodes.Ldfld,
					 OpCodes.Ldflda,
					 OpCodes.Ldsfld] = new OpCodeHandler(OpCode_ldfld);
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
			ScriptAttribute sq = ScriptAttribute.Of(m);
			ScriptAttribute sqt = ScriptAttribute.Of(m.DeclaringType);

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

			Debugger.Break();
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
			object[] o = i.TargetField.DeclaringType.GetCustomAttributes(typeof(ScriptAttribute), true);

			if (o.Length == 1)
			{
				ScriptAttribute sa = o[0] as ScriptAttribute;

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

			w.WriteDecoratedMemberInfo(i.TargetField);


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

			w.WriteDecoratedMemberInfo(i.TargetMethod, true);
		}


		static bool OpCode_newobj_override(IdentWriter w, ilbp p, ili i, ilfsi[] s)
		{
			Type _decl_type = i.TargetConstructor.DeclaringType;

			#region custom implementation



			//ScriptAttribute a = ScriptAttribute.Of(_decl_type);

			//#region ExternalTarget fixup
			//if (a != null && a.ExternalTarget != null)
			//{

			//    w.Write("new ");
			//    w.Write(a.ExternalTarget);
			//    w.Write("(");
			//    w.WriteParameters(p, i, s, 0, null);
			//    w.Write(")");

			//    return true;
			//}
			//#endregion




			MethodBase _impl_type_ctor = w.Session.ResolveImplementation(_decl_type, i.TargetConstructor);

			if (_impl_type_ctor != null)
			{
				//w.WriteCommentLine(_impl_type_ctor.DeclaringType.FullName + "." + _impl_type_ctor.Name);

				WriteCreateType(w, p, i, s, _impl_type_ctor);

				/*
				w.Helper.DOMCreateAndInvokeConstructor(

					_impl_type_ctor.DeclaringType,
					_impl_type_ctor, p, i, s);
				 */

				return true;
			}


			#endregion


			return false;
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

		static void OpCode_newobj(IdentWriter w, ilbp p, ili i, ilfsi[] s)
		{
			MethodBase m = i.TargetConstructor;

			if (ScriptAttribute.IsAnonymousType(m.DeclaringType))
			{
				goto TryDefault;
			}

			//if (ScriptAttribute.IsCompilerGenerated(m.DeclaringType))
			if (ScriptAttribute.OfProvider(m.DeclaringType) == null
				&& w.Session.ResolveImplementation(m.DeclaringType) == null)
			{
				w.Write("/* DOMCreateType */");
				w.Helper.DOMCreateType(m.DeclaringType);

				return;
			}


			if (OpCode_newobj_override(w, p, i, s))
				return;

			var m_type_attribute = ScriptAttribute.Of(m.DeclaringType, true);



			#region missing script attribute
			if (m_type_attribute == null)
			{
				if (m.DeclaringType.IsArray)
				{
					// when is this hit? arrays are implemented!

					Task.Error("new array not implemented");
					Task.Fail(i);
				}

				using (StringWriter sw = new StringWriter())
				{
					// static? in js?

					if (m.IsStatic)
						sw.Write("static ");

					sw.Write("{0}.{1}", m.DeclaringType.FullName, m.Name);
					sw.Write("(");
					int pix = 0;
					foreach (ParameterInfo pi in m.GetParameters())
					{
						if (pix++ > 0)
							sw.Write(", ");

						sw.Write(pi.ParameterType.FullName);
					}

					sw.Write(")");

					Task.Error("Missing Script Attribute?\nNative constructor was invoked, please implement [{0}]", sw.ToString());

					w.Session.ResolveImplementationTrace(m.DeclaringType, m);

					Task.Fail(i);
				}
				Debugger.Break();
			}
			#endregion

		TryDefault:

			WriteCreateType(w, p, i, s, m);

		}

		private static void WriteCreateType(IdentWriter w, ilbp p, ili i, ilfsi[] s, MethodBase m)
		{
			ScriptAttribute sa =
				ScriptAttribute.IsAnonymousType(m.DeclaringType) ?
					new ScriptAttribute() :
					ScriptAttribute.Of(m.DeclaringType, true);

			if (sa == null)
				Script.CompilerBase.BreakToDebugger("no script attribute for type " + m.DeclaringType.FullName);


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
							if (i.TargetType == typeof(int))
								w.Write("0");
							else if (i.TargetType == typeof(sbyte))
								w.Write("0");
							else
								CompilerBase.BreakToDebugger("default for " + i.TargetType.FullName + " is unknown");
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
				if (s.First().SingleStackInstruction == OpCodes.Ldc_I4_0)
				{
					w.Write("[]");
				}
				else
				{
					w.Write("new Array(");
					IL2ScriptGenerator.OpCodeHandler(w, p, i, s[0]);
					w.Write(")");
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

		static void OpCode_isinst(IdentWriter w, ilbp p, ili i, ilfsi[] s)
		{
			// this opcode should unwrap itself from cgt null
			// we might be inside double not operator

			// we might need the library help here

			w.Write("!(");
			OpCodeHandler(w, p, i, s[0]);

			w.WriteSpace();
			w.Write("instanceof");
			// http://developer.mozilla.org/en/Core_JavaScript_1.5_Reference/Operators/Special_Operators/instanceof_Operator
			w.WriteSpace();

			w.WriteDecoratedType(w.Session.ResolveImplementation(i.TargetType) ?? i.TargetType, false);
			w.Write(")");

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
						OpCodeHandler(w, p, i, s[0]);

						// il is like this:
						// (u as T) != null
						// yet javascript supports this:
						// u instanceof T

						return;
					}


			w.Write("(");

			OpCodeHandler(w, p, i, s[0]);

			w.WriteSpace();

			if (i.IsAnyOpCodeOf(OpCodes.Div, OpCodes.Div_Un)) w.Write("/");

			if (i == OpCodes.Sub) w.Write("-");
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
			if (i == OpCodes.Rem) w.Write("%");

			w.WriteSpace();

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