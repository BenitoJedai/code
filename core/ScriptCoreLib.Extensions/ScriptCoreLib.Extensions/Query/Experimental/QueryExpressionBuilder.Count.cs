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
            public static readonly Func<IQueryStrategy, long> CountReference = Count;
        }

        // first lets allow scalar subqueries
        public static long Count(this IQueryStrategy Strategy)
        {
            return 0;
        }





    }

}
