using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.filters;

namespace AnonymousType.Core
{
	public static class PromotionProvider
	{
		public static TextField GetPromotion()
		{
			var promotion = new { text = "powered by jsc", size = 60 };

			return new TextField
			{
				width = 600,
				height = 400,
				x = 20,
				y = 20,
				defaultTextFormat = new TextFormat
				{
					size = promotion.size,
					color = 0xff,
					font = "Verdana"
				},
				text = promotion.text,

				filters = new BitmapFilter[] { new DropShadowFilter() },
			};

			
		}
	}
}
