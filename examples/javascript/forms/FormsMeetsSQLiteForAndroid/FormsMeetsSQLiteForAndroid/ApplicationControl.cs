using FormsMeetsSQLiteForAndroid;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsMeetsSQLiteForAndroid
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            new Form2().Show();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            new Form3().Show();
        }

    }
}
