using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.CSharp.Avalon.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using System.Windows;

namespace PromotionWebApplication.AvalonLogo.Test
{
	class Program
	{
		

		[STAThread]
		static void Main(string[] args)
		{
			var c = new PromotionWebApplication.AvalonLogo.AvalonLogoCanvas();

			c.Container.BitmapEffect = new DropShadowBitmapEffect();

			var w = c.ToWindow();

			c.AtClose += w.Close;

			// http://blog.joachim.at/?p=39
			// http://blogs.msdn.com/changov/archive/2009/01/19/webbrowser-control-on-transparent-wpf-window.aspx
			// http://blogs.interknowlogy.com/johnbowen/archive/2007/06/20/20458.aspx
			w.AllowsTransparency = true;
			w.WindowStyle = System.Windows.WindowStyle.None;
			w.Background = Brushes.Transparent;
			w.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
			w.Topmost = true;

			w.ExplicitWithGlass();

			w.ShowDialog();


		}
	}
}
