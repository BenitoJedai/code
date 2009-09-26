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


		private static void DefineLabel(TypeBuilder t, ILGenerator il, string Name, int Left, int Top, int Width, int Height, string Text)
		{
			var c = t.DefineField(Name, typeof(System.Windows.Forms.Label), FieldAttributes.Public);

			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Newobj, typeof(System.Windows.Forms.Label).GetConstructors().Single());
			il.Emit(OpCodes.Stfld, c);

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
			il.EmitSetProperty(typeof(System.Windows.Forms.Label).GetProperty("Text"), Text);
			#endregion


			#region this.Controls.Add(this.button1);
			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Call, typeof(Control).GetProperty("Controls").GetGetMethod());

			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldfld, c);

			il.Emit(OpCodes.Call, typeof(Control.ControlCollection).GetMethod("Add"));
			#endregion
		}

	}
}
