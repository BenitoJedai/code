using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SimpleChat.Library;
using System.Collections;

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

		private void button2_Click(object sender, EventArgs e)
		{
			// http://knowgramming.com/nicknames_pet_names_and_metaphors.htm

			var PopularNickname = this.localPopular1.Text.RandomLine();

			//this.textBox2.Text = PopularNickname + ":80; ";
			this.textBox2.Text = PopularNickname;
		}



		//readonly WebServerCollection WebServerCollection1 = new WebServerCollection();

		string textBox2_Cache = "";
		int textBox2_Counter;

		private void timer2_Tick(object sender, EventArgs e)
		{
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

			if (textBox2_Counter == 5)
				webServerComponent1.Configuration = this.textBox2.Text.ToMessageFromArray().ToWebServers();

			PrimaryThreadQueue.Invoke();
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

		private void webServerComponent1_IncomingData(WebServerProvider sender, WebServerProvider.IncomingDataArguments a)
		{
			// we are probably on a wrong thread here

			a.SetLogText(History.ToString());

			PrimaryThreadQueue.Enqueue(
				delegate
				{
					this.AppendTextLine(a.QueryAndPath);
				}
			);
		}

		public readonly StringBuilder History = new StringBuilder();

		public void AppendTextLine(string e)
		{
			this.History.AppendLine(e);
			this.textBox1.AppendTextLine(e);
		}

	}
}
