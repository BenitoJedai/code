using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MichaelVincentProgramManager
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


        bool InternalGoingFullscreen;
        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            if (InternalGoingFullscreen)
                return;

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

        private void Form1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;

        }


     



    }

 }
