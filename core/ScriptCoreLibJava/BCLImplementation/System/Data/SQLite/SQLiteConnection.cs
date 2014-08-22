using java.sql;
using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
//using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJava.BCLImplementation.System.Data.SQLite
{
    [Script(ImplementsViaAssemblyQualifiedName = "System.Data.SQLite.SQLiteConnection")]
    internal class __XSQLiteConnection : __DbConnection
    {
        public int LastInsertRowId
        {
            get
            {
                // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Extensions\IDbConnectionExtensions.cs
                // does our Insert return the key in the correct type yet?

                return 0;
            }
        }

        public override void Close()
        {
            throw new NotImplementedException();
        }

        public override string ConnectionString
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void Open()
        {
            throw new NotImplementedException();
        }
    }

    //[Script(Implements = typeof(global::System.Data.SQLite.SQLiteConnection))]
    [Script(ImplementsViaAssemblyQualifiedName = "System.Data.MySQL.MySQLConnection")]
    internal class __SQLiteConnection : __DbConnection
    {
        //Y:\TestAppEngineOrderByThenGroupBy.ApplicationWebService\staging.java\web\java\TestAppEngineOrderByThenGroupBy\ApplicationWebService.java:81: error: cannot find symbol
        //        connection0.Dispose_060000a2();
        //                   ^
        //  symbol:   method Dispose_060000a2()
        //  location: variable connection0 of type __SQLiteConnection

        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Data\SQLite\SQLiteConnection.cs

        public int BusyTimeout { get; set; }

        public java.sql.Connection InternalConnection;
        public __MySQLConnectionStringBuilder InternalConnectionString;

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20121001-solutionbuilderv1/20121014-gae-data
        // X:\jsc.svn\core\ScriptCoreLib\PHP\BCLImplementation\System\Data\SQLite\SQLiteConnection.cs

        public override string ConnectionString { get; set; }

        public override global::System.Data.Common.DbCommand CreateDbCommand()
        {
            return (global::System.Data.Common.DbCommand)(object)new __SQLiteCommand("", this);
        }

        public __SQLiteConnection(string connectionstring)
        {
            //Console.WriteLine("__SQLiteConnection ctor " + new { connectionstring });


            // should parse instead
            this.InternalConnectionString = __MySQLConnectionStringBuilder.InternalGetConnectionString(connectionstring);

            if (this.InternalConnectionString == null)
                throw new InvalidOperationException("make sure to use new __SQLiteConnection(SQLiteConnectionStringBuilder)");

            ConnectionString = connectionstring;
        }


        public override void Open()
        {
            // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectGroupByAndConstant\TestSelectGroupByAndConstant\ApplicationWebService.cs
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140322

            // 39ms __SQLiteConnection ctor { connectionstring = Data Source=file:StressData.s3db }

            //Console.WriteLine("__SQLiteConnection.Open");

            //java.lang.RuntimeException: __SQLiteConnection { Message = , StackTrace = java.lang.NullPointerException
            //   at ScriptCoreLibJava.BCLImplementation.System.Data.SQLite.__SQLiteConnection.Open(__SQLiteConnection.java:55)
            //   at SQLiteWithDataGridView.Schema.XX._xAsWithConnection_b__0(XX.java:54)
            //   at sun.reflect.NativeMethodAccessorImpl.invoke0(Native Method)
            //   at sun.reflect.NativeMethodAccessorImpl.invoke(Unknown Source)

            try
            {
                var x = new
                {
                    this.InternalConnectionString.Server,
                    this.InternalConnectionString.UserID,
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

                //java.lang.RuntimeException: __SQLiteConnection { Message = Access denied for user 'user3'@'localhost' (using password: NO), StackTrace = java.sql.SQLException: Access denied for user 'user3'@'localhost' (using password: NO)
                //   at com.google.cloud.sql.jdbc.internal.Exceptions.newSqlException(Exceptions.java:219)
                //   at com.google.cloud.sql.jdbc.internal.SqlProtoClient.check(SqlProtoClient.java:158)
                //   at com.google.cloud.sql.jdbc.internal.SqlProtoClient.openConnection(SqlProtoClient.java:60)
                //   at com.google.cloud.sql.jdbc.Driver.connect(Driver.java:66)
                //   at com.google.cloud.sql.jdbc.Driver.connect(Driver.java:26)
                //   at java.sql.DriverManager.getConnection(Unknown Source)
                //   at java.sql.DriverManager.getConnection(Unknown Source)
                //   at ScriptCoreLibJava.BCLImplementation.System.Data.SQLite.__SQLiteConnection.Open(__SQLiteConnection.java:66)


                // https://groups.google.com/forum/?fromgroups=#!topic/google-appengine-java/Vm5PTq4_0lg
                // Instance == Your Google API Project ID:InstanceName
                this.InternalConnection = DriverManager.getConnection(
                    "jdbc:google:rdbms://" + x.Server,
                    x.UserID,
                    x.Password
                );
            }
            catch (Exception ex)
            {
                //        Caused by: java.lang.RuntimeException: java.lang.ClassNotFoundException: com.mysql.jdbc.Driver
                //at ScriptCoreLibJava.BCLImplementation.System.Data.SQLite.__SQLiteConnection.Open(__SQLiteConnection.java:39)
                //at ScriptCoreLib.Shared.Data.Diagnostics.WithConnectionLambda___c__DisplayClass2._WithConnection_b__1(WithConnectionLambda___c__DisplayClass2.java:36)

                if (ex.Message != null)
                    if (ex.Message.Contains("com.mysql.jdbc.Driver"))
                    {
                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestAppEngineOrderByThenGroupBy\ApplicationWebService.cs

                        // "C:\util\appengine-java-sdk-1.9.2\lib\impl\mysql-connector-java-5.1.30-bin.jar"
                        // "C:\util\appengine-java-sdk-1.8.8\lib\impl\mysql-connector-java-5.1.22-bin.jar"
                        // "C:\util\appengine-java-sdk-1.9.9\lib\impl\mysql-connector-java-5.1.30-bin.jar"

                        // http://dev.mysql.com/downloads/connector/j/

                        Console.WriteLine(
                            @"!!! did you set up the mysql jar? check C:\util\appengine-java-sdk-1.9.9\lib\impl\mysql-connector-java-5.1.30-bin.jar"
                            );
                    }

                //                C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe  -encoding UTF-8 -cp Y:\AppEngineUserAgentLoggerWithXSLXAsset.ApplicationWebService\staging.java\web\release;C:\util\appengine-java-sdk-1.8.8\lib\impl\*;C:\util\appengine-java-sdk-1.8.8\lib\shared\* -d "Y:\AppEngineUserAgentLoggerWithXSLXAsset.ApplicationWebService\staging.java\web\release" @"Y:\AppEngineUserAgentLoggerWithXSLXAsset.ApplicationWebService\staging.java\web\files"

                //Y:\AppEngineUserAgentLoggerWithXSLXAsset.ApplicationWebService\staging.java\web\java\ScriptCoreLibJava\BCLImplementation\System\Data\SQLite\__SQLiteConnection.java:47: error: unreported exception SQLException; must be caught or declared to be thrown
                //            throw exception1;
                //            ^
                //Note: Y:\AppEngineUserAgentLoggerWithXSLXAsset.ApplicationWebService\staging.java\web\java\ScriptCoreLibJava\BCLImplementation\System\Threading\__Thread.java uses or overrides a deprecated API.

                throw new InvalidOperationException("__SQLiteConnection " + new { ex.Message, ex.StackTrace });
            }

            // to be done by the user?
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestAppEngineOrderByThenGroupBy\ApplicationWebService.cs


            //using (var cmd = new __SQLiteCommand(
            //    "CREATE DATABASE IF NOT EXISTS `" + this.InternalConnectionString.DataSource + "`",
            //    (__SQLiteConnection)(object)this))
            //{
            //    cmd.ExecuteNonQuery();
            //}


            //// http://stackoverflow.com/questions/1675333/php-mysql-joins-across-databases
            //using (var cmd = new __SQLiteCommand(
            //    "USE `" + this.InternalConnectionString.DataSource + "`",
            //    (__SQLiteConnection)(object)this))
            //{
            //    cmd.ExecuteNonQuery();
            //}

        }

        public override void Close()
        {
            //Console.WriteLine("__SQLiteConnection.Close");

            try
            {
                if (this.InternalConnection != null)
                {
                    this.InternalConnection.close();
                    this.InternalConnection = null;

                }

            }
            catch
            {
                throw;
            }
        }



        public override void Dispose(bool e)
        {
            this.Close();
        }

        public __SQLiteCommand InternalLastInsertRowIdCommand;

        //public long LastInsertRowId
        public int LastInsertRowId
        {
            get
            {
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140325

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

                return (int)value;
            }
        }
    }
}
