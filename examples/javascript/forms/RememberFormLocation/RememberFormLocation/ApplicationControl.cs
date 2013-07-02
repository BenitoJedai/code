using RememberFormLocation;
using RememberFormLocation.Properties;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RememberFormLocation
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            new Form1().Show();
        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {

        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            new Form().KeepLocationInSettings(Settings.Default, "button2form").Show();

        }

    }
}
