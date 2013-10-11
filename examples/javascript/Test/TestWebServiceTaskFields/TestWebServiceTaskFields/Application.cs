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
using System.Threading.Tasks;
using System.Xml.Linq;
using TestWebServiceTaskFields;
using TestWebServiceTaskFields.Design;
using TestWebServiceTaskFields.HTML.Pages;

namespace TestWebServiceTaskFields
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

            new IHTMLButton { innerText = "yield" }.AttachToDocument().WhenClicked(
                async delegate
                {
                    await yield();

                    // Time to load fields from the cookie, were they even sent?

                    new { this.Foo }.ToString().ToDocumentTitle();
                }
            );

        }

    }
}
