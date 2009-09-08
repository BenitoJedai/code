using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace FormsAvalonAnimation
{
	public class AnimationCanvas : Canvas
	{
		public AnimationCanvas()
		{
			var r = new Rectangle
			{
				Width = 200,
				Height = 200,
				Fill = Brushes.Yellow
			};

			r.MouseEnter += delegate
			{
				r.Fill = Brushes.Red;
			};

			r.MouseLeave += delegate
			{
				r.Fill = Brushes.Yellow;
			};

			this.Children.Add(r);

			Canvas.SetLeft(r, 300);
			Canvas.SetTop(r, 16);
		}
	}
}
