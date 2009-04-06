﻿using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using System.Collections.Generic;
using System;
using System.IO;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.ui;
using FlashAlchemyEcho.Shared;

namespace FlashAlchemyEcho.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint]
	[SWF]
	public class FlashAlchemyEcho : Sprite
	{



		/// <summary>
		/// Default constructor
		/// </summary>
		public FlashAlchemyEcho()
		{

			for (var j = 0.0; j < 1; j += 0.1)
			{
				this.graphics.beginFill(0xff0000, j);
				this.graphics.drawCircle(40, 40, 40 * (1.0 - j));
				this.graphics.endFill();
			}

			var step = 100;
			for (int i = 0; i < 4; i++)
			{
				addChild(
				   new TextField
				   {
					   text = "hello world",
					   x = step * i,
					   y = 20,
					   textColor = 0x00ff00,
					   sharpness = 400
				   });
			}

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

		static FlashAlchemyEcho()
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