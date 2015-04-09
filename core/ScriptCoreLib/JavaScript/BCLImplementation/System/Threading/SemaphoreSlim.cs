using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Threading
{
	// http://msdn.microsoft.com/en-us/library/system.threading.SemaphoreSlim(v=vs.110).aspx

	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Threading/SemaphoreSlim.cs

	[Script(Implements = typeof(global::System.Threading.SemaphoreSlim))]
	public class __SemaphoreSlim
	{
		public int CurrentCount { get; set; }


		// tested by?
		// X:\jsc.svn\examples\javascript\async\AsyncHopToUIFromWorker\AsyncHopToUIFromWorker\Application.cs
		// would this type allow to jump back from another thread?

		// X:\jsc.svn\examples\javascript\async\Test\TestGetUserMedia\TestGetUserMedia\Application.cs

		public __SemaphoreSlim(int c)
		{
			this.CurrentCount = c;
		}

		// set by thread hopper, to indicate, this object is living 
		// in multiple workers, and is connected.
		// for network signals, webrtc/udp needs to be available
		public bool InternalIsEntangled;

		#region Release
		public Action InternalVirtualRelease;

		public int Release()
		{
			// are we entangled?
			// if so then we need to send a signal to us in that other thread?
			// what if we were entangled into multiple threads? would need to do round robin
			// for the versions that are awaiting?

			Console.WriteLine("SemaphoreSlim.Release " + new { InternalIsEntangled, Thread.CurrentThread.ManagedThreadId });

			//37418ms SemaphoreSlim.Release { { InternalIsEntangled = true, ManagedThreadId = 1 } }

			if (InternalIsEntangled)
			{
				// this semaphore was sent to a new worker.
				// now, we are about to signal that new thread.


				InternalVirtualRelease();
			}
			// otherwise, stash the release? 

			return 0;
		}

		public int Release(int releaseCount)
		{
			// X:\jsc.svn\examples\javascript\async\test\TestBytesFromSemaphore\TestBytesFromSemaphore\Application.cs

			return Release();
		}
		#endregion


		// X:\jsc.svn\examples\javascript\async\test\TestAsyncLocal\TestAsyncLocal\ApplicationControl.cs



		#region WaitAsync
		public Action<TaskCompletionSource<object>> InternalVirtualWaitAsync;

		public Task WaitAsync()
		{
			// X:\jsc.svn\examples\javascript\async\test\TestBytesFromSemaphore\TestBytesFromSemaphore\Application.cs
			// X:\jsc.svn\examples\javascript\async\test\TestSemaphoreSlim\TestSemaphoreSlim\ApplicationControl.cs

			var c = new TaskCompletionSource<object>();

			Console.WriteLine("SemaphoreSlim.WaitAsync " + new { InternalIsEntangled, Thread.CurrentThread.ManagedThreadId });

			// at this point, the worker thread may not yet have connected back, entangled
			if (InternalVirtualWaitAsync != null)
				InternalVirtualWaitAsync(c);

			return c.Task;
		}
		#endregion


		public Task<bool> WaitAsync(int millisecondsTimeout, CancellationToken cancellationToken)
		{

			return null;
		}


	}
}
