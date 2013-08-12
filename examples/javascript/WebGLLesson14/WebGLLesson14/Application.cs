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
using WebGLLesson14.Design;
using WebGLLesson14.HTML.Pages;
using ScriptCoreLib.GLSL;
using System.ComponentModel;


namespace WebGLLesson14
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;

    [Description("This type will run as JavaScript.")]
    public sealed class Application
    {
        // based on view-source:http://learningwebgl.com/lessons/lesson14/index.html

        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly DefaultStyle style = new DefaultStyle();

        public Action Dispose;

        public sealed class TeapotType
        {
            public f[] vertexNormals;
            public f[] vertexPositions;
            public f[] vertexTextureCoords;
            public ushort[] indices;
        }

        [Script(ExternalTarget = "Teapot")]
        public static TeapotType Teapot;

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page = null)
        {
            #region await __glMatrix
            new __glMatrix().Content.With(
              source =>
              {
                  source.onload +=
                    delegate
                    {
                        #region await Teapot
                        new WebGLLesson14.Data.Teapot().Content.With(
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
                toolbar.Container.AttachToDocument();
                toolbar.Container.style.Opacity = 0.7;
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
                    gl.createProgram(new Shaders.PerFragmentLightingVertexShader(),
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
                        var samplerUniform = gl.getUniformLocation(shaderProgram, "uSampler");
                        var materialShininessUniform = gl.getUniformLocation(shaderProgram, "uMaterialShininess");
                        var showSpecularHighlightsUniform = gl.getUniformLocation(shaderProgram, "uShowSpecularHighlights");
                        var useTexturesUniform = gl.getUniformLocation(shaderProgram, "uUseTextures");
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
                            samplerUniform,
                           materialShininessUniform ,
                           showSpecularHighlightsUniform ,
                           useTexturesUniform ,
                           useLightingUniform ,
                           ambientColorUniform ,
                           pointLightingLocationUniform , 
                           pointLightingSpecularColorUniform,
                           pointLightingDiffuseColorUniform






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

            // await earth
            new HTML.Images.FromAssets.earth().InvokeOnComplete(
                earth =>
                    // await metail
                    new HTML.Images.FromAssets.arroway_de_metal_structure_06_d100_flat().InvokeOnComplete(
                        metal =>
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
                                gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, (int)gl.LINEAR);
                                gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, (int)gl.LINEAR_MIPMAP_NEAREST);
                                gl.generateMipmap(gl.TEXTURE_2D);

                                gl.bindTexture(gl.TEXTURE_2D, null);
                            };
                            #endregion



              
                              var earthTexture = gl.createTexture();
                              handleLoadedTexture(earthTexture, earth);

                                 
                              var galvanizedTexture = gl.createTexture();
                              handleLoadedTexture(galvanizedTexture, metal);


                              #region loadTeapot
                              var teapotData = Application.Teapot;

                            var teapotVertexNormalBuffer = gl.createBuffer();
                            gl.bindBuffer(gl.ARRAY_BUFFER, teapotVertexNormalBuffer);
                            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(teapotData.vertexNormals), gl.STATIC_DRAW);
                            var teapotVertexNormalBuffer_itemSize = 3;
                            var teapotVertexNormalBuffer_numItems = teapotData.vertexNormals.Length / 3;

                            var teapotVertexTextureCoordBuffer = gl.createBuffer();
                            gl.bindBuffer(gl.ARRAY_BUFFER, teapotVertexTextureCoordBuffer);
                            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(teapotData.vertexTextureCoords), gl.STATIC_DRAW);
                            var teapotVertexTextureCoordBuffer_itemSize = 2;
                            var teapotVertexTextureCoordBuffer_numItems = teapotData.vertexTextureCoords.Length / 2;

                            var teapotVertexPositionBuffer = gl.createBuffer();
                            gl.bindBuffer(gl.ARRAY_BUFFER, teapotVertexPositionBuffer);
                            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(teapotData.vertexPositions), gl.STATIC_DRAW);
                            var teapotVertexPositionBuffer_itemSize = 3;
                            var teapotVertexPositionBuffer_numItems = teapotData.vertexPositions.Length / 3;

                            var teapotVertexIndexBuffer = gl.createBuffer();
                            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, teapotVertexIndexBuffer);
                            gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(teapotData.indices), gl.STATIC_DRAW);
                            var teapotVertexIndexBuffer_itemSize = 1;
                            var teapotVertexIndexBuffer_numItems = teapotData.indices.Length;

                              #endregion







                            gl.clearColor(0.0f, 0.0f, 0.0f, 1.0f);
                            gl.enable(gl.DEPTH_TEST);

                            var teapotAngle = 180f;

                            var lastTime = 0L;

                            #region animate
                            Action animate = () =>
                            {
                                var timeNow = new IDate().getTime();
                                if (lastTime != 0)
                                {
                                    var elapsed = timeNow - lastTime;

                                    teapotAngle += 0.05f * elapsed;
                                }
                                lastTime = timeNow;
                            };
                            #endregion

                           

                            //Func<string, f> parseFloat = Convert.ToSingle;
                            Func<string, f> parseFloat = x => float.Parse(x);


                            #region drawScene
                            Action drawScene = () =>
                            {
                                gl.useProgram(currentProgram.program);

                                gl.viewport(0, 0, gl_viewportWidth, gl_viewportHeight);
                                gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

                                //if (teapotVertexPositionBuffer == null || teapotVertexNormalBuffer == null || teapotVertexTextureCoordBuffer == null || teapotVertexIndexBuffer == null) {
                                //    return;
                                //}

                                __glMatrix.mat4.perspective(45, gl_viewportWidth / gl_viewportHeight, 0.1f, 100.0f, pMatrix);

                                var shaderProgram = currentProgram;

                                var specularHighlights = toolbar.specular.@checked;

                                #region [uniform] bool uShowSpecularHighlights <-  toolbar.specular.@checked
                                gl.uniform1i(shaderProgram.showSpecularHighlightsUniform, Convert.ToInt32( specularHighlights));
                                #endregion

                                var lighting = toolbar.lighting.@checked;

                                #region [uniform] bool uUseLighting <- toolbar.lighting.@checked
                                gl.uniform1i(shaderProgram.useLightingUniform, Convert.ToInt32( lighting));
                                #endregion

                                if (lighting)
                                {

                                    #region [uniform] uAmbientColor <- ambientR, ambientG, ambientB
                                    gl.uniform3f(
                                        shaderProgram.ambientColorUniform,
                                        parseFloat(toolbar.ambientR.value),
                                        parseFloat(toolbar.ambientG.value),
                                        parseFloat(toolbar.ambientB.value)
                                    );
                                    #endregion

                                    #region [uniform] uPointLightingLocation <- lightPositionX, lightPositionY, lightPositionZ
                                    gl.uniform3f(
                                        shaderProgram.pointLightingLocationUniform,
                                        parseFloat(toolbar.lightPositionX.value),
                                        parseFloat(toolbar.lightPositionY.value),
                                        parseFloat(toolbar.lightPositionZ.value)
                                    );
                                    #endregion

                                    #region [uniform] uPointLightingSpecularColor <- specularR, specularG, specularB
                                    gl.uniform3f(
                                        shaderProgram.pointLightingSpecularColorUniform,
                                        parseFloat(toolbar.specularR.value),
                                        parseFloat(toolbar.specularG.value),
                                        parseFloat(toolbar.specularB.value)
                                    );
                                    #endregion

                                    #region [uniform] uPointLightingDiffuseColor <- diffuseR, diffuseG, diffuseB
                                    gl.uniform3f(
                                        shaderProgram.pointLightingDiffuseColorUniform,
                                        parseFloat(toolbar.diffuseR.value),
                                        parseFloat(toolbar.diffuseG.value),
                                        parseFloat(toolbar.diffuseB.value)
                                    );
                                    #endregion

                                }

                                var texture = toolbar.texture[ toolbar.texture.selectedIndex].value;
                                gl.uniform1i(shaderProgram.useTexturesUniform, Convert.ToInt32( texture != "none"));

                                __glMatrix.mat4.identity(mvMatrix);

                                __glMatrix.mat4.translate(mvMatrix, 0, 0, -40);
                                __glMatrix.mat4.rotate(mvMatrix, degToRad(23.4f), 1, 0, -1);
                                __glMatrix.mat4.rotate(mvMatrix, degToRad(teapotAngle), 0, 1, 0);

                                gl.activeTexture(gl.TEXTURE0);
                                if (texture == "earth") {
                                    gl.bindTexture(gl.TEXTURE_2D, earthTexture);
                                } else if (texture == "galvanized") {
                                    gl.bindTexture(gl.TEXTURE_2D, galvanizedTexture);
                                }
                                gl.uniform1i(shaderProgram.samplerUniform, 0);

                                gl.uniform1f(shaderProgram.materialShininessUniform, parseFloat(toolbar.shininess.value));

                                gl.bindBuffer(gl.ARRAY_BUFFER, teapotVertexPositionBuffer);
                                gl.vertexAttribPointer((uint)shaderProgram.vertexPositionAttribute, teapotVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);

                                gl.bindBuffer(gl.ARRAY_BUFFER, teapotVertexTextureCoordBuffer);
                                gl.vertexAttribPointer((uint)shaderProgram.textureCoordAttribute, teapotVertexTextureCoordBuffer_itemSize, gl.FLOAT, false, 0, 0);

                                gl.bindBuffer(gl.ARRAY_BUFFER, teapotVertexNormalBuffer);
                                gl.vertexAttribPointer((uint)shaderProgram.vertexNormalAttribute, teapotVertexNormalBuffer_itemSize, gl.FLOAT, false, 0, 0);

                                gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, teapotVertexIndexBuffer);
                                setMatrixUniforms();
                                gl.drawElements(gl.TRIANGLES, teapotVertexIndexBuffer_numItems, gl.UNSIGNED_SHORT, 0);


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
