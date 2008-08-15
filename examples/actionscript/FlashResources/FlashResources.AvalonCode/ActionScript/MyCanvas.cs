using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using System.Windows.Media;

namespace FlashResources.ActionScript
{
	[Script]
	public class MyCanvas : Canvas
	{
		// this class is for wpf renderer and flash emulated wpf renderer

		public MyCanvas()
		{
			var r = new Rectangle();

			r.Fill = System.Windows.Media.Brushes.GreenYellow;
			r.Width = 600;
			r.Height = 400;

			Children.Add(r);


			var e = new Image { Source = "assets/FlashResources.Assets/tipsi2.png".ToSource() };

			e.BitmapEffect = new DropShadowBitmapEffect();

			e.MouseEnter +=
				(s, ev) =>
				{
					e.Opacity = 1;
				};

			e.MouseLeave +=
				(s, ev) =>
				{
					e.Opacity = 0.5;
				};

			Canvas.SetLeft(e, 32);
			Canvas.SetTop(e, 32);
			Children.Add(e);
		}

		
	}
}
