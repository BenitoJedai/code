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
	public static partial class ILTranslationExtensions
	{
		public static void EmitTo(this Delegate source, ILGenerator il, Func<ConstructorInfo, ConstructorInfo> Newobj_redirect)
		{
			source.Method.EmitTo(il,
				new EmitToArguments
				{
					Newobj_redirect = Newobj_redirect
				}
			);
		}

		public static void EmitTo(this Delegate source, ILGenerator il, EmitToArguments a)
		{
			source.Method.EmitTo(il, a);
		}


		public static void EmitTo(this MethodBase m, ILGenerator il)
		{
			EmitTo(m, il, new EmitToArguments());
		}

		public static void EmitTo(this MethodBase m, ILGenerator il, EmitToArguments a)
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
				let declared = il.DeclareLocal(a.DefineLocal_redirect(local.LocalType))
				select new { local, declared }
			).ToDictionary(
				k => k.local.LocalIndex,
				k => k.declared.LocalIndex
			);

			var x = new EmitToArguments(a.Configuration)
			{
				TranslateLocalIndex = LocalIndex => locals[LocalIndex]
			};

			var xb = new ILBlock(m);

			var missing = xb.Instructrions.Where(k => !x.Configuration.ContainsKey(k.OpCode)).Select(k => k.OpCode.ToString()).ToArray();

			if (missing.Any())
			{
				throw new NotSupportedException(
					"Some OpCodes are not supported yet: " + string.Join(", ", missing)
				);
			}

			foreach (var i in xb.Instructrions)
			{
				x.Configuration[i.OpCode](new EmitToArguments.ILRewriteContext { i = i, il = il });
			}

	
		}



	}
}
