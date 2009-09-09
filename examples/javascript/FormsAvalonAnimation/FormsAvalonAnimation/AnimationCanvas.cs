using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Input;
using System.Windows;

namespace FormsAvalonAnimation
{
	public class AnimationCanvas : Canvas
	{
		class XRectangle
		{
			public Rectangle Element;

			public XRectangle()
			{
				Element = new Rectangle
				{
					Fill = Brushes.Red,
					Width = 200,
					Height = 50
				};
			}
			public void ApplyMatrix(double OriginX, double OriginY, double Rotation, double Dx, double Dy)
			{
				var costheta = Math.Cos(Rotation);
				var sintheta = Math.Sin(Rotation);

				var M11 = costheta;
				var M12 = sintheta;
				var M21 = -sintheta;
				var M22 = costheta;

				ApplyMatrix(OriginX, OriginY, M11, M12, M21, M22, Dx, Dy);
			}

			public void ApplyMatrix(double OriginX, double OriginY, double M11, double M12, double M21, double M22, double Dx, double Dy)
			{
				var g = new TransformGroup();
				// http://social.msdn.microsoft.com/Forums/en-US/vswpfdesigner/thread/578a058e-75ac-4550-b4be-18cf9569cf5e

				g.Children.Add(
					new TranslateTransform
					{
						X = OriginX,
						Y = OriginY
					});

				g.Children.Add(
					 new MatrixTransform
					 {

						 Matrix = new Matrix
						 {
							 M11 = M11,
							 M12 = M12,
							 M21 = M21,
							 M22 = M22,
							 OffsetX = Dx - OriginX,
							 OffsetY = Dy - OriginY,
						 }
					 }
				);



				this.Element.RenderTransform = g;
			}
		}

		public AnimationCanvas()
		{
			{
				var r = new XRectangle
				{

				};

				r.Element.Opacity = 0.3;
				Canvas.SetLeft(r.Element, 300);
				Canvas.SetTop(r.Element, 50);
				this.Children.Add(r.Element);


			}

			{
				var r = new XRectangle
				{

				};

				Canvas.SetLeft(r.Element, 300);
				Canvas.SetTop(r.Element, 50);
				this.Children.Add(r.Element);

				r.ApplyMatrix(-100, -25, 22.DegreesToRadians(), 0, 0);
			}




			{
				var r = new XRectangle
				{

				};

				r.Element.Opacity = 0.3;
				Canvas.SetLeft(r.Element, 300);
				Canvas.SetTop(r.Element, 150);
				this.Children.Add(r.Element);


			}

			{
				var r = new XRectangle
				{

				};

				Canvas.SetLeft(r.Element, 300);
				Canvas.SetTop(r.Element, 150);
				this.Children.Add(r.Element);

				r.ApplyMatrix(0, 0, 22.DegreesToRadians(), 0, 0);
			}
		}







	}
}
