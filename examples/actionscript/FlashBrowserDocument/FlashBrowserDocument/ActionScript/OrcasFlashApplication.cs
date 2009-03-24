using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using System.Collections.Generic;
using System;
using System.IO;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.ui;
using FlashBrowserDocument.Shared;
using ScriptCoreLib.ActionScript.DOM;
using ScriptCoreLib.ActionScript.DOM.Extensions;
using ScriptCoreLib.ActionScript.DOM.HTML;

namespace FlashBrowserDocument.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint(Width = DefaultWidth, Height = DefaultHeight, Feed = DefaultFeed)]
	[SWF(width = DefaultWidth, height = DefaultHeight)]
	public class FlashBrowserDocument : Sprite
	{
		// media rss for cooliris
		public const string DefaultFeed = "http://api.flickr.com/services/feeds/photos_public.gne?id=22203361@N03&lang=en-us&format=rss_200";
		public const int DefaultWidth = 400;
		public const int DefaultHeight = 200;

		// change: C:\util\xampplite\apache\conf\httpd.conf

		// http://localhost/jsc/FlashBrowserDocument/FlashBrowserDocument.htm

		/*
		Alias /jsc/FlashBrowserDocument "C:\work\jsc.svn\examples\actionscript\FlashBrowserDocument\FlashBrowserDocument\bin\Release\web"
		<Directory "C:\work\jsc.svn\examples\actionscript\FlashBrowserDocument\FlashBrowserDocument\bin\Release\web">
		       Options Indexes FollowSymLinks ExecCGI
		       AllowOverride All
		       Order allow,deny
		       Allow from all
		</Directory>
		*/

		/// <summary>
		/// Default constructor
		/// </summary>
		public FlashBrowserDocument()
		{
			var t = new TextField
			{
				defaultTextFormat = new TextFormat { font = "Courier" },
				backgroundColor = 0x303030,
				textColor = 0xffff00,
				text = "powered by jsc",
				background = true,
				x = 0,
				y = 0,
				alwaysShowSelection = true,
				width = DefaultWidth,
				height = DefaultHeight
			}.AttachTo(this);

			// you should be running within the browser
			//SecurityError: Error #2060: Security sandbox violation: ExternalInterface caller file:///C:/work/jsc.svn/examples/actionscript/FlashBrowserDocument/FlashBrowserDocument/bin/Release/web/FlashBrowserDocument.swf cannot access file:///C:/work/jsc.svn/examples/actionscript/FlashBrowserDocument/FlashBrowserDocument/bin/Release/web/FlashBrowserDocument.htm.
			//    at flash.external::ExternalInterface$/_initJS()
			//    at flash.external::ExternalInterface$/addCallback()
			//    at Extensions::ExternalExtensions$/External_100668292()
			//    at DOM::ExternalContext()
			//    at DOM::ExternalContext$/ExternalAuthentication_100663321()
			//    at FlashBrowserDocument.ActionScript::FlashBrowserDocument()

			ExternalContext.ExternalAuthentication(
				context =>
				{
					context.Document.body.style.backgroundColor = "#afafff";
					context.Document.body.style.color = "#000080";

					t.appendText("\nflash element was found within html document");

					context.Document.title = "hello world";

					#region hide/show flash element
					var HideFlashButtonCounter = 0;
					var HideFlashButton = new IHTMLButton { innerHTML = "hide flash element" };

					HideFlashButton.AttachTo(context);
					HideFlashButton.onclick +=
						delegate
						{
							if (HideFlashButtonCounter % 2 == 0)
							{
								t.appendText("\nflash element hidden");
								context.Element.width = 0;
								context.Element.height = 0;
								HideFlashButton.innerHTML = "show flash element";
							}
							else
							{
								t.appendText("\nflash element shown");
								context.Element.width = DefaultWidth;
								context.Element.height = DefaultHeight;
								HideFlashButton.innerHTML = "hide flash element";
							}

							HideFlashButtonCounter++;
						};
					#endregion

					var Content = @"
					<hr />
					<blockqoute>
						<h1>This application was written in c# and was compiled to actionscript with <a href='http://jsc.sf.net'>jsc compiler</a>.</h1>
						<h2>Currently supported browsers:</h2>
						<ul>
							<li><img src='http://www.w3schools.com/images/compatible_firefox.gif' />Firefox</li>
							<li><img src='http://www.w3schools.com/images/compatible_chrome.gif' />Google Chrome</li>
							<li><img src='http://www.w3schools.com/images/compatible_safari.gif' />Safari</li>
							<li><img src='http://www.w3schools.com/images/compatible_opera.gif' />Opera</li>
						</ul>
					</blockqoute>
					".AttachAsDiv(context);

					var DynamicChild = new IHTMLSpan { innerHTML = "hello world" }.AttachTo(Content);

					DynamicChild.style.color = "red";
					DynamicChild.innerHTML = "click on the image to remove it!";

					var DynamicChildImage = new IHTMLImage
					{
						title = "jsc diagram",
						src = "http://jsc.sourceforge.net/jsc.png"
					}.AttachTo(DynamicChild);

					DynamicChildImage.style.backgroundColor = "white";

					DynamicChildImage.onclick +=
						delegate
						{
							DynamicChild.removeChild(DynamicChildImage);
							DynamicChild.innerHTML = "you have removed that image!";

							var Undo = new IHTMLButton { innerHTML = "undo" }.AttachTo(DynamicChild);

							Undo.onclick +=
								delegate
								{
									DynamicChildImage.AttachTo(DynamicChild);
									DynamicChild.removeChild(Undo);
								};
						};

					DynamicChild.onclick +=
						delegate
						{

						};
				}
			);

		}

		static FlashBrowserDocument()
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