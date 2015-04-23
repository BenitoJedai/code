using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ChromeTCPServerAppWindow;
using ChromeTCPServerAppWindow.Design;
using ChromeTCPServerAppWindow.HTML.Pages;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms;
using System.Windows.Forms;

namespace ChromeTCPServer
{


	// http://www.snip2code.com/Snippet/19734/Visual-studio-intellisense-file-for-chro
	[Script(HasNoPrototype = true)]
	class xPointerLockPermissionRequest
	{
		// https://developer.chrome.com/apps/tags/webview#type-PointerLockPermissionRequest

		// tested by ?
		public void allow()
		{
		}
	}

	public static class TheServerWithAppWindow
	{

		//        script: error JSC1000: *** stack is empty, invalid pop?
		//script: error JSC1000: error at ChromeTCPServer.TheServerWithAppWindow+<>c__DisplayClass8+<<Invoke>b__13>d__0+<MoveNext>06000022.<008a> ldloca.s.try,
		// assembly: W:\ChromeTCPServerAppWindow.Application.exe
		// type: ChromeTCPServer.TheServerWithAppWindow+<>c__DisplayClass8+<<Invoke>b__13>d__0+<MoveNext>06000022, ChromeTCPServerAppWindow.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
		// offset: 0x0036
		//  method:Int32<008a> ldloca.s.try(<MoveNext>06000022, <<Invoke>b__13>d__0 ByRef, System.Runtime.CompilerServices.TaskAwaiter ByRef, System.Runtime.CompilerServices.TaskAwaiter`1[ScriptCoreLib.JavaScript.DOM.MessageEvent] ByRef)


