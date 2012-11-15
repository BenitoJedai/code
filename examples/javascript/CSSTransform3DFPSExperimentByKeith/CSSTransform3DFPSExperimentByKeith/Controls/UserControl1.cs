﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CSSTransform3DFPSExperimentByKeith.Controls
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var cf = new Form1();

            cf.Show();

            cf.FormClosing +=
                (ss, ee) =>
                {
                    if (cf.WindowState == FormWindowState.Normal)
                    {
                        if (ee.CloseReason == CloseReason.UserClosing)
                        {
                            ee.Cancel = true;
                            cf.WindowState = FormWindowState.Minimized;
                        }
                    }
                };
        }
    }
}
