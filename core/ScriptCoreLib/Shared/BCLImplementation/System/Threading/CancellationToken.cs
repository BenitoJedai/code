using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Threading
{
	// http://referencesource.microsoft.com/#mscorlib/system/threading/CancellationToken.cs
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Threading/CancellationToken.cs

	[Script(Implements = typeof(global::System.Threading.CancellationToken))]
    internal class __CancellationToken
    {
        // jsc does not yet allow to cancel a task
		// if the task was a worker thread, would we need to call terminate?
    }
}
