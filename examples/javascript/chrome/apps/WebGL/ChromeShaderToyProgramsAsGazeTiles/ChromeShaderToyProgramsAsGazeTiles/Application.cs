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
using ChromeShaderToyProgramsAsGazeTiles;
using ChromeShaderToyProgramsAsGazeTiles.Design;
using ChromeShaderToyProgramsAsGazeTiles.HTML.Pages;
using ScriptCoreLib.JavaScript.WebGL;
using ChromeShaderToyPrograms;
using System.Diagnostics;
using ChromeShaderToyColumns.Library;

namespace ChromeShaderToyProgramsAsGazeTiles
{
	using ScriptCoreLib.GLSL;
	using gl = WebGLRenderingContext;

	/// <summary>
	/// Your client side code running inside a web browser as JavaScript.
	/// </summary>
	public sealed class Application : ApplicationWebService
	{
		// This type of exception does happen when you are stuck inside unmanaged code which performs an uninterruptable blocking operation. Waiting for a native socket select would be such a case. If your Dll does cause socket connections make sure you close them before they your unload your AppDomain.
		// https://social.msdn.microsoft.com/Forums/vstudio/en-US/01feeacf-883b-4058-b6c4-40ddbd67fa79/error-while-unloading-appdomain-exception-from-hresult-0x80131015?forum=clr

		//2ae8:01:01 [jsc.meta]
		//		worker unloading... { Count = 0 }

		//		Unhandled Exception: System.CannotUnloadAppDomainException: Error while unloading appdomain. (Exception from HRESULT: 0x80131015)
		//   at System.AppDomain.Unload(AppDomain domain)
		//   at MultiAssemblyLauncher.Invoke(String[] args, String id, WorkerStartAction yield) in X:\jsc.internal.git\compiler\jsc\Program.cs:line 290

