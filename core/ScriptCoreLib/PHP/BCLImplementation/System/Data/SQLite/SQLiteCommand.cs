using ScriptCoreLib.PHP.BCLImplementation.System.Data.Common;
using ScriptCoreLib.PHP.Runtime;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Data.SQLite
{
    [Script(Implements = typeof(global::System.Data.SQLite.SQLiteCommand))]
    internal class __SQLiteCommand : __DbCommand
    {
        __SQLiteConnection c;
        string sql;
        object queryResult;

        bool debug = false;

        public __SQLiteCommand(string sql, SQLiteConnection c)
        {
            this.c = (__SQLiteConnection)(object)c;
            this.sql = sql;
        }

        public override int ExecuteNonQuery()
        {
            // c.db.execSQL(sql);

            object o = MySQL.API.mysql_query(sql);  //no NonQuery Native Avail for PHP

            if (debug)
            {
                string s = "<empty>";
                if (o != null)
                    s = (string)o;

                Console.WriteLine("ExecuteNonQuery result:" + s);
            }

            return 0;
        }

        public __SQLiteDataReader ExecuteReader()
        {
            queryResult = MySQL.API.mysql_query(sql);

            if ((queryResult == null))
                return null;

            if (((bool)queryResult == false))
                return null;

            return new __SQLiteDataReader
            {
                queryResult = queryResult
                //c.db.rawQuery(sql, null) };
            };
        }
    }

}
