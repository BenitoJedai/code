using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using System.Collections.Generic;
using System;
using System.IO;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.ui;
using FlashVideo.Shared;
using ScriptCoreLib.ActionScript.fl.video;

namespace FlashVideo.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint]
	[SWF]
	public class FlashVideo : Sprite
	{



		/// <summary>
		/// Default constructor
		/// </summary>
		public FlashVideo()
		{
			var flv = new FLVPlayback();

			// http://flashdevelop.org/community/viewtopic.php?t=1779
			// C:\work\jsc.svn\examples\actionscript\FlashVideo\FlashVideo\bin\Release\web\FlashVideo\ActionScript\FlashVideo.as(21): 
			// col: 30 Error: Type was not found or was not a compile-time constant: FLVPlayback.
            // var flv:fl.video.FLVPlayback;
			// http://www.flashcomguru.com/index.cfm/2007/4/17/Flex-Builder-2-to-write-AS3-code-for-Flash
			// http://www.kirupa.com/forum/showthread.php?p=2450954

			// ...this doesnt seem to be supported yet

			flv.AttachTo(this);
			flv.play("http://www.youtube.com/get_video.php?video_id=p-SaOAx5lsw&t=vjVQa1PpcFOFrOM2wc4i7L7zOZoUqdluABHB0suVwso=");

			var t = new TextField
				{
					text = "powered by jsc",
					background = true,
					x = 20,
					y = 40,
					alwaysShowSelection = true,
				}.AttachTo(this);


			KnownEmbeddedResources.Default[KnownAssets.Path.Assets + "/Preview.png"].ToBitmapAsset().AttachTo(this).MoveTo(100, 200);
		}

		static FlashVideo()
		{
			// add resources to be found by ImageSource
			KnownEmbeddedAssets.RegisterTo(
				KnownEmbeddedResources.Default.Handlers
			);
		}

	}

	[Script]
	public class KnownEmbeddedAssets
	{
		[EmbedByFileName]
		public static Class ByFileName(string e)
		{
			throw new NotImplementedException();
		}

		public static void RegisterTo(List<Converter<string, Class>> Handlers)
		{
			// assets from current assembly
			Handlers.Add(e => ByFileName(e));

			//AvalonUgh.Assets.ActionScript.KnownEmbeddedAssets.RegisterTo(Handlers);

			//// assets from referenced assemblies
			//Handlers.Add(e => global::ScriptCoreLib.ActionScript.Avalon.Cursors.EmbeddedAssets.Default[e]);
			//Handlers.Add(e => global::ScriptCoreLib.ActionScript.Avalon.TiledImageButton.Assets.Default[e]);

		}
	}

}