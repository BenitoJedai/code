using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TestFormsAvalon
{
	class Content1Canvas : Canvas
	{
		public Content1Canvas()
		{
			this.Width = 200;
			this.Height = 100;
			this.Background = Brushes.Red;

			var r = new Rectangle
			{
				Fill = Brushes.Yellow,
				Width = 200,
				Height = 8
			};

			this.Children.Add(r);

			var t = new TextBox
			{
				Width = 60,
				Height = 24,
				Text = "Hi!"
			};

			Canvas.SetTop(t, 10);
			Canvas.SetLeft(t, 10);

			this.Children.Add(t);
		}
	}

	class MyContent : UserControl
	{
		public MyContent()
		{
			this.Content = new Content1Canvas();
		}

		
	}
}
