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
using WebGLLesson01.Sylvester;

namespace WebGLLesson01
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;
    using ScriptCoreLib.Shared.Lambda;


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

            Action webGLStart = delegate
            {
                var canvas = new IHTMLCanvas().AttachTo(page.Content);

                canvas.style.SetSize(500, 500);

                var gl = default(WebGLRenderingContext);
                var shaderProgram = default(WebGLProgram);

                var shaderProgram_vertexPositionAttribute = default(ulong);

                var triangleVertexPositionBuffer = default(WebGLBuffer);
                var squareVertexPositionBuffer = default(WebGLBuffer);

                var triangleVertexPositionBuffer_itemSize = 3;
                var triangleVertexPositionBuffer_numItems = 3;

                var squareVertexPositionBuffer_itemSize = 3;
                var squareVertexPositionBuffer_numItems = 4;

                var gl_viewportWidth = default(int);
                var gl_viewportHeight = default(int);

                var shaderProgram_pMatrixUniform = default(WebGLUniformLocation);
                var shaderProgram_mvMatrixUniform = default(WebGLUniformLocation);

                // JSC could realy try harder to unify Matrix objects on different platforms :)
                var mvMatrix = default(Matrix);

                Action loadIdentity = delegate
                {
                    mvMatrix = Matrix.I(4);
                };

                Action<Matrix> multMatrix = m =>
                {
                    mvMatrix = mvMatrix.x(m);
                };

                ParamsAction<float> mvTranslate = v =>
                {
                    var m = Matrix.Translation(
                        new Vector(v[0], v[1], v[2])
                    ).ensure4x4();

                    multMatrix(m);
                };

                var pMatrix = default(Matrix);

                #region makeFrustum
                Func<f, f, f, f, f, f, Matrix> makeFrustum =
                    (left, right,
                     bottom, top,
                     znear, zfar) =>
                    {
                        var X = 2 * znear / (right - left);
                        var Y = 2 * znear / (top - bottom);
                        var A = (right + left) / (right - left);
                        var B = (top + bottom) / (top - bottom);
                        var C = -(zfar + znear) / (zfar - znear);
                        var D = -2 * zfar * znear / (zfar - znear);



                        return new Matrix(
                            X, 0f, A, 0f,
                            0f, Y, B, 0f,
                            0f, 0f, C, D,
                            0f, 0f, -1f, 0f
                        );
                    };
                #endregion


                #region makePerspective
                Func<f, f, f, f, Matrix> makePerspective = (fovy, aspect, znear, zfar) =>
                {
                    var ymax = znear * (float)Math.Tan(fovy * Math.PI / 360.0);
                    var ymin = -ymax;
                    var xmin = ymin * aspect;
                    var xmax = ymax * aspect;

                    return makeFrustum(xmin, xmax, ymin, ymax, znear, zfar);
                };
                #endregion

                #region perspective
                Action<float, float, float, float> perspective = (fovy, aspect, znear, zfar) =>
                {
                    pMatrix = makePerspective(fovy, aspect, znear, zfar);
                };
                #endregion

                #region setMatrixUniforms
                Action setMatrixUniforms = delegate
                {
                    gl.uniformMatrix4fv(shaderProgram_pMatrixUniform, false, new Float32Array(pMatrix.flatten()));
                    gl.uniformMatrix4fv(shaderProgram_mvMatrixUniform, false, new Float32Array(mvMatrix.flatten()));
                };
                #endregion


                #region initBuffers
                Action initBuffers = delegate
                {
                    #region triangleVertexPositionBuffer
                    {
                        triangleVertexPositionBuffer = gl.createBuffer();

                        gl.bindBuffer(gl.ARRAY_BUFFER, triangleVertexPositionBuffer);

                        float[] vertices = {
                         0.0f,  1.0f,  0.0f,
                        -1.0f, -1.0f,  0.0f,
                         1.0f, -1.0f,  0.0f
                    };

                        gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices), gl.STATIC_DRAW);
                    }


                    #endregion

                    #region squareVertexPositionBuffer
                    {
                        squareVertexPositionBuffer = gl.createBuffer();

                        gl.bindBuffer(gl.ARRAY_BUFFER, squareVertexPositionBuffer);

                        float[] vertices = {
                         1.0f,  1.0f,  0.0f,
                        -1.0f,  1.0f,  0.0f,
                         1.0f, -1.0f,  0.0f,
                        -1.0f, -1.0f,  0.0f
                    };

                        gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices), gl.STATIC_DRAW);

                    }

                    #endregion

                };
                #endregion

                #region drawScene
                Action drawScene = delegate
                {
                    gl.viewport(0, 0, gl_viewportWidth, gl_viewportHeight);

                    gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

                    perspective(45, gl_viewportWidth / gl_viewportHeight, 0.1f, 100.0f);

                    loadIdentity();

                    mvTranslate(-1.5f, 0.0f, -7.0f);

                    gl.bindBuffer(gl.ARRAY_BUFFER, triangleVertexPositionBuffer);
                    gl.vertexAttribPointer(shaderProgram_vertexPositionAttribute, triangleVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);

                    setMatrixUniforms();

                    gl.drawArrays(gl.TRIANGLES, 0, triangleVertexPositionBuffer_numItems);

                    mvTranslate(3.0f, 0.0f, 0.0f);

                    gl.bindBuffer(gl.ARRAY_BUFFER, squareVertexPositionBuffer);
                    gl.vertexAttribPointer(shaderProgram_vertexPositionAttribute, squareVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);

                    setMatrixUniforms();

                    gl.drawArrays(gl.TRIANGLE_STRIP, 0, squareVertexPositionBuffer_numItems);
                };
                #endregion

                #region initGL
                Action initGL = delegate
                {
                    try
                    {
                        gl = (WebGLRenderingContext)canvas.getContext("experimental-webgl");
                        gl_viewportWidth = canvas.width;
                        gl_viewportHeight = canvas.height;
                    }
                    catch
                    {
                    }

                    if (gl == null)
                        Native.Window.alert("Could not initialise WebGL, sorry :-(");

                };
                #endregion

                #region getShader
                Func<string, ulong, WebGLShader> getShader = (source, type) =>
                {
                    var shader = gl.createShader(type);

                    gl.shaderSource(shader, source);
                    gl.compileShader(shader);

                    if (gl.getShaderParameter(shader, gl.COMPILE_STATUS) == null)
                    {
                        Native.Window.alert(gl.getShaderInfoLog(shader));
                        return null;
                    }

                    return shader;
                };
                #endregion


                #region initShaders
                Action initShaders = delegate
                {
                    const string FragmentShader = @"
  precision highp float;
 
  void main(void) {
    gl_FragColor = vec4(1.0, 1.0, 1.0, 1.0);
  }
                    ";

                    const string VertexShader = @"
  attribute vec3 aVertexPosition;
 
  uniform mat4 uMVMatrix;
  uniform mat4 uPMatrix;
 
  void main(void) {
    gl_Position = uPMatrix * uMVMatrix * vec4(aVertexPosition, 1.0);
  }
                    ";

                    var vertexShader = getShader(FragmentShader, gl.FRAGMENT_SHADER);
                    var fragmentShader = getShader(VertexShader, gl.VERTEX_SHADER);


                    shaderProgram = gl.createProgram();
                    gl.attachShader(shaderProgram, vertexShader);
                    gl.attachShader(shaderProgram, fragmentShader);
                    gl.linkProgram(shaderProgram);

                    if (gl.getProgramParameter(shaderProgram, gl.LINK_STATUS) == null)
                    {
                        Native.Window.alert("Could not initialise shaders");
                    }

                    gl.useProgram(shaderProgram);

                    shaderProgram_vertexPositionAttribute = (ulong)gl.getAttribLocation(shaderProgram, "aVertexPosition");
                    gl.enableVertexAttribArray(shaderProgram_vertexPositionAttribute);

                    shaderProgram_pMatrixUniform = gl.getUniformLocation(shaderProgram, "uPMatrix");
                    shaderProgram_mvMatrixUniform = gl.getUniformLocation(shaderProgram, "uMVMatrix");
                };
                #endregion

                initGL();
                initShaders();
                initBuffers();

                gl.clearColor(0.0f, 0.0f, 0.0f, 1.0f);
                gl.clearDepth(1.0f);
                gl.enable(gl.DEPTH_TEST);
                gl.depthFunc(gl.LEQUAL);


                Native.Window.setInterval(drawScene, 15);

            };


            webGLStart();

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
