using FormsDataGridViewDeleteRow;
using FormsDataGridViewDeleteRow.Design;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsDataGridViewDeleteRow
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            var data = ScriptedNotifications.GetDataTable();


            this.dataGridView1.DataSource = data;


        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            Console.WriteLine("dataGridView1_UserAddedRow " + new { e.Row.Index });
        }

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            Console.WriteLine("dataGridView1_UserDeletedRow " + new { e.Row.Index });
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine("dataGridView1_CellValueChanged " + new { e.RowIndex });
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridViewCellValidatingEventArgs.get_ColumnIndex()]
            Console.WriteLine(new { e.ColumnIndex, e.RowIndex });

            if (e.ColumnIndex == 0)
            {
                var i = 0;

                // X:\jsc.svn\examples\javascript\Test\TestIntTryParse\TestIntTryParse\Application.cs

                var value =
                    (string)e.FormattedValue;

                Console.WriteLine(new { e.ColumnIndex, e.RowIndex, value });
                var ok = int.TryParse(value, out i);

                if (ok)
                {
                    this.dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.White;

                    return;
                }

                Console.WriteLine(new { e.ColumnIndex, e.RowIndex, value, ok });

                // { ColumnIndex = 0, RowIndex = 2, value = u, ok = false } 
                this.dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Yellow;
                e.Cancel = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
