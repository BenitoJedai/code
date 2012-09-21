using TestFormsDatagrid;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System;

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
            var rr = this.dataGridView1.Rows.Count;

            for (int i = 0; i < 7; i++)
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
            label1.Text = new { dataGridView1.SelectedCells.Count }.ToString();

        }

        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            dataGridView1.MultiSelect = checkBox1.Checked;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine("dataGridView1_CellEndEdit");
            label2.Text = "dataGridView1_CellEndEdit: " + new { e.ColumnIndex, e.RowIndex }.ToString();

            dataGridView1[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.Blue;
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            label2.Text = "dataGridView1_CellBeginEdit: " + new { e.ColumnIndex, e.RowIndex }.ToString();
            dataGridView1[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.Red;

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine("dataGridView1_CellValueChanged");
            label2.Text = "changed: " + new { e.ColumnIndex, e.RowIndex }.ToString();

        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            label1.Text = "new row: " + new { e.Row.Index }.ToString();

        }

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            label1.Text = "deleted row: " + new { e.Row.Index }.ToString();

        }

        private void label2_Click(object sender, System.EventArgs e)
        {

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Column cannot be added because its CellType property is null.
            this.dataGridView1.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    HeaderText = textBox2.Text,
                    //CellType = 
                }
            );
        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            label3.Text = "dataGridView1_ColumnAdded: " + e.Column.HeaderText;
        }

    }
}
