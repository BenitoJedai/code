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
        // X:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\Query\Experimental\QueryExpressionBuilderAsync.SumAsync.cs

        public static long Sum(this IQueryStrategy<long> source)
        {
            var value = default(long);

            // tested by
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSQLiteCLRSumLong\Program.cs

            QueryExpressionBuilder.WithConnection(
                cc =>
                {
                    var xDbCommand = QueryExpressionBuilder.GetScalarCommand(
                        source,
                        cc: cc,
                        Operand: QueryExpressionBuilder.xReferencesOfLong.SumOfLongReference.Method
                    );

                    var __value = xDbCommand.ExecuteScalar();

                    value = (long)__value;
                }
            );

            return value;
        }







    }

}
