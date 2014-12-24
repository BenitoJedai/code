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
using TestServiceWorkerClient;
using TestServiceWorkerClient.Design;
using TestServiceWorkerClient.HTML.Pages;

namespace TestServiceWorkerClient
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
            #region serviceworker
            // https://developer.chrome.com/extensions/event_pages

            // http://www.w3.org/TR/service-workers/#service-worker-interface
            // The lifetime of a service worker is tied to the execution lifetime of events, not references held by service worker clients to the ServiceWorker object. 

            if (Native.serviceworker != null)
            {
                // were we just stopped and restarted? no events to run?

                Console.WriteLine("we seem to run as a background page, service worker ");


                new { }.With(
                    async delegate
                {
                    // what other configuration can we inherit from the first launch?
                    var clients = await Native.serviceworker.clients.getAll().AsTask();

                    Console.WriteLine("> " + new
                    {
                        controlledClients = clients.Length
                    }
                        );
                }
                );



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

                //
                Native.serviceworker.addEventListener("evicted",
                  e =>
                    {
                        Console.WriteLine("evicted");
                    }
              );

                // when is this useful?
                Native.serviceworker.addEventListener("sync",
                     e =>
                                {
                                    Console.WriteLine("sync");
                                }
                 );


                //                35ms oninstall
                //2014-12-24 13:57:24.833view-source:43453 42ms onmessage {{ data = hi installing! }}
                //2014-12-24 13:57:24.833view-source:43453 42ms onactivate
                //2014-12-24 13:57:24.834view-source:43453 43ms onmessage {{ data = hi installing statechange! {{ state = installed }} }}
                //2014-12-24 13:57:24.834view-source:43453 43ms onmessage {{ data = hi installing statechange! {{ state = activating }} }}
                //2014-12-24 13:57:24.835view-source:43453 44ms onmessage {{ data = hi installing statechange! {{ state = activated }} }}

                // yes we are getting a message.

                Native.serviceworker.onmessage += async e =>
                {

                    // is that all we know whch client the message is from?
                    // controled
                    var clients = await Native.serviceworker.clients.getAll().AsTask();


                    Console.WriteLine("onmessage " + new
                    {
                        e.data,
                        controlledClients = clients.Length
                    });


                    e.postMessage("reply from serviceworker: " + new { e.data, controlledClients = clients.Length });
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

                // https://chromium.googlesource.com/chromium/blink.git/+/master/Source/modules/serviceworkers/FetchEvent.h
                // https://www.w3.org/Bugs/Public/show_bug.cgi?id=26533
                // https://github.com/slightlyoff/ServiceWorker/issues/575
                Native.serviceworker.onfetch +=
                    async e =>
                    {
                        // would we know which tab/client is doing the request?


                        // can we do async/worker threads and io here?
                        // refresh will cause a new fetch.

                        fetchCounter++;

                        // is that all we know whch client the message is from?
                        var clients = await Native.serviceworker.clients.getAll().AsTask();

                        Console.WriteLine("fetch " + new
                        {
                            fetchCounter,

                            e.isReload,

                            request = new
                            {
                                e.request.url,
                                e.request.context
                            },
                            //client = new { e.client.url }

                            // ??

                            controlledClients = clients.Length,

                            client = new { e.client }
                        });
                    };


                return;
            }
            #endregion



            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201412/20141224

            // X:\jsc.svn\examples\javascript\Test\TestServiceWorker\TestServiceWorker\Application.cs
            new IHTMLPre {
                new {
                    Native.window.navigator.serviceWorker.installing,
                    Native.window.navigator.serviceWorker.waiting,
                    Native.window.navigator.serviceWorker.active,

                        // will report after register and refresh
                        Native.window.navigator.serviceWorker.controller
    }
}.AttachToDocument();

            new IHTMLBreak { }.AttachToDocument();
            new IHTMLAnchor { href = "chrome://inspect/#service-workers", innerText = "chrome://inspect/#service-workers" }.AttachToDocument();
            new IHTMLBreak { }.AttachToDocument();
            new IHTMLAnchor { href = "chrome://serviceworker-internals", innerText = "chrome://serviceworker-internals" }.AttachToDocument();
            new IHTMLPre { "Opens the DevTools window for ServiceWorker on start for debugging." }.AttachToDocument();

            new IHTMLBreak { }.AttachToDocument();



            /// should jsc app do this

            #region register
            new IHTMLButton { "register for the first time, to be then reloaded" }.AttachToDocument().onclick += async delegate
            {
                new IHTMLPre { "navigator.serviceWorker.register... (Opening Developer Tools Source at the break point?)" }.AttachToDocument();

                // 8ms serviceworker! { href = https://192.168.43.252:10049/view-source }

                // once registered
                // can we take over the cache and sub pages?

                // register the running app to be the service worker.
                var registration = await Native.window.navigator.serviceWorker.register();

                new IHTMLPre { "navigator.serviceWorker.register... " + new {
                    registration.scope,
                                                                                // http://www.w3.org/TR/service-workers/
                                                                                registration.active,
                    registration.installing,
                    registration.waiting }
                }.AttachToDocument();

                #region installing
                if (registration.installing != null)
                {
                    //registration.installing.addEventListener(

                    new IHTMLPre { "navigator.serviceWorker.register... installing " +
                        new
                        {

                            registration.installing.state,
                        }
                     }.AttachToDocument();



                    var i = registration.installing;

                    i.postMessage("hi installing!");

                    i.addEventListener("statechange",
                        e =>
                        {
                            // The state attribute of the ServiceWorker object is changed.

                            new IHTMLPre { "navigator.serviceWorker.register... installing statechange " + new
                                {
                                    i.state,

                                      registration.scope,
                                    // http://www.w3.org/TR/service-workers/
                                    registration.active,
                                    registration.installing,
                                    registration.waiting
                                }
                             }.AttachToDocument();

                            i.postMessage("hi installing statechange! " + new { i.state });


                            if (i.state == "activated")
                            {
                                i.postMessage("UI has activated itself as a service worker!",
                                    m =>
                                    {
                                        // navigator.serviceWorker.register... installing... activated {{ data = reply from serviceworker: {{ data = UI has activated itself as a service worker!, clients = 0 }} }}

                                        // um, activating a service worker, it cant see us?

                                        new IHTMLPre { "> " +
                                            new
                                            {
                                                m.data
                                            }
                                         }.AttachToDocument();

                                        // do we have to reload to become controlled?

                                        new IHTMLButton { "reload to become controlled client " }.AttachToDocument().onclick +=
                                        delegate
                                        {
                                            // is this something jsc app should do automatically?
                                            Native.document.location.reload();

                                        };
                                    }
                                );
                            }
                        }
                    );

                }
                #endregion



                //w.installing.rea
                // how long will it be installing?

                // this wont help
                #region check again
                new IHTMLButton { "check again" }.AttachToDocument().onclick += delegate
                {
                    // why wont it report the status?
                    new IHTMLPre {
                        new {
                                Native.window.navigator.serviceWorker.installing,
                                Native.window.navigator.serviceWorker.waiting,
                                Native.window.navigator.serviceWorker.active,
                                    Native.window.navigator.serviceWorker.controller
                        }
                    }.AttachToDocument();
                    new IHTMLBreak { }.AttachToDocument();

                    // the below actually reports a change
                    // navigator.serviceWorker.register... {{ scope = https://192.168.43.252:10865/, active = [object ServiceWorker], installing = null, waiting = null }}

                    new IHTMLPre { "navigator.serviceWorker.register... " + new {
                        registration.scope,
                                                                                    // http://www.w3.org/TR/service-workers/
                                                                                    registration.active,
                                                                                    registration.installing,
                                                                                    registration.waiting }
                    }.AttachToDocument();

                    //navigator.serviceWorker.register... installing statechange {{ state = installed, scope = https://192.168.43.252:19966/, active = null, installing = null, waiting = [object ServiceWorker] }}
                    //navigator.serviceWorker.register... installing statechange {{ state = activating, scope = https://192.168.43.252:19966/, active = [object ServiceWorker], installing = null, waiting = null }}
                    //navigator.serviceWorker.register... installing statechange {{ state = activated, scope = https://192.168.43.252:19966/, active = [object ServiceWorker], installing = null, waiting = null }}
                    //{{ installing = null, waiting = null, active = null, controller = null }}

                    //navigator.serviceWorker.register... {{ scope = https://192.168.43.252:19966/, active = [object ServiceWorker], installing = null, waiting = null }}
                };
                #endregion

            };
            #endregion

            if (Native.window.navigator.serviceWorker.controller != null)
            {
                new IHTMLPre { "Seems like, we were already installed as a service worker. lets notify." }.AttachToDocument();

                // navigator.serviceWorker.register... installing... activated {{ data = reply from serviceworker: {{ data = UI has activated itself as a service worker! }} }}



                Native.window.navigator.serviceWorker.controller.onmessage +=
                                     m =>
                     {
                         // would the service worker be able to tell us something of interesting?

                         new IHTMLPre { "serviceWorker.controller.onmessage > " + new { m.data } }.AttachToDocument();
                     };


                Native.window.navigator.serviceWorker.controller.postMessage("hello from UI",
                    m =>
                    {
                        new IHTMLPre { "> " + new { m.data } }.AttachToDocument();
                    }
                );
            }
        }

    }
}
