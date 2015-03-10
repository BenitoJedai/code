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
using WebGLHand.HTML.Pages;
using WebGLHand.Shaders;

namespace WebGLHand
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;



    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {

        public Action Dispose;

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page = null)
        {

            var gl_viewportWidth = Native.window.Width;
            var gl_viewportHeight = Native.window.Height;





            var gl = new WebGLRenderingContext();
            var canvas = gl.canvas.AttachToDocument();

            canvas.style.SetLocation(0, 0, gl_viewportWidth, gl_viewportHeight);

            canvas.width = gl_viewportWidth;
            canvas.height = gl_viewportHeight;

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



            var mvMatrix = glMatrix.mat4.create();
            var mvMatrixStack = new Stack<Float32Array>();

            var pMatrix = glMatrix.mat4.create();

            #region new in lesson 03
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
            #endregion


            #region setMatrixUniforms
            Action setMatrixUniforms =
                delegate
                {
                    gl.uniformMatrix4fv(shaderProgram_pMatrixUniform, false, pMatrix);
                    gl.uniformMatrix4fv(shaderProgram_mvMatrixUniform, false, mvMatrix);
                };
            #endregion

            #region cube
            var cubeVertexPositionBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
            var cubesize = 1.0f * 0.10f;
            var vertices = new[]{
                // Front face
                -cubesize, -cubesize,  cubesize,
                 cubesize, -cubesize,  cubesize,
                 cubesize,  cubesize,  cubesize,
                -cubesize,  cubesize,  cubesize,

                // Back face
                -cubesize, -cubesize, -cubesize,
                -cubesize,  cubesize, -cubesize,
                 cubesize,  cubesize, -cubesize,
                 cubesize, -cubesize, -cubesize,

                // Top face
                -cubesize,  cubesize, -cubesize,
                -cubesize,  cubesize,  cubesize,
                 cubesize,  cubesize,  cubesize,
                 cubesize,  cubesize, -cubesize,

                // Bottom face
                -cubesize, -cubesize, -cubesize,
                 cubesize, -cubesize, -cubesize,
                 cubesize, -cubesize,  cubesize,
                -cubesize, -cubesize,  cubesize,

                // Right face
                 cubesize, -cubesize, -cubesize,
                 cubesize,  cubesize, -cubesize,
                 cubesize,  cubesize,  cubesize,
                 cubesize, -cubesize,  cubesize,

                // Left face
                -cubesize, -cubesize, -cubesize,
                -cubesize, -cubesize,  cubesize,
                -cubesize,  cubesize,  cubesize,
                -cubesize,  cubesize, -cubesize
            };
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices), gl.STATIC_DRAW);

            var cubeVertexPositionBuffer_itemSize = 3;
            var cubeVertexPositionBuffer_numItems = 36;

            var cubeVertexColorBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer);

            // 216, 191, 18
            var colors = new[]{
                1.0f, 0.6f, 0.0f, 1.0f, // Front face
                1.0f, 0.6f, 0.0f, 1.0f, // Front face
                1.0f, 0.6f, 0.0f, 1.0f, // Front face
                1.0f, 0.6f, 0.0f, 1.0f, // Front face

                0.8f, 0.4f, 0.0f, 1.0f, // Back face
                0.8f, 0.4f, 0.0f, 1.0f, // Back face
                0.8f, 0.4f, 0.0f, 1.0f, // Back face
                0.8f, 0.4f, 0.0f, 1.0f, // Back face

                0.9f, 0.5f, 0.0f, 1.0f, // Top face
                0.9f, 0.5f, 0.0f, 1.0f, // Top face
                0.9f, 0.5f, 0.0f, 1.0f, // Top face
                0.9f, 0.5f, 0.0f, 1.0f, // Top face

                1.0f, 0.5f, 0.0f, 1.0f, // Bottom face
                1.0f, 0.5f, 0.0f, 1.0f, // Bottom face
                1.0f, 0.5f, 0.0f, 1.0f, // Bottom face
                1.0f, 0.5f, 0.0f, 1.0f, // Bottom face

                
                1.0f, 0.8f, 0.0f, 1.0f, // Right face
                1.0f, 0.8f, 0.0f, 1.0f, // Right face
                1.0f, 0.8f, 0.0f, 1.0f, // Right face
                1.0f, 0.8f, 0.0f, 1.0f, // Right face

                1.0f, 0.8f, 0.0f, 1.0f,  // Left face
                1.0f, 0.8f, 0.0f, 1.0f,  // Left face
                1.0f, 0.8f, 0.0f, 1.0f,  // Left face
                1.0f, 0.8f, 0.0f, 1.0f  // Left face
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
            var cubeVertexIndexBuffer_numItems = 36;

            #endregion

            gl.clearColor(0.0f, 0.0f, 0.0f, 1.0f);
            gl.enable(gl.DEPTH_TEST);

            #region new in lesson 04

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
            #endregion

            // jsc error
            //var fdeg_state = new float[5];

            var fdeg_state = new int[5];
            // int array not initialized?

            fdeg_state[0] = 0;
            fdeg_state[1] = 66;
            fdeg_state[2] = 66;
            fdeg_state[3] = 0;
            fdeg_state[4] = 66;

            var fdeg_relax = new int[5];


            fdeg_relax[0] = 0;
            fdeg_relax[1] = 0;
            fdeg_relax[2] = 0;
            fdeg_relax[3] = 0;
            fdeg_relax[4] = 0;

            var fdeg_relaxstate = new int[5];


            fdeg_relaxstate[0] = 11;
            fdeg_relaxstate[1] = 11;
            fdeg_relaxstate[2] = 11;
            fdeg_relaxstate[3] = 11;
            fdeg_relaxstate[4] = 33;

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


            #region drawScene
            Action drawScene = delegate
            {
                gl.viewport(0, 0, gl_viewportWidth, gl_viewportHeight);
                gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);


                glMatrix.mat4.perspective(45f, (float)gl_viewportWidth / (float)gl_viewportHeight, 0.1f, 100.0f, pMatrix);

                glMatrix.mat4.identity(mvMatrix);




                #region vertex
                gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
                gl.vertexAttribPointer((uint)shaderProgram_vertexPositionAttribute, cubeVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);
                #endregion

                #region color
                gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer);
                gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);
                #endregion


                glMatrix.mat4.translate(mvMatrix, new float[] { -1.5f, 0.0f, -7.0f });

                mvPushMatrix();


                // rotate all of it
                glMatrix.mat4.rotate(mvMatrix, degToRad(rCube * 0.05f), new float[] { -1f, 0.5f, 0f });
                glMatrix.mat4.rotate(mvMatrix, __pointer_y * 0.01f, new float[] { 1f, 0, 0f });
                glMatrix.mat4.rotate(mvMatrix, __pointer_x * 0.01f, new float[] { 0, 1f, 0f });


                #region DrawCubeAt
                Action<float, float> DrawCubeAt =
                    (x, y) =>
                    {
                        mvPushMatrix();
                        glMatrix.mat4.translate(mvMatrix, new float[] { 2 * cubesize * x, 2 * cubesize * -y, 0 });

                        gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);
                        setMatrixUniforms();
                        gl.drawElements(gl.TRIANGLES, cubeVertexPositionBuffer_numItems, gl.UNSIGNED_SHORT, 0);
                        mvPopMatrix();
                    };
                #endregion

                for (int ix = 0; ix < 11; ix++)
                {
                    for (int iy = 0; iy < 11; iy++)
                    {
                        DrawCubeAt(ix, iy);

                    }
                }

                #region DrawFinger
                Action<int, float> DrawFinger =
                    (x, fdeg) =>
                    {
                        mvPushMatrix();


                        glMatrix.mat4.rotate(mvMatrix, degToRad(fdeg), new float[] { 1f, 0f, 0f });

                        // 01.34.67.89.01
                        DrawCubeAt(3 * x + 0, -2);
                        DrawCubeAt(3 * x + 1, -2);
                        DrawCubeAt(3 * x + 0, -3);
                        DrawCubeAt(3 * x + 1, -3);

                        mvPushMatrix();

                        glMatrix.mat4.translate(mvMatrix, new float[] { 0, 2 * cubesize * (5), 0 });
                        glMatrix.mat4.rotate(mvMatrix, degToRad(fdeg), new float[] { 1f, 0f, 0f });
                        glMatrix.mat4.translate(mvMatrix, new float[] { 0, 2 * cubesize * (-5), 0 });

                        DrawCubeAt(3 * x + 0, -5);
                        DrawCubeAt(3 * x + 1, -5);
                        DrawCubeAt(3 * x + 0, -6);
                        DrawCubeAt(3 * x + 1, -6);

                        mvPushMatrix();

                        glMatrix.mat4.translate(mvMatrix, new float[] { 0, 2 * cubesize * (8), 0 });
                        glMatrix.mat4.rotate(mvMatrix, degToRad(fdeg), new float[] { 1f, 0f, 0f });
                        glMatrix.mat4.translate(mvMatrix, new float[] { 0, 2 * cubesize * (-8), 0 });

                        DrawCubeAt(3 * x + 0, -8);
                        DrawCubeAt(3 * x + 1, -8);
                        DrawCubeAt(3 * x + 0, -9);
                        DrawCubeAt(3 * x + 1, -9);


                        mvPopMatrix();
                        mvPopMatrix();
                        mvPopMatrix();
                    };
                #endregion





                // pinky
                DrawFinger(0, fdeg_state[0]);
                DrawFinger(1, fdeg_state[1]);
                // middle
                DrawFinger(2, fdeg_state[2]);
                // index
                DrawFinger(3, fdeg_state[3]);


                mvPushMatrix();

                glMatrix.mat4.rotate(mvMatrix, degToRad(-90), new float[] { 0f, 0f, 1f });
                // we have misplaced it now. lets put it into its place:)
                glMatrix.mat4.translate(mvMatrix, new float[] { 2 * cubesize * -4, 2 * cubesize * 11, 0 });




                // the thumb
                DrawFinger(4, fdeg_state[4]);

                mvPopMatrix();


                mvPopMatrix();


                #region original cube
                glMatrix.mat4.translate(mvMatrix, new float[] { 3.0f, 0.0f, 0.0f });

                mvPushMatrix();

                glMatrix.mat4.rotate(mvMatrix, degToRad(rCube), new float[] { 1f, 1f, 1f });

                gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);
                setMatrixUniforms();
                gl.drawElements(gl.TRIANGLES, cubeVertexPositionBuffer_numItems, gl.UNSIGNED_SHORT, 0);

                mvPopMatrix();
                #endregion

            };
            drawScene();
            #endregion


            var c = 0;



            #region pinky
            toolbar.f0.onmousedown +=
                delegate
                {
                    toolbar.f0.style.color = Color.Blue;
                    fdeg_state[0] = 80;
                    fdeg_relax[0] = 0;
                };

            toolbar.f0.onmouseup +=
                 delegate
                 {
                     toolbar.f0.style.color = Color.None;
                     //fdeg_state[0] = 11;
                     fdeg_relax[0] = 1;
                 };
            #endregion

            #region finger
            toolbar.f1.onmousedown +=
                delegate
                {
                    toolbar.f0.style.color = Color.Blue;
                    fdeg_state[1] = 80;
                    fdeg_relax[1] = 0;
                };

            toolbar.f1.onmouseup +=
                 delegate
                 {
                     toolbar.f1.style.color = Color.None;
                     fdeg_relax[1] = 1;
                 };
            #endregion

            #region middle
            toolbar.f2.onmousedown +=
                delegate
                {
                    toolbar.f0.style.color = Color.Blue;
                    fdeg_state[2] = 80;
                    fdeg_relax[2] = 0;
                };

            toolbar.f2.onmouseup +=
                 delegate
                 {
                     toolbar.f1.style.color = Color.None;
                     fdeg_relax[2] = 1;
                 };
            #endregion

            #region index
            toolbar.f3.onmousedown +=
                delegate
                {
                    toolbar.f3.style.color = Color.Blue;
                    fdeg_state[3] = 80;
                    fdeg_relax[3] = 0;
                };

            toolbar.f3.onmouseup +=
                 delegate
                 {
                     toolbar.f3.style.color = Color.None;
                     fdeg_relax[3] = 1;
                 };
            #endregion

            #region thumb
            toolbar.f4.onmousedown +=
                delegate
                {
                    toolbar.f4.style.color = Color.Blue;
                    fdeg_state[4] = 80;
                    fdeg_relax[4] = 0;
                };

            toolbar.f4.onmouseup +=
                 delegate
                 {
                     toolbar.f4.style.color = Color.None;
                     fdeg_relax[4] = 1;
                 };
            #endregion


            #region rock
            toolbar.fRock.onmousedown +=
                delegate
                {
                    toolbar.fRock.style.color = Color.Blue;
                    fdeg_state[0] = 0;
                    fdeg_state[1] = 77;
                    fdeg_state[2] = 77;
                    fdeg_state[3] = 0;
                    fdeg_state[4] = 77;

                    fdeg_relax[0] = 0;
                    fdeg_relax[1] = 0;
                    fdeg_relax[2] = 0;
                    fdeg_relax[3] = 0;
                    fdeg_relax[4] = 0;
                };

            toolbar.fRock.onmouseup +=
                 delegate
                 {
                     toolbar.fRock.style.color = Color.None;

                     fdeg_relax[0] = 1;
                     fdeg_relax[1] = 1;
                     fdeg_relax[2] = 1;
                     fdeg_relax[3] = 1;
                     fdeg_relax[4] = 1;
                 };
            #endregion

            #region fFist
            toolbar.fFist.onmousedown +=
                delegate
                {
                    toolbar.fRock.style.color = Color.Blue;
                    fdeg_state[0] = 88;
                    fdeg_state[1] = 88;
                    fdeg_state[2] = 88;
                    fdeg_state[3] = 88;
                    fdeg_state[4] = 88;

                    fdeg_relax[0] = 0;
                    fdeg_relax[1] = 0;
                    fdeg_relax[2] = 0;
                    fdeg_relax[3] = 0;
                    fdeg_relax[4] = 0;
                };

            toolbar.fFist.onmouseup +=
                 delegate
                 {
                     toolbar.fFist.style.color = Color.None;

                     fdeg_relax[0] = 1;
                     fdeg_relax[1] = 1;
                     fdeg_relax[2] = 1;
                     fdeg_relax[3] = 1;
                     fdeg_relax[4] = 1;
                 };
            #endregion

            #region electric
            toolbar.fElectric.onmousedown +=
                delegate
                {
                    toolbar.fElectric.style.color = Color.Blue;
                    fdeg_state[0] = 0;
                    fdeg_state[1] = 0;
                    fdeg_state[2] = 0;
                    fdeg_state[3] = 0;
                    fdeg_state[4] = 0;

                    fdeg_relax[0] = 0;
                    fdeg_relax[1] = 0;
                    fdeg_relax[2] = 0;
                    fdeg_relax[3] = 0;
                    fdeg_relax[4] = 0;
                };

            toolbar.fElectric.onmouseup +=
                 delegate
                 {
                     toolbar.fElectric.style.color = Color.None;

                     fdeg_relax[0] = 1;
                     fdeg_relax[1] = 1;
                     fdeg_relax[2] = 1;
                     fdeg_relax[3] = 1;
                     fdeg_relax[4] = 1;
                 };
            #endregion

            #region fRelax
            toolbar.fRelax.onclick +=
             delegate
             {
                 toolbar.fElectric.style.color = Color.None;

                 fdeg_relax[0] = 1;
                 fdeg_relax[1] = 1;
                 fdeg_relax[2] = 1;
                 fdeg_relax[3] = 1;
                 fdeg_relax[4] = 1;
             };
            #endregion


            Native.window.onframe += delegate
            {
                c++;

                for (int i = 0; i < 5; i++)
                {
                    if (fdeg_relax[i] > 0)
                    {
                        // Math.Sign(int) does not exist.
                        // next release should have it!

                        var a = (fdeg_state[i] - fdeg_relaxstate[i]);

                        if (a > 4)
                            fdeg_state[i] -= 3;

                        if (a < -4)
                            fdeg_state[i] += 3;

                    }
                }

                Native.document.title = "" + c;

                drawScene();
                animate();
            };

            //new IHTMLAnchor { "drag me" }.AttachTo(Native.document.documentElement).With(
            //    dragme =>
            //    {
            //        dragme.style.position = IStyle.PositionEnum.@fixed;
            //        dragme.style.left = "1em";
            //        dragme.style.bottom = "1em";

            //        dragme.style.zIndex = 1000;

            //        dragme.AllowToDragAsApplicationPackage();
            //    }
            //);

        }

    }


}
