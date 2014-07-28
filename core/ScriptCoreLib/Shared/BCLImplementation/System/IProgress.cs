using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/iprogress.cs

    //[Script(Implements = typeof(global::System.IProgress))]
    [Script(ImplementsViaAssemblyQualifiedName = "System.IProgress`1")]
    internal interface __IProgress<in T>
    {
        // tested by
        // X:\jsc.svn\examples\javascript\forms\AsyncWithProgress\AsyncWithProgress\ApplicationControl.cs


        void Report(T value);
    }
}
