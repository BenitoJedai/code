using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.flash.geom;

namespace InteractiveMatrixTransformC
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
			var cloud = new TheCloudEffect();

			cloud.AttachTo(this);


			new TextField
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


			var img = KnownEmbeddedResources.Default["assets/InteractiveMatrixTransformC/sand.png"].ToBitmapAsset().AttachTo(this).MoveTo(100, 200);

			var m = img.transform.matrix;

			m.a = 2;
			m.b = 0.5;

			m.c = 0.5;
			m.d = 2;

			img.transform.matrix = m;

		}


	}

}