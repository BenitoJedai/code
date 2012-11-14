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

            // http://dev.mysql.com/doc/refman/5.0/en/example-auto-increment.html
            // http://www.sqlite.org/autoinc.html

            sql = sql.Replace(
                "PRIMARY KEY AUTOINCREMENT",
                "PRIMARY KEY AUTO_INCREMENT"
            );

            // select * from sqlite_master

            // SELECT * FROM INFORMATION_SCHEMA.TABLES
            // TABLE_SCHEMA

            sql = sql.Replace(
                // { type: 'table', name: 'Table1', tbl_name: 'Table1', rootpage: 2, sql: 'CREATE TABLE Table1 (ContentKey INTEGER PRIMARY KEY AUTOINCREMENT, ContentValue text not null)'}
                "from sqlite_master",
                // { TABLE_CATALOG: 'def', TABLE_SCHEMA: 'sqlitewithdatagridview5', TABLE_NAME: 'table1', TABLE_TYPE: 'BASE TABLE', ENGINE: 'InnoDB', VERSION: 10, ROW_FORMAT: 'Compact', TABLE_ROWS: 5, AVG_ROW_LENGTH: 3276, DATA_LENGTH: 16384, MAX_DATA_LENGTH: 0, INDEX_LENGTH: 0, DATA_FREE: 5242880, AUTO_INCREMENT: 6, CREATE_TIME: '2012-11-14 13:48:57', UPDATE_TIME: '', CHECK_TIME: '', TABLE_COLLATION: 'latin1_swedish_ci', CHECKSUM: 0, CREATE_OPTIONS: '', TABLE_COMMENT: ''}
                "from (SELECT * FROM INFORMATION_SCHEMA.TABLES where TABLE_SCHEMA = '" + this.c.InternalDatabaseName + "') as sqlite_master"
            );


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
