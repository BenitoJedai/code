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
				try
				{
					while (true)
					{
						Value++;

						// we should be running on our own thread
						// which enables us to loop forever and sleep when tired :)
						Thread.Sleep(100);
					}
				}
				catch
				{
					// there is nothing we should do in case our operation is aborted
					// catching this exception in java keeps console clean 

					// also note for .net:
					// 1.
					// ThreadAbortException is a special exception that can be caught, but 
					// it will automatically be raised again at the end of the catch block
					// 2.
					// you cannot use ThreadAbortException to detect when background threads 
					// are being terminated by the CLR. 
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
