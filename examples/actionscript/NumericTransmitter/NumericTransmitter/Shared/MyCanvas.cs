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
using ScriptCoreLib.Shared.Avalon.Cursors;
using System.Windows.Input;
using Mahjong.NetworkCode.ClientSide.Shared;

namespace NumericTransmitter.Shared
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

			var c1 = new ArrowCursorControl
			{

			};

			c1.Red.Opacity = 0.8;
			c1.Container.AttachTo(this);


			var c2 = new ArrowCursorControl
			{

			};

			c2.Yellow.Opacity = 0.8;
			c2.Container.AttachTo(this);


			var c3 = new ArrowCursorControl
			{

			};

			c3.Green.Opacity = 0.8;
			c3.Container.AttachTo(this);

			var Overlay = new Rectangle
			{
				Fill = Brushes.White,
				Width = DefaultWidth,
				Height = DefaultHeight,
				Opacity = 0,
				Cursor = Cursors.None
			}.AttachTo(this);

			Action<int, int> Emitter = NumericEmitter.Of(
				(x, y) => c3.Container.MoveTo(x, y)
			);


			Action<int, int> Omitter = NumericOmitter.Of(
				(x, y) =>
				{
					c2.Container.MoveTo(x, y);
					Emitter(x, y);
				}
			);

			


			Overlay.MouseMove +=
				(sender, e) =>
				{
					var p = e.GetPosition(this);

					c1.Container.MoveTo(p.X, p.Y);

					//1000.AtDelay(
					//    delegate
					//    {
					//        c2.Container.MoveTo(p.X, p.Y);
					//    }
					//);

					300.AtDelay(
						() => Omitter(Convert.ToInt32(p.X), Convert.ToInt32(p.Y))
					);
				};
		}
	}
}
