using TestDataTableToJavascript;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;

namespace TestDataTableToJavascript
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private async void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            var table = await this.applicationWebService1.GetQueryResultAsDataTable();
            this.dataGridView1.DataSource = table;
        }

    }
}
