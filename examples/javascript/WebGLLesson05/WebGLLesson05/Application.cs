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
using WebGLLesson05.Design;
using WebGLLesson05.HTML.Pages;
using WebGLLesson05.Shaders;

namespace WebGLLesson05
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;



    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application
    {
        /* This example will be a port of http://learningwebgl.com/blog/?p=370 by Giles
         * 
         * 01. Created a new project of type Web Application
         * 02. initGL
         * 03. initShaders
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

            #region initShaders
            var shaderProgram = gl.createProgram(
                new GeometryVertexShader(),
                new GeometryFragmentShader()
            );

        
            gl.linkProgram(shaderProgram);
            gl.useProgram(shaderProgram);

            var shaderProgram_vertexPositionAttribute = gl.getAttribLocation(shaderProgram, "aVertexPosition");
            gl.enableVertexAttribArray((uint)shaderProgram_vertexPositionAttribute);
            
            var shaderProgram_textureCoordAttribute = gl.getAttribLocation(shaderProgram, "aTextureCoord");
            gl.enableVertexAttribArray((uint)shaderProgram_textureCoordAttribute);

            var shaderProgram_pMatrixUniform = gl.getUniformLocation(shaderProgram, "uPMatrix");
            var shaderProgram_mvMatrixUniform = gl.getUniformLocation(shaderProgram, "uMVMatrix");

            // new in lesson 05
            var shaderProgram_samplerUniform = gl.getUniformLocation(shaderProgram, "uSampler");
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
            var neheTexture = gl.createTexture();
            var neheTexture_image = new WebGLLesson05.HTML.Images.FromAssets.nehe();

            neheTexture_image.InvokeOnComplete(
                delegate
                {
                    gl.bindTexture(gl.TEXTURE_2D, neheTexture);
                    #region with neheTexture
                    gl.pixelStorei(gl.UNPACK_FLIP_Y_WEBGL, 1);
                    gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, neheTexture_image);
                    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, (int)gl.NEAREST);
                    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, (int)gl.NEAREST);
                    #endregion
                    gl.bindTexture(gl.TEXTURE_2D, null);




                    gl.clearColor(0.0f, 0.0f, 0.0f, 1.0f);
                    gl.enable(gl.DEPTH_TEST);

                    #region new in lesson 04

                    var xRot = 0.0f;
                    var yRot = 0.0f;
                    var zRot = 0.0f;
                    var lastTime = 0L;
                    Action animate = delegate
                    {
                        var timeNow = new IDate().getTime();
                        if (lastTime != 0)
                        {
                            var elapsed = timeNow - lastTime;

                            xRot += (90 * elapsed) / 1000.0f;
                            yRot += (90 * elapsed) / 1000.0f;
                            zRot += (90 * elapsed) / 1000.0f;
                        }
                        lastTime = timeNow;
                    };

                    Func<float, float> degToRad = (degrees) =>
                    {
                        return degrees * (f)Math.PI / 180f;
                    };
                    #endregion



                    #region drawScene
                    Action drawScene = delegate
                    {
                        gl.viewport(0, 0, gl_viewportWidth, gl_viewportHeight);
                        gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

                        __glMatrix.mat4.perspective(45f, (float)gl_viewportWidth / (float)gl_viewportHeight, 0.1f, 100.0f, pMatrix);

                        __glMatrix.mat4.identity(mvMatrix);


                        __glMatrix.mat4.translate(mvMatrix, new float[] { 0.0f, 0.0f, -5.0f });

                        __glMatrix.mat4.rotate(mvMatrix, degToRad(xRot), new[] { 1f, 0f, 0f });
                        __glMatrix.mat4.rotate(mvMatrix, degToRad(yRot), new[] { 0f, 1f, 0f });
                        __glMatrix.mat4.rotate(mvMatrix, degToRad(zRot), new[] { 0f, 0f, 1f });


                        gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
                        gl.vertexAttribPointer((uint)shaderProgram_vertexPositionAttribute, cubeVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);

                        gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexTextureCoordBuffer);
                        gl.vertexAttribPointer((uint)shaderProgram_textureCoordAttribute, cubeVertexTextureCoordBuffer_itemSize, gl.FLOAT, false, 0, 0);


                        gl.activeTexture(gl.TEXTURE0);
                        gl.bindTexture(gl.TEXTURE_2D, neheTexture);
                        gl.uniform1i(shaderProgram_samplerUniform, 0);


                        gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);
                        setMatrixUniforms();
                        gl.drawElements(gl.TRIANGLES, cubeVertexIndexBuffer_numItems, gl.UNSIGNED_SHORT, 0);

                  

                    };
                    drawScene();
                    #endregion

                 


                    var c = 0;

                    #region tick - new in lesson 03
                    var tick = default(Action);

                    tick = delegate
                    {
                        c++;

                        Native.Document.title = "" + c;

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
