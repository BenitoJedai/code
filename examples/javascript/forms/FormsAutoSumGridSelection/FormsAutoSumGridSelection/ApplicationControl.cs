using FormsAutoSumGridSelection;
using FormsAutoSumGridSelection.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsAutoSumGridSelection
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            // do we only get 1 column?
            //this.dataGridView1.DataSource =
            //    new BindingSource(
            //    MyOtherDataSource.GetData(), ""
            //    );

            //this.dataGridView1.DataSource = new MyDataSource();
            //this.dataGridView1.DataSource = this.myDataSource1;

            //this.myDataSource1.DataSource = typeof(System.Data.DataRow);
            // 
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
