using TestFlowDataGridPadding;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestFlowDataGridPadding
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        public Form1 f = new Form1();

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {

            f.Show();

            trackBar1.ValueChanged +=
                delegate
                {

                    //f.flowLayoutPanel1.Padding.All = trackBar1.Value;
                    f.flowLayoutPanel1.Padding = new Padding(trackBar1.Value);
                };
        }

    }
}
