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
using TestBase64;
using TestBase64.Design;
using TestBase64.HTML.Pages;

namespace TestBase64
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

            // https://sites.google.com/a/jsc-solutions.net/backlog/system/app/pages/createPage?source=/knowledge-base/2014/201401
            Console.WriteLine("findme");

            var a = new byte[] { 1, 2, 3 };

            new IHTMLPre {
                new {
                    base64 = Convert.ToBase64String(a)
                    // {{ base64 = AQID }}
                }
            }.AttachToDocument();

        }

    }
}
