using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace com.abstractatech.multiscreen.formsexample
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            this.InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.button1.ForeColor = Color.Blue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;

            var s = new ApplicationXWebService();

            s.AddItem(textBox1.Text,
                k =>
                {
                    this.Close();
                }
            );
        }



    }

}