		/// <summary>
		/// This is a javascript application.
		/// </summary>
		/// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
		public Application(IApp page)
		{
			// first lets add special spacing for non selected tile.
			// then lets slow the frame rate for every other tile to get 60fps!
			// then add more tiles
			// then 2x detail


			// 
			// https://forums.creativecow.net/thread/2/982779

			// https://www.shadertoy.com/view/MsfXDS


			// show shader based on tab selection?



			//Native.document.documentElement.style.overflow = IStyle.OverflowEnum.auto;


			// chrome by default has no scrollbar, bowser does
			Native.document.documentElement.style.overflow = IStyle.OverflowEnum.hidden;
			//Native.document.documentElement.style.overflow = IStyle.OverflowEnum.auto;
			Native.body.style.margin = "0px";
			Native.body.style.fontSize = "x-small";
			(Native.body.style as dynamic).webkitColumnCount = "4";	/* Chrome, Safari, Opera */

			//Native.body.style.backgroundColor = "yellow";
			Native.body.Clear();

			// ipad?
			Native.window.onerror +=
				e =>
				{
					new IHTMLPre {
						"error " + new { e.message, e.error }
					}.AttachToDocument();
				};

			// https://www.youtube.com/watch?v=tnS8K0yhmZU
			// http://www.reddit.com/r/oculus/comments/2sv5lk/new_release_of_shadertoy_vr/
			// https://www.shadertoy.com/view/lsSGRz

			// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150309
			// https://zproxy.wordpress.com/2015/03/09/project-windstorm/
			// https://github.com/jimbo00000/RiftRay


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


			// http://stackoverflow.com/questions/3433010/what-is-the-getcsscanvascontext-method-of-an-html5-element
			// CanvasRenderingContext getCSSCanvasContext(in DOMString contextType, in DOMString identifier, in long width, in long height);



			var gl = new WebGLRenderingContext(alpha: true);

			if (gl == null)
			{

				new IHTMLPre {
					// https://code.google.com/p/chromium/issues/detail?id=294207
					"Rats! WebGL hit a snag."
					//,
					//new IHTMLAnchor { href = "about:gpu", innerText = "about:gpu" }
				}.AttachToDocument();
				return;
			}

			#region oncontextlost
			gl.oncontextlost +=
				e =>
				{
					//[12144:10496:0311 / 120850:ERROR: gpu_watchdog_thread.cc(314)] : The GPU process hung. Terminating after 10000 ms.
					//   GpuProcessHostUIShim: The GPU process crashed!
					gl.canvas.Orphanize();
					gl = null;

					Native.document.body.Clear();

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






			//// page by page
			//var combo = new IHTMLSelect().AttachToDocument();

			//combo.style.position = IStyle.PositionEnum.absolute;
			//combo.style.left = "0px";
			//combo.style.top = "0px";
			////combo.style.right = "0px";
			//combo.style.width = "100%";

			//combo.style.backgroundColor = "rgba(255,255,255,0.5)";
			////combo.style.backgroundColor = "rgba(255,255,0,0.5)";
			////combo.style.background = "linear-gradient(to bottom, rgba(255,255,255,0.5 0%,rgba(255,255,255,0.0 100%))";
			//combo.style.border = "0px solid transparent";
			//combo.style.fontSize = "large";
			//combo.style.paddingLeft = "1em";
			//combo.style.fontFamily = IStyle.FontFamilyEnum.Verdana;
			//combo.style.cursor = IStyle.CursorEnum.pointer;





			var c = gl.canvas.AttachToDocument();
			c.style.position = IStyle.PositionEnum.@fixed;
			//c.css.not.hover.style.Opacity = 0.7;

			c.onmouseover += delegate { c.style.Opacity = 1.0; };
			c.onmouseout += delegate { c.style.Opacity = 0.7; };

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


			// middle mouse/two fingers to pan?
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

				// while mousemove or !mouseup

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

			//c.onmousewheel += ev =>
			//{
			//	ev.preventDefault();
			//	ev.stopPropagation();

			//	mMousePosY += 3 * ev.WheelDirection;
			//};

			#endregion




			// will this crash rdp session=?
			//var xWebGLRenderbuffer0size = 1024;
			//var xWebGLRenderbuffer0size = 512;
			var xWebGLRenderbuffer0size = 256;
			//var xWebGLRenderbuffer0size = 128;
			//var xWebGLRenderbuffer0size = 64;

			//var xWebGLRenderbuffer0size = 16;
			//var xWebGLRenderbuffer0size = 8;
			//var xWebGLRenderbuffer0size = 4;
			//var xWebGLRenderbuffer0size = 2;
			//var xWebGLRenderbuffer0size = 1;



			#region createShader
			// dont we have a better api already?
			Func<ScriptCoreLib.GLSL.Shader, WebGLShader> createShader = (src) =>
			{
				var shader = gl.createShader(src);

				// verify
				if (gl.getShaderParameter(shader, gl.COMPILE_STATUS) == null)
				{
					Native.window.alert("error in SHADER:\n" + gl.getShaderInfoLog(shader));
					throw new InvalidOperationException("shader failed");

					return null;
				}

				return shader;
			};
			#endregion

			var vs = createShader(new Shaders.GeometryVertexShader());
			var fs = createShader(new Shaders.GeometryFragmentShader());

			var shaderProgram = new WebGLProgram(gl);
			gl.attachShader(shaderProgram, vs);
			gl.attachShader(shaderProgram, fs);
			gl.linkProgram(shaderProgram);

			var vec3aVertexPositionBuffer = new WebGLBuffer(gl);
			var vec2aTextureCoordBuffer = new WebGLBuffer(gl);

			//gl.clearColor(0.0f, 0.0f, 0.0f, 1.0f);
			gl.enable(gl.DEPTH_TEST);


			#region newPass
			Func<FragmentShader, ShaderToy.EffectPass> newPass = frag =>
			{
				var xWebGLFramebuffer0 = new WebGLFramebuffer(gl);
				gl.bindFramebuffer(gl.FRAMEBUFFER, xWebGLFramebuffer0);
				// generateMipmap: level 0 not power of 2 or not all the same size
				//var rttFramebuffer_width = canvas.width;
				// WebGL: INVALID_OPERATION: generateMipmap: level 0 not power of 2 or not all the same size

				// Max Combined Texture Image Units:	8 ipad
				// Max Combined Texture Image Units:	20 asus7
				// Max Combined Texture Image Units:	32
				// need to start reusing after 32?
				// http://webglstats.com/
				var xWebGLTexture0 = new WebGLTexture(gl);
				gl.bindTexture(gl.TEXTURE_2D, xWebGLTexture0);
				gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, (int)gl.LINEAR);
				gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, (int)gl.LINEAR_MIPMAP_NEAREST);

				gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, xWebGLRenderbuffer0size, xWebGLRenderbuffer0size, 0, gl.RGBA, gl.UNSIGNED_BYTE, null);

