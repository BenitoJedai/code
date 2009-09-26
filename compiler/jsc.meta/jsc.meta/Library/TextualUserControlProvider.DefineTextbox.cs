using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;
using System.Windows.Forms;
using System.IO;

namespace jsc.meta.Library
{
	partial class TextualUserControlProvider
	{

		private static void DefineTextbox(TypeBuilder t, ILGenerator il, string Name, int Left, int Top, int Width, int Height, string Text)
		{

			var c = t.DefineField("TextBox" + Name, typeof(System.Windows.Forms.TextBox), FieldAttributes.Public);

			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Newobj, typeof(System.Windows.Forms.TextBox).GetConstructors().Single());
			il.Emit(OpCodes.Stfld, c);

			#region this.textBox2.Multiline = true;
			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldfld, c);
			il.Emit(OpCodes.Ldc_I4_1);
			il.Emit(OpCodes.Call, typeof(TextBox).GetProperty("Multiline").GetSetMethod());
			#endregion

			#region this.textBox2.AcceptsReturn = true;
			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldfld, c);
			il.Emit(OpCodes.Ldc_I4_1);
			il.Emit(OpCodes.Call, typeof(TextBox).GetProperty("AcceptsReturn").GetSetMethod());
			#endregion


			#region this.button1.Location = new System.Drawing.Point(45, 50);
			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldfld, c);

			il.Emit(OpCodes.Ldc_I4, Left);
			il.Emit(OpCodes.Ldc_I4, Top);
			il.Emit(OpCodes.Newobj, typeof(System.Drawing.Point).GetConstructor(new[] { typeof(int), typeof(int) }));

			il.Emit(OpCodes.Call, typeof(Control).GetProperty("Location").GetSetMethod());
			#endregion

			#region this.button1.Name = "button1";
			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldfld, c);
			il.EmitSetProperty(typeof(Control).GetProperty("Name"), Name);
			#endregion

			#region this.button1.Size = new System.Drawing.Size(75, 23);
			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldfld, c);

			il.Emit(OpCodes.Ldc_I4, Width);
			il.Emit(OpCodes.Ldc_I4, Height);
			il.Emit(OpCodes.Newobj, typeof(System.Drawing.Size).GetConstructor(new[] { typeof(int), typeof(int) }));

			il.Emit(OpCodes.Call, typeof(Control).GetProperty("Size").GetSetMethod());
			#endregion

			#region this.button1.Text = "button1";
			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldfld, c);
			il.EmitSetProperty(typeof(System.Windows.Forms.TextBox).GetProperty("Text"), Text);
			#endregion


			#region this.Controls.Add(this.button1);
			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Call, typeof(Control).GetProperty("Controls").GetGetMethod());

			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldfld, c);

			il.Emit(OpCodes.Call, typeof(Control.ControlCollection).GetMethod("Add"));
			#endregion



			var q = t.DefineProperty(Name, PropertyAttributes.None, typeof(string), null);
			var q_get = t.DefineMethod("get_" + Name, MethodAttributes.Public, CallingConventions.HasThis, typeof(string), new Type[0]);

			{
				var q_il = q_get.GetILGenerator();

				q_il.Emit(OpCodes.Ldarg_0);
				q_il.Emit(OpCodes.Ldfld, c);
				q_il.Emit(OpCodes.Call, typeof(System.Windows.Forms.TextBox).GetProperty("Text").GetGetMethod());

				q_il.Emit(OpCodes.Ret);
			}

			q.SetGetMethod(q_get);

			var q_set = t.DefineMethod("set_" + Name, MethodAttributes.Public, CallingConventions.HasThis, typeof(void), new[] { typeof(string) });

			{
				var q_il = q_set.GetILGenerator();

				q_il.Emit(OpCodes.Ldarg_0);
				q_il.Emit(OpCodes.Ldfld, c);

				q_il.Emit(OpCodes.Ldarg_1);

				q_il.Emit(OpCodes.Call, typeof(System.Windows.Forms.TextBox).GetProperty("Text").GetSetMethod());

				q_il.Emit(OpCodes.Ret);
			}

			q.SetSetMethod(q_set);
		}

	}
}
