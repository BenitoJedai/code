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
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;


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
                var shaderProgram = default(WebGLShader);

                var shaderProgram_vertexPositionAttribute = default(ulong);

                var triangleVertexPositionBuffer = default(WebGLBuffer);
                var squareVertexPositionBuffer = default(WebGLBuffer);

                var triangleVertexPositionBuffer_itemSize = 3;
                var triangleVertexPositionBuffer_numItems = 3;

                var squareVertexPositionBuffer_itemSize = 3;
                var squareVertexPositionBuffer_numItems = 4;

                var gl_viewportWidth = default(int);
                var gl_viewportHeight = default(int);

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
                    // viewport?
                    //gl.viewport(0, 0, gl.viewportWidth, gl.viewportHeight);

                    gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

                    // perspective ?
                    //perspective(45, gl.viewportWidth / gl.viewportHeight, 0.1, 100.0);

                    //loadIdentity();

                    //mvTranslate([-1.5, 0.0, -7.0]);

                    gl.bindBuffer(gl.ARRAY_BUFFER, triangleVertexPositionBuffer);
                    gl.vertexAttribPointer(shaderProgram_vertexPositionAttribute, triangleVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);

                    // setMatrixUniforms();

                    gl.drawArrays(gl.TRIANGLES, 0, triangleVertexPositionBuffer_numItems);

                    //mvTranslate([3.0, 0.0, 0.0])

                    gl.bindBuffer(gl.ARRAY_BUFFER, squareVertexPositionBuffer);
                    gl.vertexAttribPointer(shaderProgram_vertexPositionAttribute, squareVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);

                    //setMatrixUniforms();

                    gl.drawArrays(gl.TRIANGLE_STRIP, 0, squareVertexPositionBuffer_numItems);
                };
                #endregion

                Action initGL = delegate
                {
                    // should JSC throw type is not castable in JavaScript?
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

                // initGL
                // initShaders
                initBuffers();

                //    gl.clearColor(0.0, 0.0, 0.0, 1.0);
                //    gl.clearDepth(1.0)
                //    gl.enable(gl.DEPTH_TEST);
                //    gl.depthFunc(gl.LEQUAL);

                //    setInterval(drawScene, 15);

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