				gl.generateMipmap(gl.TEXTURE_2D);


				var xWebGLRenderbuffer0 = new WebGLRenderbuffer(gl);
				gl.bindRenderbuffer(gl.RENDERBUFFER, xWebGLRenderbuffer0);
				gl.renderbufferStorage(gl.RENDERBUFFER, gl.DEPTH_COMPONENT16, xWebGLRenderbuffer0size, xWebGLRenderbuffer0size);

				gl.framebufferTexture2D(gl.FRAMEBUFFER, gl.COLOR_ATTACHMENT0, gl.TEXTURE_2D, xWebGLTexture0, 0);
				gl.framebufferRenderbuffer(gl.FRAMEBUFFER, gl.DEPTH_ATTACHMENT, gl.RENDERBUFFER, xWebGLRenderbuffer0);

				gl.bindTexture(gl.TEXTURE_2D, null);
				gl.bindRenderbuffer(gl.RENDERBUFFER, null);
				gl.bindFramebuffer(gl.FRAMEBUFFER, null);

				var pass0 = new ChromeShaderToyColumns.Library.ShaderToy.EffectPass(
					gl: gl,
					precission: ChromeShaderToyColumns.Library.ShaderToy.DetermineShaderPrecission(gl),
					supportDerivatives: gl.getExtension("OES_standard_derivatives") != null
				);

				pass0.xWebGLFramebuffer0 = xWebGLFramebuffer0;
				pass0.xWebGLTexture0 = xWebGLTexture0;

				pass0.MakeHeader_Image();
				pass0.NewShader_Image(
					frag
					//new ChromeShaderToyColumns.Shaders.ProgramFragmentShader()
					//new ChromeShaderToyTriangleDistanceByIq.Shaders.ProgramFragmentShader()
					);

				return pass0;
			};
			#endregion

			//// we need a share group? to reuse framebuffer?
			//var frag0 =
			//var frag1 =
			//var frag2 =
			//var frag3 =

			var sw = Stopwatch.StartNew();
			var verticesBuffer = new WebGLBuffer(gl);

			#region paintToTex
			Func<ShaderToy.EffectPass, ShaderToy.EffectPass> paintToTex = (xpass0) =>
			{
				gl.bindFramebuffer(gl.FRAMEBUFFER, xpass0.xWebGLFramebuffer0);

				//// http://stackoverflow.com/questions/20362023/webgl-why-does-transparent-canvas-show-clearcolor-color-component-when-alpha-is
				//gl.clearColor(1, 1, 0, 1.0f);

				gl.viewport(0, 0, xWebGLRenderbuffer0size, xWebGLRenderbuffer0size);
				// need to clear, otherewise we see an old image?
				gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);


