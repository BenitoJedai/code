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
using SVGFromHTMLDiv;
using SVGFromHTMLDiv.Design;
using SVGFromHTMLDiv.HTML.Pages;
using ScriptCoreLib.JavaScript.DOM.SVG;

namespace SVGFromHTMLDiv
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
            // X:\jsc.svn\examples\javascript\WebGL\WebGLSVGAnonymous\WebGLSVGAnonymous\Application.cs
            // X:\jsc.svn\examples\javascript\WebGL\WebGLVRCreativeLeadership\WebGLVRCreativeLeadership\Application.cs
            // X:\jsc.svn\examples\javascript\WebGL\WebGLSVGSprite\WebGLSVGSprite\Application.cs

            var l = new NotificationLayout().layout;

            l.AttachToDocument();


            new { }.With(
                async delegate
                {
                    var retry = new IHTMLButton { "retry" }.AttachToDocument();

                    do
                    {
                        new IHTMLHorizontalRule().AttachToDocument();

                        Task<ISVGSVGElement> n = l;

                        var svg = await n;

                        svg.AttachToDocument();

                        IHTMLImage i = svg;
                        i.AttachToDocument();

                        var c = new CanvasRenderingContext2D(svg.clientWidth, svg.clientHeight);

                        // http://www.w3schools.com/tags/canvas_fillstyle.asp
                        c.fillStyle = "red";

                        c.fillRect(0, 0, svg.clientWidth, svg.clientHeight);

                        // rather than to overload drawimage lets do an operator which is more exotic for the api
                        // at some point the canvas may implement the html drawing obseleting this workaround
                        c.drawImage(i, 0, 0, svg.clientWidth, svg.clientHeight);

                        c.canvas.AttachToDocument();
                    }
                    while (await retry.async.onclick);

                }
            );
        }

    }
}
