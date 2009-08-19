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

		public static void EmitCall(this ILGenerator il, Delegate h)
		{
			il.EmitCall(OpCodes.Call, h.Method, null);
		}

		public static void EmitCall(this ILGenerator il, Action<Type> h, Type t)
		{
			il.EmitTypeOf(t);
			il.EmitCall(OpCodes.Call, h.Method, null);
		}

		public static void EmitCall(this ILGenerator il, Action h)
		{
			il.EmitCall(OpCodes.Call, h.Method, null);
		}


		public static void EmitPopCallTo<_Type, T>(this Func<_Type, T> h, ILGenerator il, _Type t)
			where _Type : Type
		{
			il.EmitTypeOf(t);
			il.EmitCall(OpCodes.Call, h.Method, null);
			il.Emit(OpCodes.Pop);
		}
	}
}
