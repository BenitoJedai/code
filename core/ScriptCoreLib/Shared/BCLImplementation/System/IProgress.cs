using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/iprogress.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/IProgress.cs

    //[Script(Implements = typeof(global::System.IProgress))]
    [Script(ImplementsViaAssemblyQualifiedName = "System.IProgress`1")]
    internal interface __IProgress<in T>
    {
        // can we use this to respond early form server to
        // client via WebSocket or EventSource ?


        // tested by
        // X:\jsc.svn\examples\javascript\forms\AsyncWithProgress\AsyncWithProgress\ApplicationControl.cs


        void Report(T value);
    }
}