				#region Paint_Image
				ChromeShaderToyColumns.Library.ShaderToy.EffectPass.Paint_ImageDelegate Paint_Image = (time, mouseOriX, mouseOriY, mousePosX, mousePosY, zoom) =>
				{
					var mProgram = xpass0.xCreateShader.mProgram;


					var xres = xWebGLRenderbuffer0size;
					//var yres = xWebGLRenderbuffer0size;
					// widescreen. discard top half?
					var yres = xWebGLRenderbuffer0size * 0.5f;

					#region Paint_Image

					//new IHTMLPre { "enter Paint_Image" }.AttachToDocument();

					// this is enough to do pip to bottom left, no need to adjust vertex positions even?
					//gl.viewport(0, 0, (int)xres, (int)yres);

					// useProgram: program not valid
					gl.useProgram(mProgram);

					// uniform4fv
					var mouse = new[] { mousePosX, mousePosY, mouseOriX, mouseOriY };

					// X:\jsc.svn\examples\glsl\future\GLSLShaderToyPip\GLSLShaderToyPip\Application.cs
					//gl.getUniformLocation(mProgram, "fZoom").With(fZoom => gl.uniform1f(fZoom, zoom));


					var l2 = gl.getUniformLocation(mProgram, "iGlobalTime"); if (l2 != null) gl.uniform1f(l2, time);
					var l3 = gl.getUniformLocation(mProgram, "iResolution"); if (l3 != null) gl.uniform3f(l3, xres, yres, 1.0f);
					var l4 = gl.getUniformLocation(mProgram, "iMouse"); if (l4 != null) gl.uniform4fv(l4, mouse);
					//var l7 = gl.getUniformLocation(this.mProgram, "iDate"); if (l7 != null) gl.uniform4fv(l7, dates);
					//var l9 = gl.getUniformLocation(this.mProgram, "iSampleRate"); if (l9 != null) gl.uniform1f(l9, this.mSampleRate);

					// i wonder, does shader toy allow sending render targets as a feedback loop?
					// could we compute the average 1px value over 1000 frames in a shader?
					var ich0 = gl.getUniformLocation(mProgram, "iChannel0"); if (ich0 != null) gl.uniform1i(ich0, 0);
					var ich1 = gl.getUniformLocation(mProgram, "iChannel1"); if (ich1 != null) gl.uniform1i(ich1, 1);
					var ich2 = gl.getUniformLocation(mProgram, "iChannel2"); if (ich2 != null) gl.uniform1i(ich2, 2);
					var ich3 = gl.getUniformLocation(mProgram, "iChannel3"); if (ich3 != null) gl.uniform1i(ich3, 3);


					// what if there are other textures too?
					// X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeWebGLFrameBuffer\ChromeWebGLFrameBuffer\Application.cs

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




					// using ?
					var vec2pos = (uint)gl.getAttribLocation(mProgram, "pos");
					//gl.bindBuffer(gl.ARRAY_BUFFER, quadVBO);


					#region vertices
					float left = -1.0f;
					// y reversed?
					float bottom = -1.0f;
					float right = 1.0f;
					float top = 1.0f;

					var fvertices =
						new float[]
						{
							// left top
							left, bottom,

							// right top
							//right, -1.0f,
							right, bottom,

							// left bottom
							left, top,

							// right top
							//right, -1.0f,
							right, bottom,

							// right bottom
							//right, 1.0f,
							right, top,

							// left bottom
							left,top
						};

					var vertices = new Float32Array(fvertices);
					#endregion

					gl.bindBuffer(gl.ARRAY_BUFFER, verticesBuffer);
					gl.bufferData(gl.ARRAY_BUFFER, vertices, gl.STATIC_DRAW);

					gl.vertexAttribPointer(vec2pos, 2, gl.FLOAT, false, 0, 0);
					gl.enableVertexAttribArray(vec2pos);

					// GL ERROR :GL_INVALID_OPERATION : glDrawArrays: attempt to render with no buffer attached to enabled attribute 1
					gl.drawArrays(gl.TRIANGLES, 0, 6);


					// first frame is now visible
					gl.disableVertexAttribArray(vec2pos);
					gl.bindBuffer(gl.ARRAY_BUFFER, null);
					#endregion

					//mFrame++;

				};
				#endregion

				Paint_Image(
					sw.ElapsedMilliseconds / 1000.0f,

					0,
					0,
					0,
					0


				);

				gl.flush();

				//// INVALID_OPERATION: generateMipmap: level 0 not power of 2 or not all the same size
				gl.bindTexture(gl.TEXTURE_2D, xpass0.xWebGLTexture0);
				gl.generateMipmap(gl.TEXTURE_2D);
				gl.bindTexture(gl.TEXTURE_2D, null);

				gl.bindFramebuffer(gl.FRAMEBUFFER, null);

