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
using GLSLShaderToyPip;
using GLSLShaderToyPip.Design;
using GLSLShaderToyPip.HTML.Pages;
using ScriptCoreLib.GLSL;
using ScriptCoreLib.JavaScript.WebGL;
using System.Diagnostics;

namespace GLSLShaderToyPip
{
	using ChromeShaderToyColumns.Library;
	using gl = WebGLRenderingContext;

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
			// the idea of this exammple
			// is to look at how multiple shaders can be linked to work together.
			// we need two shaders
			// first we could run them as separate programs in pip mode
			// selected by the host/javascript
			// then repeat the same experiment, but have the shader do the pip in a single program
			// later shader code could be nugeted
			// lets have a copy of
			// X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeShaderToyQuadraticBezierByMattdesl\ChromeShaderToyQuadraticBezierByMattdesl\Shaders\Program.frag
			// locally should we need to modify it..

			// can we change colors?


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

			AttachToDocument();
		}

		public static async void AttachToDocument()
		{
			Native.body.style.margin = "0px";


			var gl = new WebGLRenderingContext(alpha: true);

			if (gl == null)
			{

				new IHTMLPre {
					// https://code.google.com/p/chromium/issues/detail?id=294207
					"Rats! WebGL hit a snag. \n WebGL: Unavailable.\n GPU process was unable to boot. \n restart chrome.",

					// chrome sends us to about:blank?
					//new IHTMLAnchor {

					//	target = "_blank",

					//	href = "about:gpu", innerText = "about:gpu",

					//	// http://tirania.org/blog/archive/2009/Jul-27-1.html
					//	//onclick += de
					//}
					//.With(a => {  a.onclick += e => { e.preventDefault();  Native.window.open("about:gpu"); }; } )


				}.AttachToDocument();
				return;
			}

			var c = gl.canvas.AttachToDocument();

			#region oncontextlost
			gl.oncontextlost +=
				e =>
				{
					//[12144:10496:0311 / 120850:ERROR: gpu_watchdog_thread.cc(314)] : The GPU process hung. Terminating after 10000 ms.
					//   GpuProcessHostUIShim: The GPU process crashed!
					gl.canvas.Orphanize();

					new IHTMLPre {
						// https://code.google.com/p/chromium/issues/detail?id=294207
						@"Rats! WebGL hit a snag.
oncontextlost.
The GPU process hung. Terminating. 
check chrome://gpu for log messages.  
do we have a stack trace?

" + new { e.statusMessage } ,

						// chrome sends us to about:blank?
						//new IHTMLAnchor {

						//	target = "_blank",

						//	href = "about:gpu", innerText = "about:gpu",

						//	// http://tirania.org/blog/archive/2009/Jul-27-1.html
						//	//onclick += de
						//}
						//.With(a => {  a.onclick += e => { e.preventDefault();  Native.window.open("about:gpu"); }; } )


					}.AttachToDocument();
				};
			#endregion


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

			var quadVBO = ShaderToy.createQuadVBO(gl);
            var pass0 = new ShaderToy.EffectPass(
				null,
				gl,
				precission: ShaderToy.DetermineShaderPrecission(gl),
				supportDerivatives: gl.getExtension("OES_standard_derivatives") != null,
				callback: null,
				obj: null,
				forceMuted: false,
				forcePaused: false,
				quadVBO: quadVBO,
				outputGainNode: null
			);
			pass0.MakeHeader_Image();
			var frag = new GLSLShaderToyPip.Shaders.ProgramFragmentShader();
			pass0.NewShader_Image(frag);

			var sw = Stopwatch.StartNew();
			do
			{
				ChromeShaderToyColumns.Library.ShaderToy.EffectPass.Paint_ImageDelegate Paint_Image =
					(time, mouseOriX, mouseOriY, mousePosX, mousePosY) =>
					{
						var mProgram = pass0.xCreateShader.mProgram;


						var viewportxres = gl.canvas.width / 2;
						var viewportyres = gl.canvas.height;

						#region Paint_Image
						//new IHTMLPre { "enter Paint_Image" }.AttachToDocument();


						gl.viewport(0, viewportyres / 2, viewportxres, viewportyres);

						// useProgram: program not valid
						gl.useProgram(mProgram);

						// uniform4fv
						var mouse = new[] { mousePosX, mousePosY, mouseOriX, mouseOriY };

						var l2 = gl.getUniformLocation(mProgram, "iGlobalTime"); if (l2 != null) gl.uniform1f(l2, time);
						var l3 = gl.getUniformLocation(mProgram, "iResolution"); if (l3 != null) gl.uniform3f(l3, viewportxres, viewportyres, 1.0f);
						var l4 = gl.getUniformLocation(mProgram, "iMouse"); if (l4 != null) gl.uniform4fv(l4, mouse);
						//var l7 = gl.getUniformLocation(this.mProgram, "iDate"); if (l7 != null) gl.uniform4fv(l7, dates);
						//var l9 = gl.getUniformLocation(this.mProgram, "iSampleRate"); if (l9 != null) gl.uniform1f(l9, this.mSampleRate);

						var ich0 = gl.getUniformLocation(mProgram, "iChannel0"); if (ich0 != null) gl.uniform1i(ich0, 0);
						var ich1 = gl.getUniformLocation(mProgram, "iChannel1"); if (ich1 != null) gl.uniform1i(ich1, 1);
						var ich2 = gl.getUniformLocation(mProgram, "iChannel2"); if (ich2 != null) gl.uniform1i(ich2, 2);
						var ich3 = gl.getUniformLocation(mProgram, "iChannel3"); if (ich3 != null) gl.uniform1i(ich3, 3);

						// using ?
						var l1 = (uint)gl.getAttribLocation(mProgram, "pos");
						gl.bindBuffer(gl.ARRAY_BUFFER, quadVBO);
						gl.vertexAttribPointer(l1, 2, gl.FLOAT, false, 0, 0);
						gl.enableVertexAttribArray(l1);


						//for (var i = 0; i < mInputs.Length; i++)
						//{
						//	var inp = mInputs[i];

						//	gl.activeTexture((uint)(gl.TEXTURE0 + i));

						//	if (inp == null)
						//	{
						//		gl.bindTexture(gl.TEXTURE_2D, null);
						//	}
						//}

						var times = new[] { 0.0f, 0.0f, 0.0f, 0.0f };
						var l5 = gl.getUniformLocation(mProgram, "iChannelTime");
						if (l5 != null) gl.uniform1fv(l5, times);

						var resos = new float[12] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
						var l8 = gl.getUniformLocation(mProgram, "iChannelResolution");
						if (l8 != null) gl.uniform3fv(l8, resos);


						gl.drawArrays(gl.TRIANGLES, 0, 6);
						// first frame is now visible
						gl.disableVertexAttribArray(l1);
						#endregion

						//mFrame++;

					};


				Paint_Image(
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
	}
}
