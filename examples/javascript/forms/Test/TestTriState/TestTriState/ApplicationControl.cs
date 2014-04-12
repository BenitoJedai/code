using TestTriState;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestTriState
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void checkBox1_CheckStateChanged(object sender, System.EventArgs e)
        {
            // does jsc store enum names yet?
            // X:\jsc.svn\examples\javascript\forms\FormsDualDataSource\FormsDualDataSource\ApplicationControl.cs
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140412
            checkBox1.Text = new {  checkBox1.CheckState }.ToString();

        }

    }
}
