using TestWebWorker;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;

namespace TestWebWorker
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private async void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            // how do we start a task?
            var value = await Task.Factory.StartNew(
                new { UIThread = Thread.CurrentThread.ManagedThreadId },
                state =>
                {
                    // slow it down
                    Thread.Sleep(1000);

                    return new { state.UIThread, WorkerThread = Thread.CurrentThread.ManagedThreadId };
                }
            );


            // toString was lost by thread boundary!
            //this.ParentForm.Text = new { value }.ToString();

            // would we need to repackage the anonymous type to regain metadata?
            this.ParentForm.Text = new { value.UIThread, value.WorkerThread }.ToString();
        }
    }
}
