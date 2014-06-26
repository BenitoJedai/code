using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ScriptCoreLib.Extensions;
using System.Reflection;
using System.IO;
using System.Data;

namespace ScriptCoreLib.Query.Experimental
{
    public static partial class QueryExpressionBuilder
    {
        public static TElement FirstOrDefault<TElement>(this IQueryStrategy<TElement> source, IDbConnection cc)
        {
            return source.Take(1).AsEnumerable(cc).FirstOrDefault();
        }

    }

}
