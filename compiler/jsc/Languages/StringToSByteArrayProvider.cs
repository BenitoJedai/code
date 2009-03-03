using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ScriptCoreLib.CSharp.Extensions;
using System.Reflection.Emit;

namespace jsc.Languages
{
	public static class StringToSByteArrayProvider
	{
		public class Info
		{
			public Func<string, sbyte[]> ToSBytes;

			public MethodInfo TargetMethod;

			public ILFlow.StackItem[] Arguments;
		}

	

		public static Info GetProvideImplementation(MethodBase m)
		{
			if (m == null)
				return null;

			if (m.GetMethodBody() == null)
				return null;

			if (m.DeclaringType.Assembly.ToScriptAttribute() == null)
				return null;

			// some code is calling to a method, which has no script attribute applied
			// if that function is now in turn calling to a method with script attribute
			// we could resolve strings to sbyte[].


			var c = new ILBlock(m);

			// the block should only contain a call instruction
			// we can ignore nop and ret opcodes

			var PrestatementCommands = c.Prestatements.PrestatementCommands;

			if (PrestatementCommands.Any(k => k.Instruction == null))
				return null;


			var i = Enumerable.ToArray(
				from k in PrestatementCommands
				where k.Instruction.OpCode != OpCodes.Nop
				where k.Instruction.OpCode != OpCodes.Ret
				select k.Instruction
			);

			if (i.Length != 1)
				return null;

			// did we find a call instruction?
			var TargetMethod = i[0].TargetMethod;

			// no!
			if (TargetMethod == null)
				return null;

			// we should be calling to a script type
			if (TargetMethod.DeclaringType.ToScriptAttribute() == null)
				return null;

			// now we need to verify all parameters being passed to that script type
			// the only exception is ToSBytes which we will convert at compile time

			Func<string, sbyte[]> ToSBytes = StringToSByteArray.ToSBytes;


			foreach (var arg in i[0].StackBeforeStrict)
			{
				var SingleStackInstruction = arg.SingleStackInstruction;

				if (SingleStackInstruction.TargetParameter != null)
					continue;

				var arg_TargetMethod = SingleStackInstruction.TargetMethod;

				if (arg_TargetMethod != null)
				{
					if (arg_TargetMethod == ToSBytes.Method)
					{
						// this is the only special method we are supporting at this time
						continue;
					}
				}

				return null;
			}


			return new Info
			{
				Arguments = i[0].StackBeforeStrict,
				TargetMethod = TargetMethod,
				ToSBytes = ToSBytes
			};
		}

	}
}
