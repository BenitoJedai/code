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
            public static readonly Func<IQueryStrategy<TElement>, TElement> FirstOrDefaultReference = FirstOrDefault;
        }

        public static TElement FirstOrDefault<TElement>(this IQueryStrategy<TElement> source)
        {
            // cache it?
            var sql = new SQLWriter<TElement>(source, new IQueryStrategy[0].AsEnumerable());


            return default(TElement);
        }

        public static TElement Last<TKey, TElement>(this IQueryStrategyGrouping<TKey, TElement> source)
        //public static TElement Last<TElement>(this IQueryStrategy<TElement> source)
        {
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestGroupBy\Program.cs

            return default(TElement);
        }

    }

}
