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
using TestCastToInt64;
using TestCastToInt64.Design;
using TestCastToInt64.HTML.Pages;

namespace TestCastToInt64
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
            //~~1388083254728
            //808818120
            //Math.floor(1388083254728)
            //1388083254728

            // X:\jsc.svn\examples\javascript\Test\TestCastToInt64\TestCastToInt64\Application.cs

            double x = 1388083254728.6;

            new IHTMLPre { new { x } }.AttachToDocument();

            long y = 1388083254728;

            new IHTMLPre { new { y } }.AttachToDocument();

            //{ x = 1388083254728 }
            //{ y = 1388083254728 }
            //{ z = 808818120 }

            var z = (long)x;

            new IHTMLPre { new { z } }.AttachToDocument();
        }

    }
}
