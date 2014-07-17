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
using TestNavigatorServiceWorker;
using TestNavigatorServiceWorker.Design;
using TestNavigatorServiceWorker.HTML.Pages;

namespace TestNavigatorServiceWorker
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


            new IHTMLPre {


                // {{ userAgent = Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/37.0.2062.20 Safari/537.36, serviceWorker = undefined }}
                // {{ userAgent = Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/38.0.2094.0 Safari/537.36, serviceWorker = undefined }}

                // why is it unavalable?

                // chrome://flags/
                //Enable support for ServiceWorker background sync event. Mac, Windows, Linux, Chrome OS, Android
                //ServiceWorker background synchronization lets ServiceWorkers send messages and update resources even when the page is in the background. #enable-service-worker-sync
                // http://git.chromium.org/gitweb/?p=chromium/chromium.git;a=commitdiff;h=8d5622dc351b1dc2b8945c974f53135bece733fa

                // restart chrome?
                // https://github.com/slightlyoff/ServiceWorker/issues/223

                // this API is not there yet.
                // test later...

                new {
                        Native.window.navigator.userAgent,

                    Native.window.navigator.serviceWorker
                }

            }.AttachToDocument();


        }

    }
}
