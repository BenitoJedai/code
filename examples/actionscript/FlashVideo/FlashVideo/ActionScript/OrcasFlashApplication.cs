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
			this.InvokeWhenStageIsReady(
				delegate
				{
					var flv = new FLVPlayback();
					flv.width = 320;
					flv.height = 240;

					this.addChild(flv);

					// this should work:
					// http://www.flashcomguru.com/apps/flex/flvplayback/flvpb.html
					// http://www.flashcomguru.com/apps/flex/flvplayback/srcview/

					//flv.skin = "assets/FlashVideo/steel.swf";

					// http://theflashblog.com/?p=129
					// http://blogs.adobe.com/accessibility/2006/10/captionskins.html
					// http://flashdevelop.org/community/viewtopic.php?t=1779
					// C:\work\jsc.svn\examples\actionscript\FlashVideo\FlashVideo\bin\Release\web\FlashVideo\ActionScript\FlashVideo.as(21): 
					// col: 30 Error: Type was not found or was not a compile-time constant: FLVPlayback.
					// var flv:fl.video.FLVPlayback;
					// http://www.flashcomguru.com/index.cfm/2007/4/17/Flex-Builder-2-to-write-AS3-code-for-Flash
					// http://www.kirupa.com/forum/showthread.php?p=2450954

					// ...this doesnt seem to be supported yet
					// http://yourpalmark.com/2008/04/30/flvplayback-directly-in-flex/
					// http://blog.flexexamples.com/2008/12/11/using-the-flash-flvplayback-control-in-flex/

					// download here:
					// http://kb2.adobe.com/cps/406/kb406018.html

					//flv.AttachTo(this);
					flv.play("http://v9.lscache3.googlevideo.com/videoplayback?ip=0.0.0.0&sparams=id%2Cexpire%2Cip%2Cipbits%2Citag%2Cburst%2Cfactor&itag=5&ipbits=0&signature=52263C98A206203E7A6ADD4D2100DE1150EF9C09.9840C4F946309D368AB946AB0371D8455743E2E2&sver=3&expire=1247853600&key=yt1&factor=1.25&burst=40&id=6be88f6d5fff3db0");

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
			);
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