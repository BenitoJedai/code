using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AsCanvasExperiment;
using AsCanvasExperiment.Design;
using AsCanvasExperiment.HTML.Pages;

namespace AsCanvasExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            Native.body.style.backgroundColor = "cyan";

            // X:\jsc.svn\examples\javascript\WebGL\WebGLSVGAnonymous\WebGLSVGAnonymous\Application.cs
            // X:\jsc.svn\examples\javascript\WebGL\WebGLVRCreativeLeadership\WebGLVRCreativeLeadership\Application.cs
            // X:\jsc.svn\examples\javascript\WebGL\WebGLSVGSprite\WebGLSVGSprite\Application.cs

            //var l = new NotificationLayout().layout;
            var l = new NotificationLayout();

            l.layout.style.background = "";


            // : INodeConvertible<IHTMLElement>
            //var c = (IHTMLCanvas)l.layout;
            //var c = (IHTMLCanvas)l;

            // look we have a databound 2D image!
            var c = l.AsCanvas();

            // could we apply hit test areas?
            c.AttachToDocument();


            Native.document.onmousemove +=
                e =>
                {

                    l.Message = new { e.CursorX, e.CursorY }.ToString();
                };
            //};


        }

    }
}
