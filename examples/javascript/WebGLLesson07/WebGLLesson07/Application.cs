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
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using WebGLLesson07.Design;
using WebGLLesson07.HTML.Pages;
using WebGLLesson07.Library;
using WebGLLesson07.Shaders;

namespace WebGLLesson07
{

    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;



    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application
    {
        /* This example will be a port of http://learningwebgl.com/blog/?p=684 by Giles
         */

        public readonly ApplicationWebService service = new ApplicationWebService();


        public Action Dispose;


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault  page = null)
        {
            #region glMatrix.js -> InitializeContent
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




            #region initShaders
 
            var shaderProgram = gl.createProgram(
                new GeometryVertexShader(),
                new GeometryFragmentShader()
                );

     
            gl.linkProgram(shaderProgram);
            gl.useProgram(shaderProgram);

            #region getAttribLocation
            Func<string, long> getAttribLocation =
                    name => gl.getAttribLocation(shaderProgram, name);

            var shaderProgram_vertexPositionAttribute = getAttribLocation("aVertexPosition");
            gl.enableVertexAttribArray((uint)shaderProgram_vertexPositionAttribute);

            var shaderProgram_vertexNormalAttribute = getAttribLocation("aVertexNormal");
            gl.enableVertexAttribArray((uint)shaderProgram_vertexNormalAttribute);

            var shaderProgram_textureCoordAttribute = getAttribLocation("aTextureCoord");
            gl.enableVertexAttribArray((uint)shaderProgram_textureCoordAttribute);
            #endregion

            #region getUniformLocation
            Func<string, WebGLUniformLocation> getUniformLocation =
                name => gl.getUniformLocation(shaderProgram, name);

            var shaderProgram_pMatrixUniform = getUniformLocation("uPMatrix");
            var shaderProgram_mvMatrixUniform = getUniformLocation("uMVMatrix");
            var shaderProgram_nMatrixUniform = getUniformLocation("uNMatrix");
            var shaderProgram_samplerUniform = getUniformLocation("uSampler");
            var shaderProgram_useLightingUniform = getUniformLocation("uUseLighting");
            var shaderProgram_ambientColorUniform = getUniformLocation("uAmbientColor");
            var shaderProgram_lightingDirectionUniform = getUniformLocation("uLightingDirection");
            var shaderProgram_directionalColorUniform = getUniformLocation("uDirectionalColor");
            #endregion

            #endregion



            var mvMatrix = __glMatrix.mat4.create();
            var mvMatrixStack = new Stack<Float32Array>();

            var pMatrix = __glMatrix.mat4.create();

            #region new in lesson 03
            Action mvPushMatrix = delegate
            {
                var copy = __glMatrix.mat4.create();
                __glMatrix.mat4.set(mvMatrix, copy);
                mvMatrixStack.Push(copy);
            };

            Action mvPopMatrix = delegate
            {
                mvMatrix = mvMatrixStack.Pop();
            };
            #endregion


            #region setMatrixUniforms
            Action setMatrixUniforms =
                delegate
                {
                    gl.uniformMatrix4fv(shaderProgram_pMatrixUniform, false, pMatrix);
                    gl.uniformMatrix4fv(shaderProgram_mvMatrixUniform, false, mvMatrix);

                    #region new in lesson 07
                    var normalMatrix = __glMatrix.mat3.create();
                    __glMatrix.mat4.toInverseMat3(mvMatrix, normalMatrix);
                    __glMatrix.mat3.transpose(normalMatrix);
                    gl.uniformMatrix3fv(shaderProgram_nMatrixUniform, false, normalMatrix);
                    #endregion
                };
            #endregion




            #region init buffers


            #region cubeVertexPositionBuffer
            var cubeVertexPositionBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
            var vertices = new[]{

                // Front face RED
                -1.0f, -1.0f,  1.0f,
                 1.0f, -1.0f,  1.0f,
                 1.0f,  1.0f,  1.0f,
                -1.0f,  1.0f,  1.0f,

                // Back face YELLOW
                -1.0f, -1.0f, -1.0f,
                -1.0f,  1.0f, -1.0f,
                 1.0f,  1.0f, -1.0f,
                 1.0f, -1.0f, -1.0f,

                // Top face GREEN
                -1.0f,  1.0f, -1.0f,
                -1.0f,  1.0f,  1.0f,
                 1.0f,  1.0f,  1.0f,
                 1.0f,  1.0f, -1.0f,

                // Bottom face BEIGE
                -1.0f, -1.0f, -1.0f,
                 1.0f, -1.0f, -1.0f,
                 1.0f, -1.0f,  1.0f,
                -1.0f, -1.0f,  1.0f,

                // Right face PURPLE
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
            var cubeVertexPositionBuffer_numItems = 6 * 6;
            #endregion

            #region cubeVertexNormalBuffer - new in lesson 07
            var cubeVertexNormalBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexNormalBuffer);
            var vertexNormals = new[]{
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
            var textureCoords = new float[]{
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
            var cubeVertexTextureCoordBuffer_numItems = 24;

            var cubeVertexIndexBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);
            var cubeVertexIndices = new UInt16[]{
                0, 1, 2,      0, 2, 3,    // Front face
                4, 5, 6,      4, 6, 7,    // Back face
                8, 9, 10,     8, 10, 11,  // Top face
                12, 13, 14,   12, 14, 15, // Bottom face
                16, 17, 18,   16, 18, 19, // Right face
                20, 21, 22,   20, 22, 23  // Left face
            };

            gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(cubeVertexIndices), gl.STATIC_DRAW);
            var cubeVertexIndexBuffer_itemSize = 1;
            var cubeVertexIndexBuffer_numItems = cubeVertexPositionBuffer_numItems;

            #endregion

            #endregion


            // initTexture new in lesson 05
            var textures = new[]
            {
                gl.createTexture(),
                gl.createTexture(),
                gl.createTexture(),
            };

            var xRot = 0.0f;
            var xSpeed = 2.0f;

            var yRot = 0.0f;
            var ySpeed = 2.0f;

            var z = -5.0f;

            var filter = 2;

            #region currentlyPressedKeys
            var currentlyPressedKeys = new Dictionary<int, bool>
            {
                {33, false},
                {34, false},
                {37, false},
                {39, false},
                {38, false},
                {40, false}
            };

            Native.Document.onkeydown +=
                e =>
                {
                    currentlyPressedKeys[e.KeyCode] = true;


                    if (e.KeyCode == 13)
                    {
                        filter += 1;
                        if (filter == 3)
                        {
                            filter = 0;
                        }
                    }
                };

            Native.Document.onkeyup +=
               e =>
               {
                   currentlyPressedKeys[e.KeyCode] = false;
               };
            #endregion


         


            new WebGLLesson07.HTML.Images.FromAssets.crate().InvokeOnComplete(
                texture_image =>
                {
                    #region handleLoadedTexture
                    gl.pixelStorei(gl.UNPACK_FLIP_Y_WEBGL, 1);

                    gl.bindTexture(gl.TEXTURE_2D, textures[0]);
                    gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, texture_image);
                    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, (int)gl.NEAREST);
                    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, (int)gl.NEAREST);

