using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TwentyTenSimpleWindowsFormsApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            GoForward(text1: "You clicked '" + button2.Text + "'");
        }

        void GoForward(string text1, string text2 = "powered by jsc", string blog = "http://zproxy.wordpress.com")
        {
            var w = new StringBuilder();

            w.AppendLine(text1);
            w.AppendLine();
            w.AppendLine(text2);
            w.AppendLine(blog);

            MessageBox.Show(w.ToString());
        }
    }
}
