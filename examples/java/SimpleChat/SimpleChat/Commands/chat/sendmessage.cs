using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleChat.Library;

namespace SimpleChat.Commands.chat
{
	public class sendmessage : CommandBase
	{
		public string myname = "guest";
		public string ip = "0.0.0.0";
		public string message;
		public string ttl = "0";



		public override void RaiseDisplay()
		{
			if (this.Display != null)
				this.Display(this);
		}

		public delegate void SayDelegate(sendmessage e);

		public SayDelegate Display;
	}
}
