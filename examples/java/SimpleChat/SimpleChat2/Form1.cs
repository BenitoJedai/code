using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SimpleChat.Commands.chat;
using ScriptCoreLib.Reflection.Options;
using System.IO;

namespace SimpleChat2
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			DisableConfiguration();


			// start web server
			var vsr = new VirtualServerRack
			{
				Ports = textBox1.Text.ToInt32Array(),


			};

			vsr.CommandRequest +=
				(s, path) =>
				{
					var sendname = new sendname();
					var findname = new findname();
					var asknames = new asknames();
					var sendmessage = new sendmessage();



					// http://localhost:6666/chat/sendmessage?message=hi
					sendmessage.BeforeInvoke =
						delegate
						{
							AppendTextLine(
								sendmessage.myname + ": " + sendmessage.message
							);


							s.WriteWebContent("Thank you!");
						};

					path.Chop("/chat").GetArguments().AsParametersTo(
						sendname.Invoke,
						findname.Invoke,
						asknames.Invoke,
						sendmessage.Invoke
					);
				};

			vsr.Start();

			this.Stop +=
				delegate
				{
					if (vsr.Stop == null)
						return;

					vsr.Stop();
				};
			// populate network

			// claim the name

		}

		public void DisableConfiguration()
		{
			textBox1.Enabled = false;
			textBox2.Enabled = false;
			textBox3.Enabled = false;

			button1.Enabled = false;
			button2.Enabled = true;
		}

		private void EnableConfiguration()
		{
			textBox1.Enabled = true;
			textBox2.Enabled = true;
			textBox3.Enabled = true;

			button1.Enabled = true;
			button2.Enabled = false;
		}

		public void AppendTextLine(string e)
		{
			var textBox4 = this.textBox4;
			mySync1.Queue.Enqueue(
				delegate
				{
					textBox4.AppendTextLine(e);
				}
			);
		}

		public Action Stop;

		private void button2_Click(object sender, EventArgs e)
		{
			if (Stop != null)
				Stop();

			EnableConfiguration();
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (Stop != null)
				Stop();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			this.AppendTextLine("We haven't found such user yet");

			textBox6.Clear();
		}

	}
}
