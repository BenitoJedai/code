using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ScriptCoreLib.Extensions;
using System.Reflection;
using System.Data;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.SQLite;

namespace ScriptCoreLib.Query.Experimental
{
    public static partial class QueryExpressionBuilderAsync
    {
        // X:\jsc.svn\examples\javascript\Test\TestSQLiteConnection\TestSQLiteConnection\Application.cs
        // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\Experimental\QueryExpressionBuilder.IDbConnection.Insert.cs

        public static Task<long> CountAsync<TElement>(this IQueryStrategy<TElement> source, IDbConnection cc)
        {
            // in CLR and in browser this would work.

            var z = new TaskCompletionSource<long>();

            var c = source.GetCountCommand(cc) as SQLiteCommand;
            var n = c.ExecuteScalarAsync();

            // X:\jsc.svn\examples\javascript\LINQ\LINQWebCamAvatars\LINQWebCamAvatars\Application.cs

            n.ContinueWithResult(
                zz =>
                {
                    z.SetResult((long)zz);
                }
            );

            return z.Task;
        }


    }

}
