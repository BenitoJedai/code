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
		
		private static void DefineButton(TypeBuilder t, ILGenerator il, string Name, int Left, int Top, int Width, int Height, string Text)
		{
			var raise = default(MethodInfo);
			// the raise method may have to made public for java
			// to add event handlers...
			var clicked = t.DefineWorkingEvent(Name, typeof(Action), r => raise = r);


			// family modifier is better
			// because then we enable subclassing scenario
			var button1 = t.DefineField("Button" + Name, typeof(Button), FieldAttributes.Public);

			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Newobj, typeof(Button).GetConstructors().Single());
			il.Emit(OpCodes.Stfld, button1);

			#region this.button1.Location = new System.Drawing.Point(45, 50);
			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldfld, button1);

			il.Emit(OpCodes.Ldc_I4, Left);
			il.Emit(OpCodes.Ldc_I4, Top);
			il.Emit(OpCodes.Newobj, typeof(System.Drawing.Point).GetConstructor(new[] { typeof(int), typeof(int) }));

			il.Emit(OpCodes.Call, typeof(Control).GetProperty("Location").GetSetMethod());
			#endregion

			#region this.button1.Name = "button1";
			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldfld, button1);
			il.EmitSetProperty(typeof(Control).GetProperty("Name"), Name);
			#endregion

			#region this.button1.Size = new System.Drawing.Size(75, 23);
			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldfld, button1);

			il.Emit(OpCodes.Ldc_I4, Width);
			il.Emit(OpCodes.Ldc_I4, Height);
			il.Emit(OpCodes.Newobj, typeof(System.Drawing.Size).GetConstructor(new[] { typeof(int), typeof(int) }));

			il.Emit(OpCodes.Call, typeof(Control).GetProperty("Size").GetSetMethod());
			#endregion

			#region this.button1.Text = "button1";
			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldfld, button1);
			il.EmitSetProperty(typeof(Control).GetProperty("Text"), Text);
			#endregion

			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldfld, button1);

			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldftn, raise);
			il.Emit(OpCodes.Newobj, typeof(EventHandler).GetConstructors().Single());
			il.Emit(OpCodes.Call, typeof(Button).GetEvent("Click").GetAddMethod());


			#region this.Controls.Add(this.button1);
			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Call, typeof(Control).GetProperty("Controls").GetGetMethod());

			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldfld, button1);

			il.Emit(OpCodes.Call, typeof(Control.ControlCollection).GetMethod("Add"));
			#endregion
		}

	}
}
