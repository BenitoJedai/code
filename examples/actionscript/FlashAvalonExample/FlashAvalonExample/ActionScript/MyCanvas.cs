using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Input;

namespace FlashAvalonExample.ActionScript
{
	[Script]
	public class MyCanvas : Canvas
	{
		public MyCanvas()
		{
			// http://msdn.microsoft.com/en-us/magazine/cc337899.aspx

			var path2 = new Path
			{
				Fill = Brushes.Yellow,
				Data = new RectangleGeometry
				{
					
					Rect = new Rect
					{
						X = 4,
						Y = 4,
						Width = 400,
						Height = 300
					}
				}
			};

		
			this.Children.Add(path2);
			
			var LastX = 0.0;
			var LastY = 0.0;

			Action<double, double> LineTo =
				(x, y) =>
				{
					var l1 = new Line
					{
						Stroke = Brushes.Red,
						X1 = LastX,
						Y1 = LastY,
						X2 = x,
						Y2 = y
					};

					this.Children.Add(l1);

					LastX = x;
					LastY = y;
				};

		


			this.Background = Brushes.GreenYellow;
			
			var text = new TextBox();

			text.Background = Brushes.Transparent;
			text.Text = "some text";

			Canvas.SetLeft(text, 8);
			Canvas.SetTop(text, 12);

			this.Children.Add(text);


			var text2 = new TextBox();

			text2.Text = "some other text";
			
			
			
			Canvas.SetLeft(text2, 8);
			Canvas.SetTop(text2, 36);

			this.Children.Add(text2);

			text.Foreground = Brushes.Red;
			text2.Background = Brushes.GreenYellow;
			
			text.AppendText("dynamic");
			text2.BitmapEffect = new DropShadowBitmapEffect();

			text2.Opacity = 0.8;
			text2.RenderTransform = new ScaleTransform { ScaleX = 2, ScaleY = 2 };

			text.TextChanged +=
				delegate
				{
					text2.Text = "auto: " + text.Text;
				};

			path2.MouseMove +=
				(object sender, MouseEventArgs e) =>
				{
					var p = e.GetPosition(this);

					LineTo(p.X, p.Y);

					Canvas.SetLeft(text2, p.X + 32);
					Canvas.SetTop(text2, p.Y);
				};
		}
	}
}
