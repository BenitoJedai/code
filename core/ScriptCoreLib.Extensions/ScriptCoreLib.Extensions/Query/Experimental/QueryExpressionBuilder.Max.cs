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



        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxSelectMax\Program.cs
        public static long Max(this IQueryStrategy<long> source)
        {

            // first, lets apprach it in a similar way. lets copy count


            var xDbCommand = GetScalarCommand(source, cc: null, Operand: xReferencesOfLong.MaxOfLongReference.Method);

            if (xDbCommand != null)
            {
                return (long)xDbCommand.ExecuteScalar();
            }

            return 0;
        }






    }

}
