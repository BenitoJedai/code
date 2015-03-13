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
			Native.body.style.backgroundColor = "yellow";
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




			//var xWebGLRenderbuffer0size = 128;
			//var xWebGLRenderbuffer0size = 64;
			//var xWebGLRenderbuffer0size = 16;
			//var xWebGLRenderbuffer0size = 8;
			var xWebGLRenderbuffer0size = 4;



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
					var yres = xWebGLRenderbuffer0size;

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
			Action<ShaderToy.EffectPass, float, float, float> drawArrays = (xpass0, x, y, z) =>
			{
				// using has a spevial meaning here
				//using (var u = new ChromeWebGLFrameBufferToSquare.Shaders.__GeometryVertexShader())
				//{
				//	// should jsc implement structs as BufferData so we could send them over?
				//	u.pMatrix = pMatrix;
				//	u.uMVMatrix = mvMatrix;
				//}


				gl.useProgram(shaderProgram);


				var mvMatrix = glMatrix.mat4.create();
				var pMatrix = glMatrix.mat4.create();


				glMatrix.mat4.perspective(45f, (float)gl.canvas.aspect, 0.1f, 120.0f, pMatrix);
				gl.uniformMatrix4fv(gl.getUniformLocation(shaderProgram, "uPMatrix"), false, pMatrix);


				glMatrix.mat4.identity(mvMatrix);
				glMatrix.mat4.translate(mvMatrix,
					new float[] {
					x
					+ (float)Math.Cos(sw.ElapsedMilliseconds  *0.001f ) * 0.1f
					, y,
						//-( rttFramebuffer_height / gl.canvas.width )


						// we see 1 tile
						//-1

						// we see 4 tiles
						//-4

						// jsc should be able to keep scanning the il for small changes
						// and keep talking to live instances for hot patching if possible.
						//-7

						z
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


					gl.uniform1i(gl.getUniformLocation(shaderProgram, "uSampler"), 0);
					#endregion


					#region aVertexPosition
					var vec3aVertexPosition = gl.getAttribLocation(shaderProgram, "aVertexPosition");
					gl.bindBuffer(gl.ARRAY_BUFFER, vec3aVertexPositionBuffer);

					var rsize = 1f;

					#region vec3vertices
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

			//var frags = new[] {
			//	newPass(new ChromeShaderToyColumns.Shaders.ProgramFragmentShader()),
			//	newPass(new ChromeShaderToyTriangleDistanceByIq.Shaders.ProgramFragmentShader()),
			//	newPass(new ChromeShaderToySphereAndWalls.Shaders.ProgramFragmentShader()),
			//	newPass(new ChromeShaderToyPlasmaTriangleByElusivePete.Shaders.ProgramFragmentShader()),
			//};

			var rows = 4;

			// tested on ipad! 8
			// make it an async list?
			// add until fps low?

			// first load ready to go
			var loadDelay = new TaskCompletionSource<object>();
			loadDelay.SetResult(null);

			var frags = Enumerable.ToArray(
				from key in ChromeShaderToyPrograms.References.programs.Keys.Take(rows * 4)

					//await
				select new { }.WithAsync(
					async delegate
					{
						var oldloadDelay = loadDelay;
						var newloadDelay = new TaskCompletionSource<object>();
						loadDelay = newloadDelay;

						await oldloadDelay.Task;

						//var text = (1 + index) + " of " + References.programs.Count + " " + key.SkipUntilIfAny("ChromeShaderToy").Replace("By", " by ");
						var text = key.SkipUntilIfAny("ChromeShaderToy").Replace("By", " by ");
						Native.document.title = text;
						Native.document.body.style.backgroundColor = "cyan";
						await Task.Delay(2000);


						// entering a blocking api...
						Native.document.body.style.backgroundColor = "red";
						// remote desktop takes a frame to show
						await Task.Delay(300);
						//await Native.window.async.onframe;
						//await Native.window.async.onframe;
						// red and title visible?

						var blockingCall = Stopwatch.StartNew();
						var ctor = ChromeShaderToyPrograms.References.programs[key];
						var frag = ctor();
						var pass = newPass(frag);
						blockingCall.Stop();

						// cool off
						Native.document.body.style.backgroundColor = "cyan";
						Native.document.title = text + " " + blockingCall.ElapsedMilliseconds + "ms";
						await Native.window.async.onframe;
						await Task.Delay(2000);

						// done?
						Native.document.body.style.backgroundColor = "yellow";
						Native.document.title = "...";

						await Task.Delay(300);

						// moveNext
						newloadDelay.SetResult(null);

						return new { key, pass };
					}
				)
			);



			Native.window.onframe += e =>
			{
				// GL_INVALID_OPERATION : glDrawArrays: Source and destination textures of the draw are the same.


				frags.WithEachIndex(
					(f, i) =>
					{

						var x = (1) + (2.0f) * ((i / rows) - (frags.Length / rows) / 2);

						//drawArrays(paintToTex(f), x, -2f, -15f);


						//var y = x % 2;
						var y = (i % rows - rows / 2 + 0.5f) * 2.0f;


						if (f.IsCompleted)
						{
							// only draw the ones loaded

							drawArrays(
								paintToTex(f.Result.pass),
								x,
								y,
								-15f
							);
						}

					}
				);

				// when can we do 3d shaders?
				//drawArrays(paintToTex(frag2), -3, -1f);

				//drawArrays(paintToTex(frag1), -3, 1f);

				//drawArrays(paintToTex(frag3), -1, 1f);
				//drawArrays(paintToTex(frag0), -1, -1f);
				//drawArrays(paintToTex(frag1), 1, 1f);
				//drawArrays(paintToTex(frag3), 1, -1f);

				//drawArrays(paintToTex(frag0), 3, -1f);

				//drawArrays(paintToTex(frag2), 3, 1f);
			};


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
