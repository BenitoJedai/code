using ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.BCLImplementation.System;
using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks
{
    public partial class __Task
    {
        //// STAThread changes Task.Yield
        //[STAThread]

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

        [Obsolete("Task.Yield is not correctly working. workaround Task.Delay")]
        public static __YieldAwaitable Yield()
        {
            // "X:\jsc.svn\examples\rewrite\async\Test453Async\Test453Async.sln"

            // x:\jsc.svn\examples\javascript\async\asyncworkersourcesha1\asyncworkersourcesha1\application.cs

            // X:\jsc.svn\examples\javascript\Test\TestAsyncAssignArrayToEnumerable\TestAsyncAssignArrayToEnumerable\Application.cs
            // X:\jsc.svn\examples\java\hybrid\Test\TestJVMCLRAsync\TestJVMCLRAsync\Program.cs



            return new __YieldAwaitable { };
        }

    }
}
