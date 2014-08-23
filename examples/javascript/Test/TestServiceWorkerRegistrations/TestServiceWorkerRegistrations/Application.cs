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
            // X:\jsc.svn\examples\javascript\async\Test\TestWebCryptoAsync\TestWebCryptoAsync\Application.cs

            // X:\jsc.svn\examples\javascript\Test\TestNavigatorServiceWorker\TestNavigatorServiceWorker\Application.cs

            Console.WriteLine("enter Application");

            if (Native.window.navigator.serviceWorker == null)
            {
                new IHTMLPre {
                    @"chrome://flags/
                    Enable support for ServiceWorker background sync event. Mac, Windows, Linux, Chrome OS, Android. Relaunch!"

                }.AttachToDocument();

                return;
            }

            #region secure origin
            new IHTMLPre { new { Native.document.location.host } }.AttachToDocument();

            if (Native.document.location.host.TakeUntilOrEmpty(":") != "127.0.0.1")
            {
                new IHTMLAnchor
                {
                    href = "http://127.0.0.1:" + Native.document.location.host.SkipUntilOrEmpty(":"),
                    innerText = "open as secure origin!"
                }.AttachToDocument();

                return;
            }
            #endregion



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


                // at this point the service will be runing us again.
                // what will we do in the service?
                // jsc need to learn ssl. for chrome, clr, and android.
                // and chrome needs to lift the flag.
                // then lets continue looking at it.

                Native.window.navigator.serviceWorker.register("view-source#serviceWorker", null).then(
                        w =>
                            {
                                // http://www.w3.org/TR/service-workers/#service-worker-interface

                                new IHTMLPre { "navigator.serviceWorker.register... " + new { w.scope, w.active, w.installing, w.waiting } }.AttachToDocument();
                                // navigator.serviceWorker.register... {{ scope = http://127.0.0.1:23573/, active = null, installing = null, waiting = null }}

                                //new IHTMLPre { "navigator.serviceWorker.register... " + new {  } }.AttachToDocument();



                                // navigator.serviceWorker.register... {{ w = [object ServiceWorkerRegistration] }}

                                // did we just rerun ourself?
                                // like a Worker?
                                // do we know how to relaunch as a service worker?

                            }
                    );

            };
        }

    }
}
