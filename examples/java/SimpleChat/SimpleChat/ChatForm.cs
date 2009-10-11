using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimpleChat
{
	public partial class ChatForm : Form
	{
		public ChatForm()
		{
			InitializeComponent();
		}

		private void label2_Click(object sender, EventArgs e)
		{

		}


		int Counter;
		private void timer1_Tick(object sender, EventArgs e)
		{
			Counter++;


			this.localInformation1.WriteTo(
				(_sender, _e) =>
				{
					var a = (MyContext.LocalInformation.WriteToArguments)_e;

					if ((Counter % a.Count) == a.Position)
						this.label6.Text = a.Name + ": " + a.Value;
				}
			);
		}
	}
}
