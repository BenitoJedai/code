using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ScriptCoreLib.Extensions;
using System.Reflection;

namespace ScriptCoreLib.Query.Experimental
{
    public static partial class QueryExpressionBuilder
    {

        partial class SQLWriter<TElement>
        {



            //[Obsolete("we need a MemberInfo for Key. are we correctly using it everywhere? or should we se IGrouping.Key?")]
            //public static TKey Key< TKey, TElement>(IQueryStrategyGrouping< TKey, TElement> g)
            //{
            //    return g.Key;
            //}
        }



        public class xGroupBy : IQueryStrategy
        {
            public IQueryStrategy source;
            public LambdaExpression keySelector;
            public LambdaExpression elementSelector;

            public override string ToString()
            {
                return "groupby " + keySelector.Parameters[0].Name ;
            }
        }

        public class xGroupBy<TElement> : xGroupBy, IQueryStrategy<TElement>
        {
        }


        // used by order by GroupingKey detection
        public interface IQueryStrategyGrouping
        {
        }

        [Obsolete("to make intellisense happy, and dispay only supported methods")]
        //public interface IQueryStrategyGrouping<out TKey, out TElement> : IQueryStrategy<TElement>
        public interface IQueryStrategyGrouping<out TKey, TElement> : IQueryStrategy<TElement>, IQueryStrategyGrouping
        {
            TKey Key { get; }
        }

        public static IQueryStrategy<IQueryStrategyGrouping<TKey, TSource>>
             GroupBy<TSource, TKey>(this IQueryStrategy<TSource> source, 
            Expression<Func<TSource, TKey>> keySelector)
        {
            // ReferenceError: type$AAAAAAAAAAAAAAAAAAAAAA is not defined
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestWebOrderByThenGroupBy\Application.cs
            // jsc. need to do MakeArray?

            Expression<Func<TSource, TSource>> elementSelector = e => e;


            return new xGroupBy<IQueryStrategyGrouping<TKey, TSource>>
            {
                source = source,
                keySelector = keySelector,

                // better than null?
                elementSelector = elementSelector
            };
        }

        public static IQueryStrategy<IQueryStrategyGrouping<TKey, TElement>>
             GroupBy<TSource, TKey, TElement>(this IQueryStrategy<TSource> source,
                 Expression<Func<TSource, TKey>> keySelector,
                 Expression<Func<TSource, TElement>> elementSelector
            )
        {
            return new xGroupBy<IQueryStrategyGrouping<TKey, TElement>>
            {
                source = source,
                keySelector = keySelector,
                elementSelector = elementSelector 
            };
        }

    }

}
