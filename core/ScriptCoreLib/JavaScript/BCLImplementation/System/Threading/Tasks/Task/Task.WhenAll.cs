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
    internal partial class __Task
    {
        // BCL4.5
        public static Task WhenAll(IEnumerable<Task> tasks)
        {
            return WhenAll(tasks.ToArray());
        }

        public static Task WhenAll(params Task[] tasks)
        {
            // used by
            // X:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\Query\Experimental\QueryExpressionBuilderAsync.IDbConnection.Insert.cs

            return __Task.InternalFactory.ContinueWhenAll(tasks,
                u =>
                {
                    // nop
                }
            );
        }

        public static Task<TResult[]> WhenAll<TResult>(params Task<TResult>[] tasks)
        {
            // tested by 
            // X:\jsc.svn\examples\javascript\forms\VBAsyncExperiment\VBAsyncExperiment\ApplicationControl.vb

            // script: error JSC1000: No implementation found for this native method, please implement [System.Threading.Tasks.TaskFactory.ContinueWhenAll(System.Threading.Tasks.Task[], System.Func`2[[System.Threading.Tasks.Task[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]])]

            //Console.WriteLine("__Task.WhenAll " + new { tasks.Length });

            return __Task.InternalFactory.ContinueWhenAll(tasks,
                u =>
                {
                    //Console.WriteLine("__Task.WhenAll yield " + new { tasks = tasks.Length, u = u.Length });

                    // nop

                    var a = u.Select(k => k.Result).ToArray();

                    //Console.WriteLine("__Task.WhenAll yield " + new { a = a.Length, Thread.CurrentThread.ManagedThreadId });

                    return a;
                },
                cancellationToken: default(CancellationToken),
                continuationOptions: default(TaskContinuationOptions),
                scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            );
        }

        public static Task<TResult[]> WhenAll<TResult>(IEnumerable<Task<TResult>> tasks)
        {
            return WhenAll( tasks.ToArray() );
        }

    }
}
