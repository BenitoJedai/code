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
using System.Data.MySQL;

namespace ScriptCoreLib.Query.Experimental
{
    public static partial class QueryExpressionBuilderAsync
    {
        // X:\jsc.svn\examples\javascript\Test\TestSQLiteConnection\TestSQLiteConnection\Application.cs
        // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\Experimental\QueryExpressionBuilder.IDbConnection.Insert.cs
        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestXMySQL\Program.cs

        public static Task<long> CountAsync<TElement>(this IQueryStrategy<TElement> source, IDbConnection cc)
        {
            // in CLR and in browser this would work.

            var z = new TaskCompletionSource<long>();

            var c = source.GetCountCommand(cc);

            #region xSQLiteCommand
            var xSQLiteCommand = c as SQLiteCommand;
            if (xSQLiteCommand != null)
            {
                var n = xSQLiteCommand.ExecuteScalarAsync();

                // X:\jsc.svn\examples\javascript\LINQ\LINQWebCamAvatars\LINQWebCamAvatars\Application.cs

                n.ContinueWithResult(
                    zz =>
                    {
                        z.SetResult((long)zz);
                    }
                );

                return z.Task;
            }
            #endregion


            // how would this work in the browser if scriptcorelib does not yet provide the implementation?
            #region xMySQLCommand
            var xMySQLCommand = c as MySQLCommand;
            if (xMySQLCommand != null)
            {
                Console.WriteLine("before xMySQLCommand ExecuteScalarAsync");
                var n = xMySQLCommand.ExecuteScalarAsync();
                Console.WriteLine("after xMySQLCommand ExecuteScalarAsync " + new { n.IsCompleted });

                // X:\jsc.svn\examples\javascript\LINQ\LINQWebCamAvatars\LINQWebCamAvatars\Application.cs

                n.ContinueWithResult(
                    zz =>
                    {
                        Console.WriteLine("at xMySQLCommand ExecuteScalarAsync");

                        z.SetResult((long)zz);
                    }
                );

                return z.Task;
            }
            #endregion

            throw new NotSupportedException();
        }


    }

}
