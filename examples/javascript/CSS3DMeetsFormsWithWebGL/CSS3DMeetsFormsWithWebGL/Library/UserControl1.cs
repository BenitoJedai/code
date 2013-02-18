using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CSS3DMeetsFormsWithWebGL.Library
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("hi");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Form1().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Form2().Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //var f = new Form();
            //var c = new FormsAvalonAnimation.AvalonWindowDrawerHost();
            //c.Dock = DockStyle.Fill;
            //c.Size = new System.Drawing.Size(284, 262);
            //f.Controls.Add(c);
            //f.ClientSize = new System.Drawing.Size(284, 262);
            //f.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var f = new Form();
            var c = new PlasmaFormsControl.ApplicationControl();
            c.Dock = DockStyle.Fill;
            c.Size = new System.Drawing.Size(284, 262);
            f.Controls.Add(c);
            f.ClientSize = new System.Drawing.Size(284, 262);
            f.Show();
        }

       
    }
}
