using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace com.abstractatech.multiscreen.formsexample.Library
{
    public partial class MandelbrotForm : Form
    {
        public MandelbrotForm()
        {
            this.InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }



        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            mandelbrotComponent1.timer1.Enabled = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            mandelbrotComponent2.timer1.Enabled = checkBox2.Checked;
        }



    }

}
