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

		public static WebGLBuffer createQuadVBO(gl gl)
		{
			//new IHTMLPre { "enter createQuadVBO" }.AttachToDocument();

			var vertices = new Float32Array(new float[]
				{ -1.0f, -1.0f, 1.0f, -1.0f, -1.0f, 1.0f, 1.0f, -1.0f, 1.0f, 1.0f, -1.0f, 1.0f }
				);

			var vbo = gl.createBuffer();
			gl.bindBuffer(gl.ARRAY_BUFFER, vbo);
			gl.bufferData(gl.ARRAY_BUFFER, vertices, gl.STATIC_DRAW);
			gl.bindBuffer(gl.ARRAY_BUFFER, null);

			return vbo;
		}

		public class samplerCube
		{

		}

		class CreateShaderResult
		{
			public string mInfo;
			public bool mSuccess;

			public WebGLProgram mProgram;
		}

		static CreateShaderResult CreateShader(
			WebGLRenderingContext gl,
			string tvs,
			string tfs,
			bool nativeDebug
			)
		{
			new IHTMLPre { "enter CreateShader" }.AttachToDocument();

			//new WebGLProgram(gl);
			var p = gl.createProgram();

			var vs = gl.createShader(gl.VERTEX_SHADER);
			var fs = gl.createShader(gl.FRAGMENT_SHADER);

			gl.shaderSource(vs, tvs);
			gl.shaderSource(fs, tfs);

			gl.compileShader(vs);
			gl.compileShader(fs);

			if (gl.getShaderParameter(vs, gl.COMPILE_STATUS) == null)
			{
				var infoLog = gl.getShaderInfoLog(vs);
				gl.deleteProgram(p);
				new IHTMLPre { "error CreateShader " + new { infoLog } }.AttachToDocument();
				return new CreateShaderResult { mSuccess = false, mInfo = infoLog };
			}

			if (gl.getShaderParameter(fs, gl.COMPILE_STATUS) == null)
			{
				var infoLog = gl.getShaderInfoLog(fs);
				gl.deleteProgram(p);
				new IHTMLPre { "error CreateShader " + new { infoLog } }.AttachToDocument();
				return new CreateShaderResult { mSuccess = false, mInfo = infoLog };
			}

			if (nativeDebug)
			{
				var dbgext = gl.getExtension("WEBGL_debug_shaders");
				if (dbgext != null)
				{
					//var hlsl = dbgext.getTranslatedShaderSource(fs);
					//console.log("------------------------\nHLSL code\n------------------------\n" + hlsl + "\n------------------------\n");
				}
			}

			gl.attachShader(p, vs);
			gl.attachShader(p, fs);

			// using dispose?
			gl.deleteShader(vs);
			gl.deleteShader(fs);

			gl.linkProgram(p);

			if (gl.getProgramParameter(p, gl.LINK_STATUS) == null)
			{
				var infoLog = gl.getProgramInfoLog(p);
				gl.deleteProgram(p);
				new IHTMLPre { "error CreateShader " + new { infoLog } }.AttachToDocument();
				return new CreateShaderResult { mSuccess = false, mInfo = infoLog };
			}

			// https://msdn.microsoft.com/en-us/library/ie/dn302415(v=vs.85).aspx
			new IHTMLPre { "exit CreateShader" }.AttachToDocument();

			return new CreateShaderResult { mSuccess = true, mProgram = p };
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
				float mousePosY

				);
			public Paint_ImageDelegate Paint_Image;


			public EffectPass(
				AudioContext wa,
				gl gl,

				string precission,
				bool supportDerivatives,
				RefreshTexturThumbailDelegate callback,
				object obj,
				bool forceMuted,
				bool forcePaused,

				// ARRAY_BUFFER
				WebGLBuffer quadVBO,
				GainNode outputGainNode
				)
			{
				//new IHTMLPre { "enter EffectPass" }.AttachToDocument();


				// used by?
				var mFrame = 0;

				this.MakeHeader_Image = delegate
				{
					#region MakeHeader_Image
					new IHTMLPre { "enter MakeHeader_Image" }.AttachToDocument();


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
							new IHTMLPre { "add MakeHeader_Image sampler2D" }.AttachToDocument();
							header += "uniform sampler2D iChannel" + i + ";\n";
						}

						headerlength++;
					}

					var mImagePassFooter = "\nvoid main( void )" +
					"{" +
						//"vec4 color[4];" +
						//"mainImage( color[0], gl_FragCoord.xy );" +
						//"gl_FragColor = color[0];" +
						"vec4 color;" +
						"mainImage( color, gl_FragCoord.xy );" +
						"color.w = 1.0;" +
						"gl_FragColor = color;" +
					"}";
					#endregion


					this.NewShader_Image = (fs) =>
					{
						#region NewShader_Image
						new IHTMLPre { "enter NewShader_Image" }.AttachToDocument();
						var shaderCode = fs.ToString();

						var vsSource = "attribute vec2 pos; void main() { gl_Position = vec4(pos.xy,0.0,1.0); }";
						var res = CreateShader(gl, vsSource, header + shaderCode + mImagePassFooter, false);

						var mProgram = res.mProgram;
						#endregion


						#region calledby
						//EffectPass.Paint_Image(effect.js:724)
						//EffectPass.Paint(effect.js:1038)
						//Effect.Paint(effect.js:1247)
						//renderLoop2(pgWatch.js:404)
						//ShaderToy.startRendering(pgWatch.js:420)
						//watchInit(pgWatch.js:1386)
						//onload(Xls3WS: 78)
						#endregion
						this.Paint_Image = (time, mouseOriX, mouseOriY, mousePosX, mousePosY) =>
						{
							var xres = gl.canvas.width;
							var yres = gl.canvas.height;

							#region Paint_Image
							//new IHTMLPre { "enter Paint_Image" }.AttachToDocument();


							gl.viewport(0, 0, xres, yres);

							// useProgram: program not valid
							gl.useProgram(mProgram);

							// uniform4fv
							var mouse = new[] { mousePosX, mousePosY, mouseOriX, mouseOriY };

							var l2 = gl.getUniformLocation(mProgram, "iGlobalTime"); if (l2 != null) gl.uniform1f(l2, time);
							var l3 = gl.getUniformLocation(mProgram, "iResolution"); if (l3 != null) gl.uniform3f(l3, xres, yres, 1.0f);
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


							for (var i = 0; i < mInputs.Length; i++)
							{
								var inp = mInputs[i];

								gl.activeTexture((uint)(gl.TEXTURE0 + i));

								if (inp == null)
								{
									gl.bindTexture(gl.TEXTURE_2D, null);
								}
							}

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

				this.mGainNode = ac.createGain();
				this.mGainNode.connect(ac.destination);

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
	}
}
