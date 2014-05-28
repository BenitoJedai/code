using AsyncTaskYieldViaProgress;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;
using System.Threading.Tasks;

namespace AsyncTaskYieldViaProgress
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            // X:\jsc.svn\examples\javascript\AsyncWithProgressAndStateViaTupleExperiment\AsyncWithProgressAndStateViaTupleExperiment\Application.cs

            button1.Enabled = false;

            foreach (var item in X.Invoke())
            {

                var x = new { item };

                Console.WriteLine(x);
                button1.Text = x.ToString();
                Thread.Yield();

                //type: AsyncTaskYieldViaProgress.ApplicationControl, AsyncTaskYieldViaProgress.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
                System.Windows.Forms.Application.DoEvents();
            }

            button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            (new Progress<string>(
           x =>
           {
               button2.Text = x;

               Console.WriteLine("DOM Progress: " + new { x, Thread.CurrentThread.ManagedThreadId });
           }
        ) as IProgress<string>).With(
                //async 
                progress =>
                {
                    Console.WriteLine("before");
                    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201309-1/20130904-iprogress
                    //var xxx = await 
                    Task.Factory.StartNew(
                     Tuple.Create(progress, new { hello = "world!" }),
                      scope =>
                      {
                          var xprogress = scope.Item1;

                          foreach (var item in X.Invoke())
                          //var item = X.Invoke().First();
                          {

                              var x = new { item, Thread.CurrentThread.ManagedThreadId };

                              Console.WriteLine(x);



                              xprogress.Report(x.ToString());

                              // Cross-thread operation not valid: Control 'button1' accessed from a thread other than the thread it was created on.
                              //Thread.Yield();



                              //System.Windows.Forms.Application.DoEvents();
                          }

                          //await Task.Delay(333);

                          return "";
                      }
                  ).ContinueWithResult(
                          xxx =>
                        Console.WriteLine("after")
                    );

                }
        );
        }

        private
            //async 
            void button3_Click(object sender, EventArgs e)
        {
            Console.WriteLine("before");

            // Error	4	The type arguments for method 'ScriptCoreLib.Extensions.TaskAsyncExtensions.StartNew<TSource,TResult>(System.Threading.Tasks.TaskFactory, TSource, System.Action<TResult>, System.Func<System.Tuple<System.IProgress<TResult>,TSource>,TResult>)' cannot be inferred from the usage. Try specifying the type arguments explicitly.	X:\jsc.svn\examples\javascript\forms\AsyncTaskYieldViaProgress\AsyncTaskYieldViaProgress\ApplicationControl.cs	97	29	AsyncTaskYieldViaProgress
            //Error	4	The type arguments for method 'ScriptCoreLib.Extensions.TaskAsyncExtensions.StartNewWithProgress<TSource,TResult>(System.Threading.Tasks.TaskFactory, TSource, System.Action<TResult>, System.Func<System.Tuple<System.IProgress<TResult>,TSource>,TResult>)' cannot be inferred from the usage. Try specifying the type arguments explicitly.	X:\jsc.svn\examples\javascript\forms\AsyncTaskYieldViaProgress\AsyncTaskYieldViaProgress\ApplicationControl.cs	101	29	AsyncTaskYieldViaProgress

            //before
            //after { hello = early done { ManagedThreadId = 4 } }
            //{ item = 4, ManagedThreadId = 5 }
            //DOM Progress: { hello = { item = 4, ManagedThreadId = 5 }, GUI = 3 }
            //{ item = 8, ManagedThreadId = 5 }
            //DOM Progress: { hello = { item = 8, ManagedThreadId = 5 }, GUI = 3 }
            //{ item = 15, ManagedThreadId = 5 }
            //DOM Progress: { hello = { item = 15, ManagedThreadId = 5 }, GUI = 3 }
            //{ item = 16, ManagedThreadId = 5 }
            //DOM Progress: { hello = { item = 16, ManagedThreadId = 5 }, GUI = 3 }
            //{ item = 23, ManagedThreadId = 5 }
            //DOM Progress: { hello = { item = 23, ManagedThreadId = 5 }, GUI = 3 }
            //{ item = 42, ManagedThreadId = 5 }
            //DOM Progress: { hello = { item = 42, ManagedThreadId = 5 }, GUI = 3 }


            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201309-1/20130904-iprogress
            Task.Factory.StartNewWithProgress(
                 new { hello = "world!" },

                 progress:
                     x =>
                     {
                         button3.Text = x.hello;

                         //Console.WriteLine("DOM Progress: " + new { x.hello, GUI = Thread.CurrentThread.ManagedThreadId });
                     },

                 function:
                     scope =>
                     {
                         var xprogress = scope.Item1;

                         // this will spawn another thread
                         // if current worker already exited... ?
                         //await Task.Delay(333);

                         //foreach (var item in X.Invoke().ToArray())


                         Action work = delegate
                         {
                             foreach (var item in X.Invoke())

                             //var item = X.Invoke().First();
                             {

                                 var x = new { item, DelayThread = Thread.CurrentThread.ManagedThreadId };

                                 //Console.WriteLine(x);



                                 xprogress.Report(
                                     new { hello = x.ToString() }
                                 );

                                 // Cross-thread operation not valid: Control 'button1' accessed from a thread other than the thread it was created on.
                                 //Thread.Yield();



                                 //System.Windows.Forms.Application.DoEvents();
                             }
                         };

                         //cript: error JSC1000: No implementation found for this native method, please implement 
                         // [System.Threading.Tasks.Task.ContinueWith(System.Action`1[[System.Threading.Tasks.Task, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]])]

                         Task.Delay(1).GetAwaiter().OnCompleted(work);


                         //yield();


                         return new { hello = "early done " + new { Thread.CurrentThread.ManagedThreadId } };
                     }



              ).ContinueWithResult(
                xxx =>
                {

                    Console.WriteLine("after " + new { xxx.hello });
                }
            );

        }

    }


    static class X
    {


        public static IEnumerable<int> Invoke()
        {
            // X:\jsc.svn\examples\rewrite\TestSwitchRewriteForInitializeArray\TestSwitchRewriteForInitializeArray\Class1.cs


            Console.WriteLine("enter Invoke");

            var x = System.Environment.CurrentManagedThreadId;

            // http://lostpedia.wikia.com/wiki/The_Numbers

            var value = new[] { 4, 8, 15, 16, 23, 42 };

            for (int i = 0; i < value.Length; i++)
            {
                Console.WriteLine("Invoke before Sleep " + new { i, value.Length });

                Thread.Sleep(
                    new Random().Next(100, 1600)
                );



                var valuei = value[i];
                Console.WriteLine("Invoke before yield return " + new { valuei });
                yield return valuei;

                //yield return value[i];
            }

        }
    }
}
