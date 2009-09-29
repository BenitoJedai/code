using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimpleForms1
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			MessageBox.Show("message1");

		}

		private void button2_Click(object sender, EventArgs e)
		{
			MessageBox.Show("message2");

		}

		private void button3_Click(object sender, EventArgs e)
		{
			MessageBox.Show(this.component21.SomeText);

		}

		
	}
}
