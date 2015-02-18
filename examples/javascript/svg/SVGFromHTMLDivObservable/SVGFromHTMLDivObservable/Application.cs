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
using SVGFromHTMLDivObservable;
using SVGFromHTMLDivObservable.Design;
using SVGFromHTMLDivObservable.HTML.Pages;
using ScriptCoreLib.JavaScript.DOM.SVG;

namespace SVGFromHTMLDivObservable
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
            var l = new NotificationLayout().layout;

            l.AttachToDocument();

            var div0 = new IHTMLDiv().AttachToDocument();

            new { }.With(
                async delegate
                {

                    do
                    {
                        div0.Clear();

                        new IHTMLHorizontalRule().AttachToDocument();

                        Task<ISVGSVGElement> n = l;

                        var svg = await n;

                        IHTMLImage i = svg;

                        var c = new CanvasRenderingContext2D(l.clientWidth, l.clientHeight);
                        c.drawImage(i, 0, 0, l.clientWidth, l.clientHeight);
                        c.canvas.AttachTo(div0);
                    }
                    while (await l.async.onmutation);

                }
            );
        }

    }
}
