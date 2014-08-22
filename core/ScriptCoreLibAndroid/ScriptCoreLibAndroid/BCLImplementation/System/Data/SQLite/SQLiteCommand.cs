using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.SQLite;
//using ScriptCoreLibJava.BCLImplementation.System.Data.SQLite;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.BCLImplementation.System.Data.SQLite
{
    //[Script(Implements = typeof(global::System.Data.SQLite.SQLiteCommand))]
    [Script(ImplementsViaAssemblyQualifiedName = "System.Data.SQLite.SQLiteCommand")]
    internal class __SQLiteCommand : __DbCommand
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Data\SQLite\SQLiteCommand.cs
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Data\SQLite\SQLiteCommand.cs

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140322

        public __SQLiteConnection InternalConnection;
        public override string CommandText { get; set; }



        public __SQLiteCommand(string sql, __SQLiteConnection c)
        {
            this.InternalConnection = (__SQLiteConnection)(object)c;
            this.CommandText = sql;
            this.Parameters = new __SQLiteParameterCollection { };
        }

        #region InternalCreateStatement
        [Script]
        internal class __InternalCreateStatement
        {
            public Func<int> ExecuteNonQuery;
            public Func<__SQLiteDataReader> ExecuteReader;
        }

        internal __InternalCreateStatement InternalCreateStatement()
        {
            // x:\jsc.svn\examples\javascript\linq\test\auto\testselect\testandroidorderbythengroupby\applicationwebservice.cs
            // error?

            Console.WriteLine("InternalCreateStatement...");

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


            Console.WriteLine("InternalCreateStatement: " + sql);

            // http://stackoverflow.com/questions/8776861/sqlite3-rawquery-update-does-not-work
            // http://stackoverflow.com/questions/9341204/android-sqlite-rawquery-parameters
            // http://stackoverflow.com/questions/3672933/sqlite-inserting-a-string-with-newlines-into-database-from-csv-file

            var db = InternalConnection.db;

            #region ExecuteNonQuery
            Func<int> ExecuteNonQuery = delegate
           {
               Console.WriteLine("enter InternalCreateStatement ExecuteNonQuery " + new { parameters.Count });
               // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Collections\Generic\List.cs

               if (parameters.Count > 0)
               {
                   var bindArgs = default(object[]);
                   // jvm toarray needs to produce object array now.
                   bindArgs = index.Select(k => k.p.Value).ToArray();
                   db.execSQL(sql, bindArgs);
                   return 0;
               }

               db.execSQL(sql);
               return 0;
           };
            #endregion

            #region ExecuteReader
            Func<__SQLiteDataReader> ExecuteReader = delegate
            {
                Console.WriteLine("enter InternalCreateStatement ExecuteReader " + new { parameters.Count });


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

                Console.WriteLine("before InternalCreateStatement ExecuteReader rawQuery");
                var cursor = db.rawQuery(sql, selectionArgs);
                return new __SQLiteDataReader { cursor = cursor };
            };
            #endregion


            return new __InternalCreateStatement
            {
                ExecuteNonQuery = ExecuteNonQuery,
                ExecuteReader = ExecuteReader
            };
        }
        #endregion



        public override object ExecuteScalar()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140822
            // 3h to figure out we did not implement this method for android!

            var r = ExecuteReader();
            var value = default(object);
            if (r.Read())
            {
                value = r[0];
            }
            r.Dispose();
            return value;
        }


        #region ExecuteNonQuery
        public override int ExecuteNonQuery()
        {
            Console.WriteLine("__SQLiteCommand.ExecuteNonQuery " + new { this.CommandText, this.InternalConnection.InternalReadOnly });

            return InternalCreateStatement().ExecuteNonQuery();
        }
        #endregion


        #region ExecuteReader
        public override global::System.Data.Common.DbDataReader __DbCommand_ExecuteReader()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140322

            // this took a few hous to figure out!
            return (global::System.Data.Common.DbDataReader)(object)this.ExecuteReader();
        }

        public new __SQLiteDataReader ExecuteReader()
        {
            // X:\jsc.svn\examples\javascript\appengine\AppEngineUserAgentLoggerWithXSLXAsset\AppEngineUserAgentLoggerWithXSLXAsset\ApplicationWebService.cs
            Console.WriteLine("__SQLiteCommand.ExecuteReader " + new { this.CommandText, this.InternalConnection.InternalReadOnly });

            return (__SQLiteDataReader)(object)InternalCreateStatement().ExecuteReader();
        }
        #endregion

        #region Parameters
        public override global::System.Data.Common.DbParameter CreateDbParameter()
        {
            return (global::System.Data.Common.DbParameter)(object)new __SQLiteParameter();
        }

        public __SQLiteParameterCollection Parameters { get; set; }

        public override global::System.Data.Common.DbParameterCollection DbParameterCollection
        {
            get { return Parameters; }
        }
        #endregion

    }

}
