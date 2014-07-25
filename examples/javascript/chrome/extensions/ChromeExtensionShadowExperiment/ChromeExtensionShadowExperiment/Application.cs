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
using ChromeExtensionShadowExperiment;
using ChromeExtensionShadowExperiment.Design;
using ChromeExtensionShadowExperiment.HTML.Pages;
using chrome;

namespace ChromeExtensionShadowExperiment
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
            // based on
            // X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionWithWorker\ChromeExtensionWithWorker\Application.cs
            // X:\jsc.svn\examples\javascript\Test\TestShadowDocumentWithForms\TestShadowDocumentWithForms\Application.cs

            // lets test shadow on external web apps

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


                var IgnoreSecondaryUpdatesFor = new List<TabIdInteger>();

                #region Updated
                chrome.tabs.Updated +=
                    async (i, x, tab) =>
                    {
                        // chrome://newtab/

                        if (tab.url.StartsWith("chrome-devtools://"))
                            return;

                        if (tab.url.StartsWith("chrome://"))
                            return;

                        if (tab.status != "complete")
                            return;


                        if (IgnoreSecondaryUpdatesFor.Contains(tab.id))
                            return;

                        // inject?

                        // what if we sent the uri to our android tab?
                        var n = new Notification
                        {
                            Message = "Updated! " + new { tab.id, tab.url }
                        };

                        IgnoreSecondaryUpdatesFor.Add(tab.id);
                    };
                #endregion



                return;

            }

            // run it
            // save view-source
            // subst
            // test in chrome

        }

    }
}
