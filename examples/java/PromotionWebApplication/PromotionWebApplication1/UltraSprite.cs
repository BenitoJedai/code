using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PromotionWebApplication.AvalonLogo;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;

namespace PromotionWebApplication1
{
	public sealed class UltraSprite : Sprite
	{
		public const int DefaultWidth = AvalonLogoCanvas.DefaultWidth;
		public const int DefaultHeight = AvalonLogoCanvas.DefaultHeight;

		public UltraSprite()
		{
			var c = new AvalonLogoCanvas();

			

			c.Container.AttachToContainer(this);
		}
	}

}
