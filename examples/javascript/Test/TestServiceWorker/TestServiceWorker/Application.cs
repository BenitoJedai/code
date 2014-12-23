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
using TestServiceWorker;
using TestServiceWorker.Design;
using TestServiceWorker.HTML.Pages;

namespace TestServiceWorker
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
            //   // ScriptCoreLib.JavaScript.Extensions.INodeExtensionsWithXLinq.InternalReplaceAll
            //ScriptCoreLib.JavaScript.Extensions.INodeExtensionsWithXLinq.InternalReplaceAll


            // install
            // fetch
            // sync

            if (Native.serviceworker != null)
            {
                Console.WriteLine("we seem to run as a background page, service worker ");

                Native.serviceworker.addEventListener("install",
                    e =>
                    {
                        Console.WriteLine("oninstall");
                    }
                );

                // http://jakearchibald.com/2014/using-serviceworker-today/

                Native.serviceworker.addEventListener("activate",
                   e =>
                    {
                        Console.WriteLine("onactivate");
                    }
               );

                Native.serviceworker.addEventListener("beforeevicted",
                    e =>
                    {
                        Console.WriteLine("beforeevicted");
                    }
                );


                Native.serviceworker.addEventListener("evicted",
                  e =>
                    {
                        Console.WriteLine("evicted");
                    }
              );

                Native.serviceworker.onmessage += e =>
                {
                    Console.WriteLine("onmessage " + new
                    {
                        e.data
                    });
                };


                var fetchCounter = 0;

                // https://fetch.spec.whatwg.org/#request
                // FetchEvent
                //Native.serviceworker.addEventListener("fetch",
                //    e =>
                //    {
                //        // can we do async/worker threads and io here?
                //        // refresh will cause a new fetch.

                //        fetchCounter++;

                //        Console.WriteLine("fetch " + new { fetchCounter });
                //    }
                //);


                Native.serviceworker.onfetch +=
                    e =>
                    {
                        // can we do async/worker threads and io here?
                        // refresh will cause a new fetch.

                        fetchCounter++;

                        Console.WriteLine("fetch " + new
                        {
                            fetchCounter,

                            e.isReload,

                            request = new { e.request.url },
                            //client = new { e.client.url }
                            //client = new { e.client }
                        });
                    };


                return;
            }

            // responses 

            // Worker pid:10244
            https://192.168.43.252:16034/view-source
            // https://jakearchibald.github.io/isserviceworkerready/

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201412/20141223
            //Native.serviceworker 
            // whatif
            // http://www.chromium.org/blink/serviceworker/service-worker-faq

            new IHTMLPre {
                new {
                        Native.window.navigator.serviceWorker.installing,
                        Native.window.navigator.serviceWorker.waiting,
                        Native.window.navigator.serviceWorker.active,
                         Native.window.navigator.serviceWorker.controller
                }
            }.AttachToDocument();


            new IHTMLButton { "controller post" }.AttachToDocument().onclick +=
                delegate
            {
                Native.window.navigator.serviceWorker.controller.postMessage("hello from UI");

            };
            new IHTMLBreak { }.AttachToDocument();

            //  chrome://serviceworker-internals .
            new IHTMLAnchor { href = "chrome://inspect/#service-workers", innerText = "chrome://inspect/#service-workers" }.AttachToDocument();
            new IHTMLBreak { }.AttachToDocument();
            new IHTMLAnchor { href = "chrome://serviceworker-internals", innerText = "chrome://serviceworker-internals" }.AttachToDocument();
            new IHTMLBreak { }.AttachToDocument();


            new IHTMLButton { "register" }.AttachToDocument().onclick += delegate
            {
                new IHTMLPre { "navigator.serviceWorker.register... " }.AttachToDocument();

                // 8ms serviceworker! { href = https://192.168.43.252:10049/view-source }

                // once registered
                // can we take over the cache and sub pages?
                Native.window.navigator.serviceWorker.register("view-source#serviceWorker", null).then(
                    w =>
                    {
                        // https://www.youtube.com/watch?v=SmZ9XcTpMS4
                        // http://www.w3.org/TR/service-workers/#service-worker-interface

                        new IHTMLPre { "navigator.serviceWorker.register... " + new {

                            w.scope,

                                                                                        // http://www.w3.org/TR/service-workers/
                                                                                        w.active, w.installing, w.waiting } }.AttachToDocument();


                        //w.installing.rea
                        // how long will it be installing?

                        // this wont help
                        new IHTMLButton { "check again" }.AttachToDocument().onclick += delegate
                        {
                            new IHTMLPre {
                                    new {
                                            Native.window.navigator.serviceWorker.installing,
                                            Native.window.navigator.serviceWorker.waiting,
                                            Native.window.navigator.serviceWorker.active,
                                             Native.window.navigator.serviceWorker.controller
                                    }
                                }.AttachToDocument();
                        };

                    }
                );


            };


        }

    }
}
