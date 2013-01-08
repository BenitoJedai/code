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
using MultiMouse.SVGPaint.Design;
using MultiMouse.SVGPaint.HTML.Pages;
using ScriptCoreLib.JavaScript.DOM.SVG;

namespace MultiMouse.SVGPaint
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            page.svg.Orphanize();

            var svg = new ISVGSVGElement().AttachToDocument();
            var path = new ISVGPathElement().AttachTo(svg);

            path.setAttribute("style", "stroke: black; stroke-width: 4; fill: none;");

            path.d = "    M100,50  L10,10   L200,200   ";

            Native.Document.body.onmousemove +=
                e =>
                {
                    path.d += " L" + e.CursorX + "," + e.CursorY;

                };
        }

    }
}
