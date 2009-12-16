using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SimpleChat2.ClientProvider;
using SimpleChat2.Buffer;
using System.Threading;
using SimpleChat2.Network;
using SimpleChat2.ServerProvider.Library;
using System.IO;
using SimpleChat.Commands.chat;
using ScriptCoreLib.Reflection.Options;

namespace SimpleChat2
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}




		private void findname_handler1_Request_1(SimpleChat2.Buffer.Server.findname_handler.findname_response e)
		{
			e.Content = "hi!";
		}



		int Counter = 0;
		private void requestDispatcher1_Tick()
		{
			Counter++;
			this.Text = "" + Counter;
		}

		

		private void sendmessage_handler1_Request(SimpleChat2.Buffer.Server.sendmessage_handler.sendmessage_response e)
		{
			// this is how we get messages
			this.Delay(
				delegate
				{
					this.textBox1.AppendText(
						e.myname + " - " +
						DateTime.Now + " : " + e.message +
						Environment.NewLine
					);

					// update gui
				}
			);
		}



		public void Delay(Action e)
		{
			this.mySync1.Queue.Enqueue(e);
		}

		#region asknames
		private void button6_Click(object sender, EventArgs e)
		{
			button6.Enabled = false;
			Poller.Enabled = true;
			textBox3.Enabled = false;
			textBox4.Enabled = false;

			// remember the nickname for crossthread access
			_Nickname = Nickname;

			AppendTextLine("Registering your nickname... " + Nickname + " (" + Pseudoname + ")");


			// yay, lets try to see if any of our friends is online
			foreach (var Friend in this.textBox4.Lines)
			{
				if (string.IsNullOrEmpty(Friend))
				{
					// shooting blanks are we?
				}
				else
				{
					AppendTextLine("Asking " + Friend);

					ahmanize(
						new ChatRequest.Requests.findname(
							Friend,
							Pseudoname,
							Nickname,
							"0.0.0.0:0", "100"
						)
					);
				}
			}

			RegistrationTimeout.Enabled = true;
		}
		#endregion


		#region hail to ahman
		public readonly Uri ahman = new Uri("http://ahman.no-ip.info");

		public void ahmanize(IDefaultRequestPath e)
		{
			e.ThreadedSendTo(ahman);
		}
		#endregion


		int PollerCounter = 0;

		#region Poller_Tick
		private void Poller_Tick(object sender, EventArgs e)
		{
			this.PollerCounter++;
			this.Text = "" + PollerCounter;

			Poller.Enabled = false;

			if (NicknameRegistered)
				ChatCheck(Nickname);
			else
				ChatCheck(Pseudoname);
		}

		private void ChatCheck(string myname)
		{
			new ChatCheck
			{
				myname = myname
			}.ThreadedSendTo(ahman,
				 response =>
				 {
					 Delay(ResumePoller);

					 if (string.IsNullOrEmpty(response))
						 return;

					 PollerGotData(response, myname);
				 }
			 );
		}
		#endregion

		public void PollerGotData(string response, string myname)
		{
			var r = new StringReader(response);

			var x = r.ReadLine();

			while (x != null)
			{

				PollerGotDataLine(x, myname);

				x = r.ReadLine();
			}
		}

		public bool NicknameRegistered;

		public void PollerGotDataLine(string path, string myname)
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


					// if we do we should let the poor sap know about it!

					if (findname.name == this.Nickname)
					{
						if (findname.myname == this.Pseudoname)
						{
							// we swallow our own question!
						}
						else
						{
							this.AppendTextLine(findname.name + " is registered to me!");

							ahmanize(
								new ChatRequest.Requests.sendname(
								// to whom?
									findname.myname,
								// from whom?
									Nickname,
								// name we know about
									Nickname,
									"0"
								)
							);
						}

						return;
					}

					this.AppendTextLine("Do we know " + findname.name + "?");
				};

			sendname.BeforeInvoke =
				delegate
				{
					if (sendname.name == Nickname)
					{
						this.AppendTextLine("*** Nickname " + Nickname + " already taken! Better luck next time!");
						this.RegistrationTimeout.Enabled = false;
						this.Poller.Enabled = false; 
						return;
					}

				};

			sendmessage.BeforeInvoke =
				delegate
				{
					AppendTextLine(sendmessage.myname + ": " + sendmessage.message);
				};

			path.Chop("/chat").GetArguments().AsParametersTo(
				sendname.Invoke,
				findname.Invoke,
				asknames.Invoke,
				sendmessage.Invoke
			);
		}

		public void ResumePoller()
		{
			this.Poller.Enabled = true;
		}

		private void button7_Click(object sender, EventArgs e)
		{
			new Form1
			{
				Nickname = "Cartman"
			}.Show();

		}

		#region Pseudoname
		string _Pseudoname;
		Random _PseudonameRandom = new Random();
		public string Pseudoname
		{
			get
			{
				if (_Pseudoname == null)
				{
					var w = new StringBuilder();

					w.Append("_");

					for (int i = 0; i < 8; i++)
					{
						w.Append("" + _PseudonameRandom.Next(0, 10));
					}

					_Pseudoname = w.ToString();
				}

				return _Pseudoname;
			}
		}
		#endregion

		string _Nickname;
		public string Nickname
		{
			get
			{
				if (_Nickname == null)
					return this.textBox3.Text;

				return _Nickname;
			}
			set
			{
				this.textBox3.Text = value;
			}
		}

		private void button8_Click(object sender, EventArgs e)
		{
			new Form1
			{
				Nickname = "Tom"
			}.Show();
		}

		public void AppendTextLine(string e)
		{
			Delay(
				delegate
				{
					label3.Text = e;
					this.textBox1.AppendText(e + Environment.NewLine);
				}
			);
		}

		private void RegistrationTimeout_Tick(object sender, EventArgs e)
		{
			NicknameRegistered = true;
			AppendTextLine("Your nickname is now registered!");
			RegistrationTimeout.Enabled = false;

			textBox2.Enabled = true;
			button5.Enabled = true;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			new Form1
			{
				Nickname = "ken"
			}.Show();
		}


		private void button5_Click(object sender, EventArgs e)
		{
			var x = textBox2.Text;
			textBox2.Text = "";

			// which of our friends is online and chatting?

			AppendTextLine(Nickname + ": " + x);


			// yay, lets try to see if any of our friends is online
			foreach (var Friend in this.textBox4.Lines)
			{
				if (string.IsNullOrEmpty(Friend))
				{
					// shooting blanks are we?
				}
				else if (Friend == Nickname)
				{
				}
				else
				{

					ahmanize(
						new ChatRequest.Requests.sendmessage(
							Friend,
							Nickname,
							"0",
							x,
							"100"
						)
					);
				}
			}
		}
	}
}
