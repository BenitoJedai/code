using ChromeAppWindowFrameNoneExperiment;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChromeAppWindowFrameNoneExperiment
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            new Form().Show();

        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {

        }

        private void ApplicationControl_ParentChanged(object sender, System.EventArgs e)
        {

            this.ParentForm.Text = "ParentChanged";
        }

    }
}
