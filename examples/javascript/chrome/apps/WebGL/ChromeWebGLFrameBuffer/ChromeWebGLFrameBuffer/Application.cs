using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.WebGL;
using ChromeWebGLFrameBuffer.HTML.Pages;
using ScriptCoreLib.GLSL;
using System.ComponentModel;


namespace ChromeWebGLFrameBuffer
{
	using System.Diagnostics;
	using f = System.Single;
	using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;

	[Description("This type will run as JavaScript.")]
	public sealed class Application
	{
		// based on http://learningwebgl.com/lessons/lesson16/index.html

		public readonly ApplicationWebService service = new ApplicationWebService();


		public Action Dispose;

		public sealed class DataType
		{
			public f[] vertexNormals;
			public f[] vertexPositions;
			public f[] vertexTextureCoords;
			public ushort[] indices;
		}

		[Script(ExternalTarget = "macbook")]
		public static DataType macbook;

		/// <summary>
		/// This is a javascript application.
		/// </summary>
		/// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
		public Application(IDefault page = null)
		{
			new Data.macbook().Content.AttachToDocument().onload +=
					delegate
					{
						InitializeContent(page);
					};



		}

		void InitializeContent(IDefault page = null)
		{

			var size = 500;


			var gl = new WebGLRenderingContext();


			var canvas = gl.canvas.AttachToDocument();

			Native.document.body.style.overflow = IStyle.OverflowEnum.hidden;
			canvas.style.SetLocation(0, 0, size, size);

			canvas.width = size;
			canvas.height = size;

			var gl_viewportWidth = size;
			var gl_viewportHeight = size;
			#region IsDisposed
			var IsDisposed = false;

			this.Dispose = delegate
			{
				if (IsDisposed)
					return;

				IsDisposed = true;

				canvas.Orphanize();
			};
			#endregion

			#region AtResize
			Action AtResize =
				delegate
				{
					gl_viewportWidth = Native.window.Width;
					gl_viewportHeight = Native.window.Height;

					canvas.style.SetLocation(0, 0, gl_viewportWidth, gl_viewportHeight);

					canvas.width = gl_viewportWidth;
					canvas.height = gl_viewportHeight;
				};

			Native.window.onresize +=
				e =>
				{
					AtResize();
				};
			AtResize();
			#endregion


			#region requestFullscreen
			Native.document.body.ondblclick +=
				delegate
				{
					if (IsDisposed)
						return;

					// http://tutorialzine.com/2012/02/enhance-your-website-fullscreen-api/

					Native.document.body.requestFullscreen();


				};
			#endregion

			#region createShader
			Func<ScriptCoreLib.GLSL.Shader, WebGLShader> createShader = (src) =>
			{
				var shader = gl.createShader(src);

				// verify
				if (gl.getShaderParameter(shader, gl.COMPILE_STATUS) == null)
				{
					Native.window.alert("error in SHADER:\n" + gl.getShaderInfoLog(shader));
					throw new InvalidOperationException("shader failed");
				}

				return shader;
			};
			#endregion

			#region programs
			var programs =
				new[]
				{
					new
					{
						vs = (FragmentShader)new Shaders.PerFragmentLightingFragmentShader(),
						fs = (VertexShader)new Shaders.PerFragmentLightingVertexShader()
					},

				}.Select(
					p =>
					{
						var vs = createShader(p.vs);
						var fs = createShader(p.fs);


						var shaderProgram = gl.createProgram();

						gl.attachShader(shaderProgram, vs);
						gl.attachShader(shaderProgram, fs);

						gl.linkProgram(shaderProgram);


						var vertexPositionAttribute = gl.getAttribLocation(shaderProgram, "aVertexPosition");
						gl.enableVertexAttribArray((uint)vertexPositionAttribute);

						var vertexNormalAttribute = gl.getAttribLocation(shaderProgram, "aVertexNormal");
						gl.enableVertexAttribArray((uint)vertexNormalAttribute);

						var textureCoordAttribute = gl.getAttribLocation(shaderProgram, "aTextureCoord");
						gl.enableVertexAttribArray((uint)textureCoordAttribute);


						return new
						{
							program = shaderProgram,

							vertexPositionAttribute,
							vertexNormalAttribute,
							textureCoordAttribute,

							pMatrixUniform = gl.getUniformLocation(shaderProgram, "uPMatrix"),
							mvMatrixUniform = gl.getUniformLocation(shaderProgram, "uMVMatrix"),
							nMatrixUniform = gl.getUniformLocation(shaderProgram, "uNMatrix"),
							samplerUniform = gl.getUniformLocation(shaderProgram, "uSampler"),

							materialAmbientColorUniform = gl.getUniformLocation(shaderProgram, "uMaterialAmbientColor"),
							materialDiffuseColorUniform = gl.getUniformLocation(shaderProgram, "uMaterialDiffuseColor"),
							materialSpecularColorUniform = gl.getUniformLocation(shaderProgram, "uMaterialSpecularColor"),
							materialShininessUniform = gl.getUniformLocation(shaderProgram, "uMaterialShininess"),
							materialEmissiveColorUniform = gl.getUniformLocation(shaderProgram, "uMaterialEmissiveColor"),
							showSpecularHighlightsUniform = gl.getUniformLocation(shaderProgram, "uShowSpecularHighlights"),
							useTexturesUniform = gl.getUniformLocation(shaderProgram, "uUseTextures"),
							ambientLightingColorUniform = gl.getUniformLocation(shaderProgram, "uAmbientLightingColor"),
							pointLightingLocationUniform = gl.getUniformLocation(shaderProgram, "uPointLightingLocation"),
							pointLightingSpecularColorUniform = gl.getUniformLocation(shaderProgram, "uPointLightingSpecularColor"),
							pointLightingDiffuseColorUniform = gl.getUniformLocation(shaderProgram, "uPointLightingDiffuseColor"),





						};
					}
			).ToArray();
			#endregion




			var currentProgram = programs.First();

			var mvMatrix = glMatrix.mat4.create();
			var mvMatrixStack = new Stack<Float32Array>();

			var pMatrix = glMatrix.mat4.create();

			#region mvPushMatrix
			Action mvPushMatrix = delegate
			{
				var copy = glMatrix.mat4.create();
				glMatrix.mat4.set(mvMatrix, copy);
				mvMatrixStack.Push(copy);
			};
			#endregion

			#region mvPopMatrix
			Action mvPopMatrix = delegate
			{
				mvMatrix = mvMatrixStack.Pop();
			};
			#endregion



			#region degToRad
			Func<float, float> degToRad = (degrees) =>
			{
				return degrees * (f)Math.PI / 180f;
			};
			#endregion

			#region requestPointerLock
			var __pointer_x = 0;
			var __pointer_y = 0;

			canvas.onmousedown +=
				delegate
				{
					canvas.requestPointerLock();
				};

			canvas.onmousemove +=
				e =>
				{
					if (Native.Document.pointerLockElement == canvas)
					{

						__pointer_x += e.movementX;
						__pointer_y += e.movementY;
					}
				};

			canvas.onmouseup +=
				delegate
				{
					Native.Document.exitPointerLock();
				};
			#endregion

			// await crate
			new HTML.Images.FromAssets.crate().InvokeOnComplete(
				crate =>
					// await moon
					new HTML.Images.FromAssets.moon().InvokeOnComplete(
						moon =>
						{

							#region setMatrixUniforms
							Action setMatrixUniforms =
								delegate
								{
									#region [uniform] mat4 uPMatrix <- pMatrix
									gl.uniformMatrix4fv(currentProgram.pMatrixUniform, false, pMatrix);
									#endregion

									#region [uniform] mat4 uMVMatrix <- mvMatrix
									gl.uniformMatrix4fv(currentProgram.mvMatrixUniform, false, mvMatrix);
									#endregion

									var normalMatrix = glMatrix.mat3.create();
									glMatrix.mat4.toInverseMat3(mvMatrix, normalMatrix);
									glMatrix.mat3.transpose(normalMatrix);

									#region [uniform] mat3 uNMatrix <- normalMatrix
									gl.uniformMatrix3fv(currentProgram.nMatrixUniform, false, normalMatrix);
									#endregion
								};
							#endregion


							#region handleLoadedTexture
							Action<WebGLTexture, IHTMLImage> handleLoadedTexture = (texture, texture_image) =>
							{
								gl.pixelStorei(gl.UNPACK_FLIP_Y_WEBGL, 1);
								gl.bindTexture(gl.TEXTURE_2D, texture);
								gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, texture_image);
								gl.texParameteri((uint)gl.TEXTURE_2D, (uint)gl.TEXTURE_MAG_FILTER, (int)gl.LINEAR);
								gl.texParameteri((uint)gl.TEXTURE_2D, (uint)gl.TEXTURE_MIN_FILTER, (int)gl.LINEAR_MIPMAP_NEAREST);
								// INVALID_OPERATION: generateMipmap: level 0 not power of 2 or not all the same size 
								gl.generateMipmap(gl.TEXTURE_2D);

								gl.bindTexture(gl.TEXTURE_2D, null);
							};
							#endregion

							var crateTexture = gl.createTexture();
							handleLoadedTexture(crateTexture, crate);

							var moonTexture = gl.createTexture();
							handleLoadedTexture(moonTexture, moon);



							#region initTextureFramebuffer
							var rttFramebuffer = new WebGLFramebuffer(gl);
							gl.bindFramebuffer(gl.FRAMEBUFFER, rttFramebuffer);
							// generateMipmap: level 0 not power of 2 or not all the same size
							//var rttFramebuffer_width = canvas.width;
							var rttFramebuffer_width = 512;
							var rttFramebuffer_height = 512;

							var rttTexture = new WebGLTexture(gl);
							gl.bindTexture(gl.TEXTURE_2D, rttTexture);
							gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, (int)gl.LINEAR);
							gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, (int)gl.LINEAR_MIPMAP_NEAREST);
							gl.generateMipmap(gl.TEXTURE_2D);

							gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, rttFramebuffer_width, rttFramebuffer_height, 0, gl.RGBA, gl.UNSIGNED_BYTE, null);

