using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq
{

    static partial class __Enumerable
    {
        // X:\jsc.svn\examples\javascript\test\TestGroupJoin\TestGroupJoin\Application.cs
        // script: error JSC1000: No implementation found for this native method, please implement [static System.Linq.Enumerable.GroupJoin(


        public static IEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(
            this IEnumerable<TOuter> outer,
            IEnumerable<TInner> inner,
            Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector,
            Func<TOuter, IEnumerable<TInner>, TResult> resultSelector
            )
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140514
            // javac does not like this function?
            // X:\jsc.svn\examples\java\test\JVMCLRStringJoin\JVMCLRStringJoin\Program.cs

            return null;



            var c = Comparer<TKey>.Default;

            //var o = outer.ToArray();
            //var i = inner.ToArray();

            return
                from jo in outer
                let ko = outerKeySelector(jo)
                let e = from ji in inner
                        let ki = innerKeySelector(ji)
                        where c.Compare(ko, ki) == 0
                        select ji
                let r = resultSelector(jo, e)
                select r;

        }


    }
}
