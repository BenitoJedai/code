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
using InlineWorkerExperiment;
using InlineWorkerExperiment.Design;
using InlineWorkerExperiment.HTML.Pages;

namespace InlineWorkerExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            Native.worker.With(
                // #0
                  worker =>
                  {
                      // ww3.onmessage: { data = xxx fake0 { href = http://192.168.1.100:4216/view-source#goo, 

                      var href = Native.worker.location.href;
                      var index = href.SkipUntilOrEmpty("#");

                      // fake inline
                      worker.postMessage("xxx fake0 " + new { index, worker.location.href });
                  }
              );

            Native.worker.With(
                // #1
                worker =>
                {
                    // fake inline
                    worker.postMessage("xxx fake1 ");
                }
            );

            Native.window.With(
                window =>
                {
                    // what about multiple?
                    // what about dynamic handler selector?


                    new IHTMLButton { innerText = "w2" }.AttachToDocument().WhenClicked(
                        btn =>
                        {
                            // whats next, ApplicationWorker with methods or WorkerAction<> delegate
                            // what if jsc would detect any delegate Action<Action> that only communicates 
                            // cia delegates and turn them into background workers?
                            var ww = new Worker(
                                // #2
                                worker =>
                                {
                                    // running in worker context. cannot talk to outer scope yet.

                                    Console.WriteLine("xxx via Console");

                                    worker.postMessage("xxx");
                                }
                            );

                            // ww.onmessage: { data = xxx fake }
                            ww.onmessage +=
                                e =>
                                {
                                    Console.WriteLine("ww.onmessage: " + new { e.data });
                                };
                        }
                    );


                    Action<string, Action<DedicatedWorkerGlobalScope>, Action<MessageEvent>> work =
                        (innerText, handler, onmessage) =>
                        {
                            new IHTMLButton { innerText = innerText }.AttachToDocument().WhenClicked(
                                  btn =>
                                  {
                                      var ww = new Worker(handler);

                                      // ww.onmessage: { data = xxx fake }
                                      ww.onmessage += onmessage;
                                  }
                              );
                        };


                    Action<MessageEvent> work7onmessage = e =>
                    {
                        Console.WriteLine("work 7 onmessage " + new { e.data });
                    };

                    work("work 7",
                        arg2: worker =>
                        {
                            worker.postMessage("work 7");
                        },
                          arg3: work7onmessage
                    );



                    Action<MessageEvent> work8onmessage = e =>
                    {
                        Console.WriteLine("work 8 onmessage " + new { e.data });
                    };


                    work("work 8",
                        arg2: worker =>
                        {
                            worker.postMessage("work 8");
                        },
                        arg3: work8onmessage
                    );


                    new IHTMLButton { innerText = "w3" }.AttachToDocument().WhenClicked(
                        btn =>
                        {
                            var ww = new Worker(
                                // #2
                                worker =>
                                {
                                    // running in worker context. cannot talk to outer scope yet.

                                    worker.postMessage("xxx3");
                                }
                            );

                            // ww.onmessage: { data = xxx fake }
                            ww.onmessage +=
                                e =>
                                {
                                    Console.WriteLine("ww3.onmessage: " + new { e.data });
                                };
                        }
                    );

                    new IHTMLButton { innerText = "w goo" }.AttachToDocument().WhenClicked(
                           btn =>
                           {
                               var ww = new Worker("view-source#goo");

                               // ww.onmessage: { data = xxx fake }
                               ww.onmessage +=
                                   e =>
                                   {
                                       Console.WriteLine("ww3.onmessage: " + new { e.data });
                                   };
                           }
                       );
                }
            );

        }

    }
}
