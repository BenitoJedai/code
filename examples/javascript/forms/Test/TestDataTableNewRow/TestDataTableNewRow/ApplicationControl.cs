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
                    Console.WriteLine("ColumnChanged " + new { a.Row, a.Column, a.ProposedValue });
                };

            DataTable.TableNewRow +=
                (s, a) =>
                {
                    Console.WriteLine("TableNewRow " + new { a.Row });

                };
            this.dataGridView1.DataSource = DataTable;
        }

    }
}
