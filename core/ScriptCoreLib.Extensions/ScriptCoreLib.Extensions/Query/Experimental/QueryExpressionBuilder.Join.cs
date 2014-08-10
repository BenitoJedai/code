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

        }


        #region xJoin
        public class xJoin : IQueryStrategy
        {
            public IQueryStrategy outer;
            public IQueryStrategy inner;


            public LambdaExpression outerKeySelector;
            public LambdaExpression innerKeySelector;

            public LambdaExpression resultSelector;

            public override string ToString()
            {
                return "join " + resultSelector.Parameters[0].Name + " " + resultSelector.Parameters[1].Name;
            }
        }

        public class xJoin<TElement> : xJoin, IQueryStrategy<TElement>
        {
        }


        public static
            IQueryStrategy<TResult>
            Join<TOuter, TInner, TKey, TResult>(
            this IQueryStrategy<TOuter> outer,
            IQueryStrategy<TInner> inner,
            Expression<Func<TOuter, TKey>> outerKeySelector,
            Expression<Func<TInner, TKey>> innerKeySelector,
            Expression<Func<TOuter, TInner, TResult>> resultSelector
            )
        {
            return new xJoin<TResult>
            {
                outer = outer,
                inner = inner,
                outerKeySelector = outerKeySelector,
                innerKeySelector = innerKeySelector,
                resultSelector = resultSelector
            };
        }
        #endregion



    }

}
