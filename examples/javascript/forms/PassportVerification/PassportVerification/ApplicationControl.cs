using PassportVerification;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace PassportVerification
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private async void button1_Click(object sender, System.EventArgs e)
        {
            // Warning	4	This async method lacks 'await' operators and will run 
            // synchronously. Consider using the 'await' operator to 
            // await non-blocking API calls, or 'await Task.Run(...)' to 
            // do CPU-bound work on a background thread.	


            //button1.Enabled = false;

            //Func<System.Threading.Tasks.Task> countdown = async delegate
            //{
            //    // http://blogs.msdn.com/b/pfxteam/archive/2011/01/13/10115642.aspx


            //    button1.Text = "Please wait 3...";

            await TimeSpan.FromSeconds(1);

            button1.Text = "Please wait 2...";

            await TimeSpan.FromSeconds(1);

            button1.Text = "Please wait 1...";

            //    await TimeSpan.FromSeconds(1);
            //};

            //await countdown();

            //button1.Text = "Connecting...";
            //// refresh?
            ////await System.Threading.Tasks.Task.Yield();

            //// Error	1	'PassportVerification.AsyncApplicationWebService.WebMethod2(string, System.Action<string>)' 
            //// does not return a Task and cannot be awaited. Consider changing 
            //// it to return Task.	

            //await this.asyncApplicationWebService1.WebMethod2(
            //    this.textBox1.Text,
            //    x => MessageBox.Show(x)
            //);

            //button1.Text = "Verify";

            //button1.Enabled = true;
        }

    }

    static class X
    {
        public static TaskAwaiter<object> GetAwaiter(this TimeSpan timeSpan)
        {
            var s = new System.Threading.Tasks.TaskCompletionSource<object>();
            var t = new Timer();

            t.Interval = (int)timeSpan.TotalMilliseconds;

            t.Tick += delegate
            {
                t.Stop();
                s.SetResult(null);
            };

            t.Start();

            return s.Task.GetAwaiter();
        }
    }
}
