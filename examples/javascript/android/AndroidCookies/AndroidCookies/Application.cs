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
using AndroidCookies.Design;
using AndroidCookies.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;

namespace AndroidCookies
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

            page.Output.innerText = new Cookie("message").Value;

            new Cookie("status").Value = "from javascript";


            page.Refresh.WhenClicked(
                delegate
                {
                    Native.Window.applicationCache.update();
                }
            );
        }

    }
}
