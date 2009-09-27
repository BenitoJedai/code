using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SwingForms
{
	public partial class MyMessageBox : Form
	{
		public MyMessageBox()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.Text = "message 1";
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.Text = "message 2";
		}

		private void button3_Click(object sender, EventArgs e)
		{
			this.Text = "message 3";
		}
	}
}
