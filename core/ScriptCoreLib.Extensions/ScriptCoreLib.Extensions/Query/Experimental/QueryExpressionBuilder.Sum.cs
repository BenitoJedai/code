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
        public static long Sum(this IQueryStrategy<long> source)
        {
            var value = default(long);

            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSQLiteCLRSumLong\Program.cs

            // first, lets apprach it in a similar way. lets copy count


          
            // tested by ?
            WithConnection(
                    cc =>
                 {
  var xDbCommand = GetScalarCommand(
                source, 
                cc: cc, 
                Operand: xReferencesOfLong.SumOfLongReference.Method
            );


                var __value = xDbCommand.ExecuteScalar();
                     value = (long)__value;
                 }
                );

            return value;
        }







    }

}
