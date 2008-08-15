using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Media.Effects;

namespace FlashResources.ActionScript
{
	[Script]
	public class MyCanvas : Canvas
	{
		// this class is for wpf renderer and flash emulated wpf renderer

		public MyCanvas()
		{
			var e = new Image { Source = "assets/FlashResources.Assets/tipsi2.png".ToSource() };

			e.BitmapEffect = new DropShadowBitmapEffect();

			Children.Add(e);
		}

		
	}
}