		public static void Invoke(
			string AppSource
		)
		{
			Console.WriteLine("enter TheServerWithAppWindow.Invoke");


			if (!(Native.window.opener == null && Native.window.parent == Native.window.self))
			{
				Console.WriteLine("chrome.app.window.create, is that you?");

				#region __WebBrowser.InitializeInternalElement
				__WebBrowser.InitializeInternalElement = that =>
				{
					var webview = Native.document.createElement("webview");
					// You do not have permission to use <webview> tag. Be sure to declare 'webview' permission in your manifest. 
					webview.setAttribute("partition", "p1");
					webview.setAttribute("allowtransparency", "true");

					// wont work?
					// http://stackoverflow.com/questions/25421586/webview-in-chrome-packaged-app-cannot-be-full-screen
					webview.setAttribute("allowfullscreen", "true");

					webview.style.Opacity = 0.0;


					// 2012 for web faults..
					// 2012 for desktop faults..
					// 2013 for web works. roslynctp 0.5

					webview.addEventListener("loadstop", async e =>
							 {
								 Console.WriteLine("loadstop");
								 // prevent showing white while loading...



								 // http://stackoverflow.com/questions/21624897/how-can-a-chrome-packaged-app-interact-with-a-webview-listen-to-events-fired-fr

								 IWindow contentWindow = (webview as dynamic).contentWindow;

								 // https://groups.google.com/a/chromium.org/forum/#!topic/chromium-apps/gOKDKDk99pQ
								 // X:\jsc.svn\examples\javascript\WebGL\WebGLTiltShift\WebGLTiltShift\Application.cs
								 // X:\jsc.svn\examples\javascript\chrome\apps\ChromeWebviewFullscreen\ChromeWebviewFullscreen\Application.cs

								 // this will break the async even for roslyn ctp 0.5

								 new { }.With(
									 async delegate
								 {
									 retry:
									 {
										 Console.WriteLine("awaiting to go fullscreen");

										 await contentWindow.postMessageAsync("virtual webview.requestFullscreen");

										 Console.WriteLine("awaiting to go fullscreen. go!");


										 // this makes webview reload. thats bad.
										 //that.FindForm().FormBorderStyle = FormBorderStyle.None;


										 // http://stackoverflow.com/questions/15451888/how-can-i-make-a-chrome-packaged-app-which-runs-in-fullscreen-at-startup
										 chrome.app.window.current().fullscreen();
										 //webview.requestFullscreen();
										 goto retry;
									 }
								 }
								 );


								 await Task.Delay(100);

								 //webview.style.display = IStyle.DisplayEnum.block;
								 webview.style.Opacity = 1.0;
							 }
					 );



					#region permissionrequest
					// https://github.com/GoogleChrome/chromium-webview-samples
					// permissionrequest
					// https://developer.chrome.com/apps/tags/webview#type-WebRequestEventInteface
					webview.addEventListener("permissionrequest",
						(e) =>
								{
									// https://code.google.com/p/chromium/issues/detail?id=141198

									//% c9:176376ms permissionrequest { { permission = pointerLock } }
									//Uncaught TypeError: Cannot read property 'allow' of undefined
									//< webview >: The permission request for "pointerLock" has been denied.

									// X:\jsc.internal.git\market\chrome\ChromeMyJscSolutionsNet\ChromeMyJscSolutionsNet\Application.cs

									// https://chromium.googlesource.com/chromium/src/+/git-svn/chrome/common/extensions/api/webview_tag.json
									// https://bugzilla.mozilla.org/show_bug.cgi?id=896143
									// https://developer.chrome.com/apps/tags/webview#event-permissionrequest
									// https://code.google.com/p/chromium/issues/detail?id=153540

									//  The permission request for "pointerLock" has been denied.
									// http://stackoverflow.com/questions/16302627/geolocation-in-a-webview-inside-a-chrome-packaged-app
									// http://git.chromium.org/gitweb/?p=chromium.git;a=commitdiff;h=e1d226c0ea739adaed36cc4b617f7a387d44eca0

									string permission = (e as dynamic).permission;
									xPointerLockPermissionRequest e_request = (e as dynamic).request;

									Console.WriteLine("permissionrequest " + new
									{
										permission,
										e,
										e_request
									});
									//% c9:167409ms permissionrequest { { permission = pointerLock } }
									//Uncaught TypeError: Cannot read property 'allow' of undefined

									e.preventDefault();


									//9:122010ms permissionrequest { { permission = pointerLock, e = [object Event], e_request = [object Object] } }
									//9:122028ms delay permissionrequest { { permission = pointerLock, e = [object Event], delay_e_request = [object Object] } }
									//Uncaught Error: < webview >: Permission has already been decided for this "permissionrequest" event. 

									//Expando.

									if (e_request != null)
										e_request.allow();

									//Task.Delay(1).ContinueWith(
									//    delegate
									//{
									//    xPointerLockPermissionRequest delay_e_request = (e as dynamic).request;

									//    Console.WriteLine("delay permissionrequest " + new { permission, e, delay_e_request });


									//    if (delay_e_request != null)
									//        delay_e_request.allow();
									//}
									//);
								}
					);
					#endregion


					that.InternalElement = (IHTMLIFrame)(object)webview;

					// src was not copied for some reason. force it.
					that.Size = that.Size;
					that.Refresh();

				};
				#endregion

				var css = Native.css[typeof(Form)][" .caption"];
				(css.style as dynamic).webkitAppRegion = "drag";


				// FormStyler.AtFormCreated = FormStylerLikeFloat.LikeFloat;

				var ShadowRightBottom = 8;

				#region Form
				var f = new Form
				{

					ShowIcon = false,

					Text = Native.document.title,

					//Text = Native.document.location.hash,
					StartPosition = FormStartPosition.Manual
				};


				f.MoveTo(0, 0).SizeTo(
						Native.window.Width - ShadowRightBottom,
						Native.window.Height - ShadowRightBottom
					);

				//f.Opacity = 0.5;

				f.Show();
				#endregion





				var w = new WebBrowser
				{

					// this wont work?
					//Dock = DockStyle.Fill

				}.AttachTo(f);

				w.Navigate(
					Native.document.title
				);


				f.FormClosing +=
					(sender, e) =>
					{
						// X:\jsc.svn\examples\javascript\chrome\apps\ChromeWebviewFullscreen\ChromeWebviewFullscreen\Application.cs
						if (chrome.app.window.current().isFullscreen())
						{
							e.Cancel = true;

							chrome.app.window.current().restore();
							return;
						}
					};

				f.FormClosed +=
					delegate
				{
					// close the appwindow

					// DWM animates the close.
					Native.window.close();
				};

				{
					var cs = f.ClientSize;

					w.SizeTo(
						cs.Width,
						cs.Height
						);
				}


				f.SizeChanged +=
					delegate
				{
					var cs = f.ClientSize;

					w.SizeTo(
						cs.Width,
						cs.Height
						);

					Native.window.resizeTo(
						f.Width + ShadowRightBottom,
						f.Height + ShadowRightBottom
					);

				};

				Native.window.onresize +=
					delegate
				{
					if (chrome.app.window.current().isFullscreen())
					{
						f.SizeGripStyle = SizeGripStyle.Hide;
						f.SizeTo(
					 Native.window.Width,
					 Native.window.Height
				 );
						return;
					}

					f.SizeGripStyle = SizeGripStyle.Auto;
					// outer frame is resized
					f.SizeTo(
						Native.window.Width - ShadowRightBottom,
						Native.window.Height - ShadowRightBottom
					);

				};

				return;
			}

			// looks like alpha is still not available for chrome, nor is it available if aero is disabled, red.
			var alphaEnabled = false;


			Console.WriteLine("before invoke ChromeTCPServer.TheServer.InvokeAsync " + new { alphaEnabled });

			ChromeTCPServer.TheServer.InvokeAsync(AppSource, async uri =>
			{
				var o = new object();
				var hidden = o == o;
				//var alphaEnabled = o == o;

				var alwaysOnTop = o == o;


				var options = new
				{
					//allow webkitAppRegion
					frame = "none",
					//hidden,
					//alphaEnabled,
					alwaysOnTop
				};

				// The URL used for window creation must be local for security reasons.

				Console.WriteLine("before app.window.create");

				//				24950ms before app.window.create
				//view - source:51336 25004ms { { socketId = 21, port = 19866, xmessage = < string reason = "" c = "1" preview = "" n = "Audi Visualization" > Visit me at 192.168.1.200:8324 </ string > } }
				//				view - source:51336 25045ms after app.window.create { { xappwindow = null } }

				var xappwindow = await chrome.app.window.create(
				   Native.document.location.pathname,
						   options
			);

				Console.WriteLine("after app.window.create " + new { xappwindow });
				// 20265ms after app.window.create {{ xappwindow = null }}

				// X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeAudi\Application.cs
				// why the ef is it null?
				if (xappwindow == null)
					return;

				//xappwindow.set

				// can we prevent the white page from appearing?
				await xappwindow.contentWindow.async.onload;

				//xappwindow.contentWindow.document.title = "http://example.com";
				xappwindow.contentWindow.document.title = uri;

				await Task.Delay(100);
				//await Task.Delay(200);

				xappwindow.show();
			}
			);


		}
	}
}

