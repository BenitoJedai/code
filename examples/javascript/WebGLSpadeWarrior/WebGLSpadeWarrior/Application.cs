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
using WebGLSpadeWarrior.HTML.Pages;
using WebGLSpadeWarrior.Design;
using WebGLSpadeWarrior.Shaders;

namespace WebGLSpadeWarrior
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
            //page.PageContainer.style.color = Color.Blue;

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



            var mvMatrix = __glMatrix.mat4.create();
            var mvMatrixStack = new Stack<Float32Array>();

            var pMatrix = __glMatrix.mat4.create();


            #region mvMatrixScope
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

            Action<Action> mvMatrixScope =
                h =>
                {
                    mvPushMatrix();
                    h();
                    mvPopMatrix();
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


            var cubeVertexColorBuffer_orange = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer_orange);
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(colors_orange), gl.STATIC_DRAW);
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

            #region colors3
            var colors_green = new[]{
                0.0f, 1.0f, 0.0f, 1.0f, // Front face
                0.0f, 1.0f, 0.0f, 1.0f, // Front face
                0.0f, 1.0f, 0.0f, 1.0f, // Front face
                0.0f, 1.0f, 0.0f, 1.0f, // Front face

                0.0f, 0.4f, 0.0f, 1.0f, // Back face
                0.0f, 0.4f, 0.0f, 1.0f, // Back face
                0.0f, 0.4f, 0.0f, 1.0f, // Back face
                0.0f, 0.4f, 0.0f, 1.0f, // Back face

                0.0f, 0.5f, 0.0f, 1.0f, // Top face
                0.0f, 0.5f, 0.0f, 1.0f, // Top face
                0.0f, 0.5f, 0.0f, 1.0f, // Top face
                0.0f, 0.5f, 0.0f, 1.0f, // Top face

                0.0f, 0.7f, 0.0f, 1.0f, // Bottom face
                0.0f, 0.7f, 0.0f, 1.0f, // Bottom face
                0.0f, 0.7f, 0.0f, 1.0f, // Bottom face
                0.0f, 0.7f, 0.0f, 1.0f, // Bottom face

                
                0.0f, 0.8f, 0.0f, 1.0f, // Right face
                0.0f, 0.8f, 0.0f, 1.0f, // Right face
                0.0f, 0.8f, 0.0f, 1.0f, // Right face
                0.0f, 0.8f, 0.0f, 1.0f, // Right face

                0.0f, 0.9f, 0.0f, 1.0f,  // Left face
                0.0f, 0.9f, 0.0f, 1.0f,  // Left face
                0.0f, 0.9f, 0.0f, 1.0f,  // Left face
                0.0f, 0.9f, 0.0f, 1.0f  // Left face
            };


            var cubeVertexColorBuffer_green = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer_green);
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(colors_green), gl.STATIC_DRAW);
            #endregion

            #region colors_black
            var colors_black = new[]{
                0.0f, 0.0f, 0.0f, 1.0f, // Front face
                0.0f, 0.0f, 0.0f, 1.0f, // Front face
                0.0f, 0.0f, 0.0f, 1.0f, // Front face
                0.0f, 0.0f, 0.0f, 1.0f, // Front face
                0.0f, 0.0f, 0.0f, 1.0f, // Back face
                0.0f, 0.0f, 0.0f, 1.0f, // Back face
                0.0f, 0.0f, 0.0f, 1.0f, // Back face
                0.0f, 0.0f, 0.0f, 1.0f, // Back face
                0.0f, 0.0f, 0.0f, 1.0f, // Top face
                0.0f, 0.0f, 0.0f, 1.0f, // Top face
                0.0f, 0.0f, 0.0f, 1.0f, // Top face
                0.0f, 0.0f, 0.0f, 1.0f, // Top face
                0.0f, 0.0f, 0.0f, 1.0f, // Bottom face
                0.0f, 0.0f, 0.0f, 1.0f, // Bottom face
                0.0f, 0.0f, 0.0f, 1.0f, // Bottom face
                0.0f, 0.0f, 0.0f, 1.0f, // Bottom face
                0.0f, 0.0f, 0.0f, 1.0f, // Right face
                0.0f, 0.0f, 0.0f, 1.0f, // Right face
                0.0f, 0.0f, 0.0f, 1.0f, // Right face
                0.0f, 0.0f, 0.0f, 1.0f, // Right face
                0.0f, 0.0f, 0.0f, 1.0f,  // Left face
                0.0f, 0.0f, 0.0f, 1.0f,  // Left face
                0.0f, 0.0f, 0.0f, 1.0f,  // Left face
                0.0f, 0.0f, 0.0f, 1.0f  // Left face
            };


            var cubeVertexColorBuffer_black = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer_black);
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(colors_black), gl.STATIC_DRAW);
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

            var IsWalking = false;
            var WalkMultiplier = 0.0f;


            var ego_x = 0f;
            var ego_y = 0f;
            var ego_z = 0f;
            var ego_za = 0f;

            var rCube = 0f;
            var raCube = 0f;

            var lastTime = 0L;
            Action animate = delegate
            {
                var timeNow = new IDate().getTime();
                if (lastTime != 0)
                {
                    var elapsed = timeNow - lastTime;

                    var u = ego_z + ego_za * (elapsed) / 1000.0f;

                    // script: error JSC1000: No implementation found for this native method, please implement [static System.Math.Min(System.Single, System.Single)]

                    if (u < 0)
                        ego_z = (float)Math.Min(ego_z, 0);
                    else
                        ego_z = u;


                    ego_za -= 3.2f * (elapsed) / 1000.0f;


                    raCube += (75 * elapsed) / 1000.0f;
                }
                lastTime = timeNow;
            };

            Func<float, float> degToRad = (degrees) =>
            {
                return degrees * (f)Math.PI / 180f;
            };


            var c = 0;

            #region drawScene
            Action drawScene = delegate
            {
                if (ego_x < 0)
                    ego_x = 0;

                if (ego_y > 0)
                    ego_y = 0;

                gl.viewport(0, 0, gl_viewportWidth, gl_viewportHeight);
                gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);


                __glMatrix.mat4.perspective(45f, (float)gl_viewportWidth / (float)gl_viewportHeight, 0.1f, 100.0f, pMatrix);

                __glMatrix.mat4.identity(mvMatrix);




                #region vertex
                gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
                gl.vertexAttribPointer((uint)shaderProgram_vertexPositionAttribute, cubeVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);
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
                        __glMatrix.mat4.rotate(mvMatrix, degToRad(-80), new float[] { 1f, 0f, 0f });


                        #region grid

                        #region color
                        gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer_green);
                        gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);
                        #endregion



                        var GridZoom = 0.3f;

                        Action<float> WriteYLine =
                            x =>
                            {
                                for (int i = -12; i < -1; i++)
                                {
                                    OriginalCubeAt(x * GridZoom, (i) * GridZoom, 0);


                                }
                            };

                        for (int i = 1; i < 12; i++)
                            WriteYLine(i);


                        #region color
                        gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer_orange);
                        gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);
                        #endregion

                        for (int i = 1; i < 12; i++)
                            OriginalCubeAt(i * GridZoom, 0, 0);

                        for (int i = -12; i < 0; i++)
                            OriginalCubeAt(0, (i) * GridZoom, 0);


                        #endregion

                        #region color
                        gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer2);
                        gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);
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


                              #region cube
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
                              #endregion cube

                              #region draw

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
                              #endregion draw

                              #region leg
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
                                      gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer_orange);
                                      gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);
                                      #endregion


                                      #region lower leg
                                      rect(3, 5, 0);
                                      rect(3, 5, 1);
                                      rect(3, 5, 2);
                                      rect(3, 3, 3);
                                      #endregion



                                      #region color
                                      gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer2);
                                      gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);
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

                              var seed = (raCube * WalkMultiplier * 16);

                              #region animated_leg
                              Action<int, int> animated_leg = (seed_offset, x) =>
                              {
                                  var seed_180 = (float)((seed + seed_offset) % 180f);

                                  var left_hip = 30f;
                                  var left_knee = 0f;


                                  if (ego_z < 0)
                                  {
                                      // crouch
                                      left_hip = -66;
                                      left_knee = left_hip * -2;
                                  }
                                  else if (ego_z > 0)
                                  {
                                      // crouch
                                      left_hip = 10;
                                      left_knee = 10;
                                  }
                                  else if (!IsWalking)
                                  {
                                      left_hip = 0;
                                      left_knee = 0;
                                  }
                                  else
                                  {
                                      left_hip = seed_180;

                                      if (left_hip > 90)
                                      {
                                          left_hip = 180 - left_hip;
                                          // -60 should be 0 -  front
                                          // 0 should be 60
                                          // 30 should be 0 - back


                                          var v = (90 - left_hip) - 70;



                                          if (v < 0)
                                              left_knee = 70 + v;
                                          else if (v == 0)
                                              left_knee = 70;
                                          else if (v > 0)
                                              left_knee = (20 - v) * (70 / 20);



                                          //page.Data1.innerText = "" + new { left_hip, v, left_knee };
                                      }
                                      else
                                      {
                                      }

                                      left_hip -= 45;
                                  }

                                  leg(x, left_hip, left_knee);
                              };
                              #endregion

                              animated_leg(0, -2);
                              animated_leg(90, 3);





                              #region body
                              mvMatrixScope(
                                  delegate
                                  {
                                      __glMatrix.mat4.translate(mvMatrix, new float[] { 
                                        2 * cubesize * 1, 
                                        2 * cubesize * -2, 
                                        2 * cubesize  * 11});

                                      rect(8, 4, 0);
                                      rect(8, 4, 1);
                                      rect(8, 4, 2);
                                      rect(8, 4, 3);
                                      rect(8, 4, 4);
                                      rect(8, 4, 5);
                                      rect(8, 4, 6);
                                      rect(8, 4, 7);
                                      rect(8, 4, 8);

                                      mvMatrixScope(
                                         delegate
                                         {
                                             __glMatrix.mat4.translate(mvMatrix, new float[] { 
                                                2 * cubesize * 0, 
                                                2 * cubesize * -2, 
                                                2 * cubesize  * 7});

                                             rect(2, 10, 0);
                                             rect(2, 10, 1);
                                         }
                                     );


                                      mvMatrixScope(
                                          delegate
                                          {
                                              #region color
                                              gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer_orange);
                                              gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);
                                              #endregion

                                              __glMatrix.mat4.translate(mvMatrix, new float[] { 
                                                    2 * cubesize * 10, 
                                                    2 * cubesize * -2, 
                                                    2 * cubesize  * 7});

                                              rect(2, 2, 0);
                                              rect(2, 2, 1);

                                              #region color
                                              gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer2);
                                              gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);
                                              #endregion
                                          }
                                      );

                                      mvMatrixScope(
                                          delegate
                                          {
                                              #region color
                                              gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer_orange);
                                              gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);
                                              #endregion

                                              __glMatrix.mat4.translate(mvMatrix, new float[] { 
                                                    2 * cubesize * 10, 
                                                    2 * cubesize * 8, 
                                                    2 * cubesize  * 7});

                                              rect(2, 2, 0);
                                              rect(2, 2, 1);

                                              #region color
                                              gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer2);
                                              gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);
                                              #endregion
                                          }
                                      );

                                      mvMatrixScope(
                                      delegate
                                      {

                                          __glMatrix.mat4.translate(mvMatrix, new float[] { 
                                                2 * cubesize * 0, 
                                                2 * cubesize * 8, 
                                                2 * cubesize  * 7});

                                          rect(2, 10, 0);
                                          rect(2, 10, 1);
                                      }
                                    );



                                  }
                              );
                              #endregion

                              #region head


                              mvMatrixScope(
                                  delegate
                                  {
                                      __glMatrix.mat4.translate(mvMatrix, new float[] { 
                                        2 * cubesize * 0, 
                                        2 * cubesize * -1, 
                                        2 * cubesize  * 20});

                                      #region color
                                      gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer_black);
                                      gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);
                                      #endregion


                                      cube(5, 4, 2);
                                      cube(5, 1, 2);

                                      cube(3, 0, 0);
                                      cube(3, 0, 1);
                                      cube(3, 0, 2);
                                      cube(3, 5, 0);
                                      cube(3, 5, 1);
                                      cube(3, 5, 2);

                                      #region color
                                      gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer_orange);
                                      gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);
                                      #endregion




                                      rect(6, 3, 0);
                                      rect(6, 3, 1);

                                      cube(4, 0, 2);
                                      cube(5, 0, 2);
                                      cube(5, 2, 2);
                                      cube(5, 3, 2);
                                      cube(5, 5, 2);
                                      cube(4, 5, 2);

                                      cube(4, 0, 1);
                                      cube(5, 0, 1);
                                      cube(5, 1, 1);
                                      cube(5, 2, 1);
                                      cube(5, 3, 1);
                                      cube(5, 4, 1);
                                      cube(5, 5, 1);
                                      cube(4, 5, 1);

                                      cube(4, 0, 0);
                                      cube(5, 0, 0);
                                      cube(5, 1, 0);
                                      cube(5, 2, 0);
                                      cube(5, 3, 0);
                                      cube(5, 4, 0);
                                      cube(5, 5, 0);
                                      cube(4, 5, 0);

                                      //rect(6, 2, 2);

                                      #region color
                                      gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer2);
                                      gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);
                                      #endregion


                                      // 3 or 3?? :)
                                      rect(6, 3, 2);

                                      rect(6, 6, 3);
                                      rect(6, 6, 4);
                                      rect(6, 6, 5);


                                  }
                              );
                              #endregion



                          }
                      );
                    }
                );

            };
            drawScene();
            #endregion






            #region tick - new in lesson 03
            var tick = default(Action);

            tick = delegate
            {
                c++;


                Native.Document.title = "" + c + " " + (rCube) + " " + ego_z + " " + ego_za;

                drawScene();
                animate();

                Native.Window.requestAnimationFrame += tick;
            };

            tick();
            #endregion


            #region onkeyup
            Native.Document.onkeyup +=
                (e) =>
                {
                    //Native.Document.title = "" + new { e.KeyCode };


                    if (e.KeyCode == 17)
                    {
                        ego_z = 0;
                    }

                    if (e.KeyCode == 32)
                    {
                        ego_z = 0;
                        ego_za = 2;
                    }

                    if (e.KeyCode == 38)
                    {
                        IsWalking = false;
                    }



                    if (e.KeyCode == 40)
                    {
                        IsWalking = false;
                    }
                };
            #endregion

            #region onkeydown
            Native.Document.onkeydown +=
                (e) =>
                {
                    // see also: http://www.cambiaresearch.com/articles/15/javascript-char-codes-key-codes

                    e.PreventDefault();

                    #region turnspeed
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
                    #endregion

                    if (e.KeyCode == 17)
                    {
                        ego_z = -cubesize * 6;
                    }

                    if (e.KeyCode == 32)
                    {
                        ego_z = -cubesize * 6;
                    }

                    if (ego_z > 0)
                    {
                        // not on ground. cant walk :)
                    }
                    else
                    {
                        if (e.KeyCode == 38)
                        {
                            IsWalking = true;

                            // mat aint working ..


                            if (!e.shiftKey)
                            {
                                WalkMultiplier = 0.04f;
                            }
                            else
                            {
                                WalkMultiplier = 0.02f;



                            }

                            ego_y += (float)Math.Sin(rCube) * WalkMultiplier;
                            ego_x += (float)Math.Cos(rCube) * WalkMultiplier;
                        }



                        if (e.KeyCode == 40)
                        {
                            IsWalking = true;
                            WalkMultiplier = 0.02f;

                            ego_y -= (float)Math.Sin(rCube) * WalkMultiplier;
                            ego_x -= (float)Math.Cos(rCube) * WalkMultiplier;

                        }

                    }
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

        }

        public bool IsDisposed;
    }


}
