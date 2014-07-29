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
using chrome;

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

            // if we are able to preload, would we be able to act as adblock?


            // as per X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionShadowExperiment\ChromeExtensionShadowExperiment\Application.cs



            // first lets get this test running in chrome


            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_tabs = self_chrome.tabs;

            if (self_chrome_tabs != null)
            {
                #region Installed

                chrome.runtime.Installed += delegate
                {
                    // our API does not have a Show
                    new chrome.Notification
                    {
                        Message = "Extension Installed!"
                    };
                };
                #endregion


                chrome.webNavigation.Committed +=
                    z =>
                    {
                        var n = new Notification
                        {
                            Message = "webNavigation! " + new { z }
                        };
                    };


                chrome.tabs.Created +=
                     (z) =>
                    {
                        var n = new Notification
                        {
                            Message = "Created! " + new { z.id }
                        };
                    };

                chrome.tabs.Activated +=
                     (z) =>
                    {
                        var n = new Notification
                        {
                            Message = "Activated! " + new { z }
                        };

                    };


                return;
            }
        }

    }
}
