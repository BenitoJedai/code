using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Threading
{
	// https://msdn.microsoft.com/en-us/library/system.threading.synchronizationcontext%28v=vs.110%29.aspx
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Threading/SynchronizationContext.cs

	[Script(Implements = typeof(global::System.Threading.SynchronizationContext))]
	public class __SynchronizationContext
	{
		// X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Progress.cs

		// tested by?

		public static void SetThreadStaticContext(SynchronizationContext syncContext)
		{
			// ?
		}


		//  We cannot call the WinRT APIs directly from mscorlib, because we don't have the fancy projections here.
	}
}
