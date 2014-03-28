using FormsDualDataSource;
using FormsDualDataSource.Design;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsDualDataSource
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            if (checkBox1.Checked)
                this.dataGridView1.DataSource = data;
            else
                this.dataGridView1.DataSource = null;
        }

        private void checkBox2_CheckedChanged(object sender, System.EventArgs e)
        {
            if (checkBox2.Checked)
                this.dataGridView2.DataSource = data;
            else
                this.dataGridView2.DataSource = null;

        }


        DataTable data;

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            data = ScriptedNotifications.GetDataTable();
        }

    }
}
