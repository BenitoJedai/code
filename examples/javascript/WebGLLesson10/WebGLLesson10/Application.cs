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
using WebGLLesson10.HTML.Pages;
using WebGLLesson10.Styles;
using WebGLLesson10.Design;

namespace WebGLLesson10
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;
    using System.Collections.Generic;

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





            //var gl;

            //function initGL(canvas) {
            //    try {
            //        gl = canvas.getContext("experimental-webgl");
            //        gl.viewportWidth = canvas.width;
            //        gl.viewportHeight = canvas.height;
            //    } catch (e) {
            //    }
            //    if (!gl) {
            //        alert("Could not initialise WebGL, sorry :-(");
            //    }
            //}


            //function getShader(gl, id) {
            //    var shaderScript = document.getElementById(id);
            //    if (!shaderScript) {
            //        return null;
            //    }

            //    var str = "";
            //    var k = shaderScript.firstChild;
            //    while (k) {
            //        if (k.nodeType == 3) {
            //            str += k.textContent;
            //        }
            //        k = k.nextSibling;
            //    }

            //    var shader;
            //    if (shaderScript.type == "x-shader/x-fragment") {
            //        shader = gl.createShader(gl.FRAGMENT_SHADER);
            //    } else if (shaderScript.type == "x-shader/x-vertex") {
            //        shader = gl.createShader(gl.VERTEX_SHADER);
            //    } else {
            //        return null;
            //    }

            //    gl.shaderSource(shader, str);
            //    gl.compileShader(shader);

            //    if (!gl.getShaderParameter(shader, gl.COMPILE_STATUS)) {
            //        alert(gl.getShaderInfoLog(shader));
            //        return null;
            //    }

            //    return shader;
            //}


            //var shaderProgram;

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
            //}


            //function handleLoadedTexture(texture) {
            //    gl.pixelStorei(gl.UNPACK_FLIP_Y_WEBGL, true);
            //    gl.bindTexture(gl.TEXTURE_2D, texture);
            //    gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, texture.image);
            //    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, gl.LINEAR);
            //    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, gl.LINEAR);

            //    gl.bindTexture(gl.TEXTURE_2D, null);
            //}


            //var mudTexture;

            //function initTexture() {
            //    mudTexture = gl.createTexture();
            //    mudTexture.image = new Image();
            //    mudTexture.image.onload = function () {
            //        handleLoadedTexture(mudTexture)
            //    }

            //    mudTexture.image.src = "mud.gif";
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


            //var pitch = 0;
            //var pitchRate = 0;

            //var yaw = 0;
            //var yawRate = 0;

            //var xPos = 0;
            //var yPos = 0.4;
            //var zPos = 0;

            //var speed = 0;

            //function handleKeys() {
            //    if (currentlyPressedKeys[33]) {
            //        // Page Up
            //        pitchRate = 0.1;
            //    } else if (currentlyPressedKeys[34]) {
            //        // Page Down
            //        pitchRate = -0.1;
            //    } else {
            //        pitchRate = 0;
            //    }

            //    if (currentlyPressedKeys[37] || currentlyPressedKeys[65]) {
            //        // Left cursor key or A
            //        yawRate = 0.1;
            //    } else if (currentlyPressedKeys[39] || currentlyPressedKeys[68]) {
            //        // Right cursor key or D
            //        yawRate = -0.1;
            //    } else {
            //        yawRate = 0;
            //    }

            //    if (currentlyPressedKeys[38] || currentlyPressedKeys[87]) {
            //        // Up cursor key or W
            //        speed = 0.003;
            //    } else if (currentlyPressedKeys[40] || currentlyPressedKeys[83]) {
            //        // Down cursor key
            //        speed = -0.003;
            //    } else {
            //        speed = 0;
            //    }

            //}


            //var worldVertexPositionBuffer = null;
            //var worldVertexTextureCoordBuffer = null;

            //function handleLoadedWorld(data) {
            //    var lines = data.split("\n");
            //    var vertexCount = 0;
            //    var vertexPositions = [];
            //    var vertexTextureCoords = [];
            //    for (var i in lines) {
            //        var vals = lines[i].replace(/^\s+/, "").split(/\s+/);
            //        if (vals.length == 5 && vals[0] != "//") {
            //            // It is a line describing a vertex; get X, Y and Z first
            //            vertexPositions.push(parseFloat(vals[0]));
            //            vertexPositions.push(parseFloat(vals[1]));
            //            vertexPositions.push(parseFloat(vals[2]));

            //            // And then the texture coords
            //            vertexTextureCoords.push(parseFloat(vals[3]));
            //            vertexTextureCoords.push(parseFloat(vals[4]));

            //            vertexCount += 1;
            //        }
            //    }

            //    worldVertexPositionBuffer = gl.createBuffer();
            //    gl.bindBuffer(gl.ARRAY_BUFFER, worldVertexPositionBuffer);
            //    gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertexPositions), gl.STATIC_DRAW);
            //    worldVertexPositionBuffer.itemSize = 3;
            //    worldVertexPositionBuffer.numItems = vertexCount;

            //    worldVertexTextureCoordBuffer = gl.createBuffer();
            //    gl.bindBuffer(gl.ARRAY_BUFFER, worldVertexTextureCoordBuffer);
            //    gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertexTextureCoords), gl.STATIC_DRAW);
            //    worldVertexTextureCoordBuffer.itemSize = 2;
            //    worldVertexTextureCoordBuffer.numItems = vertexCount;

            //    document.getElementById("loadingtext").textContent = "";
            //}


            //function loadWorld() {
            //    var request = new XMLHttpRequest();
            //    request.open("GET", "world.txt");
            //    request.onreadystatechange = function () {
            //        if (request.readyState == 4) {
            //            handleLoadedWorld(request.responseText);
            //        }
            //    }
            //    request.send();
            //}



            //function drawScene() {
            //    gl.viewport(0, 0, gl.viewportWidth, gl.viewportHeight);
            //    gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

            //    if (worldVertexTextureCoordBuffer == null || worldVertexPositionBuffer == null) {
            //        return;
            //    }

            //    mat4.perspective(45, gl.viewportWidth / gl.viewportHeight, 0.1, 100.0, pMatrix);

            //    mat4.identity(mvMatrix);

            //    mat4.rotate(mvMatrix, degToRad(-pitch), [1, 0, 0]);
            //    mat4.rotate(mvMatrix, degToRad(-yaw), [0, 1, 0]);
            //    mat4.translate(mvMatrix, [-xPos, -yPos, -zPos]);

            //    gl.activeTexture(gl.TEXTURE0);
            //    gl.bindTexture(gl.TEXTURE_2D, mudTexture);
            //    gl.uniform1i(shaderProgram.samplerUniform, 0);

            //    gl.bindBuffer(gl.ARRAY_BUFFER, worldVertexTextureCoordBuffer);
            //    gl.vertexAttribPointer(shaderProgram.textureCoordAttribute, worldVertexTextureCoordBuffer.itemSize, gl.FLOAT, false, 0, 0);

            //    gl.bindBuffer(gl.ARRAY_BUFFER, worldVertexPositionBuffer);
            //    gl.vertexAttribPointer(shaderProgram.vertexPositionAttribute, worldVertexPositionBuffer.itemSize, gl.FLOAT, false, 0, 0);

            //    setMatrixUniforms();
            //    gl.drawArrays(gl.TRIANGLES, 0, worldVertexPositionBuffer.numItems);
            //}


            //var lastTime = 0;
            //// Used to make us "jog" up and down as we move forward.
            //var joggingAngle = 0;

            //function animate() {
            //    var timeNow = new Date().getTime();
            //    if (lastTime != 0) {
            //        var elapsed = timeNow - lastTime;

            //        if (speed != 0) {
            //            xPos -= Math.sin(degToRad(yaw)) * speed * elapsed;
            //            zPos -= Math.cos(degToRad(yaw)) * speed * elapsed;

            //            joggingAngle += elapsed * 0.6; // 0.6 "fiddle factor" - makes it feel more realistic :-)
            //            yPos = Math.sin(degToRad(joggingAngle)) / 20 + 0.4
            //        }

            //        yaw += yawRate * elapsed;
            //        pitch += pitchRate * elapsed;

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
            //    var canvas = document.getElementById("lesson10-canvas");
            //    initGL(canvas);
            //    initShaders();
            //    initTexture();
            //    loadWorld();

            //    gl.clearColor(0.0, 0.0, 0.0, 1.0);
            //    gl.enable(gl.DEPTH_TEST);

            //    document.onkeydown = handleKeyDown;
            //    document.onkeyup = handleKeyUp;

            //    tick();
            //}
        }
    }


}
