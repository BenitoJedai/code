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
using TestServiceWorkerScreens;
using TestServiceWorkerScreens.Design;
using TestServiceWorkerScreens.HTML.Pages;
using System.Diagnostics;
using ScriptCoreLib.Query;


namespace TestServiceWorkerScreens
{
    public class ClientData
    {
        // postMessage forgets type info
        // atleast now we can cast and get the fields.

        // each tab needs its own identity. shall we use random until .client or .source becomes available?
        public int identity;

        public int screen_width;
        public int screen_height;
        public int window_screenLeft;
        public int window_screenTop;
        public int window_Width;
        public int window_Height;
    }

    static class __reinventingthewheel
    {
        // X:\jsc.svn\core\ScriptCoreLib.Query\Shared\Lambda\Lambda.ForEach.cs

        public static ScriptCoreLib.Shared.Lambda.BindingListWithEvents<T> AsEmptyListWithEvents<T>(this T template)
        {
            return new ScriptCoreLib.Shared.Lambda.BindingListWithEvents<T>(
                new System.ComponentModel.BindingList<T>()
            );

        }
    }

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
            // based on 
            // X:\jsc.svn\examples\javascript\Test\TestServiceWorkerFetchHTML\TestServiceWorkerFetchHTML\Application.cs
            // X:\jsc.svn\examples\javascript\Test\TestIScreen\TestIScreen\Application.cs

            // what about BroadcastChannel ?

            #region serviceworker
            if (Native.serviceworker != null)
            {
                // since chrome does not yet tell us who the clients
                //are we need to collect the intel in user code?

                var clients = new { e = default(MessageEvent), edata = default(ClientData) }.AsEmptyListWithEvents();

                Native.serviceworker.onmessage += e =>
                {
                    var edata = (ClientData)e.data;


                    // how could the as operator know if object is of a type
                    //var edata = e.data as ClientData;

                    // 48686ms serviceworker.onmessage {{ source = null, data = [object Object] }}

                    // tab is telling us something.

                    Console.WriteLine(
                        "serviceworker.onmessage " + new
                    {
                        edata.screen_width,
                        edata.screen_height

                    }
                    );

                    //e.postMessage("got it! " + new
                    //{
                    //    edata.screen_width,
                    //    edata.screen_height
                    //}
                    //);

                    // echo back what the tab told us
                    e.postMessage(edata);

                    // late to the party?
                    // let the newby know about the others.

                    clients.Source.WithEach(
                        n =>
                        {
                            e.postMessage(n.edata);
                        }
                    );

                    clients.Source.Add(new { e, edata });

                    clients.Added +=
                        (n, nindex) =>
                        {
                            Console.WriteLine(
                                "serviceworker.onmessage  clients.Added " + new
                            {
                                nindex
                            }
                            );


                            // report that somebody joined?
                            e.postMessage(n.edata);
                        };

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


            // should we even have a special dispatcher we could do Task.Run on?

            // switchto service worker?
            // switch to tab?
            //System.Windows.Threading.Dispatcher.PushFrame

            // we are about to switch over to service worker.
            var data = new ClientData
            {
                identity = new Random().Next(),


                screen_width = Native.screen.width,
                screen_height = Native.screen.height,

                //Native.window.aspect,

                window_Width = Native.window.Width,
                window_Height = Native.window.Height,

                // where is this window on current screen?
                //(Native.window as dynamic).offsetLeft,
                //(Native.window as dynamic).offsetTop,

                window_screenLeft = (Native.window as dynamic).screenLeft,
                window_screenTop = (Native.window as dynamic).screenTop,
            };

            new IHTMLPre { "lets tell the service, we have opened a new tab. " }.AttachToDocument();

            Native.window.navigator.serviceWorker.controller.postMessage(
                data,

                // data updated o the other side.
                // lets decode.
                m =>
                {
                    var mdata = (ClientData)m.data;

                    // > {{ data = reply from serviceworker: {{ data = hello from UI, controlledClients = 1 }} }}
                    // > {{ data = reply from serviceworker: {{ data = hello from UI, controlledClients = 2 }} }}
                    new IHTMLPre { "> " + new
                        {

                                                  mdata.identity,

                                                  mdata.window_screenLeft,
                                                  mdata.window_screenTop,

                        mdata.screen_width,
                        mdata.screen_height

                        } }.AttachToDocument();

                    //> { { identity = 256648414, window_screenLeft = 548, window_screenTop = 177, screen_width = 1920, screen_height = 1080 } }
                    //> { { identity = 2132896978, window_screenLeft = -1420, window_screenTop = 271, screen_width = 1600, screen_height = 900 } }

                    // if identity is what we already used, just update it?
            }
           );
        }

    }
}
