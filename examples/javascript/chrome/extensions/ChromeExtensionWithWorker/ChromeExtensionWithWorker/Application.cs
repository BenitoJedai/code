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
using ChromeExtensionWithWorker;
using ChromeExtensionWithWorker.Design;
using ChromeExtensionWithWorker.HTML.Pages;
using chrome;
using System.Net;

namespace ChromeExtensionWithWorker
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
            // jsc does not yet pre package chrome apps nor extensions
            // thus we do it manually.
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/20140705/20140712
            // X:\jsc.svn\examples\javascript\chrome\extensions\ChromeTabsExperiment\ChromeTabsExperiment\Application.cs

            // A single extension can override only one page. For example, an extension can't override both the Bookmark Manager and History pages.


            // we could provide special API for scriptcorelib runtime on the tab being loaded
            // http://blog.chromium.org/

            // what else can we override besides options?
            // "options_page": "Application.htm",
            // https://code.google.com/p/chromium/issues/detail?id=171752
            // https://developer.chrome.com/extensions/options

            // can we provide an API about available android devices?


            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_tabs = self_chrome.tabs;

            if (self_chrome_tabs != null)
            {
                // jsc, add chrome nuget
                chrome.runtime.Installed += delegate
                {
                    // our API does not have a Show
                    new chrome.Notification
                    {
                        Message = "Extension Installed!"
                    };
                };

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


                        // inject?


                        var n = new Notification
                        {
                            Message = "Updated! " + new { tab.id, tab.url }
                        };


                        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Net\WebClient.cs

                        Console.WriteLine("before DownloadStringTaskAsync");
                        var code = await new WebClient().DownloadStringTaskAsync(
                             new Uri(Worker.ScriptApplicationSource, UriKind.Relative)
                        );

                        // show our icon?
                        tab.id.show();

                        // when will roslyn learn to expose events as async?
                        Console.WriteLine("before tab.pageAction.async.onclick");
                        await tab.pageAction.async.onclick;

                        tab.id.hide();


                        var nn = new Notification
                        {
                            Message = "Clicked " + new { tab.id, tab.url }
                        };


                        Console.WriteLine("before insertCSS");

                        // https://developer.chrome.com/extensions/tabs#type-Tab
                        // http://stackoverflow.com/questions/9795058/how-to-run-chrome-tabs-insertcss-from-the-background-page-on-each-page
                        // chrome::
                        tab.id.insertCSS(
                            new
                            {
                                // .css do we have a CSS parser/builder available yet?

                                //code = "body { border: 1em solid red; }"
                                code = "body { border: 1em solid yellow; }"
                            },
                            null
                        );

                        // await tab.id.Run(async delegate {});
                        //};





                    };
            }
        }

    }
}
