using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Input;

namespace FormsAvalonAnimation
{
	public class AnimationCanvas : Canvas
	{
		public AnimationCanvas()
		{
			var rotor = new Canvas();

			// in IE we wont have input where we expect it...
		

			{
				var r = new Rectangle
				{
					Width = 24,
					Height = 100,
					Fill = Brushes.Yellow,
					Opacity = 0.2,
					Cursor = Cursors.Hand,

				};

				r.MouseEnter += delegate
				{
					r.Opacity = 0.9;
					r.Fill = Brushes.Red;
				};

				r.MouseLeave += delegate
				{
					r.Opacity = 0.2;
					r.Fill = Brushes.Yellow;
				};

				rotor.Children.Add(r);
				
			}

			{
				var r = new Rectangle
				{
					Width = 100,
					Height = 24,
					Fill = Brushes.GreenYellow,
					Opacity = 0.2,
					Cursor = Cursors.Hand,
				};

				r.MouseEnter += delegate
				{
					r.Opacity = 0.9;
					r.Fill = Brushes.Blue;
				};

				r.MouseLeave += delegate
				{
					r.Opacity = 0.2;
					r.Fill = Brushes.GreenYellow;
				};

				rotor.Children.Add(r);
			}
			this.Children.Add(rotor);
			Canvas.SetLeft(rotor, 400);
			Canvas.SetTop(rotor, 116);

			var angle = -33.0;

			ApplyRotation(rotor, angle);

			var t = new DispatcherTimer();

			t.Tick +=
				delegate
				{
					angle++;

					ApplyRotation(rotor, angle);
				};

			t.Interval = TimeSpan.FromMilliseconds(1000 / 30);

			t.Start();
		}

		private static void ApplyRotation(Canvas rotor, double angle)
		{
			var deg2rad = Math.PI * 2 / 360;
			var rad = angle * deg2rad;
			var costheta = Math.Cos(rad);
			var sintheta = Math.Sin(rad);
			rotor.RenderTransform = new MatrixTransform
			{
				Matrix = new Matrix
				{
					M11 = costheta,
					M12 = -sintheta,
					M21 = sintheta,
					M22 = costheta
				}
			};
		}



	}
}
