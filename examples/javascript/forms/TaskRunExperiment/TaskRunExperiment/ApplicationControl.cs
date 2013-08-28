using TaskRunExperiment;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System;
using System.Threading.Tasks;

namespace TaskRunExperiment
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        sealed class xfoo
        {
            public string foo; public int HandlerThread;

            public xfoo()
            {

            }

            public xfoo(string foo, int HandlerThread)
            {
                this.foo = foo;
                this.HandlerThread = HandlerThread;


            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201308/20130828-thread-run

            // script: error JSC1000: Missing Script Attribute? Native constructor was invoked, please implement [System.Threading.Tasks.Task`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]..ctor(System.Func`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]])]




            //            __Task.ctor
            //__Task.ContinueWith
            //__Task.Start


            var t = Task.Factory.StartNew(
                new
                {
                    foo = "foo",
                    HandlerThread = Thread.CurrentThread.ManagedThreadId

#if DEBUG
                    ,
                    s = TaskScheduler.FromCurrentSynchronizationContext()
#endif
                },
                o =>
                {

                    var yield_value = "done " + new
                    {
                        TaskThread = Thread.CurrentThread.ManagedThreadId,
                        o.foo,
                        o.HandlerThread
                    };

                    // +		TaskScheduler.FromCurrentSynchronizationContext()	
                    // 'TaskScheduler.FromCurrentSynchronizationContext()' threw an exception of 
                    // type 'System.InvalidOperationException'	System.Threading.Tasks.TaskScheduler {System.InvalidOperationException}


#if DEBUG
                    Task.Factory.StartNew(
                        delegate
                        {
                            this.button1.Text = "...";

                        },

                        default(CancellationToken),
                        TaskCreationOptions.None,
                        o.s
                    );
#endif




                    Console.WriteLine(new { yield_value });

                    return yield_value;
                }
            );

            // { Result = done { TaskThread = 4, o = { foo = foo, HandlerThread = 3 } }, ContinueWithThread = 4 }
            //var t = new System.Threading.Tasks.Task<string>(
            //    o =>
            //    {
            //        //Thread.Sleep(2000);

            //        var yield_value = "done " + new { TaskThread = Thread.CurrentThread.ManagedThreadId, o };

            //        Console.WriteLine(new { yield_value });

            //        return yield_value;
            //    },
            //    new { foo = "foo", HandlerThread = Thread.CurrentThread.ManagedThreadId }

            //);

            // Additional information: Start may not be called on a continuation task.

            // http://stackoverflow.com/questions/16032102/continuewith-taskscheduler-and-taskscheduler-fromcurrentsynchronizationcontext

            var ThreadPool = TaskScheduler.Current;
            var ZScheduler = TaskScheduler.Default;
            var GUIScheduler = TaskScheduler.FromCurrentSynchronizationContext();


            var tt = t.ContinueWith(
                y =>
                {
                    // { Result = done { ManagedThreadId = 4 }, ManagedThreadId = 5 }


                    Console.WriteLine("inside ContinueWith 1");
                    Console.WriteLine(new { y });

                    //d = (b.vwYABjG5PzKFZ_bjhfdVsnw() == null);
                    if (y.Result != null)
                    {
                        Console.WriteLine(

                            new { y.Result, ContinueWithThread = Thread.CurrentThread.ManagedThreadId }
                            );
                    }
                    else
                    {
                        Console.WriteLine(

                            new { PartialContinueWithThread = Thread.CurrentThread.ManagedThreadId }
                            );
                    }

                    return "new result 1";
                },
                ZScheduler
            );

            var ttt = tt.ContinueWith(
                 y =>
                 {
                     Console.WriteLine("inside ContinueWith 2");

                     // { Result = done { ManagedThreadId = 4 }, ManagedThreadId = 5 }
                     Console.WriteLine(

                         new { y.Result, ZContinueWithThread = Thread.CurrentThread.ManagedThreadId }
                     );


                     return "new result 2";
                 }, GUIScheduler
             );

            var tttt = ttt.ContinueWith(
                  y =>
                  {
                      Console.WriteLine("inside ContinueWith 3");

                      // { Result = done { ManagedThreadId = 4 }, ManagedThreadId = 5 }
                      Console.WriteLine(

                          new { y.Result, GUIContinueWithThread = Thread.CurrentThread.ManagedThreadId }
                          );


                  }, GUIScheduler



              );

            //            { Result = done { TaskThread = 1, o = { foo = foo, HandlerThread = 1 } }, ContinueWithThread = 1 } view-source:25193

            // view-source:25193
            //__Task.InternalStart outer complete
            // view-source:25193
            //__Task.InternalStart outer
            // view-source:25193
            //{ Result = done { TaskThread = 1, o = { foo = foo, HandlerThread = 1 } }, ZContinueWithThread = 1 } view-source:25193

            // view-source:25193
            //__Task.InternalStart outer complete
            // view-source:25193
            //__Task.InternalStart outer
            // view-source:25193
            //{ Result = done { TaskThread = 1, o = { foo = foo, HandlerThread = 1 } }, GUIContinueWithThread = 1 } 





            //            t.GetAwaiter().OnCompleted { AwaiterThread = 3 }
            //{ Result = done { TaskThread = 4, o = { foo = foo, HandlerThread = 3 } }, ContinueWithThread = 5 }
            //{ Result = done { TaskThread = 4, o = { foo = foo, HandlerThread = 3 } }, GUIContinueWithThread = 3 }
            //tt.GetAwaiter().OnCompleted { AwaiterThread = 3 }

            //            t.GetAwaiter().OnCompleted { AwaiterThread = 3 }
            //{ Result = done { TaskThread = 4, o = { foo = foo, HandlerThread = 3 } }, ContinueWithThread = 5 }
            //tt.GetAwaiter().OnCompleted { AwaiterThread = 3 }


            // tt.GetAwaiter().OnCompleted(
            //    delegate
            //    {
            //        Console.WriteLine("tt.GetAwaiter().OnCompleted " + new { AwaiterThread = Thread.CurrentThread.ManagedThreadId });
            //    }
            //);

            // t.GetAwaiter().OnCompleted(
            //     delegate
            //     {
            //         Console.WriteLine("t.GetAwaiter().OnCompleted " + new { AwaiterThread = Thread.CurrentThread.ManagedThreadId });
            //     }
            // );


            // Additional information: Start may not be called on a task that was already started.
            //t.Start();


        }

    }
}
