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

namespace ScriptCoreLib.Query.Experimental
{
    public static partial class QueryExpressionBuilderAsync
    {
        // X:\jsc.svn\examples\javascript\Test\TestSQLiteConnection\TestSQLiteConnection\Application.cs
        // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\Experimental\QueryExpressionBuilder.IDbConnection.Insert.cs


        public static Task<TKey[]> InsertAsync<TElement, TKey>(this QueryExpressionBuilder.xSelect<TKey, TElement> source, params TElement[] collection)
        {
            // used by
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectAverage\Program.cs
            // x:\jsc.svn\examples\javascript\linq\test\auto\testselect\testweborderbythengroupby\application.cs
            Console.WriteLine("enter InsertAsync");
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestXMySQL\Program.cs

            var z = new TaskCompletionSource<TKey[]>();

            // was it manually set?
            QueryExpressionBuilder.WithConnection(
                (IDbConnection cc) =>
                {
                    var i = from c in collection
                            select InsertAsync(source, cc, c);


                    //Task.Factory.ContinueWhenAll(
                    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\Task\Task.WhenAll.cs
                    Task.WhenAll(i.ToArray()).ContinueWith(
                        delegate
                    {
                        Console.WriteLine("after InsertAsync");

                        z.SetResult(
                            i.Select(x => x.Result).ToArray()
                            );
                    }
                    );
                }
            );
            Console.WriteLine("exit InsertAsync");
            return z.Task;
        }

        public static Task<TKey> InsertAsync<TElement, TKey>(this QueryExpressionBuilder.xSelect<TKey, TElement> source, TElement value)
        {
            Console.WriteLine("enter InsertAsync");
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestXMySQL\Program.cs
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestWebInsert\Application.cs

            var z = new TaskCompletionSource<TKey>();

            // was it manually set?
            QueryExpressionBuilder.WithConnection(
                (IDbConnection cc) =>
                {
                    InsertAsync(source, cc, value).ContinueWith(
                        task =>
                    {
                        Console.WriteLine("after InsertAsync");

                        z.SetResult(task.Result);
                    }
                    );
                }
            );
            Console.WriteLine("exit InsertAsync");
            return z.Task;
        }

        public static Task<TKey> InsertAsync<TElement, TKey>(this QueryExpressionBuilder.xSelect<TKey, TElement> source, IDbConnection cc, TElement value)
        {
            // in CLR and in browser this would work.

            var xDbCommand = QueryExpressionBuilder.GetInsertCommand(source, cc, value) as DbCommand;
            // why ExecuteNonQueryAsync is not part of CLR, now we need to link in SQLite and PHP!

            if (xDbCommand != null)
            {
                Console.WriteLine("before ExecuteNonQueryAsync");
                var n = xDbCommand.ExecuteNonQueryAsync();

                var c = new TaskCompletionSource<TKey>();

                n.ContinueWith(
                    task =>
                    {
                        // jsc makes all Keys of long, yet data layer seems to talk int?
                        long LastInsertRowId = IDbConnectionExtensions.GetLastInsertRowId(cc);

                        Console.WriteLine("InsertAsync " + new { LastInsertRowId });

                        c.SetResult(
                            (TKey)(object)LastInsertRowId
                        );
                    }
                );

                return c.Task;
            }


            // how would this work in the browser if scriptcorelib does not yet provide the implementation?
            //var xMySQLCommand = c as System.Data.MySQL.MySQLCommand;
            //if (xMySQLCommand != null)
            //{
            //    var n = xMySQLCommand.ExecuteNonQueryAsync();
            //    return n;
            //}

            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestXMySQL\Program.cs
            // should we report back the new key?

            throw new NotSupportedException();
        }


    }

}
