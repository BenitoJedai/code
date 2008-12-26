﻿using System;
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
using ScriptCoreLib.Shared.Avalon.Extensions;

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

		public static Window ToWindow(this ISupportsContainer e)
		{
			return e.Container.ToWindow();
		}

		public static Window ToWindow(this Canvas e)
		{
			// http://blogs.telerik.com/manoldonev/Posts/08-06-16/WPF_Line_Drawing_and_the_Device-Pixel-_In_dependence.aspx?ReturnURL=%2Fmanoldonev%2Fposts.aspx%3FYear%3D2008%26Month%3D6
			
			var w = new Window
			{
				Background = Brushes.Black,
				SizeToContent = SizeToContent.WidthAndHeight,
				Content = e,
				Title = e.GetType().Name,
				SnapsToDevicePixels = true,
			};


			return w;
		}


	}
}
