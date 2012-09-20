using TestFormsDatagrid;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestFormsDatagrid
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
            for (int i = 0; i < 60; i++)
            {
                var r = new DataGridViewRow();
                r.Height = 60;

                for (int j = 0; j < 7; j++)
                {
                    r.Cells.AddRange(
                       new DataGridViewTextBoxCell
                       {
                           Value = "world #" + i + "," + j
                       }
                   );


                }


                this.dataGridView1.Rows.AddRange(r);
            }

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            var r = new DataGridViewRow();

            r.Cells.AddRange(
                new DataGridViewTextBoxCell
                {
                    Value = "foo #" + this.dataGridView1.Rows.Count
                }
            );

            r.Cells.AddRange(
             new DataGridViewTextBoxCell
             {
                 Value = textBox1.Text
             }
         );

            this.dataGridView1.Rows.AddRange(r);
        }

        private void dataGridView1_SelectionChanged(object sender, System.EventArgs e)
        {
            //label1.Text = new { dataGridView1.SelectedCells.Count }.ToString();

        }

        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            dataGridView1.MultiSelect = checkBox1.Checked;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.Blue;
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            dataGridView1[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.Red;

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            label1.Text = new { e.ColumnIndex, e.RowIndex }.ToString();

        }

    }
}
