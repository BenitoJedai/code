using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AsyncResearch;

namespace ClientSideForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var a = new ApplicationWebService();

            a.WebMethod2(button1.Text, x => button1.Text = x);
        }

        int c = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            c++;
            this.button1.ForeColor = (c % 2) == 0 ? Color.Red : SystemColors.WindowText;
            this.button2.ForeColor = (c % 2) != 0 ? Color.Red : SystemColors.WindowText;
        }
    }
}
