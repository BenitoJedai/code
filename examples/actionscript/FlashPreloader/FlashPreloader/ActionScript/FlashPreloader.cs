using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using System.Collections.Generic;
using System;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.events;

namespace FlashPreloader.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint, Frame(typeof(MyFactory))]
	public class FlashPreloader : Sprite
	{
		public const string Type_FullName = "FlashPreloader.ActionScript.FlashPreloader";

		// http://www.onflex.org/flexapps/components/CustomPreloader/srcview/index.html
		// http://www.onflex.org/ted/2006/07/flex-2-custom-preloaders.php
		// http://www.onflex.org/flexapps/components/CustomPreloader/PNG/srcview/
		// http://www.flexer.info/2008/02/07/very-first-flex-preloader-customization/
		// http://www.nulldesign.de/2007/11/30/as3-preloading-continued/
		// http://adventuresinactionscript.com/blog/03-04-2008/as3-preloader-with-mochiad-mochibot-simple-domain-locking-and-glossy-vista-style-pro
		// http://blog.jerrydon.com/index.php/2008/07/fullscreen-flex-preloader/
		// http://www.docsultant.com/site2/articles/flex_cmd.html
		// http://jerrydon.com/flex/fullscreenpreloader/srcview/index.html
		// http://www.ghost23.de/blogarchive/2008/04/as3-application-1.html
		// http://livedocs.adobe.com/flex/3/langref/mx/preloaders/DownloadProgressBar.html
		// http://livedocs.adobe.com/flex/3/html/help.html?content=app_container_4.html
		// http://www.bit-101.com/blog/?p=946

		/// <summary>
		/// Default constructor
		/// </summary>
		public FlashPreloader()
		{
			this.InvokeWhenStageIsReady(
				delegate
				{
					new TextField { text = "This is the main content" }.AttachTo(this);


					Assets.Default[Assets.Path + "/WONDERIN.mp3"].ToSoundAsset().play();
				}
			);
		}
	}

	[Script]
	public class MyFactory : Sprite
	{
		public MyFactory()
		{
			// this.stop();

			var e = default(Action<Event>);

			var s = new Sprite().AttachTo(this);

			var t = new TextField { text = "loading", autoSize = TextFieldAutoSize.LEFT }.AttachTo(s);

			e = delegate
			{
				t.text = new { root.loaderInfo.bytesLoaded, root.loaderInfo.bytesTotal /*, framesLoaded, totalFrames */}.ToString();

				if (root.loaderInfo.bytesLoaded == root.loaderInfo.bytesTotal)
				{
					this.enterFrame -= e;

					t.textColor = 0xff0000;

					s.FadeOut(
						delegate
						{
							var x = Activator.CreateInstance(Type.GetType(FlashPreloader.Type_FullName)) as DisplayObject;

							s.Orphanize();
							x.AttachTo(this);
						}
					);
						


				}
			};

			this.enterFrame += e;
		}
	}
}