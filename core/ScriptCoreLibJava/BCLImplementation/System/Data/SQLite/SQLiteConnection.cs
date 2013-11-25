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
                var x = new
                {
                    this.InternalConnectionString.InternalInstanceName,
                    this.InternalConnectionString.InternalUser,
                    this.InternalConnectionString.Password,
                };

                //                { InternalInstanceName = instance_name, InternalUser = user1, Password =  }
                //Dec 24, 2012 9:25:59 AM com.google.appengine.api.rdbms.dev.LocalRdbmsServiceLocalDriver openConnection
                //SEVERE: Could not allocate a connection
                //java.sql.SQLException: Access denied for user 'user1'@'localhost' (using password: NO)
                //        at com.mysql.jdbc.SQLError.createSQLException(SQLError.java:1074)
                //        at com.mysql.jdbc.MysqlIO.checkErrorPacket(MysqlIO.java:4096)

                //Console.WriteLine(x.ToString());

                // Caused by: java.sql.SQLException: No suitable driver found for jdbc:google:rdbms://instance_name

                // https://groups.google.com/forum/?fromgroups=#!topic/google-appengine-java/Vm5PTq4_0lg
                // Instance == Your Google API Project ID:InstanceName
                this.InternalConnection = DriverManager.getConnection(
                    "jdbc:google:rdbms://" + x.InternalInstanceName,
                    x.InternalUser,
                    x.Password
                );
            }
            catch (Exception ex)
            {
                //        Caused by: java.lang.RuntimeException: java.lang.ClassNotFoundException: com.mysql.jdbc.Driver
                //at ScriptCoreLibJava.BCLImplementation.System.Data.SQLite.__SQLiteConnection.Open(__SQLiteConnection.java:39)
                //at ScriptCoreLib.Shared.Data.Diagnostics.WithConnectionLambda___c__DisplayClass2._WithConnection_b__1(WithConnectionLambda___c__DisplayClass2.java:36)

                if (ex.Message.Contains("com.mysql.jdbc.Driver"))
                {
                    // "C:\util\appengine-java-sdk-1.8.8\lib\impl\mysql-connector-java-5.1.22-bin.jar"

                    Console.WriteLine("did you set up the mysql jar?");
                }

                //                C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe  -encoding UTF-8 -cp Y:\AppEngineUserAgentLoggerWithXSLXAsset.ApplicationWebService\staging.java\web\release;C:\util\appengine-java-sdk-1.8.8\lib\impl\*;C:\util\appengine-java-sdk-1.8.8\lib\shared\* -d "Y:\AppEngineUserAgentLoggerWithXSLXAsset.ApplicationWebService\staging.java\web\release" @"Y:\AppEngineUserAgentLoggerWithXSLXAsset.ApplicationWebService\staging.java\web\files"

                //Y:\AppEngineUserAgentLoggerWithXSLXAsset.ApplicationWebService\staging.java\web\java\ScriptCoreLibJava\BCLImplementation\System\Data\SQLite\__SQLiteConnection.java:47: error: unreported exception SQLException; must be caught or declared to be thrown
                //            throw exception1;
                //            ^
                //Note: Y:\AppEngineUserAgentLoggerWithXSLXAsset.ApplicationWebService\staging.java\web\java\ScriptCoreLibJava\BCLImplementation\System\Threading\__Thread.java uses or overrides a deprecated API.

                throw new InvalidOperationException("__SQLiteConnection " + new { ex.Message, ex.StackTrace });
            }

            using (var cmd = new SQLiteCommand(
                "CREATE DATABASE IF NOT EXISTS `" + this.InternalConnectionString.DataSource + "`", (SQLiteConnection)(object)this))
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
                if (this.InternalConnection != null)
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