							var renderbuffer = new WebGLRenderbuffer(gl);
							gl.bindRenderbuffer(gl.RENDERBUFFER, renderbuffer);
							gl.renderbufferStorage(gl.RENDERBUFFER, gl.DEPTH_COMPONENT16, rttFramebuffer_width, rttFramebuffer_height);

							gl.framebufferTexture2D(gl.FRAMEBUFFER, gl.COLOR_ATTACHMENT0, gl.TEXTURE_2D, rttTexture, 0);
							gl.framebufferRenderbuffer(gl.FRAMEBUFFER, gl.DEPTH_ATTACHMENT, gl.RENDERBUFFER, renderbuffer);

							gl.bindTexture(gl.TEXTURE_2D, null);
							gl.bindRenderbuffer(gl.RENDERBUFFER, null);
							gl.bindFramebuffer(gl.FRAMEBUFFER, null);
							#endregion



							#region initBuffers

							var cubeVertexPositionBuffer = gl.createBuffer();
							gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
							var vertices = new f[]{
								// Front face
								-1.0f, -1.0f,  1.0f,
			 1.0f, -1.0f,  1.0f,
			 1.0f,  1.0f,  1.0f,
			-1.0f,  1.0f,  1.0f,

								// Back face
								-1.0f, -1.0f, -1.0f,
			-1.0f,  1.0f, -1.0f,
			 1.0f,  1.0f, -1.0f,
			 1.0f, -1.0f, -1.0f,

								// Top face
								-1.0f,  1.0f, -1.0f,
			-1.0f,  1.0f,  1.0f,
			 1.0f,  1.0f,  1.0f,
			 1.0f,  1.0f, -1.0f,

								// Bottom face
								-1.0f, -1.0f, -1.0f,
			 1.0f, -1.0f, -1.0f,
			 1.0f, -1.0f,  1.0f,
			-1.0f, -1.0f,  1.0f,

								// Right face
								1.0f, -1.0f, -1.0f,
			 1.0f,  1.0f, -1.0f,
			 1.0f,  1.0f,  1.0f,
			 1.0f, -1.0f,  1.0f,

								// Left face
								-1.0f, -1.0f, -1.0f,
			-1.0f, -1.0f,  1.0f,
			-1.0f,  1.0f,  1.0f,
			-1.0f,  1.0f, -1.0f,
		};
							gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices), gl.STATIC_DRAW);
							var cubeVertexPositionBuffer_itemSize = 3;
							var cubeVertexPositionBuffer_numItems = 24;

							var cubeVertexNormalBuffer = gl.createBuffer();
							gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexNormalBuffer);
							var vertexNormals = new f[]{
								// Front face
								0.0f,  0.0f,  1.0f,
			 0.0f,  0.0f,  1.0f,
			 0.0f,  0.0f,  1.0f,
			 0.0f,  0.0f,  1.0f,

								// Back face
								0.0f,  0.0f, -1.0f,
			 0.0f,  0.0f, -1.0f,
			 0.0f,  0.0f, -1.0f,
			 0.0f,  0.0f, -1.0f,

								// Top face
								0.0f,  1.0f,  0.0f,
			 0.0f,  1.0f,  0.0f,
			 0.0f,  1.0f,  0.0f,
			 0.0f,  1.0f,  0.0f,

								// Bottom face
								0.0f, -1.0f,  0.0f,
			 0.0f, -1.0f,  0.0f,
			 0.0f, -1.0f,  0.0f,
			 0.0f, -1.0f,  0.0f,

								// Right face
								1.0f,  0.0f,  0.0f,
			 1.0f,  0.0f,  0.0f,
			 1.0f,  0.0f,  0.0f,
			 1.0f,  0.0f,  0.0f,

								// Left face
								-1.0f,  0.0f,  0.0f,
			-1.0f,  0.0f,  0.0f,
			-1.0f,  0.0f,  0.0f,
			-1.0f,  0.0f,  0.0f,
		};
							gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertexNormals), gl.STATIC_DRAW);
							var cubeVertexNormalBuffer_itemSize = 3;
							var cubeVertexNormalBuffer_numItems = 24;

							var cubeVertexTextureCoordBuffer = gl.createBuffer();
							gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexTextureCoordBuffer);
							var textureCoords = new f[]{
								// Front face
								0.0f, 0.0f,
			1.0f, 0.0f,
			1.0f, 1.0f,
			0.0f, 1.0f,

								// Back face
								1.0f, 0.0f,
			1.0f, 1.0f,
			0.0f, 1.0f,
			0.0f, 0.0f,

								// Top face
								0.0f, 1.0f,
			0.0f, 0.0f,
			1.0f, 0.0f,
			1.0f, 1.0f,

								// Bottom face
								1.0f, 1.0f,
			0.0f, 1.0f,
			0.0f, 0.0f,
			1.0f, 0.0f,

								// Right face
								1.0f, 0.0f,
			1.0f, 1.0f,
			0.0f, 1.0f,
			0.0f, 0.0f,

								// Left face
								0.0f, 0.0f,
			1.0f, 0.0f,
			1.0f, 1.0f,
			0.0f, 1.0f
		};
							gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(textureCoords), gl.STATIC_DRAW);
							var cubeVertexTextureCoordBuffer_itemSize = 2;
							var cubeVertexTextureCoordBuffer_numItems = 24;

							var cubeVertexIndexBuffer = gl.createBuffer();
							gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);
							var cubeVertexIndices = new ushort[]{
			0, 1, 2,      0, 2, 3,	  // Front face
								4, 5, 6,      4, 6, 7,	  // Back face
								8, 9, 10,     8, 10, 11,  // Top face
								12, 13, 14,   12, 14, 15, // Bottom face
								16, 17, 18,   16, 18, 19, // Right face
								20, 21, 22,   20, 22, 23  // Left face
							};
							gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(cubeVertexIndices), gl.STATIC_DRAW);
							var cubeVertexIndexBuffer_itemSize = 1;
							var cubeVertexIndexBuffer_numItems = 36;


							var latitudeBands = 30;
							var longitudeBands = 30;
							var radius = 1;

							var vertexPositionData = new List<f>();
							var normalData = new List<f>();
							var textureCoordData = new List<f>();
							for (var latNumber = 0; latNumber <= latitudeBands; latNumber++)
							{
								var theta = latNumber * Math.PI / latitudeBands;
								var sinTheta = (f)Math.Sin(theta);
								var cosTheta = (f)Math.Cos(theta);

								for (var longNumber = 0; longNumber <= longitudeBands; longNumber++)
								{
									var phi = longNumber * 2 * Math.PI / longitudeBands;
									var sinPhi = (f)Math.Sin(phi);
									var cosPhi = (f)Math.Cos(phi);

									var x = cosPhi * sinTheta;
									var y = cosTheta;
									var z = sinPhi * sinTheta;
									var u = 1 - (longNumber / longitudeBands);
									var v = 1 - (latNumber / latitudeBands);

									normalData.Add(x);
									normalData.Add(y);
									normalData.Add(z);
									textureCoordData.Add(u);
									textureCoordData.Add(v);
									vertexPositionData.Add(radius * x);
									vertexPositionData.Add(radius * y);
									vertexPositionData.Add(radius * z);
								}
							}

							var indexData = new List<ushort>();
							for (var latNumber = 0; latNumber < latitudeBands; latNumber++)
							{
								for (var longNumber = 0; longNumber < longitudeBands; longNumber++)
								{
									var first = (latNumber * (longitudeBands + 1)) + longNumber;
									var second = first + longitudeBands + 1;
									indexData.Add((ushort)(first));
									indexData.Add((ushort)(second));
									indexData.Add((ushort)(first + 1));

									indexData.Add((ushort)(second));
									indexData.Add((ushort)(second + 1));
									indexData.Add((ushort)(first + 1));
								}
							}

							var moonVertexNormalBuffer = gl.createBuffer();
							gl.bindBuffer(gl.ARRAY_BUFFER, moonVertexNormalBuffer);
							gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(normalData.ToArray()), gl.STATIC_DRAW);
							var moonVertexNormalBuffer_itemSize = 3;
							var moonVertexNormalBuffer_numItems = normalData.Count / 3;

							var moonVertexTextureCoordBuffer = gl.createBuffer();
							gl.bindBuffer(gl.ARRAY_BUFFER, moonVertexTextureCoordBuffer);
							gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(textureCoordData.ToArray()), gl.STATIC_DRAW);
							var moonVertexTextureCoordBuffer_itemSize = 2;
							var moonVertexTextureCoordBuffer_numItems = textureCoordData.Count / 2;

							var moonVertexPositionBuffer = gl.createBuffer();
							gl.bindBuffer(gl.ARRAY_BUFFER, moonVertexPositionBuffer);
							gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertexPositionData.ToArray()), gl.STATIC_DRAW);
							var moonVertexPositionBuffer_itemSize = 3;
							var moonVertexPositionBuffer_numItems = vertexPositionData.Count / 3;

							var moonVertexIndexBuffer = gl.createBuffer();
							gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, moonVertexIndexBuffer);
							gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(indexData.ToArray()), gl.STREAM_DRAW);
							var moonVertexIndexBuffer_itemSize = 1;
							var moonVertexIndexBuffer_numItems = indexData.Count;


							var laptopScreenVertexPositionBuffer = gl.createBuffer();
							gl.bindBuffer(gl.ARRAY_BUFFER, laptopScreenVertexPositionBuffer);
							vertices = new[]{
			 0.580687f, 0.659f, 0.813106f,
			-0.580687f, 0.659f, 0.813107f,
			 0.580687f, 0.472f, 0.113121f,
			-0.580687f, 0.472f, 0.113121f,
		};
							gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices), gl.STATIC_DRAW);
							var laptopScreenVertexPositionBuffer_itemSize = 3;
							var laptopScreenVertexPositionBuffer_numItems = 4;

							var laptopScreenVertexNormalBuffer = gl.createBuffer();
							gl.bindBuffer(gl.ARRAY_BUFFER, laptopScreenVertexNormalBuffer);
							vertexNormals = new f[]{
			 0.000000f, -0.965926f, 0.258819f,
			 0.000000f, -0.965926f, 0.258819f,
			 0.000000f, -0.965926f, 0.258819f,
			 0.000000f, -0.965926f, 0.258819f,
		};
							gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertexNormals), gl.STATIC_DRAW);
							var laptopScreenVertexNormalBuffer_itemSize = 3;
							var laptopScreenVertexNormalBuffer_numItems = 4;

							var laptopScreenVertexTextureCoordBuffer = gl.createBuffer();
							gl.bindBuffer(gl.ARRAY_BUFFER, laptopScreenVertexTextureCoordBuffer);
							textureCoords = new f[]{
			1.0f, 1.0f,
			0.0f, 1.0f,
			1.0f, 0.0f,
			0.0f, 0.0f,
		};
							gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(textureCoords), gl.STATIC_DRAW);
							var laptopScreenVertexTextureCoordBuffer_itemSize = 2;
							var laptopScreenVertexTextureCoordBuffer_numItems = 4;


							#endregion

							#region handleLoadedLaptop
							var laptopData = macbook;

							var laptopVertexNormalBuffer = gl.createBuffer();
							gl.bindBuffer(gl.ARRAY_BUFFER, laptopVertexNormalBuffer);
							gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(laptopData.vertexNormals), gl.STATIC_DRAW);
							var laptopVertexNormalBuffer_itemSize = 3;
							var laptopVertexNormalBuffer_numItems = laptopData.vertexNormals.Length / 3;

							var laptopVertexTextureCoordBuffer = gl.createBuffer();
							gl.bindBuffer(gl.ARRAY_BUFFER, laptopVertexTextureCoordBuffer);
							gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(laptopData.vertexTextureCoords), gl.STATIC_DRAW);
							var laptopVertexTextureCoordBuffer_itemSize = 2;
							var laptopVertexTextureCoordBuffer_numItems = laptopData.vertexTextureCoords.Length / 2;

							var laptopVertexPositionBuffer = gl.createBuffer();
							gl.bindBuffer(gl.ARRAY_BUFFER, laptopVertexPositionBuffer);
							gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(laptopData.vertexPositions), gl.STATIC_DRAW);
							var laptopVertexPositionBuffer_itemSize = 3;
							var laptopVertexPositionBuffer_numItems = laptopData.vertexPositions.Length / 3;

							var laptopVertexIndexBuffer = gl.createBuffer();
							gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, laptopVertexIndexBuffer);
							gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(laptopData.indices), gl.STREAM_DRAW);
							var laptopVertexIndexBuffer_itemSize = 1;
							var laptopVertexIndexBuffer_numItems = laptopData.indices.Length;
							#endregion


							gl.clearColor(0.0f, 0.0f, 0.0f, 1.0f);
							gl.enable(gl.DEPTH_TEST);


							var moonAngle = 180f;
							var cubeAngle = 0f;
							var laptopAngle = 0f;

							var lastTime = 0L;



							var laptopScreenAspectRatio = 1.66f;

							//Func<string, f> parseFloat = Convert.ToSingle;
							Func<string, f> parseFloat = x => float.Parse(x);

							var shaderProgram = currentProgram;

							#region drawSceneOnLaptopScreen
							Action drawSceneOnLaptopScreen =
							delegate
							{
								gl.viewport(0, 0, rttFramebuffer_width, rttFramebuffer_height);
								gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

								glMatrix.mat4.perspective(45, laptopScreenAspectRatio, 0.1f, 100.0f, pMatrix);

								gl.uniform1i(shaderProgram.showSpecularHighlightsUniform, Convert.ToInt32(false));
								gl.uniform3f(shaderProgram.ambientLightingColorUniform, 0.2f, 0.2f, 0.2f);
								gl.uniform3f(shaderProgram.pointLightingLocationUniform, 0, 0, -5);
								gl.uniform3f(shaderProgram.pointLightingDiffuseColorUniform, 0.8f, 0.8f, 0.8f);

								gl.uniform1i(shaderProgram.showSpecularHighlightsUniform, Convert.ToInt32(false));
								gl.uniform1i(shaderProgram.useTexturesUniform, Convert.ToInt32(true));

								gl.uniform3f(shaderProgram.materialAmbientColorUniform, 1.0f, 1.0f, 1.0f);
								gl.uniform3f(shaderProgram.materialDiffuseColorUniform, 1.0f, 1.0f, 1.0f);
								gl.uniform3f(shaderProgram.materialSpecularColorUniform, 0.0f, 0.0f, 0.0f);
								gl.uniform1f(shaderProgram.materialShininessUniform, 0);
								gl.uniform3f(shaderProgram.materialEmissiveColorUniform, 0.0f, 0.0f, 0.0f);

								glMatrix.mat4.identity(mvMatrix);

								glMatrix.mat4.translate(mvMatrix, new f[] { 0, 0, -5 });
								glMatrix.mat4.rotate(mvMatrix, degToRad(30), new f[] { 1, 0, 0 });

								mvPushMatrix();
								glMatrix.mat4.rotate(mvMatrix, degToRad(moonAngle), new f[] { 0, 1, 0 });
								glMatrix.mat4.translate(mvMatrix, new f[] { 2, 0, 0 });
								gl.activeTexture(gl.TEXTURE0);
								gl.bindTexture(gl.TEXTURE_2D, moonTexture);
								gl.uniform1i(shaderProgram.samplerUniform, 0);

								gl.bindBuffer(gl.ARRAY_BUFFER, moonVertexPositionBuffer);
								gl.vertexAttribPointer((uint)shaderProgram.vertexPositionAttribute, moonVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);

								gl.bindBuffer(gl.ARRAY_BUFFER, moonVertexTextureCoordBuffer);
								gl.vertexAttribPointer((uint)shaderProgram.textureCoordAttribute, moonVertexTextureCoordBuffer_itemSize, gl.FLOAT, false, 0, 0);

								gl.bindBuffer(gl.ARRAY_BUFFER, moonVertexNormalBuffer);
								gl.vertexAttribPointer((uint)shaderProgram.vertexNormalAttribute, moonVertexNormalBuffer_itemSize, gl.FLOAT, false, 0, 0);

								gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, moonVertexIndexBuffer);
								setMatrixUniforms();
								gl.drawElements(gl.TRIANGLES, moonVertexIndexBuffer_numItems, gl.UNSIGNED_SHORT, 0);
								mvPopMatrix();


								mvPushMatrix();
								glMatrix.mat4.rotate(mvMatrix, degToRad(cubeAngle), new f[] { 0, 1, 0 });
								glMatrix.mat4.translate(mvMatrix, new f[] { 1.25f, 0, 0 });
								gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
								gl.vertexAttribPointer((uint)shaderProgram.vertexPositionAttribute, cubeVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);

								gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexNormalBuffer);
								gl.vertexAttribPointer((uint)shaderProgram.vertexNormalAttribute, cubeVertexNormalBuffer_itemSize, gl.FLOAT, false, 0, 0);

								gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexTextureCoordBuffer);
								gl.vertexAttribPointer((uint)shaderProgram.textureCoordAttribute, cubeVertexTextureCoordBuffer_itemSize, gl.FLOAT, false, 0, 0);

								gl.activeTexture(gl.TEXTURE0);
								gl.bindTexture(gl.TEXTURE_2D, crateTexture);
								gl.uniform1i(shaderProgram.samplerUniform, 0);

								gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);
								setMatrixUniforms();
								gl.drawElements(gl.TRIANGLES, cubeVertexIndexBuffer_numItems, gl.UNSIGNED_SHORT, 0);
								mvPopMatrix();

								gl.bindTexture(gl.TEXTURE_2D, rttTexture);
								gl.generateMipmap(gl.TEXTURE_2D);
								gl.bindTexture(gl.TEXTURE_2D, null);
							};
							#endregion

							//var pass0frag = new ChromeShaderToyColumns.Shaders.ProgramFragmentShader();

							//var pass = new ChromeShaderToyColumns.Library.ShaderToy.EffectPass(
							//	gl: gl,
							//	precission: ChromeShaderToyColumns.Library.ShaderToy.DetermineShaderPrecission(gl),
							//	supportDerivatives: gl.getExtension("OES_standard_derivatives") != null
							//);
							//pass.MakeHeader_Image();
							//pass.NewShader_Image(pass0frag);

							var sw = Stopwatch.StartNew();
							Native.window.onframe += delegate
							{
								if (IsDisposed)
									return;


								var timeNow = new IDate().getTime();
								if (lastTime != 0)
								{
									var elapsed = timeNow - lastTime;

									moonAngle += 0.05f * elapsed;
									cubeAngle += 0.05f * elapsed;

									laptopAngle -= 0.005f * elapsed;
								}
								lastTime = timeNow;


								#region FRAMEBUFFER
								gl.bindFramebuffer(gl.FRAMEBUFFER, rttFramebuffer);

								// http://stackoverflow.com/questions/20362023/webgl-why-does-transparent-canvas-show-clearcolor-color-component-when-alpha-is
								gl.clearColor(1, 1, 0, 0);

								gl.viewport(0, 0, rttFramebuffer_width, rttFramebuffer_height);
								gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

								// drawSceneOnLaptopScreen();

								//// glDrawArrays: attempt to render with no buffer attached to enabled attribute 1
								//pass.Paint_Image(
								//	sw.ElapsedMilliseconds / 1000.0f,

								//	0,
								//	0,
								//	0,
								//	0,

								//	// gl_FragCoord
								//	// cannot be scaled, and can be referenced directly.
								//	// need another way to scale
								//	zoom: 1.0f
								//);

								//gl.flush();

								// INVALID_OPERATION: generateMipmap: level 0 not power of 2 or not all the same size
								gl.bindTexture(gl.TEXTURE_2D, rttTexture);
								gl.generateMipmap(gl.TEXTURE_2D);
								gl.bindTexture(gl.TEXTURE_2D, null);

								gl.bindFramebuffer(gl.FRAMEBUFFER, null);
								#endregion




								gl.useProgram(shaderProgram.program);

								gl.viewport(0, 0, gl_viewportWidth, gl_viewportHeight);
								gl.clearColor(0, 0, 1, 0);
								gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

								glMatrix.mat4.perspective(45, gl_viewportWidth / gl_viewportHeight, 0.1f, 100.0f, pMatrix);

								glMatrix.mat4.identity(mvMatrix);

								mvPushMatrix();

								glMatrix.mat4.translate(mvMatrix,
									new f[]{
									0, -0.4f,
								   (float)Math.Min(0,
									-2.2f
									+ __pointer_y * 0.01)
									}
									);

								if (__pointer_x != 0)
									laptopAngle = __pointer_x + 0.01f;

								glMatrix.mat4.rotate(mvMatrix, degToRad(laptopAngle), new f[] { 0, 1, 0 });
								glMatrix.mat4.rotate(mvMatrix, degToRad(-90), new f[] { 1, 0, 0 });

								//glMatrix.mat4.rotate(mvMatrix, __pointer_y * 0.01f, 0, 1, 0);
								//glMatrix.mat4.rotate(mvMatrix, __pointer_x * 0.01f, 1, 0, 0);

								gl.uniform1i(shaderProgram.showSpecularHighlightsUniform, Convert.ToInt32(true));
								gl.uniform3f(shaderProgram.pointLightingLocationUniform, -1, 2, -1);

								gl.uniform3f(shaderProgram.ambientLightingColorUniform, 0.2f, 0.2f, 0.2f);
								gl.uniform3f(shaderProgram.pointLightingDiffuseColorUniform, 0.8f, 0.8f, 0.8f);
								gl.uniform3f(shaderProgram.pointLightingSpecularColorUniform, 0.8f, 0.8f, 0.8f);

								// The laptop body is quite shiny and has no texture.  It reflects lots of specular light
								gl.uniform3f(shaderProgram.materialAmbientColorUniform, 1.0f, 1.0f, 1.0f);
								gl.uniform3f(shaderProgram.materialDiffuseColorUniform, 1.0f, 1.0f, 1.0f);
								gl.uniform3f(shaderProgram.materialSpecularColorUniform, 1.5f, 1.5f, 1.5f);
								gl.uniform1f(shaderProgram.materialShininessUniform, 5);
								gl.uniform3f(shaderProgram.materialEmissiveColorUniform, 0.0f, 0.0f, 0.0f);
								gl.uniform1i(shaderProgram.useTexturesUniform, Convert.ToInt32(false));

								//if (laptopVertexPositionBuffer) {
								gl.bindBuffer(gl.ARRAY_BUFFER, laptopVertexPositionBuffer);
								gl.vertexAttribPointer((uint)shaderProgram.vertexPositionAttribute, laptopVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);

								gl.bindBuffer(gl.ARRAY_BUFFER, laptopVertexTextureCoordBuffer);
								gl.vertexAttribPointer((uint)shaderProgram.textureCoordAttribute, laptopVertexTextureCoordBuffer_itemSize, gl.FLOAT, false, 0, 0);

								gl.bindBuffer(gl.ARRAY_BUFFER, laptopVertexNormalBuffer);
								gl.vertexAttribPointer((uint)shaderProgram.vertexNormalAttribute, laptopVertexNormalBuffer_itemSize, gl.FLOAT, false, 0, 0);

								gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, laptopVertexIndexBuffer);
								setMatrixUniforms();
								gl.drawElements(gl.TRIANGLES, laptopVertexIndexBuffer_numItems, gl.UNSIGNED_SHORT, 0);
								//}

								gl.uniform3f(shaderProgram.materialAmbientColorUniform, 0.0f, 0.0f, 0.0f);
								gl.uniform3f(shaderProgram.materialDiffuseColorUniform, 0.0f, 0.0f, 0.0f);
								gl.uniform3f(shaderProgram.materialSpecularColorUniform, 0.5f, 0.5f, 0.5f);
								gl.uniform1f(shaderProgram.materialShininessUniform, 20);
								gl.uniform3f(shaderProgram.materialEmissiveColorUniform, 1.5f, 1.5f, 1.5f);
								gl.uniform1i(shaderProgram.useTexturesUniform, Convert.ToInt32(true));

								gl.bindBuffer(gl.ARRAY_BUFFER, laptopScreenVertexPositionBuffer);
								gl.vertexAttribPointer((uint)shaderProgram.vertexPositionAttribute, laptopScreenVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);

								gl.bindBuffer(gl.ARRAY_BUFFER, laptopScreenVertexNormalBuffer);
								gl.vertexAttribPointer((uint)shaderProgram.vertexNormalAttribute, laptopScreenVertexNormalBuffer_itemSize, gl.FLOAT, false, 0, 0);

								gl.bindBuffer(gl.ARRAY_BUFFER, laptopScreenVertexTextureCoordBuffer);
								gl.vertexAttribPointer((uint)shaderProgram.textureCoordAttribute, laptopScreenVertexTextureCoordBuffer_itemSize, gl.FLOAT, false, 0, 0);

								gl.activeTexture(gl.TEXTURE0);
								gl.bindTexture(gl.TEXTURE_2D, rttTexture);
								gl.uniform1i(shaderProgram.samplerUniform, 0);

								setMatrixUniforms();
								gl.drawArrays(gl.TRIANGLE_STRIP, 0, laptopScreenVertexPositionBuffer_numItems);

								mvPopMatrix();

								// first frame only
								IsDisposed = true;


							};


						}
					)

			);
		}

	}


}
