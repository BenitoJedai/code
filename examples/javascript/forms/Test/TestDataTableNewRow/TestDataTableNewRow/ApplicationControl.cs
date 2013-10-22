using TestDataTableNewRow;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System;

namespace TestDataTableNewRow
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private async void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            var DataTable = await applicationWebService1.DoEnterData();

            init(DataTable);
        }

        private void init(System.Data.DataTable DataTable)
        {
            // cut handlers
            //DataTable = DataTable.Clone();

            //            TableNewRow { Row = System.Data.DataRow }
            //ColumnChanged { Row = System.Data.DataRow, Column = Column 2, ProposedValue = fg }
            //ColumnChanged { Row = System.Data.DataRow, Column = Column 1, ProposedValue = 4 }
            //TableNewRow { Row = System.Data.DataRow }

            Console.WriteLine("add ColumnChanged ");

            DataTable.ColumnChanged +=
                (s, a) =>
                {
                    Console.WriteLine("ColumnChanged " + new { RowIndexOf = DataTable.Rows.IndexOf(a.Row), a.Column, a.ProposedValue });
                };
            //add ColumnChanged
            //Server TableNewRow { Row = System.Data.DataRow }
            //TableNewRow { RowIndexOf = -1 }
            //Server ColumnChanged { Row = System.Data.DataRow, Column = Column 2, ProposedValue = x }
            //ColumnChanged { RowIndexOf = -1, Column = Column 2, ProposedValue = x }
            //Server TableNewRow { Row = System.Data.DataRow }
            //TableNewRow { RowIndexOf = -1 }



            DataTable.TableNewRow +=
                (s, a) =>
                {
                    Console.WriteLine("TableNewRow " + new { RowIndexOf = DataTable.Rows.IndexOf(a.Row) });

                };
            this.dataGridView1.DataSource = DataTable;


            button1.Click +=
                delegate
                {
                    // resync
                    this.dataGridView1.DataSource = DataTable;
                };
        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.dataGridView1.DataSource = null;
        }

    }
}
