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
using ChromeShaderToySeascapeByTDM;
using ChromeShaderToySeascapeByTDM.Design;
using ChromeShaderToySeascapeByTDM.HTML.Pages;
using System.Diagnostics;
using ScriptCoreLib.JavaScript.WebAudio;
using ScriptCoreLib.JavaScript.WebGL;

namespace ChromeShaderToySeascapeByTDM
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
			// X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeShaderToyColumns\ChromeShaderToyColumns\Application.cs
			// X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeShaderToySeascapeByTDM\ChromeShaderToySeascapeByTDM\Application.cs

			// https://www.shadertoy.com/view/Ms2SD1

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

			new { }.With(
			async delegate
			{
				Native.body.style.margin = "0px";

				var vs = new Shaders.ProgramFragmentShader();

				var mAudioContext = new AudioContext();
				var gl = new WebGLRenderingContext(alpha: true);
				var c = gl.canvas.AttachToDocument();

				#region onresize
				new { }.With(
					async delegate
					{
						do
						{
							c.width = Native.window.Width;
							c.height = Native.window.Height;
							c.style.SetSize(c.width, c.height);
						}
						while (await Native.window.async.onresize);
					}
				);
				#endregion




				#region CaptureMouse
				var mMouseOriX = 0;
				var mMouseOriY = 0;
				var mMousePosX = 0;
				var mMousePosY = 0;

				c.onmousedown += ev =>
				{
					mMouseOriX = ev.CursorX;
					mMouseOriY = ev.CursorY;
					mMousePosX = mMouseOriX;
					mMousePosY = mMouseOriY;

					ev.CaptureMouse();
				};

				c.onmousemove += ev =>
				{
					if (ev.MouseButton == IEvent.MouseButtonEnum.Left)
					{
						mMousePosX = ev.CursorX;
						mMousePosY = c.height - ev.CursorY;
					}
				};


				c.onmouseup += ev =>
				{
					mMouseOriX = -Math.Abs(mMouseOriX);
					mMouseOriY = -Math.Abs(mMouseOriY);
				};
				#endregion

				var mEffect = new ChromeShaderToyColumns.Library.ShaderToy.Effect(
					mAudioContext,
					gl,

					callback: delegate
					{
						new IHTMLPre { "at callback" }.AttachToDocument();

					},
					obj: null,
					forceMuted: false,
					forcePaused: false
				);

				mEffect.mPasses[0].MakeHeader_Image();
				mEffect.mPasses[0].NewShader_Image(vs);

				var sw = Stopwatch.StartNew();
				do
				{
					mEffect.mPasses[0].Paint_Image(
						sw.ElapsedMilliseconds / 1000.0f,

						mMouseOriX,
						mMouseOriY,
						mMousePosX,
						mMousePosY

					);

					// what does it do?
					gl.flush();

				}
				while (await Native.window.async.onframe);

			}
		);
		}

	}
}
