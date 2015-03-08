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
using ChromeShaderToyVRCardboardGrid;
using ChromeShaderToyVRCardboardGrid.Design;
using ChromeShaderToyVRCardboardGrid.HTML.Pages;
using ScriptCoreLib.JavaScript.WebAudio;
using ScriptCoreLib.JavaScript.WebGL;
using System.Diagnostics;

namespace ChromeShaderToyVRCardboardGrid
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
			// how does this work on android?

			// https://www.shadertoy.com/view/MdfSRj#


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


			new Shaders.ProgramFragmentShader().With(
				async vs =>
				{
					Native.body.style.margin = "0px";
					//Native.document.documentElement.style.overflow = IStyle.OverflowEnum.auto;

					var mAudioContext = new AudioContext();
					var gl = new WebGLRenderingContext(alpha: true);
					var c = gl.canvas.AttachToDocument();

					c.style.SetSize(460, 237);
					c.width = 460;
					c.height = 237;

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










					#region CaptureMouse
					var mMouseOriX = 0;
					var mMouseOriY = 0;
					var mMousePosX = 0;
					var mMousePosY = 0;

					c.onmousedown += async ev =>
					{
						mMouseOriX = ev.CursorX;
						mMouseOriY = ev.CursorY;
						mMousePosX = mMouseOriX;
						mMousePosY = mMouseOriY;

						// why aint it canvas?
						//ev.Element
						//ev.CaptureMouse();

						// using ?
						ev.Element.requestPointerLock();
						await ev.Element.async.onmouseup;
						Native.document.exitPointerLock();

						mMouseOriX = -Math.Abs(mMouseOriX);
						mMouseOriY = -Math.Abs(mMouseOriY);
					};

					c.onmousemove += ev =>
					{
						if (ev.MouseButton == IEvent.MouseButtonEnum.Left)
						{
							mMousePosX += ev.movementX;
							mMousePosY += ev.movementY;
						}
					};

					#endregion


					mEffect.mPasses[0].mInputs[0] = new ChromeShaderToyColumns.Library.ShaderToy.samplerCube { };

					mEffect.mPasses[0].MakeHeader_Image();
					mEffect.mPasses[0].NewShader_Image(vs);

					#region onresize
					new { }.With(
						async delegate
						{
							do
							{
								c.width = Native.window.Width;
								//c.height = Native.window.Height / 2;
								c.height = Native.window.Height;
								c.style.SetSize(c.width, c.height);
							}
							while (await Native.window.async.onresize);
						}
					);
					#endregion

					var sw = Stopwatch.StartNew();
					do
					{
						mEffect.mPasses[0].Paint_Image(
							sw.ElapsedMilliseconds / 1000.0f,

							mMouseOriX,
							mMouseOriY,
							mMousePosX,
							mMousePosY,

							xres: c.width,
							yres: c.height
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
