using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    internal class __Task
    {
    }

    internal class __Task<TResult>
    {
        // see also: http://msdn.microsoft.com/en-us/library/system.threading.tasks.task.getawaiter(v=vs.110).aspx
        public __TaskAwaiter<TResult> GetAwaiter()
        {
            return default(__TaskAwaiter<TResult>);
        }
    }
}
