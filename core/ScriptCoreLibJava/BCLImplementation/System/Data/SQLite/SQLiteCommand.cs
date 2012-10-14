using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJava.BCLImplementation.System.Data.SQLite
{
    [Script(Implements = typeof(global::System.Data.SQLite.SQLiteCommand))]
    internal class __SQLiteCommand : __DbCommand
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20121001-solutionbuilderv1/20121014-gae-data
        // X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\BCLImplementation\System\Data\SQLite\SQLiteCommand.cs
        // X:\jsc.svn\core\ScriptCoreLib\PHP\BCLImplementation\System\Data\SQLite\SQLiteCommand.cs

        __SQLiteConnection c;
        string sql;

        java.sql.Statement InternalStatement;

        public __SQLiteCommand(string sql, SQLiteConnection c)
        {
            this.c = (__SQLiteConnection)(object)c;

            // http://dev.mysql.com/doc/refman/5.0/en/example-auto-increment.html
            // http://www.sqlite.org/autoinc.html

            sql = sql.Replace(
                "PRIMARY KEY AUTOINCREMENT",
                "PRIMARY KEY AUTO_INCREMENT"
            );

            this.sql = sql;

            try
            {
                this.InternalStatement = this.c.InternalConnection.createStatement();
            }
            catch
            {
                throw;
            }

        }

        public override int ExecuteNonQuery()
        {
            var value = default(int);

            try
            {
                value = this.InternalStatement.executeUpdate(this.sql);
            }
            catch
            {
                throw;
            }

            return value;
        }

        public SQLiteDataReader ExecuteReader()
        {
            var value = default(SQLiteDataReader);

            try
            {
                var r = this.InternalStatement.executeQuery(this.sql);

                value = (SQLiteDataReader)(object)new __SQLiteDataReader { InternalResultSet = r };
            }
            catch
            {
                throw;
            }

            return value;
        }
    }
}
