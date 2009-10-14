using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using jsc.Languages.IL;

namespace jsc.meta.Library
{
	public static class WriteToProvider
	{
		/// <summary>
		/// Creates a WriteToArguments nested type and creates a method which will
		/// stream all fields to the handler
		/// </summary>
		/// <param name="t"></param>
		/// <param name="f"></param>
		public static void DefineWriteToMethod(this TypeBuilder t, IEnumerable<FieldBuilder> f)
		{
			var WriteToArguments = t.DefineNestedType(
				"WriteToArguments",
				System.Reflection.TypeAttributes.NestedPublic,
				typeof(EventArgs)
			);

			var WriteToArguments_Sender = WriteToArguments.DefineField("Sender", t, System.Reflection.FieldAttributes.Public);
			var WriteToArguments_Position = WriteToArguments.DefineField("Position", typeof(int), System.Reflection.FieldAttributes.Public);
			var WriteToArguments_Count = WriteToArguments.DefineField("Count", typeof(int), System.Reflection.FieldAttributes.Public);
			var WriteToArguments_Name = WriteToArguments.DefineField("Name", typeof(string), System.Reflection.FieldAttributes.Public);
			var WriteToArguments_Value = WriteToArguments.DefineField("Value", typeof(string), System.Reflection.FieldAttributes.Public);

			var WriteToArguments_Fields = new[] {
				WriteToArguments_Sender,
				WriteToArguments_Position,
				WriteToArguments_Count,
				WriteToArguments_Name,
				WriteToArguments_Value
			};
			var WriteToArguments_ctor = WriteToArguments.DefineDefaultConstructor(System.Reflection.MethodAttributes.Public);

			var WriteToArgumentsType = WriteToArguments.CreateType();

			var m = t.DefineMethod("WriteTo",
				System.Reflection.MethodAttributes.Public,
				typeof(void),
				new[] { typeof(EventHandler) }
			);

			{
				var il = m.GetILGenerator();


				foreach (var k in f.Select((q, i) => new { q, i }))
				{
					Action<object, EventHandler, int, int, string, string> Initialize =
						(_this, _handler, _Position, _Count, _Name, _Value) =>
						{
							var a = new WriteToArgumentsMarker();

							a.Sender = _this;
							a.Position = _Position;
							a.Count = _Count;
							a.Name = _Name;
							a.Value = _Value;

							_handler(_this, a);
						};

					var ea = new ILTranslationExtensions.EmitToArguments
					{
						Newobj_redirect =
							ctor => ctor.DeclaringType == typeof(WriteToArgumentsMarker)
								? WriteToArguments_ctor
								: ctor,

						DefineLocal_redirect =
							_t => _t == typeof(WriteToArgumentsMarker)
								? WriteToArguments
								: _t,
					};


					// _this - sender
					// Ldarg_0
					ea[OpCodes.Ldarg_2] = e => e.il.Emit(OpCodes.Ldc_I4, k.i);
					ea[OpCodes.Ldarg_3] = e => e.il.Emit(OpCodes.Ldc_I4, f.Count());
					ea[OpCodes.Ldarg_S] =
						e =>
						{
							if (e.i.OpParamAsInt8 == 4)
							{
								e.il.Emit(OpCodes.Ldstr, k.q.Name);
								return;
							}

							if (e.i.OpParamAsInt8 == 5)
							{
								e.il.Emit(OpCodes.Ldarg_0);
								e.il.Emit(OpCodes.Ldfld, k.q);
								return;
							}

							throw new NotSupportedException();
						};

					ea[OpCodes.Stfld] =
						e =>
						{
							if (e.i.TargetField.DeclaringType == typeof(WriteToArgumentsMarker))
							{
								e.il.Emit(OpCodes.Stfld,
									WriteToArguments_Fields.Single(_k => _k.Name == e.i.TargetField.Name)
								);
								return;
							}

							e.il.Emit(OpCodes.Stfld, e.i.TargetField);

						};

					ea[OpCodes.Ret] = delegate {};

					Initialize.EmitTo(il, ea);
				}

				il.Emit(OpCodes.Ret);
			}
		}


		class WriteToArgumentsMarker : EventArgs
		{
			public object Sender;
			public int Position;
			public int Count;
			public string Name;
			public string Value;
		}


	}
}
