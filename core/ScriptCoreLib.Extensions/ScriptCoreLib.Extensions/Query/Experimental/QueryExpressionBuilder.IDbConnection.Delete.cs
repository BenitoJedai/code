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
    public static partial class QueryExpressionBuilder
    {
        // X:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\Query\Experimental\QueryExpressionBuilderAsync.IDbConnection.Insert.cs


        public static void Delete<TElement>(this IQueryStrategy<TElement> source, IDbConnection cc)
        {
            // tested by?

            Console.WriteLine("enter Delete ");

            // X:\jsc.svn\examples\javascript\LINQ\GGearAlpha\GGearAlpha\Library\GoogleGearsAdvanced.cs

            //var c = GetInsertCommand(source, cc, value);
            //var n = c.ExecuteNonQuery();

            var c = (DbCommand)cc.CreateCommand();

            c.CommandText = "delete from (";

            var w = new SQLWriter<TElement>(source, new IQueryStrategy[0].AsEnumerable(), Command: c);

            c.CommandText += ")";

            Console.WriteLine("Delete " + new { c.CommandText });

            c.ExecuteNonQuery();
        }


    }

}
