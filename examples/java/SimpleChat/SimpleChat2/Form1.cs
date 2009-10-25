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
using ScriptCoreLib.JSON;
using System.IO;
using System.Collections;
using System.Net;
using System.Net.NetworkInformation;

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

			var ContinueRegistration = true;



			//AppendTextLine("Registering " + textBox3.Text + " as " + );


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

					findname.BeforeInvoke =
						delegate
						{
							// let the discovery service know
							// that somebody wants that name

						};

					
					sendname.BeforeInvoke =
						delegate
						{
							// we are being told a name was found
						};

					// http://localhost:6666/chat/asknames
					asknames.BeforeInvoke =
						delegate
						{
							s.WriteWebContent(
								MyData.ToString(
									MyData.Parse(this.textBox2.Text)
								), "text/plain"
							);
						};

					// http://localhost:6666/chat/sendmessage?message=hi
					sendmessage.BeforeInvoke =
						delegate
						{
							Say(sendmessage);


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

			label6.Show();
			RegisteringTimer.Start();

			
			RegisteringTimer.Tick +=
				delegate
				{
					if (!ContinueRegistration)
						return;

					ContinueRegistration = false;

					label6.Hide();

					AppendTextLine("Your name has been registered!");

					RegisteringTimer.Stop();

					textBox5.Enabled = true;
					textBox6.Enabled = true;
					button3.Enabled = true;
				};
		}

		private void Say(sendmessage sendmessage)
		{
			AppendTextLine(
				 sendmessage.myname + ": " + sendmessage.message
			 );
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
			var s = MyData.Parse(textBox2.Text);
			var t = default(MyData);

			foreach (var k in s)
			{
				if (k.Name == textBox5.Text.Trim())
				{

					t = k;
				}
			}

			if (t == null)
			{
				this.AppendTextLine("We haven't found such user yet");
				return;
			}
			
			

			this.outgoingMessages1.SendCommand(
				t.Target,
				new sendmessage
				{
					// we should show our primary name
					// first name is the primary for now
					myname = this.textBox3.Text,
					// how do we know what IP are we on?
					ip = "0.0.0.0",
					// the message is clear to us atleast
					message = this.textBox6.Text,
				}
			);

			Say(new sendmessage
			{
				// we should show our primary name
				// first name is the primary for now
				myname = this.textBox3.Text,
				// how do we know what IP are we on?
				ip = "0.0.0.0",
				// the message is clear to us atleast
				message =  this.textBox6.Text + " to " + t.Target, 
			});

			textBox6.Clear();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			new Form1().Show();
		}

	}
}
