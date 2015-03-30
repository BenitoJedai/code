using ScriptCoreLib.Shared.BCLImplementation.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/progress.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Progress.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/Progress.cs

    //[Script(Implements = typeof(global::System.Progress<>))]
    [Script(ImplementsViaAssemblyQualifiedName = "System.Progress`1")]
    internal class __Progress<T> : __IProgress<T>
    {
		// http://stackoverflow.com/questions/10750577/looking-for-net-4-5-progresst-source-code
		// X:\jsc.svn\examples\javascript\test\TestSwitchToIFrame\TestSwitchToIFrame\Application.cs

		// how would we implement it for java/android?

		// how would we implement it for server early yield/event stream/websocket?


		Action<T> handler;

        public __Progress(Action<T> handler)
        {
            if (handler == null)
                throw new InvalidOperationException("__Progress handler null");

            this.handler = handler;
        }

        protected virtual void OnReport(T value)
        {
            handler(value);
        }

        void __IProgress<T>.Report(T value)
        {
            OnReport(value);
        }
    }
}
