using ScriptCoreLib.Android.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.BCLImplementation.System.Data.SQLite
{
    [Script(Implements = typeof(global::System.Data.SQLite.SQLiteCommand))]
    public class __SQLiteCommand : __DbCommand
    {
        __SQLiteConnection c;
        string sql;

        public __SQLiteCommand(string sql, SQLiteConnection c)
        {
            this.c = (__SQLiteConnection)(object)c;
            this.sql = sql;
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
    }

}
