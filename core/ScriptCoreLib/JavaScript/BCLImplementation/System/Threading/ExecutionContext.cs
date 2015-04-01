using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Threading
{
	// http://msdn.microsoft.com/en-us/library/system.threading.ExecutionContext(v=vs.110).aspx
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Threading/ExecutionContext.cs

	[Script(Implements = typeof(global::System.Threading.ExecutionContext))]
	public class __ExecutionContext
	{
		// tested by?

		public static ExecutionContext Capture()
		{
			// we should have one test to ty to capture the stacktrace. where is it?
			return null;
		}
	}
}
