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
using WebGLLesson09.HTML.Pages;
using WebGLLesson09.Styles;
using WebGLLesson09.Design;

namespace WebGLLesson09
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;

    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    internal sealed class Application
    {
        // based on http://learningwebgl.com/lessons/lesson09/index.html

        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly DefaultStyle style = new DefaultStyle();

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



            //         var shaderProgram;

            //function initShaders() {
            //    var fragmentShader = getShader(gl, "shader-fs");
            //    var vertexShader = getShader(gl, "shader-vs");

            //    shaderProgram = gl.createProgram();
            //    gl.attachShader(shaderProgram, vertexShader);
            //    gl.attachShader(shaderProgram, fragmentShader);
            //    gl.linkProgram(shaderProgram);

            //    if (!gl.getProgramParameter(shaderProgram, gl.LINK_STATUS)) {
            //        alert("Could not initialise shaders");
            //    }

            //    gl.useProgram(shaderProgram);

            //    shaderProgram.vertexPositionAttribute = gl.getAttribLocation(shaderProgram, "aVertexPosition");
            //    gl.enableVertexAttribArray(shaderProgram.vertexPositionAttribute);

            //    shaderProgram.textureCoordAttribute = gl.getAttribLocation(shaderProgram, "aTextureCoord");
            //    gl.enableVertexAttribArray(shaderProgram.textureCoordAttribute);

            //    shaderProgram.pMatrixUniform = gl.getUniformLocation(shaderProgram, "uPMatrix");
            //    shaderProgram.mvMatrixUniform = gl.getUniformLocation(shaderProgram, "uMVMatrix");
            //    shaderProgram.samplerUniform = gl.getUniformLocation(shaderProgram, "uSampler");
            //    shaderProgram.colorUniform = gl.getUniformLocation(shaderProgram, "uColor");
            //}







            //var mvMatrix = mat4.create();
            //var mvMatrixStack = [];
            //var pMatrix = mat4.create();

            //function mvPushMatrix() {
            //    var copy = mat4.create();
            //    mat4.set(mvMatrix, copy);
            //    mvMatrixStack.push(copy);
            //}

            //function mvPopMatrix() {
            //    if (mvMatrixStack.length == 0) {
            //        throw "Invalid popMatrix!";
            //    }
            //    mvMatrix = mvMatrixStack.pop();
            //}


            //function setMatrixUniforms() {
            //    gl.uniformMatrix4fv(shaderProgram.pMatrixUniform, false, pMatrix);
            //    gl.uniformMatrix4fv(shaderProgram.mvMatrixUniform, false, mvMatrix);
            //}


            //function degToRad(degrees) {
            //    return degrees * Math.PI / 180;
            //}


            //var currentlyPressedKeys = {};

            //function handleKeyDown(event) {
            //    currentlyPressedKeys[event.keyCode] = true;
            //}


            //function handleKeyUp(event) {
            //    currentlyPressedKeys[event.keyCode] = false;
            //}


            //var zoom = -15;


            //var tilt = 90;
            //var spin = 0;


            //function handleKeys() {
            //    if (currentlyPressedKeys[33]) {
            //        // Page Up
            //        zoom -= 0.1;
            //    }
            //    if (currentlyPressedKeys[34]) {
            //        // Page Down
            //        zoom += 0.1;
            //    }
            //    if (currentlyPressedKeys[38]) {
            //        // Up cursor key
            //        tilt += 2;
            //    }
            //    if (currentlyPressedKeys[40]) {
            //        // Down cursor key
            //        tilt -= 2;
            //    }
            //}


            //var starVertexPositionBuffer;
            //var starVertexTextureCoordBuffer;

            //function initBuffers() {
            //    starVertexPositionBuffer = gl.createBuffer();
            //    gl.bindBuffer(gl.ARRAY_BUFFER, starVertexPositionBuffer);
            //    vertices = [
            //        -1.0, -1.0,  0.0,
            //         1.0, -1.0,  0.0,
            //        -1.0,  1.0,  0.0,
            //         1.0,  1.0,  0.0
            //    ];
            //    gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices), gl.STATIC_DRAW);
            //    starVertexPositionBuffer.itemSize = 3;
            //    starVertexPositionBuffer.numItems = 4;

            //    starVertexTextureCoordBuffer = gl.createBuffer();
            //    gl.bindBuffer(gl.ARRAY_BUFFER, starVertexTextureCoordBuffer);
            //    var textureCoords = [
            //        0.0, 0.0,
            //        1.0, 0.0,
            //        0.0, 1.0,
            //        1.0, 1.0
            //    ];
            //    gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(textureCoords), gl.STATIC_DRAW);
            //    starVertexTextureCoordBuffer.itemSize = 2;
            //    starVertexTextureCoordBuffer.numItems = 4;
            //}


            //function drawStar() {
            //    gl.activeTexture(gl.TEXTURE0);
            //    gl.bindTexture(gl.TEXTURE_2D, starTexture);
            //    gl.uniform1i(shaderProgram.samplerUniform, 0);

            //    gl.bindBuffer(gl.ARRAY_BUFFER, starVertexTextureCoordBuffer);
            //    gl.vertexAttribPointer(shaderProgram.textureCoordAttribute, starVertexTextureCoordBuffer.itemSize, gl.FLOAT, false, 0, 0);

            //    gl.bindBuffer(gl.ARRAY_BUFFER, starVertexPositionBuffer);
            //    gl.vertexAttribPointer(shaderProgram.vertexPositionAttribute, starVertexPositionBuffer.itemSize, gl.FLOAT, false, 0, 0);

            //    setMatrixUniforms();
            //    gl.drawArrays(gl.TRIANGLE_STRIP, 0, starVertexPositionBuffer.numItems);
            //}



            //function Star(startingDistance, rotationSpeed) {
            //    this.angle = 0;
            //    this.dist = startingDistance;
            //    this.rotationSpeed = rotationSpeed;

            //    // Set the colors to a starting value.
            //    this.randomiseColors();
            //}

            //Star.prototype.draw = function (tilt, spin, twinkle) {
            //    mvPushMatrix();

            //    // Move to the star's position
            //    mat4.rotate(mvMatrix, degToRad(this.angle), [0.0, 1.0, 0.0]);
            //    mat4.translate(mvMatrix, [this.dist, 0.0, 0.0]);

            //    // Rotate back so that the star is facing the viewer
            //    mat4.rotate(mvMatrix, degToRad(-this.angle), [0.0, 1.0, 0.0]);
            //    mat4.rotate(mvMatrix, degToRad(-tilt), [1.0, 0.0, 0.0]);

            //    if (twinkle) {
            //        // Draw a non-rotating star in the alternate "twinkling" color
            //        gl.uniform3f(shaderProgram.colorUniform, this.twinkleR, this.twinkleG, this.twinkleB);
            //        drawStar();
            //    }

            //    // All stars spin around the Z axis at the same rate
            //    mat4.rotate(mvMatrix, degToRad(spin), [0.0, 0.0, 1.0]);

            //    // Draw the star in its main color
            //    gl.uniform3f(shaderProgram.colorUniform, this.r, this.g, this.b);
            //    drawStar()

            //    mvPopMatrix();
            //};


            //var effectiveFPMS = 60 / 1000;
            //Star.prototype.animate = function (elapsedTime) {
            //    this.angle += this.rotationSpeed * effectiveFPMS * elapsedTime;

            //    // Decrease the distance, resetting the star to the outside of
            //    // the spiral if it's at the center.
            //    this.dist -= 0.01 * effectiveFPMS * elapsedTime;
            //    if (this.dist < 0.0) {
            //        this.dist += 5.0;
            //        this.randomiseColors();
            //    }

            //};


            //Star.prototype.randomiseColors = function () {
            //    // Give the star a random color for normal
            //    // circumstances...
            //    this.r = Math.random();
            //    this.g = Math.random();
            //    this.b = Math.random();

            //    // When the star is twinkling, we draw it twice, once
            //    // in the color below (not spinning) and then once in the
            //    // main color defined above.
            //    this.twinkleR = Math.random();
            //    this.twinkleG = Math.random();
            //    this.twinkleB = Math.random();
            //};



            //var stars = [];

            //function initWorldObjects() {
            //    var numStars = 50;

            //    for (var i=0; i < numStars; i++) {
            //        stars.push(new Star((i / numStars) * 5.0, i / numStars));
            //    }
            //}


            //function drawScene() {
            //    gl.viewport(0, 0, gl.viewportWidth, gl.viewportHeight);
            //    gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

            //    mat4.perspective(45, gl.viewportWidth / gl.viewportHeight, 0.1, 100.0, pMatrix);

            //    gl.blendFunc(gl.SRC_ALPHA, gl.ONE);
            //    gl.enable(gl.BLEND);

            //    mat4.identity(mvMatrix);
            //    mat4.translate(mvMatrix, [0.0, 0.0, zoom]);
            //    mat4.rotate(mvMatrix, degToRad(tilt), [1.0, 0.0, 0.0]);

            //    var twinkle = document.getElementById("twinkle").checked;
            //    for (var i in stars) {
            //        stars[i].draw(tilt, spin, twinkle);
            //        spin += 0.1;
            //    }

            //}


            //var lastTime = 0;

            //function animate() {
            //    var timeNow = new Date().getTime();
            //    if (lastTime != 0) {
            //        var elapsed = timeNow - lastTime;

            //        for (var i in stars) {
            //            stars[i].animate(elapsed);
            //        }
            //    }
            //    lastTime = timeNow;

            //}


            //function tick() {
            //    requestAnimFrame(tick);
            //    handleKeys();
            //    drawScene();
            //    animate();
            //}



            //function webGLStart() {
            //    var canvas = document.getElementById("lesson09-canvas");
            //    initGL(canvas);
            //    initShaders();
            //    initBuffers();
            //    initTexture();
            //    initWorldObjects();

            //    gl.clearColor(0.0, 0.0, 0.0, 1.0);

            //    document.onkeydown = handleKeyDown;
            //    document.onkeyup = handleKeyUp;

            //    tick();
            //}

            new HTML.Images.FromAssets.star().InvokeOnComplete(
               texture_image =>
               {
                   var starTexture = gl.createTexture();

                   #region handleLoadedTexture
                   gl.pixelStorei(gl.UNPACK_FLIP_Y_WEBGL, 1);
                   gl.bindTexture(gl.TEXTURE_2D, starTexture);
                   gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, texture_image);
                   gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, (long)gl.LINEAR);
                   gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, (long)gl.LINEAR);

                   gl.bindTexture(gl.TEXTURE_2D, null);
                   #endregion



               }
            );
        }
    }
}
