using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System
{
	// https://msdn.microsoft.com/en-us/library/dd990377%28v=vs.110%29.aspx
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/IObservable.cs

	[Script(Implements = typeof(global::System.IObservable<>))]
    internal interface __IObservable<out T>
	{
		IDisposable Subscribe(IObserver<T> observer);
    }
}
