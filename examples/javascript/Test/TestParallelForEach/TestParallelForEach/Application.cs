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
using TestParallelForEach;
using TestParallelForEach.Design;
using TestParallelForEach.HTML.Pages;
using System.Threading;
using System.Diagnostics;

namespace TestParallelForEach
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
            new IHTMLButton { "go" }.AttachToDocument().onclick +=
                e =>
            {
                var button1 = e.Element;

                // see also
                // X:\jsc.svn\examples\javascript\Test\TestThreadStartAsWebWorker\TestThreadStartAsWebWorker\Application.cs

                // 
                button1.disabled = true;

                // Switch to a thread pool thread
                // how the fck to do that?
                //await new SynchronizationContext();    

                Console.WriteLine("before " + new { Thread.CurrentThread.ManagedThreadId });

                Action<string> Worker =
                    x =>
                {
                    Console.WriteLine("start " + new { x, Thread.CurrentThread.ManagedThreadId });

                    var t = new Stopwatch();
                    t.Start();

                    var foo = 0L;

                    // make CPU busy
                    while (t.ElapsedMilliseconds < 1000)
                    {

                        foo = foo + 3 - 2;
                    };


                    Console.WriteLine("stop " + new { x, Thread.CurrentThread.ManagedThreadId });


                };

                new[] {
                "without PLINQ 1", "without PLINQ 2", "without PLINQ 3", "without PLINQ4"
            }.AsParallel().WithExecutionMode(ParallelExecutionMode.ForceParallelism).ForAll(Worker);



                //Worker("without PLINQ");
                Console.WriteLine("after " + new { Thread.CurrentThread.ManagedThreadId });

                button1.disabled = false;
            };





        }

    }
}
