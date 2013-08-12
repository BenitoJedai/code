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
using WebGLLesson09.Design;
using WebGLLesson09.HTML.Pages;

namespace WebGLLesson09
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;

    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application
    {
        // based on http://learningwebgl.com/lessons/lesson09/index.html

        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly DefaultStyle style = new DefaultStyle();

        public Action Dispose;


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault  page = null)
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
  
        }

        void InitializeContent(IDefault  page = null)
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

            var shaderProgram_vertexPositionAttribute = getAttribLocation("aVertexPosition");
            gl.enableVertexAttribArray((uint)shaderProgram_vertexPositionAttribute);

            var shaderProgram_textureCoordAttribute = getAttribLocation("aTextureCoord");
            gl.enableVertexAttribArray((uint)shaderProgram_textureCoordAttribute);

            #region getUniformLocation
            Func<string, WebGLUniformLocation> getUniformLocation =
                name => gl.getUniformLocation(shaderProgram, name);
            #endregion
            var shaderProgram_pMatrixUniform = getUniformLocation("uPMatrix");
            var shaderProgram_mvMatrixUniform = getUniformLocation("uMVMatrix");
            var shaderProgram_samplerUniform = getUniformLocation("uSampler");
            var shaderProgram_colorUniform = getUniformLocation("uColor");

            #endregion



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
            #endregion


            #region currentlyPressedKeys
            var currentlyPressedKeys = new Dictionary<int, bool>
            {
                {33, false},
                {34, false},
                {37, false},
                {39, false},
                {38, false},
                {40, false}
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

            var zoom = -15f;

            var tilt = 90f;
            var spin = 0f;


            #region handleKeys
            Action handleKeys =
                delegate
                {
                    if (currentlyPressedKeys[33])
                    {
                        // Page Up
                        zoom -= 0.1f;
                    }
                    if (currentlyPressedKeys[34])
                    {
                        // Page Down
                        zoom += 0.1f;
                    }
                    if (currentlyPressedKeys[38])
                    {
                        // Up cursor key
                        tilt += 2;
                    }
                    if (currentlyPressedKeys[40])
                    {
                        // Down cursor key
                        tilt -= 2;
                    }
                };
            #endregion

            #region initBuffers
            var starVertexPositionBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, starVertexPositionBuffer);
            var vertices = new f[]{
                    -1.0f, -1.0f,  0.0f,
                     1.0f, -1.0f,  0.0f,
                    -1.0f,  1.0f,  0.0f,
                     1.0f,  1.0f,  0.0f
                };
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices), gl.STATIC_DRAW);
            var starVertexPositionBuffer_itemSize = 3;
            var starVertexPositionBuffer_numItems = 4;

            var starVertexTextureCoordBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, starVertexTextureCoordBuffer);
            var textureCoords = new f[]{
                    0.0f, 0.0f,
                    1.0f, 0.0f,
                    0.0f, 1.0f,
                    1.0f, 1.0f
                };
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(textureCoords), gl.STATIC_DRAW);
            var starVertexTextureCoordBuffer_itemSize = 2;
            var starVertexTextureCoordBuffer_numItems = 4;
            #endregion


            #region initWorldObjects
            var stars = new List<Star>();
            var numStars = 50f;

            for (var i = 0; i < numStars; i++)
            {
                stars.Add(new Star((i / numStars) * 5.0f, i / numStars));
            }
            #endregion





            #region animate
            var lastTime = 0L;

            Action animate = () =>
            {
                var timeNow = new IDate().getTime();
                if (lastTime != 0)
                {
                    var elapsed = timeNow - lastTime;

                    foreach (var star in stars)
                    {
                        star.animate(elapsed);
                    }
                }
                lastTime = timeNow;

            };
            #endregion




            gl.clearColor(0.0f, 0.0f, 0.0f, 1.0f);



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



            new HTML.Images.FromAssets.star().InvokeOnComplete(
               texture_image =>
               {
                   var starTexture = gl.createTexture();

                   #region handleLoadedTexture
                   gl.pixelStorei(gl.UNPACK_FLIP_Y_WEBGL, 1);
                   gl.bindTexture(gl.TEXTURE_2D, starTexture);
                   gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, texture_image);
                   gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, (int)gl.LINEAR);
                   gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, (int)gl.LINEAR);

                   gl.bindTexture(gl.TEXTURE_2D, null);
                   #endregion

                   #region drawStar
                   Action drawStar = () =>
                   {
                       gl.activeTexture(gl.TEXTURE0);
                       gl.bindTexture(gl.TEXTURE_2D, starTexture);
                       gl.uniform1i(shaderProgram_samplerUniform, 0);

                       gl.bindBuffer(gl.ARRAY_BUFFER, starVertexTextureCoordBuffer);
                       gl.vertexAttribPointer((uint)shaderProgram_textureCoordAttribute, starVertexTextureCoordBuffer_itemSize, gl.FLOAT, false, 0, 0);

                       gl.bindBuffer(gl.ARRAY_BUFFER, starVertexPositionBuffer);
                       gl.vertexAttribPointer((uint)shaderProgram_vertexPositionAttribute, starVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);

                       setMatrixUniforms();
                       gl.drawArrays(gl.TRIANGLE_STRIP, 0, starVertexPositionBuffer_numItems);
                   };
                   #endregion

                   #region drawScene
                   Action drawScene = delegate
                   {
                       gl.viewport(0, 0, gl_viewportWidth, gl_viewportHeight);
                       gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

                       __glMatrix.mat4.perspective(45, gl_viewportWidth / gl_viewportHeight, 0.1f, 100.0f, pMatrix);

                       gl.blendFunc(gl.SRC_ALPHA, gl.ONE);
                       gl.enable(gl.BLEND);

                       __glMatrix.mat4.identity(mvMatrix);
                       __glMatrix.mat4.translate(mvMatrix, 0.0f, 0.0f, zoom);
                       __glMatrix.mat4.rotate(mvMatrix, degToRad(tilt), 1.0f, 0.0f, 0.0f);

                       //var twinkle = document.getElementById("twinkle").checked;
                       var twinkle = false;

                       foreach (var star in stars)
                       {
                           star.draw(
                               tilt,
                               spin,
                               twinkle,
                               mvPushMatrix,
                               mvPopMatrix,
                               mvMatrix,
                               drawStar,
                               shaderProgram_colorUniform,
                               gl


                               );
                           spin += 0.1f;
                       }

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
    }

    public sealed class Star
    {
        private f angle;
        private f dist;
        private f rotationSpeed;
        private f r;
        private f g;
        private f b;
        private f twinkleR;
        private f twinkleG;
        private f twinkleB;

        public Star(f startingDistance, f rotationSpeed)
        {
            this.angle = 0;
            this.dist = startingDistance;
            this.rotationSpeed = rotationSpeed;

            //    // Set the colors to a starting value.
            this.randomiseColors();
        }

        public void randomiseColors()
        {
            var r = new Random();
            Func<float> Math_random = () => (float)r.NextDouble();

            // Give the star a random color for normal
            // circumstances...
            this.r = Math_random();
            this.g = Math_random();
            this.b = Math_random();

            // When the star is twinkling, we draw it twice, once
            // in the color below (not spinning) and then once in the
            // main color defined above.
            this.twinkleR = Math_random();
            this.twinkleG = Math_random();
            this.twinkleB = Math_random();
        }


        public void draw(f tilt, f spin, bool twinkle, Action mvPushMatrix, Action mvPopMatrix, Float32Array mvMatrix, Action drawStar, WebGLUniformLocation shaderProgram_colorUniform, WebGLRenderingContext gl)
        {
            #region degToRad
            Func<float, float> degToRad = (degrees) =>
            {
                return degrees * (f)Math.PI / 180f;
            };
            #endregion

            mvPushMatrix();

            // Move to the star's position
            __glMatrix.mat4.rotate(mvMatrix, degToRad(this.angle), 0.0f, 1.0f, 0.0f);
            __glMatrix.mat4.translate(mvMatrix, this.dist, 0.0f, 0.0f);

            // Rotate back so that the star is facing the viewer
            __glMatrix.mat4.rotate(mvMatrix, degToRad(-this.angle), 0.0f, 1.0f, 0.0f);
            __glMatrix.mat4.rotate(mvMatrix, degToRad(-tilt), 1.0f, 0.0f, 0.0f);

            //if (twinkle) {
            //    // Draw a non-rotating star in the alternate "twinkling" color
            //    gl.uniform3f(shaderProgram_colorUniform, this.twinkleR, this.twinkleG, this.twinkleB);
            //    drawStar();
            //}

            // All stars spin around the Z axis at the same rate
            __glMatrix.mat4.rotate(mvMatrix, degToRad(spin), 0.0f, 0.0f, 1.0f);

            // Draw the star in its main color
            gl.uniform3f(shaderProgram_colorUniform, this.r, this.g, this.b);
            drawStar();

            mvPopMatrix();
        }


        const f effectiveFPMS = 60f / 1000f;

        public void animate(long elapsedTime)
        {
            this.angle += this.rotationSpeed * effectiveFPMS * elapsedTime;

            // Decrease the distance, resetting the star to the outside of
            // the spiral if it's at the center.
            this.dist -= 0.01f * effectiveFPMS * elapsedTime;
            if (this.dist < 0.0f)
            {
                this.dist += 5.0f;
                this.randomiseColors();
            }

        }



    }


}
