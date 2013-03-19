using ParallelForEachExperiment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ParallelForEachExperiment
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private async void button1_Click(object sender, System.EventArgs e)
        {
            button1.Enabled = false;

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
                    while (t.ElapsedMilliseconds < 5000)
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

            button1.Enabled = true;
        }

    }
}
