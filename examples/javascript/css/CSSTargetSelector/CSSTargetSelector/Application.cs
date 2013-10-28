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
using CSSTargetSelector;
using CSSTargetSelector.Design;
using CSSTargetSelector.HTML.Pages;

namespace CSSTargetSelector
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

            page.body.style.transition = "background-color 300ms linear";

            // pretty useless?
            page.RED.css[":target"].style.color = "red";
            page.GREEN.css[":target"].style.color = "green";
            page.BLUE.css[":target"].style.color = "blue";

            Native.window.onhashchange +=
                delegate
                {
                    var c = Native.document.location.hash.Substring(1);

                    Native.document.title = c;
                    page.body.style.backgroundColor = c;
                };
        }

    }
}
