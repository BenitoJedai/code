using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.filters;
using System;

namespace ConditionalBranch
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint(WithResources = true)]
	[SWF]
	public class Application : Sprite
	{
		private static bool InRange(int i)
		{
			var rtn = false;
			bool v = ((i >= 97) && (i <= 122));
			if (v)
			{
				rtn = true;
			}
			return rtn;
		}

		public Application()
		{
			var cloud = new TheCloudEffect(0);

			cloud.AttachTo(this);

			var cloud2 = new TheCloudEffect(1);

			cloud2.AttachTo(this);

			var t = new TextField
			{
				width = 600,
				height = 400,
				x = 20,
				y = 20,
				defaultTextFormat = new TextFormat
				{
					size = 60,
					color = 0xff,
					font = "Verdana"
				},
				text = "powered by jsc",

				filters = new BitmapFilter[] { new DropShadowFilter() },
			}.AttachTo(this);

			var r = new Sprite {  alpha = 0.5}.AttachTo(this);

			r.graphics.beginFill(0);
			r.graphics.drawRect(97, 0, 122 - 97, 500);

			this.mouseMove +=
				e =>
				{
					var x = Convert.ToInt32(e.stageX);

					t.text = "x: " + x;

					if (InRange(x))
					{
						cloud.visible = false;
						cloud2.visible = true;
					}
					else
					{
						cloud2.visible = false;
						cloud.visible = true;
					}
				};

			//KnownEmbeddedResources.Default["assets/ConditionalBranch/Preview.png"].ToBitmapAsset().AttachTo(this).MoveTo(100, 200);
		}


	}

}