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

namespace ScriptCoreLib.CSharp.Avalon.Extensions
{
	public static class AvalonExtensions
	{
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
