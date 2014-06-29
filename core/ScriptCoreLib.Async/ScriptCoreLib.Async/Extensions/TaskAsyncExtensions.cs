using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Threading.Tasks
//namespace ScriptCoreLib.Extensions
{
    public static class TaskAsyncExtensions
    {
        public static Task<TSource> AsResult<TSource>(this TSource source)
        {
            // defined only for 4.5
            // http://msdn.microsoft.com/en-us/library/hh228607(v=vs.110).aspx
            return Task.FromResult(source);
        }

        [Obsolete("AsResult")]
        public static Task<TSource> ToTaskResult<TSource>(this TSource source)
        {
            return Task.FromResult(source);
        }
    }

    public static class TaskAsyncIProgressExtensions
    {

        // defined only for 4.5
        [Obsolete]
        public static Task<TSource> StartNewWithProgress<TSource>(this TaskFactory that,
            TSource state,
            Func<Tuple<IProgress<TSource>, TSource>, TSource> function,
            Action<TSource> progress

            ) where TSource : class
        {
            if (progress == null)
                throw new InvalidOperationException("StartNewWithProgress progress null");

            IProgress<TSource> p = new Progress<TSource>(progress);

            //Error	8	Argument 1: cannot convert from
            // 'System.Func<System.Tuple<System.IProgress<TResult>,TSource>,TResult>' to
            // 'System.Func<object,TResult>'	X:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\Extensions\TaskAsyncExtensions.cs	20	25	ScriptCoreLib.Async

            //Error	193	The type arguments for method 'System.Threading.Tasks.ScriptCoreLib_TaskExtensions.StartNew<TSource,TResult>(System.Threading.Tasks.TaskFactory, TSource, System.Func<TSource,TResult>)' cannot be inferred from the usage. Try specifying the type arguments explicitly.	X:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\Extensions\TaskAsyncExtensions.cs	23	20	ScriptCoreLib.Async


            return System.Threading.Tasks.ScriptCoreLib_TaskExtensions.StartNew<
                Tuple<IProgress<TSource>, TSource>,
                TSource>
                (
                    that: Task.Factory,
                    state: Tuple.Create(p, state),
                    function: function
                );

        }
    }
}
