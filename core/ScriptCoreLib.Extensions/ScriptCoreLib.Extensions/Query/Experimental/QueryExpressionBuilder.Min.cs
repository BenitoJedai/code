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
        public static long Min<TSource>(this IQueryStrategy<TSource> source, Expression<Func<TSource, long>> selector)
        {
            // X:\jsc.svn\examples\javascript\forms\test\TestMinSelector\TestMinSelector\ApplicationControl.cs

            return source.Select(selector).Min();
        }

        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxSelectMin\Program.cs
        public static long Min(this IQueryStrategy<long> source)
        {
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMin\Program.cs
            // first, lets apprach it in a similar way. lets copy count

            var __value = default(long);
            WithConnection(
                cc =>
                {

                    var xDbCommand = GetScalarCommand(source, cc: cc, Operand: xReferencesOfLong.MinOfLongReference.Method);

                    if (xDbCommand != null)
                    {
                        __value = (long)xDbCommand.ExecuteScalar();
                    }

                }
            );
            return __value;
        }







    }

}
