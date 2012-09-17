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

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            if (!checkBox1.Checked)
                return;
            checkBox1.Enabled = false;
            timer1.Enabled = false;

            new ApplicationWebService().CountItems("",
                c =>
                {
                    button2.Text = "Read (" + c + ")";
                    timer1.Enabled = true;
                    checkBox1.Enabled = true;
                }
            );

        }

    }
}
