using FormsDataGridForXElement;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsDataGridForXElement
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private async void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            var source = await this.applicationWebService1.GetDataSource();
            System.Console.WriteLine(source);
            //this.dataGridView1.Rows.Add();
            this.dataGridView1.Columns.Add("Col1", "col1value");
            DataGridViewRow row = new DataGridViewRow();
            row.Cells.Add(new DataGridViewTextBoxCell{Value="test"});
            this.dataGridView1.Rows.Add(row);
            
        }

    }
}
