using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace SQLiteWithDataGridView.Library
{
    public partial class GridForm : Form
    {
        public GridForm()
        {
            this.InitializeComponent();

        }

        public string TableName = "SQLiteWithDataGridView_0_Table001";

        ApplicationWebService service = new ApplicationWebService();

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = TableName;

            dataGridView1.Enabled = false;
            service.EnumerateItems("",
                (ContentKey, ContentValue, ContentComment) =>
                {
                    var r = new DataGridViewRow();

                    r.Cells.AddRange(

                        new DataGridViewTextBoxCell
                        {
                            Value = ContentKey
                        },
                        new DataGridViewTextBoxCell
                        {
                            Value = ContentValue
                        },
                        new DataGridViewTextBoxCell
                        {
                            Value = ContentComment
                        }
                    );

                    dataGridView1.Rows.Add(r

                   );

                },
                TableName: TableName,
                done:
                delegate
                {
                    dataGridView1.Enabled = true;

                }
            );
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            // this is the ISNewRow
            //e.Row.Cells[0].Value = "" + dataGridView1.Rows.Count;
        }


        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0)
                return;
            var c0 = dataGridView1[0, e.RowIndex];

            if (string.IsNullOrEmpty((string)c0.Value))
            {
                dataGridView1[0, e.RowIndex].Value = "" + (dataGridView1.Rows.Count - 1);
                dataGridView1[0, e.RowIndex].Style.ForeColor = Color.Red;

                var ContentValue = (string)dataGridView1[1, e.RowIndex].Value;
                if (ContentValue == null)
                    ContentValue = "";

                var ContentComment = (string)dataGridView1[2, e.RowIndex].Value;
                if (ContentComment == null)
                    ContentComment = "";

                service.AddItem(
                    ContentValue,
                    ContentComment,
                    delegate
                    {

                    },
                    TableName: TableName
                );
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



    }

}
