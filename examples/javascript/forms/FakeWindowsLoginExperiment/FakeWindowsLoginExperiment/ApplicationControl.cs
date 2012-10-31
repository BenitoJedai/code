using FakeWindowsLoginExperiment;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FakeWindowsLoginExperiment
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            new Library.MDIParent1().Show();
        }

    }
}
