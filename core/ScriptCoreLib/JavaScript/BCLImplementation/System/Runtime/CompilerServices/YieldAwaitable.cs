using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices
{
    // http://referencesource.microsoft.com/#mscorlib/system/runtime/compilerservices/YieldAwaitable.cs


#if NET45
    [Script(Implements = typeof(global::System.Runtime.CompilerServices.YieldAwaitable))]
#else
    [Script(ImplementsViaAssemblyQualifiedName = "System.Runtime.CompilerServices.YieldAwaitable")]
#endif
    internal class __YieldAwaitable
    {
        // X:\jsc.svn\examples\javascript\Test\TestAsyncAssignArrayToEnumerable\TestAsyncAssignArrayToEnumerable\Application.cs

        public __YieldAwaiter GetAwaiter()
        {
            return new __YieldAwaiter { };
        }
    }

    [Script(ImplementsViaAssemblyQualifiedName = "System.Runtime.CompilerServices.YieldAwaitable+YieldAwaiter")]
    internal class __YieldAwaiter
    {
        // CLR seems to do the oppisite for now..
        // later this might be the place to synchronize context data between worker threads..
        public bool IsCompleted { get { return true; } }

        public void OnCompleted(Action continuation)
        {
            continuation();
        }

        public void GetResult() { } // Nop. It exists purely because the compiler pattern demands it.
    }
}
