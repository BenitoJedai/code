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






        #region OrderBy
        public class xOrderBy
        {
            public IQueryStrategy source;


            public IEnumerable<LambdaExpression> keySelector;
        }

        public class xOrderBy<TElement> : xOrderBy, IQueryStrategy<TElement>
        {
        }

        public static IQueryStrategy<TElement> ThenBy<TElement, TKey>(this IQueryStrategy<TElement> source, Expression<Func<TElement, TKey>> keySelector)
        {
            var xOrderBy = source as xOrderBy;
            if (xOrderBy != null)
            {
                // flatten orderbys
                return new xOrderBy<TElement>
                {
                    source = xOrderBy.source,
                    keySelector = xOrderBy.keySelector.Concat(new[] { keySelector })
                };
            }

            return new xOrderBy<TElement>
            {
                source = source,
                keySelector = new[] { keySelector }
            };
        }

        //[Obsolete("mutable")]
        public static IQueryStrategy<TElement> OrderBy<TElement, TKey>(this IQueryStrategy<TElement> source, Expression<Func<TElement, TKey>> keySelector)
        {
            return new xOrderBy<TElement>
            {
                source = source,
                keySelector = new[] { keySelector }
            };
        }
        #endregion




    }

}
