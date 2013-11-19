using TestDataGridDatasourceChange;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestDataGridDatasourceChange
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private async void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            var temp = await this.applicationWebService1.EnterData();
            this.dataGridView1.DataSource = temp;
        }

        private async void button1_Click(object sender, System.EventArgs e)
        {
            var temp = await this.applicationWebService1.EnterData2();
            this.dataGridView1.DataSource = temp;
        }

    }
}
