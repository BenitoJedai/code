using TestDataGridViewCellFormattingEven;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System;

namespace TestDataGridViewCellFormattingEven
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            this.dataGridView1.DataSource = TestDataGridViewCellFormattingEven.Design.Foo1.GetDataSet();
            this.dataGridView1.DataMember = "Sheet1";
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // no implementation for System.Windows.Forms.ConvertEventArgs 4d8976c9-2008-3949-ae6b-4b26fc4fddeb
            //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.ConvertEventArgs.get_Value()]

            // http://msdn.microsoft.com/en-us/library/system.windows.forms.datagridview.cellformatting(v=vs.110).aspx

            if (e.Value == null)
                return;

            if (e.Value is System.DBNull)
                return;

            // when will it be null??
            if (e.CellStyle == null)
            {
                //Console.WriteLine(
                //    "dataGridView1_CellFormatting " + new { e.RowIndex, e.ColumnIndex }
                //    );

                return;
            }

            // Additional information: Unable to cast object of type 'System.DBNull' to type 'System.String'.

            // Check for the string "pink" in the cell.
            string stringValue = (string)e.Value;
            stringValue = stringValue.ToLower();
            if ((stringValue.IndexOf("pink") > -1))
            {
                // script: error JSC1000: No implementation found for this native method, please implement [static System.Drawing.Color.get_Pink()]

                //Color.Pink
                // script: error JSC1000: No implementation found for this native method, please implement [static System.Drawing.Color.FromArgb(System.Int32)]
                //e.CellStyle.BackColor = Color.FromArgb(0xffc0cb);
                e.CellStyle.BackColor = Color.FromArgb(0xff, 0xc0, 0xcb);

                // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridView.add_CellFormatting(System.Windows.Forms.DataGridViewCellFormattingEventHandler)]
            }

            if (stringValue.Contains("sans"))
            {
                e.CellStyle.Font = this.label1.Font;
                e.CellStyle.ForeColor = Color.Yellow;
                e.CellStyle.BackColor = Color.Blue;
            }


            var i = 0;

            if (int.TryParse(stringValue, out i))
            {
                if (i <= 0)
                {
                    e.CellStyle.ForeColor = Color.Red;
                }
                else
                {
                    e.CellStyle.ForeColor = Color.Blue;
                }
            }

        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex < 0)
                return;

            var c = this.dataGridView1[
                  e.ColumnIndex, e.RowIndex
              ];


            var s = c.Style;

            var old = s.Font;

            s.Font = new Font(s.Font, FontStyle.Underline);

            dataGridView1.CellMouseLeave +=
                delegate
                {
                    if (old == null) return;

                    s.Font = old;
                    old = null;

                };

        }



    }
}
