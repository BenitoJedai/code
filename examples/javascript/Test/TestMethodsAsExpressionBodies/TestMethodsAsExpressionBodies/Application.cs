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
using TestMethodsAsExpressionBodies;
using TestMethodsAsExpressionBodies.Design;
using TestMethodsAsExpressionBodies.HTML.Pages;

namespace TestMethodsAsExpressionBodies
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public string Name => Field1 + " " + Field2;

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // Upcoming%20Features%20in%20CSharp%20-%20Preview.pdf
            // X:\jsc.svn\examples\javascript\Test\TestStringInterpolation\TestStringInterpolation\Application.cs

            new IHTMLPre { "name: \{this.Name}" }.AttachToDocument();
        }

    }
}
