using TestDataGridViewClear;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System;

namespace TestDataGridViewClear
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            Action<string> Add =
                text =>
                {
                    var r = new DataGridViewRow();

                    r.Cells.AddText(text);

                    this.dataGridView1.Rows.Add(r);
                };

            Add("foo1");
            Add("foo2");
            Add("foo3");
            Add("foo4");
            Add("foo5");

            int i = 5;

            button2.Click +=
                delegate
                {
                    i++;
                    Add("foox " + i);
                };
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (this.dataGridView1.Rows.Count > 3)
                this.dataGridView1.Rows.RemoveAt(1);
            else
                this.dataGridView1.Rows.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // cstarts at 0
            if (this.dataGridView1.Columns.Count > 0)
                this.dataGridView1.Columns.RemoveAt(this.dataGridView1.Columns.Count -1);

            // script: error JSC1000: No implementation found for this native method, please implement
            // [System.Windows.Forms.DataGridViewColumnCollection.RemoveAt(System.Int32)]

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

    }
}
