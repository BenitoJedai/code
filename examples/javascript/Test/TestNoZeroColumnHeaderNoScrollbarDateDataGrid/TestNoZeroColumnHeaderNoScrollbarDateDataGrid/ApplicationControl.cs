using TestNoZeroColumnHeaderNoScrollbarDateDataGrid;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestNoZeroColumnHeaderNoScrollbarDateDataGrid
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void ApplicationControl_Load(object sender, System.EventArgs e)
        {

            var data = await new ApplicationWebService().DoEnterData();

            this.dataGridView1.DataSource = data;
        }

        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            this.dataGridView1.RowHeadersVisible =
                checkBox1.Checked;
        }

    }
}