namespace ChromeTCPServerAppWindow
{

	/// <summary>
	/// Your client side code running inside a web browser as JavaScript.
	/// </summary>
	public sealed class Application : ApplicationWebService
	{
		// X:\jsc.svn\examples\javascript\WebGL\WebGLHZBlendCharacter\WebGLHZBlendCharacter\Application.cs

		/// <summary>
		/// This is a javascript application.
		/// </summary>
		/// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
		public Application(IApp page)
		{
			// we do not like our old server anymore
			// we like the new one.

			// based on 
			// X:\jsc.svn\examples\javascript\chrome\apps\ChromeAppWindowForm\ChromeAppWindowForm\Application.cs
			// X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerWithFrameNone\ChromeTCPServerWithFrameNone\Application.cs

			#region += Launched chrome.app.window
			// X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerAppWindow\ChromeTCPServerAppWindow\Application.cs
			dynamic self = Native.self;
			dynamic self_chrome = self.chrome;
			object self_chrome_socket = self_chrome.socket;

			if (self_chrome_socket != null)
			{
				ChromeTCPServer.TheServerWithAppWindow.Invoke(AppSource.Text);

				return;
			}
			#endregion

			Native.body.style.backgroundColor = "transparent";


			new IHTMLPre { "app is now running" }.AttachToDocument();

		}

	}
}
