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
using WebGLLesson10.Design;
using WebGLLesson10.HTML.Pages;

namespace WebGLLesson10
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;

    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application
    {
        // based on http://learningwebgl.com/lessons/lesson10/index.html

        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly DefaultStyle style = new DefaultStyle();

        public Action Dispose;


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page = null)
        {
            #region await __glMatrix then do InitializeContent
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


            style.Content.AttachToHead();
            //@"Hello world".ToDocumentTitle();
            //// Send data from JavaScript to the server tier
            //service.WebMethod2(
            //    @"A string from JavaScript.",
            //    value => value.ToDocumentTitle()
            //);
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

        

            #region init shaders


            var shaderProgram = gl.createProgram(
                new Shaders.GeometryVertexShader(),
                new Shaders.GeometryFragmentShader()
            );

            gl.linkProgram(shaderProgram);
            gl.useProgram(shaderProgram);

            #region getAttribLocation
            Func<string, long> getAttribLocation =
                    name => gl.getAttribLocation(shaderProgram, name);
            #endregion

            #region getUniformLocation
            Func<string, WebGLUniformLocation> getUniformLocation =
                name => gl.getUniformLocation(shaderProgram, name);
            #endregion

            #endregion



            var shaderProgram_vertexPositionAttribute = getAttribLocation("aVertexPosition");
            gl.enableVertexAttribArray((uint)shaderProgram_vertexPositionAttribute);

            var shaderProgram_textureCoordAttribute = getAttribLocation("aTextureCoord");
            gl.enableVertexAttribArray((uint)shaderProgram_textureCoordAttribute);

            var shaderProgram_pMatrixUniform = getUniformLocation("uPMatrix");
            var shaderProgram_mvMatrixUniform = getUniformLocation("uMVMatrix");
            var shaderProgram_samplerUniform = getUniformLocation("uSampler");





            var mvMatrix = __glMatrix.mat4.create();
            var mvMatrixStack = new Stack<Float32Array>();

            var pMatrix = __glMatrix.mat4.create();

            #region mvPushMatrix
            Action mvPushMatrix = delegate
            {
                var copy = __glMatrix.mat4.create();
                __glMatrix.mat4.set(mvMatrix, copy);
                mvMatrixStack.Push(copy);
            };
            #endregion

            #region mvPopMatrix
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

            #region degToRad
            Func<float, float> degToRad = (degrees) =>
            {
                return degrees * (f)Math.PI / 180f;
            };
            Func<float, float> radToDeg = (rad) =>
            {
                return rad * 180f / (f)Math.PI;
            };
            #endregion


            var pitch = 0f;
            var pitchRate = 0f;

            var yaw = 0f;
            var yawRate = 0f;

            var xPos = 0f;
            var yPos = 0.4f;
            var zPos = 0f;

            var speed = 0f;

            #region currentlyPressedKeys
            var currentlyPressedKeys = new Dictionary<int, bool>
            {
                {33, false},
                {34, false},
                {37, false},
                {39, false},
                {38, false},
                {40, false},
                {83, false},
                {87, false},
                {65, false},
                {68, false},
            };

            Native.Document.onkeydown +=
                e =>
                {
                    currentlyPressedKeys[e.KeyCode] = true;
                };

            Native.Document.onkeyup +=
               e =>
               {
                   currentlyPressedKeys[e.KeyCode] = false;
               };

            #endregion


            #region currentlyPressedKeysEitherOf
            Func<int, int, bool> currentlyPressedKeysEitherOf =
                (a, b) =>
                {
                    if (currentlyPressedKeys[a])
                        return true;

                    if (currentlyPressedKeys[b])
                        return true;

                    return false;
                };
            #endregion


            #region handleKeys
            Action handleKeys =
                delegate
                {
                    if (currentlyPressedKeys[33])
                    {
                        // Page Up
                        pitchRate = 0.1f;
                    }
                    else if (currentlyPressedKeys[34])
                    {
                        // Page Down
                        pitchRate = -0.1f;
                    }
                    else
                    {
                        pitchRate = 0;
                    }


                    if (currentlyPressedKeysEitherOf(37, 65))
                    {
                        // Left cursor key or A
                        yawRate = 0.1f;
                    }
                    else if (currentlyPressedKeysEitherOf(39, 68))
                    {
                        // Right cursor key or D
                        yawRate = -0.1f;
                    }
                    else
                    {
                        yawRate = 0;
                    }

                    if (currentlyPressedKeysEitherOf(38, 87))
                    {
                        // Up cursor key or W
                        speed = 0.003f;
                    }
                    else if (currentlyPressedKeysEitherOf(40, 83))
                    {
                        // Down cursor key
                        speed = -0.003f;
                    }
                    else
                    {
                        speed = 0;
                    }
                };
            #endregion

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
                    if (Native.Document.pointerLockElement == canvas)
                    {

                        __pointer_x += e.movementX;
                        __pointer_y += e.movementY;
                    }
                };

            canvas.onmouseup +=
                delegate
                {
                    // keep at it...
                    //Native.Document.exitPointerLock();
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


            new HTML.Images.FromAssets.mud().InvokeOnComplete(
                mud =>
                {
                    var mudTexture = gl.createTexture();

                    gl.pixelStorei(gl.UNPACK_FLIP_Y_WEBGL, 1);
                    gl.bindTexture(gl.TEXTURE_2D, mudTexture);
                    gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, mud);
                    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, (int)gl.LINEAR);
                    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, (int)gl.LINEAR);

                    gl.bindTexture(gl.TEXTURE_2D, null);




                    Func<string, f> parseFloat = x => (f)double.Parse(x);

                    var lines = data.Split('\n');
                    var vertexCount = 0;
                    var vertexPositions = new List<f>();
                    var vertexTextureCoords = new List<f>();
                    foreach (var i in lines)
                    {
                        var vals = i.Trim().Replace("   ", "  ").Replace("  ", " ").Split(' ');

                        if (vals.Length == 5)
                            if (vals[0] != "//")
                            {
                                // It is a line describing a vertex; get X, Y and Z first
                                vertexPositions.Add(parseFloat(vals[0]));
                                vertexPositions.Add(parseFloat(vals[1]));
                                vertexPositions.Add(parseFloat(vals[2]));

                                // And then the texture coords
                                vertexTextureCoords.Add(parseFloat(vals[3]));
                                vertexTextureCoords.Add(parseFloat(vals[4]));

                                vertexCount += 1;
                            }
                    }

                    var worldVertexPositionBuffer = gl.createBuffer();
                    gl.bindBuffer(gl.ARRAY_BUFFER, worldVertexPositionBuffer);
                    gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertexPositions.ToArray()), gl.STATIC_DRAW);
                    var worldVertexPositionBuffer_itemSize = 3;
                    var worldVertexPositionBuffer_numItems = vertexCount;

                    var worldVertexTextureCoordBuffer = gl.createBuffer();
                    gl.bindBuffer(gl.ARRAY_BUFFER, worldVertexTextureCoordBuffer);
                    gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertexTextureCoords.ToArray()), gl.STATIC_DRAW);
                    var worldVertexTextureCoordBuffer_itemSize = 2;
                    var worldVertexTextureCoordBuffer_numItems = vertexCount;





                    gl.clearColor(0.0f, 0.0f, 0.0f, 1.0f);
                    gl.enable(gl.DEPTH_TEST);




                    var lastTime = 0L;
                    // Used to make us "jog" up and down as we move forward.
                    var joggingAngle = 0f;

                    #region animate
                    Action animate = () =>
                    {
                        var timeNow = new IDate().getTime();
                        if (lastTime != 0)
                        {
                            var elapsed = timeNow - lastTime;


                            if (speed != 0)
                            {
                                xPos -= (f)Math.Sin(degToRad(yaw)) * speed * elapsed;
                                zPos -= (f)Math.Cos(degToRad(yaw)) * speed * elapsed;

                                joggingAngle += elapsed * 0.6f; // 0.6 "fiddle factor" - makes it feel more realistic :-)
                                yPos = (f)Math.Sin(degToRad(joggingAngle)) / 20 + 0.4f;
                            }
                            else
                            {
                                joggingAngle += elapsed * 0.06f; // 0.6 "fiddle factor" - makes it feel more realistic :-)
                                yPos = (f)Math.Sin(degToRad(joggingAngle)) / 200 + 0.4f;
                            }

                            yaw += yawRate * elapsed;
                            pitch += pitchRate * elapsed;

                        }
                        lastTime = timeNow;
                    };
                    #endregion


                    #region drawScene
                    Action drawScene = () =>
                    {
                        gl.viewport(0, 0, gl_viewportWidth, gl_viewportHeight);
                        gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);



                        __glMatrix.mat4.perspective(45, gl_viewportWidth / gl_viewportHeight, 0.1f, 100.0f, pMatrix);

                        __glMatrix.mat4.identity(mvMatrix);

                        if (__pointer_y != 0)
                            pitch = radToDeg(__pointer_y * -0.01f);

                        if (__pointer_x != 0)
                            yaw = radToDeg(__pointer_x * -0.01f);


                        __glMatrix.mat4.rotate(mvMatrix, degToRad(-pitch), 1, 0, 0);
                        __glMatrix.mat4.rotate(mvMatrix, degToRad(-yaw), 0, 1, 0);

                        //__glMatrix.mat4.rotate(mvMatrix, __pointer_y * 0.01f, 1, 0, 0);
                        //__glMatrix.mat4.rotate(mvMatrix, __pointer_x * 0.01f, 0, 1, 0);


                        __glMatrix.mat4.translate(mvMatrix, -xPos, -yPos, -zPos);

                        gl.activeTexture(gl.TEXTURE0);
                        gl.bindTexture(gl.TEXTURE_2D, mudTexture);
                        gl.uniform1i(shaderProgram_samplerUniform, 0);

                        gl.bindBuffer(gl.ARRAY_BUFFER, worldVertexTextureCoordBuffer);
                        gl.vertexAttribPointer((uint)shaderProgram_textureCoordAttribute, worldVertexTextureCoordBuffer_itemSize, gl.FLOAT, false, 0, 0);

                        gl.bindBuffer(gl.ARRAY_BUFFER, worldVertexPositionBuffer);
                        gl.vertexAttribPointer((uint)shaderProgram_vertexPositionAttribute, worldVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);

                        setMatrixUniforms();
                        gl.drawArrays(gl.TRIANGLES, 0, worldVertexPositionBuffer_numItems);
                    };
                    #endregion


                    #region tick
                    Action tick = null;

                    tick = () =>
                    {
                        if (IsDisposed)
                            return;

                        handleKeys();
                        drawScene();
                        animate();

                        Native.Window.requestAnimationFrame += tick;
                    };

                    tick();
                    #endregion


                }
            );
        }

        public static readonly string data = @"
 NUMPOLLIES 36

  // Floor 1
  -3.0  0.0 -3.0 0.0 6.0
  -3.0  0.0  3.0 0.0 0.0
  3.0  0.0  3.0 6.0 0.0

  -3.0  0.0 -3.0 0.0 6.0
  3.0  0.0 -3.0 6.0 6.0
  3.0  0.0  3.0 6.0 0.0

  // Ceiling 1
  -3.0  1.0 -3.0 0.0 6.0
  -3.0  1.0  3.0 0.0 0.0
  3.0  1.0  3.0 6.0 0.0
  -3.0  1.0 -3.0 0.0 6.0
  3.0  1.0 -3.0 6.0 6.0
  3.0  1.0  3.0 6.0 0.0

  // A1

  -2.0  1.0  -2.0 0.0 1.0
  -2.0  0.0  -2.0 0.0 0.0
  -0.5  0.0  -2.0 1.5 0.0
  -2.0  1.0  -2.0 0.0 1.0
  -0.5  1.0  -2.0 1.5 1.0
  -0.5  0.0  -2.0 1.5 0.0

  // A2

  2.0  1.0  -2.0 2.0 1.0
  2.0  0.0  -2.0 2.0 0.0
  0.5  0.0  -2.0 0.5 0.0
  2.0  1.0  -2.0 2.0 1.0
  0.5  1.0  -2.0 0.5 1.0
  0.5  0.0  -2.0 0.5 0.0

  // B1

  -2.0  1.0  2.0 2.0  1.0
  -2.0  0.0   2.0 2.0 0.0
  -0.5  0.0   2.0 0.5 0.0
  -2.0  1.0  2.0 2.0  1.0
  -0.5  1.0  2.0 0.5  1.0
  -0.5  0.0   2.0 0.5 0.0

  // B2

  2.0  1.0  2.0 2.0  1.0
  2.0  0.0   2.0 2.0 0.0
  0.5  0.0   2.0 0.5 0.0
  2.0  1.0  2.0 2.0  1.0
  0.5  1.0  2.0 0.5  1.0
  0.5  0.0   2.0 0.5 0.0

  // C1

  -2.0  1.0  -2.0 0.0  1.0
  -2.0  0.0   -2.0 0.0 0.0
  -2.0  0.0   -0.5 1.5 0.0
  -2.0  1.0  -2.0 0.0  1.0
  -2.0  1.0  -0.5 1.5  1.0
  -2.0  0.0   -0.5 1.5 0.0

  // C2

  -2.0  1.0   2.0 2.0 1.0
  -2.0  0.0   2.0 2.0 0.0
  -2.0  0.0   0.5 0.5 0.0
  -2.0  1.0  2.0 2.0 1.0
  -2.0  1.0  0.5 0.5 1.0
  -2.0  0.0   0.5 0.5 0.0

  // D1

  2.0  1.0  -2.0 0.0 1.0
  2.0  0.0   -2.0 0.0 0.0
  2.0  0.0   -0.5 1.5 0.0
  2.0  1.0  -2.0 0.0 1.0
  2.0  1.0  -0.5 1.5 1.0
  2.0  0.0   -0.5 1.5 0.0

  // D2

  2.0  1.0  2.0 2.0 1.0
  2.0  0.0   2.0 2.0 0.0
  2.0  0.0   0.5 0.5 0.0
  2.0  1.0  2.0 2.0 1.0
  2.0  1.0  0.5 0.5 1.0
  2.0  0.0   0.5 0.5 0.0

  // Upper hallway - L
  -0.5  1.0  -3.0 0.0 1.0
  -0.5  0.0   -3.0 0.0 0.0
  -0.5  0.0   -2.0 1.0 0.0
  -0.5  1.0  -3.0 0.0 1.0
  -0.5  1.0  -2.0 1.0 1.0
  -0.5  0.0   -2.0 1.0 0.0

  // Upper hallway - R
  0.5  1.0  -3.0 0.0 1.0
  0.5  0.0   -3.0 0.0 0.0
  0.5  0.0   -2.0 1.0 0.0
  0.5  1.0  -3.0 0.0 1.0
  0.5  1.0  -2.0 1.0 1.0
  0.5  0.0   -2.0 1.0 0.0

  // Lower hallway - L
  -0.5  1.0  3.0 0.0 1.0
  -0.5  0.0   3.0 0.0 0.0
  -0.5  0.0   2.0 1.0 0.0
  -0.5  1.0  3.0 0.0 1.0
  -0.5  1.0  2.0 1.0 1.0
  -0.5  0.0   2.0 1.0 0.0

  // Lower hallway - R
  0.5  1.0  3.0 0.0 1.0
  0.5  0.0   3.0 0.0 0.0
  0.5  0.0   2.0 1.0 0.0
  0.5  1.0  3.0 0.0 1.0
  0.5  1.0  2.0 1.0 1.0
  0.5  0.0   2.0 1.0 0.0


  // Left hallway - Lw

  -3.0  1.0  0.5 1.0 1.0
  -3.0  0.0   0.5 1.0 0.0
  -2.0  0.0   0.5 0.0 0.0
  -3.0  1.0  0.5 1.0 1.0
  -2.0  1.0  0.5 0.0 1.0
  -2.0  0.0   0.5 0.0 0.0

  // Left hallway - Hi

  -3.0  1.0  -0.5 1.0 1.0
  -3.0  0.0   -0.5 1.0 0.0
  -2.0  0.0   -0.5 0.0 0.0
  -3.0  1.0  -0.5 1.0 1.0
  -2.0  1.0  -0.5 0.0 1.0
  -2.0  0.0   -0.5 0.0 0.0

  // Right hallway - Lw

  3.0  1.0  0.5 1.0 1.0
  3.0  0.0   0.5 1.0 0.0
  2.0  0.0   0.5 0.0 0.0
  3.0  1.0  0.5 1.0 1.0
  2.0  1.0  0.5 0.0 1.0
  2.0  0.0   0.5 0.0 0.0

  // Right hallway - Hi

  3.0  1.0  -0.5 1.0 1.0
  3.0  0.0   -0.5 1.0 0.0
  2.0  0.0   -0.5 0.0 0.0
  3.0  1.0  -0.5 1.0 1.0
  2.0  1.0 -0.5 0.0 1.0
  2.0  0.0   -0.5 0.0 0.0

";
    }


}
