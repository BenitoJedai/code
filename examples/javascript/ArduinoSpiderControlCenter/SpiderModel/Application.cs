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
using SpiderModel.HTML.Pages;
using SpiderModel.Library;
using SpiderModel.Shaders;

namespace SpiderModel
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;



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
        public Application(IDefaultPage page = null)
        {
            new ApplicationContent();

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }


    }

    public class ApplicationContent
    {
        public ApplicationContent()
        {
            var canvas = new IHTMLCanvas().AttachToDocument();

            #region glMatrix.js -> InitializeContent
            new __glMatrix().Content.With(
               source =>
               {
                   source.onload +=
                       delegate
                       {
                           InitializeContent(canvas);
                       };

                   source.AttachToDocument();
               }
           );
            #endregion


        }

        void InitializeContent(IHTMLCanvas canvas)
        {


            #region canvas

            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;

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


            var gl_viewportWidth = Native.Window.Width;
            var gl_viewportHeight = Native.Window.Height;



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

            #region initShaders
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



            var size = 0.06f;


            #region cube
            var cubeVertexPositionBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
            #region vertices
            var vertices = new[]{

                // Front face RED
                -size, -size,  size,
                 size, -size,  size,
                 size,  size,  size,
                -size,  size,  size,

                // Back face YELLOW
                -size, -size, -size,
                -size,  size, -size,
                 size,  size, -size,
                 size, -size, -size,

                // Top face GREEN
                -size,  size, -size,
                -size,  size,  size,
                 size,  size,  size,
                 size,  size, -size,

                // Bottom face BEIGE
                -size, -size, -size,
                 size, -size, -size,
                 size, -size,  size,
                -size, -size,  size,

                // Right face PURPLE
                 size, -size, -size,
                 size,  size, -size,
                 size,  size,  size,
                 size, -size,  size,

                // Left face BLUE
                -size, -size, -size,
                -size, -size,  size,
                -size,  size,  size,
                -size,  size, -size
            };
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices), gl.STATIC_DRAW);
            #endregion


            var cubeVertexPositionBuffer_itemSize = 3;
            var cubeVertexPositionBuffer_numItems = 6 * 6;

            #region colors_orange
            var cubeVertexColorBuffer_orange = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer_orange);
            var colors_orange = new[]{
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



            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(colors_orange), gl.STATIC_DRAW);
            #endregion

            #region colors_white
            var cubeVertexColorBuffer_white = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer_white);
            var colors_white = new[]{
                1.0f, 1.0f, 1.0f, 1.0f, // Front face
                1.0f, 1.0f, 1.0f, 1.0f, // Front face
                1.0f, 1.0f, 1.0f, 1.0f, // Front face
                1.0f, 1.0f, 1.0f, 1.0f, // Front face

                0.8f, 0.4f, 0.0f, 1.0f, // Back face
                0.8f, 0.4f, 0.0f, 1.0f, // Back face
                0.8f, 0.4f, 0.0f, 1.0f, // Back face
                0.8f, 0.4f, 0.0f, 1.0f, // Back face

                1.0f, 1.0f, 1.0f, 1.0f, // Top face
                1.0f, 1.0f, 1.0f, 1.0f, // Top face
                1.0f, 1.0f, 1.0f, 1.0f, // Top face
                1.0f, 1.0f, 1.0f, 1.0f, // Top face

                1.0f, 1.0f, 1.0f, 1.0f, // Bottom face
                1.0f, 1.0f, 1.0f, 1.0f, // Bottom face
                1.0f, 1.0f, 1.0f, 1.0f, // Bottom face
                1.0f, 1.0f, 1.0f, 1.0f, // Bottom face

                
                0.9f, 0.9f, 0.9f, 1.0f, // Right face
                0.9f, 0.9f, 0.9f, 1.0f, // Right face
                0.9f, 0.9f, 0.9f, 1.0f, // Right face
                0.9f, 0.9f, 0.9f, 1.0f, // Right face

                0.9f, 0.9f, 0.9f, 1.0f,  // Left face
                0.9f, 0.9f, 0.9f, 1.0f,  // Left face
                0.9f, 0.9f, 0.9f, 1.0f,  // Left face
                0.9f, 0.9f, 0.9f, 1.0f  // Left face
            };



            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(colors_white), gl.STATIC_DRAW);
            #endregion

            #region colors_red
            var cubeVertexColorBuffer_red = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer_red);
            var colors_red = new[]{
                1.0f, 0.0f, 0.0f, 1.0f, // Front face
                1.0f, 0.0f, 0.0f, 1.0f, // Front face
                1.0f, 0.0f, 0.0f, 1.0f, // Front face
                1.0f, 0.0f, 0.0f, 1.0f, // Front face

                0.8f, 0.4f, 0.0f, 1.0f, // Back face
                0.8f, 0.4f, 0.0f, 1.0f, // Back face
                0.8f, 0.4f, 0.0f, 1.0f, // Back face
                0.8f, 0.4f, 0.0f, 1.0f, // Back face

                8.0f, 0.0f, 0.0f, 1.0f, // Top face
                8.0f, 0.0f, 0.0f, 1.0f, // Top face
                8.0f, 0.0f, 0.0f, 1.0f, // Top face
                8.0f, 0.0f, 0.0f, 1.0f, // Top face

                8.0f, 0.0f, 0.0f, 1.0f, // Bottom face
                8.0f, 0.0f, 0.0f, 1.0f, // Bottom face
                8.0f, 0.0f, 0.0f, 1.0f, // Bottom face
                8.0f, 0.0f, 0.0f, 1.0f, // Bottom face

                
                9.0f, 0.0f, 0.0f, 1.0f, // Right face
                9.0f, 0.0f, 0.0f, 1.0f, // Right face
                9.0f, 0.0f, 0.0f, 1.0f, // Right face
                9.0f, 0.0f, 0.0f, 1.0f, // Right face

                9.0f, 0.0f, 0.0f, 1.0f,  // Left face
                9.0f, 0.0f, 0.0f, 1.0f,  // Left face
                9.0f, 0.0f, 0.0f, 1.0f,  // Left face
                9.0f, 0.0f, 0.0f, 1.0f  // Left face
            };



            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(colors_red), gl.STATIC_DRAW);
            #endregion

            #region colors_green
            var cubeVertexColorBuffer_green = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer_green);
            var colors_green = new[]{
                0.0f, 0.9f, 0.0f, 1.0f, // Front face
                0.0f, 0.9f, 0.0f, 1.0f, // Front face
                0.0f, 0.9f, 0.0f, 1.0f, // Front face
                0.0f, 0.9f, 0.0f, 1.0f, // Front face

                0.8f, 0.4f, 0.0f, 1.0f, // Back face
                0.8f, 0.4f, 0.0f, 1.0f, // Back face
                0.8f, 0.4f, 0.0f, 1.0f, // Back face
                0.8f, 0.4f, 0.0f, 1.0f, // Back face

                0.0f, 0.6f, 0.0f, 1.0f, // Top face
                0.0f, 0.6f, 0.0f, 1.0f, // Top face
                0.0f, 0.6f, 0.0f, 1.0f, // Top face
                0.0f, 0.6f, 0.0f, 1.0f, // Top face

                0.0f, 0.6f, 0.0f, 1.0f, // Bottom face
                0.0f, 0.6f, 0.0f, 1.0f, // Bottom face
                0.0f, 0.6f, 0.0f, 1.0f, // Bottom face
                0.0f, 0.6f, 0.0f, 1.0f, // Bottom face

                
                0.0f, 0.8f, 0.0f, 1.0f, // Right face
                0.0f, 0.8f, 0.0f, 1.0f, // Right face
                0.0f, 0.8f, 0.0f, 1.0f, // Right face
                0.0f, 0.8f, 0.0f, 1.0f, // Right face

                0.0f, 0.8f, 0.0f, 1.0f,  // Left face
                0.0f, 0.8f, 0.0f, 1.0f,  // Left face
                0.0f, 0.8f, 0.0f, 1.0f,  // Left face
                0.0f, 0.8f, 0.0f, 1.0f  // Left face
            };



            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(colors_green), gl.STATIC_DRAW);
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
            var cubeVertexIndexBuffer_numItems = cubeVertexPositionBuffer_numItems;

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

            var camera_z = 0f;

            #region drawScene
            Action drawScene = delegate
            {
                gl.viewport(0, 0, gl_viewportWidth, gl_viewportHeight);
                gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

                __glMatrix.mat4.perspective(45f, (float)gl_viewportWidth / (float)gl_viewportHeight, 0.1f, 100.0f, pMatrix);

                __glMatrix.mat4.identity(mvMatrix);


                Action<Action> mw =
                 h =>
                 {
                     mvPushMatrix();
                     h();
                     mvPopMatrix();
                 };


                mw(
                    delegate
                    {
                        __glMatrix.mat4.translate(mvMatrix, 0f, 0.0f, -4f);
                        __glMatrix.mat4.rotate(mvMatrix, degToRad(-70), new float[] { 1f, 0f, 0f });
                        __glMatrix.mat4.rotate(mvMatrix, degToRad(rCube * 0.1f), new float[] { 0f, 0f, 1f });
                        __glMatrix.mat4.translate(mvMatrix, 0f, camera_z, 0f);



                        gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
                        gl.vertexAttribPointer((ulong)shaderProgram_vertexPositionAttribute, cubeVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);


                        gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);




                        // gl.TRIANGLES;
                        var drawElements_mode = gl.TRIANGLES;

                        #region draw
                        Action<f, f, f> draw =
                            (x, y, z) =>
                            {
                                mw(
                                    delegate
                                    {
                                        __glMatrix.mat4.translate(mvMatrix, x * size * 2, y * size * 2, z * size * 2);

                                        setMatrixUniforms();
                                        gl.drawElements(drawElements_mode, cubeVertexIndexBuffer_numItems, gl.UNSIGNED_SHORT, 0);
                                        //gl.drawElements(gl.TRIANGLES, cubeVertexIndexBuffer_numItems, gl.UNSIGNED_SHORT, 0);
                                    }
                                );
                            };
                        #endregion


                        #region at
                        Action<f, f, f, Action> at = (x, y, z, h) =>
                        {
                            mw(
                                  delegate
                                  {
                                      __glMatrix.mat4.translate(mvMatrix, x * size * 2, y * size * 2, z * size * 2);

                                      h();
                                  }
                          );
                        };
                        #endregion

                        gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer_white);
                        gl.vertexAttribPointer((ulong)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);


                        #region white arrow
                        at(0, 32, 1,
                            delegate
                            {

                                __glMatrix.mat4.translate(mvMatrix, 0, 0, (float)(Math.Sin(rCube * 0.1) * size));
                                __glMatrix.mat4.rotate(mvMatrix, degToRad(rCube * 0.1f), 0f, 0f, -1f);

                                __glMatrix.mat4.rotate(mvMatrix, degToRad(rCube * -0.5f), 0f, 0f, 1f);


                                #region // __X__
                                draw(0, 0, 0);
                                #endregion
                                #region // _XXX_
                                draw(0, 0, 1);
                                draw(-1, 0, 1);
                                draw(1, 0, 1);

                           

                                #endregion
                                #region // XXXXX
                                draw(0, 0, 2);
                                draw(-2, 0, 2);
                                draw(-1, 0, 2);
                                draw(1, 0, 2);
                                draw(2, 0, 2);

                         
                                #endregion
                                #region // __X__
                                draw(0, 0, 3);
                                #endregion
                                #region // __X__
                                draw(0, 0, 4);
                                #endregion
                                #region // __X__
                                draw(0, 0, 5);
                                #endregion
                            }
                        );
                        #endregion

                        #region draw_obstacle
                        gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer_red);
                        gl.vertexAttribPointer((ulong)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);


                        drawElements_mode = gl.LINE_STRIP;

                        Action draw_obstacle = delegate
                        {
                            draw(-1, 0, 0);
                            draw(-1, 0, 2);
                            draw(0, 0, 1);
                            draw(1, 0, 0);
                            draw(1, 0, 2);
                        };

                        // red
                        at(-6, 16, 0, draw_obstacle);
                        at(-2, 16, 0, draw_obstacle);
                        at(2, 16, 0, draw_obstacle);
                        at(6, 16, 0, draw_obstacle);
                        #endregion


                        drawElements_mode = gl.TRIANGLES;


                        gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer_orange);
                        gl.vertexAttribPointer((ulong)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);


                        draw(4, 7, 0);
                        draw(4, 6, 0);
                        draw(4, 5, 0);
                        draw(4, 4, 0);
                        draw(4, 3, 0);

                        draw(2, 1, 1);
                        draw(3, 1, 1);
                        draw(2, 2, 1);
                        draw(3, 2, 1);

                        #region body
                        gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer_green);
                        gl.vertexAttribPointer((ulong)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);

                        at(0, 0, 2,
                          delegate
                          {



                              draw(-1, 0, 0);
                              draw(0, -1, 0);

                              draw(0, 0, 0);

                              draw(1, 0, 0);
                              draw(0, 1, 0);


                          }
                      );
                        #endregion

                    }
                );

            };
            drawScene();
            #endregion

            #region AtResize
            Action AtResize = delegate
            {
                canvas.style.SetLocation(0, 0, Native.Window.Width, Native.Window.Height);

                gl_viewportWidth = Native.Window.Width;
                gl_viewportHeight = Native.Window.Height;

                canvas.width = Native.Window.Width;
                canvas.height = Native.Window.Height;
            };

            AtResize();

            Native.Window.onresize += delegate { AtResize(); };
            #endregion

            Native.Document.body.onmousewheel +=
                e =>
                {
                    camera_z += e.WheelDirection * 0.1f;
                };

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

            canvas.ondblclick +=
                delegate
                {
                    // http://tutorialzine.com/2012/02/enhance-your-website-fullscreen-api/

                    var request = new IFunction(@"
		if (this.requestFullscreen) {
		    this.requestFullscreen();
		}
		else if (this.mozRequestFullScreen) {
		    this.mozRequestFullScreen();
		}
		else if (this.webkitRequestFullScreen) {
		    this.webkitRequestFullScreen();
		}
                    
                    "
                    );

                    request.apply(canvas);
                };

        }
    }
}
