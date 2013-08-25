using TestTaskDelay;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;
using System.Runtime.CompilerServices;

namespace TestTaskDelay
{

    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private async void button1_Click(object sender, System.EventArgs e)
        {
            Console.WriteLine("at button1_Click");

            button1.Text = new { Thread.CurrentThread.ManagedThreadId }.ToString();

            // basically a timer event on the same thread
            await Task.Delay(2000);

            Console.WriteLine("button1_Click done");
            button1.Text = new { Thread.CurrentThread.ManagedThreadId }.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Console.WriteLine("at button2_Click");

            button2.Text = new { Thread.CurrentThread.ManagedThreadId }.ToString();
            //await Task.Delay(2000);
            // blocks the thread
            Thread.Sleep(2000);

            Console.WriteLine("button2_Click done");
            button2.Text = new { Thread.CurrentThread.ManagedThreadId }.ToString();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            // http://billwagner.azurewebsites.net/blog/async-10-switching-threads
            // http://stackoverflow.com/questions/4815261/where-can-i-find-the-threadpool-switchto-method
            // http://social.msdn.microsoft.com/Forums/en-US/642ffef6-d3ce-4010-978d-bc5d8b65c00f/where-are-the-switchto-extensions
            // http://stackoverflow.com/questions/303116/system-windows-threading-dispatcher-and-winforms
            // https://code.google.com/p/backgrounder/
            // http://msdn.microsoft.com/en-us/magazine/gg598924.aspx
            // http://blogs.msdn.com/b/lucian/archive/2012/12/12/how-to-write-a-custom-awaiter.aspx
            // http://stackoverflow.com/questions/15363413/why-was-switchto-removed-from-async-ctp-release
            // http://msdn.microsoft.com/en-us/library/vstudio/ee370351.aspx


            await Async.SwitchToWebWorker();


            Console.WriteLine("On the UI thread.");

            // Switch to a thread pool thread
            // Error	3	Cannot await 'TestTaskDelay.Extensions.SynchronizationContextAwaiter'	X:\jsc.svn\examples\javascript\Test\TestTaskDelay\TestTaskDelay\ApplicationControl.cs	61	13	TestTaskDelay
            await new SynchronizationContext().SwitchTo();

            Console.WriteLine("Starting CPU-intensive work on background thread...");

            //int result = DoCpuIntensiveWork();
            Thread.Sleep(1000);

            Console.WriteLine("Done with CPU-intensive work!");

            // Switch back to UI thread
            await Dispatcher.SwitchTo();

            Console.WriteLine("Back on the UI thread.  ");
            Async


            //await ThreadPool.SwitchTo();
            //try
            //{
            //  // Do something dangerous here.
            //}
            //finally
            //{
            //  await button1.Dispatcher.SwitchTo(); // COMPILE ERROR!
            //}

        }


    }

    public static class Extensions
    {
        public static Task SwitchTo(this SynchronizationContext context)
        {
            var x = new Task(
                delegate
                {
                    Console.WriteLine();
                }
            );

            

            return x;
        }


    }
}
