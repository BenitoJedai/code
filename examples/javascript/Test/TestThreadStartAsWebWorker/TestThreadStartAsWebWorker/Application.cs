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
using System.Threading;
using System.Xml.Linq;
using TestThreadStartAsWebWorker;
using TestThreadStartAsWebWorker.Design;
using TestThreadStartAsWebWorker.HTML.Pages;

namespace TestThreadStartAsWebWorker
{
    public static class X
    {
        public static void JoinAsync(this Thread t, Action yield)
        {
            new ScriptCoreLib.JavaScript.Runtime.Timer(
                timer =>
                {
                    if (t.IsAlive)
                        return;

                    // { goo = from thread } 
                    yield();

                    timer.Stop();
                }
            ).StartInterval(1000 / 10);
        }
    }
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
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201406/20140628

            // X:\jsc.svn\examples\java\ParallelForEachExperiment\ParallelForEachExperiment\ApplicationControl.cs

            var t = new Thread(
                new ParameterizedThreadStart(
                    o =>
                    {
                        // we have a local copy of the object.
                        var oo = o as XData;
                        // all member access methods should be non blocking
                        // if they need to span across thread boundaries

                        Console.WriteLine("working on the other thread");


                        // update local, then update parent memory
                        oo.goo += ", from thread " + new { Thread.CurrentThread.ManagedThreadId };

                        // or should the fields be transferable dictionary?
                        // we could just send the data part back now
                    }
                )
            );

            var ooo = new XData { goo = "goo1 " + new { Thread.CurrentThread.ManagedThreadId } };

            t.Start(
                // we are sending in data/objects
                ooo
            );


            t.JoinAsync(
                delegate
                {
                    //                    0:18ms working on the other thread
                    //0:122ms { goo = from thread }

                    //Console.WriteLine(new { ooo.goo });

                    // { goo = goo1 { ManagedThreadId = 1 }, from thread { ManagedThreadId = 1 } }

                    new IHTMLPre
                    {
                        new {  ooo.goo }
                    }.AttachToDocument();

                }
            );


        }

    }

    class XData
    {
        public string goo;
    }
}
