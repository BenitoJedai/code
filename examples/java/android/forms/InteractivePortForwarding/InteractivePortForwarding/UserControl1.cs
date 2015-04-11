using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InteractivePortForwarding
{
	public partial class UserControl1 : UserControl
	{
		public UserControl1()
		{
			InitializeComponent();
		}

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("about to start. " + new {

                external = textBox1.Text,
                @internal = textBox2.Text,
            
            });
        }
	}
}
