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
using InlineSharedWorkerExperiment;
using InlineSharedWorkerExperiment.Design;
using InlineSharedWorkerExperiment.HTML.Pages;
using System.Diagnostics;
using System.Collections.Generic;

namespace InlineSharedWorkerExperiment
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
            Native.window.With(
                window =>
                {
                    new IHTMLButton { innerText = "open" }.AttachToDocument().onclick +=
                        delegate
                        {
                            Console.WriteLine("opening SharedWorker");

                            // what about async keyword?
                            var s = new SharedWorker(
                                sharedworker =>
                                {
                                    // the portal effect
                                    // this code is no longer in the same context
                                    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201308/20130812-sharedworker

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
        }

    }
}
