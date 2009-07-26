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
				FontSize = 32,
				Text = "powered by jsc",
				BorderThickness = new Thickness(0),
				Foreground = 0xffffffff.ToSolidColorBrush(),
				Background = Brushes.Transparent,
				IsReadOnly = true,
				Width = 300,
				Height = 60
			}.MoveTo(32, 32).AttachTo(this);

//script : error JSC1000: ActionScript : unable to emit newarr at 'FileReferenceExample.Shared.OrcasAvalonApplicationCanvas..ctor'#01bf: Byte
//   at jsc.Languages.ActionScript.ActionScriptCompiler.<CreateInstructionHandlers>b__21(CodeEmitArgs e) in C:\work\jsc.svn\compiler\jsc\Languages\ActionScript\ActionScriptCompiler.OpCodes.cs:line 780
//   at jsc.Script.CompilerBase.EmitInstruction(Prestatement p, ILInstruction i, Type TypeExpectedOrDefault) in C:\work\jsc.svn\compiler\jsc\Languages\CompilerBase.cs:line 1339

			var crc = new Crc32Helper();
			crc.ComputeCrc32(new byte[] { 1, 2, 0xfe, 0xff });

			// Crc32Value = 1027690409

			t.Text = "" + crc.Crc32Value + " vs 1027690409";

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

					//if (cc % 2 == 0)
					//{
					t.Text = "saving...";

					var r = new FileDialog();

					var z = new ZIPFile
						{
							{"default.txt", "hello world"}
						};

					var m = z.ToBytes();

					/*
 50 4b 03 04 0a 00 00 00 00 00 80 5c fa 3a 16 ff
 ff ff 0b 00 00 00 0b 00 00 00 0b 00 00 00 64 65
 66 61 75 6c 74 2e 74 78 74 50 4b 01 02 14 00 0a
 00 00 00 00 00 80 5c fa 3a 95 2e 51 ff 29 00 00
 00 29 00 00 00 0b 00 00 00 00 00 00 00 00 00 00
 00 00 00 00 00 00 00 64 65 66 61 75 6c 74 2e 74
 78 74 50 4b 05 06 00 00 00 00 01 00 01 00 39 00
 00 00 29 00 00 00 00 00					 
					 */
					r.Save(new MemoryStream(m), "archive1.zip");

					var w = new StringBuilder();
					t.FontSize = 10;

					var xxi = 0;
					foreach (var xx in m)
					{
						xxi++;
						w.Append(" " + xx.ToString("x2"));
						if ((xxi % 16) == 0)
							w.AppendLine();
					}

					t.Text = w.ToString();
					//}
					//else
					//{
					//    t.Text = "loading...";

					//    var r = new FileDialog();

					//    r.Open(
					//        m =>
					//        {
					//            m.Position = 0;

					//            ZIPFile z = m;
					//            var w = new StringBuilder();

					//            foreach (var zf in z.Entries)
					//            {
					//                w.AppendLine(zf.FileName);
					//            }

					//            t.Text = w.ToString();
					//        }
					//    );
					//}

				};

		}
	}
}
