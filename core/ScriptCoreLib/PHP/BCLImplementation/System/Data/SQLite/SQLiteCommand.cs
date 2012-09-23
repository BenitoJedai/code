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
            //Native.API.error_log("ExecuteNonQuery " + sql);
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
            // http://php.net/manual/en/mysqli.query.php
            queryResult = MySQL.API.mysql_query(sql);


            if ((queryResult == null))
            {
                Native.echo(MySQL.API.mysql_error());

                return new __SQLiteDataReader
                {
                    queryResult = null
                };
            }

            if (((bool)queryResult == false))
            {
                Native.echo(MySQL.API.mysql_error());

                return new __SQLiteDataReader
                {
                    queryResult = queryResult
                };
            }

            return new __SQLiteDataReader
            {
                queryResult = queryResult
            };
        }
    }

}
