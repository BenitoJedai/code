﻿using ScriptCoreLib;
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

namespace ChromeShaderToyColumns.Library
{
	using ScriptCoreLib.GLSL;
	using System.Diagnostics;
	using gl = WebGLRenderingContext;

	//		Create Partial Type: ChromeShaderToyColumns.Application+RefreshTexturThumbailDelegate
	//0930:02:01 RewriteToAssembly error: System.NotSupportedException: Parent does not have a default constructor.The default constructor must be explicitly defined.

	public delegate void RefreshTexturThumbailDelegate(
		object myself,
		int slot,
		object img,
		bool forceFrame,
		bool gui,
		int guiID,
		int time
		);


	public class ShaderToy
	{
		// X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeShaderToySeascapeByTDM\ChromeShaderToySeascapeByTDM\Application.cs

		public static WebGLBuffer createQuadVBO(gl gl

			, float left = -1.0f
			// y reversed?
			, float bottom = -1.0f
			, float right = 1.0f
			, float top = 1.0f
			)
		{
			// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150311

			//enter createQuadVBO { { i = 0, value = null } }
			//enter createQuadVBO { { i = 1, value = null } }
			//enter createQuadVBO { { i = 2, value = 1 } }
			//enter createQuadVBO { { i = 3, value = null } }
			//enter createQuadVBO { { i = 4, value = null } }
			//enter createQuadVBO { { i = 5, value = null } }
			//enter createQuadVBO { { i = 6, value = 1 } }
			//enter createQuadVBO { { i = 7, value = null } }
			//enter createQuadVBO { { i = 8, value = 1 } }
			//enter createQuadVBO { { i = 9, value = null } }
			//enter createQuadVBO { { i = 10, value = null } }
			//enter createQuadVBO { { i = 11, value = null } }

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

			//new IHTMLPre { "enter createQuadVBO" }.AttachToDocument();
			//for (int i = 0; i < fvertices.Length; i++)
			//{
			//	var value = fvertices[i];

			//	new IHTMLPre { "enter createQuadVBO " + new { i, value } }.AttachToDocument();
			//}

			// new Buffer?
			var vbo = new WebGLBuffer(gl);
			//var vbo = gl.createBuffer();
			gl.bindBuffer(gl.ARRAY_BUFFER, vbo);
			var vertices = new Float32Array(fvertices);
			gl.bufferData(gl.ARRAY_BUFFER, vertices, gl.STATIC_DRAW);
			gl.bindBuffer(gl.ARRAY_BUFFER, null);

			return vbo;
		}

		public class samplerCube
		{

		}

		public class CreateShaderResult
		{
			public string mInfo;
			public bool mSuccess;

			public WebGLProgram mProgram;

			public string vsTranslatedShaderSource;
			public string fsTranslatedShaderSource;
		}

		static CreateShaderResult CreateShader(
			WebGLRenderingContext gl,
			string tvs,
			string tfs,
			bool nativeDebug
			)
		{
			//new IHTMLPre { "enter CreateShader" }.AttachToDocument();

			//var p = gl.createProgram();

			var vs = new WebGLShader(gl, gl.VERTEX_SHADER);
			var fs = new WebGLShader(gl, gl.FRAGMENT_SHADER);

			gl.shaderSource(vs, tvs);
			gl.shaderSource(fs, tfs);

			gl.compileShader(vs);

			var ok = new CreateShaderResult { mSuccess = true };

			// https://www.khronos.org/registry/webgl/extensions/WEBGL_debug_shaders/
			new WebGLDebugShaders(gl).With(x => ok.vsTranslatedShaderSource = x.getTranslatedShaderSource(vs));

			//gl.getExtension("WEBGL_debug_shaders").With(
			//(dynamic WEBGL_debug_shaders) =>
			//	{
			//		ok.vsTranslatedShaderSource = WEBGL_debug_shaders.getTranslatedShaderSource((WebGLShader)vs);
			//	}
			//);

			gl.compileShader(fs);

			// ipad wont have it available
			new WebGLDebugShaders(gl).With(x => ok.fsTranslatedShaderSource = x.getTranslatedShaderSource(fs));

			//gl.getExtension("WEBGL_debug_shaders").With(
			//	(dynamic WEBGL_debug_shaders) =>
			//	{
			//		ok.fsTranslatedShaderSource = WEBGL_debug_shaders.getTranslatedShaderSource((WebGLShader)fs);
			//	}
			//);

			if (gl.getShaderParameter(vs, gl.COMPILE_STATUS) == null)
			{
				var infoLog = gl.getShaderInfoLog(vs);
				new IHTMLPre { "error CreateShader " + new { infoLog } }.AttachToDocument();
				return new CreateShaderResult { mSuccess = false, mInfo = infoLog };
			}

			var fsCOMPILE_STATUS = (bool)gl.getShaderParameter(fs, gl.COMPILE_STATUS);
			//new IHTMLPre { "CreateShader " + new { fsCOMPILE_STATUS } }.AttachToDocument();

			if (!fsCOMPILE_STATUS)
			{
				var infoLog = gl.getShaderInfoLog(fs);
				new IHTMLPre { "error CreateShader " + new { infoLog } }.AttachToDocument();
				return new CreateShaderResult { mSuccess = false, mInfo = infoLog };
			}


			var p = new WebGLProgram(gl);
			gl.attachShader(p, vs);
			gl.attachShader(p, fs);

			// using dispose?
			gl.deleteShader(vs);
			gl.deleteShader(fs);

			gl.linkProgram(p);

			var linkResult = (bool)gl.getProgramParameter(p, gl.LINK_STATUS);

			//new IHTMLPre { "CreateShader " + new { linkResult } }.AttachToDocument();

			if (!linkResult)
			{
				var infoLog = gl.getProgramInfoLog(p);
				gl.deleteProgram(p);
				new IHTMLPre { "error CreateShader " + new { infoLog } }.AttachToDocument();
				return new CreateShaderResult { mSuccess = false, mInfo = infoLog };
			}

			// https://msdn.microsoft.com/en-us/library/ie/dn302415(v=vs.85).aspx
			//new IHTMLPre { "exit CreateShader" }.AttachToDocument();

			ok.mProgram = p;
			return ok;
		}

