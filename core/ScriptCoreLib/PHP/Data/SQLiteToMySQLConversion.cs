using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

// ScriptCoreLib.Shared
namespace ScriptCoreLib.PHP.Data
{
    [Description("PHP and Google App Engine use MySQL. When SQLite API is used SQL rewrite is required.")]
    [Script]
    [Obsolete("Move namespace!")]
    public static class SQLiteToMySQLConversion
    {
        public static string Convert(string sql, string InternalDatabaseName = null)
        {
            // 592ms enter SQLiteToMySQLConversion{ sql =  }

            //Console.WriteLine("enter SQLiteToMySQLConversion" + new { sql });

            // X:\jsc.svn\examples\javascript\appengine\AppEngineUserAgentLoggerWithXSLXAsset\AppEngineUserAgentLoggerWithXSLXAsset\ApplicationWebService.cs


            // http://dev.mysql.com/doc/refman/5.0/en/example-auto-increment.html
            // http://www.sqlite.org/autoinc.html

            sql = sql.Replace(
                "PRIMARY KEY AUTOINCREMENT",
                "PRIMARY KEY AUTO_INCREMENT"
            );

            // int to string
            // X:\jsc.svn\examples\javascript\appengine\WebNotificationsViaDataAdapter\WebNotificationsViaDataAdapter\Schema\FooTable\select all.sql
            // http://stackoverflow.com/questions/3568779/string-concatenation-does-not-work-in-sqlite
            sql = sql.Replace(
              "('' ||",
              "concat('',"
            );

            // select * from sqlite_master

            // SELECT * FROM INFORMATION_SCHEMA.TABLES
            // TABLE_SCHEMA

            if (InternalDatabaseName != null)
            {
                sql = sql.Replace(
                    // { type: 'table', name: 'Table1', tbl_name: 'Table1', rootpage: 2, sql: 'CREATE TABLE Table1 (ContentKey INTEGER PRIMARY KEY AUTOINCREMENT, ContentValue text not null)'}
                    "from sqlite_master",
                    // { TABLE_CATALOG: 'def', TABLE_SCHEMA: 'sqlitewithdatagridview5', TABLE_NAME: 'table1', TABLE_TYPE: 'BASE TABLE', ENGINE: 'InnoDB', VERSION: 10, ROW_FORMAT: 'Compact', TABLE_ROWS: 5, AVG_ROW_LENGTH: 3276, DATA_LENGTH: 16384, MAX_DATA_LENGTH: 0, INDEX_LENGTH: 0, DATA_FREE: 5242880, AUTO_INCREMENT: 6, CREATE_TIME: '2012-11-14 13:48:57', UPDATE_TIME: '', CHECK_TIME: '', TABLE_COLLATION: 'latin1_swedish_ci', CHECKSUM: 0, CREATE_OPTIONS: '', TABLE_COMMENT: ''}
                    "from (SELECT * FROM INFORMATION_SCHEMA.TABLES where TABLE_SCHEMA = '" + InternalDatabaseName + "') as sqlite_master"
                );
            }

            //Console.WriteLine("exit SQLiteToMySQLConversion" + new { sql });
            return sql;
        }
    }
}
