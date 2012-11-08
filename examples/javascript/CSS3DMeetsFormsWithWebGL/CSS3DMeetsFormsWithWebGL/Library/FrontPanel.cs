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
    public partial class FrontPanel : UserControl
    {
        public FrontPanel()
        {
            InitializeComponent();
        }

        private void FrontPanel_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var f = new Form
            {

                Text = this.comboBox1.Text
            };
            var c = new WebBrowser();
            c.Dock = DockStyle.Fill;
            c.Size = new System.Drawing.Size(800, 600);
            f.Controls.Add(c);
            f.ClientSize = new System.Drawing.Size(800, 600);
            c.Navigate(this.comboBox1.Text);
            //c.Url = new Uri(this.comboBox1.Text);
            f.Show();
        }
    }
}
