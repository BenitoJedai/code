using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ScriptCoreLib.Extensions;
using System.Reflection;
using System.Data;

namespace ScriptCoreLib.Query.Experimental
{
    public static partial class QueryExpressionBuilder
    {


        [Obsolete("this might need to return the row for the selector")]
        public static double Average<TSource>(this IQueryStrategy<TSource> source, Expression<Func<TSource, long>> selector)
        {
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectAverage\Program.cs

            return source.Select(selector).Average();
        }





        // TElement : long
        public static double Average(this IQueryStrategy<long> source)
        {
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectAverage\Program.cs

            var value = default(double);

            // what if there is no connection?
            WithConnection(
                cc =>
                {
                    value = Average(source, cc);
                }
            );

            return value;
        }

        public static double Average(this IQueryStrategy<long> source, IDbConnection cc)
        {
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectAverage\Program.cs

            // first, lets apprach it in a similar way. lets copy count


            var xDbCommand = GetScalarCommand(source, cc, Operand: xReferencesOfLong.AverageOfLongReference.Method);

            if (xDbCommand != null)
            {
                var __value = xDbCommand.ExecuteScalar();

                return (double)__value;
            }

            return 0;
        }





    }

}
