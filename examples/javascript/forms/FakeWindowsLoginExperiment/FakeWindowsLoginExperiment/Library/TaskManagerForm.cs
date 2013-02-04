using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FakeWindowsLoginExperiment.Library
{
    [ToolboxItem(true)]
    [DesignTimeVisible(true)]
#if DEBUG
    [Designer(typeof(MyControlDesigner))]
#endif
    public partial class TaskManagerForm : Form
    {
        public TaskManagerForm()
        {
            InitializeComponent();
        }

        private void TaskManagerForm_Load(object sender, EventArgs e)
        {
            var r = new DataGridViewRow();
            r.Cells.Add(new DataGridViewTextBoxCell { Value = "App.exe" });
            r.Cells.Add(new DataGridViewTextBoxCell { Value = "2004" });
            r.Cells.Add(new DataGridViewTextBoxCell { Value = "Guest" });
            r.Cells.Add(new DataGridViewTextBoxCell { Value = "02" });
            r.Cells.Add(new DataGridViewTextBoxCell { Value = "100,000K" });
            this.dataGridView1.Rows.Add(
                r
            );
        }
    }
}
