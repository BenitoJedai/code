using FormsAppletButtonForFormExperiment;
using FormsAppletButtonForFormExperiment.Library;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsAppletButtonForFormExperiment
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            button1.Text = "Showing form...";

            // java applet aint showing it, why?
            new ApplicationForm().Show();

            button1.Text = "Showing form... done!";

        }

    }
}
