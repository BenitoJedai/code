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
using TestDateTimeOperators;
using TestDateTimeOperators.Design;
using TestDateTimeOperators.HTML.Pages;

namespace TestDateTimeOperators
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
            var d1 = DateTime.Now;
            var d2 = d1.AddDays(1);
            var d3 = DateTime.Now;

            if (d1 <= d2)
            {
                new IHTMLPre { innerText = "d1 <= d2" }.AttachToDocument();
            }
            if(d1 == d2)
            {
                new IHTMLPre { innerText = "d1 == d2" }.AttachToDocument();
            }
            if (d1 == d3)
            {
                new IHTMLPre { innerText = "d1 == d3" }.AttachToDocument();
            }
            if (d2 <= d1)
            {
                new IHTMLPre { innerText = "d2 <= d1" }.AttachToDocument();
            }
            if (d2 >= d1)
            {
                new IHTMLPre { innerText = "d2 >= d1" }.AttachToDocument();
            }
            if (d1 > d2)
            {
                new IHTMLPre { innerText = "d1 > d2" }.AttachToDocument();
            }
            if (d1 < d3)
            {
                new IHTMLPre { innerText = "d1 < d2" }.AttachToDocument();
            }
            if (d2 > d1)
            {
                new IHTMLPre { innerText = "d2 > d1" }.AttachToDocument();
            }
            if (d1 != d2)
            {
                new IHTMLPre { innerText = "d1 != d2" }.AttachToDocument();
            }
        }

    }
}
