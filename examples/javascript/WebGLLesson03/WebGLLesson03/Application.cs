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
using WebGLLesson03.Design;
using WebGLLesson03.HTML.Pages;
using WebGLLesson03.Shaders;

namespace WebGLLesson03
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;



    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application
    {
        /* This example will be a port of http://learningwebgl.com/blog/?p=239 by Giles
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
        public Application(IDefaultPage page = null)
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

            var shaderProgram = gl.createProgram();

            var vs = createShader(new GeometryVertexShader());
            var fs = createShader(new GeometryFragmentShader());


            gl.attachShader(shaderProgram, vs);
            gl.attachShader(shaderProgram, fs);


            gl.linkProgram(shaderProgram);
            gl.useProgram(shaderProgram);

            var shaderProgram_vertexPositionAttribute = gl.getAttribLocation(shaderProgram, "aVertexPosition");

            gl.enableVertexAttribArray((uint)shaderProgram_vertexPositionAttribute);

            // new in lesson 02
            var shaderProgram_vertexColorAttribute = gl.getAttribLocation(shaderProgram, "aVertexColor");
            gl.enableVertexAttribArray((uint)shaderProgram_vertexColorAttribute);

            var shaderProgram_pMatrixUniform = gl.getUniformLocation(shaderProgram, "uPMatrix");
            var shaderProgram_mvMatrixUniform = gl.getUniformLocation(shaderProgram, "uMVMatrix");



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
            var triangleVertexPositionBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, triangleVertexPositionBuffer);
            var vertices = new[]{
                     0.0f,  1.0f,  0.0f,
                    -1.0f, -1.0f,  0.0f,
                     1.0f, -1.0f,  0.0f
                };
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices), gl.STATIC_DRAW);
            var triangleVertexPositionBuffer_itemSize = 3;
            var triangleVertexPositionBuffer_numItems = 3;

            #region new in lesson 02

            var triangleVertexColorBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, triangleVertexColorBuffer);

            var colors = new[]{
                1.0f, 0.0f, 0.0f, 1.0f,
                0.0f, 1.0f, 0.0f, 1.0f,
                0.0f, 0.0f, 1.0f, 1.0f
            };
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(colors), gl.STATIC_DRAW);
            var triangleVertexColorBuffer_itemSize = 4;
            var triangleVertexColorBuffer_numItems = 3;
            #endregion


            var squareVertexPositionBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, squareVertexPositionBuffer);
            vertices = new[]{
                     1.0f,  1.0f,  0.0f,
                    -1.0f,  1.0f,  0.0f,
                     1.0f, -1.0f,  0.0f,
                    -1.0f, -1.0f,  0.0f
                };
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices), gl.STATIC_DRAW);

            var squareVertexPositionBuffer_itemSize = 3;
            var squareVertexPositionBuffer_numItems = 4;

            #region new in lesson 02
            var squareVertexColorBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, squareVertexColorBuffer);
            #region loop unrolled :)
            colors = new[]{
                0.5f, 0.5f, 1.0f, 1.0f,
                0.5f, 0.5f, 1.0f, 1.0f,
                0.5f, 0.5f, 1.0f, 1.0f,
                0.5f, 0.5f, 1.0f, 1.0f
            };
            #endregion



            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(colors), gl.STATIC_DRAW);
            var squareVertexColorBuffer_itemSize = 4;
            var squareVertexColorBuffer_numItems = 4;
            #endregion

            #endregion




            gl.clearColor(0.0f, 0.0f, 0.0f, 1.0f);
            gl.enable(gl.DEPTH_TEST);

            #region new in lesson 03

            var rTri = 0f;
            var rSquare = 0f;

            var lastTime = 0L;
            Action animate = delegate
            {
                var timeNow = new IDate().getTime();
                if (lastTime != 0)
                {
                    var elapsed = timeNow - lastTime;

                    rTri += (90 * elapsed) / 1000.0f;
                    rSquare += (75 * elapsed) / 1000.0f;
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

                __glMatrix.mat4.translate(mvMatrix, new float[] { -1.5f, 0.0f, -7.0f });

                #region new in lesson 03
                mvPushMatrix();
                // we’re changing our current rotation state as stored in the model-view matrix
                // MVC? :)
                __glMatrix.mat4.rotate(mvMatrix, degToRad(rTri), new float[] { 0f, 1f, 0f });
                #endregion

                gl.bindBuffer(gl.ARRAY_BUFFER, triangleVertexPositionBuffer);
                gl.vertexAttribPointer((uint)shaderProgram_vertexPositionAttribute, triangleVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);


                #region new in lesson 02
                gl.bindBuffer(gl.ARRAY_BUFFER, triangleVertexColorBuffer);
                gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, triangleVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);

                #endregion



                setMatrixUniforms();
                gl.drawArrays(gl.TRIANGLES, 0, triangleVertexPositionBuffer_numItems);

                #region new in lesson 03
                mvPopMatrix();
                #endregion

                __glMatrix.mat4.translate(mvMatrix, new float[] { 3.0f, 0.0f, 0.0f });

                #region new in lesson 03
                mvPushMatrix();
                __glMatrix.mat4.rotate(mvMatrix, degToRad(rSquare), new float[] { 1f, 0f, 0f });
                #endregion


                gl.bindBuffer(gl.ARRAY_BUFFER, squareVertexPositionBuffer);
                gl.vertexAttribPointer((uint)shaderProgram_vertexPositionAttribute, squareVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);

                #region new in lesson 02
                gl.bindBuffer(gl.ARRAY_BUFFER, squareVertexColorBuffer);
                gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, squareVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);

                #endregion


                setMatrixUniforms();
                gl.drawArrays(gl.TRIANGLE_STRIP, 0, squareVertexPositionBuffer_numItems);

                #region new in lesson 03
                mvPopMatrix();
                #endregion

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

    }


}
