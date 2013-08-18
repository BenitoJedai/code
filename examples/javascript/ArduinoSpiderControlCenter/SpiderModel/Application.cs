using System;
using System.Collections.Generic;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.WebGL;
using ScriptCoreLib.Shared.Avalon.Tween;
using SpiderModel.HTML.Pages;
using SpiderModel.Shaders;

namespace SpiderModel
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;


    using notify = Action<float, float>;

    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public Action Dispose;

        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page = null)
        {
            new ApplicationContent().With(
                Content =>
                {
                    Dispose = () => Content.Dispose();

                    var hh = new IHTMLDiv();

                    //hh.AttachToDocument();
                    hh.style.SetLocation(64, 32);
                    hh.style.color = JSColor.White;
                    hh.style.fontSize = "30px";


                    var vv = new IHTMLIFrame
                    {
                        border = "0",

                        // http://stackoverflow.com/questions/5845484/force-html5-youtube-video
                        //src = "http://www.youtube.com/embed/hKksAVmekAE?html5=1"

                        src = "http://www.youtube.com/embed/hKksAVmekAE"
                    };

                    vv.style.position = IStyle.PositionEnum.absolute;
                    vv.style.left = "0px";
                    vv.style.width = "20em";
                    vv.style.bottom = "0px";
                    vv.style.height = "10em";
                    vv.style.border = "0";

                    //vv.AttachToDocument();


                    new ScriptCoreLib.JavaScript.Runtime.Timer(
                        t =>
                        {
                            var c = t.Counter % 10;

                            Action<int, int> rewire =
                                (_c, _po) =>
                                {
                                    if (c == _c)
                                    {
                                        Content.po = _po;
                                        hh.innerText = "Spider Control Center Program Override Code: " + _po;
                                    }
                                };

                            rewire(1, 43);
                            rewire(2, 53);
                            rewire(3, 13);
                            rewire(4, 14);
                            rewire(5, 15);
                            rewire(6, 16);
                            rewire(7, 17);
                            rewire(8, 18);
                            rewire(9, 23);
                        },
                        0,
                        2000
                    );

                    Content.AtTick +=
                        delegate
                        {
                            // ?
                        };

                    Native.document.onclick +=
                        e =>
                        {
                        };

                    Native.document.onmousemove +=
                        e =>
                        {
                            var x = e.CursorX / Native.window.Width;
                            var y = e.CursorY / Native.window.Height;
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


        }


    }

    public class ApplicationContent
    {


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

        public f cyclecount = 6;

        public f cycle
        {
            get
            {
                return (f)(Math.Ceiling((t / Math.PI) % cyclecount));
            }
        }

        public float a = 0f;
        public float camera_z = -1.7f;

        public event Action AtTick;


        public IHTMLCanvas canvas = new IHTMLCanvas();

        public ApplicationContent()
        {
            canvas.AttachToDocument();



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



            #region canvas

            Native.document.body.style.overflow = IStyle.OverflowEnum.hidden;

            #endregion

            #region gl - Initialise WebGL


            var gl = new WebGLRenderingContext();

            try
            {

                gl = (WebGLRenderingContext)canvas.getContext("experimental-webgl");

            }
            catch { }

            if (gl == null)
            {
                Native.window.alert("WebGL not supported");
                throw new InvalidOperationException("cannot create webgl context");
            }
            #endregion


            var gl_viewportWidth = Native.window.Width;
            var gl_viewportHeight = Native.window.Height;



            var shaderProgram = gl.createProgram();


            #region createShader
            Func<ScriptCoreLib.GLSL.Shader, WebGLShader> createShader = (src) =>
            {
                var shader = gl.createShader(src);

                // verify
                if (gl.getShaderParameter(shader, gl.COMPILE_STATUS) == null)
                {
                    Native.window.alert("error in SHADER:\n" + gl.getShaderInfoLog(shader));
                    throw new InvalidOperationException("shader failed");
                }

                return shader;
            };
            #endregion

            #region initShaders
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
            #endregion



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


            var t_start = new IDate().getTime();

            var lastTime = 0L;
            Action animate = delegate
            {
                var timeNow = new IDate().getTime();
                if (lastTime != 0)
                {
                    var elapsed = timeNow - lastTime;

                    a -= (75 * elapsed) / 1000.0f;
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



            #region drawScene
            Action drawScene = delegate
            {
                gl.viewport(0, 0, gl_viewportWidth, gl_viewportHeight);
                gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

                glMatrix.mat4.perspective(45f, (float)gl_viewportWidth / (float)gl_viewportHeight, 0.1f, 100.0f, pMatrix);

                glMatrix.mat4.identity(mvMatrix);


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
                        gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);

                    };

                Action green =
                  delegate
                  {
                      gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer_green);
                      gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);

                  };

                Action orange =
                delegate
                {
                    gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer_orange);
                    gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);

                };

                Action white =
                 delegate
                 {
                     gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer_white);
                     gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);

                 };

                Action cyan =
                delegate
                {
                    gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer_cyan);
                    gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);

                };
                #endregion


                mw(
                    delegate
                    {
                        glMatrix.mat4.translate(mvMatrix, new[] { 0f, 0.0f, -4f });
                        glMatrix.mat4.rotate(mvMatrix, degToRad(-70), new[] { 1f, 0f, 0f });
                        glMatrix.mat4.rotate(mvMatrix, degToRad((f)(Math.Sin(a * 0.0003f) * 33)), new[] { 0f, 0f, 1f });
                        glMatrix.mat4.translate(mvMatrix, new[] { 0f, camera_z, 0f });



                        gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
                        gl.vertexAttribPointer((uint)shaderProgram_vertexPositionAttribute, cubeVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);


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
                                        glMatrix.mat4.translate(mvMatrix, new[] { x * size * 2, y * size * 2, z * size * 2 });

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
                                      glMatrix.mat4.translate(mvMatrix, new[] { x * size * 2, y * size * 2, z * size * 2 });

                                      h();
                                  }
                          );
                        };
                        #endregion

                        #region arrow
                        Action arrow =
                            delegate
                            {
                                glMatrix.mat4.translate(mvMatrix, new[] { 0, 0, (float)(Math.Sin(a * 0.1) * size) });
                                glMatrix.mat4.rotate(mvMatrix, degToRad(a * 0.1f), new[] { 0f, 0f, -1f });

                                glMatrix.mat4.rotate(mvMatrix, degToRad(a * -0.5f), new[] { 0f, 0f, 1f });

                                var cc = (cyclecount - cycle) - 1;

                                #region // __X__
                                if (cc == 0)
                                    green();
                                else
                                    white();
                                draw(0, 0, 0);
                                #endregion
                                #region // _XXX_
                                if (cc == 1)
                                    red();
                                else
                                    white();

                                draw(-1, 0, 1);
                                draw(1, 0, 1);



                                draw(0, 0, 1);



                                #endregion
                                #region // XXXXX
                                if (cc == 2)
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
                                if (cc == 3)
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

                                if (cc == 4)
                                    orange();
                                else
                                    white();

                                draw(0, 0, 4);
                                #endregion
                                #region // __X__

                                if (cc == 5)
                                    orange();
                                else
                                    white();

                                draw(0, 0, 5);
                                #endregion
                                #region // __X__
                                if (cc == 6)
                                    orange();
                                else
                                    white();
                                draw(0, 0, 6);
                                #endregion
                                #region // __X__
                                if (cc == 7)
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

                        var sidewaysrange = 20;
                        var verticalrange = 30;


                        f pi = (f)Math.PI;
                        Func<f, f> cos = x => (f)Math.Cos(x);
                        Func<f, f> sin = x => (f)Math.Sin(x);
                        Func<f, f, f> max = (x, y) => (f)Math.Max(x, y);
                        // http://arduino.cc/en/Reference/max

                        #region program_leg0
                        Action<f, Action<f, f>> program_leg0 =
                            /* void program_leg0 */ (float tphase, notify notify) =>
                            {
                                float deg_sideway = (cos(tphase) * sidewaysrange);
                                float deg_vertical = max(0, sin(tphase) * verticalrange);

                                notify(deg_sideway, deg_vertical);
                            }
                            ;
                        #endregion


                        #region program_leg_delay_move_hold_commit
                        Action<int, int, int, Action<f, f>> program_leg_delay_move_hold_commit =

                        /* void program_leg_delay_move_hold_commit */ (int _delay, int hold, int reverse, notify notify) =>
                        {
                            float t_accelerated = t * 12;
                            float mod = (pi * (_delay + 1 + hold + 1));

                            // error: invalid operands of types 'float' and 'float' to binary 'operator%'
                            float phase = (float)((int)(t_accelerated * 100) % (int)(mod * 100)) * 0.01f;

                            // delay
                            if (phase < (pi * _delay))
                            {
                                if (reverse > 0)
                                    phase = pi;
                                else
                                    phase = 0;

                                program_leg0(phase, notify);
                                return;
                            }

                            phase -= (pi * _delay);


                            // move
                            if (phase < (pi))
                            {
                                if (reverse > 0)
                                    phase = pi - phase;

                                program_leg0(phase, notify);
                                return;
                            }

                            phase -= (pi);



                            // delay
                            if (phase < (pi * hold))
                            {
                                if (reverse > 0)
                                    phase = 0;
                                else
                                    phase = pi;

                                program_leg0(phase, notify);
                                return;
                            }

                            phase -= (pi * hold);

                            if (reverse > 0)
                                phase = pi - phase;

                            // commit
                            program_leg0((pi + phase), notify);

                        }
                        ;
                        #endregion





                        #region program_23_high_five_calibration_far
                        Action program_23_high_five_calibration_far =
                            delegate
                            {
                                leg1up_sideway_deg = sidewaysrange;
                                leg2up_sideway_deg = -sidewaysrange;
                                leg3up_sideway_deg = -sidewaysrange;
                                leg4up_sideway_deg = sidewaysrange;

                                leg1down_vertical_deg = verticalrange;
                                leg2down_vertical_deg = verticalrange;
                                leg3down_vertical_deg = verticalrange;
                                leg4down_vertical_deg = verticalrange;
                            };
                        #endregion

                        #region program_33_high_five_calibration
                        Action program_33_high_five_calibration =
                            delegate
                            {
                                leg1up_sideway_deg = -sidewaysrange;
                                leg2up_sideway_deg = sidewaysrange;
                                leg3up_sideway_deg = sidewaysrange;
                                leg4up_sideway_deg = -sidewaysrange;

                                leg1down_vertical_deg = verticalrange;
                                leg2down_vertical_deg = verticalrange;
                                leg3down_vertical_deg = verticalrange;
                                leg4down_vertical_deg = verticalrange;
                            };
                        #endregion

                        #region program_43_high_five_calibration_stand
                        Action program_43_high_five_calibration_stand =
                            delegate
                            {
                                leg1up_sideway_deg = -sidewaysrange;
                                leg2up_sideway_deg = sidewaysrange;
                                leg3up_sideway_deg = sidewaysrange;
                                leg4up_sideway_deg = -sidewaysrange;

                                leg1down_vertical_deg = 0;
                                leg2down_vertical_deg = 0;
                                leg3down_vertical_deg = 0;
                                leg4down_vertical_deg = 0;
                            };
                        #endregion



                        #region program_53_mayday
                        Action program_53_mayday =
                            delegate
                            {
                                leg1down_vertical_deg = verticalrange;
                                leg2down_vertical_deg = verticalrange;
                                leg3down_vertical_deg = verticalrange;
                                leg4down_vertical_deg = verticalrange;

                                leg1up_sideway_deg = -cos(t * 6) * sidewaysrange;
                                leg2up_sideway_deg = cos(t * 6) * sidewaysrange;
                                leg3up_sideway_deg = cos(t * 6) * sidewaysrange;
                                leg4up_sideway_deg = -cos(t * 6) * sidewaysrange;


                            };
                        #endregion


                        #region program_13_turn_left
                        Action program_13_turn_left =
                            /* void program_13_turn_left */ () =>
                            {
                                program_leg_delay_move_hold_commit(1, 2, 0,
                                    (deg_sideway, deg_vertical) =>
                                    {
                                        leg1up_sideway_deg = deg_sideway;
                                        leg1down_vertical_deg = deg_vertical;
                                    }
                                );

                                program_leg_delay_move_hold_commit(3, 0, 0,
                                    (deg_sideway, deg_vertical) =>
                                    {
                                        leg2up_sideway_deg = deg_sideway;
                                        leg2down_vertical_deg = deg_vertical;
                                    }
                                );

                                program_leg_delay_move_hold_commit(2, 1, 0,
                                     (deg_sideway, deg_vertical) =>
                                     {
                                         leg3up_sideway_deg = deg_sideway;
                                         leg3down_vertical_deg = deg_vertical;
                                     }
                                 );

                                program_leg_delay_move_hold_commit(0, 3, 0,
                                    (deg_sideway, deg_vertical) =>
                                    {
                                        leg4up_sideway_deg = deg_sideway;
                                        leg4down_vertical_deg = deg_vertical;
                                    }
                                );
                            }
                            ;
                        #endregion

                        #region program_14_turn_right
                        Action program_14_turn_right =
                            /* void program_13_turn_left */ () =>
                            {
                                program_leg_delay_move_hold_commit(1, 2, 1,
                                    (deg_sideway, deg_vertical) =>
                                    {
                                        leg1up_sideway_deg = deg_sideway;
                                        leg1down_vertical_deg = deg_vertical;
                                    }
                                );

                                program_leg_delay_move_hold_commit(3, 0, 1,
                                    (deg_sideway, deg_vertical) =>
                                    {
                                        leg2up_sideway_deg = deg_sideway;
                                        leg2down_vertical_deg = deg_vertical;
                                    }
                                );

                                program_leg_delay_move_hold_commit(2, 1, 1,
                                     (deg_sideway, deg_vertical) =>
                                     {
                                         leg3up_sideway_deg = deg_sideway;
                                         leg3down_vertical_deg = deg_vertical;
                                     }
                                 );

                                program_leg_delay_move_hold_commit(0, 3, 1,
                                    (deg_sideway, deg_vertical) =>
                                    {
                                        leg4up_sideway_deg = deg_sideway;
                                        leg4down_vertical_deg = deg_vertical;
                                    }
                                );
                            }
                            ;
                        #endregion

                        #region program_15_go_backwards
                        Action program_15_go_backwards =
                            /* void program_13_turn_left */ () =>
                            {
                                program_leg_delay_move_hold_commit(1, 2, 0,
                                    (deg_sideway, deg_vertical) =>
                                    {
                                        leg1up_sideway_deg = deg_sideway;
                                        leg1down_vertical_deg = deg_vertical;
                                    }
                                );

                                program_leg_delay_move_hold_commit(3, 0, 1,
                                    (deg_sideway, deg_vertical) =>
                                    {
                                        leg2up_sideway_deg = deg_sideway;
                                        leg2down_vertical_deg = deg_vertical;
                                    }
                                );

                                program_leg_delay_move_hold_commit(2, 1, 0,
                                     (deg_sideway, deg_vertical) =>
                                     {
                                         leg3up_sideway_deg = deg_sideway;
                                         leg3down_vertical_deg = deg_vertical;
                                     }
                                 );

                                program_leg_delay_move_hold_commit(0, 3, 1,
                                    (deg_sideway, deg_vertical) =>
                                    {
                                        leg4up_sideway_deg = deg_sideway;
                                        leg4down_vertical_deg = deg_vertical;
                                    }
                                );
                            }
                            ;
                        #endregion

                        #region program_16_go_forwards
                        Action program_16_go_forwards =
                            /* void program_13_turn_left */ () =>
                            {
                                program_leg_delay_move_hold_commit(1, 2, 1,
                                    (deg_sideway, deg_vertical) =>
                                    {
                                        leg1up_sideway_deg = deg_sideway;
                                        leg1down_vertical_deg = deg_vertical;
                                    }
                                );

                                program_leg_delay_move_hold_commit(3, 0, 0,
                                    (deg_sideway, deg_vertical) =>
                                    {
                                        leg2up_sideway_deg = deg_sideway;
                                        leg2down_vertical_deg = deg_vertical;
                                    }
                                );

                                program_leg_delay_move_hold_commit(2, 1, 1,
                                     (deg_sideway, deg_vertical) =>
                                     {
                                         leg3up_sideway_deg = deg_sideway;
                                         leg3down_vertical_deg = deg_vertical;
                                     }
                                 );

                                program_leg_delay_move_hold_commit(0, 3, 0,
                                    (deg_sideway, deg_vertical) =>
                                    {
                                        leg4up_sideway_deg = deg_sideway;
                                        leg4down_vertical_deg = deg_vertical;
                                    }
                                );
                            }
                            ;
                        #endregion

                        #region program_17_go_left
                        Action program_17_go_left =
                            /* void program_17_go_left */ () =>
                            {
                                program_leg_delay_move_hold_commit(1, 2, 1,
                                    (deg_sideway, deg_vertical) =>
                                    {
                                        leg1up_sideway_deg = deg_sideway;
                                        leg1down_vertical_deg = deg_vertical;
                                    }
                                );

                                program_leg_delay_move_hold_commit(3, 0, 1,
                                    (deg_sideway, deg_vertical) =>
                                    {
                                        leg2up_sideway_deg = deg_sideway;
                                        leg2down_vertical_deg = deg_vertical;
                                    }
                                );

                                program_leg_delay_move_hold_commit(2, 1, 0,
                                     (deg_sideway, deg_vertical) =>
                                     {
                                         leg3up_sideway_deg = deg_sideway;
                                         leg3down_vertical_deg = deg_vertical;
                                     }
                                 );

                                program_leg_delay_move_hold_commit(0, 3, 0,
                                    (deg_sideway, deg_vertical) =>
                                    {
                                        leg4up_sideway_deg = deg_sideway;
                                        leg4down_vertical_deg = deg_vertical;
                                    }
                                );
                            }
                            ;
                        #endregion

                        #region program_18_go_right
                        Action program_18_go_right =
                            /* void program_18_go_right */ () =>
                            {
                                program_leg_delay_move_hold_commit(1, 2, 0,
                                    (deg_sideway, deg_vertical) =>
                                    {
                                        leg1up_sideway_deg = deg_sideway;
                                        leg1down_vertical_deg = deg_vertical;
                                    }
                                );

                                program_leg_delay_move_hold_commit(3, 0, 0,
                                    (deg_sideway, deg_vertical) =>
                                    {
                                        leg2up_sideway_deg = deg_sideway;
                                        leg2down_vertical_deg = deg_vertical;
                                    }
                                );

                                program_leg_delay_move_hold_commit(2, 1, 1,
                                     (deg_sideway, deg_vertical) =>
                                     {
                                         leg3up_sideway_deg = deg_sideway;
                                         leg3down_vertical_deg = deg_vertical;
                                     }
                                 );

                                program_leg_delay_move_hold_commit(0, 3, 1,
                                    (deg_sideway, deg_vertical) =>
                                    {
                                        leg4up_sideway_deg = deg_sideway;
                                        leg4down_vertical_deg = deg_vertical;
                                    }
                                );
                            }
                            ;
                        #endregion

                        //();


                        if (pp == 23) program_23_high_five_calibration_far();
                        if (pp == 43) program_43_high_five_calibration_stand();
                        if (pp == 53) program_53_mayday();
                        if (pp == 13) program_13_turn_left();
                        if (pp == 14) program_14_turn_right();
                        if (pp == 15) program_15_go_backwards();
                        if (pp == 16) program_16_go_forwards();
                        if (pp == 17) program_17_go_left();
                        if (pp == 18) program_18_go_right();



                        #region legx
                        Action<Action, Action, f, f, f> legx =
                            (wirecolor, fillcolor, x, deg_sideway, deg_vertical) =>
                            {
                                mw(
                                    delegate
                                    {

                                        glMatrix.mat4.rotate(mvMatrix, degToRad(x), new[] { 0f, 0f, 1f });
                                        glMatrix.mat4.rotate(mvMatrix, degToRad(deg_sideway), new[] { 0f, 0f, 1f });
                                        glMatrix.mat4.rotate(mvMatrix, degToRad(deg_vertical), new[] { 1f, 0f, 0 });


                                        leg(wirecolor, fillcolor);
                                    }
                                );
                            };
                        #endregion

                        #region body
                        at(0, 0, 0.5f,
                              delegate
                              {
                                  green();

                                  draw(-1, 0, 0);
                                  draw(1, 0, 0);

                                  orange();

                                  draw(0, -1, 0);
                                  draw(0, 1, 0);
                              }
                          );
                        #endregion

                        #region left front - GREEN - leg2
                        legx(green, orange, 45, leg2up_sideway_deg, leg2down_vertical_deg);
                        #endregion


                        //legx_y = -2;

                        #region leg right back - BLUE - leg3
                        legx(cyan, green, 45 + 180, leg3up_sideway_deg, leg3down_vertical_deg);
                        #endregion

                        #region leg left back - WHITE - leg4
                        legx(white, green, -45 + 180, leg4up_sideway_deg, leg4down_vertical_deg);
                        #endregion


                        #region right front - RED - leg1
                        legx(red, orange, -45, leg1up_sideway_deg, leg1down_vertical_deg);
                        #endregion

                    }
                );

            };
            drawScene();
            #endregion

            #region IsDisposed
            var IsDisposed = false;

            Dispose = delegate
            {
                if (IsDisposed)
                    return;

                IsDisposed = true;

                canvas.Orphanize();
            };
            #endregion

            #region AtResize
            Action AtResize = delegate
            {
                canvas.style.SetLocation(0, 0, Native.window.Width, Native.window.Height);

                gl_viewportWidth = Native.window.Width;
                gl_viewportHeight = Native.window.Height;

                canvas.width = Native.window.Width;
                canvas.height = Native.window.Height;
            };

            AtResize();

            Native.window.onresize += delegate { AtResize(); };
            #endregion

            #region onmousewheel
            Native.Document.body.onmousewheel +=
                e =>
                {
                    camera_z += e.WheelDirection * 0.1f;
                };
            #endregion



            #region tick
            var c = 0;

            Native.window.onframe += delegate
            {
                if (IsDisposed)
                    return;

                c++;

                //Native.Document.title = "" + c;

                drawScene();
                animate();

                if (AtTick != null)
                    AtTick();


            };

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


        }

        /// <summary>
        /// Program Override
        /// </summary>
        public int po = 0;

        public int p = 0;


        public int pp
        {
            get
            {
                if (po == 0)
                    return p;
                return po;
            }
        }

        public f leg1down_vertical_deg = 0.0f;
        public f leg2down_vertical_deg = 0.0f;
        public f leg3down_vertical_deg = 0.0f;
        public f leg4down_vertical_deg = 0.0f;

        public f leg1up_sideway_deg = 0.0f;
        public f leg2up_sideway_deg = 0.0f;
        public f leg3up_sideway_deg = 0.0f;
        public f leg4up_sideway_deg = 0.0f;

        public Action Dispose;
    }
}
