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
using ChromeShaderToyProgramsAsTiles;
using ChromeShaderToyProgramsAsTiles.Design;
using ChromeShaderToyProgramsAsTiles.HTML.Pages;
using ScriptCoreLib.JavaScript.WebGL;
using ChromeShaderToyPrograms;
using System.Diagnostics;
using ChromeShaderToyColumns.Library;

namespace ChromeShaderToyProgramsAsTiles
{
	using ScriptCoreLib.GLSL;
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
			// https://www.shadertoy.com/view/MsfXDS


			// show shader based on tab selection?



			//Native.document.documentElement.style.overflow = IStyle.OverflowEnum.auto;


			// chrome by default has no scrollbar, bowser does
			Native.document.documentElement.style.overflow = IStyle.OverflowEnum.hidden;
			Native.body.style.margin = "0px";
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




			var gl = new WebGLRenderingContext(alpha: true);

			if (gl == null)
			{

				new IHTMLPre {
					// https://code.google.com/p/chromium/issues/detail?id=294207
					"Rats! WebGL hit a snag.",

					new IHTMLAnchor { href = "about:gpu", innerText = "about:gpu" }
				}.AttachToDocument();
				return;
			}

			// page by page
			var combo = new IHTMLSelect().AttachToDocument();

			combo.style.position = IStyle.PositionEnum.absolute;
			combo.style.left = "0px";
			combo.style.top = "0px";
			//combo.style.right = "0px";
			combo.style.width = "100%";

			combo.style.backgroundColor = "rgba(255,255,255,0.5)";
			//combo.style.backgroundColor = "rgba(255,255,0,0.5)";
			//combo.style.background = "linear-gradient(to bottom, rgba(255,255,255,0.5 0%,rgba(255,255,255,0.0 100%))";
			combo.style.border = "0px solid transparent";
			combo.style.fontSize = "large";
			combo.style.paddingLeft = "1em";
			combo.style.fontFamily = IStyle.FontFamilyEnum.Verdana;
			combo.style.cursor = IStyle.CursorEnum.pointer;





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

			c.onmousewheel += ev =>
			{
				ev.preventDefault();
				ev.stopPropagation();

				mMousePosY += 3 * ev.WheelDirection;
			};

			#endregion




			var rttFramebuffer_width = 128;
			var rttFramebuffer_height = 128;



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

			var mvMatrix = glMatrix.mat4.create();
			var pMatrix = glMatrix.mat4.create();

			var vec3aVertexPositionBuffer = new WebGLBuffer(gl);
			var vec2aTextureCoordBuffer = new WebGLBuffer(gl);

			gl.clearColor(0.0f, 0.0f, 0.0f, 1.0f);
			gl.enable(gl.DEPTH_TEST);

			#region initTextureFramebuffer
			var xWebGLFramebuffer = new WebGLFramebuffer(gl);
			gl.bindFramebuffer(gl.FRAMEBUFFER, xWebGLFramebuffer);
			// generateMipmap: level 0 not power of 2 or not all the same size
			//var rttFramebuffer_width = canvas.width;
			// WebGL: INVALID_OPERATION: generateMipmap: level 0 not power of 2 or not all the same size


