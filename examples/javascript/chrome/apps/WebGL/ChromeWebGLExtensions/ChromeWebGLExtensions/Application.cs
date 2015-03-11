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
using ChromeWebGLExtensions;
using ChromeWebGLExtensions.Design;
using ChromeWebGLExtensions.HTML.Pages;
using ScriptCoreLib.JavaScript.WebGL;

namespace ChromeWebGLExtensions
{
	/// <summary>
	/// Your client side code running inside a web browser as JavaScript.
	/// </summary>
	public sealed class Application : ApplicationWebService
	{
		/// <summary>
		/// This is a javascript application.
		/// </summary>
		/// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
		public Application(IApp page)
		{
			// https://www.shadertoy.com/view/lsSGRz


			#region += Launched chrome.app.window
			dynamic self = Native.self;
			dynamic self_chrome = self.chrome;
			object self_chrome_socket = self_chrome.socket;

			if (self_chrome_socket != null)
			{
				if (!(Native.window.opener == null && Native.window.parent == Native.window.self))
				{
					Console.WriteLine("chrome.app.window.create, is that you?");

					// pass thru
				}
				else
				{
					// should jsc send a copresence udp message?
					chrome.runtime.UpdateAvailable += delegate
					{
						new chrome.Notification(title: "UpdateAvailable");

					};

					chrome.app.runtime.Launched += async delegate
					{
						// 0:12094ms chrome.app.window.create {{ href = chrome-extension://aemlnmcokphbneegoefdckonejmknohh/_generated_background_page.html }}
						Console.WriteLine("chrome.app.window.create " + new { Native.document.location.href });

						new chrome.Notification(title: "ChromeUDPSendAsync");

						var xappwindow = await chrome.app.window.create(
							   Native.document.location.pathname, options: null
						);

						//xappwindow.setAlwaysOnTop

						xappwindow.show();

						await xappwindow.contentWindow.async.onload;

						Console.WriteLine("chrome.app.window loaded!");
					};


					return;
				}
			}
			#endregion



			// X:\jsc.svn\examples\javascript\WorkerMD5Experiment\WorkerMD5Experiment\Application.cs


			var ctx1 = new WebGLRenderingContext();

			// http://webglreport.com/
			//       unMaskedRenderer: getUnmaskedInfo(gl).renderer,
			//<th>Unmasked Renderer:</th>
			//			<td><%= report.unMaskedRenderer %></td>

			var UNMASKED_RENDERER_WEBGL = "";
			var WEBGL_debug_renderer_info = new
			{
				UNMASKED_RENDERER_WEBGL = 0x9246u
			};



			var dbgRenderInfo = ctx1.getExtension("WEBGL_debug_renderer_info");
			if (dbgRenderInfo != null)
			{
				// https://www.khronos.org/registry/webgl/extensions/WEBGL_debug_renderer_info/
				UNMASKED_RENDERER_WEBGL = (string)ctx1.getParameter(WEBGL_debug_renderer_info.UNMASKED_RENDERER_WEBGL);
			}



			new IHTMLPre { new { UNMASKED_RENDERER_WEBGL } }.AttachToDocument();

			// https://www.khronos.org/registry/webgl/extensions/WEBGL_shared_resources/

			var sharedResourcesExtension = ctx1.getExtension("WEBGL_shared_resources");
			new IHTMLPre { new { sharedResourcesExtension } }.AttachToDocument();

			// https://code.google.com/p/chromium/issues/detail?id=245894
			// https://bugzilla.mozilla.org/show_bug.cgi?id=964788


		}

	}
}
