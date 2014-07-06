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


        public static Task<TElement> FirstOrDefaultAsync<TElement>(this IQueryStrategy<TElement> source)
        {
            // X:\jsc.svn\examples\javascript\LINQ\ClickCounter\ClickCounter\Application.cs

            var z = new TaskCompletionSource<TElement>();

            // was it manually set?
            QueryExpressionBuilder.WithConnection(
                (IDbConnection cc) =>
                {
                    FirstOrDefaultAsync(source, cc).ContinueWithResult(z.SetResult);
                }
            );
            return z.Task;
        }

        public static async Task<TElement> FirstOrDefaultAsync<TElement>(this IQueryStrategy<TElement> source, IDbConnection cc)
        {
            var x = await source.Take(1).AsEnumerableAsync(cc);

            return x.FirstOrDefault();
        }

    }

}
