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
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\Task\Task.Delay.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\Task\Task.Delay.cs

    internal partial class __Task
    {
        public static Task Delay(int millisecondsDelay)
        {
            // or to we have a scheduler we can talk to?

            var t = new TaskCompletionSource<object>();

            new Thread(
                delegate()
                {
                    // in java it we can keep our call refs.

                    Thread.Sleep(millisecondsDelay);

                    // signal ready?
                    t.SetResult(null);
                }
            ).Start();


            return t.Task;
        }
    }
}
