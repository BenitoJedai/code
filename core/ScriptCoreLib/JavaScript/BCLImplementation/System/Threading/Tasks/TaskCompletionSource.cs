using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks
{
    // http://referencesource.microsoft.com/#mscorlib/system/threading/Tasks/TaskCompletionSource.cs
    // https://github.com/mono/mono/blob/a31c107f59298053e4ff17fd09b2fa617b75c1ba/mcs/class/corlib/System.Threading.Tasks/TaskCompletionSource.cs

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
