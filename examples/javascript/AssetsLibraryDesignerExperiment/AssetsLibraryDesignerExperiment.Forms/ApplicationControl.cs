using AssetsLibraryDesignerExperiment.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AssetsLibraryDesignerExperiment.Forms
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            applicationWebService1.WebMethod2(
                "hi",
                x => MessageBox.Show(x)
            );
        }

        private void fooToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            applicationWebService1.WebMethod2(
                "foo",
                x => MessageBox.Show(x)
            );
        }

    }
}
