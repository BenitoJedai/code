using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices
{
    // http://referencesource.microsoft.com/#mscorlib/system/runtime/compilerservices/YieldAwaitable.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Runtime/CompilerServices/YieldAwaitable.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Runtime.CompilerServices/YieldAwaitable.cs

    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Runtime\CompilerServices\YieldAwaitable.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Runtime\CompilerServices\YieldAwaitable.cs

#if NET45
    [Script(Implements = typeof(global::System.Runtime.CompilerServices.YieldAwaitable))]
#else
    [Script(ImplementsViaAssemblyQualifiedName = "System.Runtime.CompilerServices.YieldAwaitable")]
#endif
    public class __YieldAwaitable
    {
        // X:\jsc.svn\examples\javascript\Test\TestAsyncAssignArrayToEnumerable\TestAsyncAssignArrayToEnumerable\Application.cs

        public __YieldAwaiter GetAwaiter()
        {
            return new __YieldAwaiter { };
        }
    }

    [Script(ImplementsViaAssemblyQualifiedName = "System.Runtime.CompilerServices.YieldAwaitable+YieldAwaiter")]
    public class __YieldAwaiter
    {
        // x:\jsc.svn\examples\javascript\async\asyncworkersourcesha1\asyncworkersourcesha1\application.cs




        // CLR seems to do the oppisite for now..
        // later this might be the place to synchronize context data between worker threads..
        //public bool IsCompleted { get { return InternalDelay.IsCompleted; } }
        public bool IsCompleted { get { return true; } }

        [Obsolete("Task.Yield is not correctly working. workaround Task.Delay")]
        public void OnCompleted(Action continuation)
        {
            continuation();

            // do we have a test for Worker thread?

            //Native.setTimeout(continuation, 1);

            //ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks.__Task.Delay(1).ContinueWith(
            //    delegate
            //    {
            //        //IsCompleted = true;

            //        continuation();
            //    }
            //);
        }

        public void GetResult() { } // Nop. It exists purely because the compiler pattern demands it.
    }
}
