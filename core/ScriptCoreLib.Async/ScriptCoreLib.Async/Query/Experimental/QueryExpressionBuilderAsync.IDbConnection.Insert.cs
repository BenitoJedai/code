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

        [Obsolete("we need to extend xsqlite and xmysql to have the async methods defined as interfaces")]
        public static Task InsertAsync<TElement>(this IQueryStrategy<TElement> source, IDbConnection cc, TElement value)
        {
            // in CLR and in browser this would work.

            var c =  QueryExpressionBuilder.GetInsertCommand(source, cc, value);
            // why ExecuteNonQueryAsync is not part of CLR, now we need to link in SQLite and PHP!
            
            var xSQLiteCommand = c as System.Data.SQLite.SQLiteCommand;
            if (xSQLiteCommand != null)
            {
                var n = xSQLiteCommand.ExecuteNonQueryAsync();
                return n;
            }


            // how would this work in the browser if scriptcorelib does not yet provide the implementation?
            var xMySQLCommand = c as System.Data.MySQL.MySQLCommand;
            if (xMySQLCommand != null)
            {
                var n = xMySQLCommand.ExecuteNonQueryAsync();
                return n;
            }

            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestXMySQL\Program.cs
            // should we report back the new key?

            throw new NotSupportedException();
        }


    }

}
