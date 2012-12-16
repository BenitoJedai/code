//using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using ScriptCoreLibJava.BCLImplementation.System.Data.SQLite;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.BCLImplementation.System.Data.SQLite
{
    [Script(Implements = typeof(global::System.Data.SQLite.SQLiteCommand))]
    internal class __SQLiteCommand : __DbCommand
    {
        // see also: X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Data\SQLite\SQLiteCommand.cs

        public __SQLiteConnection c;
        public string sql;

        public __SQLiteCommand(string sql, SQLiteConnection c)
        {
            this.c = (__SQLiteConnection)(object)c;
            this.sql = sql;

            this.InternalParameters = new __SQLiteParameterCollection { };
            this.Parameters = (SQLiteParameterCollection)(object)this.InternalParameters;
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

            var sql = this.sql;
            var parameters = this.InternalParameters.InternalParameters;
            var index =
                from p in parameters
                from i in this.sql.GetIndecies(p.ParameterName)
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

            var db = c.db;

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
                    selectionArgs = index.Select(k => k.p.Value.ToString()).ToArray();
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
            return InternalCreateStatement().ExecuteNonQuery();
        }

        public __SQLiteDataReader ExecuteReader()
        {
            return InternalCreateStatement().ExecuteReader();
        }

        public __SQLiteParameterCollection InternalParameters;
        public SQLiteParameterCollection Parameters { get; set; }
    }

}
