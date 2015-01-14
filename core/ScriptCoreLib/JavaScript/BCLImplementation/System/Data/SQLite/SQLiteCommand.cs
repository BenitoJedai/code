using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.SQLite;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Data.SQLite
{
    [Script(ImplementsViaAssemblyQualifiedName = "System.Data.SQLite.SQLiteCommand")]
    public partial class __SQLiteCommand : __DbCommand
    {
        // https://code.google.com/p/linq-cube/

        // X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\BCLImplementation\System\Data\SQLite\SQLiteCommand.cs
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Data\SQLite\SQLiteCommand.cs

        // http://caniuse.com/#search=database

        public __SQLiteConnection InternalConnection;
        public override string CommandText { get; set; }


        public __SQLiteCommand(string sql, __SQLiteConnection c)
        {
            this.InternalConnection = c;
            this.CommandText = sql;
            this.Parameters = new __SQLiteParameterCollection { };

        }

        #region __DbCommand_ExecuteReader
        public override DbDataReader __DbCommand_ExecuteReader()
        {
            throw new NotSupportedException("await ExecuteReaderAsync");
        }

        public override Task<DbDataReader> __DbCommand_ExecuteReaderAsync()
        {
            Console.WriteLine("enter __DbCommand_ExecuteReaderAsync " + new { CommandText });

            var x = new TaskCompletionSource<DbDataReader>();

            // X:\jsc.svn\examples\javascript\Test\TestWebSQLDatabase\TestWebSQLDatabase\Application.cs

            this.InternalConnection.db.transaction(
                callback:
                     tx =>
                     {
                         tx.executeSql(
                             sqlStatement: this.CommandText,

                             callback:
                                   (SQLTransaction xtx, SQLResultSet r) =>
                                   {
                                       this.InternalConnection.InternalLastSQLResultSet = null;

                                       __DbDataReader xx = new __SQLiteDataReader { r = r };

                                       x.SetResult(xx);
                                   },

                            errorCallback:
                                 (SQLTransaction transaction, SQLError error) =>
                                 {
                                     Console.WriteLine("executeSql error __DbCommand_ExecuteReaderAsync " + new { error.message, error.code });

                                 }

                         );

                     },


                     errorCallback:
                         (SQLError error) =>
                         {
                             Console.WriteLine("transaction error __DbCommand_ExecuteReaderAsync " + new { error.message, error.code });

                         }

            );

            return x.Task;
        }
        #endregion


        public override Task<object> ExecuteScalarAsync()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201412/20141207/async-query

            Console.WriteLine("enter ExecuteScalarAsync before ExecuteReaderAsync");

            // X:\jsc.svn\examples\javascript\LINQ\LINQWebCamAvatars\LINQWebCamAvatars\Application.cs
            // X:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\Query\Experimental\QueryExpressionBuilderAsync.IDbConnection.Count.cs
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestWebInsert\Application.cs
            // X:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\Query\Experimental\QueryExpressionBuilderAsync.SumAsync.cs

            var z = new TaskCompletionSource<object>();

            this.ExecuteReaderAsync().ContinueWith(
                zz =>
                {
                    Console.WriteLine("enter ExecuteScalarAsync ContinueWith");

                    zz.Result.Read();

                    var __value = zz.Result[0];

                    Console.WriteLine("enter ExecuteScalarAsync SetResult " + new { __value });

                    z.SetResult(__value);
                }
            );

            return z.Task;
        }

        #region ExecuteNonQuery
        // http://msdn.microsoft.com/en-us/library/hh223678(v=vs.110).aspx
        // http://msdn.microsoft.com/en-us/library/system.data.common.dbcommand.executenonquery(v=vs.110).aspx
        public override global::System.Threading.Tasks.Task<int> ExecuteNonQueryAsync()
        {
            Console.WriteLine("enter ExecuteNonQueryAsync");

            // how the heck do we support the damn arguments?
            // http://stackoverflow.com/questions/15644654/can-executesql-take-named-parameters

            return InternalCreateStatement().ExecuteNonQuery();
        }

        public override int ExecuteNonQuery()
        {
            ExecuteNonQueryAsync();

            return 0;
        }
        #endregion


        #region Parameters
        public override global::System.Data.Common.DbParameter CreateDbParameter()
        {
            return (global::System.Data.Common.DbParameter)(object)new __SQLiteParameter();
        }

        public new __SQLiteParameterCollection Parameters { get; set; }

        public override DbParameterCollection DbParameterCollection
        {
            get { return this.Parameters; }
        }
        #endregion

    }
}
