using ScriptCoreLib.PHP.Data;
using ScriptCoreLib.PHP.Runtime;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
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


            this.sql = SQLiteToMySQLConversion.Convert(sql, this.c.InternalDatabaseName);
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

            // http://php.net/manual/en/function.mysql-query.php
            // For other type of SQL statements, INSERT, UPDATE, DELETE, DROP, etc, mysql_query() returns TRUE on success or FALSE on error.
            queryResult = MySQL.API.mysql_query(sql);

            var errno = MySQL.API.mysql_errno();

            //Native.echo("<!-- " + errno + " -->");

            if (errno != 0)
            {
                throw new Exception("mysql_query failed, mysql_errno: " + errno + " " + MySQL.API.mysql_error());
            }

            var queryResult_bool = (bool)queryResult;
            var __true = true;

            if (queryResult_bool == __true)
            {

                return new __SQLiteDataReader
                {
                    queryResult = null
                };
            }

            return new __SQLiteDataReader
            {
                queryResult = queryResult
            };
        }
    }

}
