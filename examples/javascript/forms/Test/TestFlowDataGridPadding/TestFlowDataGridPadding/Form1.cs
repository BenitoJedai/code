using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestFlowDataGridPadding.Design;

namespace TestFlowDataGridPadding
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Book1.GetDataSet().Tables[0];

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.ClientSize.Width == 0)
            {
                Console.WriteLine("why we get 0?");
                return;
            }

            dataGridView1.Width = this.ClientSize.Width;
        }
    }
}
