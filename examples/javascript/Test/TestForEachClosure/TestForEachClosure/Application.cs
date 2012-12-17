using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TestForEachClosure.Design;
using TestForEachClosure.HTML.Pages;

namespace TestForEachClosure
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        // via http://www.amazedsaint.com/2012/12/a-quick-note-on-closing-lambda-loop.html


        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            var items = new List<int>() { 1, 2, 3, 4, 5 };
            var queue = new List<Func<int>>();
            foreach (var item in items)
                queue.Add(() => item);
            foreach (var q in queue) new IHTMLPre { innerText = "" + q() }.AttachToDocument();
        }

    }
}
