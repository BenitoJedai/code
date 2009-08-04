using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.filters;

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


			//KnownEmbeddedResources.Default["assets/AnonymousType/Preview.png"].ToBitmapAsset().AttachTo(this).MoveTo(100, 200);
		}


	}

}