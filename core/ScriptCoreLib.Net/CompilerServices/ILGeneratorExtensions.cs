using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;

namespace ScriptCoreLib.CompilerServices
{
	public static class ILGeneratorExtensions
	{
		public static void EmitTypeOf(this ILGenerator il, Type t)
		{
			il.Emit(OpCodes.Ldtoken, t);
			il.EmitCall(OpCodes.Call, new Func<RuntimeTypeHandle, Type>(Type.GetTypeFromHandle).Method, null);
		}

		public static void EmitCall(this ILGenerator il, Action<Type> h, Type t)
		{
			il.EmitTypeOf(t);
			il.EmitCall(OpCodes.Call, h.Method, null);

		}
	}
}
