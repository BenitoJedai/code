using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;

namespace jsc.Languages.IL
{
	/// <summary>
	/// Here we are years later, actually rewriting IL for a later phase!
	/// </summary>
	public static class ILTranslationExtensions
	{
		public static void EmitTo(this Delegate source, ILGenerator il, Func<ConstructorInfo, ConstructorInfo> Newobj)
		{
			source.Method.EmitTo(il, Newobj);
		}

		public static void EmitTo(this MethodBase m, ILGenerator il, Func<ConstructorInfo, ConstructorInfo> Newobj)
		{
			//+		[0]	{[0x0000] nop        +0 -0}	jsc.ILInstruction
			//+		[1]	{[0x0001] newobj     +1 -0}	jsc.ILInstruction
			//+		[2]	{[0x0006] stloc.0    +0 -1{[0x0001] newobj     +1 -0} }	jsc.ILInstruction
			//+		[3]	{[0x0007] newobj     +1 -0}	jsc.ILInstruction
			//+		[4]	{[0x000c] stloc.1    +0 -1{[0x0007] newobj     +1 -0} }	jsc.ILInstruction
			//+		[5]	{[0x000d] ldarg.0    +1 -0}	jsc.ILInstruction
			//+		[6]	{[0x000e] ldloc.0    +1 -0}	jsc.ILInstruction
			//+		[7]	{[0x000f] callvirt   +0 -2{[0x000d] ldarg.0    +1 -0} {[0x000e] ldloc.0    +1 -0} }	jsc.ILInstruction
			//+		[8]	{[0x0014] nop        +0 -0}	jsc.ILInstruction
			//+		[9]	{[0x0015] ldloc.1    +1 -0}	jsc.ILInstruction
			//+		[10]	{[0x0016] ldloc.0    +1 -0}	jsc.ILInstruction
			//+		[11]	{[0x0017] call       +1 -2{[0x0015] ldloc.1    +1 -0} {[0x0016] ldloc.0    +1 -0} }	jsc.ILInstruction
			//+		[12]	{[0x001c] pop        +0 -1{[0x0017] call       +1 -2{[0x0015] ldloc.1    +1 -0} {[0x0016] ldloc.0    +1 -0} } }	jsc.ILInstruction
			//+		[13]	{[0x001d] ret        +0 -0}	jsc.ILInstruction

			var body = m.GetMethodBody();

			var locals = Enumerable.ToArray(
				from local in body.LocalVariables
				let declared = il.DeclareLocal(local.LocalType)
				select new { local, declared }
			).ToDictionary(
				k => k.local.LocalIndex,
				k => k.declared.LocalIndex
			);



			new Dictionary<OpCode, Action<ILInstruction>>
			{
				{OpCodes.Nop, i => il.Emit(OpCodes.Nop)},
				{OpCodes.Newobj, i => il.Emit(OpCodes.Newobj, Newobj(i.TargetConstructor))},
				{OpCodes.Stloc_0, i => il.Emit(OpCodes.Stloc_S, (byte)locals[0])},
				{OpCodes.Stloc_1, i => il.Emit(OpCodes.Stloc_S, (byte)locals[1])},
				{OpCodes.Ldloc_0, i => il.Emit(OpCodes.Ldloc_S, (byte)locals[0])},
				{OpCodes.Ldloc_1, i => il.Emit(OpCodes.Ldloc_S, (byte)locals[1])},
				{OpCodes.Ldarg_0, i => il.Emit(OpCodes.Ldarg_0)},
				{OpCodes.Callvirt, i => il.Emit(OpCodes.Callvirt, i.TargetMethod)},
				{OpCodes.Call, i => il.Emit(OpCodes.Call, i.TargetMethod)},
				{OpCodes.Pop, i => il.Emit(OpCodes.Pop)},
				{OpCodes.Ret, i => il.Emit(OpCodes.Ret)},
			}.Translate(new ILBlock(m));
		}

		static void Translate(this  Dictionary<OpCode, Action<ILInstruction>> handler, ILBlock s)
		{
			foreach (var i in s.Instructrions)
			{
				handler[i.OpCode](i);
			}
		}

	}
}
