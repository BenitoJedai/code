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
using ChromeShaderToyColumns;
using ChromeShaderToyColumns.Design;
using ChromeShaderToyColumns.HTML.Pages;
using ScriptCoreLib.JavaScript.WebGL;
using ScriptCoreLib.JavaScript.WebAudio;

namespace ChromeShaderToyColumns
{
	using ScriptCoreLib.GLSL;
	using System.Diagnostics;
	using gl = WebGLRenderingContext;


	/// <summary>
	/// Your client side code running inside a web browser as JavaScript.
	/// </summary>
	public sealed class Application : ApplicationWebService
	{

		public Application(IApp page)
		{
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


			// view-source:https://www.shadertoy.com/view/Xls3WS
			// https://www.shadertoy.com/api

			// https://www.shadertoy.com/view/Xls3WS
			// https://www.shadertoy.com/js/cmRenderUtils.js
			// https://www.shadertoy.com/js/effect.js

			// what does it take to import those nice shaders into jsc world?

			// x:\jsc.svn\examples\javascript\webgl\webglchocolux\webglchocolux\application.cs
			// it looks there are no channels.
			// is it a vert or frag?
			//  fragColor = vec4( col, 1.0 );
			// must be a frag
			// <body onload="watchInit()" 


			//ChromeShaderToyColumns.Library.ShaderToy.AttachToDocument(
			//	new Shaders.ProgramFragmentShader()
			//);

			new { }.With(
				async delegate
				{
					Native.body.style.margin = "0px";
					(Native.body.style as dynamic).webkitUserSelect = "auto";

					var vs = new Shaders.ProgramFragmentShader();

					var mAudioContext = new AudioContext();
					var gl = new WebGLRenderingContext(alpha: true);
					var c = gl.canvas.AttachToDocument();

					c.style.SetSize(460, 237);
					c.width = 460;
					c.height = 237;

					var u = new UIKeepRendering
					{
						animate = true
					}.AttachToDocument();

					//new IHTMLPre { "init..." }.AttachToDocument();

					// function ShaderToy( parentElement, editorParent, passParent )
					// function buildInputsUI( me )

					//  this.mGLContext = createGlContext( this.mCanvas, false, true );
					//  {alpha: useAlpha, depth: false, antialias: false, stencil: true, premultipliedAlpha: false, preserveDrawingBuffer: usePreserveBuffer } 

					var mMouseOriX = 0;
					var mMouseOriY = 0;
					var mMousePosX = 0;
					var mMousePosY = 0;

					// 308
					//var mEffect = new Library.ShaderToy.Effect(
					//	mAudioContext,
					//	gl,

					//	callback: delegate
					//	{
					//		new IHTMLPre { "at callback" }.AttachToDocument();

					//	},
					//	obj: null,
					//	forceMuted: false,
					//	forcePaused: false
					//);


					////mEffect.mPasses[0].NewTexture
					//// EffectPass.prototype.NewTexture = function( wa, gl, slot, url )
					//// this.mPasses[j].Create( rpass.type, this.mAudioContext, this.mGLContext );
					//// EffectPass.prototype.MakeHeader_Image = function( precission, supportDerivatives )
					//mEffect.mPasses[0].MakeHeader_Image();

					//// EffectPass.prototype.NewShader = function( gl, shaderCode )
					//// EffectPass.prototype.NewShader_Image = function( gl, shaderCode )
					//mEffect.mPasses[0].NewShader_Image(vs);

					//// ShaderToy.prototype.resetTime = function()
					// Effect.prototype.ResetTime = function()

					// ShaderToy.prototype.startRendering = function()
					// Effect.prototype.Paint = function(time, mouseOriX, mouseOriY, mousePosX, mousePosY, isPaused)
					// EffectPass.prototype.Paint = function( wa, gl, time, mouseOriX, mouseOriY, mousePosX, mousePosY, xres, yres, isPaused )
					// EffectPass.prototype.Paint_Image = function( wa, gl, time, mouseOriX, mouseOriY, mousePosX, mousePosY, xres, yres )

					var pass = new Library.ShaderToy.EffectPass(
						mAudioContext,
						gl,
						precission: Library.ShaderToy.DetermineShaderPrecission(gl),
						supportDerivatives: gl.getExtension("OES_standard_derivatives") != null,
						callback: null,
						obj: null,
						forceMuted: false,
						forcePaused: false,
						//quadVBO: Library.ShaderToy.createQuadVBO(gl, right: 0, top: 0),
						outputGainNode: null
					);
					pass.MakeHeader_Image();
					pass.NewShader_Image(vs);

					var sw = Stopwatch.StartNew();

					do
					{
						pass.Paint_Image(
							sw.ElapsedMilliseconds / 1000.0f,

							mMouseOriX,
							mMouseOriY,
							mMousePosX,
							mMousePosY,

							// gl_FragCoord
							// cannot be scaled, and can be referenced directly.
							// need another way to scale
							zoom: 0.3f
						);

						// what does it do?
						gl.flush();

						await u.animate.async.@checked;
					}
					while (await Native.window.async.onframe);

				}
			);
		}

	}
}
