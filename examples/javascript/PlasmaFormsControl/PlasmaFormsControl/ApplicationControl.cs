using PlasmaFormsControl;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PlasmaFormsControl
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            plasmaControl1.timer1.Enabled = checkBox1.Checked;

        }

        private void checkBox2_CheckedChanged(object sender, System.EventArgs e)
        {
            plasmaControl2.timer1.Enabled = checkBox2.Checked;

        }

    }
}
