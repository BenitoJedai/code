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
            // install
            // fetch
            // sync


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

            new IHTMLAnchor { href = "chrome://inspect/#service-workers", innerText = "chrome://inspect/#service-workers" }.AttachToDocument();

            new IHTMLBreak { }.AttachToDocument();


            new IHTMLButton { "register" }.AttachToDocument().onclick += delegate
            {
                new IHTMLPre { "navigator.serviceWorker.register... " }.AttachToDocument();

                // 8ms serviceworker! { href = https://192.168.43.252:10049/view-source }

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
