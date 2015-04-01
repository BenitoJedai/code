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
		// tested by?
		// X:\jsc.svn\examples\javascript\async\AsyncHopToUIFromWorker\AsyncHopToUIFromWorker\Application.cs
		// would this type allow to jump back from another thread?

		public int Release()
		{
			return 0;
		}



		public Task WaitAsync()
		{

			return null;
		}

		public Task<bool> WaitAsync(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			return null;
		}


	}
}
