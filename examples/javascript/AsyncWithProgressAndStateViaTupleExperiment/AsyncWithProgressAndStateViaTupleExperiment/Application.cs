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
using AsyncWithProgressAndStateViaTupleExperiment;
using AsyncWithProgressAndStateViaTupleExperiment.Design;
using AsyncWithProgressAndStateViaTupleExperiment.HTML.Pages;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AsyncWithProgressAndStateViaTupleExperiment
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
            new IHTMLButton { innerText = "go 1" }.AttachToDocument().WhenClicked(
                delegate
                {

                    // http://blogs.msdn.com/b/dotnet/archive/2012/06/06/async-in-4-5-enabling-progress-and-cancellation-in-async-apis.aspx
                    (new Progress<string>(
                       x =>
                       {
                           Console.WriteLine("DOM Progress: " + new { x, Thread.CurrentThread.ManagedThreadId });
                           Native.document.body.innerText = new { x, Thread.CurrentThread.ManagedThreadId }.ToString();
                       }
                    ) as IProgress<string>).With(
                       async progress =>
                       {
                           Console.WriteLine("before");

                           // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201309-1/20130904-iprogress
                           var x = await Task.Factory.StartNew(
                               Tuple.Create(progress, new { hello = "world!" }),
                               scope =>
                               {
                                   if (scope == null)
                                   {
                                       // { x = scope_progress is null { state = [object Object], BackgroundThread = 10 }, ManagedThreadId = 1 }
                                       return "scope is null " + new { BackgroundThread = Thread.CurrentThread.ManagedThreadId };
                                   }


                                   var scope_progress = scope.Item1;

                                   var state = scope.Item2;


                                   if (scope_progress == null)
                                   {
                                       // { x = null { BackgroundThread = 10 }, ManagedThreadId = 1 }
                                       return "scope_progress is null " + new { state, BackgroundThread = Thread.CurrentThread.ManagedThreadId };
                                   }

                                   Action<string> yield = Console.WriteLine;
                                   yield += scope_progress.Report;

                                   var e = new Stopwatch();

                                   e.Start();

                                   yield("hi " + new { e.ElapsedMilliseconds, BackgroundThread = Thread.CurrentThread.ManagedThreadId });

                                   for (int i = 0; i < 20; i++)
                                   {
                                       yield(". " + new { i, state.hello, e.ElapsedMilliseconds, BackgroundThread = Thread.CurrentThread.ManagedThreadId });
                                       Thread.Sleep(100);

                                   }


                                   return "done " + new { e.ElapsedMilliseconds, BackgroundThread = Thread.CurrentThread.ManagedThreadId };
                               }
                           );

                           Console.WriteLine("after");

                           Native.document.body.innerText = new { x, Thread.CurrentThread.ManagedThreadId }.ToString();
                       }
                    );

                }
            );



            new IHTMLButton { innerText = "go 2" }.AttachToDocument().WhenClicked(
                //async
              delegate
              {
                  // hsc, why wont lambda declaration work within async scope?

                  // http://blogs.msdn.com/b/dotnet/archive/2012/06/06/async-in-4-5-enabling-progress-and-cancellation-in-async-apis.aspx

                  Console.WriteLine("before");

                  // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201309-1/20130904-iprogress
                  //var x =
                  //await 
                  Task.Factory.StartNewWithProgress(

                  new { value = "world!" },

                  progress:
                     xx =>
                     {
                         Console.WriteLine("DOM Progress: " + new { xx.value, Thread.CurrentThread.ManagedThreadId });

                         Native.document.body.innerText = new { xx.value, Thread.CurrentThread.ManagedThreadId }.ToString();
                     },

                  function:
                      scope =>
                      {
                          #region scope is null
                          if (scope == null)
                          {
                              // { x = scope_progress is null { state = [object Object], BackgroundThread = 10 }, ManagedThreadId = 1 }
                              return new { value = "scope is null " + new { BackgroundThread = Thread.CurrentThread.ManagedThreadId } };
                          }
                          #endregion



                          var state = scope.Item2;

                          #region scope_progress
                          var scope_progress = scope.Item1;
                          if (scope_progress == null)
                          {
                              // { x = null { BackgroundThread = 10 }, ManagedThreadId = 1 }
                              return new { value = "scope_progress is null " + new { state, BackgroundThread = Thread.CurrentThread.ManagedThreadId } };
                          }
                          #endregion

                          var e = new Stopwatch();
                          e.Start();

                          scope_progress.Report(
                               new
                               {
                                   value = "hi " + new { e.ElapsedMilliseconds, BackgroundThread = Thread.CurrentThread.ManagedThreadId }
                               }
                          );


                          for (int i = 0; i < 20; i++)
                          {
                              scope_progress.Report(
                                 new
                                 {
                                     value = ". " + new { i, state.value, e.ElapsedMilliseconds, BackgroundThread = Thread.CurrentThread.ManagedThreadId }
                                 }
                              );

                              Thread.Sleep(100);
                          }


                          return new { value = "done " + new { e.ElapsedMilliseconds, BackgroundThread = Thread.CurrentThread.ManagedThreadId } };
                      }
              ).ContinueWithResult(
                  x =>
                  {
                      Console.WriteLine("after");
                      Native.document.body.innerText = new { x.value, Thread.CurrentThread.ManagedThreadId }.ToString();
                  }
              );

              }
          );
        }

    }
}
