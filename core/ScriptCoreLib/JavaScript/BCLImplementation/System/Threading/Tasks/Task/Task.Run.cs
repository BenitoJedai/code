using ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.BCLImplementation.System;
using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks
{
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Threading\Tasks\Task\Task.Run.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\Task\Task.Run.cs
    // X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\Threading\Tasks\Task\Task.Run.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\Task\Task.Run.cs

    public partial class __Task
    {
        // X:\jsc.svn\examples\java\hybrid\JVMCLRHopToThreadPool\JVMCLRHopToThreadPool\Program.cs

        // X:\jsc.svn\examples\actionscript\FlashWorkerExperiment\FlashWorkerExperiment\ApplicationSprite.cs
        // what about AIR?

        // X:\jsc.svn\examples\javascript\async\test\TestTaskRun\TestTaskRun\Application.cs
        // X:\jsc.svn\examples\javascript\async\test\TaskAsyncTaskRun\TaskAsyncTaskRun\Application.cs

        public static Task Run<TResult>(Func<Task> function)
        {
            // X:\jsc.svn\examples\javascript\async\test\TestWorkerScopeProgress\TestWorkerScopeProgress\Application.cs

            return Task.Factory.StartNew(function).Unwrap();
        }

        public static Task<TResult> Run<TResult>(Func<Task<TResult>> function)
        {
            return Task.Factory.StartNew(function).Unwrap();
        }

        public static Task<TResult> Run<TResult>(Func<TResult> function)
        {
            // X:\jsc.svn\examples\javascript\WorkerMD5Experiment\WorkerMD5Experiment\Application.cs

            //new Task(
            return Task.Factory.StartNew(function);
        }


        //public static Task<TResult> Unwrap<TResult>(this Task<Task<TResult>> task);




        [Script]
        sealed class InternalTaskExtensionsScope
        {
            [Obsolete("Special hint for JavaScript runtime, until scope sharing is implemented..")]
            public Action InternalTaskExtensionsScope_function;

            public void f()
            {
                this.InternalTaskExtensionsScope_function();
            }
        }

        [Obsolete("scope sharing, do we have it yet?")]
        public static Task Run(Action action)
        {
            // X:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\Extensions\TaskAsyncExtensions.cs
            // X:\jsc.svn\examples\javascript\Test\TestHopToThreadPoolAwaitable\TestHopToThreadPoolAwaitable\Application.cs
            // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Extensions\TaskExtensions.cs

            //return Task.Factory.StartNew(action);



            //// ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices.__AsyncVoidMethodBuilder+<>c__DisplayClass2`2.<AwaitUnsafeOnCompleted>b__1
            //type$O_b44J8AxbTiq5EFPbq1SVA.nicABsAxbTiq5EFPbq1SVA = function ()
            //{
            //  var a = [this];
            //  a[0].yield.hCAABiRtYD2yr4CzwPIbLw();
            //};


            var xx = new InternalTaskExtensionsScope { InternalTaskExtensionsScope_function = action };


            var x = new __Task<object>();

            x.InternalInitializeInlineWorker(
                new Action(xx.f),
                //action,
                default(object),
                default(CancellationToken),
                default(TaskCreationOptions),
                TaskScheduler.Default
            );


            x.Start();

            return (Task<object>)x;
        }
    }
}
