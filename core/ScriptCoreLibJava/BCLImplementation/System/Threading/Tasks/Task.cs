using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJava.BCLImplementation.System.Threading.Tasks
{
    // http://msdn.microsoft.com/en-us/library/system.threading.tasks.task.aspx
    [Script(Implements = typeof(global::System.Threading.Tasks.Task))]
    internal class __Task
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\Task.cs
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201402/20140216/task

    }

    [Script(Implements = typeof(global::System.Threading.Tasks.Task<>))]
    internal class __Task<TResult> : __Task
    {

    }
}
