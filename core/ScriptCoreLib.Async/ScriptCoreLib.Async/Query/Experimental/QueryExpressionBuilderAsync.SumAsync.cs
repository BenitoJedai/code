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
        //  x:\jsc.svn\core\scriptcorelib.extensions\scriptcorelib.extensions\query\experimental\queryexpressionbuilder.sum.cs

        public static Task<long> SumAsync(this IQueryStrategy<long> source)
        {
            Console.WriteLine("enter SumAsync");
            // tested by
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestWebSumLong\Application.cs


            var z = new TaskCompletionSource<long>();

            //QueryExpressionBuilder.WithConnectionAsync(
            QueryExpressionBuilder.WithConnection(
                async cc =>
                {
                    Console.WriteLine("enter WithConnection SumAsync");

                    var xDbCommand = QueryExpressionBuilder.GetScalarCommand(
                        source,
                        cc: cc,
                        Operand: QueryExpressionBuilder.xReferencesOfLong.SumOfLongReference.Method
                    );

                    Console.WriteLine("enter WithConnection SumAsync before ExecuteScalarAsync");
                    // will WithConnection actually wait or terminate?
                    // never returns?
                    var __value = await xDbCommand.ExecuteScalarAsync();

                    Console.WriteLine("enter WithConnection SumAsync after ExecuteScalarAsync " + new { __value });

                    z.SetResult(
                        (long)__value
                    );

                }
            );

            return z.Task;
        }

    }

}
