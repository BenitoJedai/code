using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Mahjong.Code;

namespace Mahjong.ConsoleTest
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
		static void Main(string[] args)
		{
			ToWindow(new MyCanvas()).ShowDialog();

		}
	}
}
