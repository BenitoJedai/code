using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.WebGL;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using WebGLLesson11.HTML.Pages;
using WebGLLesson11.Styles;
using WebGLLesson11.Design;

namespace WebGLLesson11
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;
    using System.Collections.Generic;

    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application
    {
        // based on http://learningwebgl.com/lessons/lesson11/index.html

        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly DefaultStyle style = new DefaultStyle();

        public Action Dispose;


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
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

            if (page != null)
                page.Toolbar.Orphanize().AttachToDocument();

            if (page == null)
                page = new DefaultPage();

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

                    return null;
                }

                return shader;
            };
            #endregion


            #region init shaders
            var vs = createShader(new Shaders.GeometryVertexShader());
            var fs = createShader(new Shaders.GeometryFragmentShader());

            if (vs == null || fs == null) throw new InvalidOperationException("shader failed");

            var shaderProgram = gl.createProgram();

            gl.attachShader(shaderProgram, vs);
            gl.attachShader(shaderProgram, fs);


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
            gl.enableVertexAttribArray((ulong)shaderProgram_vertexPositionAttribute);

            var shaderProgram_textureCoordAttribute = getAttribLocation("aTextureCoord");
            gl.enableVertexAttribArray((ulong)shaderProgram_textureCoordAttribute);

            var shaderProgram_vertexNormalAttribute = getAttribLocation("aVertexNormal");
            gl.enableVertexAttribArray((ulong)shaderProgram_vertexNormalAttribute);

            var shaderProgram_pMatrixUniform = gl.getUniformLocation(shaderProgram, "uPMatrix");
            var shaderProgram_mvMatrixUniform = gl.getUniformLocation(shaderProgram, "uMVMatrix");
            var shaderProgram_nMatrixUniform = gl.getUniformLocation(shaderProgram, "uNMatrix");
            var shaderProgram_samplerUniform = gl.getUniformLocation(shaderProgram, "uSampler");
            var shaderProgram_useLightingUniform = gl.getUniformLocation(shaderProgram, "uUseLighting");
            var shaderProgram_ambientColorUniform = gl.getUniformLocation(shaderProgram, "uAmbientColor");
            var shaderProgram_lightingDirectionUniform = gl.getUniformLocation(shaderProgram, "uLightingDirection");
            var shaderProgram_directionalColorUniform = gl.getUniformLocation(shaderProgram, "uDirectionalColor");





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

                    var normalMatrix = __glMatrix.mat3.create();
                    __glMatrix.mat4.toInverseMat3(mvMatrix, normalMatrix);
                    __glMatrix.mat3.transpose(normalMatrix);
                    gl.uniformMatrix3fv(shaderProgram_nMatrixUniform, false, normalMatrix);
                };
            #endregion

            #region degToRad
            Func<float, float> degToRad = (degrees) =>
            {
                return degrees * (f)Math.PI / 180f;
            };
            #endregion


            #region mouseDown
            var mouseDown = false;
            var lastMouseX = 0L;
            var lastMouseY = 0L;

            var moonRotationMatrix = __glMatrix.mat4.create();
            __glMatrix.mat4.identity(moonRotationMatrix);

            canvas.onmousedown +=
                e =>
                {
                    mouseDown = true;
                    lastMouseX = e.CursorX;
                    lastMouseY = e.CursorY;
                };

            Native.Document.onmouseup +=
                e =>
                {
                    mouseDown = false;

                };

            Native.Document.onmousemove +=
                e =>
                {
                    if (!mouseDown)
                    {
                        return;
                    }
                    var newX = e.CursorX;
                    var newY = e.CursorY;

                    var deltaX = newX - lastMouseX;
                    var newRotationMatrix = __glMatrix.mat4.create();
                    __glMatrix.mat4.identity(newRotationMatrix);
                    __glMatrix.mat4.rotate(newRotationMatrix, degToRad(deltaX / 10), 0, 1, 0);

                    var deltaY = newY - lastMouseY;
                    __glMatrix.mat4.rotate(newRotationMatrix, degToRad(deltaY / 10), 1, 0, 0);

                    __glMatrix.mat4.multiply(newRotationMatrix, moonRotationMatrix, moonRotationMatrix);

                    lastMouseX = newX;
                    lastMouseY = newY;
                };
            #endregion








            var latitudeBands = 30;
            var longitudeBands = 30;
            var radius = 2;

            var vertexPositionData = new List<f>();
            var normalData = new List<f>();
            var textureCoordData = new List<f>();
            for (var latNumber = 0; latNumber <= latitudeBands; latNumber++)
            {
                var theta = latNumber * Math.PI / latitudeBands;
                var sinTheta = (f)Math.Sin(theta);
                var cosTheta = (f)Math.Cos(theta);

                for (var longNumber = 0; longNumber <= longitudeBands; longNumber++)
                {
                    var phi = longNumber * 2 * Math.PI / longitudeBands;
                    var sinPhi = (f)Math.Sin(phi);
                    var cosPhi = (f)Math.Cos(phi);

                    var x = cosPhi * sinTheta;
                    var y = cosTheta;
                    var z = sinPhi * sinTheta;
                    var u = 1 - (longNumber / longitudeBands);
                    var v = 1 - (latNumber / latitudeBands);

                    normalData.Add(x);
                    normalData.Add(y);
                    normalData.Add(z);
                    textureCoordData.Add(u);
                    textureCoordData.Add(v);
                    vertexPositionData.Add(radius * x);
                    vertexPositionData.Add(radius * y);
                    vertexPositionData.Add(radius * z);
                }
            }

            var indexData = new List<ushort>();
            for (var latNumber = 0; latNumber < latitudeBands; latNumber++)
            {
                for (var longNumber = 0; longNumber < longitudeBands; longNumber++)
                {
                    var first = (latNumber * (longitudeBands + 1)) + longNumber;
                    var second = first + longitudeBands + 1;
                    indexData.Add((ushort)first);
                    indexData.Add((ushort)second);
                    indexData.Add((ushort)(first + 1));

                    indexData.Add((ushort)second);
                    indexData.Add((ushort)(second + 1));
                    indexData.Add((ushort)(first + 1));
                }
            }

            var moonVertexNormalBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, moonVertexNormalBuffer);
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(normalData.ToArray()), gl.STATIC_DRAW);
            var moonVertexNormalBuffer_itemSize = 3;
            var moonVertexNormalBuffer_numItems = normalData.Count / 3;

            var moonVertexTextureCoordBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, moonVertexTextureCoordBuffer);
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(textureCoordData.ToArray()), gl.STATIC_DRAW);
            var moonVertexTextureCoordBuffer_itemSize = 2;
            var moonVertexTextureCoordBuffer_numItems = textureCoordData.Count / 2;

            var moonVertexPositionBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, moonVertexPositionBuffer);
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertexPositionData.ToArray()), gl.STATIC_DRAW);
            var moonVertexPositionBuffer_itemSize = 3;
            var moonVertexPositionBuffer_numItems = vertexPositionData.Count / 3;

            var moonVertexIndexBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, moonVertexIndexBuffer);
            gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(indexData.ToArray()), gl.STATIC_DRAW);
            var moonVertexIndexBuffer_itemSize = 1;
            var moonVertexIndexBuffer_numItems = indexData.Count;




            new HTML.Images.FromAssets.moon().InvokeOnComplete(
                mud =>
                {
                    var moonTexture = gl.createTexture();

                    gl.pixelStorei(gl.UNPACK_FLIP_Y_WEBGL, 1);
                    gl.bindTexture(gl.TEXTURE_2D, moonTexture);
                    gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, mud);
                    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, (long)gl.LINEAR);
                    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, (long)gl.LINEAR_MIPMAP_NEAREST);
                    gl.generateMipmap(gl.TEXTURE_2D);

                    gl.bindTexture(gl.TEXTURE_2D, null);




                    Func<string, f> parseFloat = x => (f)double.Parse(x);



                    gl.clearColor(0.0f, 0.0f, 0.0f, 1.0f);
                    gl.enable(gl.DEPTH_TEST);





                    Action drawScene = () =>
                    {
                        gl.viewport(0, 0, gl_viewportWidth, gl_viewportHeight);
                        gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

                        __glMatrix.mat4.perspective(45, gl_viewportWidth / gl_viewportHeight, 0.1f, 100.0f, pMatrix);

                        var lighting = page.lighting.@checked;
                        gl.uniform1i(shaderProgram_useLightingUniform, Convert.ToInt32(lighting));
                        if (lighting)
                        {
                            gl.uniform3f(
                                shaderProgram_ambientColorUniform,
                                parseFloat(page.ambientR.value),
                                parseFloat(page.ambientG.value),
                                parseFloat(page.ambientB.value)
                            );

                            var lightingDirection = new[]{
                                    parseFloat(page.lightDirectionX.value),
                                    parseFloat(page.lightDirectionY.value),
                                    parseFloat(page.lightDirectionZ.value)
                                };

                            var adjustedLD = __glMatrix.vec3.create();
                            __glMatrix.vec3.normalize(lightingDirection, adjustedLD);
                            __glMatrix.vec3.scale(adjustedLD, -1);
                            gl.uniform3fv(shaderProgram_lightingDirectionUniform, adjustedLD);

                            gl.uniform3f(
                                shaderProgram_directionalColorUniform,
                                parseFloat(page.directionalR.value),
                                parseFloat(page.directionalG.value),
                                parseFloat(page.directionalB.value)
                            );
                        }

                        __glMatrix.mat4.identity(mvMatrix);

                        __glMatrix.mat4.translate(mvMatrix, 0, 0, -6);

                        __glMatrix.mat4.multiply(mvMatrix, moonRotationMatrix);

                        gl.activeTexture(gl.TEXTURE0);
                        gl.bindTexture(gl.TEXTURE_2D, moonTexture);
                        gl.uniform1i(shaderProgram_samplerUniform, 0);

                        gl.bindBuffer(gl.ARRAY_BUFFER, moonVertexPositionBuffer);
                        gl.vertexAttribPointer((ulong)shaderProgram_vertexPositionAttribute, moonVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);

                        gl.bindBuffer(gl.ARRAY_BUFFER, moonVertexTextureCoordBuffer);
                        gl.vertexAttribPointer((ulong)shaderProgram_textureCoordAttribute, moonVertexTextureCoordBuffer_itemSize, gl.FLOAT, false, 0, 0);

                        gl.bindBuffer(gl.ARRAY_BUFFER, moonVertexNormalBuffer);
                        gl.vertexAttribPointer((ulong)shaderProgram_vertexNormalAttribute, moonVertexNormalBuffer_itemSize, gl.FLOAT, false, 0, 0);

                        gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, moonVertexIndexBuffer);
                        setMatrixUniforms();
                        gl.drawElements(gl.TRIANGLES, moonVertexIndexBuffer_numItems, gl.UNSIGNED_SHORT, 0);
                    };



                    #region tick
                    Action tick = null;

                    tick = () =>
                    {
                        if (IsDisposed)
                            return;


                        drawScene();


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
