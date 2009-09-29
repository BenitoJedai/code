﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;
using System.Windows.Forms;
using System.IO;

namespace jsc.meta.Library
{
	public static partial class TextualUserControlProvider
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
			var m = t.DefineMethod("InitializeComponent" + t.Name, MethodAttributes.Private);

			var il = m.GetILGenerator();



			// lets convert textual representation to code

			il.EmitSuspendLayout(
				delegate
				{
					var LayoutFile = Sources.SingleOrDefault(k => k.Name == "Layout.txt");
					if (LayoutFile != null)
					{
						var Layout = File.ReadAllText(LayoutFile.FullName);
						var LayoutCharWidth = 12;
						var LayoutCharHeight = 24;

						#region TextBox
						foreach (var TextBox in
							from k in Layout.ToCharArray().Select((c, i) => new { c, i })
							where char.IsLetter(k.c)
							where k.i > 0
							where Layout[k.i - 1] == '|'
							let Position = Layout.ToCharArray().Take(k.i).Reverse()

							let ButtonLeft = Position.TakeWhile(c => "\r\n".IndexOf(c) < 0).Count()
							let ButtonTop = Position.Count(c => c == '\n')

							let Width = Layout.ToCharArray().Skip(k.i).TakeWhile(c => c != '|').Count() + 1
							let Height = Enumerable.Range(1, 8).TakeWhile(
								offset =>
								{
									var CurrentLine = Layout.ToCharArray().Select((c, i) => new { c, i }).Skip(k.i + Width);
									for (int i = 0; i < offset; i++)
									{
										CurrentLine = CurrentLine.SkipWhile(c => c.c != '\n').Skip(1);
									}

									var Start = CurrentLine.Skip(ButtonLeft - 1).FirstOrDefault();
									if (Start == null)
										return false;

									var End = CurrentLine.Skip(ButtonLeft + Width - 1).FirstOrDefault();
									if (End == null)
										return false;

									return Start.c == '|' && End.c == '|';
								}
							).Count() + 1
							let Name = new string(Layout.ToCharArray().Skip(k.i).TakeWhile(c => char.IsLetter(c) || char.IsDigit(c)).ToArray())
							let Source = Sources.SingleOrDefault(kk => kk.Name == Name + ".txt")
							let Text = Source == null ? Name : File.ReadAllText(Source.FullName)


							select new { Name, ButtonLeft, ButtonTop, Text, Width, Height }
							)
						{
							DefineTextbox(t, il,
								TextBox.Name.ToCamelCase(),
								(TextBox.ButtonLeft - 1) * LayoutCharWidth,
								TextBox.ButtonTop * LayoutCharHeight,
								TextBox.Width * LayoutCharWidth,
								TextBox.Height * LayoutCharHeight,
								TextBox.Text
							);
						}
						#endregion

						#region Label
						foreach (var Label in
							from k in Layout.ToCharArray().Select((c, i) => new { c, i })
							where char.IsLetter(k.c)
							where k.i == 0 || char.IsWhiteSpace(Layout[k.i - 1])
							let Name = new string(Layout.ToCharArray().Skip(k.i).TakeWhile(c => char.IsLetter(c) || char.IsDigit(c)).ToArray())
							let Position = Layout.ToCharArray().Take(k.i).Reverse()
							let Padding = Layout.ToCharArray().Skip(k.i).SkipWhile(c => char.IsLetter(c) || char.IsDigit(c)).TakeWhile(c => c == '-').Count()
							// in this line if [ comes before ] then we should skip this label candidate

							let Newline = Position.TakeWhile(c => c != '\n')

							let ButtonStart = Newline.TakeWhile(c => c != '[').Count()
							let ButtonEnd = Newline.TakeWhile(c => c != ']').Count()

							where ButtonStart >= ButtonEnd

							let ButtonLeft = Position.TakeWhile(c => "\r\n".IndexOf(c) < 0).Count()
							let ButtonTop = Position.Count(c => c == '\n')
							let Source = Sources.SingleOrDefault(kk => kk.Name == Name + ".txt")
							let Text = Source == null ? Name : File.ReadAllText(Source.FullName)
							select new { Name, ButtonLeft, ButtonTop, Text, Padding }
							)
						{

							DefineLabel(t, il,
								Label.Name.ToCamelCase(),
								Label.ButtonLeft * LayoutCharWidth,
								Label.ButtonTop * LayoutCharHeight + 4,
								(Label.Text.Length + Label.Padding) * LayoutCharWidth,
								LayoutCharHeight - 4,
								Label.Text
							);

						}
						#endregion

						#region Buttons
						foreach (var Button in
							from k in Layout.ToCharArray().Select((c, i) => new { c, i })
							where k.c == '['
							let Name = new string(Layout.ToCharArray().Skip(k.i + 1).TakeWhile(c => c != ']').ToArray())
							let Position = Layout.ToCharArray().Take(k.i).Reverse()
							let Left = Position.TakeWhile(c => "\r\n".IndexOf(c) < 0).Count()
							let Top = Position.Count(c => c == '\n')
							select new { Name, Left, Top }
						)
						{

							DefineButton(t, il,
								Button.Name.Trim().ToCamelCase(), Button.Left * LayoutCharWidth,
								Button.Top * LayoutCharHeight,
								(Button.Name.Length + 2) * LayoutCharWidth,
								LayoutCharHeight,
								Button.Name.Trim()
							);

						}
						#endregion




						{
							var Width = (Layout.ToCharArray().Select((c, i) => new { c, i }).Aggregate(

								new { Current = 0, Max = 0 },

								(s, k) =>
								{
									var Current = s.Current;
									var Max = s.Max;

									if (k.c == '\n')
									{
										if (Max < Current)
											Max = Current;

										Current = 0;
									}
									else
									{
										Current++;
									}

									return new { Current, Max };
								}

							).Max + 1) * LayoutCharWidth;



							var Height = (Layout.ToCharArray().Select((c, i) => new { c, i }).Count(k => k.c == '\n') + 1) * LayoutCharHeight;

							#region this.button1.Size = new System.Drawing.Size(75, 23);
							il.Emit(OpCodes.Ldarg_0);

							il.Emit(OpCodes.Ldc_I4, Width);
							il.Emit(OpCodes.Ldc_I4, Height);
							il.Emit(OpCodes.Newobj, typeof(System.Drawing.Size).GetConstructor(new[] { typeof(int), typeof(int) }));

							il.Emit(OpCodes.Call, typeof(Control).GetProperty("Size").GetSetMethod());
							#endregion
						}

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





	}
}
