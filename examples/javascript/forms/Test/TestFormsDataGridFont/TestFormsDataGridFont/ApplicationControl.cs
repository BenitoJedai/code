using TestFormsDataGridFont;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestFormsDataGridFont
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private async void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            var DataTable = await new ApplicationWebService().DoEnterData();
            this.dataGridView1.DataSource = DataTable;

        }

    }
}
