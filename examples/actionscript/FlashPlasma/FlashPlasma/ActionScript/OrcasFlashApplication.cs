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

namespace FlashPlasma.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint]
	[SWF]
	public class FlashPlasma : Sprite
	{
		// http://www.unitzeroone.com/blog/2009/04/06/more-play-with-alchemy-lookup-table-effects/


		/// <summary>
		/// Default constructor
		/// </summary>
		public FlashPlasma()
		{

			var c = new cmodule.FlashPlasma.CLibInit();

			var x = new DynamicContainer.Delegates { Subject = c.init() };


			var t = new TextField
			{
				multiline = true,
				text = "powered by jsc",
				background = true,
				x = 20,
				y = 40,
				width = 300,
				alwaysShowSelection = true,
			}.AttachTo(this);

			t.text = x.ToConverter<string, string>("echo")("goo");


			//var echo = x["echo"] as Function;

			//var a = new ScriptCoreLib.ActionScript.Array();
			//a.push("yo");
			//t.text = (string)echo.apply(x.Subject, a); 
			//t.text = echo.GetType().ToString();


			//t.text = x.Invoke<string, string>("echo", "foo");

			KnownEmbeddedResources.Default[KnownAssets.Path.Assets + "/Preview.png"].ToBitmapAsset().AttachTo(this).MoveTo(100, 200);
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