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
    public class __SQLiteCommand : __DbCommand
    {
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

        #region InternalCreateStatement
        [Script]
        internal class __InternalCreateStatement
        {
            public Func<Task<int>> ExecuteNonQuery;
            public Func<__SQLiteDataReader> ExecuteReader;
        }

        internal __InternalCreateStatement InternalCreateStatement()
        {
            //Console.WriteLine("InternalCreateStatement...");

            #region sql
            var sql = this.CommandText;
            var parameters = this.Parameters.InternalParameters;
            var index =
                from p in parameters
                from i in this.CommandText.GetIndecies(p.ParameterName)
                orderby i
                select new { p, i };

            foreach (var p in parameters)
            {
                // java seems to like indexed parameters instead
                sql = sql.Replace(p.ParameterName, "?");
            }
            #endregion


            //Console.WriteLine("InternalCreateStatement: " + sql);

            // http://stackoverflow.com/questions/8776861/sqlite3-rawquery-update-does-not-work
            // http://stackoverflow.com/questions/9341204/android-sqlite-rawquery-parameters
            // http://stackoverflow.com/questions/3672933/sqlite-inserting-a-string-with-newlines-into-database-from-csv-file

            var db = InternalConnection.db;

            #region ExecuteNonQuery
            Func<Task<int>> ExecuteNonQuery = delegate
            {
                Console.WriteLine("enter InternalCreateStatement.ExecuteNonQueryAsync");

                var x = new TaskCompletionSource<int>();

                // X:\jsc.svn\examples\javascript\Test\TestWebSQLDatabase\TestWebSQLDatabase\Application.cs

                var bindArgs = default(object[]);
                bindArgs = index.Select(k => k.p.Value).ToArray();

                this.InternalConnection.db.transaction(
                    callback:
                         tx =>
                         {
                             Console.WriteLine("enter InternalCreateStatement.ExecuteNonQueryAsync in transaction, before executeSql " + new { sql, bindArgs.Length });


                             tx.executeSql(
                                 sqlStatement: sql,

                                 arguments: bindArgs,

                                 callback:
                                       (SQLTransaction xtx, SQLResultSet r) =>
                                       {
                                           Console.WriteLine("enter InternalCreateStatement.ExecuteNonQueryAsync callback");

                                           this.InternalConnection.InternalLastSQLResultSet = r;

                                           x.SetResult((int)r.rowsAffected);
                                       },


                                errorCallback:
                                    (xtx, err) =>
                                    {
                                        Console.WriteLine("errorCallback: " + new { err.code, err.message });
                                    }


                             );

                         },

                    errorCallback:
                        (xerr) =>
                        {
                            Console.WriteLine("errorCallback: " + new { xerr.code, xerr.message });
                        }
                );

                return x.Task;

            };
            #endregion

            #region ExecuteReader
            Func<__SQLiteDataReader> ExecuteReader = delegate
            {
                var selectionArgs = default(string[]);

                if (parameters.Count > 0)
                {
                    // what about null args?
                    // we had to crash on android to come back and fix null support:P

                    //Caused by: java.lang.IllegalArgumentException: the bind value at index 1 is null
                    //       at android.database.sqlite.SQLiteProgram.bindString(SQLiteProgram.java:237)
                    //       at android.database.sqlite.SQLiteQuery.bindString(SQLiteQuery.java:185)
                    //       at android.database.sqlite.SQLiteDirectCursorDriver.query(SQLiteDirectCursorDriver.java:48)
                    //       at android.database.sqlite.SQLiteDatabase.rawQueryWithFactory(SQLiteDatabase.java:1356)

                    selectionArgs = index.Select(
                        k =>
                        {
                            // does android even allow null?
                            if (k.p.Value == null)
                                return "";

                            return k.p.Value.ToString();
                        }
                    ).ToArray();
                }

                //var cursor = db.rawQuery(sql, selectionArgs);
                //return new __SQLiteDataReader { cursor = cursor };
                return null;
            };
            #endregion


            return new __InternalCreateStatement
            {
                ExecuteNonQuery = ExecuteNonQuery,
                ExecuteReader = ExecuteReader
            };
        }
        #endregion

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
