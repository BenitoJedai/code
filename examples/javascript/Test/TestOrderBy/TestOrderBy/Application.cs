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
using TestOrderBy.Design;
using TestOrderBy.HTML.Pages;

namespace TestOrderBy
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
        public Application(IDefaultPage page)
        {
            var data = new[]
            {
                new { Text = "foo", i = "a", j = 71},
                new { Text = "goo", i = "d", j = 7},
                new { Text = "boo", i = "b", j = 87},
                new { Text = "noo", i = "c", j = 2}
            };

            data.OrderBy(k => k.i).WithEach(
                k =>
                {
                    new IHTMLPre { innerText = k.ToString() }.AttachToDocument();
                }
            );

            
            data.OrderBy(k => k.j).WithEach(
                k =>
                {
                    new IHTMLPre { innerText = k.ToString() }.AttachToDocument();
                }
            );

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
