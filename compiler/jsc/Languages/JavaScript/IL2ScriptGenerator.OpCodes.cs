
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


	partial class IL2ScriptGenerator
	{
		private static void CreateInstructionHandlers()
		{
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
					 OpCodes.Rem_Un,
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
					OpCodes.Conv_I2,
					OpCodes.Conv_Ovf_I4,
					OpCodes.Conv_Ovf_I4_Un,
					OpCodes.Conv_I8,
					OpCodes.Conv_U8,
					OpCodes.Conv_R4,
					OpCodes.Conv_U4,
					OpCodes.Conv_U2,
					OpCodes.Conv_U,
					 OpCodes.Conv_U1,
					 OpCodes.Conv_Ovf_I
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
				OpCodes.Ldelem_U4,
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

	}

}