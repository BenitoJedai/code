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
using WebGLLesson15.Design;
using WebGLLesson15.HTML.Pages;
using ScriptCoreLib.GLSL;
using System.ComponentModel;


namespace WebGLLesson15
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;

    [Description("This type will run as JavaScript.")]
    public sealed class Application
    {
        // based on http://learningwebgl.com/lessons/lesson15/index.html

        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly DefaultStyle style = new DefaultStyle();

        public Action Dispose;


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page = null)
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

            #region toolbar
            var toolbar = new Toolbar();

            if (page != null)
            {
                toolbar.Container.AttachToDocument();
                toolbar.Container.style.Opacity = 0.7;
                toolbar.HideButton.onclick +=
                 delegate
                 {
                     // ScriptCoreLib.Extensions
                     toolbar.HideTarget.ToggleVisible();
                 };
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
                    gl.createProgram(
                            new Shaders.PerFragmentLightingVertexShader(),
                            new Shaders.PerFragmentLightingFragmentShader()
                    )

                }.Select(
                    shaderProgram =>
                    {
                        gl.linkProgram(shaderProgram);


                        var vertexPositionAttribute = gl.getAttribLocation(shaderProgram, "aVertexPosition");
                        gl.enableVertexAttribArray((uint)vertexPositionAttribute);

                        var vertexNormalAttribute = gl.getAttribLocation(shaderProgram, "aVertexNormal");
                        gl.enableVertexAttribArray((uint)vertexNormalAttribute);

                        var textureCoordAttribute = gl.getAttribLocation(shaderProgram, "aTextureCoord");
                        gl.enableVertexAttribArray((uint)textureCoordAttribute);

                        var pMatrixUniform = gl.getUniformLocation(shaderProgram, "uPMatrix");
                        var mvMatrixUniform = gl.getUniformLocation(shaderProgram, "uMVMatrix");
                        var nMatrixUniform = gl.getUniformLocation(shaderProgram, "uNMatrix");
                        var colorMapSamplerUniform = gl.getUniformLocation(shaderProgram, "uColorMapSampler");
                        var specularMapSamplerUniform = gl.getUniformLocation(shaderProgram, "uSpecularMapSampler");
                        var useColorMapUniform = gl.getUniformLocation(shaderProgram, "uUseColorMap");
                        var useSpecularMapUniform = gl.getUniformLocation(shaderProgram, "uUseSpecularMap");
                        var useLightingUniform = gl.getUniformLocation(shaderProgram, "uUseLighting");

                        var ambientColorUniform = gl.getUniformLocation(shaderProgram, "uAmbientColor");
                        var pointLightingLocationUniform = gl.getUniformLocation(shaderProgram, "uPointLightingLocation");
                        var pointLightingSpecularColorUniform = gl.getUniformLocation(shaderProgram, "uPointLightingSpecularColor");
                        var pointLightingDiffuseColorUniform = gl.getUniformLocation(shaderProgram, "uPointLightingDiffuseColor");

                        return new
                        {
                            program = shaderProgram,

                            vertexPositionAttribute,
                            vertexNormalAttribute,
                            textureCoordAttribute,

                            pMatrixUniform,
                            mvMatrixUniform,
                            nMatrixUniform,
                            colorMapSamplerUniform,
                            specularMapSamplerUniform,
                            useColorMapUniform,
                            useSpecularMapUniform,
                            useLightingUniform,

                            ambientColorUniform,
                            pointLightingLocationUniform,
                            pointLightingSpecularColorUniform,
                            pointLightingDiffuseColorUniform

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

            // await earth
            new HTML.Images.FromAssets.earth().InvokeOnComplete(
                earth =>
                    // await earth_specular
                    new HTML.Images.FromAssets.earth_specular().InvokeOnComplete(
                        earth_specular =>
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
                                gl.generateMipmap(gl.TEXTURE_2D);

                                gl.bindTexture(gl.TEXTURE_2D, null);
                            };
                            #endregion

                            var earthColorMapTexture = gl.createTexture();
                            handleLoadedTexture(earthColorMapTexture, earth);

                            var earthSpecularMapTexture = gl.createTexture();
                            handleLoadedTexture(earthSpecularMapTexture, earth_specular);

                            #region initBuffers
                            var latitudeBands = 30;
                            var longitudeBands = 30;
                            var radius = 13;

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

                            var sphereVertexNormalBuffer = gl.createBuffer();
                            gl.bindBuffer(gl.ARRAY_BUFFER, sphereVertexNormalBuffer);
                            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(normalData.ToArray()), gl.STATIC_DRAW);
                            var sphereVertexNormalBuffer_itemSize = 3;
                            var sphereVertexNormalBuffer_numItems = normalData.Count / 3;

                            var sphereVertexTextureCoordBuffer = gl.createBuffer();
                            gl.bindBuffer(gl.ARRAY_BUFFER, sphereVertexTextureCoordBuffer);
                            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(textureCoordData.ToArray()), gl.STATIC_DRAW);
                            var sphereVertexTextureCoordBuffer_itemSize = 2;
                            var sphereVertexTextureCoordBuffer_numItems = textureCoordData.Count / 2;

                            var sphereVertexPositionBuffer = gl.createBuffer();
                            gl.bindBuffer(gl.ARRAY_BUFFER, sphereVertexPositionBuffer);
                            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertexPositionData.ToArray()), gl.STATIC_DRAW);
                            var sphereVertexPositionBuffer_itemSize = 3;
                            var sphereVertexPositionBuffer_numItems = vertexPositionData.Count / 3;

                            var sphereVertexIndexBuffer = gl.createBuffer();
                            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, sphereVertexIndexBuffer);
                            gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(indexData.ToArray()), gl.STREAM_DRAW);
                            var sphereVertexIndexBuffer_itemSize = 1;
                            var sphereVertexIndexBuffer_numItems = indexData.Count;
                            #endregion



                            gl.clearColor(0.0f, 0.0f, 0.0f, 1.0f);
                            gl.enable(gl.DEPTH_TEST);

                            var earthAngle = 180f;

                            var lastTime = 0L;

                            #region animate
                            Action animate = () =>
                            {
                                var timeNow = new IDate().getTime();
                                if (lastTime != 0)
                                {
                                    var elapsed = timeNow - lastTime;

                                    earthAngle += 0.05f * elapsed;
                                }
                                lastTime = timeNow;
                            };
                            #endregion



                            //Func<string, f> parseFloat = Convert.ToSingle;
                            //Func<string, f> parseFloat = x => float.Parse(x);


                            #region drawScene
                            Action drawScene = () =>
                            {
                                var shaderProgram = currentProgram;

                                gl.useProgram(shaderProgram.program);

                                gl.viewport(0, 0, gl_viewportWidth, gl_viewportHeight);
                                gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

                                glMatrix.mat4.perspective(45, gl_viewportWidth / gl_viewportHeight, 0.1f, 100.0f, pMatrix);

                                #region [uniform] uUseColorMap <- color_map
                                var useColorMap = toolbar.color_map.@checked;
                                gl.uniform1i(shaderProgram.useColorMapUniform, Convert.ToInt32(useColorMap));
                                #endregion

                                #region [uniform] uUseSpecularMap <- specular_map
                                var useSpecularMap = toolbar.specular_map.@checked;
                                gl.uniform1i(shaderProgram.useSpecularMapUniform, Convert.ToInt32(useSpecularMap));
                                #endregion

                                #region [uniform] uUseLighting <- lighting
                                var lighting = toolbar.lighting.@checked;
                                gl.uniform1i(shaderProgram.useLightingUniform, Convert.ToInt32(lighting));
                                #endregion


                                if (lighting)
                                {
                                    #region [uniform] uAmbientColor <- ambientR, ambientG, ambientB
                                    gl.uniform3f(
                                        shaderProgram.ambientColorUniform,
                                        float.Parse(toolbar.ambientR.value),
                                        float.Parse(toolbar.ambientG.value),
                                        float.Parse(toolbar.ambientB.value)
                                    );
                                    #endregion


                                    #region [uniform] uPointLightingLocation <- lightPositionX, lightPositionY, lightPositionZ
                                    gl.uniform3f(
                                        shaderProgram.pointLightingLocationUniform,
                                        float.Parse(toolbar.lightPositionX.value),
                                        float.Parse(toolbar.lightPositionY.value),
                                        float.Parse(toolbar.lightPositionZ.value)
                                    );
                                    #endregion


                                    #region [uniform] uPointLightingSpecularColor <- specularR, specularG, specularB
                                    gl.uniform3f(
                                        shaderProgram.pointLightingSpecularColorUniform,
                                        float.Parse(toolbar.specularR.value),
                                        float.Parse(toolbar.specularG.value),
                                        float.Parse(toolbar.specularB.value)
                                    );
                                    #endregion

                                    #region [uniform] uPointLightingDiffuseColor <- diffuseR, diffuseG, diffuseB
                                    gl.uniform3f(
                                        shaderProgram.pointLightingDiffuseColorUniform,
                                        float.Parse(toolbar.diffuseR.value),
                                        float.Parse(toolbar.diffuseG.value),
                                        float.Parse(toolbar.diffuseB.value)
                                    );
                                    #endregion

                                }

                                glMatrix.mat4.identity(mvMatrix);

                                glMatrix.mat4.translate(mvMatrix, new f[] { 0, 0, -40 });
                                glMatrix.mat4.rotate(mvMatrix, degToRad(23.4f), new f[] { 1, 0, -1 });
                                glMatrix.mat4.rotate(mvMatrix, degToRad(earthAngle), new f[] { 0, 1, 0 });

                                gl.activeTexture(gl.TEXTURE0);
                                gl.bindTexture(gl.TEXTURE_2D, earthColorMapTexture);
                                gl.uniform1i(shaderProgram.colorMapSamplerUniform, 0);

                                gl.activeTexture(gl.TEXTURE1);
                                gl.bindTexture(gl.TEXTURE_2D, earthSpecularMapTexture);
                                gl.uniform1i(shaderProgram.specularMapSamplerUniform, 1);

                                gl.bindBuffer(gl.ARRAY_BUFFER, sphereVertexPositionBuffer);
                                gl.vertexAttribPointer((uint)shaderProgram.vertexPositionAttribute, sphereVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);

                                gl.bindBuffer(gl.ARRAY_BUFFER, sphereVertexTextureCoordBuffer);
                                gl.vertexAttribPointer((uint)shaderProgram.textureCoordAttribute, sphereVertexTextureCoordBuffer_itemSize, gl.FLOAT, false, 0, 0);

                                gl.bindBuffer(gl.ARRAY_BUFFER, sphereVertexNormalBuffer);
                                gl.vertexAttribPointer((uint)shaderProgram.vertexNormalAttribute, sphereVertexNormalBuffer_itemSize, gl.FLOAT, false, 0, 0);

                                gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, sphereVertexIndexBuffer);
                                setMatrixUniforms();
                                gl.drawElements(gl.TRIANGLES, sphereVertexIndexBuffer_numItems, gl.UNSIGNED_SHORT, 0);

                            };
                            #endregion





                            Native.window.onframe += delegate
                            {
                                if (IsDisposed)
                                    return;


                                animate();
                                drawScene();


                            };



                        }
                    )

            );
        }

    }


}
