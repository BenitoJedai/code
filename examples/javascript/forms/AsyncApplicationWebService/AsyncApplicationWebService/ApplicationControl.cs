using AsyncApplicationWebService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncApplicationWebService
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            Console.WriteLine(new { Thread.CurrentThread.ManagedThreadId });

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            Console.WriteLine(new { Thread.CurrentThread.ManagedThreadId });

            var gui = TaskScheduler.FromCurrentSynchronizationContext();

            // GUI to backround
            await Task.Delay(1).ConfigureAwait(false);

            Console.WriteLine(new { Thread.CurrentThread.ManagedThreadId });

            //{ ManagedThreadId = 3 }
            //{ ManagedThreadId = 5 }

            this.applicationWebService1.WebMethod2(
                "foo",
                async y =>
                {

                    Console.WriteLine("got y: " + new { y, Thread.CurrentThread.ManagedThreadId });

                    // Additional information: The current SynchronizationContext may not be used as a TaskScheduler.


                    await Task.Factory.StartNew(
                        delegate
                        {
                            Console.WriteLine("before GUI got y: " + new { y, Thread.CurrentThread.ManagedThreadId });



                        },
                        default(object),
                        default(CancellationToken),
                        TaskCreationOptions.LongRunning,
                        gui
                    );

                    //Console.WriteLine("GUI got y: " + new { y, Thread.CurrentThread.ManagedThreadId });
                }
            );

            Console.WriteLine("will wait for service... " + new { Thread.CurrentThread.ManagedThreadId });

        }

    }
}
