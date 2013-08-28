using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks
{
    [Script(Implements = typeof(global::System.Threading.Tasks.TaskFactory))]
    internal class __TaskFactory
    {
    }

    [Script(Implements = typeof(global::System.Threading.Tasks.TaskFactory<>))]
    internal class __TaskFactory<TResult>
    {
        public Task<TResult> StartNew(Func<object, TResult> function, object state)
        {
            var x = new Task<TResult>(function, state);

            x.Start();

            return x;
        }
    }
}
