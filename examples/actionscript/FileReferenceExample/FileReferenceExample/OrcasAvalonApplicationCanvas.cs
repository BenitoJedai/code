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
				FontSize = 32,
				Text = "powered by jsc",
				BorderThickness = new Thickness(0),
				Foreground = 0xffffffff.ToSolidColorBrush(),
				Background = Brushes.Transparent,
				IsReadOnly = true,
				Width = 300,
				Height = 60
			}.MoveTo(32, 32).AttachTo(this);

			help_idle.Opacity = 0;
			help.Opacity = 1;
			img.Opacity = 0.5;

			t.MouseEnter +=
				delegate
				{
					help_idle.Opacity = 1;
					help.Opacity = 0;

					img.Opacity = 1;
					t.Foreground = 0xffffff00.ToSolidColorBrush();
				};

			t.MouseLeave +=
				delegate
				{
					help_idle.Opacity = 0;
					help.Opacity = 1;

					img.Opacity = 0.5;
					t.Foreground = 0xffffffff.ToSolidColorBrush();
				};

			//3000.AtDelay(
			//    delegate
			//    {
			//        // load new text from embedded resource

			//        (KnownAssets.Path.Assets + "/about.txt").ToStringAsset(
			//            e =>
			//            {
			//                t.FontSize = 16;
			//                t.Text = e;
			//            }
			//        );
			//    }
			//);

			var cc = 0;

			this.MouseLeftButtonUp +=
				delegate
				{
					cc++;

					if (cc % 2 == 0)
					{
						t.Text = "saving...";

						var r = new FileDialog();

						var z = new ZIPFile
						{
							{"default.txt", "hello world"}
						};

						r.Save((MemoryStream)z, "demo2.zip");
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
								}

								t.Text = w.ToString();
							}
						);
					}

				};

		}
	}
}
