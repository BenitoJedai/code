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
		public static void EmitInstance(this ILGenerator il, object value, ILTranslationContext context)
		{
			var t = value.GetType();
			var ctor = t.GetConstructor(new Type[0]);

			// lets now implement C# field initializer

			var loc = il.DeclareLocal(context.TypeCache[t]);

			il.Emit(OpCodes.Newobj, context.ConstructorCache[ctor]);
			il.Emit(OpCodes.Stloc_S, (byte)loc.LocalIndex);

			foreach (var k in from f in t.GetFields()
							  let v = f.GetValue(value)
							  select new { f, v })
			{

				il.Emit(OpCodes.Ldloc_S, (byte)loc.LocalIndex);

				if (k.f.FieldType == typeof(string))
				{
					il.Emit(OpCodes.Ldstr, (string)k.v);
				}
				else throw new NotImplementedException();

				il.Emit(OpCodes.Stfld, context.FieldCache[k.f]);
			}


			il.Emit(OpCodes.Ldloc_S, (byte)loc.LocalIndex);
		}


	}
}
