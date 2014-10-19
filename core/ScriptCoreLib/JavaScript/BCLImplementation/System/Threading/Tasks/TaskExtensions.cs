using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks
{
    // http://referencesource.microsoft.com/#System.Core/System/Threading/Tasks/TaskExtensions.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Threading.Tasks/TaskExtensionsImpl.cs
    // https://github.com/mono/mono/blob/master/mcs/class/System.Core/System.Threading.Tasks/TaskExtensions.cs

    [Script(Implements = typeof(global::System.Threading.Tasks.TaskExtensions))]
    public static class __TaskExtensions
    {
        public static Task<TResult> Unwrap<TResult>(Task<Task<TResult>> task)
        {
            //Console.WriteLine("enter Unwrap");

            // X:\jsc.svn\examples\javascript\async\test\TaskAsyncTaskRun\TaskAsyncTaskRun\Application.cs

            //async worker done0:3872ms 
            //0:3872ms Task ContinueWithResult
            //0:3873ms async worker running ? { xTask = [object Object] }
            //Uncaught TypeError: undefined is not a function 
            var x = new TaskCompletionSource<TResult>();

            task.ContinueWith(
                r =>
                {
                    var xTask = r.Result;

                    //var isTaskOfT = xTask is Task<object>;
                    //Console.WriteLine("async worker running ? " + new { xTask, isTaskOfT });

                    xTask.ContinueWith(
                        rr =>
                        {
                            x.SetResult(
                                rr.Result
                            );
                        }
                    );
                }
            );

            return x.Task;
        }


        public static Task Unwrap(Task<Task> task)
        {
            // X:\jsc.svn\examples\javascript\async\test\TestWorkerScopeProgress\TestWorkerScopeProgress\Application.cs

            //Console.WriteLine("enter Unwrap");

            // X:\jsc.svn\examples\javascript\async\test\TaskAsyncTaskRun\TaskAsyncTaskRun\Application.cs

            //async worker done0:3872ms 
            //0:3872ms Task ContinueWithResult
            //0:3873ms async worker running ? { xTask = [object Object] }
            //Uncaught TypeError: undefined is not a function 
            var x = new TaskCompletionSource<object>();

            task.ContinueWith(
                (Task<Task> r) =>
                {
                    var xTask = r.Result;


                    xTask.ContinueWith(
                        rr =>
                        {
                            x.SetResult(
                                new object()
                            );
                        }
                    );
                }
            );

            return x.Task;
        }
    }
}
