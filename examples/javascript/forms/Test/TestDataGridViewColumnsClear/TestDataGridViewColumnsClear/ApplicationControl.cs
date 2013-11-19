using TestDataGridViewColumnsClear;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace TestDataGridViewColumnsClear
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Columns.Clear();
        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            var col = new DataColumn();
            col.ColumnName = "Test1";
            var col2 =  new DataColumn();
            col2.ColumnName = "Test2";

            var table = new DataTable { TableName = "DoEnterData" };

            table.Columns.Add(col);
            table.Columns.Add(col2);

            var row = table.NewRow();
            row[col] = "test";
            row[col2] = "test2";

            table.Rows.Add(row);

            dataGridView1.DataSource = table;
        }

    }
}
