using ParallelTaskExperiment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParallelTaskExperiment
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
			// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201504/20150401

			button1.Enabled = false;

            //enter { ms = 800, text = task1, Caller = 1, Current = 10 }
            // view-source:24898
            //{ href = http://192.168.43.252:28676/view-source#worker, InternalThreadCounter = 11, MethodToken = HwAABqq2ADiZW_bpS6mGIrg, MethodType = FuncOfObjectToObject } view-source:24898

            // view-source:24898
            //enter { ms = 1089, text = task2, Caller = 1, Current = 11 }
            // view-source:24898
            //exit { text = task1, Caller = 1, Current = 10 }
            // view-source:24898
            //exit { text = task2, Caller = 1, Current = 11 }
            // view-source:24898
            //{ sum = 1889, ElapsedMilliseconds = 1244, Current = 1 }
            // view-source:24898
            //done { Current = 1 }


            var watch = new Stopwatch();
            watch.Start();

            // pick slowest
            var t = Task.Factory.ContinueWhenAll(
                tasks: new[] {
                    Task.Factory.StartNew(
                        new { ms = 800, text = "task1",  Thread.CurrentThread.ManagedThreadId },
                        state =>
                        {
                            Console.WriteLine("enter " + new { state.ms, state.text, Caller = state.ManagedThreadId, Current = Thread.CurrentThread.ManagedThreadId});
                            Thread.Sleep(state.ms);
                            Console.WriteLine("exit " + new {state.text, Caller = state.ManagedThreadId, Current = Thread.CurrentThread.ManagedThreadId});

                            return new { state.ms, state.text, goo = 32 };
                        }
                    ),

                    Task.Factory.StartNew(
                        new { ms = new Random().Next(400, 1200), text = "task2",  Thread.CurrentThread.ManagedThreadId },
                        state =>
                        {
                            // will same code be merged?

                            Console.WriteLine("enter " + new { state.ms, state.text, Caller = state.ManagedThreadId, Current = Thread.CurrentThread.ManagedThreadId });
                            Thread.Sleep(state.ms);
                            Console.WriteLine("exit " + new {state.text, Caller = state.ManagedThreadId, Current = Thread.CurrentThread.ManagedThreadId});


                           return new { state.ms, state.text, goo = 32 };
                        }
                    )
                },
                cancellationToken: default(CancellationToken),
                continuationOptions: default(TaskContinuationOptions),
                continuationFunction:
                    tasks =>
                    {
                        // { tasks = 0 } 
                        Console.WriteLine(new { tasks = tasks.Length });

                        // public static int Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector);
                        var sum = 0;

                        if (tasks.Length > 0)
                        {
                            sum = tasks.AsEnumerable().Sum(x => x.Result.ms);
                        }

                        // { sum = 1972, ElapsedMilliseconds = 1321, Current = 1 }
                        Console.WriteLine("" + new
                            {
                                sum,

                                // cannot share scope just yet
                                //watch.ElapsedMilliseconds,

                                Current = Thread.CurrentThread.ManagedThreadId
                            }
                        );



                        return new { sum };
                    },

                scheduler: TaskScheduler.Default
                // GUI ?
                //scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            );


            t.ContinueWith(
                task =>
                {
                    Console.WriteLine("done " + new { watch.ElapsedMilliseconds, Current = Thread.CurrentThread.ManagedThreadId });
                    button1.Enabled = true;

                    button1.Text = new { task.Result.sum }.ToString();
                },
                // GUI ?
                scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            );

            //            enter { ms = 867, text = task2, Caller = 3, Current = 4 }
            //enter { ms = 800, text = task1, Caller = 3, Current = 5 }
            //exit { text = task1, Caller = 3, Current = 5 }
            //exit { text = task2, Caller = 3, Current = 4 }
            //{ sum = 1667, ElapsedMilliseconds = 875, Current = 4 }
            //done { Current = 3 }


        }

    }
}
