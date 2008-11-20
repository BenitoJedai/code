using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib;
using System.Windows.Media;
using System.Windows.Shapes;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows;
using WebApplication.Shared;

namespace WebApplication.Client.Avalon
{
	[Script]
	public class AvalonCanvas : Canvas
	{
		public const int DefaultWidth = 640;
		public const int DefaultHeight = 400;


		public AvalonCanvas()
		{
			this.Width = DefaultWidth;
			this.Height = DefaultHeight;

			this.ClipToBounds = true;

			Colors.Black.ToGradient(Colors.Red, DefaultHeight / 4).Select(
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
				FontSize = 20,
				Text = "C# to JavaScript and ActionScript".WithBranding(),
				BorderThickness = new Thickness(0),
				Foreground = 0xffffffff.ToSolidColorBrush(),
				Background = Brushes.Transparent,
				IsReadOnly = true,
				Width = DefaultWidth
			}.MoveTo(0, 32).AttachTo(this);
		}
	}
}
