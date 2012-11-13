using System;
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
            var f = new Form1();

            f.Show();

            f.FormClosing +=
                (ss, ee) =>
                {
                    if (ee.CloseReason == CloseReason.UserClosing)
                    {
                        ee.Cancel = true;

                        f.WindowState = FormWindowState.Minimized;
                    }
                };
        }
    }
}
