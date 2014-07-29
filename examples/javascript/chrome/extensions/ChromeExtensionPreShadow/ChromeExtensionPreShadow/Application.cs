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
using ChromeExtensionPreShadow;
using ChromeExtensionPreShadow.Design;
using ChromeExtensionPreShadow.HTML.Pages;

namespace ChromeExtensionPreShadow
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
            // can we inject ourselves into a chrome tab
            // before the page loads?

            // http://stackoverflow.com/questions/19191679/chrome-extension-inject-js-before-page-load
            // If you want to dynamically run a script as soon as possible, then call chrome.tabs.executeScript when the chrome.webNavigation.onCommitted event is triggered.

            // when does that happen?

        }

    }
}
