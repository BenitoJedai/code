using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Threading.Tasks
{
    // http://referencesource.microsoft.com/#mscorlib/system/threading/Tasks/TaskCompletionSource.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Threading.Tasks/TaskCompletionSource.cs

    [Script(Implements = typeof(global::System.Threading.Tasks.TaskCompletionSource<>))]
    internal class __TaskCompletionSource<TResult>
    {
        // http://stackoverflow.com/questions/15316613/real-life-scenarios-for-using-taskcompletionsourcet

        public __Task<TResult> InternalTask;

        public Task<TResult> Task { get { return this.InternalTask; } }

        public __TaskCompletionSource()
        {
            this.InternalTask = new __Task<TResult>
            {
                //InternalStart = null 
            };
        }

        public void SetResult(TResult result)
        {
            // X:\jsc.svn\core\ScriptCoreLib\ActionScript\flash\display\InteractiveObject.cs

            if (this.InternalTask.IsCompleted)
                return;

            //Console.WriteLine("enter __TaskCompletionSource.SetResult");
            this.InternalTask.InternalSetCompleteAndYield(result);
        }


        public override string ToString()
        {
            // X:\jsc.svn\examples\actionscript\air\AIRThreadedSoundAsync\AIRThreadedSoundAsync\ApplicationSprite.cs

            return "TaskCompletionSource " + this.InternalTask;

        }
    }


}
