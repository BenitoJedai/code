using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using System.Collections.Generic;
using System;
using System.IO;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.ui;
using URLRequestHeaderExample.Shared;
using ScriptCoreLib.ActionScript.flash.net;

namespace URLRequestHeaderExample.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint]
	[SWF]
	public class URLRequestHeaderExample : Sprite
	{



		/// <summary>
		/// Default constructor
		/// </summary>
		public URLRequestHeaderExample()
		{
			// http://livedocs.adobe.com/flex/3/langref/flash/net/URLRequestHeader.html#includeExamplesSummary
			// http://www.judahfrangipane.com/blog/?p=87

			var t = new TextField
			{
				multiline = true,
				text = "powered by jsc",
				background = true,
				width = 400,
				x = 8,
				y = 8,
				alwaysShowSelection = true,
			}.AttachTo(this);

			var loader = new URLLoader();

			var header = new URLRequestHeader("XMyHeader", "got milk?");

			t.appendText("\n" + this.loaderInfo.url);

			var request = new URLRequest("http://localhost:58229/WebForm1.aspx");
			var data = new DynamicContainer { Subject = new URLVariables("name=John+Doe") };
			data["age"] = 23;

			request.data = data.Subject;
			request.method = URLRequestMethod.POST;
			request.requestHeaders.push(header);

			loader.complete +=
				args =>
				{
					t.appendText("\n" + loader.data);
				};

			loader.load(request);



			KnownEmbeddedResources.Default[KnownAssets.Path.Assets + "/Preview.png"].ToBitmapAsset().AttachTo(this).MoveTo(100, 200);
		}

		static URLRequestHeaderExample()
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