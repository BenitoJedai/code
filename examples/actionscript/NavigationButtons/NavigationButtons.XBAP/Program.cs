using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows;
using NavigationButtons.Code;

namespace NavigationButtons.XBAP
{
	class Program
	{
		public static void Main(string[] args)
		{

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
