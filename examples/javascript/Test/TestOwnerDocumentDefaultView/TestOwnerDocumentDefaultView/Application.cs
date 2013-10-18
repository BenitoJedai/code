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
using TestOwnerDocumentDefaultView;
using TestOwnerDocumentDefaultView.Design;
using TestOwnerDocumentDefaultView.HTML.Pages;

namespace TestOwnerDocumentDefaultView
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
            // http://stackoverflow.com/questions/2297217/ie-8-defaultview-equivalent
            // https://developer.mozilla.org/en-US/docs/Web/API/document.defaultView
            // http://www.whatwg.org/specs/web-apps/current-work/#dom-document-defaultview
            // http://stackoverflow.com/questions/9183555/whats-the-point-of-document-defaultview

            {

                var div = new IHTMLDiv { innerText = "?" }.AttachToDocument();

                // http://msdn.microsoft.com/en-us/library/ms534331%28VS.85%29.aspx
                var defaultView = (div.ownerDocument as dynamic).defaultView;

                div.innerText = new { defaultView }.ToString();
            }


          { 

                var div = new IHTMLDiv { innerText = "?" }.AttachToDocument();

                // http://msdn.microsoft.com/en-us/library/ms534331%28VS.85%29.aspx
                var parentwindow = (div.ownerDocument as dynamic).parentwindow;

                div.innerText = new { parentwindow }.ToString();
            }


        }

    }
}
