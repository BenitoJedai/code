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
            this.WindowState = FormWindowState.Maximized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var f = new Form1
            {
                Owner = this
            };

            f.checkBox5.Checked = false;
            f.Show();
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

        bool InternalGoingFullscreen;
        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            if (InternalGoingFullscreen)
                return;

            if (checkBox5.Checked)
                if (this.WindowState == FormWindowState.Maximized)
                {
                    if (this.FormBorderStyle != System.Windows.Forms.FormBorderStyle.None)
                    {
                        InternalGoingFullscreen = true;
                        this.MaximumSize = this.DefaultMaximumSize;

                        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

                        this.WindowState = FormWindowState.Normal;
                        this.WindowState = FormWindowState.Maximized;

                        InternalGoingFullscreen = false;
                    }
                }
                else if (this.WindowState == FormWindowState.Normal)
                {
                    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Enabled = !checkBox5.Checked;
            this.WindowState = FormWindowState.Normal;
        }



    }

}
