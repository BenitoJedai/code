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

        partial class SQLWriter<TElement>
        {
        }

        class xDelete : IQueryStrategy
        {
            public IQueryStrategy source;

            public override string ToString()
            {
                return "delete";
            }
        }

        class xDelete<TElement> : xDelete, IQueryStrategy<TElement>
        {

        }

        public static void Delete<TElement>(this IQueryStrategy<TElement> source, IDbConnection cc)
        {
            // how was it done before?
            // tested by?

            var nsource = new xDelete { source = source };


            var c = (DbCommand)cc.CreateCommand();


            var w = new SQLWriter<TElement>(nsource, new IQueryStrategy[] { nsource }, Command: c);



            c.ExecuteNonQuery();
        }

        // http://stackoverflow.com/questions/4471277/mysql-delete-from-with-subquery-as-condition
        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMath\Program.cs
        // X:\jsc.svn\examples\javascript\LINQ\GGearAlpha\GGearAlpha\Library\GoogleGearsAdvanced.cs

    }

}
