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
    public partial class __SQLiteCommand : __DbCommand
    {
       

        #region InternalCreateStatement
        [Script]
        internal class __InternalCreateStatement
        {
            public Func<Task<int>> ExecuteNonQuery;
            public Func<__SQLiteDataReader> ExecuteReader;
        }

        internal __InternalCreateStatement InternalCreateStatement()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150111
            // X:\jsc.svn\examples\javascript\Test\Test453CallWithMultipleDelegates\Test453CallWithMultipleDelegates\Program.cs



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
                             // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150111
                             // tested by ?


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

    }
}
