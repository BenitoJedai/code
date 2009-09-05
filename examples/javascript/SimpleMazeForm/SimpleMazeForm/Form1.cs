using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimpleMazeForm
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var f = new Form2();

			f.CreateMaze(
				int.Parse(this.textBox1.Text),
				int.Parse(this.textBox2.Text)
				);

			f.Show();


		}

	
	
	}
}
