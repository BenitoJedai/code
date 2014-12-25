using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using SharedWorkerExperiment;
using SharedWorkerExperiment.Design;
using SharedWorkerExperiment.HTML.Pages;
using System.Diagnostics;
using System.Collections.Generic;

namespace SharedWorkerExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // what about service worker?

            // http://stackoverflow.com/questions/6778360/whats-the-difference-between-shared-worker-and-worker-in-html5
            //http://stackoverflow.com/questions/9336774/do-shared-web-workers-persist-across-a-single-page-reload-link-navigation

            Native.window.With(
                window =>
                {
                    new IHTMLButton { innerText = "open" }.AttachToDocument().onclick +=
                        delegate
                        {
                            Console.WriteLine("opening SharedWorker");

                            // cached. done once?
                            var s = new SharedWorker("view-source", "foo");
                            //var s = new SharedWorker("view-source");

                            s.port.onmessage +=
                                e =>
                                {
                                    // onmessage: { data = reply { name = foo, Elapsed = 0.00:00:06, data = hello! } }

                                    Console.WriteLine("onmessage: " + new { e.data });
                                };

                            s.port.start();

                            new IHTMLButton { innerText = "post" }.AttachToDocument().onclick +=
                                delegate
                                {
                                    s.port.postMessage("hello!");
                                };

                        };
                }
            );



            // http://tutorials.jenkov.com/html5/web-workers.html

            Native.sharedworker.With(
                sharedworker =>
                {
                    var random = new Random().Next();


                    var st = new Stopwatch();
                    st.Start();


                    var ports = new List<MessagePort>();
                    var messages = 0;

                    // this might happen once?
                    sharedworker.onconnect +=
                        e =>
                        {

                            e.ports.WithEach(
                                port =>
                                {
                                    port.start();

                                    ports.AddDistinct(port);

                                    port.onmessage +=
                                        x =>
                                        {
                                            messages++;

                                            port.postMessage("reply " + new { sharedworker.name, st.Elapsed, x.data, messages, ports.Count, random });

                                        };

                                    ports.Add(port);


                                    port.postMessage("hi from shared worker " + new { sharedworker.name, st.Elapsed, ports.Count, random });


                                }
                            );

                        };

                }
                );





            //@"Hello world".ToDocumentTitle();
            //// Send data from JavaScript to the server tier
            //service.WebMethod2(
            //    @"A string from JavaScript.",
            //    value => value.ToDocumentTitle()
            //);
        }

    }
}