			var xWebGLTexture = new WebGLTexture(gl);
			gl.bindTexture(gl.TEXTURE_2D, xWebGLTexture);
			gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, (int)gl.LINEAR);
			gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, (int)gl.LINEAR_MIPMAP_NEAREST);

			gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, rttFramebuffer_width, rttFramebuffer_height, 0, gl.RGBA, gl.UNSIGNED_BYTE, null);

			gl.generateMipmap(gl.TEXTURE_2D);


			var xWebGLRenderbuffer = new WebGLRenderbuffer(gl);
			gl.bindRenderbuffer(gl.RENDERBUFFER, xWebGLRenderbuffer);
			gl.renderbufferStorage(gl.RENDERBUFFER, gl.DEPTH_COMPONENT16, rttFramebuffer_width, rttFramebuffer_height);

			gl.framebufferTexture2D(gl.FRAMEBUFFER, gl.COLOR_ATTACHMENT0, gl.TEXTURE_2D, xWebGLTexture, 0);
			gl.framebufferRenderbuffer(gl.FRAMEBUFFER, gl.DEPTH_ATTACHMENT, gl.RENDERBUFFER, xWebGLRenderbuffer);

			gl.bindTexture(gl.TEXTURE_2D, null);
			gl.bindRenderbuffer(gl.RENDERBUFFER, null);
			gl.bindFramebuffer(gl.FRAMEBUFFER, null);
			#endregion

			var pass = new ChromeShaderToyColumns.Library.ShaderToy.EffectPass(
				gl: gl,
				precission: ChromeShaderToyColumns.Library.ShaderToy.DetermineShaderPrecission(gl),
				supportDerivatives: gl.getExtension("OES_standard_derivatives") != null
			);

			pass.MakeHeader_Image();
			pass.NewShader_Image(
					 new ChromeShaderToyColumns.Shaders.ProgramFragmentShader()
				);
			var sw = Stopwatch.StartNew();
			var vbo = new WebGLBuffer(gl);

			Native.window.onframe += e =>
			{

				#region FRAMEBUFFER
				gl.bindFramebuffer(gl.FRAMEBUFFER, xWebGLFramebuffer);

				//// http://stackoverflow.com/questions/20362023/webgl-why-does-transparent-canvas-show-clearcolor-color-component-when-alpha-is
				gl.clearColor(1, 1, 0, 1.0f);

				gl.viewport(0, 0, rttFramebuffer_width, rttFramebuffer_height);
				gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);


				#region Paint_Image
				ChromeShaderToyColumns.Library.ShaderToy.EffectPass.Paint_ImageDelegate Paint_Image = (time, mouseOriX, mouseOriY, mousePosX, mousePosY, zoom) =>
				{
					var mProgram = pass.xCreateShader.mProgram;


					var xres = rttFramebuffer_width;
					var yres = rttFramebuffer_height;

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
					gl.bindBuffer(gl.ARRAY_BUFFER, vbo);


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
				gl.bindTexture(gl.TEXTURE_2D, xWebGLTexture);
				gl.generateMipmap(gl.TEXTURE_2D);
				gl.bindTexture(gl.TEXTURE_2D, null);

				gl.bindFramebuffer(gl.FRAMEBUFFER, null);
				#endregion

				gl.clearColor(0, 0, 1, 1.0f);
				gl.viewport(0, 0, gl.canvas.width, gl.canvas.height);
				gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

				glMatrix.mat4.perspective(45f, (float)gl.canvas.width / (float)gl.canvas.height, 0.1f, 120.0f, pMatrix);
				glMatrix.mat4.identity(mvMatrix);
				glMatrix.mat4.translate(mvMatrix, new float[] {
					-1.5f + (float)Math.Cos(
						sw.ElapsedMilliseconds
					//slow it down
					*0.001f
)

					, 0.0f, -15.0f });
				//glMatrix.mat4.translate(mvMatrix, new float[] { 3.0f, 0.0f, 0.0f });

				gl.useProgram(shaderProgram);



				// X:\jsc.svn\examples\javascript\WebGL\WebGLLesson05\WebGLLesson05\Application.cs


				gl.uniformMatrix4fv(gl.getUniformLocation(shaderProgram, "uPMatrix"), false, pMatrix);
				gl.uniformMatrix4fv(gl.getUniformLocation(shaderProgram, "uMVMatrix"), false, mvMatrix);



				// GL_INVALID_OPERATION : glDrawArrays: attempt to access out of range vertices in attribute 1

				for (int ihalf = 0; ihalf < 2; ihalf++)
				{

					#region vec2aTextureCoord
					var vec2aTextureCoord = gl.getAttribLocation(shaderProgram, "aTextureCoord");
					gl.bindBuffer(gl.ARRAY_BUFFER, vec2aTextureCoordBuffer);
					// http://iphonedevelopment.blogspot.com/2009/05/opengl-es-from-ground-up-part-6_25.html
					var textureCoords = new float[]{
						// Front face
						0.0f, 0.0f,
				  0.0f, -1.0f,
				  -1.0f, -1.0f,
				  -1.0f, 0.0f,


				};

					if (ihalf % 2 == 0)
					{
						textureCoords = new[]{
	0.0f, 0.0f,
				  0.0f, 1.0f,
				  1.0f, 1.0f,
				  1.0f, 0.0f,


					};

					}

					gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(textureCoords), gl.STATIC_DRAW);
					gl.vertexAttribPointer((uint)vec2aTextureCoord, 2, gl.FLOAT, false, 0, 0);
					gl.enableVertexAttribArray((uint)vec2aTextureCoord);

					gl.activeTexture(gl.TEXTURE0);
					gl.bindTexture(gl.TEXTURE_2D, xWebGLTexture);
					gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, (int)gl.NEAREST);

					gl.uniform1i(gl.getUniformLocation(shaderProgram, "uSampler"), 0);
					#endregion


					#region aVertexPosition
					var vec3aVertexPosition = gl.getAttribLocation(shaderProgram, "aVertexPosition");
					gl.bindBuffer(gl.ARRAY_BUFFER, vec3aVertexPositionBuffer);

					var rsize = 4f;

					var vec3vertices = new[]{
					rsize,  rsize,  0.0f,
					rsize,  -rsize,  0.0f,
					-rsize, -rsize,  0.0f,

						//-4.0f,  -4.0f,  0.0f,
						//-4,  4f,  0.0f,
						//4.0f, 4.0f,  0.0f,
					};

					if (ihalf % 2 == 0)
					{
						vec3vertices = new[]{


						-rsize,  -rsize,  0.0f,
						-rsize,  rsize,  0.0f,
						rsize, rsize,  0.0f,
					};

					}

					gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vec3vertices), gl.STATIC_DRAW);
					gl.vertexAttribPointer((uint)vec3aVertexPosition, 3, gl.FLOAT, false, 0, 0);
					gl.enableVertexAttribArray((uint)vec3aVertexPosition);
					#endregion


					var vec3vertices_numItems = vec3vertices.Length / 3;
					//gl.drawArrays(gl.TRIANGLE_STRIP, 0, vertices.Length / 3);
					gl.drawArrays(gl.TRIANGLE_STRIP, 0, vec3vertices_numItems);

				}

			};


		}

		private Shader GeometryFragmentShader()
		{
			throw new NotImplementedException();
		}
	}
}
