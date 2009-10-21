using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleChat.Library;

namespace SimpleChat.Commands
{
	public class CommandBase
	{
		public SynchronizedActionQueue PrimaryThreadQueue;

		public void Invoke()
		{
			this.PrimaryThreadQueue.Enqueue(
				delegate
				{
					RaiseDisplay();
				}
			);
		}

		public virtual void RaiseDisplay()
		{

		}
	}
}
