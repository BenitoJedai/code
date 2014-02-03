using TestLargeDataTable;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;

namespace TestLargeDataTable
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void bindingSource1_CurrentChanged(object sender, System.EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            //http://stackoverflow.com/questions/3042474/when-is-it-worth-using-a-bindingsource

            // X:\jsc.svn\examples\javascript\Test\TestManyTableRowsFromDataTable\TestManyTableRowsFromDataTable\Application.cs

            var s = Stopwatch.StartNew();


            var table = new DataTable();

            //var table = new IHTMLTable { border = 1 };

            var c0 = table.Columns.Add("Column0");

            //var columns = 30;
            var columns = 6;

            for (int i = 1; i < columns; i++)
            {
                var c1 = table.Columns.Add("Column" + i);
            }


            //table.AttachToDocument();
            //var tbody = table.AddBody();

            //tbody.css.odd.style.backgroundColor = "gray";
            //var count = 40000;
            //var count = 1000;
            // 1423ms event: dataGridView1 set DataSource { SourceDataTableColumnCount = 6, SourceDataTableRowCount = 1000, ElapsedMilliseconds = 1010, a = 1.008991008991009 } 

            //var count = 10000;
            // 8465ms event: dataGridView1 set DataSource { SourceDataTableColumnCount = 6, SourceDataTableRowCount = 10000, ElapsedMilliseconds = 7765, a = 0.7764223577642235 } 

            var count = 32;
            //var count = 1000;

            // 655ms event: dataGridView1 set DataSource { SourceDataTableColumnCount = 6, SourceDataTableRowCount = 100, ElapsedMilliseconds = 305, a = 3.01980198019802 } 
            // 4928ms event: dataGridView1 set DataSource { SourceDataTableColumnCount = 6, SourceDataTableRowCount = 100, ElapsedMilliseconds = 352, a = 3.485148514851485 } 

            // 455ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 8, ElapsedMilliseconds = 168, a = 18.666666666666668 } 

            for (int i = 0; i < count; i++)
            {
                var tr = table.NewRow();

                tr[0] = new { i }.ToString();

                for (int ci = 1; ci < columns; ci++)
                    tr[ci] = new { count, ci, s.ElapsedMilliseconds }.ToString();

                table.Rows.Add(tr);
            }



            //this.bindingSource1.DataSource = table;
            //this.dataGridView1.Refresh();

            this.dataGridView1.DataSourceChanged +=
                delegate
                {
                    s.Stop();

                    // 541ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 29, ElapsedMilliseconds = 280, a = 9.333333333333334 } 
                    // 890ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 98, ElapsedMilliseconds = 606, a = 6.121212121212121 } 
                    // 1029ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 98, ElapsedMilliseconds = 741, a = 7.484848484848484 } 
                    // 6566ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 998, ElapsedMilliseconds = 6252, a = 6.258258258258258 } 

                    // 293ms event: dataGridView1 set DataSource{ SourceRowIndex = 9, ElapsedMilliseconds = 129 }
                    // 663ms event: dataGridView1 set DataSource{ SourceRowIndex = 99, ElapsedMilliseconds = 498 } 
                    // 689ms event: dataGridView1 set DataSource { SourceRowIndex = 99, ElapsedMilliseconds = 533, a = 5.33 } 
                    // 4550ms event: dataGridView1 set DataSource { SourceRowIndex = 999, ElapsedMilliseconds = 4357, a = 4.357 } 

                    // 1899ms event: dataGridView1 set DataSource { SourceRowIndex = 9, ElapsedMilliseconds = 1720, a = 172 } 

                    // 281ms event: dataGridView1 set DataSource { SourceRowIndex = 9, ElapsedMilliseconds = 110, a = 11 } 
                    // 519ms event: dataGridView1 set DataSource { SourceRowIndex = 99, ElapsedMilliseconds = 307, a = 3.07 }
                    // 2625ms event: dataGridView1 set DataSource { SourceRowIndex = 999, ElapsedMilliseconds = 2421, a = 2.421 } 
                    // 2862ms event: dataGridView1 set DataSource { ColumnIndex = 3, SourceRowIndex = 999, ElapsedMilliseconds = 2654, a = 2.654 }
                    // 2841ms event: dataGridView1 set DataSource { ColumnIndex = 3, SourceRowIndex = 999, ElapsedMilliseconds = 2615, a = 2.615 }
                    // 2605ms event: dataGridView1 set DataSource { ColumnIndex = 3, SourceRowIndex = 999, ElapsedMilliseconds = 2390, a = 2.39 } 
                    // 2582ms event: dataGridView1 set DataSource { ColumnIndex = 3, SourceRowIndex = 999, ElapsedMilliseconds = 2375, a = 2.375 } 
                    // 386ms event: dataGridView1 set DataSource { ColumnIndex = 3, SourceRowIndex = 15, ElapsedMilliseconds = 212, a = 13.25 } 
                    // 476ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 8, ElapsedMilliseconds = 307, a = 34.111111111111114 } 
                    // 420ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 8, ElapsedMilliseconds = 244, a = 27.11111111111111 } 

                    // 1412ms event: dataGridView1 set DataSource { ColumnIndex = 6, SourceRowIndex = 8, ElapsedMilliseconds = 702, a = 78 } 

                    // 10: 6ms
                    this.ParentForm.Text = (count + ": " + s.ElapsedMilliseconds + "ms");
                };


            this.dataGridView1.DataSource = table;

        }

    }
}
