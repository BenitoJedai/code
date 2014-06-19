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
            public static readonly Func<IQueryStrategy<object>, Expression<Func<object, TElement>>, IQueryStrategy<TElement>> SelectReference = Select;
        }

        #region Select
        // allow xTable to predefine a select
        public class xSelect
        {
            public IQueryStrategy source;
            public LambdaExpression selector;

            public override string ToString()
            {
                return "select " + selector.Parameters[0].Name;
            }
        }

        [Obsolete("inherited by generated data table objects?")]
        public class xSelect<TResult> : xSelect, IQueryStrategy<TResult>
        {

        }

        // called by LINQ
        public static IQueryStrategy<TResult> Select<TSource, TResult>(this IQueryStrategy<TSource> source, Expression<Func<TSource, TResult>> selector)
        {
            return new xSelect<TResult>
            {
                source = source,
                selector = selector
            };
        }
        #endregion




    }

}
