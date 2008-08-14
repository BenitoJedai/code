using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace FlashAvalonExample.ActionScript
{
	[Script]
	public class MyCanvas : Canvas
	{
		public MyCanvas()
		{
			// http://msdn.microsoft.com/en-us/magazine/cc337899.aspx

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

			
		}
	}
}
