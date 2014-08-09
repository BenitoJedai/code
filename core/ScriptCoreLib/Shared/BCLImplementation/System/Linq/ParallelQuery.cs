using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq.Expressions
{
    // http://referencesource.microsoft.com/#System.Core/System/Linq/Parallel/Enumerables/ParallelQuery.cs
    [Script(Implements = typeof(global::System.Linq.ParallelQuery))]
    internal class __ParallelQuery : IEnumerable
    {

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    [Script(Implements = typeof(global::System.Linq.ParallelQuery))]
    internal class __ParallelQuery<TSource> : __ParallelQuery, IEnumerable<TSource>
    {
        public new IEnumerator<TSource> GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
