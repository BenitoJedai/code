using OrcasSimpleWindowsFormsApplication;
using SimpleWindowsFormsApplication;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimpleWindowsFormsApplication
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            new Form2().Show();
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            var context = new[]
			{
				panel1,
				panel2,
				panel3,
				panel4,
				panel5,
				panel6,
			};

            var _BackColor = context[0].BackColor;

            for (int i = 0; i < context.Length - 1; i++)
            {
                context[i].BackColor = context[i + 1].BackColor;
            }

            context[context.Length - 1].BackColor = _BackColor;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("hello world");

        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("hi");

        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show(
@"Contact jsc developers and ask for it!

Development blog:
http://zproxy.wordpress.com");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show(
@"We could provide more controls just for you! Ask for more features!

Development blog:
http://zproxy.wordpress.com");
        }

    }
}
