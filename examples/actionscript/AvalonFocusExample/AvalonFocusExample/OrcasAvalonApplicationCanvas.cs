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

namespace AvalonFocusExample.Shared
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



			var t = new TextBox
			{
				FontSize = 32,
				Text = "powered by jsc",
				BorderThickness = new Thickness(0),
				Foreground = 0xffffffff.ToSolidColorBrush(),
				Background = Brushes.Transparent,
				IsReadOnly = true,
				Width = 300
			}.MoveTo(32, 32).AttachTo(this);


			var a = new FocusDisplayCanvas(64, 100).AttachTo(this);

			var k = new FocusDisplayCanvas(64, 100).AttachTo(this).MoveTo(64, 0);

			// IE and FLASH loose focus on mouse click

			// http://www.filipesilvestrim.com/blog/21/02/2008/flash-player-loose-focus-with-button-removechild/


		}
	}

	[Script]
	public class FocusDisplayCanvas : Canvas
	{
		public FocusDisplayCanvas(int Width, int Height)
		{
			var bg = new Rectangle
			{
				Fill = Brushes.Yellow,
				Width = Width,
				Height = Height
			}.AttachTo(this);

			this.Width = Width;
			this.Height = Height;

			this.Focusable = true;
			this.GotFocus +=
				delegate
				{
					bg.Fill = Brushes.Green;

				};

			this.LostFocus +=
				delegate
				{
					bg.Fill = Brushes.Red;

				};

			this.MouseLeftButtonDown +=
				(sender, args) =>
				{
					this.Focus();
				};
		}
	}
}
