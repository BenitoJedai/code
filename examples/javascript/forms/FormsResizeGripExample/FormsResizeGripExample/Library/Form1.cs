using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace FormsResizeGripExample.Library
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.MaximumSize = this.DefaultMaximumSize;

            var x = this.DefaultMinimumSize;

            this.WindowState = FormWindowState.Maximized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Form1
            {
                Owner = this
            }.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.ControlBox = checkBox1.Checked;


        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox2.Checked)
            {
                this.BackColor = SystemColors.ButtonFace;

                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;

            }
            else
            {
                this.BackColor = Color.Red;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
                this.Text = "Form1";
            else
                this.Text = "";
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = checkBox4.Checked;
        }



    }

}
