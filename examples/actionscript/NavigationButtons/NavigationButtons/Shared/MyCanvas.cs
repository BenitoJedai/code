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

namespace NavigationButtons.Shared
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
				(color, i) =>
					new Rectangle
					{
						Fill = new SolidColorBrush(color),
						Width = DefaultWidth,
						Height = 4,
					}.MoveTo(0, i * 4).AttachTo(this)
			).ToArray();

			// http://social.msdn.microsoft.com/forums/en-US/wpf/thread/21504c22-0d79-404e-ba0e-1cee91a02c2a/

			var bg = new Image
			{
				Source = "assets/NavigationButtons/backMenuPic.png".ToSource(),
				Clip = new RectangleGeometry
				{
					Rect = new Rect { X = 0, Y = 0, Width = 58, Height = 28 }
				}
			}.AttachTo(this);

			var fg = new Image
			{
				Source = "assets/NavigationButtons/back-forward-large.png".ToSource(),
				Clip = new RectangleGeometry
				{
					Rect = new Rect { X = 0, Y = 0, Width = 58, Height = 28 }
				}
			}.AttachTo(this);



			var c = new Canvas
			{
				Width = 28,
				Height = 28,

				Clip = new RectangleGeometry
				{
					Rect = new Rect { X = 0, Y = 0, Width = 28, Height = 28 }
				}

			}.MoveTo(100, 100).AttachTo(this);

			var c_Buttons = new Image
			{
				Source = "assets/NavigationButtons/back-forward-large.png".ToSource(),

			}
			.AttachTo(c);

			var c_Overlay = new Rectangle
			{
				Fill = Brushes.Yellow,
				Width = 28,
				Height = 28,
				Opacity = 0
			}.AttachTo(c);

			c_Overlay.MouseEnter +=
				delegate
				{
					c_Buttons.MoveTo(0, -27);
				};

			c_Overlay.MouseLeave +=
				delegate
				{
					c_Buttons.MoveTo(0, 0);
				};
		}
	}
}
