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

    }
}
