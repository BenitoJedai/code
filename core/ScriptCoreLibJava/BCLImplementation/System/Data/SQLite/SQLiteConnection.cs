using java.sql;
using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJava.BCLImplementation.System.Data.SQLite
{
    [Script(Implements = typeof(global::System.Data.SQLite.SQLiteConnection))]
    internal class __SQLiteConnection : __DbConnection
    {
        public java.sql.Connection InternalConnection;
        public __SQLiteConnectionStringBuilder InternalConnectionString;

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20121001-solutionbuilderv1/20121014-gae-data
        // X:\jsc.svn\core\ScriptCoreLib\PHP\BCLImplementation\System\Data\SQLite\SQLiteConnection.cs

        public __SQLiteConnection(string connectionstring)
        {
            // should parse instead
            InternalConnectionString = __SQLiteConnectionStringBuilder.InternalConnectionString;
        }


        public override void Open()
        {
            try
            {
                this.InternalConnection = DriverManager.getConnection("jdbc:google:rdbms://instance_name", "root", "");
            }
            catch
            {
                throw;
            }

            using (var cmd = new SQLiteCommand("CREATE DATABASE IF NOT EXISTS `" + this.InternalConnectionString.DataSource + "`", (SQLiteConnection)(object)this))
            {
                cmd.ExecuteNonQuery();
            }

            using (var cmd = new SQLiteCommand("USE `" + this.InternalConnectionString.DataSource + "`", (SQLiteConnection)(object)this))
            {
                cmd.ExecuteNonQuery();
            }

        }

        public override void Close()
        {
            try
            {
                this.InternalConnection.close();

            }
            catch
            {
                throw;
            }
        }

        public override void Dispose()
        {
            this.Close();
        }

        public __SQLiteCommand InternalLastInsertRowIdCommand;

        public long LastInsertRowId
        {
            get
            {
                long value = -1;

                try
                {
                    var r = InternalLastInsertRowIdCommand.InternalStatement.getGeneratedKeys();

                    // http://stackoverflow.com/questions/1915166/how-to-get-the-insert-id-in-jdbc
                    if (r.next())
                        value = r.getLong(1);
                }
                catch
                {
                    throw;
                }

                return value;
            }
        }
    }
}
