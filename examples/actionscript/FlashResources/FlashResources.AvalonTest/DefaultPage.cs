using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace FlashResources.AvalonTest
{
	public class DefaultPage
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
			ToWindow(new FlashResources.ActionScript.MyCanvas()).ShowDialog();
		}
	}
}
