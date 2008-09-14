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

namespace System_Windows_Input_MouseEventArgs.Shared
{
	[Script]
	public class MyCanvas : Canvas
	{
		public const int DefaultWidth = 480;
		public const int DefaultHeight = 320;

		public MyCanvas()
		{
			Width = DefaultWidth;
			Height = DefaultHeight;

			Colors.Blue.ToGradient(Colors.Red, DefaultHeight / 4).Select(
				(c, i) =>
					new Rectangle
					{
						Fill = new SolidColorBrush(c),
						Width = DefaultWidth,
						Height = 4,
					}.MoveTo(0, i * 4).AttachTo(this)
			).ToArray();

			var t = new TextBox
			{

			}.AttachTo(this).MoveTo(200, 32);

			var Overlay = new Rectangle
			{
				Fill = Brushes.White,
				Opacity = 0,
				Width = DefaultWidth,
				Height = DefaultHeight
			}.AttachTo(this);

			var Item = new Rectangle
			{
				Fill = Brushes.Yellow,
				Opacity = 0.5,
				Width = 64,
				Height = 64
			}.MoveTo(100, 100).AttachTo(this);

			var cc = new Canvas
			{
				Width = 200,
				Height = 200
			}.MoveTo(200, 2).AttachTo(this);

			var Item2 = new Rectangle
			{
				Fill = Brushes.Yellow,
				Opacity = 0.5,
				Width = 64,
				Height = 64
			}.MoveTo(100, 100).AttachTo(cc);

			this.MouseMove +=
				(sender, e) =>
				{
					var p = e.GetPosition(Overlay);

					t.Text = new { p.X, p.Y }.ToString();
				};
		}
	}
}
