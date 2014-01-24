using TestDataGridPadding;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestDataGridPadding
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

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            //dataGridView1.ColumnHeadersVisible = false;
            //dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;

            dataGridView1.DataSourceChanged += delegate
            {
                dataGridView1.Columns[0].Width = 300;

                // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridViewBand.set_Visible(System.Boolean)]
                dataGridView1.Columns["Tag"].Visible = false;

                // this will not change the dbl click
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            };

            dataGridView1.DataSource = TestDataGridPadding.Design.Book1.GetDataSet().Tables[0];


        }

    }
}
