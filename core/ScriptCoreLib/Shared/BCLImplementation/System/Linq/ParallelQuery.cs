using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq
{
    // http://referencesource.microsoft.com/#System.Core/System/Linq/Parallel/Enumerables/ParallelQuery.cs
    [Script(Implements = typeof(global::System.Linq.ParallelQuery))]
    internal class __ParallelQuery : IEnumerable
    {
        public IEnumerable InternalEnumerable;

        public IEnumerator GetEnumerator()
        {
            return InternalEnumerable.GetEnumerator();
        }
    }

    [Script(Implements = typeof(global::System.Linq.ParallelQuery<>))]
    internal class __ParallelQuery<TSource> : __ParallelQuery, IEnumerable<TSource>
    {
        public IEnumerable<TSource> InternalEnumerableOfTSource;

        public new IEnumerator<TSource> GetEnumerator()
        {
            // X:\jsc.svn\examples\javascript\Test\TestParallelForEach\TestParallelForEach\Application.cs
            // jsc ActioScript already calls AsEnumerable automatically
            // why do not we do it for js ?

            return InternalEnumerableOfTSource.AsEnumerable().GetEnumerator();
        }


        public static implicit operator ParallelQuery<TSource>(__ParallelQuery<TSource> e)
        {
            return (ParallelQuery<TSource>)(object)e;
        }
    }
}
