using ChromeTCPDataGrid;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChromeTCPDataGrid
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
                        var x = await new ApplicationWebService { }.DoEnterData();

            this.dataGridView1.DataSource = x;
        }

    }
}
