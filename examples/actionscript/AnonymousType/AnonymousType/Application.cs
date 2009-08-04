using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.filters;
using AnonymousType.Core;

namespace AnonymousType
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint(WithResources = true)]
	[SWF]
	public class Application : Sprite
	{
		public Application()
		{
			GetPromotion().AttachTo(this);
			PromotionProvider.GetPromotion().AttachTo(this);
		}

		public static TextField GetPromotion()
		{
			var promotion = new { text = "powered by jsc", size = 60 };

			return new TextField
			{
				width = 600,
				height = 400,
				x = 20,
				y = 220,
				defaultTextFormat = new TextFormat
				{
					size = promotion.size,
					color = 0xff0000,
					font = "Verdana"
				},
				text = promotion.text,

				filters = new BitmapFilter[] { new DropShadowFilter() },
			};


		}
	}

}