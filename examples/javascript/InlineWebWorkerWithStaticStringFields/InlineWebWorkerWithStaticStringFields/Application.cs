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
using InlineWebWorkerWithStaticStringFields;
using InlineWebWorkerWithStaticStringFields.Design;
using InlineWebWorkerWithStaticStringFields.HTML.Pages;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;

namespace InlineWebWorkerWithStaticStringFields
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();



        public static string loc0 = "foo from cctor";

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201308/20130826-domainmemory
            // see also: x:\jsc.svn\examples\javascript\Test\TestThreadStart\TestThreadStart\Application.cs


            var counter = 0;

            var btn = new IHTMLButton { innerText = "spawn new thread" };

            Native.window.onframe +=
                delegate
                {
                    btn.innerText = "spawn new thread" + new { loc0 };
                };


            btn.AttachToDocument().WhenClicked(
                delegate
                {
                    counter++;

                    Console.WriteLine(new { loc0, Thread.CurrentThread.ManagedThreadId });

                    loc0 = "click " + new { counter };

                    //Thread.CurrentContext.DoCallBack(
                    //    delegate
                    //    {
                    //        // now what?
                    //    }
                    //);

                    //new Task().run

                    new Worker(
                         worker =>
                         {
                             Console.WriteLine("should not send changes!");
                             loc0 = loc0;
                             Console.WriteLine("should not send changes?");

                             // should a trigger see that we changed a static field?
                             loc0 += ", working...";

                             Console.WriteLine(new { loc0, Thread.CurrentThread.ManagedThreadId });



                             var s = new Stopwatch();
                             s.Start();

                             // spin the cpu 
                             // how long do we have to, in order for task manager to notice?
                             // this should keep one cpu utilized atleast at 70%
                             for (int i = 0; i < 5; i++)
                             {


                                 var xs = new Stopwatch();
                                 xs.Start();
                                 while (xs.ElapsedMilliseconds < 1000) ;

                                 Console.WriteLine(".");

                                 loc0 += " " + new { i };


                             }

                             Console.WriteLine("working ... done " + new { s.Elapsed, Thread.CurrentThread.ManagedThreadId });


                             loc0 += " done!";

                             worker.postMessage("done!");
                             //worker.close();
                         }
                    ).onmessage +=
                        e =>
                        {
                            Console.WriteLine("onmessage: " + e.data);
                        };
                }
            );

        }

    }
}