		public static string DetermineShaderPrecission(WebGLRenderingContext gl)
		{
			//new IHTMLPre { "enter DetermineShaderPrecission" }.AttachToDocument();

			var h1 = "#ifdef GL_ES\n" +
					 "precision highp float;\n" +
					 "#endif\n";

			var h2 = "#ifdef GL_ES\n" +
					 "precision mediump float;\n" +
					 "#endif\n";

			var h3 = "#ifdef GL_ES\n" +
					 "precision lowp float;\n" +
					 "#endif\n";

			var vstr = "void main() { gl_Position = vec4(1.0); }\n";
			var fstr = "void main() { gl_FragColor = vec4(1.0); }\n";

			if (CreateShader(gl, vstr, h1 + fstr, false).mSuccess == true) return h1;
			if (CreateShader(gl, vstr, h2 + fstr, false).mSuccess == true) return h2;
			if (CreateShader(gl, vstr, h3 + fstr, false).mSuccess == true) return h3;

			return "";
		}


		public class EffectPass
		{
			public WebGLTexture xWebGLTexture0;
			public WebGLFramebuffer xWebGLFramebuffer0;

			public object[] mInputs = new object[4];

			public Action MakeHeader_Image;

			public Action<FragmentShader> NewShader_Image;


			public delegate void Paint_ImageDelegate(


				// uniform1f
				float time,

				// uniform4fv
				float mouseOriX,
				float mouseOriY,
				float mousePosX,
				float mousePosY,

				float zoom = 1.0f
				);
			public Paint_ImageDelegate Paint_Image;

			public CreateShaderResult xCreateShader;

