using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Avalon;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;

namespace PromotionWebApplication1
{
	public sealed class UltraSprite : Sprite
	{
		public const int DefaultWidth = JSCSolutionsNETCarouselCanvas.DefaultWidth;
		public const int DefaultHeight = JSCSolutionsNETCarouselCanvas.DefaultHeight;

		public UltraSprite()
		{
			var c = new JSCSolutionsNETCarouselCanvas();

			c.Container.AttachToContainer(this);
		}
	}

}
