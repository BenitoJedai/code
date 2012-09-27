using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace AvalonDraggableClipRectangle
{
    public class ApplicationCanvas : Canvas
    {
		public const int DefaultWidth = 480;
		public const int DefaultHeight = 320;

        public ApplicationCanvas()
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


            var bg = new Avalon.Images.belchite_bw
			{
				Width = DefaultWidth,
				Height = DefaultHeight
			}.AttachTo(this);

            var fg_50 = new Avalon.Images.belchite
			{
				Width = DefaultWidth,
				Height = DefaultHeight,
				Opacity = 0.5
			}.AttachTo(this);

            var fg = new Avalon.Images.belchite
			{
				Width = DefaultWidth,
				Height = DefaultHeight
			}.AttachTo(this);

			var cursor = new Rectangle
			{
				Fill = Brushes.Red,
				Width = 16,
				Height = 16
			}.AttachTo(this);

			var overlay = new Rectangle
			{
				Fill = Brushes.White,
				Width = DefaultWidth,
				Height = DefaultHeight,
				Opacity = 0
			}
			//.MoveTo(0, 0)
			.AttachTo(this);

			overlay.MouseMove +=
				(sender, e) =>
				{
					var p = e.GetPosition(overlay);

					cursor.MoveTo(p.X - 8, p.Y - 8);

					{
						var c = new RectangleGeometry
						{
							Rect = new Rect
							{
								X = p.X - 64,
								Y = p.Y - 32,
								Width = 128,
								Height = 64
							}
						};

						fg_50.Clip = c;
					}

					{
						var c = new RectangleGeometry
						{
							Rect = new Rect
							{
								X = p.X - 32,
								Y = p.Y - 32,
								Width = 64,
								Height = 64
							}
						};

						fg.Clip = c;
					}

					
				};
		}

    }
}
