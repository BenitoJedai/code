using AndroidPrivateAddress;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;

namespace AndroidPrivateAddress
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private async void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            var x = await this.applicationWebService1.GetInterfaces();


            this.dataGridView1.DataSource = x;


        }

        private void dataGridView1_SelectionChanged(object sender, System.EventArgs e)
        {


        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.applicationWebService1.SelectionChanged(
                (string)dataGridView1[0, e.RowIndex].Value
            );
        }

    }
}
