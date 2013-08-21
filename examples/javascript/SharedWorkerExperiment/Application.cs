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

                    var st = new Stopwatch();
                    st.Start();


                    sharedworker.onconnect +=
                        e =>
                        {
                            e.ports.WithEach(
                                port =>
                                {
                                    port.start();

                                    port.onmessage +=
                                        x =>
                                        {
                                            port.postMessage("reply " + new { sharedworker.name, st.Elapsed, x.data });

                                        };

                                    port.postMessage("hi from shared worker " + new { sharedworker.name, st.Elapsed });


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
