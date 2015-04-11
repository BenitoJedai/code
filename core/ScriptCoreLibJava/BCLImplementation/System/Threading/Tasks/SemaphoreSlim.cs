using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLibJava.BCLImplementation.System.Threading.Tasks
{
    // http://msdn.microsoft.com/en-us/library/system.threading.SemaphoreSlim(v=vs.110).aspx

    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Threading/SemaphoreSlim.cs

    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\SemaphoreSlim.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\SemaphoreSlim.cs

    [Script(Implements = typeof(global::System.Threading.SemaphoreSlim))]
    public class __SemaphoreSlim
    {
        // X:\jsc.svn\examples\java\async\test\JVMCLRTCPServerAsync\JVMCLRTCPServerAsync\Program.cs

        public int CurrentCount { get; set; }

        public __SemaphoreSlim(int c)
        {
            this.CurrentCount = c;
        }


        public int Release()
        {
            if (InternalWaitAsync != null)
                InternalWaitAsync.SetResult(null);
            else
            {
                // X:\jsc.svn\examples\java\async\Test\JVMCLRTCPServerAsync\JVMCLRTCPServerAsync\Program.cs

                InternalWaitAsync = new TaskCompletionSource<object>();
                InternalWaitAsync.SetResult(null);

            }

            return 0;
        }



        TaskCompletionSource<object> InternalWaitAsync;


        public Task WaitAsync()
        {
            if (InternalWaitAsync == null)
            {
                // are we the first one?
                InternalWaitAsync = new TaskCompletionSource<object>();
                return InternalWaitAsync.Task;
            }



            return InternalWaitAsync.Task;
        }

        public void Wait()
        {
            WaitAsync().Wait();
        }
    }
}
