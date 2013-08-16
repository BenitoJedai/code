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
using InlineNestedWorkerExperiment;
using InlineNestedWorkerExperiment.Design;
using InlineNestedWorkerExperiment.HTML.Pages;
using System.IO;
using System.Diagnostics;

namespace InlineNestedWorkerExperiment
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
            new Abstractatech.ConsoleFormPackage.Library.ConsoleForm { }.InitializeConsoleFormWriter().Show();


            var ww = new Worker(
                worker =>
                {
                    // running in worker context. cannot talk to outer scope yet.

                    //worker.RedirectConsoleOutput();



                    // hello from the background worker { self = [object WorkerGlobalScope] }
                    Console.WriteLine("hello from the background worker " + new { Native.self });

                    // https://code.google.com/p/chromium/issues/detail?id=31666
                    // http://stackoverflow.com/questions/9259251/unable-to-create-web-worker-from-inside-webworker-in-chrome

                    // Uncaught ReferenceError: Worker is not defined 
                    // works in FF
                    // does NOT work in Chrome
                    // works in IE

                    // ww: www: goo { self = [object DedicatedWorkerGlobalScope] }
                    var www = new Worker(
                           wworker =>
                           {
                               // running in worker context. cannot talk to outer scope yet.

                               //wworker.RedirectConsoleOutput();


                               // hello from the background worker { self = [object WorkerGlobalScope] }
                               Console.WriteLine("goo " + new { Native.self });
                           }
                       );

                    www.onmessage +=
                        e =>
                        {
                            Console.Write("www: " + e.data);
                        };

                }
            );

            ww.onmessage +=
                e =>
                {
                    Console.Write("ww: " + e.data);
                };


            for (int xi = 0; xi < 4; xi++)
                new IHTMLButton { innerText = "cpu #" + xi }.AttachToDocument().WhenClicked(
                    btn =>
                    {
                        btn.disabled = true;

                        var www = new Worker(
                            wworker =>
                            {
                                // running in worker context. cannot talk to outer scope yet.

                                //wworker.RedirectConsoleOutput();


                                // hello from the background worker { self = [object WorkerGlobalScope] }

                                var x = 0.0;


                                Console.WriteLine("Start");
                                var s = new Stopwatch();
                                s.Start();
                                for (int j = 0; j < 32; j++)
                                {
                                    for (int i = 0; i < 32000000; i++)
                                    {
                                        x = Math.Sin(i);

                                    }
                                    Console.WriteLine(new { j, s.Elapsed }.ToString());
                                }
                                Console.WriteLine("Stop");
                            }
                        );

                        www.onmessage +=
                            e =>
                            {
                                Console.Write("www: " + e.data);
                            };
                    }
                );
        }

    }
}
