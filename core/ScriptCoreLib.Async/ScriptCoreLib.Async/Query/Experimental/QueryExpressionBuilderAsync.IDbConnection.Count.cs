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
//using System.Data.MySQL;

namespace ScriptCoreLib.Query.Experimental
{
    public static partial class QueryExpressionBuilderAsync
    {
        // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\Experimental\QueryExpressionBuilder.IDbConnection.Count.cs
        // X:\jsc.svn\examples\javascript\Test\TestSQLiteConnection\TestSQLiteConnection\Application.cs
        // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\Experimental\QueryExpressionBuilder.IDbConnection.Insert.cs
        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestXMySQL\Program.cs

        // X:\jsc.svn\examples\javascript\LINQ\ClickCounter\ClickCounter\Application.cs
        public static Task<long> CountAsync<TElement>(this IQueryStrategy<TElement> source)
        {
            var z = new TaskCompletionSource<long>();

            // was it manually set?
            QueryExpressionBuilder.WithConnection(
                (IDbConnection cc) =>
                {
                    CountAsync(source, cc).ContinueWithResult(z.SetResult);
                }
            );
            return z.Task;
        }

        public static Task<long> CountAsync<TElement>(this IQueryStrategy<TElement> source, IDbConnection cc)
        {
            // in CLR and in browser this would work.

            var z = new TaskCompletionSource<long>();


            // Error    178 'ScriptCoreLib.Query.Experimental.QueryExpressionBuilder.SQLWriter<TElement>' does not contain a definition for 'CountReference'    X:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\Query\Experimental\QueryExpressionBuilderAsync.IDbConnection.Count.cs   47  112 ScriptCoreLib.Async

           var c = (DbCommand)source.GetScalarCommand(cc, Operand: QueryExpressionBuilder.xReferencesOfLong.CountReference.Method );

            // http://referencesource.microsoft.com/#System.Data/data/System/Data/Common/DBCommand.cs
            var xDbCommand = c as DbCommand;
            if (xDbCommand != null)
            {
                var n = xDbCommand.ExecuteScalarAsync();

                // X:\jsc.svn\examples\javascript\LINQ\LINQWebCamAvatars\LINQWebCamAvatars\Application.cs

                n.ContinueWithResult(
                    zz =>
                    {
                        z.SetResult((long)zz);
                    }
                );

                return z.Task;
            }



            throw new NotSupportedException();
        }


    }

}
