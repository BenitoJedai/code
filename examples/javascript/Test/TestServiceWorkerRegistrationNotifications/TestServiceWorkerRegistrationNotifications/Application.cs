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
using TestServiceWorkerRegistrationNotifications;
using TestServiceWorkerRegistrationNotifications.Design;
using TestServiceWorkerRegistrationNotifications.HTML.Pages;
using System.Diagnostics;

namespace TestServiceWorkerRegistrationNotifications
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
            // X:\jsc.svn\examples\javascript\test\TestNotification\TestNotification\Application.cs
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

                Native.serviceworker.onfetch +=
                    e =>
                    {
                        // https://github.com/slightlyoff/ServiceWorker/issues/421
                        //  ServiceWorkerRegistration is not defined

                        ServiceWorkerRegistration.showNotification("hello world", new { });

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
                    //var activated = await Native.window.navigator.serviceWorker.activate();
                    var r = await Native.window.navigator.serviceWorker.activate();

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
