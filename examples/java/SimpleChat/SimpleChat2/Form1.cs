using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
			var ContinueRegistrationYetFail = false;



			//AppendTextLine("Registering " + textBox3.Text + " as " + );


			// start web server
			var vsr = new VirtualServerRack
			{
				Ports = PortTextbox.Text.ToInt32Array(),


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


							// if we do we should let the poor sap know about it!

							if (findname.name == this.NicknameTextbox.Text)
							{
								this.AppendTextLine("Thats my name! I claimed it first!");

								SendCommandSendName(findname);
							}
							else
							{
								var IsUnknown = true;

								foreach (var k in CurrentConfiguration)
								{
									if (k.Name == findname.name)
									{
										IsUnknown = false;
										if (k.Target == findname.myip)
										{
											this.AppendTextLine("Records confirm " + findname.name);
										}
										else
										{
											this.AppendTextLine("We seem to know about " + findname.name);
										}
									}
								}

								if (IsUnknown)
								{
									this.AppendTextLine("Maybe others know about " + findname.name);

									SetRequester(findname);
									// ask others!
								}
							}
						};

					sendname.BeforeInvoke =
						delegate
						{

							if (this.NicknameTextbox.Text == sendname.name && this.RegisteringTimer.Enabled)
							{
								ContinueRegistrationYetFail = true;

								AppendTextLine("Name already taken!");

								return;
							}

							UpdateConfiguration(sendname);
						};

					#region asknames
					// http://localhost:6666/chat/asknames
					asknames.BeforeInvoke =
						delegate
						{
							s.WriteWebContent(
								GetCurrentConfigurationString(), "text/plain"
							);
						};
					#endregion


					#region sendmessage
					// http://localhost:6666/chat/sendmessage?message=hi
					sendmessage.BeforeInvoke =
						delegate
						{
							Say(sendmessage);


							s.WriteWebContent("Thank you!");
						};
					#endregion

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

			// we have alloted ourselves some time
			// we should now go ahead and see if anyone has this name

			foreach (var k in CurrentConfiguration)
			{
				SendCommandFindName(k, NicknameTextbox.Text);
			}

			RegisteringTimer.Tick +=
				delegate
				{
					if (!ContinueRegistration)
						return;

					ContinueRegistration = false;

					if (ContinueRegistrationYetFail)
					{
						AppendTextLine("Go ahead and try another name and port :)");
						RegistrationFail();
						return;
					}

					AppendTextLine("Your name has been registered!");
					RegistrationReady();

					foreach (var k in CurrentConfiguration)
					{
						SendCommandSendName(k, NicknameTextbox.Text);
					}
				};
		}

		public void SetRequester(findname findname)
		{
			RelayRequester = findname;
		}

		public void Relay(findname findname)
		{
			// what about ttl?

			foreach (var k in this.CurrentConfiguration)
			{
				this.outgoingMessages1.SendCommand(k.Target,
					findname
				);
			}
		}

		public void UpdateConfiguration(sendname sendname)
		{
			var CurrentConfigurationSnapshot = CurrentConfiguration;
			var CurrentConfigurationAdd = true;

			foreach (var k in CurrentConfigurationSnapshot)
			{

				// for some reason we do not want to talk to ourself
				if (k.Name != this.NicknameTextbox.Text)
					if (k.Name == sendname.name)
					{
						k.Target = sendname.ip;
						CurrentConfigurationAdd = false;
					}
			}

			if (CurrentConfigurationAdd)
			{
				// for some reason we do not want to talk to ourself
				if (sendname.name != this.NicknameTextbox.Text)
					CurrentConfigurationSnapshot = CurrentConfigurationSnapshot.Concat(
						new MyData
						{
							Name = sendname.name,
							Target = sendname.ip
						}
					);

			}

			this.mySync1.Queue.Enqueue(
				delegate
				{
					AppendTextLine("Updating configuration for " + sendname.name);

					CurrentConfiguration = CurrentConfigurationSnapshot;
				}
			);
		}

		public void SendCommandSendName(findname findname)
		{
			var ip_host = findname.myip.GetLocalAddressByConnecting();

			if (ip_host == "")
			{
				this.AppendTextLine(findname.myip + " seems to be offline!");
			}

			var ip = ip_host + ":" + PortTextbox.Text.ToInt32Array()[0];

			// hey, thats me!
			this.outgoingMessages1.SendCommand(
				findname.myip,
				new sendname
				{
					ip = ip,
					name = findname.name,
				}
			);
		}


		public void SendCommandSendName(MyData t, string name)
		{
			Action Try =
				delegate
				{
					AppendTextLine("sending name " + t.Target);

					var ip_host = t.Target.GetLocalAddressByConnecting();

					if (ip_host == "")
					{
						this.AppendTextLine(t.Target + " seems to be offline!");
						return;
					}

					var ip = ip_host + ":" + PortTextbox.Text.ToInt32Array()[0];

					this.outgoingMessages1.SendCommand(t.Target,
						new sendname
						{
							ip = ip,
							name = name,
						}
					);
				};

			Try.TryInvokeInBackground();
		}

		private void SendCommandFindName(MyData t, string name)
		{
			Action Try =
				delegate
				{
					AppendTextLine("asking from " + t.Target);

					var ip_host = t.Target.GetLocalAddressByConnecting();

					if (ip_host == "")
					{
						this.AppendTextLine(t.Target + " seems to be offline!");
						return;
					}

					var ip = ip_host + ":" + PortTextbox.Text.ToInt32Array()[0];

					this.outgoingMessages1.SendCommand(t.Target,
						new findname
						{
							myip = ip,
							name = name,
						}
					);
				};

			Try.TryInvokeInBackground();
		}

		public MyData[] CurrentConfiguration
		{
			get
			{
				return MyData.Parse(this.textBox2.Text);
			}
			set
			{
				this.textBox2.Text = MyData.ToString(value);
			}
		}

		public string GetCurrentConfigurationString()
		{
			return MyData.ToString(
				MyData.Parse(this.textBox2.Text)
			);
		}

		public void RegistrationReady()
		{
			RegisteringTimer.Stop();
			label6.Hide();
			textBox5.Enabled = true;
			textBox6.Enabled = true;
			button3.Enabled = true;

			var discoveryService1 = this.discoveryService1;
			var notificationTimer1 = this.notificationTimer1;

			discoveryService1.Timer.Start();
			notificationTimer1.TimerEnabled = true;
			notificationTimer1.Text = "Ready!";

			Action StopOnce =
				delegate
				{
					discoveryService1.Timer.Stop();
					notificationTimer1.TimerEnabled = false;
				};

			this.Stop += StopOnce.AsOnce();
		}

		public void RegistrationFail()
		{
			RegisteringTimer.Stop();
			label6.Hide();

			if (Stop != null)
				Stop();

			EnableConfiguration();
		}

		public void Say(sendmessage sendmessage)
		{
			AppendTextLine(
				 sendmessage.myname + ": " + sendmessage.message
			 );


		}

		public void DisableConfiguration()
		{
			PortTextbox.Enabled = false;
			textBox2.Enabled = false;
			NicknameTextbox.Enabled = false;

			button1.Enabled = false;
			button2.Enabled = true;

			button5.Enabled = false;
			button6.Enabled = false;
			button7.Enabled = false;
			button8.Enabled = false;

		}

		private void EnableConfiguration()
		{
			PortTextbox.Enabled = true;
			textBox2.Enabled = true;
			NicknameTextbox.Enabled = true;

			button1.Enabled = true;
			button2.Enabled = false;

			button5.Enabled = true;
			button6.Enabled = true;
			button7.Enabled = true;
			button8.Enabled = true;
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

			var ip_host = t.Target.GetLocalAddressByConnecting();

			if (ip_host == "")
			{
				this.AppendTextLine(t.Target + " seems to be offline!");
				return;
			}

			var ip = ip_host + ":" + PortTextbox.Text.ToInt32Array()[0];


			this.outgoingMessages1.SendCommand(
				t.Target,
				new sendmessage
				{
					// we should show our primary name
					// first name is the primary for now
					myname = this.NicknameTextbox.Text,
					// how do we know what IP are we on?
					myip = ip,
					// the message is clear to us atleast
					message = this.textBox6.Text,
				}
			);

			Say(new sendmessage
			{
				// we should show our primary name
				// first name is the primary for now
				myname = this.NicknameTextbox.Text,
				// how do we know what IP are we on?
				myip = ip,
				// the message is clear to us atleast
				message = this.textBox6.Text + " to " + t.Target + " from " + ip,
			});

			textBox6.Clear();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			new Form1().Show();
		}

		private void button5_Click(object sender, EventArgs e)
		{
			PortTextbox.Text = "7001";
			NicknameTextbox.Text = "Smith";
			CurrentConfiguration = new[] {
				new MyData
				{
					Name = "Kenny", Target = "127.0.0.1:7002"
				}
			};
		}

		private void button6_Click(object sender, EventArgs e)
		{
			PortTextbox.Text = "7002";
			NicknameTextbox.Text = "Kenny";
			CurrentConfiguration = new[] {
				new MyData
				{
					Name = "Smith", Target = "127.0.0.1:7001"
				}
			};
		}

		private void discoveryService1_TimeToCheckForUnknownRecipients(DiscoveryService.DiscoveryArguments e)
		{
			e.Name = textBox5.Text;
			e.Configuration = this.CurrentConfiguration;
		}

		private void discoveryService1_MoreInformation(DiscoveryService.DiscoveryArguments e)
		{
			var c = this.CurrentConfiguration;
			foreach (var k in e.Configuration)
			{
				// for some reason we do not want to talk to ourself
				if (k.Name != this.NicknameTextbox.Text)
					c = c.ConcatDistinct(k);
			}
			this.CurrentConfiguration = c;
		}

		private void discoveryService1_Searching()
		{
			this.notificationTimer1.Text = "Searching...";
		}

		private void discoveryService1_ReadyToSearchAgain()
		{
			this.notificationTimer1.Text = "Ready to search again...";
		}

		private void button7_Click(object sender, EventArgs e)
		{
			PortTextbox.Text = "7011";
			NicknameTextbox.Text = "Smith2";
			CurrentConfiguration = new[] {
				new MyData
				{
					Name = "Kenny", Target = "127.0.0.1:7002"
				}
			};
		}

		private void button8_Click(object sender, EventArgs e)
		{
			PortTextbox.Text = "7012";
			NicknameTextbox.Text = "Kenny2";
			CurrentConfiguration = new[] {
				new MyData
				{
					Name = "Smith", Target = "127.0.0.1:7001"
				}
			};
		}

		findname RelayRequester;

		private void RelayTimer_Tick(object sender, EventArgs e)
		{
			var r = RelayRequester;
			if (r == null)
				return;
			RelayRequester = null;

			notificationTimer1.Text = "Who knows about " + r.name;
			Relay(r);
		}

	}
}
