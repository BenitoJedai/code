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
        // X:\jsc.svn\examples\javascript\LINQ\MashableVelocityGraph\MashableVelocityGraph\ApplicationWebService.cs
        
        // first lets allow scalar subqueries
        public static long Count<TElement>(this IQueryStrategy<TElement> source)
        {
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestXMySQL\Program.cs

            var value = default(long);

            // what if there is no connection?
            WithConnection(
                cc =>
                {
                    value = Count(source, cc);
                }
            );

            return value;
        }

        // chrome needs CountAsync
        public static long Count<TElement>(this IQueryStrategy<TElement> source, IDbConnection cc)
        {
            Console.WriteLine("enter Count " + new { cc });

            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestDeleteAll\Program.cs
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/20140705/20140705


            // how was it done before?
            // tested by?

            var xDbCommand = GetScalarCommand(source, cc, Operand: xReferencesOfLong.CountReference.Method);

            // Additional information: Every derived table must have its own alias
            // what?
            var x = default(long);


            if (xDbCommand != null)
            {
                Console.WriteLine("before Count ExecuteScalar " + new { xDbCommand });

                //                I / System.Console(6607): Caused by: java.lang.ClassCastException: java.lang.String
                //I / System.Console(6607):        at ScriptCoreLib.Query.Experimental.QueryExpressionBuilder.Count(QueryExpressionBuilder.java:233)
                //I / System.Console(6607):        at ScriptCoreLib.Query.Experimental.QueryExpressionBuilder___c__DisplayClass6_1._Count_b__8(QueryExpressionBuilder___c__DisplayClass6_1.java:26)


                var __value = xDbCommand.ExecuteScalar();

                // android 2.3 ?
                var xString = __value as string;
                if (xString != null)
                {
                    x = Convert.ToInt64(xString);
                }
                else
                {
                    x = (long)__value;
                }

                Console.WriteLine("after Count ExecuteScalar");
            }

            return x;
        }

        // http://stackoverflow.com/questions/4471277/mysql-delete-from-with-subquery-as-condition
        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMath\Program.cs
        // X:\jsc.svn\examples\javascript\LINQ\GGearAlpha\GGearAlpha\Library\GoogleGearsAdvanced.cs
    }

}
