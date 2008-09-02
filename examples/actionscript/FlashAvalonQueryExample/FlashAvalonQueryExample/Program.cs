using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FlashAvalonQueryExample.Shared;

namespace FlashAvalonQueryExample
{
	class Program
	{
		public static Window ToWindow(Canvas e)
		{
			return new Window
			{
				Background = Brushes.Black,
				SizeToContent = SizeToContent.WidthAndHeight,
				Content = e
			};
		}

		[STAThread]
		static public void Main(string[] args)
		{
			ToWindow(new MyCanvas()).ShowDialog();
		}
	}
}
