using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SimpleChat.Library
{
	public class SynchronizedActionQueue
	{
		readonly object SyncRoot = new object();
		readonly Queue BaseQueue = new Queue();

		public void Enqueue(Action e)
		{
			lock (SyncRoot)
			{
				BaseQueue.Enqueue(e);
			}
		}

		public void Invoke()
		{
			var k = DequeueOrDefault();

			while (k != null)
			{
				k();

				k = DequeueOrDefault();
			}
		}

		public Action DequeueOrDefault()
		{
			var r = default(Action);
			lock (SyncRoot)
			{
				if (BaseQueue.Count > 0)
					r = (Action)BaseQueue.Dequeue();
			}

			return r;
		}
	}
}
