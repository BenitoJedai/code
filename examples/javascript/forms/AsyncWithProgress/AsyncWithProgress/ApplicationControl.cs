using AsyncWithProgress;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncWithProgress
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private async void button1_Click(object sender, System.EventArgs e)
        {
            // X:\jsc.svn\examples\javascript\async\Test\TestWorkerProgress\TestWorkerProgress\Application.cs

            // http://blogs.msdn.com/b/dotnet/archive/2012/06/06/async-in-4-5-enabling-progress-and-cancellation-in-async-apis.aspx
            IProgress<string> progress = new Progress<string>(
               x =>
               {
                   button1.Text = new { x, Thread.CurrentThread.ManagedThreadId }.ToString();
               }
           );



            {
                var x = await Task.Factory.StartNew(
                    new { progress },
                    scope =>
                    {
                        Action<string> yield = progress.Report;

                        yield("hi " + new { BackgroundThread = Thread.CurrentThread.ManagedThreadId });

                        Thread.Sleep(1000);

                        yield("almost done");

                        Thread.Sleep(1000);

                        return "done " + new { BackgroundThread = Thread.CurrentThread.ManagedThreadId };
                    }
                );

                button1.Text = new { x, Thread.CurrentThread.ManagedThreadId }.ToString();
            }
        }

    }
}
