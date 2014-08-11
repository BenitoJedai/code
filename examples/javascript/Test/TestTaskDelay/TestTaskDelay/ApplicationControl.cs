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
using System.Diagnostics;

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
            //Console.WriteLine("at button1_Click");

            //button1.Text = new { Thread.CurrentThread.ManagedThreadId }.ToString();

            //// basically a timer event on the same thread
            //await Task.Delay(2000);

            //Console.WriteLine("button1_Click done");
            //button1.Text = new { Thread.CurrentThread.ManagedThreadId }.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("at button2_Click");

            //button2.Text = new { Thread.CurrentThread.ManagedThreadId }.ToString();
            ////await Task.Delay(2000);
            //// blocks the thread
            //Thread.Sleep(2000);

            //Console.WriteLine("button2_Click done");
            //button2.Text = new { Thread.CurrentThread.ManagedThreadId }.ToString();
        }



        static
        private async void button3_Click(object sender, EventArgs e)
        {
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\Task\Task.Delay.cs


            // http://billwagner.azurewebsites.net/blog/async-10-switching-threads
            // http://stackoverflow.com/questions/4815261/where-can-i-find-the-threadpool-switchto-method
            // http://social.msdn.microsoft.com/Forums/en-US/642ffef6-d3ce-4010-978d-bc5d8b65c00f/where-are-the-switchto-extensions
            // http://stackoverflow.com/questions/303116/system-windows-threading-dispatcher-and-winforms
            // https://code.google.com/p/backgrounder/
            // http://msdn.microsoft.com/en-us/magazine/gg598924.aspx
            // http://blogs.msdn.com/b/lucian/archive/2012/12/12/how-to-write-a-custom-awaiter.aspx
            // http://stackoverflow.com/questions/15363413/why-was-switchto-removed-from-async-ctp-release
            // http://msdn.microsoft.com/en-us/library/vstudio/ee370351.aspx




            Console.WriteLine("On the UI thread.");
            Console.WriteLine(new { Thread.CurrentThread.ManagedThreadId });


            var sw = Stopwatch.StartNew();

            // access UI controls
            await Task.Delay(100).ConfigureAwait(false);
            // DO NOT access UI controls here, as you're very likely on a ThreadPool thread
            Console.WriteLine("Back!");
            Console.WriteLine(new { Thread.CurrentThread.ManagedThreadId, sw.ElapsedMilliseconds });
            //            On the UI thread.
            //{ ManagedThreadId = 3 }
            //Back!
            //{ ManagedThreadId = 5, ElapsedMilliseconds = 113 }
        }


    }


}
