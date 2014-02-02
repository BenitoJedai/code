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
            var columns = 2;

            for (int i = 1; i < columns; i++)
            {
                var c1 = table.Columns.Add("Column" + i);
            }


            //table.AttachToDocument();
            //var tbody = table.AddBody();

            //tbody.css.odd.style.backgroundColor = "gray";
            //var count = 40000;
            var count = 10;

            for (int i = 0; i < count; i++)
            {
                var tr = table.NewRow();

                tr[0] = new { i }.ToString();

                for (int ci = 1; ci < columns; ci++)
                    tr[ci] = new { count, ci, s.ElapsedMilliseconds }.ToString();

                table.Rows.Add(tr);
            }



            //this.bindingSource1.DataSource = table;
            this.dataGridView1.DataSource = table;
            //this.dataGridView1.Refresh();

            s.Stop();


            // 293ms event: dataGridView1 set DataSource{ SourceRowIndex = 9, ElapsedMilliseconds = 129 }
            // 663ms event: dataGridView1 set DataSource{ SourceRowIndex = 99, ElapsedMilliseconds = 498 } 
            // 689ms event: dataGridView1 set DataSource { SourceRowIndex = 99, ElapsedMilliseconds = 533, a = 5.33 } 
            // 4550ms event: dataGridView1 set DataSource { SourceRowIndex = 999, ElapsedMilliseconds = 4357, a = 4.357 } 

            // 1899ms event: dataGridView1 set DataSource { SourceRowIndex = 9, ElapsedMilliseconds = 1720, a = 172 } 
            
            // 281ms event: dataGridView1 set DataSource { SourceRowIndex = 9, ElapsedMilliseconds = 110, a = 11 } 

            // 10: 6ms
            this.ParentForm.Text = (count + ": " + s.ElapsedMilliseconds + "ms");


        }

    }
}
