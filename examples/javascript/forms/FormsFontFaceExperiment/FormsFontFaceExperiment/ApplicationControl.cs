using FormsFontFaceExperiment;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsFontFaceExperiment
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            label1.Text = label1.Font.Name;
            label2.Text = label2.Font.Name;
            label3.Text = label3.Font.Name;
        }

    }
}
