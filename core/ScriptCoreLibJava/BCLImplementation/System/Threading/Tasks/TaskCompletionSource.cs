using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJava.BCLImplementation.System.Threading.Tasks
{
    [Script(Implements = typeof(global::System.Threading.Tasks.TaskCompletionSource<>))]
    internal class __TaskCompletionSource<TResult>
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\TaskCompletionSource.cs
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201402/20140216/task

        public void SetResult(TResult result)
        {
        }
    }
}
