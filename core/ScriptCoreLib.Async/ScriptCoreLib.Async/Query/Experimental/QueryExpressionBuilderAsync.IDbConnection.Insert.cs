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

namespace ScriptCoreLib.Query.Experimental
{
    public static partial class QueryExpressionBuilderAsync
    {
        // X:\jsc.svn\examples\javascript\Test\TestSQLiteConnection\TestSQLiteConnection\Application.cs
        // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\Experimental\QueryExpressionBuilder.IDbConnection.Insert.cs

        public static Task InsertAsync<TElement>(this IQueryStrategy<TElement> source, IDbConnection cc, TElement value)
        {
            // in CLR and in browser this would work.

            var c = QueryExpressionBuilder.GetInsertCommand(source, cc, value) as System.Data.SQLite.SQLiteCommand;
            var n = c.ExecuteNonQueryAsync();

            return n;
        }


    }

}
