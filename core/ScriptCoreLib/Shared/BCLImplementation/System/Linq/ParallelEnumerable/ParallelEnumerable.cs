using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq
{
    // http://referencesource.microsoft.com/#System.Core/System/Linq/ParallelEnumerable.cs

    [Script(Implements = typeof(global::System.Linq.ParallelEnumerable))]
    public static class __ParallelEnumerable
    {
        // tested by
        // ? X:\jsc.svn\examples\java\ParallelForEachExperiment\ParallelForEachExperiment\ApplicationControl.cs

        // see also
        // X:\jsc.svn\examples\javascript\Test\TestThreadStartAsWebWorker\TestThreadStartAsWebWorker\Application.cs

        public static ParallelQuery<TSource> AsParallel<TSource>(this IEnumerable<TSource> source)
        {
            throw new NotImplementedException();
        }

        public static ParallelQuery<TSource> WithExecutionMode<TSource>(this ParallelQuery<TSource> source, ParallelExecutionMode executionMode)
        {
            throw new NotImplementedException();
        }

        public static void ForAll<TSource>(this ParallelQuery<TSource> source, Action<TSource> action)
        {
            throw new NotImplementedException();
        }
    }
}
