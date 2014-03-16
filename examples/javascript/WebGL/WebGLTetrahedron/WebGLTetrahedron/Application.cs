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
using WebGLTetrahedron.HTML.Pages;
using WebGLTetrahedron.Shaders;

namespace WebGLTetrahedron
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

        public readonly WebGLRenderingContext gl = new WebGLRenderingContext(alpha: false, preserveDrawingBuffer: true);

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page = null)
        {
            // http://hiddenlighthouse.wordpress.com/2012/12/07/star-tetrahedron-the-star-of-david-3/

            var size = 96;




            var canvas = gl.canvas;

            if (page != null)
                canvas.AttachToDocument();



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
            if (page == null)
            {
                Console.WriteLine("page is null");
            }
            else
            {
                Console.WriteLine("got page");

                Native.document.body.style.overflow = IStyle.OverflowEnum.hidden;
                canvas.style.SetLocation(0, 0, size, size);

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
            }
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





            #region initShaders
            var shaderProgram = gl.createProgram(
                new GeometryVertexShader(),
                new GeometryFragmentShader()
            );




            gl.linkProgram(shaderProgram);
            gl.useProgram(shaderProgram);

            var shaderProgram_vertexPositionAttribute = gl.getAttribLocation(shaderProgram, "aVertexPosition");

            gl.enableVertexAttribArray((uint)shaderProgram_vertexPositionAttribute);

            // new in lesson 02
            var shaderProgram_vertexColorAttribute = gl.getAttribLocation(shaderProgram, "aVertexColor");
            gl.enableVertexAttribArray((uint)shaderProgram_vertexColorAttribute);

            var shaderProgram_pMatrixUniform = gl.getUniformLocation(shaderProgram, "uPMatrix");
            var shaderProgram_mvMatrixUniform = gl.getUniformLocation(shaderProgram, "uMVMatrix");
            #endregion



            var mvMatrix = glMatrix.mat4.create();
            var mvMatrixStack = new Stack<Float32Array>();

            var pMatrix = glMatrix.mat4.create();

            Action mvPushMatrix = delegate
            {
                var copy = glMatrix.mat4.create();
                glMatrix.mat4.set(mvMatrix, copy);
                mvMatrixStack.Push(copy);
            };

            Action mvPopMatrix = delegate
            {
                mvMatrix = mvMatrixStack.Pop();
            };


            #region setMatrixUniforms
            Action setMatrixUniforms =
                delegate
                {
                    gl.uniformMatrix4fv(shaderProgram_pMatrixUniform, false, pMatrix);
                    gl.uniformMatrix4fv(shaderProgram_mvMatrixUniform, false, mvMatrix);
                };
            #endregion




            #region init buffers

            #region pyramid
            var pyramidVertexPositionBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, pyramidVertexPositionBuffer);
            var vertices = new[]{
                // Front face
                 0.0f,  1.0f,  0.0f,
                -1.0f, -1.0f,  1.0f,
                 1.0f, -1.0f,  1.0f,

                // Right face
                 0.0f,  1.0f,  0.0f,
                 1.0f, -1.0f,  1.0f,
                 1.0f, -1.0f, -1.0f,

                // Back face
                 0.0f,  1.0f,  0.0f,
                 1.0f, -1.0f, -1.0f,
                -1.0f, -1.0f, -1.0f,

                // Left face
                 0.0f,  1.0f,  0.0f,
                -1.0f, -1.0f, -1.0f,
                -1.0f, -1.0f,  1.0f
            };
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices), gl.STATIC_DRAW);
            var pyramidVertexPositionBuffer_itemSize = 3;
            var pyramidVertexPositionBuffer_numItems = 12;


            var pyramidVertexColorBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, pyramidVertexColorBuffer);

            var colors = new[]{
                // Front face
                1.0f, 0.0f, 0.0f, 1.0f,
                0.0f, 1.0f, 0.0f, 1.0f,
                0.0f, 0.0f, 1.0f, 1.0f,

                // Right face
                1.0f, 0.0f, 0.0f, 1.0f,
                0.0f, 0.0f, 1.0f, 1.0f,
                0.0f, 1.0f, 0.0f, 1.0f,

                // Back face
                1.0f, 0.0f, 0.0f, 1.0f,
                0.0f, 1.0f, 0.0f, 1.0f,
                0.0f, 0.0f, 1.0f, 1.0f,

                // Left face
                1.0f, 0.0f, 0.0f, 1.0f,
                0.0f, 0.0f, 1.0f, 1.0f,
                0.0f, 1.0f, 0.0f, 1.0f

            };
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(colors), gl.STATIC_DRAW);
            var pyramidVertexColorBuffer_itemSize = 4;
            var pyramidVertexColorBuffer_numItems = 12;
            #endregion

            #region cube
            var cubeVertexPositionBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
            vertices = new[]{

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

                // Left face BLUE
                -1.0f, -1.0f, -1.0f,
                -1.0f, -1.0f,  1.0f,
                -1.0f,  1.0f,  1.0f,
                -1.0f,  1.0f, -1.0f
            };
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices), gl.STATIC_DRAW);

            var cubeVertexPositionBuffer_itemSize = 3;
            var cubeVertexPositionBuffer_numItems = 6 * 6;

            var cubeVertexColorBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer);
            colors = new[]{
                // RED
                1.0f, 0.0f, 0.0f, 1.0f, // Front face
                1.0f, 0.0f, 0.0f, 1.0f, // Front face
                1.0f, 0.0f, 0.0f, 1.0f, // Front face
                1.0f, 0.0f, 0.0f, 1.0f, // Front face

                // YELLOW
                1.0f, 1.0f, 0.0f, 1.0f, // Back face
                1.0f, 1.0f, 0.0f, 1.0f, // Back face
                1.0f, 1.0f, 0.0f, 1.0f, // Back face
                1.0f, 1.0f, 0.0f, 1.0f, // Back face

                // GREEN
                0.0f, 1.0f, 0.0f, 1.0f, // Top face
                0.0f, 1.0f, 0.0f, 1.0f, // Top face
                0.0f, 1.0f, 0.0f, 1.0f, // Top face
                0.0f, 1.0f, 0.0f, 1.0f, // Top face

                1.0f, 0.5f, 0.5f, 1.0f, // Bottom face
                1.0f, 0.5f, 0.5f, 1.0f, // Bottom face
                1.0f, 0.5f, 0.5f, 1.0f, // Bottom face
                1.0f, 0.5f, 0.5f, 1.0f, // Bottom face

                1.0f, 0.0f, 1.0f, 1.0f, // Right face
                1.0f, 0.0f, 1.0f, 1.0f, // Right face
                1.0f, 0.0f, 1.0f, 1.0f, // Right face
                1.0f, 0.0f, 1.0f, 1.0f, // Right face

                0.0f, 0.0f, 1.0f, 1.0f,  // Left face
                0.0f, 0.0f, 1.0f, 1.0f,  // Left face
                0.0f, 0.0f, 1.0f, 1.0f,  // Left face
                0.0f, 0.0f, 1.0f, 1.0f  // Left face
            };



            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(colors), gl.STATIC_DRAW);
            var cubeVertexColorBuffer_itemSize = 4;
            var cubeVertexColorBuffer_numItems = 24;

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




            gl.clearColor(0.0f, 0.0f, 0.0f, 1.0f);
            gl.enable(gl.DEPTH_TEST);




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
                    if (Native.document.pointerLockElement == canvas)
                    {

                        __pointer_x += e.movementX;
                        __pointer_y += e.movementY;
                    }
                };

            canvas.onmouseup +=
                delegate
                {
                    Native.document.exitPointerLock();
                };
            #endregion





            var rPyramid = 0f;
            var rCube = 0f;

            var lastTime = 0L;
            Action animate = delegate
            {
                var timeNow = new IDate().getTime();
                if (lastTime != 0)
                {
                    var elapsed = timeNow - lastTime;

                    rPyramid += (90 * elapsed) / 1000.0f;
                    rCube -= (75 * elapsed) / 1000.0f;
                }
                lastTime = timeNow;
            };

            Func<float, float> degToRad = (degrees) =>
            {
                return degrees * (f)Math.PI / 180f;
            };

            #region drawScene
            Action drawScene = delegate
            {
                gl.viewport(0, 0, gl_viewportWidth, gl_viewportHeight);
                gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

                glMatrix.mat4.perspective(45f, (float)gl_viewportWidth / (float)gl_viewportHeight, 0.1f, 100.0f, pMatrix);

                glMatrix.mat4.identity(mvMatrix);


                mvPushMatrix();
                glMatrix.mat4.translate(mvMatrix, new float[] { 0f, 0.5f, -7.0f });

                //glMatrix.mat4.rotate(mvMatrix, (f)Math.PI, new float[] { 0f, 0f, 1f });


                glMatrix.mat4.rotate(mvMatrix, __pointer_y * 0.01f, new float[] { 1f, 0, 0f });
                glMatrix.mat4.rotate(mvMatrix, __pointer_x * 0.01f, new float[] { 0, 1f, 0f });

                glMatrix.mat4.rotate(mvMatrix, degToRad(rPyramid), new float[] { 0f, 1f, 0f });

                gl.bindBuffer(gl.ARRAY_BUFFER, pyramidVertexPositionBuffer);
                gl.vertexAttribPointer((uint)shaderProgram_vertexPositionAttribute, pyramidVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);


                gl.bindBuffer(gl.ARRAY_BUFFER, pyramidVertexColorBuffer);
                gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, pyramidVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);


                setMatrixUniforms();
                gl.drawArrays(gl.TRIANGLES, 0, pyramidVertexPositionBuffer_numItems);

                mvPopMatrix();

                mvPushMatrix();

                glMatrix.mat4.translate(mvMatrix, new float[] { 0f, -0.5f, -7.0f });



                glMatrix.mat4.rotate(mvMatrix, __pointer_y * 0.01f, new float[] { 1f, 0, 0f });
                glMatrix.mat4.rotate(mvMatrix, __pointer_x * 0.01f, new float[] { 0, 1f, 0f });



                glMatrix.mat4.rotate(mvMatrix, (f)Math.PI, new float[] { 0f, 0f, 1f });

                glMatrix.mat4.rotate(mvMatrix, degToRad(rPyramid), new float[] { 0f, 1f, 0f });
                gl.bindBuffer(gl.ARRAY_BUFFER, pyramidVertexPositionBuffer);
                gl.vertexAttribPointer((uint)shaderProgram_vertexPositionAttribute, pyramidVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);


                gl.bindBuffer(gl.ARRAY_BUFFER, pyramidVertexColorBuffer);
                gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, pyramidVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);


                setMatrixUniforms();
                gl.drawArrays(gl.TRIANGLES, 0, pyramidVertexPositionBuffer_numItems);

                mvPopMatrix();

                //glMatrix.mat4.translate(mvMatrix, new float[] { 3.0f, 0.0f, 0.0f });

                //#region new in lesson 04
                //mvPushMatrix();
                //glMatrix.mat4.rotate(mvMatrix, degToRad(rCube), new float[] { 1f, 1f, 1f });
                //#endregion


                //gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
                //gl.vertexAttribPointer((uint)shaderProgram_vertexPositionAttribute, cubeVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);

                //gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer);
                //gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);

                //gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);
                //setMatrixUniforms();
                //gl.drawElements(gl.TRIANGLES, cubeVertexIndexBuffer_numItems, gl.UNSIGNED_SHORT, 0);

                //#region new in lesson 03
                //mvPopMatrix();
                //#endregion

            };
            drawScene();
            #endregion




            var c = 0;


            Native.window.onframe += delegate
            {
                c++;

                Native.Document.title = "" + c;

                drawScene();
                animate();
            };

        }

    }


}
