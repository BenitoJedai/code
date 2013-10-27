using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq
{

    [Script(Implements = typeof(global::System.Linq.Enumerable))]
    public static partial class __Enumerable
    {



        public static IEnumerable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            // X:\jsc.svn\examples\java\JVMCLRPrivateAddress\JVMCLRPrivateAddress\Program.cs

            throw new NotImplementedException();
        }

    }

}
