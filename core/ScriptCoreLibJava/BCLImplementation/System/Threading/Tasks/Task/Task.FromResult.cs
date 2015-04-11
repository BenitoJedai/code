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
        public static Task CompletedTask
        {
            get
            {
                // X:\jsc.svn\examples\javascript\async\Test\TestCompletedTask\TestCompletedTask\Application.cs

                var c = new TaskCompletionSource<object>();
                c.SetResult(null);

                return c.Task;
            }
        }

        public static Task<TResult> FromResult<TResult>(TResult result)
        {
            var x = new TaskCompletionSource<TResult>();
            x.SetResult(result);
            return x.Task;
        }
    }
}
