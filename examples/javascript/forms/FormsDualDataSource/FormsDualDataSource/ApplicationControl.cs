using FormsDualDataSource;
using FormsDualDataSource.Design;
using System;
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
                this.dataGridView2.DataSource = dataTable;
            else
                this.dataGridView2.DataSource = null;

        }


        DataTable dataTable;

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            dataTable = ScriptedNotifications.GetDataTable();

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140412
            //data.pos

            this.bindingSource1.DataSource = dataTable;
        }

        //arg[0] is typeof System.EventHandler
        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.BindingSource.add_PositionChanged(System.EventHandler)]
        //script: warning JSC1000: Did you reference ScriptCoreLib via IAssemblyReferenceToken?
        //script: error JSC1000: error at FormsDualDataSource.ApplicationControl.InitializeComponent,
        // assembly: U:\FormsDualDataSource.Application.exe
        // type: FormsDualDataSource.ApplicationControl, FormsDualDataSource.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
        // offset: 0x02a8
        //  method:Void InitializeComponent()

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
                checkBox1.Text = ("DataSource bindingSource1");
                this.dataGridView1.DataSource = this.bindingSource1;
            }
            else if (checkBox1.CheckState == CheckState.Indeterminate)
            {
                this.dataGridView1.DataSource = null;
                checkBox1.Text = ("DataSource dataTable");
                this.dataGridView1.DataSource = dataTable;
            }
            else
            {
                checkBox1.Text = ("DataSource null");
                this.dataGridView1.DataSource = null;
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //Additional information: Index was out of range. Must be non-negative and less than the size of the collection.
            //if (dataGridView1.SelectedRows.Count == 0)
            //    this.ParentForm.Text = "none";
            //else
            //    this.ParentForm.Text = "" + new { dataGridView1.SelectedRows[0].Index };

        }

    }
}
