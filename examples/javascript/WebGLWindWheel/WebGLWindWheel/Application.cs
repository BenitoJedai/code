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
using WebGLWindWheel.HTML.Pages;
using WebGLWindWheel.Shaders;

namespace WebGLWindWheel
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;



    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page = null)
        {

            var size = 600;




            var gl = new WebGLRenderingContext();

            var canvas = gl.canvas.AttachToDocument();

            Native.document.body.style.overflow = IStyle.OverflowEnum.hidden;
            canvas.style.SetLocation(0, 0, size, size);

            canvas.width = size;
            canvas.height = size;


            var gl_viewportWidth = size;
            var gl_viewportHeight = size;



            var shaderProgram = gl.createProgram();


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




            #region init buffers


            #region cube
            var cubeVertexPositionBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);

            var cubesize = 1.0f * 0.05f;
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
            var cubeVertexPositionBuffer_numItems = 6 * 6;

            var squareVertexColorBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, squareVertexColorBuffer);

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

            #endregion




            gl.clearColor(0.0f, 0.0f, 0.0f, alpha: 1.0f);
            gl.enable(gl.DEPTH_TEST);


            var rWindDelta = 0.0f;
            var rCubeDelta = 1.0f;

            if (page != null)
            {

                #region WindLeft
                page.WindLeft.onmousedown +=
                    delegate
                    {
                        rWindDelta = -2.0f;
                    };
                page.WindLeft.onmouseup +=
                 delegate
                 {
                     rWindDelta = 0.0f;
                 };
                #endregion

                #region WindRight
                page.WindRight.onmousedown +=
                    delegate
                    {
                        rWindDelta = 2.0f;
                    };
                page.WindRight.onmouseup +=
                 delegate
                 {
                     rWindDelta = 0.0f;
                 };
                #endregion


                #region SpeedSlow
                page.SpeedSlow.onmousedown +=
                    delegate
                    {
                        rCubeDelta = 0.1f;
                    };
                page.SpeedSlow.onmouseup +=
                 delegate
                 {
                     rCubeDelta = 1.0f;
                 };
                #endregion

                #region SpeedFast
                page.SpeedFast.onmousedown +=
                  delegate
                  {
                      rCubeDelta = 4.0f;
                  };
                page.SpeedFast.onmouseup +=
                 delegate
                 {
                     rCubeDelta = 1.0f;
                 };
                #endregion
            }


            #region animate
            var rCube = 0f;
            var rWind = 0f;

            var lastTime = 0L;
            Action animate = delegate
            {
                var timeNow = new IDate().getTime();
                if (lastTime != 0)
                {
                    var elapsed = timeNow - lastTime;

                    rCube -= ((75 * elapsed) / 1000.0f) * rCubeDelta;
                    rWind -= ((75 * elapsed) / 1000.0f) * rWindDelta;
                }
                lastTime = timeNow;
            };
            #endregion

            #region degToRad
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

                glMatrix.mat4.perspective(45f, (float)gl_viewportWidth / (float)gl_viewportHeight, 0.1f, 100.0f, pMatrix);

                glMatrix.mat4.identity(mvMatrix);

                glMatrix.mat4.translate(mvMatrix, new float[] { -1.5f, 0.0f, -7.0f });

                mvPushMatrix();
                glMatrix.mat4.rotate(mvMatrix, degToRad(rWind), new float[] { 0f, 1f, 0f });


                #region DrawFrameworkWingAtX
                Action<float, float> DrawFrameworkWingAtX =
                    (WingX, WingY) =>
                    {
                        #region draw center cube
                        mvPushMatrix();

                        glMatrix.mat4.translate(mvMatrix, new float[] { cubesize * WingX, cubesize * WingY, 0 });

                        gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
                        gl.vertexAttribPointer((uint)shaderProgram_vertexPositionAttribute, cubeVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);

                        gl.bindBuffer(gl.ARRAY_BUFFER, squareVertexColorBuffer);
                        gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);

                        gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);
                        setMatrixUniforms();
                        gl.drawElements(gl.TRIANGLES, cubeVertexPositionBuffer_numItems, gl.UNSIGNED_SHORT, 0);

                        mvPopMatrix();
                        #endregion
                    };
                #endregion

                #region DrawWingAtX
                Action<int, int, float, float> DrawWingAtX =
                    (WingX, WingSize, WingRotationMultiplier, WingRotationOffset) =>
                    {
                        mvPushMatrix();

                        glMatrix.mat4.translate(mvMatrix, new float[] { cubesize * WingX, 0, 0 });

                        if (WingRotationOffset == 0)
                        {
                            DrawFrameworkWingAtX(0, 0);
                        }

                        #region DrawWingPart
                        Action<float> DrawWingPart =
                            PartIndex =>
                            {
                                mvPushMatrix();
                                glMatrix.mat4.rotate(mvMatrix, degToRad(WingRotationOffset + (rCube * WingRotationMultiplier)), new float[] { 1f, 0f, 0f });
                                glMatrix.mat4.translate(mvMatrix, new float[] { 0f, cubesize * PartIndex * 2, 0 });

                                gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
                                gl.vertexAttribPointer((uint)shaderProgram_vertexPositionAttribute, cubeVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);

                                gl.bindBuffer(gl.ARRAY_BUFFER, squareVertexColorBuffer);
                                gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);

                                gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);
                                setMatrixUniforms();
                                gl.drawElements(gl.TRIANGLES, cubeVertexPositionBuffer_numItems, gl.UNSIGNED_SHORT, 0);

                                mvPopMatrix();
                            };
                        #endregion

                        #region DrawWingWithSize
                        Action<int> DrawWingWithSize =
                            length =>
                            {
                                for (int i = 4; i < length; i++)
                                {
                                    DrawWingPart(i * 1.0f);
                                    DrawWingPart(-i * 1.0f);

                                }
                            };
                        #endregion

                        DrawWingWithSize(WingSize);

                        mvPopMatrix();

                    };
                #endregion

                var x = 8;

                DrawFrameworkWingAtX(x - 8, 0);

                for (int i = 0; i < 24; i++)
                {
                    DrawFrameworkWingAtX(x - 8, -2.0f * i);

                }

                DrawWingAtX(x - 6, 0, 1f, 0);
                DrawWingAtX(x - 4, 0, 1f, 0);
                DrawWingAtX(x - 2, 0, 1f, 0);

                DrawWingAtX(x + 0, 16, 1f, 0);
                DrawWingAtX(x + 0, 16, 1f, 30);
                DrawWingAtX(x + 0, 16, 1f, 60);
                DrawWingAtX(x + 0, 16, 1f, 90);
                DrawWingAtX(x + 0, 16, 1f, 120);
                DrawWingAtX(x + 0, 16, 1f, 150);

                DrawWingAtX(x + 2, 0, 1f, 0);
                DrawWingAtX(x + 4, 0, 1f, 0);
                DrawWingAtX(x + 6, 0, 1f, 0);

                DrawWingAtX(x + 8, 12, 0.4f, 0);
                DrawWingAtX(x + 8, 12, 0.4f, 60);
                DrawWingAtX(x + 8, 12, 0.4f, 120);


                DrawWingAtX(x + 8 + 2, 0, 1f, 0);
                DrawWingAtX(x + 8 + 4, 0, 1f, 0);
                DrawWingAtX(x + 8 + 6, 0, 1f, 0);

                DrawWingAtX(x + 16, 8, 0.3f, 0);
                DrawWingAtX(x + 16, 8, 0.3f, 90);




                mvPopMatrix();


                #region draw cube on the right to remind where we started
                glMatrix.mat4.translate(mvMatrix, new float[] { 3.0f, 2.0f, 0.0f });

                mvPushMatrix();
                glMatrix.mat4.rotate(mvMatrix, degToRad(rCube), new float[] { 1f, 1f, 1f });



                gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
                gl.vertexAttribPointer((uint)shaderProgram_vertexPositionAttribute, cubeVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);

                gl.bindBuffer(gl.ARRAY_BUFFER, squareVertexColorBuffer);
                gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);

                gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);
                setMatrixUniforms();
                gl.drawElements(gl.TRIANGLES, cubeVertexPositionBuffer_numItems, gl.UNSIGNED_SHORT, 0);

                mvPopMatrix();
                #endregion

            };
            drawScene();
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




            var c = 0;

            #region tick
            var tick = default(Action);

            tick = delegate
            {
                if (IsDisposed)
                    return;

                c++;

                Native.Document.title = "" + c;

                drawScene();
                animate();

                Native.Window.requestAnimationFrame += tick;
            };

            tick();
            #endregion

            if (page != null)
                page.Mission.Orphanize().AttachToDocument().style.SetLocation(left: size - 56, top: 16);
        }

        public Action Dispose;

    }


}