				return xpass0;
			};
			#endregion


			#region drawArrays
			Action<ShaderToy.EffectPass, float, float, float> drawArrays = (xpass0, x, y, zz) =>
			{
				// using has a spevial meaning here
				//using (var u = new ChromeWebGLFrameBufferToSquare.Shaders.__GeometryVertexShader())
				//{
				//	// should jsc implement structs as BufferData so we could send them over?
				//	u.pMatrix = pMatrix;
				//	u.uMVMatrix = mvMatrix;
				//}

				// or are we already using this program? should we skip then?
				gl.useProgram(shaderProgram);


				var mvMatrix = glMatrix.mat4.create();
				var pMatrix = glMatrix.mat4.create();

				// https://developer.tizen.org/dev-guide/2.2.1/org.tizen.web.appprogramming/html/tutorials/suppl_tutorial/creating_3d_perspective.htm
				//glMatrix.mat4.perspective(45f, (float)gl.canvas.aspect, 0.1f, 120.0f, pMatrix);
				glMatrix.mat4.perspective(90f, (float)gl.canvas.aspect, 0.1f, 120.0f, pMatrix);
				gl.uniformMatrix4fv(gl.getUniformLocation(shaderProgram, "uPMatrix"), false, pMatrix);


				glMatrix.mat4.identity(mvMatrix);
				glMatrix.mat4.translate(mvMatrix,
					new float[] {
					x
					//+ (float)Math.Cos(sw.ElapsedMilliseconds  *0.001f ) * 0.1f
					, y,
						//-( rttFramebuffer_height / gl.canvas.width )


						// we see 1 tile
						//-1

						// we see 4 tiles
						//-4

						// jsc should be able to keep scanning the il for small changes
						// and keep talking to live instances for hot patching if possible.
						//-7

						zz
					}
					);
				// if we were to inspect our viewsource would we know if we were to be updated?

				gl.uniformMatrix4fv(gl.getUniformLocation(shaderProgram, "uMVMatrix"), false, mvMatrix);

				// jsc can we get audio comments?

				gl.viewport(0, 0, gl.canvas.width, gl.canvas.height);

				gl.activeTexture(gl.TEXTURE0);
				gl.bindTexture(gl.TEXTURE_2D, xpass0.xWebGLTexture0);
				gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, (int)gl.NEAREST);

				for (int ihalf = 0; ihalf < 2; ihalf++)
				{
					var rsize = 1f;

					// which is top. which is bottom?
					var rtop = -0f;

					// we dont want to cunt from bottom
					var rbottom = -1f;

					#region vec2aTextureCoord
					var vec2aTextureCoord = gl.getAttribLocation(shaderProgram, "aTextureCoord");
					gl.bindBuffer(gl.ARRAY_BUFFER, vec2aTextureCoordBuffer);
					// http://iphonedevelopment.blogspot.com/2009/05/opengl-es-from-ground-up-part-6_25.html
					var textureCoords = new float[]{
						// Front face
						0.0f, -0.5f,
				  0.0f, -1.0f,
				  -1.0f, -1.0f,
				  -1.0f, -0.5f,


				};

					if (ihalf % 2 == 0)
					{
						textureCoords = new[]{
				  0.0f, 0.0f,
				  0.0f, 0.5f,
				  1.0f, 0.5f,
				  1.0f, 0.0f,


					};

					}

					gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(textureCoords), gl.STATIC_DRAW);
					gl.vertexAttribPointer((uint)vec2aTextureCoord, 2, gl.FLOAT, false, 0, 0);
					gl.enableVertexAttribArray((uint)vec2aTextureCoord);


					gl.uniform1i(gl.getUniformLocation(shaderProgram, "uSampler"), 0);
					#endregion


					#region aVertexPosition
					var vec3aVertexPosition = gl.getAttribLocation(shaderProgram, "aVertexPosition");
					gl.bindBuffer(gl.ARRAY_BUFFER, vec3aVertexPositionBuffer);



					#region vec3vertices
					var vec3vertices = new[]{
						rsize,  rtop,  0.0f,
						rsize,   rbottom,  0.0f,
						-rsize, rbottom,  0.0f,

						//-4.0f,  -4.0f,  0.0f,
						//-4,  4f,  0.0f,
						//4.0f, 4.0f,  0.0f,
					};

					if (ihalf % 2 == 0)
					{
						vec3vertices = new[]{


							-rsize, rbottom,  0.0f,
							-rsize,  rtop,  0.0f,
							rsize, rtop,  0.0f,
						};

					}
					#endregion

					gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vec3vertices), gl.STATIC_DRAW);
					gl.vertexAttribPointer((uint)vec3aVertexPosition, 3, gl.FLOAT, false, 0, 0);
					gl.enableVertexAttribArray((uint)vec3aVertexPosition);
					#endregion

					// using has a spevial meaning here
					//using (var u = new ChromeWebGLFrameBufferToSquare.Shaders.__GeometryVertexShader())
					//{
					//	// should jsc implement structs as BufferData so we could send them over?
					//	u.aVertexPosition  = vec3vertices;
					//}


					var vec3vertices_numItems = vec3vertices.Length / 3;
					//gl.drawArrays(gl.TRIANGLE_STRIP, 0, vertices.Length / 3);
					gl.drawArrays(gl.TRIANGLE_STRIP, 0, vec3vertices_numItems);

				}

				gl.bindTexture(gl.TEXTURE_2D, null);

			};
			#endregion

			// some programs are 2d, some support setting the camera.
			// this would work if the shader knows how to play well with the camera projection
			// http://http.developer.nvidia.com/GPUGems3/gpugems3_ch30.html
			// volumeteric shaders for vr!
			// http://forum.unity3d.com/threads/volume-shader.47188/
			// we would need depth info to order such programs tho
			// and by rotating the scene we need to recalculate visible sub programs
			// and ahw we want from them
			// we could event show "not responding" for slow programs
			// essentially bounding box intel will be of interest.
			// isnt it how the impossible structures can be made, the portal effect?

			// google images search
			// or actually. we we could just select a sub tile to be rendered such a program?
			// what if the sub programs have the base data? would be hard to merge with other effects, but would work for layer1 effects


			// select the shader, program, texture, framebuffer




			// media rss cooliris
			//var rows = 8;
			//var columns = 12;

			var rows = 9;
			var columns = 14;

			// tested on ipad! 8
			// make it an async list?
			// add until fps low?

			// first load ready to go
			var loadDelay = new TaskCompletionSource<object>();
			loadDelay.SetResult(null);

			var loadCount = 0;
			//var loadTotal = new TimeSpan();
			// async init missing?

			var loadTotal = TimeSpan.FromMilliseconds(0);

			//var status = new IHTMLPre { () => new { mMouseOriX, mMouseOriY, mMousePosX, mMousePosY, loadCount, loadTotal } }.AttachToDocument();
			var status = new IHTMLPre { () => new { loadCount, loadTotal } }.AttachToDocument();

			// 2 min load?

			new IStyle(status)
			{
				position = IStyle.PositionEnum.@fixed,

				right = "0px",
				bottom = "0px",

			};

			#region xtimeestimate
			var xtimeestimate = new IHTMLDiv().AttachToDocument().With(
				async timeestimate =>
				{
					var s = new IStyle(timeestimate)
					{
						backgroundColor = "red",

						position = IStyle.PositionEnum.@fixed,

						height = "2px",
						//top = "0px",
						bottom = "0px",
						left = "0px",
					};

					s.width = "0%";
					//await Native.window.async.onframe;
					//s.transition = "width 120ms linear";
					//await Native.window.async.onframe;
					//s.width = "100%";
				}
			);
			#endregion




			Native.document.body.onselectstart +=
				e => e.preventDefault();

			var zmax = -40f;
			var zmin = -1f;
			var z = -15f;

			#region onmousewheel
			c.onmousewheel +=
				e =>
				{
					//camera.position.z = 1.5;

					// min max. shall adjust speed also!
					// max 4.0
					// min 0.6
					z += 1.6f * e.WheelDirection;


					z = z.Max(-zmax).Min(zmin);

					//Native.document.title = new { camera.position.z }.ToString();

				};
			#endregion


			var simpleLoader = default(ShaderToy.EffectPass);

			var fragsCount = rows * columns;
			var frags = Enumerable.ToArray(
				from key in ChromeShaderToyPrograms.References.programs.Keys.Take(fragsCount)

					//await
				select new { }.WithAsync(
					async delegate
					{
						var oldloadDelay = loadDelay;
						var newloadDelay = new TaskCompletionSource<object>();
						loadDelay = newloadDelay;

						await oldloadDelay.Task;

						var i = loadCount;

						//var text = (1 + index) + " of " + References.programs.Count + " " + key.SkipUntilIfAny("ChromeShaderToy").Replace("By", " by ");
						//var text = key.SkipUntilIfAny("ChromeShaderToy").Replace("By", " by ");
						var text = key.SkipUntilIfAny("ChromeShaderToy").TakeUntilIfAny("By");

						var title = new IHTMLPre { i + " " + text + " (loading)" }.AttachToDocument();
						//Native.document.body.ScrollToBottom();
						// tested by?
						//Native.document.documentElement.ScrollToBottom();
						//Native.document.documentElement.style.overflow = IStyle.OverflowEnum.auto;

						//var loadpercentage = (int)((100.0 * (i + 1)) / fragsCount);
						//Native.document.title = text + $" ({loadpercentage}%)";

						//Native.document.body.style.backgroundColor = "cyan";
						//await Task.Delay(2000);


						// entering a blocking api...
						//Native.document.body.style.backgroundColor = "red";
						Native.document.body.style.borderBottom = "1em solid red";
						// remote desktop takes a frame to show
						//await Task.Delay(300);
						//await Native.window.async.onframe;
						await Native.window.async.onframe;
						// red and title visible?

						// did we loose context?
						if (gl == null)
							await new TaskCompletionSource<object>().Task;


						var pass = default(ShaderToy.EffectPass);

						loadCount++;

						var blockingCall = Stopwatch.StartNew();
						if (simpleLoader == null)
						{

							var ctor = ChromeShaderToyPrograms.References.programs[key];
							var frag = ctor();
							pass = newPass(frag);
							blockingCall.Stop();
							loadTotal += blockingCall.Elapsed;


							// we now have a loader shader
							// this can always be activated
							// and copy of it can be used by any other unloaded tile
							simpleLoader = pass;
						}
						else
						{
							// we are skiping the blocking call until being gazed at
							blockingCall.Stop();
						}

						// branch off, yet return early
						//new { }.With(
						//	async delegate
						//	{

						// cool off
						//Native.document.body.style.backgroundColor = "cyan";
						Native.document.body.style.borderBottom = "0em solid red";

						title.innerText = i + " " + text + " " + blockingCall.ElapsedMilliseconds + $"ms ";
						Native.document.title = title.innerText;

						//await Native.window.async.onframe;
						//await Task.Delay(2000);

						//// done?
						//Native.document.body.style.backgroundColor = "yellow";
						//Native.document.title = $"({loadCount}) total {loadTotal.TotalMilliseconds}ms";

						//await Task.Delay(2000);

						// moveNext

						//xtimeestimate.style.width = loadpercentage + "%";

						// load first only?=
						// continue unloaded?
						newloadDelay.SetResult(null);

						var x = (1) + (2.2f) * ((i / rows) - (fragsCount / rows) / 2);

						//drawArrays(paintToTex(f), x, -2f, -15f);


						//var y = x % 2;
						//var y = (i % rows - rows / 2 + 0.5f) * 2.0f;
						var y = (i % rows - rows / 2 + 0.5f) * 1.2f;

						var paintToTexElapsed = 0L;
						var paintToTexCount = 0;

						var gazeStopwatch = Stopwatch.StartNew();

						gazeStopwatch.Stop();

						do
						{
							// only draw the ones loaded

							var cx = x + (mMousePosX - Math.Abs(mMouseOriX)) * 0.01f - 1.0f;
							var cy = y + -(mMousePosY - Math.Abs(mMouseOriY)) * 0.01f - 1.0f;

							var sx = 0;
							var sy = 0;
							//if (cx < 0) sx = Math.Sign(Math.Ceiling(cx)); else

							sx = Math.Sign(Math.Floor(cx));
							//if (cy < 0) sy = Math.Sign(Math.Ceiling(cy)); else
							sy = Math.Sign(Math.Floor(cy));

							if (Math.Floor(cx) == 1) sx = 0;
							if (Math.Floor(cy) == 1) sy = 0;

							// how far are we from the gaze?
							var len = (float)Math.Sqrt(cx * cx + cy * cy);

							//var isGazedAt = sx == 0 && sy == 0;
							var isGazedAt = false;

							gazeStopwatch.Stop();

							//if (sx == 0)
							//	if (sy == 0)

							if (len < 2)
							{
								isGazedAt = true;

								gazeStopwatch.Start();

								// lets make sure we are loaded..
								if (pass == null)
									if (gazeStopwatch.ElapsedMilliseconds > 1000)
									{
										// perhaps lets keep a timeout?
										title.style.backgroundColor = "red";
										await Native.window.async.onframe;

										blockingCall = Stopwatch.StartNew();
										var ctor = ChromeShaderToyPrograms.References.programs[key];
										var frag = ctor();
										pass = newPass(frag);
										blockingCall.Stop();
										loadTotal += blockingCall.Elapsed;

										title.style.backgroundColor = "";
										await Native.window.async.onframe;
									}
							}

							//if ()

							// we do want the first frame!
							//if (paintToTexCount == 0)
							//	isGazedAt = true;
							if (pass == simpleLoader)
								isGazedAt = true;

							if (isGazedAt)
							{
								if (pass != null)
								{
									// if we are actually loaded, we can render..

									paintToTexCount++;

									var paintToTexElapsedStopwatch = Stopwatch.StartNew();
									paintToTex(pass);
									paintToTexElapsed = paintToTexElapsedStopwatch.ElapsedMilliseconds;
								}
							}




							var drawArraysStopwatch = Stopwatch.StartNew();

							// cull things too far away?
							//var zlen = (float)(z - Math.Sin(len));

							var tlen = Math.Pow(len / 4.0, 2);

							var zlen = (float)(z - tlen);

							//if (z == zmin)
							if (pass == null)
							{
								// we are unloaded...
								drawArrays(
									simpleLoader, cx, cy, zlen
								);
							}
							else
							{
								drawArrays(
										pass,
										// neg mMouseOriX means mouse released
										//cx + sx * 0.2f,
										//cy + sy * 0.2f,
										cx, cy,
										zlen
									);
							}

							drawArraysStopwatch.Stop();

							//title.innerText = i + " " + text + " " + blockingCall.ElapsedMilliseconds + $"ms " + new { sx, cx, sy, cy };
							//title.innerText = i + " " + text + " " + blockingCall.ElapsedMilliseconds + $"ms " + new { len } + " tex " + paintToTexElapsed + "ms draw " + drawArraysStopwatch.ElapsedMilliseconds + "ms";
							//if (isGazedAt)

							//title.innerText = i + " " + text + " " + blockingCall.ElapsedMilliseconds + $"ms " + " tex " + paintToTexElapsed + "ms draw " + drawArraysStopwatch.ElapsedMilliseconds + "ms";
							//title.innerText = i + " " + text + " " + blockingCall.ElapsedMilliseconds + $"ms " + new { pass, simpleLoader };


							if (pass == null)
								title.innerText = i + " " + text + " " + blockingCall.ElapsedMilliseconds + $"ms " + gazeStopwatch.ElapsedMilliseconds + "ms gaze ";
							else
								title.innerText = i + " " + text + " " + blockingCall.ElapsedMilliseconds + $"ms " + new { zlen };
							//z,
							//zlen,
							//tlen
							;

							// how long have we been gazed at or ungazed at?
							if (isGazedAt)
								title.style.borderLeft = "1em solid yellow";
							else
								title.style.borderLeft = "0em solid yellow";

						}
						while (await Native.window.async.onframe);
						//	}
						//);


						// return early, result not used?
						return new { };
					}
				)
			);





		}


	}

	public static class x
	{

		public static Task<TResult> WithAsync<T, TResult>(this T that, Func<T, Task<TResult>> select)
		{
			return select(that);
		}
	}

}
