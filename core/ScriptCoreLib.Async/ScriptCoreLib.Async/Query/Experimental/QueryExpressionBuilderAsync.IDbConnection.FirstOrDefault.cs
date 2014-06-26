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
using System.Threading.Tasks;

namespace ScriptCoreLib.Query.Experimental
{
    public static partial class QueryExpressionBuilderAsync
    {
        public static async Task<TElement> FirstOrDefaultAsync<TElement>(this IQueryStrategy<TElement> source, IDbConnection cc)
        {
            var x = await source.Take(1).AsEnumerableAsync(cc);

            return x.FirstOrDefault();
        }

    }

}
