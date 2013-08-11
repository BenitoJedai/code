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
using VisibleTitleElement.Design;
using VisibleTitleElement.HTML.Pages;

namespace VisibleTitleElement
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
        public Application(IApp page)
        {
            //@"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value =>
                {
                    //value.ToDocumentTitle();

                    page.foo.AttachTo(page.title);

                    //Native.document.bo
                    page.title.Add("y");


                    //page.foo.AttachTo(Native.document.getElementsByTagName("title")[0]);

                    //Native.document.getElementsByTagName("title")[0].Add("y");

                }
            );
        }

    }
}
