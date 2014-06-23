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
            public static readonly Func<IQueryStrategy<TElement>, Expression<Func<TElement, object>>, IQueryStrategy<IQueryStrategyGrouping<object, TElement>>> GroupByReference = GroupBy;
        }



        public class xGroupBy
        {
            public IQueryStrategy source;
            public LambdaExpression keySelector;
            public LambdaExpression elementSelector;
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
            return new xGroupBy<IQueryStrategyGrouping<TKey, TSource>>
            {
                source = source,
                keySelector = keySelector,
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
