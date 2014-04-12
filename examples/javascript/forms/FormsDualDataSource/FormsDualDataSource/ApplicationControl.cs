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

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140412
            //data.pos

            this.bindingSource1.DataSource = data;
        }

        private void bindingSource1_PositionChanged(object sender, System.EventArgs e)
        {
            this.ParentForm.Text = new
            {
                // if datagrids are bound by DataTable
                // bindingSource1 wont know the position.
                this.bindingSource1.Position
            }.ToString();

        }

        private void checkBox1_CheckStateChanged(object sender, System.EventArgs e)
        {
            // http://stackoverflow.com/questions/1726096/tri-state-check-box-in-html

            // http://msdn.microsoft.com/en-us/library/system.windows.forms.checkbox.threestate(v=vs.110).aspx
            //checkBox1.ThreeState
            if (checkBox1.CheckState == CheckState.Checked)
            {
                this.dataGridView1.DataSource = null;
                this.dataGridView1.DataSource = this.bindingSource1;
            }
            else if (checkBox1.CheckState == CheckState.Indeterminate)
            {
                this.dataGridView1.DataSource = null;
                this.dataGridView1.DataSource = data;
            }
            else
                this.dataGridView1.DataSource = null;
        }

    }
}
