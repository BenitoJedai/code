using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsExample.js
{
    [ScriptCoreLib.Script]
    public partial class UserControl3 : UserControl
    {
        public UserControl3()
        {
            InitializeComponent();
        }

        int i = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {

            i++;

            if (IsHot)
                this.button1.Text = "<" + i + ">";
            else
                this.button1.Text = "#" + i;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = !this.timer1.Enabled;
        }

        bool IsHot;
        private void UserControl3_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.Yellow;

            IsHot = true;
        }

        private void UserControl3_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = SystemColors.ButtonFace;

            IsHot = false;
        }
    }
}
