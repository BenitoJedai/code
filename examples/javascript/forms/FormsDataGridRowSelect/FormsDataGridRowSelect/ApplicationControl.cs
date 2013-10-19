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

            //Additional information: Column's SortMode cannot be set to Automatic while the DataGridView control's SelectionMode is set to ColumnHeaderSelect.

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

    }
}
