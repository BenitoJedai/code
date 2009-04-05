using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using System.Collections.Generic;
using System;
using System.IO;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.ui;
using FlashStratusDemo.Shared;
using ScriptCoreLib.ActionScript.flash.net;

namespace FlashStratusDemo.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint]
	[SWF]
	public class FlashStratusDemo : Sprite
	{
		


		/// <summary>
		/// Default constructor
		/// </summary>
		public FlashStratusDemo()
		{

			var t = new TextField
			{
				multiline = true,
				text = "powered by jsc",
				background = true,
				x = 0,
				y = 0,
				width = 400,
				alwaysShowSelection = true,
			}.AttachTo(this);

			var c = new NetConnection();




			c.netStatus +=
				status =>
				{
					// http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/events/NetStatusEvent.html#info

					var info = new DynamicContainer { Subject = status.info };
					var code = info["code"];

					t.appendText("\nNetConnection: " + code);

					if (code == "NetConnection.Connect.Success")
					{
						// yay! we are online
						var s = new NetStream(c, NetStream.DIRECT_CONNECTIONS);

						s.netStatus +=
							ns_status =>
							{

							};

						s.publish("stream1");
					}
				};


			var s = new Sprite().AttachTo(this);
			var img = KnownEmbeddedResources.Default[KnownAssets.Path.Assets + "/Preview.png"].ToBitmapAsset();

			img.AttachTo(s).MoveTo(100, 200);

			// "Developer Key" means any license key, activation code, or similar installation, access or usage control codes, including serial numbers and electronic certificates digitally signed by Adobe, designed to uniquely identify your Developer Program and link it to you the Developer.
			// Attention: You cannot use this key in your applications.
			//c.connect("rtmfp://stratus.adobe.com/3f37a156abb67621000856d1-08d2970f1b43/");
			c.connect("rtmfp://stratus.adobe.com/3f37a156abb67621000856d1-08d2970f1b43");

		}

		static FlashStratusDemo()
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