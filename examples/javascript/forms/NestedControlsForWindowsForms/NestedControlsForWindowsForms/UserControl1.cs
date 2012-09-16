using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TwentyTenSimpleWindowsFormsApplication
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void UserControl1_MouseEnter(object sender, EventArgs e)
        {

            this.BackColor = Color.Yellow;
        }

        private void UserControl1_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = SystemColors.ButtonFace;

        }



    }
}
