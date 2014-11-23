using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Threading
{
    // http://referencesource.microsoft.com/#mscorlib/system/threading/AutoResetEvent.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Threading/AutoResetEvent.cs

    [Script(Implements = typeof(global::System.Threading.AutoResetEvent))]
    internal class __AutoResetEvent : __EventWaitHandle
    {
        public __AutoResetEvent(bool initialState)
            : base(initialState, global::System.Threading.EventResetMode.AutoReset)
        {
            // tested by
            // X:\jsc.svn\examples\java\Test\JVMCLRTaskStartNew\JVMCLRTaskStartNew\Program.cs
        }
    }
}
