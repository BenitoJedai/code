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
using TestServiceWorkerCache;
using TestServiceWorkerCache.Design;
using TestServiceWorkerCache.HTML.Pages;
using System.Diagnostics;

namespace TestServiceWorkerCache
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
            // X:\jsc.svn\examples\javascript\Test\TestCacheStorage\TestCacheStorage\Application.cs
            // X:\jsc.svn\examples\javascript\Test\TestServiceWorkerClient\TestServiceWorkerClient\Application.cs

            #region serviceworker
            // https://developer.chrome.com/extensions/event_pages

            // http://www.w3.org/TR/service-workers/#service-worker-interface
            // The lifetime of a service worker is tied to the execution lifetime of events, not references held by service worker clients to the ServiceWorker object. 

            if (Native.serviceworker != null)
            {
                // we are the service.


                var fetchCounter = 0;


                Native.serviceworker.onfetch += async e =>
                    {
                        // would we know which tab/client is doing the request?


                        // can we do async/worker threads and io here?
                        // refresh will cause a new fetch.

                        fetchCounter++;

                        var relative = e.request.url.SkipUntilIfAny(
                            Native.serviceworker.location.href.TakeUntilLastIfAny("/")
                            );

                        // https://developer.mozilla.org/en-US/docs/Web/API/FetchEvent

                        if (relative == "/jsc")
                        {
                            // does this obsolete the history api effort?

                            //clients.WithEachIndex(
                            //    (c, index) =>
                            //        c.postMessage("Native.serviceworker.onfetch respondWith Response" + new { relative }, null)
                            //);

                            // http://www.i-programmer.info/news/87-web-development/7494-serviceworkers-are-coming.html
                            //e.respondWith(new Response("the first string response. next time async?"));
                            // DOMException: Failed to execute 'respondWith' on 'FetchEvent': The fetch event has already been responded to.

                            // Status Code:200 OK (from ServiceWorker)
                            // if the server goes down this link will still reload from service worker.
                            e.respondWith(
                                // is this where we get to send back binary/script/images from cache?
                                // can we unpack in the background?
                                new Response(
                                    //new { body = "the first string response. next time async?" }
                                    "the first string response. next time async? or html? or xhtml?"
                                )
                            );

                            // {{ body = the first string response. next time async? }}
                        }


                        // is that all we know whch client the message is from?
                        var clients = await Native.serviceworker.clients.getAll().AsTask();

                        // window.onmessage > {{ source = null, data = Native.serviceworker.onfetch {{ index = 0, Length = 1, 
                        // href = https://192.168.43.252:22718/view-source, 
                        // url = https://192.168.43.252:22718/jsc, 
                        // relative = /https://192.168.43.252:22718/jsc 
                        // }} }}




                        clients.WithEachIndex(
                            (c, index) =>
                                c.postMessage("Native.serviceworker.onfetch " +
                                new
                        {

                            index,
                            clients.Length,

                            Native.serviceworker.location.href,

                            e.request.url,

                            relative,

                            //e.request.context,
                            //e.request.,

                            //client = new { e.client }

                            //c.focused,
                            //c.visibilityState,
                            //c.frameType
                        }, null)
                        );





                    };


                return;
            }
            #endregion

            new IHTMLAnchor { href = "chrome://serviceworker-internals", innerText = "chrome://serviceworker-internals" }.AttachToDocument();
            new IHTMLPre { "Opens the DevTools window for ServiceWorker on start for debugging." }.AttachToDocument();

            new IHTMLBreak { }.AttachToDocument();

            #region register
            if (Native.window.navigator.serviceWorker.controller == null)
            {
                Native.css.style.borderTop = "1em solid red";

                // we need to register!

                new { }.With(async delegate
                {
                    var sw = Stopwatch.StartNew();

                    new IHTMLPre { "service register!" }.AttachToDocument();

                    // should jsc do this automatically?
                    // how many test cases should be made to understand it?
                    var activated = await Native.window.navigator.serviceWorker.activate();

                    //if (registration.installing != null)
                    //{
                    //    var i = registration.installing;

                    //    i.onstatechange += delegate
                    //    {
                    //        if (i.state != "activated")
                    //            return;

                    new IHTMLPre { "service activated! " + new { sw.ElapsedMilliseconds } }.AttachToDocument();
                    // service activated! {{ ElapsedMilliseconds = 1033 }}

                    //    };
                    //}

                    Native.css.style.borderTop = "1em solid yellow";

                    new IHTMLButton { "reload to become controlled client " }.AttachToDocument().onclick +=
                         delegate
                    {
                        // is this something jsc app should do automatically?
                        Native.document.location.reload();

                    };
                }
                );



                return;
            }
            else
            {
                Native.css.style.borderTop = "1em solid green";

                new IHTMLPre { "service as controller!" }.AttachToDocument();
            }
            #endregion

            // we are good to go..


            Native.window.onmessage += m =>
            {
                // is this where clients messages are received?
                // window.onmessage > {{ data = service worker knows there are multiple tabs now {{ index = 1 }} }}


                // would the service worker be able to tell us something of interesting?

                new IHTMLPre { "window.onmessage > " + new { m.source, m.data } }.AttachToDocument();
            };

            new IHTMLBreak { }.AttachToDocument();


            new IHTMLAnchor { href = "/jsc", innerText = "/jsc" }.AttachToDocument();
            new IHTMLBreak { }.AttachToDocument();


            new IHTMLButton { "open a /jsc as iframe" }.AttachToDocument().onclick += delegate
            {
                //new IHTMLIFrame { }.AttachToDocument();

                // window.onmessage > {{ source = null, data = Native.serviceworker.onfetch {{ index = 0, Length = 1, url = https://192.168.43.252:13005/jsc, context = null, client = {{ client = null }} }} }}
                new IHTMLIFrame { src = "/jsc" }.AttachToDocument();

            };

            new IHTMLBreak { }.AttachToDocument();

            new IHTMLButton { "make a WebMethod2 fetch" }.AttachToDocument().onclick += async delegate
            {
                // window.onmessage > {{ source = null, data = Native.serviceworker.onfetch {{ index = 0, Length = 1, url = https://192.168.43.252:19791/xml/WebMethod2, context = null, client = {{ client = null }} }} }}

                this.WebMethod2("", null);

            };


        }

    }
}
