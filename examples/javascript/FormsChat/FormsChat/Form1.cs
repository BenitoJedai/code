using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsChat
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();


			AddMessage("*** You have reached FormsChat service.",
				"- This is a simple windows form application.",
				"- The chat could be synchronized between multiple instances.",
				"- .net instance could spawn javascript versions."
			);

			this.comboBox1.Text = "Style: Windows";
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var Header = "You said:";
			var Message = textBox2.Text;

			AddMessage(Header, Message);

			textBox2.Text = "";
		}

		private void AddMessage(string Header, string Message)
		{
			AddMessage(Header, Message.Split('\n'));

		}

		private void AddMessage(string Header, params string[] Messages)
		{
			var w = new StringBuilder();

			w.AppendLine(Header);


			foreach (var k in Messages)
			{
				w.AppendLine("".PadLeft(4) + k.Trim());
			}


			textBox1.AppendText(w.ToString());
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBox1.Text == "Style: Windows")
			{
				textBox1.BackColor = SystemColors.Window;
				textBox1.ForeColor = SystemColors.WindowText;

				textBox2.BackColor = SystemColors.Window;
				textBox2.ForeColor = SystemColors.WindowText;
			}

			if (comboBox1.Text == "Style: Terminal")
			{
				textBox1.BackColor = Color.Black;
				textBox1.ForeColor = Color.Yellow;

				textBox2.BackColor = Color.Black;
				textBox2.ForeColor = Color.Yellow;
			}
		}

		Random r = new Random();

		private void timer2_Tick(object sender, EventArgs e)
		{
			AddMessage("User" + r.Next() + " said:", "This meassage came from the internetz - " + r.Next());

		}



	}
}
