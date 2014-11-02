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
    internal partial class __Task
    {
        // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Threading\Tasks\Task\Task.Delay.cs

        //02000022 TestTaskDelay.ApplicationControl+<button3_Click>d__2+<MoveNext>0600001a
        //arg[0] is typeof System.Boolean
        //script: error JSC1000: No implementation found for this native method, please implement [System.Threading.Tasks.Task.ConfigureAwait(System.Boolean)]

        //public ConfiguredTaskAwaitable ConfigureAwait(bool continueOnCapturedContext)
        //{ 

        //}

        public static Task Delay(int millisecondsDelay)
        {
            // X:\jsc.svn\examples\javascript\Test\TestTaskDelay\TestTaskDelay\ApplicationControl.cs
            // "X:\jsc.svn\examples\javascript\Test\TestTaskDelay\TestTaskDelay.sln"
            // X:\jsc.svn\examples\javascript\async\ColorDisco\ColorDisco\ApplicationWebService.cs

            // tested by?

            var t = new __Task { };

            new ScriptCoreLib.JavaScript.Runtime.Timer(
                delegate
                {
                    t.InternalSetCompleteAndYield();
                }
            ).StartTimeout(millisecondsDelay);


            return t;
        }

    }
}
