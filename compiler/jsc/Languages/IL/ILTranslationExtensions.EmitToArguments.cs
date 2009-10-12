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
		public class EmitToArguments
		{
			public Action<ILInstruction, ILGenerator> Ldarg_0 =
				(i, il) => il.Emit(OpCodes.Ldarg_0);

			public Action<ILInstruction, ILGenerator> Ldarg_1 =
				(i, il) => il.Emit(OpCodes.Ldarg_1);

			public Action<ILInstruction, ILGenerator> Ldarg_2 =
				(i, il) => il.Emit(OpCodes.Ldarg_2);

			public Action<ILInstruction, ILGenerator> Ldarg_3 =
				(i, il) => il.Emit(OpCodes.Ldarg_3);

			public Action<ILInstruction, ILGenerator> Ldarg_S =
				(i, il) => il.Emit(OpCodes.Ldarg_S);

			public Action<ILInstruction, ILGenerator> Ret =
				(i, il) => il.Emit(OpCodes.Ret);

			public Action<ILInstruction, ILGenerator> Stfld =
				(i, il) => il.Emit(OpCodes.Stfld, i.TargetField);

			public Action<ILInstruction, ILGenerator> Ldfld =
				(i, il) => il.Emit(OpCodes.Ldfld, i.TargetField);

			public Func<ConstructorInfo, ConstructorInfo> Newobj_redirect =
				ctor => ctor;

			public Func<Type, Type> DefineLocal_redirect =
				t => t;

		}


	}
}
