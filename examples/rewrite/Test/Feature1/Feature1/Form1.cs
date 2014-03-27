using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Feature2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // script: error JSC1000: No implementation found for this native method, please implement [static System.Console.Beep()]
            //Console.Beep();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("button 2");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("button 1");

        }
    }
}
