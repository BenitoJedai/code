using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using ScriptCoreLibJava.BCLImplementation.System.Data.SQLite;
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
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140322
        // see also: X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Data\SQLite\SQLiteCommand.cs

        public __SQLiteConnection InternalConnection;
        public override string CommandText { get; set; }



        public override global::System.Data.Common.DbParameter CreateDbParameter()
        {
            return (global::System.Data.Common.DbParameter)(object)new __SQLiteParameter();
        }


        public __SQLiteCommand(string sql, __SQLiteConnection c)
        {
            this.InternalConnection = (__SQLiteConnection)(object)c;
            this.CommandText = sql;

            this.Parameters = new __SQLiteParameterCollection { };
        }

        [Script]
        internal class __InternalCreateStatement
        {
            public Func<int> ExecuteNonQuery;
            public Func<__SQLiteDataReader> ExecuteReader;
        }

        internal __InternalCreateStatement InternalCreateStatement()
        {
            //Console.WriteLine("InternalCreateStatement...");

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


            //Console.WriteLine("InternalCreateStatement: " + sql);

            // http://stackoverflow.com/questions/8776861/sqlite3-rawquery-update-does-not-work
            // http://stackoverflow.com/questions/9341204/android-sqlite-rawquery-parameters
            // http://stackoverflow.com/questions/3672933/sqlite-inserting-a-string-with-newlines-into-database-from-csv-file

            var db = InternalConnection.db;

            Func<int> ExecuteNonQuery = delegate
           {
               if (parameters.Count > 0)
               {
                   var bindArgs = default(object[]);
                   bindArgs = index.Select(k => k.p.Value).ToArray();
                   db.execSQL(sql, bindArgs);
                   return 0;
               }

               db.execSQL(sql);
               return 0;
           };

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

                var cursor = db.rawQuery(sql, selectionArgs);
                return new __SQLiteDataReader { cursor = cursor };
            };

            return new __InternalCreateStatement
            {
                ExecuteNonQuery = ExecuteNonQuery,
                ExecuteReader = ExecuteReader
            };
        }

        public override int ExecuteNonQuery()
        {
            Console.WriteLine("__SQLiteCommand.ExecuteNonQuery " + new { this.CommandText, this.InternalConnection.InternalReadOnly });

            return InternalCreateStatement().ExecuteNonQuery();
        }

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

        public __SQLiteParameterCollection Parameters { get; set; }

        public override global::System.Data.Common.DbParameterCollection DbParameterCollection
        {
            get { return Parameters; }
        }
    }

}
