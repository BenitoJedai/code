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
            public static readonly Func<IQueryStrategy<TElement>, Expression<Func<TElement, bool>>, IQueryStrategy<TElement>> WhereReference = Where;
        }

        class xWhere
        {
            public IQueryStrategy source;
            public IEnumerable<LambdaExpression> filter;

            public override string ToString()
            {
                return (source as xSelect).selector.Parameters[0].Name;
            }
        }

        class xWhere<TElement> : xWhere, IQueryStrategy<TElement>
        {

        }

        // called by LINQ
        public static IQueryStrategy<TElement> Where<TElement>(this IQueryStrategy<TElement> source, Expression<Func<TElement, bool>> filter)
        {
            var xWhere = source as xWhere;
            if (xWhere != null)
            {
                // flatten where
                return new xWhere<TElement>
                {
                    source = xWhere.source,
                    filter = xWhere.filter.Concat(new[] { filter })
                };
            }

            return new xWhere<TElement>
            {
                source = source,
                filter = new[] { filter }
            };
        }


    }

}
