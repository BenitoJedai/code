using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleChat.Library;

namespace SimpleChat.Commands
{
	public class CommandBase
	{
		public delegate void CommandBaseAction(CommandBase e);


		public void Invoke()
		{
			if (BeforeInvoke != null)
				BeforeInvoke(this);

	
		}

		public CommandBaseAction BeforeInvoke;

	
	}
}
