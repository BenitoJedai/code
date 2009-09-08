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
		public AnimationCanvas()
		{

			// browser will rotate by center
			// wpf will rotate by left top


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

				this.Children.Add(r);
				Canvas.SetLeft(r, 400);
				Canvas.SetTop(r, 116);

			}

			AddRotor(0);
			AddRotor(120);
			AddRotor(240);
		}

		private void AddRotor(double angle)
		{
			var r = new Rectangle
			{
				Width = 80,
				Height = 12,
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


			this.Children.Add(r);
			Canvas.SetLeft(r, 400);
			Canvas.SetTop(r, 116);




			ApplyRotation(r, angle);

			var t = new DispatcherTimer();

			t.Tick +=
				delegate
				{
					angle++;

					ApplyRotation(r, angle);
				};

			t.Interval = TimeSpan.FromMilliseconds(1000 / 30);

			t.Start();
		}

		private static void ApplyRotation(UIElement rotor, double angle)
		{
			var deg2rad = Math.PI * 2 / 360;
			var rad = angle * deg2rad;
			var costheta = Math.Cos(rad);
			var sintheta = Math.Sin(rad);

			var g = new TransformGroup();
			// http://social.msdn.microsoft.com/Forums/en-US/vswpfdesigner/thread/578a058e-75ac-4550-b4be-18cf9569cf5e

			g.Children.Add(
				new TranslateTransform
				{
					X = -40,
					Y = -6
				});

			g.Children.Add(
				 new MatrixTransform
				{
				
					Matrix = new Matrix
					{
						M11 = costheta,
						M12 = -sintheta,
						M21 = sintheta,
						M22 = costheta,
						OffsetX = 20,
						OffsetY = 20,
					}
				}
			);

			rotor.RenderTransform = g;

		}



	}
}
