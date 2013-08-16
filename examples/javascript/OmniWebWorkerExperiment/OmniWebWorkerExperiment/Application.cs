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
using OmniWebWorkerExperiment;
using OmniWebWorkerExperiment.Design;
using OmniWebWorkerExperiment.HTML.Pages;
using System.Threading;

namespace OmniWebWorkerExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201308/20130816-web-worker

        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // { self = [object Window] } 

            //var self = new IFunction("return this;").apply(null);

            // { self = [object Window], window = [object Window], screen = [object Screen], document = [object HTMLDocument], page = [object Object] } 
            Console.WriteLine(new { Native.self, Native.window, Native.screen, Native.document, page });

            Native.worker.With(
                worker =>
                {
                    worker.onmessage +=
                        e =>
                        {
                            // onmessage: { data = mirror: { data = hey you, ports = 1 } }

                            worker.postMessage(
                                "mirror: " + new { e.data, ports = e.ports.Length }
                            );

                            e.ports.WithEach(
                                port =>
                                {
                                    port.postMessage(
                                            "port mirror: " + new { e.data, ports = e.ports.Length }
                                        );
                                }
                            );
                        };


                    worker.postMessage(
                        "hello from worker? " + new { Native.self, Native.window, Native.screen, Native.document, page }
                    );
                }
            );


            Native.document.With(
                document =>
                {
                    // what if we are in web worker?


                    // Uncaught ReferenceError: document is not defined 
                    new IHTMLButton { innerText = "worker" }.AttachToDocument().WhenClicked(
                        delegate
                        {
                            Console.WriteLine("before Worker");

                            //var tt = new Thread();


                            // Error	1	'ScriptCoreLib.JavaScript.DOM.Worker.Worker(System.Action<ScriptCoreLib.JavaScript.DOM.DedicatedWorkerGlobalScope>)' is obsolete: 'not implemented yet'	x:\jsc.svn\examples\javascript\OmniWebWorkerExperiment\OmniWebWorkerExperiment\Application.cs	87	38	OmniWebWorkerExperiment
                            var ww = new Worker(
                                (DedicatedWorkerGlobalScope worker) =>
                                {
                                    // running in worker context. cannot talk to outer scope yet.

                                    worker.postMessage("xxx");
                                }
                            );

                            ww.onmessage +=
                                e =>
                                {
                                    Console.WriteLine("ww.onmessage: " + new { e.data });
                                };

                            var w = new Worker();

                            w.onmessage +=
                                    e =>
                                    {
                                        Console.WriteLine("onmessage: " + new { e.data });
                                        // onmessage: { data = hello from worker? 1 }
                                    };

                            var c = new MessageChannel();

                            c.port1.onmessage +=
                                    e =>
                                    {
                                        Console.WriteLine("port1.onmessage: " + new { e.data });
                                        // onmessage: { data = hello from worker? 1 }
                                    };

                            c.port1.start();
                            c.port2.start();

                            // Transferable should be an interface?
                            w.postMessage("hey you", new[] { c.port2 });
                            //w.postMessage("hey you", new[] { (Transferable)(object)c.port2 });

                            Console.WriteLine("after Worker");

                            //onmessage: { data = hello from worker? { self = [object global], window = , screen = , document = , page =  } }
                            //onmessage: { data = mirror: { data = hey you } }

                        }
                    );
                }
            );

        }

    }
}
