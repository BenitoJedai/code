using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;

namespace jsc.meta.Library
{
	public static class ToStringProvider
	{
		public static void DefineToStringMethod(this TypeBuilder t, IEnumerable<FieldBuilder> f)
		{
			var m = t.DefineMethod("ToString", System.Reflection.MethodAttributes.Public | System.Reflection.MethodAttributes.Virtual, typeof(string), null);

			var il = m.GetILGenerator();

			il.Emit(OpCodes.Newobj, typeof(StringBuilder).GetConstructor(new Type[0]));

			Action Append =
				delegate
				{
					il.Emit(OpCodes.Call, typeof(StringBuilder).GetMethod("Append", new[] { typeof(string) }));
				};

			Action<string> AppendString =
				n =>
				{
					il.Emit(OpCodes.Ldstr, n);
					Append();
				};


			AppendString("{ ");

			foreach (var k in f.Select((q, i) => new { q, i }))
			{
				if (k.i > 0)
					AppendString(", ");

				AppendString(k.q.Name + " = ");
				il.Emit(OpCodes.Ldarg_0);
				il.Emit(OpCodes.Ldfld, k.q);

				Append();
			}

			AppendString(" }");

		
			il.Emit(OpCodes.Call, typeof(StringBuilder).GetMethod(m.Name, new Type[0]));
			il.Emit(OpCodes.Ret);

			t.DefineMethodOverride(m, typeof(object).GetMethod(m.Name));


		}
	}
}
