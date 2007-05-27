﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsExample.js
{
    using ScriptCoreLib;

    [Script]
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            Console.WriteLine("hello world");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            listBox1.Enabled = checkBox1.Checked;
            comboBox2.Enabled = checkBox1.Checked;
            button3.Enabled = checkBox1.Checked;
            checkBox2.Enabled = checkBox1.Checked;
        }

        private void button2_Enter(object sender, EventArgs e)
        {
            button2.ForeColor = Color.FromArgb(0x00, 0x00, 0x80);
        }

        private void button2_Leave(object sender, EventArgs e)
        {
            button2.ForeColor = Color.FromArgb(0x00, 0xff, 0x00);
        }
    }
}
