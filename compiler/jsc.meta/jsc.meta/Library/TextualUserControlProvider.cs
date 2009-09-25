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
	public static class TextualUserControlProvider
	{
		public static void EmitSuspendLayout(this ILGenerator il, Action h)
		{
			var SuspendLayout = typeof(System.Windows.Forms.Control).GetMethod("SuspendLayout", new Type[0]);
			var ResumeLayout = typeof(System.Windows.Forms.Control).GetMethod("ResumeLayout", new[] { typeof(bool) });

			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Call, SuspendLayout);

			h();

			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldc_I4_0);
			il.Emit(OpCodes.Call, ResumeLayout);
		}

		public static MethodBuilder DefineInitializeComponentMethod(this TypeBuilder t, IEnumerable<FileInfo> Sources)
		{
			var m = t.DefineMethod("InitializeComponent", MethodAttributes.Private);

			var il = m.GetILGenerator();



			// lets convert textual representation to code

			il.EmitSuspendLayout(
				delegate
				{
					var LayoutFile = Sources.SingleOrDefault(k => k.Name == "Layout.txt");
					if (LayoutFile != null)
					{
						var Layout = File.ReadAllText(LayoutFile.FullName);
						var LayoutCharWidth = 10;
						var LayoutCharHeight = 24;

						foreach (var Label in
							from k in Layout.ToCharArray().Select((c, i) => new { c, i })
							where char.IsLetter(k.c)
							where k.i == 0 || char.IsWhiteSpace(Layout[k.i - 1])
							let Name = new string(Layout.ToCharArray().Skip(k.i).TakeWhile(c => char.IsLetter(c) || char.IsDigit(c)).ToArray())
							let Position = Layout.ToCharArray().Take(k.i ).Reverse()

							// in this line if [ comes before ] then we should skip this label candidate

							let Newline = Position.TakeWhile(c => c != '\n')

							let ButtonStart = Newline.TakeWhile(c => c != '[').Count()
							let ButtonEnd = Newline.TakeWhile(c => c != ']').Count()

							where ButtonStart >= ButtonEnd

							let ButtonLeft = Position.TakeWhile(c => "\r\n".IndexOf(c) < 0).Count()
							let ButtonTop = Position.Count(c => c == '\n')
							let Source = Sources.SingleOrDefault(kk => kk.Name == Name + ".txt")
							let Text = Source == null ? Name : File.ReadAllText(Source.FullName)
							select new { Name, ButtonLeft, ButtonTop, Text }
							)
						{

							DefineLabel(t, il,
								Label.Name, 
								Label.ButtonLeft * LayoutCharWidth,
								Label.ButtonTop * LayoutCharHeight + 4,
								Label.Text.Length * LayoutCharWidth,
								LayoutCharHeight,
								Label.Text
							);

						}

						#region Buttons
						foreach (var Button in
							from k in Layout.ToCharArray().Select((c, i) => new { c, i })
							where k.c == '['
							let ButtonName = new string(Layout.ToCharArray().Skip(k.i + 1).TakeWhile(c => c != ']').ToArray())
							let Position = Layout.ToCharArray().Take(k.i ).Reverse()
							let ButtonLeft = Position.TakeWhile(c => "\r\n".IndexOf(c) < 0).Count()
							let ButtonTop = Position.Count(c => c == '\n')
							select new { Name = ButtonName, ButtonLeft, ButtonTop }
						)
						{

							DefineButton(t, il, 
								Button.Name.Trim(), Button.ButtonLeft * LayoutCharWidth,
								Button.ButtonTop * LayoutCharHeight, 
								(Button.Name.Length + 2) * LayoutCharWidth, 
								LayoutCharHeight
							);

						}
						#endregion


					}

					#region this.Name = "UserControl1";
					il.Emit(OpCodes.Ldarg_0);
					il.EmitSetProperty(typeof(Control).GetProperty("Name"), t.Name);
					#endregion
				}
			);




			il.Emit(OpCodes.Ret);


			return m;
		}

		private static void DefineLabel(TypeBuilder t, ILGenerator il, string ButtonName, int ButtonLeft, int ButtonTop, int ButtonWidth, int ButtonHeight, string Text)
		{
			var button1 = t.DefineField(ButtonName, typeof(System.Windows.Forms.Label), FieldAttributes.Private);

			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Newobj, typeof(System.Windows.Forms.Label).GetConstructors().Single());
			il.Emit(OpCodes.Stfld, button1);

			#region this.button1.Location = new System.Drawing.Point(45, 50);
			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldfld, button1);

			il.Emit(OpCodes.Ldc_I4, ButtonLeft);
			il.Emit(OpCodes.Ldc_I4, ButtonTop);
			il.Emit(OpCodes.Newobj, typeof(System.Drawing.Point).GetConstructor(new[] { typeof(int), typeof(int) }));

			il.Emit(OpCodes.Call, typeof(Control).GetProperty("Location").GetSetMethod());
			#endregion

			#region this.button1.Name = "button1";
			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldfld, button1);
			il.EmitSetProperty(typeof(Control).GetProperty("Name"), ButtonName);
			#endregion

			#region this.button1.Size = new System.Drawing.Size(75, 23);
			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldfld, button1);

			il.Emit(OpCodes.Ldc_I4, ButtonWidth);
			il.Emit(OpCodes.Ldc_I4, ButtonHeight);
			il.Emit(OpCodes.Newobj, typeof(System.Drawing.Size).GetConstructor(new[] { typeof(int), typeof(int) }));

			il.Emit(OpCodes.Call, typeof(Control).GetProperty("Size").GetSetMethod());
			#endregion

			#region this.button1.Text = "button1";
			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldfld, button1);
			il.EmitSetProperty(typeof(System.Windows.Forms.Label).GetProperty("Text"), Text);
			#endregion


			#region this.Controls.Add(this.button1);
			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Call, typeof(Control).GetProperty("Controls").GetGetMethod());

			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldfld, button1);

			il.Emit(OpCodes.Call, typeof(Control.ControlCollection).GetMethod("Add"));
			#endregion
		}

		private static void DefineButton(TypeBuilder t, ILGenerator il, string ButtonName, int ButtonLeft, int ButtonTop, int ButtonWidth, int ButtonHeight)
		{
			var button1 = t.DefineField(ButtonName, typeof(Button), FieldAttributes.Private);

			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Newobj, typeof(Button).GetConstructors().Single());
			il.Emit(OpCodes.Stfld, button1);

			#region this.button1.Location = new System.Drawing.Point(45, 50);
			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldfld, button1);

			il.Emit(OpCodes.Ldc_I4, ButtonLeft);
			il.Emit(OpCodes.Ldc_I4, ButtonTop);
			il.Emit(OpCodes.Newobj, typeof(System.Drawing.Point).GetConstructor(new[] { typeof(int), typeof(int) }));

			il.Emit(OpCodes.Call, typeof(Control).GetProperty("Location").GetSetMethod());
			#endregion

			#region this.button1.Name = "button1";
			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldfld, button1);
			il.EmitSetProperty(typeof(Control).GetProperty("Name"), ButtonName);
			#endregion

			#region this.button1.Size = new System.Drawing.Size(75, 23);
			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldfld, button1);

			il.Emit(OpCodes.Ldc_I4, ButtonWidth);
			il.Emit(OpCodes.Ldc_I4, ButtonHeight);
			il.Emit(OpCodes.Newobj, typeof(System.Drawing.Size).GetConstructor(new[] { typeof(int), typeof(int) }));

			il.Emit(OpCodes.Call, typeof(Control).GetProperty("Size").GetSetMethod());
			#endregion

			#region this.button1.Text = "button1";
			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldfld, button1);
			il.EmitSetProperty(typeof(Control).GetProperty("Text"), ButtonName);
			#endregion


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
