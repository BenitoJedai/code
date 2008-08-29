using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using System.Collections.Generic;
using System;
using ScriptCoreLib.Shared;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.MochiLibrary;

namespace FlashPreloader.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint, Frame(typeof(MyFactory))]
	[SWF(width = 400, height = 400)]
	public class FlashPreloader : Sprite
	{

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


					Assets.Default["WONDERIN"].ToSoundAsset().play();
				}
			);
		}
	}


	[Script]
	[SWF(width = 400, height = 400)]
	public class MyFactory : MochiAdPreloaderBase
	{
		[TypeOfByNameOverride]
		public override DisplayObject CreateInstance()
		{
			return typeof(FlashPreloader).CreateInstance() as DisplayObject;

		}

		public override bool AutoCreateInstance()
		{
			return false;
		}

	

		public MyFactory()
		{
			var t = new TextField { text = "loading", autoSize = TextFieldAutoSize.LEFT, x = 400 }.AttachTo(this);



			var Ready = default(Action);

			Ready = delegate
			{
				// 1 more
				Ready = delegate
				{
					// done
					Ready = delegate
					{
						// nothing
					};

					CreateInstance().AttachTo(this);
				};
			};

			_mochiads_game_id = "test";

			this.InvokeWhenStageIsReady(
				delegate
				{
					this.showPreGameAd(() => Ready(), 400, 400);

				}
			);

			this.LoadingComplete +=
				delegate
				{
					t.Orphanize();

					Ready();
				};

			this.LoadingInProgress +=
				delegate
				{
					t.text = new { root.loaderInfo.bytesLoaded, root.loaderInfo.bytesTotal /*, framesLoaded, totalFrames */}.ToString();
				};

		}
	}



}