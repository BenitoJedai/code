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




        #region GroupBy
        public class xGroupBy
        {
            public IQueryStrategy source;
            public LambdaExpression keySelector;
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
             GroupBy
             <TSource, TKey>(
                 this IQueryStrategy<TSource> source,
                 Expression<Func<TSource, TKey>> keySelector
            )
        {
            return new xGroupBy<IQueryStrategyGrouping<TKey, TSource>>
            {
                source = source,
                keySelector = keySelector,
            };
        }
        #endregion


    }

}
