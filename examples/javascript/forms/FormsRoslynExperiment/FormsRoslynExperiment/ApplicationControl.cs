using FormsRoslynExperiment;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsRoslynExperiment
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            new ApplicationWebService().WebMethod2(
                textBox1.Text,
                output =>
                {
                    textBox2.Text = output;
                }
            );
        }

        private void textBox2_TextChanged(object sender, System.EventArgs e)
        {

        }

    }
}
