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
using WebGLLesson12.Design;
using WebGLLesson12.HTML.Pages;

namespace WebGLLesson12
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;

    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application
    {
        // based on http://learningwebgl.com/lessons/lesson12/index.html

        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly DefaultStyle style = new DefaultStyle();

        public Action Dispose;


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault  page = null)
        {
            #region await __glMatrix then do InitializeContent
            new __glMatrix().Content.With(
              source =>
              {
                  source.onload +=
                    delegate
                    {
                        InitializeContent(page);
                    };

                  source.AttachToDocument();
              }
           );
            #endregion


            style.Content.AttachToHead();
        
        }

        void InitializeContent(IDefault  page = null)
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

            var toolbar = new Toolbar();

            if (page != null)
            {
                toolbar.Container.style.Opacity = 0.7;
                toolbar.Container.AttachToDocument();


                toolbar.HideButton.onclick +=
                    delegate
                    {
                        // ScriptCoreLib.Extensions
                        toolbar.HideTarget.ToggleVisible();
                    };
            }

         
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

      


            #region init shaders
       

            var shaderProgram = gl.createProgram(
                new Shaders.GeometryVertexShader(),
                new Shaders.GeometryFragmentShader()
                );



            gl.linkProgram(shaderProgram);
            gl.useProgram(shaderProgram);

            #region getAttribLocation
            Func<string, long> getAttribLocation =
                    name => gl.getAttribLocation(shaderProgram, name);
            #endregion

            #region getUniformLocation
            Func<string, WebGLUniformLocation> getUniformLocation =
                name => gl.getUniformLocation(shaderProgram, name);
            #endregion

            #endregion





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


            new HTML.Images.FromAssets.crate().InvokeOnComplete(
                crate =>
                    new HTML.Images.FromAssets.moon().InvokeOnComplete(
                        moon =>
                        {


                            var shaderProgram_vertexPositionAttribute = gl.getAttribLocation(shaderProgram, "aVertexPosition");
                            gl.enableVertexAttribArray((uint)shaderProgram_vertexPositionAttribute);

                            var shaderProgram_textureCoordAttribute = gl.getAttribLocation(shaderProgram, "aTextureCoord");
                            gl.enableVertexAttribArray((uint)shaderProgram_textureCoordAttribute);

                            var shaderProgram_vertexNormalAttribute = gl.getAttribLocation(shaderProgram, "aVertexNormal");
                            gl.enableVertexAttribArray((uint)shaderProgram_vertexNormalAttribute);

                            var shaderProgram_pMatrixUniform = gl.getUniformLocation(shaderProgram, "uPMatrix");
                            var shaderProgram_mvMatrixUniform = gl.getUniformLocation(shaderProgram, "uMVMatrix");
                            var shaderProgram_nMatrixUniform = gl.getUniformLocation(shaderProgram, "uNMatrix");
                            var shaderProgram_samplerUniform = gl.getUniformLocation(shaderProgram, "uSampler");
                            var shaderProgram_useLightingUniform = gl.getUniformLocation(shaderProgram, "uUseLighting");
                            var shaderProgram_ambientColorUniform = gl.getUniformLocation(shaderProgram, "uAmbientColor");
                            var shaderProgram_pointLightingLocationUniform = gl.getUniformLocation(shaderProgram, "uPointLightingLocation");
                            var shaderProgram_pointLightingColorUniform = gl.getUniformLocation(shaderProgram, "uPointLightingColor");

                            #region setMatrixUniforms
                            Action setMatrixUniforms =
                                delegate
                                {
                                    gl.uniformMatrix4fv(shaderProgram_pMatrixUniform, false, pMatrix);
                                    gl.uniformMatrix4fv(shaderProgram_mvMatrixUniform, false, mvMatrix);

                                    var normalMatrix = __glMatrix.mat3.create();
                                    __glMatrix.mat4.toInverseMat3(mvMatrix, normalMatrix);
                                    __glMatrix.mat3.transpose(normalMatrix);
                                    gl.uniformMatrix3fv(shaderProgram_nMatrixUniform, false, normalMatrix);
                                };
                            #endregion

                            #region cubeVertexPositionBuffer
                            var cubeVertexPositionBuffer = gl.createBuffer();
                            gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
                            var vertices = new f[]
                                   {
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
                                       -1.0f,  1.0f, -1.0f
                                   };

                            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices), gl.STATIC_DRAW);
                            var cubeVertexPositionBuffer_itemSize = 3;
                            var cubeVertexPositionBuffer_numItems = 24;
                            #endregion

                            #region cubeVertexNormalBuffer
                            var cubeVertexNormalBuffer = gl.createBuffer();
                            gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexNormalBuffer);
                            var vertexNormals = new f[]
                            {
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
                            #endregion

                            #region cubeVertexTextureCoordBuffer
                            var cubeVertexTextureCoordBuffer = gl.createBuffer();
                            gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexTextureCoordBuffer);
                            var textureCoords = new f[]
                                   {
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
                                       0.0f, 1.0f,
                                   };
                            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(textureCoords), gl.STATIC_DRAW);
                            var cubeVertexTextureCoordBuffer_itemSize = 2;
                            #endregion

                            #region cubeVertexIndexBuffer
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
                            #endregion

                            #region moon
                            var latitudeBands = 30;
                            var longitudeBands = 30;
                            var radius = 2;

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
                                    indexData.Add((ushort)first);
                                    indexData.Add((ushort)second);
                                    indexData.Add((ushort)(first + 1));

                                    indexData.Add((ushort)second);
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
                            #endregion

                            #region handleLoadedTexture
                            Action<WebGLTexture, IHTMLImage> handleLoadedTexture = (texture, texture_image) =>
                            {
                                gl.pixelStorei(gl.UNPACK_FLIP_Y_WEBGL, 1);
                                gl.bindTexture(gl.TEXTURE_2D, texture);
                                gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, texture_image);
                                gl.texParameteri((uint)gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, (int)gl.LINEAR);
                                gl.texParameteri((uint)gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, (int)gl.LINEAR_MIPMAP_NEAREST);
                                gl.generateMipmap(gl.TEXTURE_2D);

                                gl.bindTexture(gl.TEXTURE_2D, null);
                            };
                            #endregion

                            var moonTexture = gl.createTexture();
                            handleLoadedTexture(moonTexture, moon);


                            var crateTexture = gl.createTexture();
                            handleLoadedTexture(crateTexture, crate);




                            gl.clearColor(0.0f, 0.0f, 0.0f, 1.0f);
                            gl.enable(gl.DEPTH_TEST);

                            var moonAngle = 180f;
                            var cubeAngle = 0f;

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
                                }
                                lastTime = timeNow;
                            };
                            #endregion


                            //Func<string, f> parseFloat = Convert.ToSingle;
                            Func<string, f> parseFloat =  x => float.Parse(x);

                            #region drawScene
                            Action drawScene = () =>
                            {
                                gl.viewport(0, 0, gl_viewportWidth, gl_viewportHeight);
                                gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

                                __glMatrix.mat4.perspective(45, gl_viewportWidth / gl_viewportHeight, 0.1f, 100.0f, pMatrix);

                                var lighting = toolbar.lighting.@checked;

                                #region [uniform] bool uUseLighting <- lighting
                                gl.uniform1i(shaderProgram_useLightingUniform, Convert.ToInt32(lighting));
                                #endregion

                                if (lighting)
                                {
                                    #region [uniform] vec3 uAmbientColor <- (f ambientR, f ambientG, f ambientB)
                                    gl.uniform3f(
                                        shaderProgram_ambientColorUniform,
                                        parseFloat(toolbar.ambientR.value),
                                        parseFloat(toolbar.ambientG.value),
                                        parseFloat(toolbar.ambientB.value)
                                    );
                                    #endregion

                                    #region [uniform] vec3 uPointLightingLocation <- (f lightPositionX, f lightPositionY, f lightPositionZ)
                                    gl.uniform3f(
                                        shaderProgram_pointLightingLocationUniform,
                                        parseFloat(toolbar.lightPositionX.value),
                                        parseFloat(toolbar.lightPositionY.value),
                                        parseFloat(toolbar.lightPositionZ.value)
                                    );
                                    #endregion

                                    #region [uniform] vec3 uPointLightingColor <- (f pointR, f pointG, f pointB)
                                    gl.uniform3f(
                                        shaderProgram_pointLightingColorUniform,
                                        parseFloat(toolbar.pointR.value),
                                        parseFloat(toolbar.pointG.value),
                                        parseFloat(toolbar.pointB.value)
                                    );
                                    #endregion

                                }

                                __glMatrix.mat4.identity(mvMatrix);

                                __glMatrix.mat4.translate(mvMatrix, 0, 0, -20);

                                #region moon
                                mvPushMatrix();
                                __glMatrix.mat4.rotate(mvMatrix, degToRad(moonAngle), 0, 1, 0);
                                __glMatrix.mat4.translate(mvMatrix, 5, 0, 0);
                                gl.activeTexture(gl.TEXTURE0);
                                gl.bindTexture(gl.TEXTURE_2D, moonTexture);
                                gl.uniform1i(shaderProgram_samplerUniform, 0);

                                gl.bindBuffer(gl.ARRAY_BUFFER, moonVertexPositionBuffer);
                                gl.vertexAttribPointer((uint)shaderProgram_vertexPositionAttribute, moonVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);

                                gl.bindBuffer(gl.ARRAY_BUFFER, moonVertexTextureCoordBuffer);
                                gl.vertexAttribPointer((uint)shaderProgram_textureCoordAttribute, moonVertexTextureCoordBuffer_itemSize, gl.FLOAT, false, 0, 0);

                                gl.bindBuffer(gl.ARRAY_BUFFER, moonVertexNormalBuffer);
                                gl.vertexAttribPointer((uint)shaderProgram_vertexNormalAttribute, moonVertexNormalBuffer_itemSize, gl.FLOAT, false, 0, 0);

                                gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, moonVertexIndexBuffer);
                                setMatrixUniforms();
                                gl.drawElements(gl.TRIANGLES, moonVertexIndexBuffer_numItems, gl.UNSIGNED_SHORT, 0);
                                mvPopMatrix();
                                #endregion

                                #region cube
                                mvPushMatrix();
                                __glMatrix.mat4.rotate(mvMatrix, degToRad(cubeAngle), 0, 1, 0);
                                __glMatrix.mat4.translate(mvMatrix, 5, 0, 0);
                                gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
                                gl.vertexAttribPointer((uint)shaderProgram_vertexPositionAttribute, cubeVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);

                                gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexNormalBuffer);
                                gl.vertexAttribPointer((uint)shaderProgram_vertexNormalAttribute, cubeVertexNormalBuffer_itemSize, gl.FLOAT, false, 0, 0);

                                gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexTextureCoordBuffer);
                                gl.vertexAttribPointer((uint)shaderProgram_textureCoordAttribute, cubeVertexTextureCoordBuffer_itemSize, gl.FLOAT, false, 0, 0);

                                gl.activeTexture(gl.TEXTURE0);
                                gl.bindTexture(gl.TEXTURE_2D, crateTexture);
                                gl.uniform1i(shaderProgram_samplerUniform, 0);

                                gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);
                                setMatrixUniforms();
                                gl.drawElements(gl.TRIANGLES, cubeVertexIndexBuffer_numItems, gl.UNSIGNED_SHORT, 0);
                                mvPopMatrix();
                                #endregion

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
