using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace FlashResources.AvalonTest
{
	class Program
	{
		public static bool? ShowDialog(Canvas e)
		{
			var w = new Window
			{
				Width = e.Width,
				Height = e.Height,
				Content = e
			};

			return w.ShowDialog();
		}

		[STAThread]
		static void Main(string[] args)
		{
			ShowDialog(new FlashResources.ActionScript.MyCanvas());
		}
	}
}
