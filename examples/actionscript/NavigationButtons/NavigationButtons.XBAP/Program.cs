﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Navigation;
using System.Windows;
using NavigationButtons.Shared;

namespace NavigationButtons.XBAP
{
	public class Program
	{


		[STAThread]
		public static void Main()
		{
			// http://blogs.microsoft.co.il/blogs/maxim/archive/2008/03/05/wpf-xbap-as-full-trust-application.aspx


			var a = new Application();

			a.Navigated +=
				(s, e) =>
				{
					// http://www.munna.shatkotha.com/blog/post/2008/03/02/Hide-Navigation-UI-(back-forward-button)-of-Xbap-application.aspx

					var ws = (e.Navigator as NavigationWindow);

					if (ws == null)
						return;
					ws.ShowsNavigationUI = false;
				};


			a.Startup +=
				delegate
				{
					a.MainWindow.Content = new MyCanvas();

				};
		}
	}
}
