using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    // http://msdn.microsoft.com/en-us/library/system.threading.tasks.task.aspx
    [Script(Implements = typeof(global::System.Threading.Tasks.Task))]
    internal class __Task
    {
        // http://msdn.microsoft.com/en-us/library/system.threading.tasks.task.getawaiter.aspx
        // !supported in: 4.5
        public __TaskAwaiter GetAwaiter()
        {
            return default(__TaskAwaiter);
        }
    }

    [Script(Implements = typeof(global::System.Threading.Tasks.Task<>))]
    internal class __Task<TResult>
    {
        // see also: http://msdn.microsoft.com/en-us/library/system.threading.tasks.task.getawaiter(v=vs.110).aspx
        public __TaskAwaiter<TResult> GetAwaiter()
        {
            return default(__TaskAwaiter<TResult>);
        }
    }
}
