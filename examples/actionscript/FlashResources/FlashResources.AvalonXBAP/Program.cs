using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using FlashResources.ActionScript;
using System.Windows.Navigation;

namespace FlashResources.AvalonXBAP
{
	public class App : Application
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

		[DebuggerNonUserCode, STAThread]
		public static void Main()
		{
			// http://blogs.microsoft.co.il/blogs/maxim/archive/2008/03/05/wpf-xbap-as-full-trust-application.aspx


			var a = new App();
			
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
