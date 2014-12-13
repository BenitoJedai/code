using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Threading.Tasks;
using System.Threading;

namespace ScriptCoreLibNative.BCLImplementation.System
{
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Threading\Tasks\Task\Task.Run.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\Task\Task.Run.cs
    // X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\Threading\Tasks\Task\Task.Run.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\Task\Task.Run.cs

    internal partial class __Task
    {
        public static Task Run(Action y)
        {
            // on appengine we need to do special thread creation it seems.
            // X:\jsc.svn\core\ScriptCoreLibJava.AppEngine\ScriptCoreLibJava.AppEngine\Extensions\ThreadManagerExtensions.cs
            // X:\jsc.svn\examples\c\Test\TestTaskRun\TestTaskRun\Program.cs

            //var t = new TaskCompletionSource<object>();

            //script: error JSC1000: type not supported: ScriptCoreLibNative.BCLImplementation.System.__Task+<>c__DisplayClass1 ; consider adding [ScriptAttribute]
            
            new Thread(
                delegate()
                {
                    // in java it we can keep our call refs.

                    // in C we should be able to access scope and its function pointers?
                    y();

                    // signal ready?
                    //t.SetResult(null);
                }
            ).Start();


            //return t.Task;
            return null;
        }
    }

}
