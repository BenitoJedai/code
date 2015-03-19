using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System
{
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/IObservable.cs

	[Script(Implements = typeof(global::System.IObserver<>))]
	public interface IObserver<in T>
	{
		void OnNext(T value);
		void OnError(Exception error);
		void OnCompleted();
	}
}
