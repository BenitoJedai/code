using FormsProjectTemplateExperiment;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsProjectTemplateExperiment
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            this.ParentForm.Text = this.textBox1.Text;
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\JavaScript\Extensions\FormExtensions.cs

            this.ParentForm.FormBorderStyle = FormBorderStyle.Sizable;
            this.ParentForm.WindowState = FormWindowState.Normal;

        }

    }
}
