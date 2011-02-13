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

                var gl = default(WebGLRenderingContext);

                var triangleVertexPositionBuffer = default(WebGLBuffer);
                var squareVertexPositionBuffer = default(WebGLBuffer);

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

                    var triangleVertexPositionBuffer_itemSize = 3;
                    var triangleVertexPositionBuffer_numItems = 3;
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
                    var squareVertexPositionBuffer_itemSize = 3;
                    var squareVertexPositionBuffer_numItems = 4;
                    #endregion

                };
                #endregion

                Action drawScene = delegate
                {
                    // viewport?
                    //gl.viewport(0, 0, gl.viewportWidth, gl.viewportHeight);

                    gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

                    // perspective ?
                    //perspective(45, gl.viewportWidth / gl.viewportHeight, 0.1, 100.0);

                    //loadIdentity();
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
