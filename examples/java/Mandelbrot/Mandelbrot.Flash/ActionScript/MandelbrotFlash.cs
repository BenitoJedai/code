using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using System.Collections.Generic;
using System;
using System.IO;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.ui;
using Mandelbrot.Flash.Shared;
using Mandelbrot.Flash.Alchemy;

namespace Mandelbrot.Flash.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint]
	[SWF]
	public class MandelbrotFlash : Sprite
	{


		/// <summary>
		/// Default constructor
		/// </summary>
		public MandelbrotFlash()
		{
			this.InvokeWhenStageIsReady(
				delegate
				{
					var Buffer = new BitmapData(MandelbrotProvider.DefaultWidth, MandelbrotProvider.DefaultHeight, false);
					var bitmap = new Bitmap(Buffer);
					addChild(bitmap);

					var bits = MandelbrotProxy.DrawMandelbrotSet(0);

					var RenderMode = 0;

					stage.click +=
						delegate
						{
							RenderMode++;
						};

					stage.enterFrame +=
						delegate
						{
						};

					Buffer.@lock();
					MandelbrotProxy.Memory.position = bits;
					Buffer.setPixels(Buffer.rect, MandelbrotProxy.Memory);
					Buffer.unlock();

					KnownEmbeddedResources.Default[KnownAssets.Path.Assets + "/jsc.png"].ToBitmapAsset().AttachTo(this).MoveTo(
						MandelbrotProvider.DefaultWidth - 128, 
						MandelbrotProvider.DefaultHeight - 128);



				}
			);
		}

		static MandelbrotFlash()
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