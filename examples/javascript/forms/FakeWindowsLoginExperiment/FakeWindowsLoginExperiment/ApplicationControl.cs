using FakeWindowsLoginExperiment;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;

namespace FakeWindowsLoginExperiment
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {
        }

        private void button1_Click_1(object sender, System.EventArgs e)
        {
            new { message = "h1", @this = this }.With(
                x =>
                {
                    MessageBox.Show(x.ToTrace().message);
                }
            );
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            this.ParentForm.Close();
        }

    }
}
