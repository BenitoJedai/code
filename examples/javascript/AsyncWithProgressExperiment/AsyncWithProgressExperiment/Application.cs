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
using AsyncWithProgressExperiment;
using AsyncWithProgressExperiment.Design;
using AsyncWithProgressExperiment.HTML.Pages;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncWithProgressExperiment
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
            // http://blogs.msdn.com/b/dotnet/archive/2012/06/06/async-in-4-5-enabling-progress-and-cancellation-in-async-apis.aspx
            (new Progress<string>(
               x =>
               {
                   Native.document.body.innerText = new { x, Thread.CurrentThread.ManagedThreadId }.ToString();
               }
            ) as IProgress<string>).With(
               async progress =>
               {
                   // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201309-1/20130904-iprogress
                   var x = await Task.Factory.StartNew(
                       progress,
                       scope_progress =>
                       {
                           if (scope_progress == null)
                           {
                               return "null " + new { BackgroundThread = Thread.CurrentThread.ManagedThreadId };
                           }

                           Action<string> yield = scope_progress.Report;

                           yield("hi " + new { BackgroundThread = Thread.CurrentThread.ManagedThreadId });

                           Thread.Sleep(1000);

                           yield("almost done");

                           Thread.Sleep(1000);

                           return "done " + new { BackgroundThread = Thread.CurrentThread.ManagedThreadId };
                       }
                   );

                   Native.document.body.innerText = new { x, Thread.CurrentThread.ManagedThreadId }.ToString();
               }
            );

        }

    }
}
