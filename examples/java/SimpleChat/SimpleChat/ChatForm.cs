using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.Reflection.Options;
using SimpleChat.Commands;
using SimpleChat.Commands.chat;
using SimpleChat.Library;

namespace SimpleChat
{
	public partial class ChatForm : Form
	{
		public ChatForm()
		{
			InitializeComponent();

			this.History.AppendLine("Your messages will be show here...");
			this.UpdateText();
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

		private void button2_Click(object sender, EventArgs e)
		{
			// http://knowgramming.com/nicknames_pet_names_and_metaphors.htm
			{
				var PopularNickname = this.localPopular1.Text.RandomLine();

				//this.textBox2.Text = PopularNickname + ":80; ";
				this.textBox2.Text = PopularNickname;
			}

			{
				var PopularNickname = this.localPopular1.Text.RandomLine();

				//this.textBox2.Text = PopularNickname + ":80; ";
				this.textBox3.Text = PopularNickname + "@localhost:6666";
			}
		}



		//readonly WebServerCollection WebServerCollection1 = new WebServerCollection();

		string textBox2_Cache = "";
		int textBox2_Counter;

		MessageEndpoint[] CurrentLocals = new MessageEndpoint[0];

		private void timer2_Tick(object sender, EventArgs e)
		{
			PrimaryThreadQueue.Invoke();

			if (textBox2_Cache == this.textBox2.Text)
			{
				textBox2_Counter++;
			}
			else
			{
				textBox2_Cache = this.textBox2.Text;
				textBox2_Counter = 0;
				return;
			}

			if (textBox2_Counter < 5)
				return;

			CurrentLocals = this.textBox2.Text.ToMessageFromArray();

			webServerComponent1.Configuration = CurrentLocals.ToWebServers();

			// lets enable outgoing messages only if we have chosen a name

			this.textBox4.Enabled = this.webServerComponent1.Configuration.Length > 0;
			this.button1.Enabled = this.webServerComponent1.Configuration.Length > 0;

		}

		private void webServerComponent1_Start(WebServerProvider e)
		{
			Console.WriteLine("Start: " + e.Port);
		}

		private void webServerComponent1_Shutdown(WebServerProvider e)
		{
			Console.WriteLine("Shutdown: " + e.Port);
		}

		private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.timer1.Enabled = false;
			this.timer2.Enabled = false;
			this.webServerComponent1.Configuration = new WebServer[0];
		}

		public readonly SynchronizedActionQueue PrimaryThreadQueue = new SynchronizedActionQueue();

		private void webServerComponent1_IncomingData(WebServerProvider sender, IncomingDataArguments a)
		{
			// we are probably on a wrong thread here

			// let the server know real fast what our log looks like
			// so thet he could send it to the client
			//this.History.AppendLine(a.PathAndQuery);
			//Console.WriteLine(a.PathAndQuery);

			a.PathAndQuery = a.PathAndQuery.Chop(outgoingMessages1.PathPrefix);

			// we need to caputre this
			var CurrentLocals = this.CurrentLocals;

			a.GetArguments().AsParametersTo(
				new asknames
				{
					BeforeInvoke =
						e =>
						{
							a.ContentType = "text/plain";



							a.Content = CurrentLocals.ToJSON();
						}
				}.Invoke,

				new sendmessage
				{
					PrimaryThreadQueue = PrimaryThreadQueue,

					Display =
						e =>
						{
							// we should already be in the correct thread

							AddTextMessageAndUpdate(e);
						}
				}.Invoke
			);

			a.SetLogText(History.ToString());

			//PrimaryThreadQueue.Enqueue(UpdateText);
		}

		private void AddTextMessageAndUpdate(sendmessage e)
		{
			this.History.AppendLine(e.myname + " said: " + e.message);
			this.UpdateText();
		}

		public readonly StringBuilder History = new StringBuilder();

		public void UpdateText()
		{
			this.textBox1.Text = History.ToString();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var o = new sendmessage
			{
				// we should show our primary name
				// first name is the primary for now
				myname = this.webServerComponent1.Configuration[0].Locals[0].Name,
				// how do we know what IP are we on?
				ip = "0.0.0.0",
				// the message is clear to us atleast
				message = this.textBox4.Text,
			};

			this.textBox4.Text = "";

			AddTextMessageAndUpdate(o);

			this.outgoingMessages1.SendCommand(
				this.textBox3.Text.ToMessageFromArray(),
				o
			);
		}

		private void outgoingMessages1_NotFound(MessageEndpoint e)
		{
			this.PrimaryThreadQueue.Enqueue(
				delegate
				{
					this.History.AppendLine("not found: " + e.ToString());
					this.UpdateText();
				}
			);
		}

		private void button3_Click(object sender, EventArgs e)
		{
			// http://knowgramming.com/nicknames_pet_names_and_metaphors.htm
			{
				var PopularNickname = this.localPopular1.Text.RandomLine();

				//this.textBox2.Text = PopularNickname + ":80; ";
				this.textBox2.Text = PopularNickname + ":6666";
			}

			{
				var PopularNickname = this.localPopular1.Text.RandomLine();

				//this.textBox2.Text = PopularNickname + ":80; ";
				this.textBox3.Text = PopularNickname + "@localhost:80";
			}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			new ChatForm().Show();
		}

		TweenLibrary tween1 = new TweenLibrary();
		Random R = new Random();

		public int randint()
		{

			return Convert.ToInt32(10 + (this.R.NextDouble() * 290));

			//return this.R.Next(10, 300);
		}

 

 


		private void button5_Click(object sender, EventArgs e)
		{
			this.tween1.startTweenEvent(sender, this.randint(), this.randint(), "easeinquad", 40);

		}

		private void button6_Click(object sender, EventArgs e)
		{
			TargetMessageHeight = 32;
			MessageHider.Start();
		}

		public int TargetMessageHeight = 0;

		private void MessageEffectTimer_Tick(object sender, EventArgs e)
		{
			this.remoteUsers1.MessageHeight += 2*Math.Sign(TargetMessageHeight - this.remoteUsers1.MessageHeight);
 
		}

		private void MessageHider_Tick(object sender, EventArgs e)
		{
			TargetMessageHeight = 0;
			MessageHider.Stop();
		}

	}
}
