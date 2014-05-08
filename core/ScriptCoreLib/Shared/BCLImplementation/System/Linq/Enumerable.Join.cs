using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq
{

    static partial class __Enumerable
    {

        public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(
            this IEnumerable<TOuter> outer,
            IEnumerable<TInner> inner,
            Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector,
            Func<TOuter, TInner, TResult> resultSelector)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140429
            // X:\jsc.svn\examples\javascript\test\TestLINQJoin\TestLINQJoin\Application.cs

            var c = Comparer<TKey>.Default;
            var s = Stopwatch.StartNew();

            //Console.WriteLine("enter Join");


            var o = outer.ToArray();
            var i = inner.ToArray();

            return
                from jo in outer
                from ji in inner
                let ko = outerKeySelector(jo)
                let ki = innerKeySelector(ji)
                where c.Compare(ko, ki) == 0
                let r = resultSelector(jo, ji)
                select r;


            //Console.WriteLine("exit Join " + new { s.ElapsedMilliseconds });
            //return null;
        }

        //public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(
        //    this IEnumerable<TOuter> outer, 
        //    IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, 
        //    Func<TInner, TKey> innerKeySelector, 
        //    Func<TOuter, TInner, TResult> resultSelector, 
        //    IEqualityComparer<TKey> comparer)
        //{

        //}
    }
}
