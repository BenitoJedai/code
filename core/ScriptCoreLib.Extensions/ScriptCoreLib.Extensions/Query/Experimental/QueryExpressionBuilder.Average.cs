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


        [Obsolete("this might need to return the row for the selector")]
        public static long Average<TSource>(this IQueryStrategy<TSource> source, Expression<Func<TSource, long>> selector)
        {
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectAverage\Program.cs

            return source.Select(selector).Average();
        }





        // TElement : long
        public static long Average(this IQueryStrategy<long> source)
        {
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectAverage\Program.cs

            // first, lets apprach it in a similar way. lets copy count


            var xDbCommand = GetScalarCommand(source, cc: null, Operand: xReferencesOfLong.AverageOfLongReference.Method);

            if (xDbCommand != null)
            {
                return (long)xDbCommand.ExecuteScalar();
            }

            return 0;
        }





    }

}
