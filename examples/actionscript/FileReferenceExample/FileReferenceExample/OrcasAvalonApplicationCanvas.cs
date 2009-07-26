using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using ScriptCoreLib.CSharp.Avalon.Controls;
using System.IO;
using ScriptCoreLib.Archive.ZIP;

namespace FileReferenceExample.Shared
{
	[Script]
	public class OrcasAvalonApplicationCanvas : Canvas
	{
		public const int DefaultWidth = 480;
		public const int DefaultHeight = 320;

		public OrcasAvalonApplicationCanvas()
		{
			Width = DefaultWidth;
			Height = DefaultHeight;

			this.ClipToBounds = true;

			Colors.Blue.ToGradient(Colors.Red, DefaultHeight / 4).Select(
				(c, i) =>
					new Rectangle
					{
						Fill = new SolidColorBrush(c),
						Width = DefaultWidth,
						Height = 4,
					}.MoveTo(0, i * 4).AttachTo(this)
			).ToArray();


			var help_idle = new Image
			{
				Source = (KnownAssets.Path.Assets + "/help_idle.png").ToSource()
			}.AttachTo(this);

			var help = new Image
			{
				Source = (KnownAssets.Path.Assets + "/help.png").ToSource()
			}.AttachTo(this);

			help.Opacity = 0;

			var img = new Image
			{
				Source = (KnownAssets.Path.Assets + "/jsc.png").ToSource()
			}.MoveTo(DefaultWidth - 128, DefaultHeight - 128).AttachTo(this);

			(1000 / 30).AtIntervalWithCounter(
				c =>
				{
					img.MoveTo(DefaultWidth - 128, DefaultHeight - 128
						+ Math.Sin(c * 0.1) * 4
					);
				}
			);

			var t = new TextBox
			{
				AcceptsReturn = true,
				FontSize = 10,
				Text = "powered by jsc",
				BorderThickness = new Thickness(0),
				Foreground = Brushes.Blue,
				Background = Brushes.White,
				IsReadOnly = true,
				Width = 400,
				Height = 400
			}.MoveTo(32, 32).AttachTo(this);



			var cc = 0;

			this.MouseLeftButtonUp +=
				delegate
				{
					cc++;

					if (cc % 2 == 1)
					{
						t.Text = "saving...";

						var r = new FileDialog();

						var z = new ZIPFile
						{
							{"default.txt", "hello world"},
							{"readme.txt", "this was created in flash via c#"}
						};

						var m = z.ToBytes();

						r.Save(new MemoryStream(m), "archive1.zip");

						var w = new StringBuilder();


						var xxi = 0;
						foreach (var xx in m)
						{
							xxi++;
							w.Append(" " + xx.ToString("x2"));
							if ((xxi % 16) == 0)
								w.AppendLine();
						}

						t.Text = w.ToString();
					}
					else
					{
						t.Text = "loading...";

						var r = new FileDialog();

						r.Open(
							m =>
							{
								m.Position = 0;

								ZIPFile z = m;
								var w = new StringBuilder();

								foreach (var zf in z.Entries)
								{
									w.AppendLine(zf.FileName);

									var xxi = 0;
									foreach (var xx in zf.Bytes)
									{
										xxi++;
										w.Append(" " + xx.ToString("x2"));
										if ((xxi % 16) == 0)
											w.AppendLine();
									}
									w.AppendLine();
									w.AppendLine(zf.Text);
								}

								t.Text = w.ToString();
							}
						);
					}

				};

		}
	}
}
