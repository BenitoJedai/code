using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.WebGL;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using WebGLSpadeWarrior.HTML.Pages;

namespace WebGLSpadeWarrior
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;
    using ScriptCoreLib.Shared.Lambda;
    using ScriptCoreLib.Shared.Drawing;
    using WebGLSpadeWarrior.Shaders;
    using WebGLSpadeWarrior.Library;
    using System.Collections.Generic;


    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    internal sealed class Application
    {
        /* This example will be a port of http://learningwebgl.com/blog/?p=370 by Giles
         * 
         * 01. Created a new project of type Web Application
         * 02. initGL
         * 03. initShaders
         */

        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            new __glMatrix().Content.With(
               source =>
               {
                   source.onload +=
                       delegate
                       {
                           //new IFunction("alert(CanvasMatrix4);").apply(null);

                           InitializeContent(page);
                       };

                   source.AttachToDocument();
               }
           );


            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

        void InitializeContent(IDefaultPage page)
        {
            page.PageContainer.style.color = Color.Blue;

            var size = 600;

            #region canvas
            var canvas = new IHTMLCanvas().AttachToDocument();

            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;
            canvas.style.SetLocation(0, 0, size, size);

            canvas.width = size;
            canvas.height = size;
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

                    return null;
                }

                return shader;
            };
            #endregion

            var vs = createShader(new GeometryVertexShader());
            var fs = createShader(new GeometryFragmentShader());

            if (vs == null || fs == null) throw new InvalidOperationException("shader failed");

            gl.attachShader(shaderProgram, vs);
            gl.attachShader(shaderProgram, fs);


            gl.linkProgram(shaderProgram);
            gl.useProgram(shaderProgram);

            var shaderProgram_vertexPositionAttribute = gl.getAttribLocation(shaderProgram, "aVertexPosition");

            gl.enableVertexAttribArray((ulong)shaderProgram_vertexPositionAttribute);

            // new in lesson 02
            var shaderProgram_vertexColorAttribute = gl.getAttribLocation(shaderProgram, "aVertexColor");
            gl.enableVertexAttribArray((ulong)shaderProgram_vertexColorAttribute);

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

            Action<Action> mvMatrixScope =
                h =>
                {
                    mvPushMatrix();
                    h();
                    mvPopMatrix();
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


            #region cube
            var cubeVertexPositionBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
            var cubesize = 1.0f * 0.02f;
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


            // 216, 191, 18
            #region colors1
            var colors1 = new[]{
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


            var cubeVertexColorBuffer1 = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer1);
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(colors1), gl.STATIC_DRAW);
            #endregion

            #region colors2
            var colors2 = new[]{
                0.0f, 0.6f, 1.0f, 1.0f, // Front face
                0.0f, 0.6f, 1.0f, 1.0f, // Front face
                0.0f, 0.6f, 1.0f, 1.0f, // Front face
                0.0f, 0.6f, 1.0f, 1.0f, // Front face

                0.0f, 0.4f, 0.8f, 1.0f, // Back face
                0.0f, 0.4f, 0.8f, 1.0f, // Back face
                0.0f, 0.4f, 0.8f, 1.0f, // Back face
                0.0f, 0.4f, 0.8f, 1.0f, // Back face

                0.0f, 0.5f, 0.9f, 1.0f, // Top face
                0.0f, 0.5f, 0.9f, 1.0f, // Top face
                0.0f, 0.5f, 0.9f, 1.0f, // Top face
                0.0f, 0.5f, 0.9f, 1.0f, // Top face

                0.0f, 0.5f, 1.0f, 1.0f, // Bottom face
                0.0f, 0.5f, 1.0f, 1.0f, // Bottom face
                0.0f, 0.5f, 1.0f, 1.0f, // Bottom face
                0.0f, 0.5f, 1.0f, 1.0f, // Bottom face

                
                0.0f, 0.8f, 1.0f, 1.0f, // Right face
                0.0f, 0.8f, 1.0f, 1.0f, // Right face
                0.0f, 0.8f, 1.0f, 1.0f, // Right face
                0.0f, 0.8f, 1.0f, 1.0f, // Right face

                0.0f, 0.8f, 1.0f, 1.0f,  // Left face
                0.0f, 0.8f, 1.0f, 1.0f,  // Left face
                0.0f, 0.8f, 1.0f, 1.0f,  // Left face
                0.0f, 0.8f, 1.0f, 1.0f  // Left face
            };


            var cubeVertexColorBuffer2 = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer2);
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(colors2), gl.STATIC_DRAW);
            #endregion


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




            gl.clearColor(0.0f, 0.0f, 0.0f, 1.0f);
            gl.enable(gl.DEPTH_TEST);


            var rCube = 0f;
            var raCube = 0f;

            var lastTime = 0L;
            Action animate = delegate
            {
                var timeNow = new IDate().getTime();
                if (lastTime != 0)
                {
                    var elapsed = timeNow - lastTime;

                    raCube -= (75 * elapsed) / 1000.0f;
                }
                lastTime = timeNow;
            };

            Func<float, float> degToRad = (degrees) =>
            {
                return degrees * (f)Math.PI / 180f;
            };

            var ego_x = 0f;
            var ego_y = 0f;
            var ego_z = 0f;

            var c = 0;

            #region drawScene
            Action drawScene = delegate
            {
                gl.viewport(0, 0, gl_viewportWidth, gl_viewportHeight);
                gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);


                __glMatrix.mat4.perspective(45f, (float)gl_viewportWidth / (float)gl_viewportHeight, 0.1f, 100.0f, pMatrix);

                __glMatrix.mat4.identity(mvMatrix);




                #region vertex
                gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
                gl.vertexAttribPointer((ulong)shaderProgram_vertexPositionAttribute, cubeVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);
                #endregion


                //__glMatrix.mat4.rotate(mvMatrix, degToRad(-33), new float[] { 0f, 1f, 0f });
                //__glMatrix.mat4.rotate(mvMatrix, rCube, new float[] { 1f, 0f, 0f });



                #region OriginalCubeAt
                Action<float, float, float> OriginalCubeAt =
                    (x, y, z) =>
                    {
                        mvMatrixScope(
                            delegate
                            {
                                __glMatrix.mat4.translate(mvMatrix, new float[] { x, y, z });
                                __glMatrix.mat4.rotate(mvMatrix, degToRad(raCube), new float[] { 1f, 1f, 1f });

                                gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);
                                setMatrixUniforms();
                                gl.drawElements(gl.TRIANGLES, cubeVertexPositionBuffer_numItems, gl.UNSIGNED_SHORT, 0);
                            }
                        );
                    };
                #endregion

                mvMatrixScope(
                    delegate
                    {
                        __glMatrix.mat4.translate(mvMatrix, new float[] { 
                            - 1.5f, 
                            0, 
                             - 7f});
                        __glMatrix.mat4.rotate(mvMatrix, degToRad(-66), new float[] { 1f, 0f, 0f });


                        #region grid
                        OriginalCubeAt(-1f, 0, 0);
                        OriginalCubeAt(0, 0, 0);
                        OriginalCubeAt(1f, 0, 0);
                        OriginalCubeAt(2f, 0, 0);
                        OriginalCubeAt(3f, 0, 0);
                        OriginalCubeAt(4f, 0, 0);



                        Action<float> WriteYLine =
                            x =>
                            {

                                OriginalCubeAt(x, 3f, 0);
                                OriginalCubeAt(x, 2f, 0);
                                OriginalCubeAt(x, 1f, 0);
                                OriginalCubeAt(x, -1f, 0);
                                OriginalCubeAt(x, -2f, 0);
                                OriginalCubeAt(x, -3f, 0);
                            };

                        WriteYLine(-1);
                        WriteYLine(0);
                        WriteYLine(1);
                        WriteYLine(2);
                        WriteYLine(3);
                        WriteYLine(4);
                        #endregion

                        {
                            var _y = (float)Math.Cos(raCube * 0.05f) * 0.1f;
                            var _x = (float)Math.Sin(raCube * 0.05f) * 0.1f;

                            OriginalCubeAt(_x, _y, 0);
                        }

                        {
                            var _y = (float)Math.Sin(rCube) * 0.2f;
                            var _x = (float)Math.Cos(rCube) * 0.2f;

                            OriginalCubeAt(_x, _y, 0);
                        }


                        mvMatrixScope(
                          delegate
                          {
                              // where are we
                              __glMatrix.mat4.translate(mvMatrix, new float[] { ego_x, ego_y, ego_z });


                              // rotate all of it
                              //__glMatrix.mat4.rotate(mvMatrix, degToRad(-45), new float[] { 1f, 0f, 0f });

                              // which way are we looking at?
                              __glMatrix.mat4.rotate(mvMatrix, rCube, new float[] { 0f, 0f, 1f });


                              #region draw
                              Action<float, float, float> cube =
                                  (x, y, z) =>
                                  {
                                      mvPushMatrix();
                                      __glMatrix.mat4.translate(mvMatrix, new float[] { 
                                        2 * cubesize * x, 
                                        2 * cubesize * y, 
                                        2 * cubesize  * z});

                                      gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);
                                      setMatrixUniforms();
                                      gl.drawElements(gl.TRIANGLES, cubeVertexPositionBuffer_numItems, gl.UNSIGNED_SHORT, 0);
                                      mvPopMatrix();
                                  };

                              Action<int, int, int> rect =
                                  (ix, iy, z) =>
                                  {
                                      for (int y = 0; y < ix; y++)
                                      {
                                          for (int x = 0; x < iy; x++)
                                          {
                                              cube(x, y, z);
                                          }
                                      }
                                  };

                              Action<int, float, float> leg =
                                  (y, hiprotation, kneerotation) =>
                                  {
                                      mvPushMatrix();

                                      #region hiprotation
                                      __glMatrix.mat4.translate(mvMatrix, new float[] { 
                                        2 * cubesize * 2, 
                                        2 * cubesize * 0, 
                                        2 * cubesize * 11});

                                      __glMatrix.mat4.rotate(mvMatrix, degToRad(hiprotation), new float[] { 0f, 1f, 0f });
                                      __glMatrix.mat4.translate(mvMatrix, new float[] { 
                                        2 * cubesize * -2, 
                                        2 * cubesize * 0, 
                                        2 * cubesize * -11});
                                      #endregion



                                      __glMatrix.mat4.translate(mvMatrix, new float[] { 
                                        2 * cubesize * 1, 
                                        2 * cubesize * y, 
                                        2 * cubesize  * 0});


                                      mvPushMatrix();

                                      #region kneerotation
                                      __glMatrix.mat4.translate(mvMatrix, new float[] { 
                                        2 * cubesize * 1, 
                                        2 * cubesize * 0, 
                                        2 * cubesize * 6f});

                                      __glMatrix.mat4.rotate(mvMatrix, degToRad(kneerotation), new float[] { 0f, 1f, 0f });
                                      __glMatrix.mat4.translate(mvMatrix, new float[] { 
                                        2 * cubesize * -1, 
                                        2 * cubesize * 0, 
                                        2 * cubesize * -6f});
                                      #endregion

                                      #region color
                                      gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer1);
                                      gl.vertexAttribPointer((ulong)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);
                                      #endregion


                                      #region lower leg
                                      rect(3, 5, 0);
                                      rect(3, 5, 1);
                                      rect(3, 5, 2);
                                      rect(3, 3, 3);
                                      #endregion



                                      #region color
                                      gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer2);
                                      gl.vertexAttribPointer((ulong)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);
                                      #endregion

                                      rect(3, 3, 4);
                                      rect(3, 3, 5);
                                      rect(3, 3, 6);

                                      mvPopMatrix();


                                      #region upper leg
                                      mvPushMatrix();
                                      __glMatrix.mat4.translate(mvMatrix, new float[] { 
                                        2 * cubesize * 1, 
                                        2 * cubesize * 0, 
                                        2 * cubesize  * 0});

                                      rect(3, 3, 7);
                                      rect(3, 3, 8);
                                      rect(3, 3, 9);


                                      mvPopMatrix();
                                      #endregion

                                      #region hips
                                      rect(3, 4, 10);
                                      //rect(3, 4, 11);
                                      #endregion

                                      mvPopMatrix();



                                  };
                              #endregion



                              leg(-2, 33, 0);
                              leg(3, -60, c);




                              #region body
                              mvPushMatrix();
                              __glMatrix.mat4.translate(mvMatrix, new float[] { 
                                        2 * cubesize * 1, 
                                        2 * cubesize * -3, 
                                        2 * cubesize  * 0});

                              rect(10, 4, 11);
                              rect(10, 4, 12);
                              rect(10, 4, 13);
                              rect(10, 4, 14);
                              rect(10, 4, 15);
                              rect(10, 4, 16);
                              rect(10, 4, 17);
                              rect(10, 4, 18);
                              rect(10, 4, 19);
                              mvPopMatrix();
                              #endregion

                              #region head
                              #region color
                              gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer1);
                              gl.vertexAttribPointer((ulong)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);
                              #endregion

                              mvPushMatrix();
                              __glMatrix.mat4.translate(mvMatrix, new float[] { 
                                        2 * cubesize * 0, 
                                        2 * cubesize * -1, 
                                        2 * cubesize  * 20});

                              rect(6, 6, 0);
                              rect(6, 6, 1);
                              rect(6, 6, 2);

                              #region color
                              gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer2);
                              gl.vertexAttribPointer((ulong)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);
                              #endregion

                              rect(6, 6, 3);
                              rect(6, 6, 4);
                              rect(6, 6, 5);


                              mvPopMatrix();
                              #endregion



                          }
                      );
                    }
                );

            };
            drawScene();
            #endregion

            #region requestAnimFrame
            var requestAnimFrame = (IFunction)new IFunction(
                @"return window.requestAnimationFrame ||
         window.webkitRequestAnimationFrame ||
         window.mozRequestAnimationFrame ||
         window.oRequestAnimationFrame ||
         window.msRequestAnimationFrame ||
         function(/* function FrameRequestCallback */ callback, /* DOMElement Element */ element) {
           window.setTimeout(callback, 1000/60);
         };"
            ).apply(null);
            #endregion





            #region tick - new in lesson 03
            var tick = default(Action);

            tick = delegate
            {
                c++;


                Native.Document.title = "" + c + " " + (rCube) + " ";

                drawScene();
                animate();

                requestAnimFrame.apply(null, IFunction.OfDelegate(tick));
            };

            tick();
            #endregion

            #region onkeydown
            Native.Document.body.onkeydown +=
                (e) =>
                {
                    // see also: http://www.cambiaresearch.com/articles/15/javascript-char-codes-key-codes

                    e.PreventDefault();

                    var turnspeed = 0.05f;

                    if (e.KeyCode == 37)
                    {
                        // left
                        rCube += turnspeed;
                    }

                    if (e.KeyCode == 65)
                    {
                        // left
                        rCube += turnspeed;
                    }


                    if (e.KeyCode == 39)
                    {
                        rCube -= turnspeed;

                        // right
                    }

                    if (e.KeyCode == 68)
                    {
                        rCube -= turnspeed;

                        // right
                    }

                    if (e.KeyCode == 38)
                    {
                        // mat aint working ..

                        ego_y += (float)Math.Sin(rCube) * 0.1f;
                        ego_x += (float)Math.Cos(rCube) * 0.1f;

                        //ego_x += 0.1f;

                        // right
                    }



                    if (e.KeyCode == 40)
                    {
                        ego_y += (float)Math.Sin(rCube) * -0.1f;
                        ego_x += (float)Math.Cos(rCube) * -0.1f;

                        // right
                    }
                };
            #endregion

        }

    }


}
