using AccountExperiment.MyDevicesComponent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AccountExperiment.MyDevicesComponent
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            var f = new MyDevicesComponent.Library.MyDevicesForm
            {
                service = new ApplicationWebService
                {
                    account = int.Parse(this.textBox1.Text)
                }
            };


            f.Show();
        }

    }
}
