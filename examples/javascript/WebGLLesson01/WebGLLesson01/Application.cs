using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using WebGLLesson01.HTML.Pages;

namespace WebGLLesson01
{
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

                Action initBuffers = delegate
                {

                };

                // initGL
                // initShaders
                // initBuffers

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
