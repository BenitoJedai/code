using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq
{
    // http://referencesource.microsoft.com/#System.Core/System/Linq/ParallelEnumerable.cs

    [Script(Implements = typeof(global::System.Linq.ParallelEnumerable))]
    public static class __ParallelEnumerable
    {
        // tested by
        // ? X:\jsc.svn\examples\java\ParallelForEachExperiment\ParallelForEachExperiment\ApplicationControl.cs
        // X:\jsc.svn\examples\javascript\Test\TestParallelForEach\TestParallelForEach\Application.cs

        // see also
        // X:\jsc.svn\examples\javascript\Test\TestThreadStartAsWebWorker\TestThreadStartAsWebWorker\Application.cs

        public static ParallelQuery<TSource> AsParallel<TSource>(this IEnumerable<TSource> source)
        {
            return new __ParallelQuery<TSource>
            {
                InternalEnumerable = source,
                InternalEnumerableOfTSource = source
            };
        }

        public static ParallelQuery<TSource> WithExecutionMode<TSource>(this ParallelQuery<TSource> source, ParallelExecutionMode executionMode)
        {
            return source;
        }

        public static void ForAll<TSource>(this ParallelQuery<TSource> source, Action<TSource> action)
        {
            var all = source.AsEnumerable().Select(
                x =>
                {
                    //return Task.Factory.StartNew(
                    //    delegate
                    //    {
                    //        // does the scope sharing capture x and action?
                    //    }
                    //);

                    action(x);

                    return default(object);
                }
            ).ToArray();

        }
    }
}
