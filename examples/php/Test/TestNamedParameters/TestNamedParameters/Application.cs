using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TestNamedParameters.Design;
using TestNamedParameters.HTML.Pages;

namespace TestNamedParameters
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
            var sw = new Stopwatch();

            sw.Start();

            service.WebMethod2(
                @"A string from JavaScript.",
                value =>
                {
                    var p = new IHTMLPre { };


                    new IHTMLSpan { innerText = "[" + sw.ElapsedMilliseconds + "ms] " }.AttachTo(p).style.color = "gray";
                    new IHTMLSpan { innerText = value }.AttachTo(p);

                    p.AttachToDocument();
                }
            );
        }

    }
}
