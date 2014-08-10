using AsyncFinally;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncFinally
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private async void button1_Click(object sender, System.EventArgs e)
        {
            // X:\jsc.svn\examples\javascript\forms\BSONExperiment\BSONExperiment\ApplicationControl.cs
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20150520/async-finally
            // 4.5
            // X:\jsc.svn\examples\rewrite\test\TestAsyncFinally\TestAsyncFinally\Program.cs
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140524
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Runtime\ExceptionServices\ExceptionDispatchInfo.cs

            try
            {
                button1.Text = "enter try";

                await Task.Delay(200);

                button1.Text = "exit try";
            }
            finally
            {
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140810/asenumerable

                button1.Text = "enter finally";

                // cannot do that before roslyn
                await Task.Delay(200);

                button1.Text = "exit finally";
            }
        }

    }
}
