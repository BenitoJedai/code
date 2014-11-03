using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{

    [Script(Implements = typeof(global::System.Threading.Tasks.Task))]
    internal partial class __Task
    {
        public static implicit operator Task(__Task e)
        {
            return (Task)(object)e;
        }

        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Runtime\CompilerServices\YieldAwaitable.cs

        public static implicit operator __Task(Task e)
        {
            return (__Task)(object)e;
        }

        // X:\jsc.svn\examples\actionscript\async\Test\TestAsyncTaskRun\TestAsyncTaskRun\ApplicationSprite.cs

        public bool IsCompleted { get; set; }

        public Action InternalContinueWithAfterIsCompleted;

        public Task ContinueWith(Action<Task> continuationAction)
        {
            //Console.WriteLine("enter __Task.ContinueWith " + new { this.IsCompleted, continuationAction });

            var x = new TaskCompletionSource<object>();

            if (this.IsCompleted)
            {
                continuationAction(this);
                x.SetResult(null);
            }
            else
            {
                InternalContinueWithAfterIsCompleted +=
                    delegate
                    {
                        if (x == null)
                            return;

                        continuationAction(this);
                        x.SetResult(null);
                        x = null;
                    };
            }


            //Console.WriteLine("exit __Task.ContinueWith " + new { this.IsCompleted, continuationAction });
            return x.Task;
        }




        // http://msdn.microsoft.com/en-us/library/system.threading.tasks.task.getawaiter.aspx
        // !supported in: 4.5
        public __TaskAwaiter GetAwaiter()
        {
            //Console.WriteLine("enter __Task.GetAwaiter");

            // see also: X:\jsc.svn\examples\javascript\forms\FormsAsyncButtonExperiment\FormsAsyncButtonExperiment\ApplicationControl.cs

            var awaiter = new __TaskAwaiter
            {
                InternalIsCompleted = () => this.IsCompleted,
            };

            // ?
            this.ContinueWith(
                delegate
                {
                    //Console.WriteLine("continue __Task.GetAwaiter InternalOnCompleted");

                    if (awaiter.InternalOnCompleted != null)
                        awaiter.InternalOnCompleted();
                }
            );

            //Console.WriteLine("exit __Task.GetAwaiter");

            return awaiter;
        }
    }


    [Script(Implements = typeof(global::System.Threading.Tasks.Task<>))]
    internal partial class __Task<TResult> : __Task
    {
        public static implicit operator Task<TResult>(__Task<TResult> e)
        {
            return (Task<TResult>)(object)e;
        }

        public static implicit operator __Task<TResult>(Task<TResult> e)
        {
            return (__Task<TResult>)(object)e;
        }

        // called by
        // TaskCompletionSource.SetResult
        public void InternalSetCompleteAndYield(TResult result)
        {
            //Console.WriteLine("enter __Task.InternalSetCompleteAndYield");

            this.IsCompleted = true;
            this.Result = result;

            if (InternalContinueWithAfterIsCompleted != null)
                InternalContinueWithAfterIsCompleted();

            //Console.WriteLine("exit __Task.InternalSetCompleteAndYield");
        }


        public TResult Result { get; set; }


        public Task ContinueWith(Action<Task<TResult>> continuationAction)
        {
            var x = new TaskCompletionSource<object>();

            if (this.IsCompleted)
            {
                continuationAction(this);
                x.SetResult(null);
            }
            else
            {
                InternalContinueWithAfterIsCompleted +=
                    delegate
                    {
                        if (x == null)
                            return;

                        continuationAction(this);
                        x.SetResult(null);
                        x = null;
                    };
            }


            return x.Task;
        }
    }
}
