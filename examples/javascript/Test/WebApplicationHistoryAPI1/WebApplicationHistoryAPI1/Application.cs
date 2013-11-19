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
using WebApplicationHistoryAPI1.Design;
using WebApplicationHistoryAPI1.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;

namespace WebApplicationHistoryAPI1
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
        public Application(IDefault page)
        {
            Action handler =
                delegate
                {
                    //Native.Window.alert(Native.Document.location.pathname);

                    page.Foo1.style.color = JSColor.None;
                    page.Foo2.style.color = JSColor.None;
                    page.Foo3.style.color = JSColor.None;

                    if (Native.Document.location.pathname == "/Button1")
                        page.Foo1.style.color = JSColor.Red;

                    if (Native.Document.location.pathname == "/Button2")
                        page.Foo2.style.color = JSColor.Red;

                    if (Native.Document.location.pathname == "/Button3")
                        page.Foo3.style.color = JSColor.Red;
                };

            page.Foo1.onclick +=
                delegate
                {
                    Native.window.history.pushState(
                        data: null,
                        title: "/Button1",
                        url: "/Button1"
                    );
                    handler();
                };

            page.Foo2.onclick +=
                delegate
                {
                    Native.window.history.pushState(
                        data: null,
                        title: "/Button2",
                        url: "/Button2"
                    );
                    handler();
                };

            page.Foo3.onclick +=
                delegate
                {
                    Native.window.history.pushState(
                        data: null,
                        title: "/Button3",
                        url: "/Button3"
                    );
                    handler();
                };

            Native.window.onpopstate +=
                delegate
                {
                    handler();
                };

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
