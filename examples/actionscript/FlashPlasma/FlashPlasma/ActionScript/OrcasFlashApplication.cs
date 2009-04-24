using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using System.Collections.Generic;
using System;
using System.IO;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.ui;
using FlashPlasma.Shared;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.filters;
using FlashPlasma.SharedAlchemy;

namespace FlashPlasma.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint(Width = DefaultWidth, Height = DefaultHeight)]
	[SWF(width = DefaultWidth, height = DefaultHeight, backgroundColor = 0)]
	[ScriptImportsType("flash.utils.getTimer")]
	public class FlashPlasma : Sprite
	{
		[Script(OptimizedCode = "return flash.utils.getTimer();")]
		public static int getTimer()
		{
			return 0;
		}

		// http://www.unitzeroone.com/blog/2009/04/06/more-play-with-alchemy-lookup-table-effects/

		public const int DefaultWidth = 600;
		public const int DefaultHeight = 600;


		/**
		 * The bitmapdata which will be used to draw
		 * the plasma on 
		 */
		protected BitmapData Buffer;
	
		protected int RenderMode;

	
		/// <summary>
		/// Default constructor
		/// </summary>
		public FlashPlasma()
		{

			this.InvokeWhenStageIsReady(
				delegate
				{
					Buffer = new BitmapData(DefaultWidth, DefaultHeight, false);
					var bitmap = new Bitmap(Buffer);
					addChild(bitmap);

					generatePlasma();

					RenderMode = 0;

					stage.click +=
						delegate
						{
							RenderMode++;
						};

					stage.enterFrame +=
						delegate
						{
							enterFrame2();
						};

					KnownEmbeddedResources.Default[KnownAssets.Path.Assets + "/jsc.png"].ToBitmapAsset().AttachTo(this).MoveTo(DefaultWidth - 128, DefaultHeight - 128);



				}
			);
		}

		private void enterFrame2()
		{
			var shift = getTimer() / 10;

			Buffer.@lock();

			if (RenderMode % 3 == 0)
			{
				PlasmaProxy.Memory.position = PlasmaProxy.shiftPlasma(shift);

				// -- Alchemy palette shifting
				//alchemyMemory.position = plasmaLib.ToFunc<int, uint>("shiftPlasma")(shift);
				// -- write the ByteArray straight to the bitmap data 
				Buffer.setPixels(Buffer.rect, PlasmaProxy.Memory);
			}
			else
			{
				Plasma.shiftPlasma(shift);

				if (RenderMode % 3 == 1)
				{
					var m = new ByteArray();
					foreach (var i in Plasma.newPlasma)
					{
						m.writeUnsignedInt(i);
					}
					m.position = 0;
					Buffer.setPixels(Buffer.rect, m);

				}
				else
				{
					// -- AS3.0 palette shifting
					for (var x = 0; x < DefaultWidth; x++)
						for (var y = 0; y < DefaultHeight; y++)
						{
							//bmd.setPixel(x, y, 0xff00ff00);
							//uint c = palette[(plasma[x][y] + shift) % 256];


							Buffer.setPixel(x, y, Plasma.newPlasma[x + y * DefaultWidth] | 0xff000000u);
						}
				}
			}

			Buffer.unlock();
		}

		private void generatePlasma()
		{
			PlasmaProxy.Memory.position = PlasmaProxy.generatePlasma(DefaultWidth, DefaultHeight);
			Plasma.generatePlasma(DefaultWidth, DefaultHeight);
		}

		static FlashPlasma()
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