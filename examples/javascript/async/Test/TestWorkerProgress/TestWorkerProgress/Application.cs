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
using TestWorkerProgress;
using TestWorkerProgress.Design;
using TestWorkerProgress.HTML.Pages;
using System.Threading;

namespace TestWorkerProgress
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
            // X:\jsc.svn\examples\javascript\forms\AsyncWithProgress\AsyncWithProgress\ApplicationControl.cs
            // http://blogs.msdn.com/b/dotnet/archive/2012/06/06/async-in-4-5-enabling-progress-and-cancellation-in-async-apis.aspx
            IProgress<string> progress = new Progress<string>(
               x =>
               {
                   new IHTMLPre {
                       new { x, Thread.CurrentThread.ManagedThreadId }
                   }.AttachToDocument();

               }
           );


            new IHTMLButton { "invoke" }.AttachToDocument().onclick +=
                async e =>
                {
                    //{ { x = hi { { BackgroundThread = 10 } }, ManagedThreadId = 1 } }
                    //{ { x = almost done, ManagedThreadId = 1 } }
                    //{ { x = done { { BackgroundThread = 10 } }, ManagedThreadId = 1 } }

                    var scope1 = "hello world";

                    var x = await Task.Factory.StartNew(
                        // this wont work yet will it
                        // message: "Failed to execute 'postMessage' on 'Worker': An object could not be cloned."
                        //new { progress },
                        progress,
                        //scope =>
                        scope =>
                        {
                            //if (scope == null)
                            //{
                            //    Console.WriteLine("scope is null?");
                            //}

                            Action<string> yield = scope.Report;

                            yield("hi " + new { scope1, BackgroundThread = Thread.CurrentThread.ManagedThreadId });

                            Thread.Sleep(1000);

                            yield("almost done");

                            Thread.Sleep(1000);

                            return "done " + new { scope1, BackgroundThread = Thread.CurrentThread.ManagedThreadId };
                        }
                    );

                    new IHTMLPre {
                       new { scope1, x, Thread.CurrentThread.ManagedThreadId }
                    }.AttachToDocument();

                    //button1.Text = new { x, Thread.CurrentThread.ManagedThreadId }.ToString();
                };
        }

    }
}
