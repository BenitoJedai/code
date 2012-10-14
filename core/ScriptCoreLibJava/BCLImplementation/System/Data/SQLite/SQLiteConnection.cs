using java.sql;
using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
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
            this.InternalConnection = DriverManager.getConnection("jdbc:google:rdbms://instance_name", "root", "");

            var s = this.InternalConnection.createStatement();

            s.executeUpdate("CREATE DATABASE IF NOT EXISTS `" + this.InternalConnectionString.DataSource + "`");
            s.executeUpdate("USE `" + this.InternalConnectionString.DataSource + "`");

            s.close();
        }

        public override void Close()
        {
            this.InternalConnection.close();
        }

        public override void Dispose()
        {
            this.Close();
        }
    }
}
