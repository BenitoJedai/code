using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using ScriptCoreLibJava.BCLImplementation.System.Data.SQLite;
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

        __SQLiteConnection c;
        string sql;

        public __SQLiteCommand(string sql, SQLiteConnection c)
        {
            this.c = (__SQLiteConnection)(object)c;
            this.sql = sql;

            this.InternalParameters = new __SQLiteParameterCollection { };
            this.Parameters = (SQLiteParameterCollection)(object)this.InternalParameters;
        }

        public override int ExecuteNonQuery()
        {
            Console.WriteLine("__SQLiteCommand: " + sql);


            c.db.execSQL(sql);
            return 0;
        }

        public __SQLiteDataReader ExecuteReader()
        {
            return new __SQLiteDataReader { cursor = c.db.rawQuery(sql, null) };
        }

        public __SQLiteParameterCollection InternalParameters;
        public SQLiteParameterCollection Parameters { get; set; }
    }

}
