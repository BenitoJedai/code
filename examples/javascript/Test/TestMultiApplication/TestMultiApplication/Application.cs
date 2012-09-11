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
using TestMultiApplication.Design;
using TestMultiApplication.HTML.Pages;

namespace TestMultiApplication
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
            new IHTMLButton { innerText = "open Other page" }.AttachToDocument().onclick +=
                delegate
                {
                    Native.Window.open("/Other", "_self");

                };

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }

    public sealed class OtherApplication
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public OtherApplication(IOtherPage page)
        {
            new IHTMLButton { innerText = "open Default page" }.AttachToDocument().onclick +=
               delegate
               {
                   Native.Window.open("/", "_self");

               };

            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"other",
                value => value.ToDocumentTitle()
            );
        }

    }
}
