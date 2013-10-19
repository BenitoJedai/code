using FormsDataGridRowSelect;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsDataGridRowSelect
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private async void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            var x = await new ApplicationWebService { }.DoEnterData();

            this.dataGridView1.DataSource = x;

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show(
                new { e.RowIndex }.ToString()
            );
        }

        private void dataGridView1_SelectionChanged(object sender, System.EventArgs e)
        {
            //this.ParentForm.Text = new { dataGridView1.SelectedRows.Count }.ToString();
            this.ParentForm.Text = new { dataGridView1.SelectedCells.Count }.ToString();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
