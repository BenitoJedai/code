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

		public SynchronizedActionQueue PrimaryThreadQueue;

		public void Invoke()
		{
			if (BeforeInvoke != null)
				BeforeInvoke(this);

			if (this.PrimaryThreadQueue != null)
				this.PrimaryThreadQueue.Enqueue(
					delegate
					{
						RaiseDisplay();
					}
				);
		}

		public CommandBaseAction BeforeInvoke;

		public virtual void RaiseDisplay()
		{

		}
	}
}
