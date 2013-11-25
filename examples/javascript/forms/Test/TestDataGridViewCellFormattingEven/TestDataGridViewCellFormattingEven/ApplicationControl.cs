using TestDataGridViewCellFormattingEven;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            // http://msdn.microsoft.com/en-us/library/system.windows.forms.datagridview.cellformatting(v=vs.110).aspx

            if (e.Value == null)
                return;

            if (e.Value is System.DBNull)
                return;

            // Additional information: Unable to cast object of type 'System.DBNull' to type 'System.String'.

            // Check for the string "pink" in the cell.
            string stringValue = (string)e.Value;
            stringValue = stringValue.ToLower();
            if ((stringValue.IndexOf("pink") > -1))
            {
                e.CellStyle.BackColor = Color.Pink;
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

    }
}
