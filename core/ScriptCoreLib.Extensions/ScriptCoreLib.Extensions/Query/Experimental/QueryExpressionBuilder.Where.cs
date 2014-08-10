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
        class xWhere
        {
            public IQueryStrategy source;
            public IEnumerable<LambdaExpression> filter;

            public override string ToString()
            {
                return "where " + filter.First().Parameters[0].Name;
            }
        }

        class xWhere<TElement> : xWhere, IQueryStrategy<TElement>
        {

        }

        // called by LINQ
        public static IQueryStrategy<TElement> Where<TElement>(this IQueryStrategy<TElement> source, Expression<Func<TElement, bool>> filter)
        {
            // X:\jsc.svn\examples\javascript\LINQ\GGearAlpha\GGearAlpha\Library\GoogleGearsAdvanced.cs

            return Where(source, (LambdaExpression)filter);
        }

        // used by 
        // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\Experimental\QueryExpressionBuilder.IDbConnection.Delete.cs
        public static IQueryStrategy<TElement> Where<TElement>(this IQueryStrategy<TElement> source, LambdaExpression filter)
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
