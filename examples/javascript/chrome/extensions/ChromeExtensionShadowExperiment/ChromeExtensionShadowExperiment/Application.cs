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
using System.Net;
using TestShadowDocumentWithForms.HTML.Pages;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms;
using TestShadowDocumentWithForms;

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
        public Application(ChromeExtensionShadowExperiment.HTML.Pages.IApp page)
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




                chrome.tabs.Created +=
                    async (z) =>
                    {
                        var n = new chrome.Notification
                        {
                            Message = "Created! " + new { z.id }
                        };
                    };

                chrome.tabs.Activated +=
                    async (z) =>
                    {
                        var n = new chrome.Notification
                        {
                            Message = "Activated! " + new { z }
                        };

                    };


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
                        var n = new chrome.Notification
                        {
                            Message = "Updated! " + new { tab.id, tab.url }
                        };

                        IgnoreSecondaryUpdatesFor.Add(tab.id);



                        await tab.pageAction.async.onclick;

                        var nn = new chrome.Notification
                        {
                            Message = "Clicked " + new { tab.id, tab.url }
                        };


                        // document.currentScript?
                        var code = await new WebClient().DownloadStringTaskAsync(
                              new Uri(Worker.ScriptApplicationSource, UriKind.Relative)
                         );

                        // https://developer.chrome.com/extensions/tabs#method-executeScript
                        // https://developer.chrome.com/extensions/tabs#type-InjectDetails
                        // https://developer.chrome.com/extensions/content_scripts#pi

                        // Content scripts execute in a special environment called an isolated world. 
                        // They have access to the DOM of the page they are injected into, but not to any JavaScript variables or 
                        // functions created by the page. It looks to each content script as if there is no other JavaScript executing
                        // on the page it is running on. The same is true in reverse: JavaScript running on the page cannot call any 
                        // functions or access any variables defined by content scripts.

                        var result = await tab.id.executeScript(
                            //new { file = url }
                            new { code }
                        );
                    };
                #endregion



                return;

            }

            // run it
            // save view-source
            // subst
            // test in chrome


            // 1999999999

            // X:\jsc.svn\examples\javascript\Test\TestShadowBody\TestShadowBody\Application.cs
            var s = new ShadowLayout().AttachTo(Native.shadow);

            // youtube
            //s.TopSideBar.style.zIndex = 19999999999;


            // forms shall use position fixed
            // to prevent overflow!?

            __Form.InternalHTMLTargetAttachToDocument =
                (that, yield) =>
                {
                    if (that.HTMLTarget.parentNode == null)
                    {
                        that.HTMLTarget.AttachTo(Native.shadow);
                    }

                    // animate!
                    yield(true);
                };

            s.TopSideBar.style.Opacity = 0.7;

            new FooUserControl { }.AttachControlTo(
                s.TopSideBar
            );
        }

    }
}
