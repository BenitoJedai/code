using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrowserAvalonExample.Code;
using System.Windows.Controls;
using System.Windows;

namespace ConsoleAvalonExample
{
	class Program
	{
		public static Window ToWindow(Canvas e)
		{
			return new Window
			{
				Width = e.Width,
				Height = e.Height,
				Content = e
			};

		}



		[STAThread]
		public static void Main(string[] args)
		{
			ToWindow(new BrowserAvalonExampleCanvas()).ShowDialog();
		}
	}
}
