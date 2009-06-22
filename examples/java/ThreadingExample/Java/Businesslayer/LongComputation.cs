using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibJava.Threading;
using System.Threading;

namespace ThreadingExample.Java.Businesslayer
{
	[Script]
	public class LongComputation
	{
		[Script]
		public class Implementation : ThreadedAction
		{
			public long Value;

			
			public override void Invoke()
			{
				while (true)
				{
					Value++;

					// we should be running on our own thread
					// which enables us to loop forever and sleep when tired :)
					Thread.Sleep(100);
				}
			}
		}

		public readonly Implementation Current = new Implementation();
		public ThreadedActionInvoker CurrentThreadedAction;

		public void Start()
		{
			if (CurrentThreadedAction != null)
				Stop();

			CurrentThreadedAction = 0.AtDelay(Current);
			
		}

		public void Stop()
		{
			if (CurrentThreadedAction == null)
				return;

			// time to abort the managed thread
			CurrentThreadedAction.Thread.Abort();
			CurrentThreadedAction = null;
		}
	}
}
