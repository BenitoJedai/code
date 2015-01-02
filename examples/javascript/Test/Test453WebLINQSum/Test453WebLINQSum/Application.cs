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
using Test453WebLINQSum;
using Test453WebLINQSum.Design;
using Test453WebLINQSum.HTML.Pages;

namespace Test453WebLINQSum
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {


        class foo { public int goo; }

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150102/linq

        static int Invoke(IEnumerable<foo> f) => (from x in f select x.goo).Sum();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {

            var a = new[]
            {
                new foo { goo = 4 },
                new foo { goo = 5 },
            };

            var sum = Invoke(a);

            Native.document.body.innerText = "sum: \{sum}";

        }

    }
}
