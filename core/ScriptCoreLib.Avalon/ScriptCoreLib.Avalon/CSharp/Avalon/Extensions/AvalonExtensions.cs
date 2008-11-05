using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ScriptCoreLib.CSharp.Extensions;
using System.Windows.Navigation;

namespace ScriptCoreLib.CSharp.Avalon.Extensions
{
	public static class AvalonExtensions
	{
		public static Application ToApplication<T>()
			where T : new()
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
					ws.Background = Brushes.Black;
				};


			a.Startup +=
				delegate
				{
					a.MainWindow.Content = new T();

				};

			return a;
		}

		public static Window ToWindow(this Canvas e)
		{
			return new Window
			{
				Background = Brushes.Black,
				SizeToContent = SizeToContent.WidthAndHeight,
				Content = e,
				Title = e.GetType().Name,
				SnapsToDevicePixels = true
			};
		}


	}
}
