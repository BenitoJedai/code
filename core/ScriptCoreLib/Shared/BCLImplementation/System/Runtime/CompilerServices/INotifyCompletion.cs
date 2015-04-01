using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices
{
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Runtime/CompilerServices/INotifyCompletion.cs

	[Script(ImplementsViaAssemblyQualifiedName = "System.Runtime.CompilerServices.INotifyCompletion")]
    public interface __INotifyCompletion
    {
        // http://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.inotifycompletion.aspx
        void OnCompleted(Action continuation);
    }
}