			// X:\jsc.svn\examples\glsl\future\GLSLShaderToyPip\GLSLShaderToyPip\Application.cs
			public EffectPass(
				AudioContext wa = null,
				gl gl = null,

				string precission = null,
				bool supportDerivatives = false,
				RefreshTexturThumbailDelegate callback = null,
				object obj = null,
				bool forceMuted = false,
				bool forcePaused = false,


				// createQuadVBO
				// ARRAY_BUFFER
				WebGLBuffer quadVBO = null,
				GainNode outputGainNode = null
				)
			{
				//new IHTMLPre { "enter EffectPass" }.AttachToDocument();


				// used by?
				var mFrame = 0;

				this.MakeHeader_Image = delegate
				{
					#region MakeHeader_Image
					//new IHTMLPre { "enter MakeHeader_Image" }.AttachToDocument();


					var header = precission;
					var headerlength = 3;

					if (supportDerivatives) { header += "#extension GL_OES_standard_derivatives : enable\n"; headerlength++; }

					header += "uniform vec3      iResolution;\n" +
							  "uniform float     iGlobalTime;\n" +
							  "uniform float     iChannelTime[4];\n" +
							  "uniform vec4      iMouse;\n" +
							  "uniform vec4      iDate;\n" +
							  "uniform float     iSampleRate;\n" +
							  "uniform vec3      iChannelResolution[4];\n";

					// not to be used by the hosted shader, but by our code in the middle on the gpu.
					// gpu code injection. first take.
					//"uniform float     fZoom;\n";

					headerlength += 7;

					for (var i = 0; i < mInputs.Length; i++)
					{
						var inp = mInputs[i];

						// X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeShaderToyVRCardboardGrid\ChromeShaderToyVRCardboardGrid\Application.cs

						//if (inp != null && inp.mInfo.mType == "cubemap")
						if (inp is samplerCube)
						{
							new IHTMLPre { "add MakeHeader_Image samplerCube" }.AttachToDocument();
							header += "uniform samplerCube iChannel" + i + ";\n";
						}
						else
						{
							//new IHTMLPre { "add MakeHeader_Image sampler2D" }.AttachToDocument();
							header += "uniform sampler2D iChannel" + i + ";\n";
						}

						headerlength++;
					}



					// rror CreateShader {{ infoLog = ERROR: 0:250: 'assign' :  l-value required "gl_FragCoord" (can't modify gl_FragCoord)
					//				ERROR: 0:251: 'assign' :  l - value required "gl_FragCoord"(can't modify gl_FragCoord)
					//}}
					// error CreateShader {{ infoLog = ERROR: 0:253: '=' :  cannot convert from 'FragCoord mediump 4-component vector of float' to 'highp 2-component vector of float'

					var mImagePassFooter = @"
void main( void )
{
	vec4 color = gl_FragColor;

	mainImage( color, gl_FragCoord.xy );
					
color.a = 1.0;

	gl_FragColor = color;
}
";
					#endregion


					this.NewShader_Image = (fs) =>
					{
						#region NewShader_Image
						//new IHTMLPre { "enter NewShader_Image" }.AttachToDocument();
						var shaderCode = fs.ToString();

						var vsSource = "attribute vec2 pos; void main() { gl_Position = vec4(pos.xy,0.0,1.0); }";

						var fsSource = header + shaderCode + mImagePassFooter;

						this.xCreateShader = CreateShader(gl, vsSource, fsSource, false);

						#endregion

						var vbo = new WebGLBuffer(gl);



						#region calledby
						//EffectPass.Paint_Image(effect.js:724)
						//EffectPass.Paint(effect.js:1038)
						//Effect.Paint(effect.js:1247)
						//renderLoop2(pgWatch.js:404)
						//ShaderToy.startRendering(pgWatch.js:420)
						//watchInit(pgWatch.js:1386)
						//onload(Xls3WS: 78)
						#endregion
						this.Paint_Image = (time, mouseOriX, mouseOriY, mousePosX, mousePosY, zoom) =>
						{
							var mProgram = xCreateShader.mProgram;


							var xres = gl.canvas.width * zoom;
							var yres = gl.canvas.height * zoom;

							#region Paint_Image

							new IHTMLPre { "enter Paint_Image" }.AttachToDocument();

							// this is enough to do pip to bottom left, no need to adjust vertex positions even?
							gl.viewport(0, 0, (int)xres, (int)yres);

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

							mFrame++;

						};
					};
				};

			}
		}

		public class Effect
		{
			private GainNode mGainNode;
			public EffectPass[] mPasses;
			private WebGLBuffer mQuadVBO;
			private bool mSupportTextureFloat;

			public Effect(
				AudioContext ac,
				WebGLRenderingContext gl,
				RefreshTexturThumbailDelegate callback,
				object obj,
				bool forceMuted,
				bool forcePaused)
			{
				new IHTMLPre { "enter Effect" }.AttachToDocument();

				var ext = gl.getExtension("OES_standard_derivatives");
				var supportsDerivatives = (ext != null);

				//if (supportsDerivatives) gl.hint(ext.FRAGMENT_SHADER_DERIVATIVE_HINT_OES, gl.NICEST);

				var ext2 = gl.getExtension("OES_texture_float");
				this.mSupportTextureFloat = (ext2 != null);

				var precision = DetermineShaderPrecission(gl);

				//this.mGainNode = ac.createGain();
				//this.mGainNode.connect(ac.destination);

				this.mQuadVBO = createQuadVBO(gl);


				this.mPasses = new EffectPass[2];

				for (var i = 0; i < 2; i++)
				{
					this.mPasses[i] = new EffectPass(
						ac,
						gl,
						precision,
						supportsDerivatives,
						callback,
						obj,
						forceMuted,
						forcePaused,
						this.mQuadVBO,
						this.mGainNode
						);
				}
			}


		}

		public static async void AttachToDocument(FragmentShader vs)
		{
			Native.body.style.margin = "0px";


			var mAudioContext = new AudioContext();
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
	}
}
