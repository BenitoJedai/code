using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.WebGL;
using WebGLLesson16.Design;
using WebGLLesson16.HTML.Pages;
using ScriptCoreLib.GLSL;
using System.ComponentModel;


namespace WebGLLesson16
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;

    [Description("This type will run as JavaScript.")]
    public sealed class Application
    {
        // based on http://learningwebgl.com/lessons/lesson16/index.html

        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly DefaultStyle style = new DefaultStyle();

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
        public Application(IDefaultPage page = null)
        {
            #region await __glMatrix
            new __glMatrix().Content.With(
              source =>
              {
                  source.onload +=
                    delegate
                    {
                        #region await Teapot
                        new Data.macbook().Content.With(
                          source2 =>
                          {
                              source2.onload +=
                                delegate
                                {
                                    InitializeContent(page);
                                };

                              source2.AttachToDocument();
                          }
                       );
                        #endregion

                    };

                  source.AttachToDocument();
              }
           );
            #endregion


            style.Content.AttachToHead();
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

        void InitializeContent(IDefaultPage page = null)
        {

            var gl_viewportWidth = Native.Window.Width;
            var gl_viewportHeight = Native.Window.Height;

            #region canvas
            var canvas = new IHTMLCanvas().AttachToDocument();

            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;
            canvas.style.SetLocation(0, 0, gl_viewportWidth, gl_viewportHeight);

            canvas.width = gl_viewportWidth;
            canvas.height = gl_viewportHeight;
            #endregion

            #region gl - Initialise WebGL


            var gl = default(WebGLRenderingContext);

            try
            {

                gl = (WebGLRenderingContext)canvas.getContext("experimental-webgl");

            }
            catch { }

            if (gl == null)
            {
                Native.Window.alert("WebGL not supported");
                throw new InvalidOperationException("cannot create webgl context");
            }
            #endregion


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
                    gl_viewportWidth = Native.Window.Width;
                    gl_viewportHeight = Native.Window.Height;

                    canvas.style.SetLocation(0, 0, gl_viewportWidth, gl_viewportHeight);

                    canvas.width = gl_viewportWidth;
                    canvas.height = gl_viewportHeight;
                };

            Native.Window.onresize +=
                e =>
                {
                    AtResize();
                };
            AtResize();
            #endregion


            #region requestFullscreen
            Native.Document.body.ondblclick +=
                delegate
                {
                    if (IsDisposed)
                        return;

                    // http://tutorialzine.com/2012/02/enhance-your-website-fullscreen-api/

                    Native.Document.body.requestFullscreen();


                };
            #endregion

            #region createShader
            Func<ScriptCoreLib.GLSL.Shader, WebGLShader> createShader = (src) =>
            {
                var shader = gl.createShader(src);

                // verify
                if (gl.getShaderParameter(shader, gl.COMPILE_STATUS) == null)
                {
                    Native.Window.alert("error in SHADER:\n" + gl.getShaderInfoLog(shader));
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

            var mvMatrix = __glMatrix.mat4.create();
            var mvMatrixStack = new Stack<Float32Array>();

            var pMatrix = __glMatrix.mat4.create();

            #region mvPushMatrix
            Action mvPushMatrix = delegate
            {
                var copy = __glMatrix.mat4.create();
                __glMatrix.mat4.set(mvMatrix, copy);
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

                                    var normalMatrix = __glMatrix.mat3.create();
                                    __glMatrix.mat4.toInverseMat3(mvMatrix, normalMatrix);
                                    __glMatrix.mat3.transpose(normalMatrix);

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
                                gl.generateMipmap(gl.TEXTURE_2D);

                                gl.bindTexture(gl.TEXTURE_2D, null);
                            };
                            #endregion

                            var crateTexture = gl.createTexture();
                            handleLoadedTexture(crateTexture, crate);

                            var moonTexture = gl.createTexture();
                            handleLoadedTexture(moonTexture, moon);



                            #region initTextureFramebuffer
                            var rttFramebuffer = gl.createFramebuffer();
                            gl.bindFramebuffer(gl.FRAMEBUFFER, rttFramebuffer);
                            var rttFramebuffer_width = 512;
                            var rttFramebuffer_height = 512;

                            var rttTexture = gl.createTexture();
                            gl.bindTexture(gl.TEXTURE_2D, rttTexture);
                            gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, (int)gl.LINEAR);
                            gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, (int)gl.LINEAR_MIPMAP_NEAREST);
                            gl.generateMipmap(gl.TEXTURE_2D);

                            gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, rttFramebuffer_width, rttFramebuffer_height, 0, gl.RGBA, gl.UNSIGNED_BYTE, null);

                            var renderbuffer = gl.createRenderbuffer();
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
            0, 1, 2,      0, 2, 3,    // Front face
            4, 5, 6,      4, 6, 7,    // Back face
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

                            #region animate
                            Action animate = () =>
                            {
                                var timeNow = new IDate().getTime();
                                if (lastTime != 0)
                                {
                                    var elapsed = timeNow - lastTime;

                                    moonAngle += 0.05f * elapsed;
                                    cubeAngle += 0.05f * elapsed;

                                    laptopAngle -= 0.005f * elapsed;
                                }
                                lastTime = timeNow;
                            };
                            #endregion

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

                                __glMatrix.mat4.perspective(45, laptopScreenAspectRatio, 0.1f, 100.0f, pMatrix);

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

                                __glMatrix.mat4.identity(mvMatrix);

                                __glMatrix.mat4.translate(mvMatrix, 0, 0, -5);
                                __glMatrix.mat4.rotate(mvMatrix, degToRad(30), 1, 0, 0);

                                mvPushMatrix();
                                __glMatrix.mat4.rotate(mvMatrix, degToRad(moonAngle), 0, 1, 0);
                                __glMatrix.mat4.translate(mvMatrix, 2, 0, 0);
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
                                __glMatrix.mat4.rotate(mvMatrix, degToRad(cubeAngle), 0, 1, 0);
                                __glMatrix.mat4.translate(mvMatrix, 1.25f, 0, 0);
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

                            #region drawScene
                            Action drawScene = () =>
                            {
                                gl.useProgram(shaderProgram.program);

                                gl.bindFramebuffer(gl.FRAMEBUFFER, rttFramebuffer);
                                drawSceneOnLaptopScreen();

                                gl.bindFramebuffer(gl.FRAMEBUFFER, null);

                                gl.viewport(0, 0, gl_viewportWidth, gl_viewportHeight);
                                gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

                                __glMatrix.mat4.perspective(45, gl_viewportWidth / gl_viewportHeight, 0.1f, 100.0f, pMatrix);

                                __glMatrix.mat4.identity(mvMatrix);

                                mvPushMatrix();

                                __glMatrix.mat4.translate(mvMatrix, 0, -0.4f, -2.2f);
                                __glMatrix.mat4.rotate(mvMatrix, degToRad(laptopAngle), 0, 1, 0);
                                __glMatrix.mat4.rotate(mvMatrix, degToRad(-90), 1, 0, 0);

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

                            };
                            #endregion




                            #region tick
                            Action tick = null;

                            tick = () =>
                            {
                                if (IsDisposed)
                                    return;


                                animate();
                                drawScene();


                                Native.Window.requestAnimationFrame += tick;
                            };

                            tick();
                            #endregion


                        }
                    )

            );
        }

    }


}
