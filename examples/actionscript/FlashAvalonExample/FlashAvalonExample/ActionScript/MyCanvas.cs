using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Windows.Controls;
using System.Windows.Media;

namespace FlashAvalonExample.ActionScript
{
	[Script]
	public class MyCanvas : Canvas
	{
		public MyCanvas()
		{
			var text = new TextBox();

			text.Text = "some text";

			Canvas.SetLeft(text, 8);
			Canvas.SetTop(text, 12);

			this.Children.Add(text);


			var text2 = new TextBox();

			text2.Text = "some other text";

			Canvas.SetLeft(text2, 8);
			Canvas.SetTop(text2, 36);

			this.Children.Add(text2);

			//text.Foreground = Brushes.Red;
			text.TextChanged +=
				delegate
				{
					text2.Text = text.Text;
				};
		}
	}
}
