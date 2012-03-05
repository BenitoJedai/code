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
using ScriptCoreLib.Shared.Lambda;
using SpiderModel.HTML.Pages;
using SpiderModel.Library;
using SpiderModel.Shaders;

namespace SpiderModel
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;
    using ScriptCoreLib.Shared.Avalon.Tween;
    using System.Windows;



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
            new ApplicationContent().With(
                Content =>
                {
                    Content.AtTick +=
                        delegate
                        {
                            Native.Document.title = "" + Content.cycle;
                        };

                    Native.Document.onclick +=
                        e =>
                        {
                        };

                    Native.Document.onmousemove +=
                        e =>
                        {
                            var x = e.CursorX / Native.Window.Width;
                            var y = e.CursorY / Native.Window.Height;
                            var cx = 1f - x;
                            var cy = 1f - y;

                            if (x < 0.2)
                            {
                                Content.red_obstacle_L_y = (cy) * 30f;
                            }
                            else if (cx < 0.2)
                            {
                                Content.red_obstacle_R_y = (cy) * 30f;
                            }
                            else
                            {
                                Content.white_arrow_x = (x - 0.5f) * 30f;
                                Content.white_arrow_y = (cy) * 30f;
                            }
                        };
                }
            );

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
        public IHTMLCanvas canvas = new IHTMLCanvas();

        public ApplicationContent()
        {
            canvas.AttachToDocument();

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

            #region tween
            NumericEmitter.OfDouble(
                (value, reserved) => red_obstacle_L_y = (f)value
            ).With(
                e =>
                {
                    tween_red_obstacle_L_y = (value) => e(value, 0);
                }
            );

            NumericEmitter.OfDouble(
               (value, reserved) => red_obstacle_R_y = (f)value
           ).With(
               e =>
               {
                   tween_red_obstacle_R_y = (value) => e(value, 0);
               }
           );

            NumericEmitter.OfDouble(
             (value, reserved) => white_arrow_x = (f)value
         ).With(
             e =>
             {
                 tween_white_arrow_x = (value) => e(value, 0);
             }
         );

            NumericEmitter.OfDouble(
      (value, reserved) => white_arrow_y = (f)value
  ).With(
      e =>
      {
          tween_white_arrow_y = (value) => e(value, 0);
      }
  );
            #endregion

        }

        // 0..30
        public sealed class vec2
        {
            public f x;
            public f y;
        }

        public Queue<vec2> white_arrows = new Queue<vec2>();

        public f white_arrow_x;
        public f white_arrow_y = 20f;

        public Action<f> tween_white_arrow_x;
        public Action<f> tween_white_arrow_y;
        public Action<f> tween_red_obstacle_L_y;
        public Action<f> tween_red_obstacle_R_y;

        public f red_obstacle_L_y = 20f;
        public f red_obstacle_R_y = 16f;

        public f t_local = 0;
        public f t_fix = 0;

        public f t
        {
            get
            {
                return t_local + t_fix;
            }
        }

        public f cycle
        {
            get
            {
                return (f)(Math.Floor((t % (Math.PI * 4)) / (Math.PI * 0.5)));
            }
        }


        public event Action AtTick;

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



            var size = 0.03f;


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

            #region colors_cyan
            var cubeVertexColorBuffer_cyan = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer_cyan);
            var colors_cyan = new[]{
                0.0f, 1.0f, 1.0f, 1.0f, 
                0.0f, 1.0f, 1.0f, 1.0f, 
                0.0f, 1.0f, 1.0f, 1.0f, 
                0.0f, 1.0f, 1.0f, 1.0f, 

                0.0f, 1.0f, 1.0f, 1.0f, 
                0.0f, 1.0f, 1.0f, 1.0f, 
                0.0f, 1.0f, 1.0f, 1.0f, 
                0.0f, 1.0f, 1.0f, 1.0f, 

                0.0f, 1.0f, 1.0f, 1.0f, 
                0.0f, 1.0f, 1.0f, 1.0f, 
                0.0f, 1.0f, 1.0f, 1.0f, 
                0.0f, 1.0f, 1.0f, 1.0f, 


                 0.0f, 1.0f, 1.0f, 1.0f, 
                0.0f, 1.0f, 1.0f, 1.0f, 
                0.0f, 1.0f, 1.0f, 1.0f, 
                0.0f, 1.0f, 1.0f, 1.0f, 

                0.0f, 1.0f, 1.0f, 1.0f, 
                0.0f, 1.0f, 1.0f, 1.0f, 
                0.0f, 1.0f, 1.0f, 1.0f, 
                0.0f, 1.0f, 1.0f, 1.0f, 

                0.0f, 1.0f, 1.0f, 1.0f, 
                0.0f, 1.0f, 1.0f, 1.0f, 
                0.0f, 1.0f, 1.0f, 1.0f, 
                0.0f, 1.0f, 1.0f, 1.0f, 
            };



            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(colors_cyan), gl.STATIC_DRAW);
            #endregion

            #region colors_white
            var cubeVertexColorBuffer_white = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer_white);
            var colors_white = new[]{
                1.0f, 1.0f, 1.0f, 1.0f, 
                1.0f, 1.0f, 1.0f, 1.0f, 
                1.0f, 1.0f, 1.0f, 1.0f, 
                1.0f, 1.0f, 1.0f, 1.0f, 

             
                1.0f, 1.0f, 1.0f, 1.0f,
                1.0f, 1.0f, 1.0f, 1.0f,
                1.0f, 1.0f, 1.0f, 1.0f,
                1.0f, 1.0f, 1.0f, 1.0f,

                1.0f, 1.0f, 1.0f, 1.0f,
                1.0f, 1.0f, 1.0f, 1.0f,
                1.0f, 1.0f, 1.0f, 1.0f,
                1.0f, 1.0f, 1.0f, 1.0f,

                 1.0f, 1.0f, 1.0f, 1.0f, 
                1.0f, 1.0f, 1.0f, 1.0f, 
                1.0f, 1.0f, 1.0f, 1.0f, 
                1.0f, 1.0f, 1.0f, 1.0f, 

             
                1.0f, 1.0f, 1.0f, 1.0f,
                1.0f, 1.0f, 1.0f, 1.0f,
                1.0f, 1.0f, 1.0f, 1.0f,
                1.0f, 1.0f, 1.0f, 1.0f,

                1.0f, 1.0f, 1.0f, 1.0f,
                1.0f, 1.0f, 1.0f, 1.0f,
                1.0f, 1.0f, 1.0f, 1.0f,
                1.0f, 1.0f, 1.0f, 1.0f,

                
            
            
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

            var t_start = new IDate().getTime();

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

                t_local = (timeNow - t_start) / 1000;


                //Native.Document.title = "t: " + t;
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

                #region colors
                Action red =
                    delegate
                    {
                        gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer_red);
                        gl.vertexAttribPointer((ulong)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);

                    };

                Action green =
                  delegate
                  {
                      gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer_green);
                      gl.vertexAttribPointer((ulong)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);

                  };

                Action orange =
                delegate
                {
                    gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer_orange);
                    gl.vertexAttribPointer((ulong)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);

                };

                Action white =
                 delegate
                 {
                     gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer_white);
                     gl.vertexAttribPointer((ulong)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);

                 };

                Action cyan =
                delegate
                {
                    gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer_cyan);
                    gl.vertexAttribPointer((ulong)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);

                };
                #endregion


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

                        #region arrow
                        Action arrow =
                            delegate
                            {
                                __glMatrix.mat4.translate(mvMatrix, 0, 0, (float)(Math.Sin(rCube * 0.1) * size));
                                __glMatrix.mat4.rotate(mvMatrix, degToRad(rCube * 0.1f), 0f, 0f, -1f);

                                __glMatrix.mat4.rotate(mvMatrix, degToRad(rCube * -0.5f), 0f, 0f, 1f);


                                #region // __X__
                                if (cycle == 7)
                                    orange();
                                else
                                    white();
                                draw(0, 0, 0);
                                #endregion
                                #region // _XXX_
                                if (cycle == 6)
                                    orange();
                                else
                                    white();

                                draw(-1, 0, 1);
                                draw(1, 0, 1);



                                draw(0, 0, 1);



                                #endregion
                                #region // XXXXX
                                if (cycle == 5)
                                    orange();
                                else
                                    white();

                                draw(-2, 0, 2);
                                draw(2, 0, 2);
                                draw(-1, 0, 2);
                                draw(1, 0, 2);



                                draw(0, 0, 2);


                                #endregion
                                #region //XXXXXXX
                                if (cycle == 4)
                                    orange();
                                else
                                    white();
                                draw(-3, 0, 3);
                                draw(-2, 0, 3);
                                draw(2, 0, 3);
                                draw(3, 0, 3);
                                draw(-1, 0, 3);
                                draw(1, 0, 3);




                                draw(0, 0, 3);


                                #endregion
                                #region // __X__

                                if (cycle == 3)
                                    orange();
                                else
                                    white();

                                draw(0, 0, 4);
                                #endregion
                                #region // __X__

                                if (cycle == 2)
                                    orange();
                                else
                                    white();

                                draw(0, 0, 5);
                                #endregion
                                #region // __X__
                                if (cycle == 1)
                                    orange();
                                else
                                    white();
                                draw(0, 0, 6);
                                #endregion
                                #region // __X__
                                if (cycle == 0)
                                    orange();
                                else
                                    white();
                                draw(0, 0, 7);
                                #endregion
                            };
                        #endregion


                        if (white_arrow_y < 8)
                            cyan();
                        else
                            white();

                        #region white arrow
                        at(white_arrow_x, white_arrow_y, 1, arrow);
                        #endregion

                        #region draw_obstacle


                        drawElements_mode = gl.LINE_STRIP;
                        white();
                        white_arrows.WithEach(p => at(p.x, p.y, 1, arrow));

                        Action draw_obstacle = delegate
                        {
                            draw(-1, 0, 0);
                            draw(-1, 0, 2);
                            draw(0, 0, 1);
                            draw(1, 0, 0);
                            draw(1, 0, 2);
                        };

                        if (red_obstacle_L_y < 8)
                            red();
                        else if (red_obstacle_L_y < 16)
                            orange();
                        else
                            green();

                        // red
                        at(-10, red_obstacle_L_y, 0, draw_obstacle);
                        at(-6, red_obstacle_L_y, 0, draw_obstacle);
                        at(-2, (red_obstacle_L_y + red_obstacle_L_y + red_obstacle_R_y) / 3, 0, draw_obstacle);

                        if (red_obstacle_R_y < 8)
                            red();
                        else if (red_obstacle_R_y < 16)
                            orange();
                        else
                            green();

                        at(2, (red_obstacle_R_y + red_obstacle_R_y + red_obstacle_L_y) / 3, 0, draw_obstacle);
                        at(6, red_obstacle_R_y, 0, draw_obstacle);
                        at(10, red_obstacle_R_y, 0, draw_obstacle);
                        #endregion


                        drawElements_mode = gl.TRIANGLES;


                        orange();

                        bool legpart_fill = true;

                        #region leg
                        Action<Action, Action, f, f, f> legpart =
                            (legcolor, fillcolor, x, y, z) =>
                            {
                                if (legpart_fill)
                                {
                                    drawElements_mode = gl.TRIANGLES;
                                    fillcolor();
                                    draw(x, y, z);
                                }

                                drawElements_mode = gl.LINE_STRIP;
                                legcolor();
                                draw(x, y, z);
                                fillcolor();
                                drawElements_mode = gl.TRIANGLES;
                            };

                        Action<Action, Action> leg =
                            (legcolor, fillcolor) =>
                            {
                                legpart(legcolor, fillcolor, 0, 6, 0);
                                legpart(legcolor, fillcolor, 0, 5, 1);
                                legpart(legcolor, fillcolor, 0, 4, 2);
                                legpart(legcolor, fillcolor, 0, 3, 2);
                                legpart(legcolor, fillcolor, 0, 2, 1);
                            };

                        #endregion

                        #region program_leg1
                        Action<f> program_leg1 = phase =>
                        {
                            __glMatrix.mat4.rotate(mvMatrix, degToRad(
                                  0
                                  + (f)(Math.Cos(t + phase) * 45)

                              ), new float[] { 0f, 0f, 1f });

                            __glMatrix.mat4.rotate(mvMatrix, degToRad(
                             (float)Math.Max(0, Math.Sin(t + phase) * 45)

                             ), 1f, 0f, 0);
                        };
                        #endregion

                        #region program_leg1_duet
                        Action<f, f> program_leg1_duet = (phase, sidewaysrange) =>
                        {
                            var tphase = t + phase;

                            var up = Math.Max(0, Math.Sign(Math.Sin(tphase / 2)));
                            var sideways = Math.Max(0, Math.Sign(Math.Cos(tphase / 2)));

                            __glMatrix.mat4.rotate(mvMatrix, degToRad(
                                  0
                                  + (f)(

                                        (
                                            ((1 - sideways) * -1) +
                                            sideways * Math.Cos(tphase)
                                        )
                                        * sidewaysrange
                                    )

                              ), 0f, 0f, 1f);

                            __glMatrix.mat4.rotate(mvMatrix, degToRad(
                             (float)Math.Max(0, up * Math.Sin(tphase) * 45)

                             ), 1f, 0f, 0);
                        };
                        #endregion

                        #region right front - RED
                        legpart_fill = false;
                        mw(
                            delegate
                            {
                                __glMatrix.mat4.rotate(mvMatrix, degToRad(-45), new float[] { 0f, 0f, 1f });

                                leg(red, orange);

                            }
                        );
                        legpart_fill = true;
                        mw(
                            delegate
                            {
                                __glMatrix.mat4.rotate(mvMatrix, degToRad(-45), new float[] { 0f, 0f, 1f });
                                program_leg1_duet(0, 33);



                                leg(red, orange);

                            }
                        );
                        #endregion



                        #region left front - GREEN
                        legpart_fill = false;
                        mw(
                            delegate
                            {

                                __glMatrix.mat4.rotate(mvMatrix, degToRad(45), new float[] { 0f, 0f, 1f });

                                leg(green, orange);

                            }
                        );
                        legpart_fill = true;
                        mw(
                            delegate
                            {

                                __glMatrix.mat4.rotate(mvMatrix, degToRad(45), new float[] { 0f, 0f, 1f });
                                program_leg1_duet(0, 33);

                                leg(green, orange);

                            }
                        );
                        #endregion


                        #region leg right back - BLUE
                        legpart_fill = false;
                        mw(
                            delegate
                            {
                                __glMatrix.mat4.rotate(mvMatrix, degToRad(45 + 180), new float[] { 0f, 0f, 1f });

                        
                                leg(cyan, green);
                            }
                        );
                        legpart_fill = true;
                        mw(
                            delegate
                            {
                                __glMatrix.mat4.rotate(mvMatrix, degToRad(45 + 180), new float[] { 0f, 0f, 1f });
                                program_leg1_duet(0, 33);


                                leg(cyan, green);
                            }
                        );
                        #endregion

                        #region leg left back - WHITE

                        legpart_fill = false;
                        mw(delegate
                        {
                            // 0.5 180
                            // 0.25 90
                            // 0.125 45

                            __glMatrix.mat4.rotate(mvMatrix, degToRad(180 * 0.75f), 0f, 0f, 1f);
                            leg(white, green);
                        }
                     );
                        legpart_fill = true;

                        mw(delegate
                             {
                                 // 0.5 180
                                 // 0.25 90
                                 // 0.125 45

                                 __glMatrix.mat4.rotate(mvMatrix, degToRad(180 * 0.75f), 0f, 0f, 1f);
                                 program_leg1_duet(0, 33);
                                 leg(white, green);
                             }
                         );




                        #endregion




                        at(0, 0, 0.5f,
                              delegate
                              {
                                  #region body

                                  green();

                                  draw(-1, 0, 0);
                                  draw(1, 0, 0);

                                  orange();

                                  draw(0, -1, 0);
                                  draw(0, 1, 0);




                                  #endregion
                              }
                          );



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

            #region tick
            var tick = default(Action);

            tick = delegate
            {
                c++;

                //Native.Document.title = "" + c;

                drawScene();
                animate();

                if (AtTick != null)
                    AtTick();

                Native.Window.requestAnimationFrame += tick;
            };

            tick();
            #endregion

            #region white_arrows
            new ScriptCoreLib.JavaScript.Runtime.Timer(
                delegate
                {
                    white_arrows.Enqueue(
                        new vec2
                        {
                            x = white_arrow_x,
                            y = white_arrow_y
                        }
                    );

                    if (white_arrows.Count > 12)
                        white_arrows.Dequeue();

                }
            ).StartInterval(1000 / 15);
            #endregion


            Native.Document.body.ondblclick +=
                delegate
                {
                    // http://tutorialzine.com/2012/02/enhance-your-website-fullscreen-api/

                    #region requestFullscreen
                    var requestFullscreen = new IFunction(@"
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

                    requestFullscreen.apply(Native.Document.body);
                    #endregion

                };

        }

    }
}
