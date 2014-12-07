using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices;
using ScriptCoreLibJava.BCLImplementation.System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScriptCoreLibJava.BCLImplementation.System.Threading.Tasks
{
    internal partial class __Task
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\Task\Task.WhenAll.cs
        // X:\jsc.svn\examples\java\hybrid\test\JVMCLRWhenAll\JVMCLRWhenAll\Program.cs

        public static Task WhenAll(params Task[] tasks)
        {
            var x = new TaskCompletionSource<object>();

            var i = tasks.Length;
            foreach (var item in tasks)
            {
                item.ContinueWith(
                    task =>
                    {
                        i--;

                        if (i == 0)
                            x.SetResult(null);
                    }
                );
            }


            return x.Task;
        }
    }
}
