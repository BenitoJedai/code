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
using WebGLLesson01.HTML.Pages;

namespace WebGLLesson01
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;
    using ScriptCoreLib.Shared.Lambda;
    using ScriptCoreLib.Shared.Drawing;


    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    internal sealed class Application
    {
        /* This example will be a port of http://learningwebgl.com/blog/?p=28 by Giles
         * 
         * 01. Created a new project of type Web Application
         * 02. Port "webGLStart" function
         * 03. Port "initBuffers" function
         * 03. Add "gl" alias for static methods
         * 04. Port "drawScene" function
         * 05. We are not using any dynamic or expando objects and we have to define such variables.
         * 06. We will have to port "sylvester" for Matrix type
         * 07. Port "initShaders" function
         * 08. Continue with Matrix.multiply implementation!
         */

        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            new Library.glMatrix().Content.With(
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

            //         var gl;
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

            //    shaderProgram.pMatrixUniform = gl.getUniformLocation(shaderProgram, "uPMatrix");
            //    shaderProgram.mvMatrixUniform = gl.getUniformLocation(shaderProgram, "uMVMatrix");
            //}


            //var mvMatrix = mat4.create();
            //var pMatrix = mat4.create();

            //function setMatrixUniforms() {
            //    gl.uniformMatrix4fv(shaderProgram.pMatrixUniform, false, pMatrix);
            //    gl.uniformMatrix4fv(shaderProgram.mvMatrixUniform, false, mvMatrix);
            //}



            //var triangleVertexPositionBuffer;
            //var squareVertexPositionBuffer;

            //function initBuffers() {
            //    triangleVertexPositionBuffer = gl.createBuffer();
            //    gl.bindBuffer(gl.ARRAY_BUFFER, triangleVertexPositionBuffer);
            //    var vertices = [
            //         0.0,  1.0,  0.0,
            //        -1.0, -1.0,  0.0,
            //         1.0, -1.0,  0.0
            //    ];
            //    gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices), gl.STATIC_DRAW);
            //    triangleVertexPositionBuffer.itemSize = 3;
            //    triangleVertexPositionBuffer.numItems = 3;

            //    squareVertexPositionBuffer = gl.createBuffer();
            //    gl.bindBuffer(gl.ARRAY_BUFFER, squareVertexPositionBuffer);
            //    vertices = [
            //         1.0,  1.0,  0.0,
            //        -1.0,  1.0,  0.0,
            //         1.0, -1.0,  0.0,
            //        -1.0, -1.0,  0.0
            //    ];
            //    gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices), gl.STATIC_DRAW);
            //    squareVertexPositionBuffer.itemSize = 3;
            //    squareVertexPositionBuffer.numItems = 4;
            //}


            //function drawScene() {
            //    gl.viewport(0, 0, gl.viewportWidth, gl.viewportHeight);
            //    gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

            //    mat4.perspective(45, gl.viewportWidth / gl.viewportHeight, 0.1, 100.0, pMatrix);

            //    mat4.identity(mvMatrix);

            //    mat4.translate(mvMatrix, [-1.5, 0.0, -7.0]);
            //    gl.bindBuffer(gl.ARRAY_BUFFER, triangleVertexPositionBuffer);
            //    gl.vertexAttribPointer(shaderProgram.vertexPositionAttribute, triangleVertexPositionBuffer.itemSize, gl.FLOAT, false, 0, 0);
            //    setMatrixUniforms();
            //    gl.drawArrays(gl.TRIANGLES, 0, triangleVertexPositionBuffer.numItems);


            //    mat4.translate(mvMatrix, [3.0, 0.0, 0.0]);
            //    gl.bindBuffer(gl.ARRAY_BUFFER, squareVertexPositionBuffer);
            //    gl.vertexAttribPointer(shaderProgram.vertexPositionAttribute, squareVertexPositionBuffer.itemSize, gl.FLOAT, false, 0, 0);
            //    setMatrixUniforms();
            //    gl.drawArrays(gl.TRIANGLE_STRIP, 0, squareVertexPositionBuffer.numItems);
            //}



            //function webGLStart() {
            //    var canvas = document.getElementById("lesson01-canvas");
            //    initGL(canvas);
            //    initShaders();
            //    initBuffers();

            //    gl.clearColor(0.0, 0.0, 0.0, 1.0);
            //    gl.enable(gl.DEPTH_TEST);

            //    drawScene();
            //}
        }

    }
}
