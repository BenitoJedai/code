using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Designer1Forms
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "hello!!";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "world!!";

        }
    }
}
