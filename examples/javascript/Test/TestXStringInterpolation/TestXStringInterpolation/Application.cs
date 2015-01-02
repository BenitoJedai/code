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
using TestXStringInterpolation;
using TestXStringInterpolation.Design;
using TestXStringInterpolation.HTML.Pages;

namespace TestXStringInterpolation
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
            // X:\jsc.svn\examples\javascript\Test\TestStringInterpolation\TestStringInterpolation\Application.cs
            // http://davefancher.com/2014/11/19/fun-with-code-diagnostic-analyzers/

            //new IHTMLPre {  $"" }.AttachToDocument();
            // looks like cshtml?
            // http://davefancher.com/2014/12/04/c-6-0-string-interpolation/
            // http://roslyn.codeplex.com/discussions/570292
            // where are xml literals?
            //var x = <xml />
            // http://roslyn.codeplex.com/workitem/328

            //new IHTMLDiv { new XElement("b", "bold text") }.AttachToDocument();

            var text = new { goo = "hello world" };

            new XElement("b", "bold \{text.goo}").AttachToDocument();
        }

    }
}