                    gl.bindTexture(gl.TEXTURE_2D, textures[1]);
                    gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, texture_image);
                    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, (int)gl.LINEAR);
                    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, (int)gl.LINEAR);

                    gl.bindTexture(gl.TEXTURE_2D, textures[2]);
                    gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, texture_image);
                    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, (int)gl.LINEAR);
                    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, (int)gl.LINEAR_MIPMAP_NEAREST);
                    gl.generateMipmap(gl.TEXTURE_2D);

                    gl.bindTexture(gl.TEXTURE_2D, null);
                    #endregion





                    gl.clearColor(0.0f, 0.0f, 0.0f, 1.0f);
                    gl.enable(gl.DEPTH_TEST);



                    #region animate
                    var lastTime = 0L;
                    Action animate = delegate
                    {
                        var timeNow = new IDate().getTime();
                        if (lastTime != 0)
                        {
                            var elapsed = timeNow - lastTime;

                            xRot += (xSpeed * elapsed) / 1000.0f;
                            yRot += (ySpeed * elapsed) / 1000.0f;
                        }
                        lastTime = timeNow;
                    };
                    #endregion


                    Func<float, float> degToRad = (degrees) =>
                    {
                        return degrees * (f)Math.PI / 180f;
                    };



                    #region drawScene
                    Action drawScene = delegate
                    {
                        gl.viewport(0, 0, gl_viewportWidth, gl_viewportHeight);
                        gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

                        __glMatrix.mat4.perspective(45f, (float)gl_viewportWidth / (float)gl_viewportHeight, 0.1f, 100.0f, pMatrix);

                        __glMatrix.mat4.identity(mvMatrix);


                        __glMatrix.mat4.translate(mvMatrix, new float[] { 0.0f, 0.0f, z });

                        __glMatrix.mat4.rotate(mvMatrix, degToRad(xRot), new[] { 1f, 0f, 0f });
                        __glMatrix.mat4.rotate(mvMatrix, degToRad(yRot), new[] { 0f, 1f, 0f });


                        gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
                        gl.vertexAttribPointer((uint)shaderProgram_vertexPositionAttribute, cubeVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);

                        #region new in lesson 07
                        gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexNormalBuffer);
                        gl.vertexAttribPointer((uint)shaderProgram_vertexNormalAttribute, cubeVertexNormalBuffer_itemSize, gl.FLOAT, false, 0, 0);
                        #endregion

                        gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexTextureCoordBuffer);
                        gl.vertexAttribPointer((uint)shaderProgram_textureCoordAttribute, cubeVertexTextureCoordBuffer_itemSize, gl.FLOAT, false, 0, 0);


                        gl.activeTexture(gl.TEXTURE0);
                        gl.bindTexture(gl.TEXTURE_2D, textures[filter]);
                        gl.uniform1i(shaderProgram_samplerUniform, 0);

                        #region new in lesson 07
                        var lighting = toolbar.lighting.@checked;
                        gl.uniform1i(shaderProgram_useLightingUniform, lighting.ToInt32());
                        if (lighting)
                        {
                            gl.uniform3f(
                                shaderProgram_ambientColorUniform,
                                toolbar.ambientR.ToFloat(),
                                toolbar.ambientG.ToFloat(),
                                toolbar.ambientB.ToFloat()
                            );

                            var lightingDirection = new[]{
                                toolbar.lightDirectionX.ToFloat(),
                                toolbar.lightDirectionY.ToFloat(),
                                toolbar.lightDirectionZ.ToFloat()
                            };
                            var adjustedLD = __glMatrix.vec3.create();
                            __glMatrix.vec3.normalize(lightingDirection, adjustedLD);
                            __glMatrix.vec3.scale(adjustedLD, -1);
                            gl.uniform3fv(shaderProgram_lightingDirectionUniform, adjustedLD);

                            gl.uniform3f(
                                shaderProgram_directionalColorUniform,
                                toolbar.directionalR.ToFloat(),
                                toolbar.directionalG.ToFloat(),
                                toolbar.directionalB.ToFloat()
                            );
                        }

                        #endregion

                        gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);
                        setMatrixUniforms();
                        gl.drawElements(gl.TRIANGLES, cubeVertexIndexBuffer_numItems, gl.UNSIGNED_SHORT, 0);



                    };
                    drawScene();
                    #endregion



                    #region handleKeys
                    Action handleKeys =
                        delegate
                        {
                            if (currentlyPressedKeys[33])
                            {
                                // Page Up
                                z -= 0.05f;
                            }
                            if (currentlyPressedKeys[34])
                            {
                                // Page Down
                                z += 0.05f;
                            }
                            if (currentlyPressedKeys[37])
                            {
                                // Left cursor key
                                ySpeed -= 1f;
                            }
                            if (currentlyPressedKeys[39])
                            {
                                // Right cursor key
                                ySpeed += 1f;
                            }
                            if (currentlyPressedKeys[38])
                            {
                                // Up cursor key
                                xSpeed -= 1f;
                            }
                            if (currentlyPressedKeys[40])
                            {
                                // Down cursor key
                                xSpeed += 1f;
                            }
                        };
                    #endregion




                    #region tick
                    var c = 0;
                    var tick = default(Action);

                    tick = delegate
                    {
                        c++;

                        Native.Document.title = "" + new { c, filter };

                        handleKeys();
                        drawScene();
                        animate();

                        Native.Window.requestAnimationFrame += tick;
                    };

                    tick();
                    #endregion

                }
            );
        }

    }


}
