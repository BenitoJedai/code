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
using TestServiceWorkerRegistrations;
using TestServiceWorkerRegistrations.Design;
using TestServiceWorkerRegistrations.HTML.Pages;

namespace TestServiceWorkerRegistrations
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

            // chrome://serviceworker-internals/
            // view-source:https://matthew-andrews.github.io/serviceworker-simple/

            new IHTMLPre {
                new {
                        Native.window.navigator.serviceWorker.installing,
                        Native.window.navigator.serviceWorker.waiting,
                        Native.window.navigator.serviceWorker.active,
                         Native.window.navigator.serviceWorker.controller
                }
            }.AttachToDocument();
            // {{ installing = null, waiting = null, active = null, controller = null }}


            // https://github.com/slightlyoff/ServiceWorker/blob/master/explainer.md


            new IHTMLButton { "register" }.AttachToDocument().onclick +=
                delegate
            {
                // The ServiceWorker itself is a bit of JavaScript that runs in a context that's very much like a shared worker.
                // how does this relate to history API? 

                new IHTMLPre { "navigator.serviceWorker.register... " }.AttachToDocument();

                // http://www.chromium.org/Home/chromium-security/prefer-secure-origins-for-powerful-new-features
                // Only secure origins are allowed. http://goo.gl/lq4gCo
                Native.window.navigator.serviceWorker.register("view-source#serviceWorker", null).then(
                        w =>
                            {
                                new IHTMLPre { "navigator.serviceWorker.register... " + new { w } }.AttachToDocument();

                                // did we just rerun ourself?
                                // like a Worker?
                                // do we know how to relaunch as a service worker?

                            }
                    );

            };
        }

    }
}
