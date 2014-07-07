﻿using System;
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

        public static Task InsertAsync<TElement>(this IQueryStrategy<TElement> source, TElement value)
        {
            Console.WriteLine("enter InsertAsync");
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestXMySQL\Program.cs

            var z = new TaskCompletionSource<Task>();

            // was it manually set?
            QueryExpressionBuilder.WithConnection(
                (IDbConnection cc) =>
                {
                    InsertAsync(source, cc, value).ContinueWith(
                        delegate
                    {
                        Console.WriteLine("after InsertAsync");

                        z.SetResult(null);
                    }
                    );
                }
            );
            Console.WriteLine("exit InsertAsync");
            return z.Task;
        }

        public static Task InsertAsync<TElement>(this IQueryStrategy<TElement> source, IDbConnection cc, TElement value)
        {
            // in CLR and in browser this would work.

            var xDbCommand = QueryExpressionBuilder.GetInsertCommand(source, cc, value) as DbCommand;
            // why ExecuteNonQueryAsync is not part of CLR, now we need to link in SQLite and PHP!

            if (xDbCommand != null)
            {
                Console.WriteLine("before ExecuteNonQueryAsync");
                var n = xDbCommand.ExecuteNonQueryAsync();
                return n;
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
