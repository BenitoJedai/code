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
using System.Collections;

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

			// we should check on our friends
			var w = new StringBuilder();

			foreach (FriendStatus k in this.FriendStatusList)
			{
				k.PollerCounter++;

				if ((k.PollerCounter % 6) == 0)
				{
					var x = new EncodedMessage { Message = Message_Ping };

					ahmanize(
						new ChatRequest.Requests.sendmessage(
							k.Name,
							Nickname,
							"0",
							x.ToString(),
							"100"
						)
					);
				}

				w.AppendLine(k.ToString());
			}

			this.textBox6.Text = w.ToString();
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

					var r = default(FriendStatus);
					foreach (FriendStatus item in this.FriendStatusList)
					{
						if (item.Name == sendmessage.myname)
							r = item;
					}
					if (r != null)
					{
						if (r.IsOnline)
						{
							this.AppendTextLine(findname.name + " is registered to a friend!");

							ahmanize(
									new ChatRequest.Requests.sendname(
								// to whom?
										findname.myname,
								// from whom?
										r.Name,
								// name we know about
										r.Name,
										"0"
									)
								);
						}
					}
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
					EncodedMessage m = sendmessage.message;

					m.Sender = sendmessage.myname;

					if (m.Message == Message_SeeYouLater)
					{
						this.AppendTextLine("*** " + m.Sender + " has left the chat!");

						var r = default(FriendStatus);
						foreach (FriendStatus item in this.FriendStatusList)
						{
							if (item.Name == sendmessage.myname)
								r = item;
						}
						if (r != null)
							this.FriendStatusList.Remove(r);

						return;
					}

					if (m.Message == Message_Ping)
					{
						var x = new EncodedMessage { Message = Message_Pong };

						ahmanize(
							new ChatRequest.Requests.sendmessage(
								sendmessage.myname,
								Nickname,
								"0",
								x.ToString(),
								"100"
							)
						);

						var r = default(FriendStatus);
						foreach (FriendStatus item in this.FriendStatusList)
						{
							if (item.Name == sendmessage.myname)
								r = item;
						}
						if (r == null)
							this.FriendStatusList.Add(new FriendStatus { Name = sendmessage.myname });

						return;
					}

					if (m.Message == Message_Pong)
					{
						foreach (FriendStatus item in this.FriendStatusList)
						{
							if (item.Name == sendmessage.myname)
								item.LastSeen = DateTime.Now.Ticks;
						}
						return;
					}

					if (m.Message == Message_Catchup)
					{
						this.AppendTextLine("*** " + m.Sender + " wants to catch up...");

						foreach (EncodedMessage mm in this.Messages)
						{
							var xx = new EncodedMessage { Time = mm.Time, Sender = Nickname, Message = mm.Sender + " said " + mm.Message };

							BroadcastMessage(xx, new[] { m.Sender });
						}

						return;
					}

					AppendMessage(m);
				};

			path.Chop("/chat").GetArguments().AsParametersTo(
				sendname.Invoke,
				findname.Invoke,
				asknames.Invoke,
				sendmessage.Invoke
			);
		}

		public bool ResumePollerDisabled;
		public void ResumePoller()
		{
			if (ResumePollerDisabled)
				return;

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

			button8.Enabled = true;


			this.textBox5.Enabled = true;
			this.textBox7.Enabled = true;
			this.button3.Enabled = true;
			this.button2.Enabled = true;

			foreach (var item in textBox4.Lines)
			{
				if (!string.IsNullOrEmpty(item))
				{
					if (item != Nickname)
						this.FriendStatusList.Add(new FriendStatus { Name = item });
				}
			}

			AppendTextLine("Will try to catch up missed messages in 5 sec...");
			Catchup.Enabled = true;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			new Form1
			{
				Nickname = "ken"
			}.Show();
		}

		public class EncodedMessage
		{
			public DateTime Time = DateTime.Now;

			public string Sender;

			public string Message;

			public string Language = "en";

			public override string ToString()
			{
				var w = new StringBuilder();

				w.Append(Time.Ticks);
				w.Append(Separator);
				w.Append(Language);
				w.Append(Separator);
				w.Append(Message);

				return w.ToString();
			}

			const string Separator = "__";

			public static implicit operator EncodedMessage(string e)
			{
				var a = e.Split(new[] { Separator }, StringSplitOptions.None);

				return new EncodedMessage
				{
					Time = new DateTime(long.Parse(a[0])),
					Language = a[1],
					Message = a[2]
				};
			}

			public string ToDisplayString()
			{
				return this.Time.ToString() + " " + this.Sender + "[" + this.Language + "]: " + this.Message;
			}
		}

		public readonly ArrayList Messages = new ArrayList();

		public void AppendMessage(EncodedMessage m)
		{
			if (Messages.Count > 10)
				Messages.RemoveAt(0);

			Messages.Add(m);

			AppendTextLine(m.ToDisplayString());
		}

		private void button5_Click(object sender, EventArgs e)
		{
			var x = new EncodedMessage { Message = textBox2.Text, Sender = Nickname, Language = textBox5.Text };

			textBox2.Text = "";

			// which of our friends is online and chatting?

			AppendMessage(x);


			BroadcastMessage(x);
		}

		public void BroadcastMessage(EncodedMessage x)
		{
			var a = new ArrayList();

			foreach (FriendStatus item in this.FriendStatusList)
			{
				if (item.IsOnline)
					a.Add(item.Name);
			}

			BroadcastMessage(x, (string[])a.ToArray(typeof(string)));
		}

		public void BroadcastMessage(EncodedMessage x, string[] Friends)
		{
			// yay, lets try to see if any of our friends is online
			foreach (var Friend in Friends)
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
							x.ToString(),
							"100"
						)
					);
				}
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{

			ResumePollerDisabled = true;

			BroadcastMessage(
				new EncodedMessage
				{
					Sender = Nickname,
					Message = Message_SeeYouLater
				},
				this.textBox4.Lines
			);

			textBox2.Enabled = false;
			textBox5.Enabled = false;
			button5.Enabled = false;
			button2.Enabled = false;

			// lets wait for a while for responses?

			Poller.Enabled = false;
			textBox7.Enabled = false;
			button3.Enabled = false;
		}

		public const string Message_SeeYouLater = "SeeYouLater";
		public const string Message_Ping = "ping";
		public const string Message_Pong = "pong";
		public const string Message_Catchup = "Catchup";

		public class FriendStatus
		{
			public string Name;

			public long LastSeen;

			public int PollerCounter;

			public bool IsOnline
			{
				get
				{
					var n = DateTime.Now - new DateTime(LastSeen);

					return n.TotalMilliseconds < 5000;
				}
			}

			public override string ToString()
			{
				if (LastSeen == 0)
					return Name + " (pending)";

				if (IsOnline)
					return Name + " (online)";

				return Name + " (offline)";
			}
		}

		public readonly ArrayList FriendStatusList = new ArrayList();

		private void button3_Click(object sender, EventArgs e)
		{
			var n = new FriendStatus { Name = textBox7.Text };

			textBox7.Clear();

			this.FriendStatusList.Add(n);
		}

		private void button4_Click(object sender, EventArgs e)
		{
			new Form1
			{
				Nickname = "Tom2"
			}.Show();
		}

		private void Catchup_Tick(object sender, EventArgs e)
		{
			Catchup.Enabled = false;

			var r = default(FriendStatus);
			foreach (FriendStatus item in this.FriendStatusList)
			{
				if (item.IsOnline)
				{
					r = item;
					break;
				}
			}

			if (r == null)
			{
				AppendTextLine("*** Starting fresh...");
			}
			else
			{
				AppendTextLine("*** Catching up with " + r.Name + "...");

				var x = new EncodedMessage { Message = Message_Catchup };


				ahmanize(
					new ChatRequest.Requests.sendmessage(
						r.Name,
						Nickname,
						"0",
						x.ToString(),
						"100"
					)
				);
			}
		}

	}
}
