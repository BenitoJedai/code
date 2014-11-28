using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Threading
{
    // X:\jsc.svn\examples\java\hybrid\Test\JVMCLRThreadPool\JVMCLRThreadPool\Program.cs

    [Script(Implements = typeof(global::System.Threading.WaitOrTimerCallback))]
    internal delegate void __WaitOrTimerCallback(object state, bool timedOut);
}
