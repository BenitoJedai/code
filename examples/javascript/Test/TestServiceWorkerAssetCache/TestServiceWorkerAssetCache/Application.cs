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
using TestServiceWorkerAssetCache;
using TestServiceWorkerAssetCache.Design;
using TestServiceWorkerAssetCache.HTML.Pages;
using System.Diagnostics;

namespace TestServiceWorkerAssetCache
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        //        Application Cache Downloading event
        //        2014-12-30 12:08:53.478192.168.43.252/:1 Application Cache Progress event (0 of 3) https://192.168.43.252:22193/assets/TestServiceWorkerAssetCache/App.css
        //2014-12-30 12:08:53.492192.168.43.252/:1 Application Cache Progress event (1 of 3) https://192.168.43.252:22193/view-source
        //2014-12-30 12:08:53.816192.168.43.252/:1 Application Cache Progress event (2 of 3) https://192.168.43.252:22193/
        //2014-12-30 12:08:54.484192.168.43.252/:1 Application Cache Error event: Failed to commit new cache to storage, would exceed quota

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // https://github.com/slightlyoff/ServiceWorker/issues/73


            // X:\jsc.svn\examples\javascript\Test\TestServiceWorkerVisualizedScreens\TestServiceWorkerVisualizedScreens\Application.cs
            // X:\jsc.svn\examples\javascript\Test\TestServiceWorkerFetchHTML\TestServiceWorkerFetchHTML\Application.cs

            // https://code.google.com/p/chromium/issues/detail?id=403076
            // http://stackoverflow.com/questions/23294792/how-to-ensure-secure-socket-communication-using-chrome-packaged-app
            // what about websql/rss feeds?
            // what about appengine/cloudlfare/chrome app
            // what does onupdate found mean?
            // could servcice worker
            // reload binary client side components
            // on their own release cycle?

            // would it work with flash?

            #region serviceworker
            // https://developer.chrome.com/extensions/event_pages

            // http://www.w3.org/TR/service-workers/#service-worker-interface
            // The lifetime of a service worker is tied to the execution lifetime of events, not references held by service worker clients to the ServiceWorker object. 

            if (Native.serviceworker != null)
            {
                // we are the service.


                // https://github.com/slightlyoff/ServiceWorker/blob/master/explainer.md

                //5395ms fetch {{ relative = / }}
                //2014-12-30 13:16:00.104view-source:43563 5589ms fetch {{ relative = /assets/TestServiceWorkerAssetCache/App.css }}
                //2014-12-30 13:16:00.108view-source:43563 5593ms fetch {{ relative = /view-source }}

                Native.serviceworker.oninstall += async delegate
                {

                    //var c = await Native.serviceworker.caches.open("cacheName").AsTask();

                    // http://blog.wanderview.com/blog/2014/12/08/implementing-the-serviceworker-cache-api-in-gecko/


                    //c.addAll(
                    //    "/",
                    //    "/jsc"
                    //);


                };


                var fetchCounter = 0;

                // pre open it, yet do not await for it
                var ca = Native.serviceworker.caches.open("cacheName").AsTask();


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

                        //if (relative == "/jsc")
                        {
                            // can we now do a multiscreen app?
                            // where window/ is synced via service worker?


                            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Xml\Linq\XElement.cs
                            // does this obsolete the history api effort?

                            //clients.WithEachIndex(
                            //    (c, index) =>
                            //        c.postMessage("Native.serviceworker.onfetch respondWith Response" + new { relative }, null)
                            //);

                            // http://www.i-programmer.info/news/87-web-development/7494-serviceworkers-are-coming.html
                            //e.respondWith(new Response("the first string response. next time async?"));
                            // DOMException: Failed to execute 'respondWith' on 'FetchEvent': The fetch event has already been responded to.


                            // will this work from service worker?
                            // // ScriptCoreLib.JavaScript.BCLImplementation.System.Xml.Linq.__XDocument.get_Root
                            // no not yet.
                            //var doc = XElement.Parse(SpecialContentFromServiceWorkerSource.Text);

                            // Status Code:200 OK (from ServiceWorker)
                            // if the server goes down this link will still reload from service worker.

                            // void waitUntil(Promise<any> f);

                            // intercept
                            // wont work for external fetches?

                            // who wont it work for web methods? missing headers?
                            //var v = Native.serviceworker.fetch(relative);

                            //window.onmessage > {{ source = null, data = Native.serviceworker.onfetch {{ index = 0, Length = 1, relative = /jsc }} }}
                            //window.onmessage > {{ source = null, data = Native.serviceworker.onfetch {{ index = 0, Length = 2, relative = /assets/ScriptCoreLib/jsc.png }} }}
                            //window.onmessage > {{ source = null, data = Native.serviceworker.onfetch {{ index = 0, Length = 2, relative = http://i.msdn.microsoft.com/deshae98.pubmethod(en-us,VS.90).gif }} }}

                            // Mixed Content: The page at 'https://192.168.43.252:22193/view-source' was loaded over HTTPS, but requested an insecure resource 'http://i.msdn.microsoft.com/dynimg/IC477625.png'. This content should also be served over HTTPS.

                            // if we do an await here
                            // the sync context should extend the event? how many times?


                            e.respondWith(
                                // will it work for nexus7?
                                Native.serviceworker.caches.match(e.request).then(
                                    response =>
                                    {
                                        // https://github.com/slightlyoff/ServiceWorker/blob/master/explainer.md

                                        if (response == null)
                                        {
                                            // https://github.com/slightlyoff/ServiceWorker/issues/52

                                            // TypeError: Request method 'POST' is unsupported

                                            if (e.request.method == "POST")
                                            {

                                                Console.WriteLine("fetch POST " + new { relative });
                                                return Native.serviceworker.fetch(e.request);
                                            }

                                            Console.WriteLine("fetch " + new { relative });
                                            var v = Native.serviceworker.fetch(e.request);

                                            //v.AsTask().ContinueWithResult(
                                            //    async r =>
                                            //    {

                                            //        // we now have the result

                                            //        var c = await Native.serviceworker.caches.open("cacheName").AsTask();


                                            //        // <exception>: TypeError: Response body is already used
                                            //        c.put(e.request, r);
                                            //    }
                                            //);



                                            return v.then(
                                                vr =>
                                                {
                                                    // we have to put it into cache first?

                                                    Console.WriteLine("add to cache " + new { relative });

                                                    // will we cache the web requests too?
                                                    return ca.Result.put(e.request, vr).then(
                                                        nop =>
                                                        {
                                                            Console.WriteLine("add to cache done " + new { relative });

                                                            return Native.serviceworker.caches.match(e.request);

                                                            // GET https://192.168.43.252:23301/ net::ERR_FAILED

                                                            //return vr;
                                                        }
                                                    );

                                                },

                                                onError:

                                                vr =>
                                                {
                                                    // 40942ms fetch error {{ relative = /jsc }}

                                                    // server offline?
                                                    Console.WriteLine("fetch error " + new { relative });

                                                    return vr;
                                                }
                                            );
                                        }

                                        Console.WriteLine("in cache " + new { relative });
                                        return response;
                                    }
                                )
                            );

                            // Application Cache Error event: Manifest fetch failed (6) https://192.168.43.252:12296/cache-manifest

                            // X:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Ultra\WebService\InternalGlobalExtensions.InternalApplication_BeginRequest.cs




                            // is this where we get to send back binary/script/images from cache?
                            // can we unpack in the background?
                            //    new Response(
                            //        //new { body = "the first string response. next time async?" }
                            //        "the first <b>html response</b>. next time async? or html? or xhtml?",

                            //        // jsc asset library magic. can we access XLinq here?
                            //        //SpecialContentFromServiceWorkerSource.Text,

                            //        new
                            //{
                            //    headers = new string[][] {
                            //                    new [] { "Content-Type", "text/html" }
                            //                }
                            //}
                            //    )

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
                            relative,
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


                    new IHTMLPre { "service activated! " + new { sw.ElapsedMilliseconds } }.AttachToDocument();

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

                //  a[0].y.vyAABlgwczGFxMM_a_a0rwGA(ZgAABjp_b2TiQqXBrpoY4_bQ(c.FgAABg1u_az24ehb3UxBCmQ('obj')));
                // we do not support null callbacks?

                //this.WebMethod2("", null);
                this.WebMethod2("", delegate { });

            };
        }

    }
}
