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
using ChromeTabsExperiment;
using ChromeTabsExperiment.Design;
using ChromeTabsExperiment.HTML.Pages;
using chrome;
using System.Diagnostics;

namespace ChromeTabsExperiment
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
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_tabs = self_chrome.tabs;

            if (self_chrome_tabs != null)
            {
                //                ---------------------------
                //Extension error
                //---------------------------
                //Could not load extension from 'A:\'. Could not load options page '_generated_background_page.html'.
                //---------------------------
                //OK   
                //---------------------------


                Console.WriteLine("loading ChromeTabsExperiment...");

                chrome.Notification.DefaultTitle = "ChromeTabsExperiment";

                #region chrome.runtime

                // unavailable for extension
                //  chrome.app.runtime.Restarted +=
                //delegate
                //{
                //    new Notification
                //    {
                //        Message = "Restarted!"
                //    };
                //};


                chrome.runtime.Installed += delegate
                {
                    new Notification
                    {
                        Message = "Installed!"
                    };
                };

                chrome.runtime.Startup +=
                    delegate
                    {
                        new Notification
                        {
                            Message = "Startup!"
                        };
                    };


                var t = new Stopwatch();
                t.Start();

                chrome.runtime.Suspend +=
                    delegate
                    {
                        var n = new Notification
                        {
                            Message = "Suspend! " + new { t.ElapsedMilliseconds }
                        };

                        n.Clicked += delegate
                        {
                            runtime.reload();
                        };

                    };
                #endregion


                chrome.tabs.Created +=
                    tab =>
                    {
                        new chrome.Notification
                        {
                            Message = "chrome.tabs.Created " + new { tab.id, tab.url, tab.title }
                        };
                    };


                chrome.tabs.Updated +=
                    (i, x, tab) =>
                    {
                        new chrome.Notification
                        {
                            Message = "chrome.tabs.Created " + new { tab.id, tab.url, tab.title }
                        };
                    };


                new Notification
                {
                    Message = "extension!"
                };

                Console.WriteLine("loading ChromeTabsExperiment... done!");

                return;
            }

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
