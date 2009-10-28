using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SimpleChat.Commands.chat;
using System.Windows.Forms;

namespace SimpleChat2
{
	public partial class DiscoveryService : Component
	{
		public OutgoingMessages Outgoing { get; set; }
		public MySync Sync { get; set; }
		public Timer Timer { get { return this._DiscoveryTimer; } }

		public DiscoveryService()
		{
			InitializeComponent();
		}

		public DiscoveryService(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
		}

		public class DiscoveryArguments
		{
			public string Name;
			public MyData[] Configuration;
		}

		public delegate void DiscoveryDelegate(DiscoveryArguments e);

		public event DiscoveryDelegate TimeToCheckForUnknownRecipients;

		string PreviousName;

		private void DiscoveryTimer_Tick(object sender, EventArgs e)
		{

			var a = new DiscoveryArguments();

			if (TimeToCheckForUnknownRecipients != null)
				TimeToCheckForUnknownRecipients(a);

			if (string.IsNullOrEmpty(a.Name))
				return;

			if (PreviousName != null)
				if (PreviousName == a.Name)
					return;


			if (!ChangeDetector.Enabled)
			{
				PreviousName = a.Name;
				ChangeDetector.Start();
			}



			var NameFound = false;
			foreach (var k in a.Configuration)
			{
				if (k.Name == a.Name)
				{
					NameFound = true;
					break;
				}

			}
			if (NameFound)
				return;



			AskNameFromPeers(a);
		}

		private void AskNameFromPeers(DiscoveryArguments a)
		{
			if (Searching != null)
				Searching();


			foreach (var k in a.Configuration)
			{
				this.Outgoing.SendCommand(k.Target,
					new asknames
					{

					},
					GotMoreInformation
				);
			}
		}

		public event DiscoveryDelegate MoreInformation;

		public void RaiseMoreInformation(DiscoveryArguments e)
		{
			if (MoreInformation != null)
				MoreInformation(e);
		}

		public void GotMoreInformation(string Document)
		{
			var a = new DiscoveryArguments
			{
				Configuration = MyData.Parse(Document)
			};

			this.Sync.Queue.Enqueue(
				delegate
				{
					this.RaiseMoreInformation(a);
				}
			);


		}

		public event Action Searching;
		public event Action ReadyToSearchAgain;

		private void ChangeDetector_Tick(object sender, EventArgs e)
		{
			PreviousName = null;
			ChangeDetector.Stop();

			if (ReadyToSearchAgain != null)
				ReadyToSearchAgain();
		}
	}
}
