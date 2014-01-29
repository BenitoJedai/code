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
                    // only applies to buttons and labels?

                    //f.flowLayoutPanel1.Padding.All = trackBar1.Value;
                    f.flowLayoutPanel1.Padding = new Padding(trackBar1.Value);
                    this.Padding = new Padding(trackBar1.Value);
                };
        }

        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            f.dataGridView1.RowHeadersVisible = checkBox1.Checked;

        }

        // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.TrackBar.add_Scroll(System.EventHandler)]
        private void trackBar1_Scroll(object sender, System.EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, System.EventArgs e)
        {
            f.dataGridView1.AllowUserToResizeColumns = checkBox2.Checked;

            // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridView.set_AllowUserToResizeColumns(System.Boolean)]

        }

    }
}
