using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks
{

    [Script(Implements = typeof(global::System.Threading.Tasks.TaskCompletionSource<>))]
    internal class __TaskCompletionSource<TResult>
    {
        // http://stackoverflow.com/questions/15316613/real-life-scenarios-for-using-taskcompletionsourcet

        public __Task<TResult> InternalTask;

        public Task<TResult> Task { get { return this.InternalTask; } }

        public __TaskCompletionSource()
        {
            this.InternalTask = new __Task<TResult> { InternalStart = null };
        }

        public void SetResult(TResult result)
        {
            this.InternalTask.InternalSetCompleteAndYield(result);
        }
    }

  
}
