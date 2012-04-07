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
using WebGLLesson12.Styles;

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
        public Application(IDefaultPage page)
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

            if (page != null)
                page.Toolbar.Orphanize().AttachToDocument();

            if (page == null)
                page = new DefaultPage();

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

                    return null;
                }

                return shader;
            };
            #endregion


            #region init shaders
            var vs = createShader(new Shaders.GeometryVertexShader());
            var fs = createShader(new Shaders.GeometryFragmentShader());

            if (vs == null || fs == null) throw new InvalidOperationException("shader failed");

            var shaderProgram = gl.createProgram();

            gl.attachShader(shaderProgram, vs);
            gl.attachShader(shaderProgram, fs);


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
                        mud =>
                        {


                            var shaderProgram_vertexPositionAttribute = gl.getAttribLocation(shaderProgram, "aVertexPosition");
                            gl.enableVertexAttribArray((ulong)shaderProgram_vertexPositionAttribute);

                            var shaderProgram_textureCoordAttribute = gl.getAttribLocation(shaderProgram, "aTextureCoord");
                            gl.enableVertexAttribArray((ulong)shaderProgram_textureCoordAttribute);

                            var shaderProgram_vertexNormalAttribute = gl.getAttribLocation(shaderProgram, "aVertexNormal");
                            gl.enableVertexAttribArray((ulong)shaderProgram_vertexNormalAttribute);

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








                      








                            //   var cubeVertexPositionBuffer;
                            //   var cubeVertexNormalBuffer;
                            //   var cubeVertexTextureCoordBuffer;
                            //   var cubeVertexIndexBuffer;

                            //   var moonVertexPositionBuffer;
                            //   var moonVertexNormalBuffer;
                            //   var moonVertexTextureCoordBuffer;
                            //   var moonVertexIndexBuffer;

                            //   function initBuffers() {
                            //       cubeVertexPositionBuffer = gl.createBuffer();
                            //       gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
                            //       vertices = [
                            //           // Front face
                            //           -1.0, -1.0,  1.0,
                            //            1.0, -1.0,  1.0,
                            //            1.0,  1.0,  1.0,
                            //           -1.0,  1.0,  1.0,

                            //           // Back face
                            //           -1.0, -1.0, -1.0,
                            //           -1.0,  1.0, -1.0,
                            //            1.0,  1.0, -1.0,
                            //            1.0, -1.0, -1.0,

                            //           // Top face
                            //           -1.0,  1.0, -1.0,
                            //           -1.0,  1.0,  1.0,
                            //            1.0,  1.0,  1.0,
                            //            1.0,  1.0, -1.0,

                            //           // Bottom face
                            //           -1.0, -1.0, -1.0,
                            //            1.0, -1.0, -1.0,
                            //            1.0, -1.0,  1.0,
                            //           -1.0, -1.0,  1.0,

                            //           // Right face
                            //            1.0, -1.0, -1.0,
                            //            1.0,  1.0, -1.0,
                            //            1.0,  1.0,  1.0,
                            //            1.0, -1.0,  1.0,

                            //           // Left face
                            //           -1.0, -1.0, -1.0,
                            //           -1.0, -1.0,  1.0,
                            //           -1.0,  1.0,  1.0,
                            //           -1.0,  1.0, -1.0
                            //       ];
                            //       gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices), gl.STATIC_DRAW);
                            //       cubeVertexPositionBuffer.itemSize = 3;
                            //       cubeVertexPositionBuffer.numItems = 24;

                            //       cubeVertexNormalBuffer = gl.createBuffer();
                            //       gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexNormalBuffer);
                            //       var vertexNormals = [
                            //           // Front face
                            //            0.0,  0.0,  1.0,
                            //            0.0,  0.0,  1.0,
                            //            0.0,  0.0,  1.0,
                            //            0.0,  0.0,  1.0,

                            //           // Back face
                            //            0.0,  0.0, -1.0,
                            //            0.0,  0.0, -1.0,
                            //            0.0,  0.0, -1.0,
                            //            0.0,  0.0, -1.0,

                            //           // Top face
                            //            0.0,  1.0,  0.0,
                            //            0.0,  1.0,  0.0,
                            //            0.0,  1.0,  0.0,
                            //            0.0,  1.0,  0.0,

                            //           // Bottom face
                            //            0.0, -1.0,  0.0,
                            //            0.0, -1.0,  0.0,
                            //            0.0, -1.0,  0.0,
                            //            0.0, -1.0,  0.0,

                            //           // Right face
                            //            1.0,  0.0,  0.0,
                            //            1.0,  0.0,  0.0,
                            //            1.0,  0.0,  0.0,
                            //            1.0,  0.0,  0.0,

                            //           // Left face
                            //           -1.0,  0.0,  0.0,
                            //           -1.0,  0.0,  0.0,
                            //           -1.0,  0.0,  0.0,
                            //           -1.0,  0.0,  0.0,
                            //       ];
                            //       gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertexNormals), gl.STATIC_DRAW);
                            //       cubeVertexNormalBuffer.itemSize = 3;
                            //       cubeVertexNormalBuffer.numItems = 24;

                            //       cubeVertexTextureCoordBuffer = gl.createBuffer();
                            //       gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexTextureCoordBuffer);
                            //       var textureCoords = [
                            //           // Front face
                            //           0.0, 0.0,
                            //           1.0, 0.0,
                            //           1.0, 1.0,
                            //           0.0, 1.0,

                            //           // Back face
                            //           1.0, 0.0,
                            //           1.0, 1.0,
                            //           0.0, 1.0,
                            //           0.0, 0.0,

                            //           // Top face
                            //           0.0, 1.0,
                            //           0.0, 0.0,
                            //           1.0, 0.0,
                            //           1.0, 1.0,

                            //           // Bottom face
                            //           1.0, 1.0,
                            //           0.0, 1.0,
                            //           0.0, 0.0,
                            //           1.0, 0.0,

                            //           // Right face
                            //           1.0, 0.0,
                            //           1.0, 1.0,
                            //           0.0, 1.0,
                            //           0.0, 0.0,

                            //           // Left face
                            //           0.0, 0.0,
                            //           1.0, 0.0,
                            //           1.0, 1.0,
                            //           0.0, 1.0,
                            //       ];
                            //       gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(textureCoords), gl.STATIC_DRAW);
                            //       cubeVertexTextureCoordBuffer.itemSize = 2;
                            //       cubeVertexTextureCoordBuffer.numItems = 24;

                            //       cubeVertexIndexBuffer = gl.createBuffer();
                            //       gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);
                            //       var cubeVertexIndices = [
                            //           0, 1, 2,      0, 2, 3,    // Front face
                            //           4, 5, 6,      4, 6, 7,    // Back face
                            //           8, 9, 10,     8, 10, 11,  // Top face
                            //           12, 13, 14,   12, 14, 15, // Bottom face
                            //           16, 17, 18,   16, 18, 19, // Right face
                            //           20, 21, 22,   20, 22, 23  // Left face
                            //       ];
                            //       gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(cubeVertexIndices), gl.STATIC_DRAW);
                            //       cubeVertexIndexBuffer.itemSize = 1;
                            //       cubeVertexIndexBuffer.numItems = 36;


                            //       var latitudeBands = 30;
                            //       var longitudeBands = 30;
                            //       var radius = 2;

                            //       var vertexPositionData = [];
                            //       var normalData = [];
                            //       var textureCoordData = [];
                            //       for (var latNumber=0; latNumber <= latitudeBands; latNumber++) {
                            //           var theta = latNumber * Math.PI / latitudeBands;
                            //           var sinTheta = Math.sin(theta);
                            //           var cosTheta = Math.cos(theta);

                            //           for (var longNumber=0; longNumber <= longitudeBands; longNumber++) {
                            //               var phi = longNumber * 2 * Math.PI / longitudeBands;
                            //               var sinPhi = Math.sin(phi);
                            //               var cosPhi = Math.cos(phi);

                            //               var x = cosPhi * sinTheta;
                            //               var y = cosTheta;
                            //               var z = sinPhi * sinTheta;
                            //               var u = 1 - (longNumber / longitudeBands);
                            //               var v = 1 - (latNumber / latitudeBands);

                            //               normalData.push(x);
                            //               normalData.push(y);
                            //               normalData.push(z);
                            //               textureCoordData.push(u);
                            //               textureCoordData.push(v);
                            //               vertexPositionData.push(radius * x);
                            //               vertexPositionData.push(radius * y);
                            //               vertexPositionData.push(radius * z);
                            //           }
                            //       }

                            //       var indexData = [];
                            //       for (var latNumber=0; latNumber < latitudeBands; latNumber++) {
                            //           for (var longNumber=0; longNumber < longitudeBands; longNumber++) {
                            //               var first = (latNumber * (longitudeBands + 1)) + longNumber;
                            //               var second = first + longitudeBands + 1;
                            //               indexData.push(first);
                            //               indexData.push(second);
                            //               indexData.push(first + 1);

                            //               indexData.push(second);
                            //               indexData.push(second + 1);
                            //               indexData.push(first + 1);
                            //           }
                            //       }

                            //       moonVertexNormalBuffer = gl.createBuffer();
                            //       gl.bindBuffer(gl.ARRAY_BUFFER, moonVertexNormalBuffer);
                            //       gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(normalData), gl.STATIC_DRAW);
                            //       moonVertexNormalBuffer.itemSize = 3;
                            //       moonVertexNormalBuffer.numItems = normalData.length / 3;

                            //       moonVertexTextureCoordBuffer = gl.createBuffer();
                            //       gl.bindBuffer(gl.ARRAY_BUFFER, moonVertexTextureCoordBuffer);
                            //       gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(textureCoordData), gl.STATIC_DRAW);
                            //       moonVertexTextureCoordBuffer.itemSize = 2;
                            //       moonVertexTextureCoordBuffer.numItems = textureCoordData.length / 2;

                            //       moonVertexPositionBuffer = gl.createBuffer();
                            //       gl.bindBuffer(gl.ARRAY_BUFFER, moonVertexPositionBuffer);
                            //       gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertexPositionData), gl.STATIC_DRAW);
                            //       moonVertexPositionBuffer.itemSize = 3;
                            //       moonVertexPositionBuffer.numItems = vertexPositionData.length / 3;

                            //       moonVertexIndexBuffer = gl.createBuffer();
                            //       gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, moonVertexIndexBuffer);
                            //       gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(indexData), gl.STREAM_DRAW);
                            //       moonVertexIndexBuffer.itemSize = 1;
                            //       moonVertexIndexBuffer.numItems = indexData.length;
                            //   }


                            //   var moonAngle = 180;
                            //   var cubeAngle = 0;

                            //   function drawScene() {
                            //       gl.viewport(0, 0, gl.viewportWidth, gl.viewportHeight);
                            //       gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

                            //       mat4.perspective(45, gl.viewportWidth / gl.viewportHeight, 0.1, 100.0, pMatrix);

                            //       var lighting = document.getElementById("lighting").checked;
                            //       gl.uniform1i(shaderProgram.useLightingUniform, lighting);
                            //       if (lighting) {
                            //           gl.uniform3f(
                            //               shaderProgram.ambientColorUniform,
                            //               parseFloat(document.getElementById("ambientR").value),
                            //               parseFloat(document.getElementById("ambientG").value),
                            //               parseFloat(document.getElementById("ambientB").value)
                            //           );

                            //           gl.uniform3f(
                            //               shaderProgram.pointLightingLocationUniform,
                            //               parseFloat(document.getElementById("lightPositionX").value),
                            //               parseFloat(document.getElementById("lightPositionY").value),
                            //               parseFloat(document.getElementById("lightPositionZ").value)
                            //           );

                            //           gl.uniform3f(
                            //               shaderProgram.pointLightingColorUniform,
                            //               parseFloat(document.getElementById("pointR").value),
                            //               parseFloat(document.getElementById("pointG").value),
                            //               parseFloat(document.getElementById("pointB").value)
                            //           );
                            //       }

                            //       mat4.identity(mvMatrix);

                            //       mat4.translate(mvMatrix, [0, 0, -20]);

                            //       mvPushMatrix();
                            //       mat4.rotate(mvMatrix, degToRad(moonAngle), [0, 1, 0]);
                            //       mat4.translate(mvMatrix, [5, 0, 0]);
                            //       gl.activeTexture(gl.TEXTURE0);
                            //       gl.bindTexture(gl.TEXTURE_2D, moonTexture);
                            //       gl.uniform1i(shaderProgram.samplerUniform, 0);

                            //       gl.bindBuffer(gl.ARRAY_BUFFER, moonVertexPositionBuffer);
                            //       gl.vertexAttribPointer(shaderProgram.vertexPositionAttribute, moonVertexPositionBuffer.itemSize, gl.FLOAT, false, 0, 0);

                            //       gl.bindBuffer(gl.ARRAY_BUFFER, moonVertexTextureCoordBuffer);
                            //       gl.vertexAttribPointer(shaderProgram.textureCoordAttribute, moonVertexTextureCoordBuffer.itemSize, gl.FLOAT, false, 0, 0);

                            //       gl.bindBuffer(gl.ARRAY_BUFFER, moonVertexNormalBuffer);
                            //       gl.vertexAttribPointer(shaderProgram.vertexNormalAttribute, moonVertexNormalBuffer.itemSize, gl.FLOAT, false, 0, 0);

                            //       gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, moonVertexIndexBuffer);
                            //       setMatrixUniforms();
                            //       gl.drawElements(gl.TRIANGLES, moonVertexIndexBuffer.numItems, gl.UNSIGNED_SHORT, 0);
                            //       mvPopMatrix();

                            //       mvPushMatrix();
                            //       mat4.rotate(mvMatrix, degToRad(cubeAngle), [0, 1, 0]);
                            //       mat4.translate(mvMatrix, [5, 0, 0]);
                            //       gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
                            //       gl.vertexAttribPointer(shaderProgram.vertexPositionAttribute, cubeVertexPositionBuffer.itemSize, gl.FLOAT, false, 0, 0);

                            //       gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexNormalBuffer);
                            //       gl.vertexAttribPointer(shaderProgram.vertexNormalAttribute, cubeVertexNormalBuffer.itemSize, gl.FLOAT, false, 0, 0);

                            //       gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexTextureCoordBuffer);
                            //       gl.vertexAttribPointer(shaderProgram.textureCoordAttribute, cubeVertexTextureCoordBuffer.itemSize, gl.FLOAT, false, 0, 0);

                            //       gl.activeTexture(gl.TEXTURE0);
                            //       gl.bindTexture(gl.TEXTURE_2D, crateTexture);
                            //       gl.uniform1i(shaderProgram.samplerUniform, 0);

                            //       gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);
                            //       setMatrixUniforms();
                            //       gl.drawElements(gl.TRIANGLES, cubeVertexIndexBuffer.numItems, gl.UNSIGNED_SHORT, 0);
                            //       mvPopMatrix();
                            //   }


                            //       initBuffers();

                            //function handleLoadedTexture(texture) {
                            //       gl.pixelStorei(gl.UNPACK_FLIP_Y_WEBGL, true);
                            //       gl.bindTexture(gl.TEXTURE_2D, texture);
                            //       gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, texture.image);
                            //       gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, gl.LINEAR);
                            //       gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, gl.LINEAR_MIPMAP_NEAREST);
                            //       gl.generateMipmap(gl.TEXTURE_2D);

                            //       gl.bindTexture(gl.TEXTURE_2D, null);
                            //   }


                            //   var moonTexture;
                            //   var crateTexture;

                            //   function initTextures() {
                            //       moonTexture = gl.createTexture();
                            //       moonTexture.image = new Image();
                            //       moonTexture.image.onload = function () {
                            //           handleLoadedTexture(moonTexture)
                            //       }
                            //       moonTexture.image.src = "moon.gif";

                            //       crateTexture = gl.createTexture();
                            //       crateTexture.image = new Image();
                            //       crateTexture.image.onload = function () {
                            //           handleLoadedTexture(crateTexture)
                            //       }
                            //       crateTexture.image.src = "crate.gif";
                            //   }


                            //       initTextures();

                            gl.clearColor(0.0f, 0.0f, 0.0f, 1.0f);
                            gl.enable(gl.DEPTH_TEST);


                            //   var lastTime = 0;

                            //   function animate() {
                            //       var timeNow = new Date().getTime();
                            //       if (lastTime != 0) {
                            //           var elapsed = timeNow - lastTime;

                            //           moonAngle += 0.05 * elapsed;
                            //           cubeAngle += 0.05 * elapsed;
                            //       }
                            //       lastTime = timeNow;
                            //   }



                            //   function tick() {
                            //       requestAnimFrame(tick);
                            //       drawScene();
                            //       animate();
                            //   }



                            //       tick();



                            #region tick
                            Action tick = null;

                            tick = () =>
                            {
                                if (IsDisposed)
                                    return;


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
