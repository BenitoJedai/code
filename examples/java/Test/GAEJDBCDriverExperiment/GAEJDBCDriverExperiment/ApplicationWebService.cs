using com.google.appengine.api.rdbms;
using java.sql;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;

namespace GAEJDBCDriverExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void AddItem(string Key, string Content, Action<string> y)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20121001-solutionbuilderv1/20121014-gae-data

            try
            {
                //LocalRdbmsServiceTestConfig rdbms = new
                //LocalRdbmsServiceTestConfig();
                //rdbms.setDriverClass("com.mysql.jdbc.Driver");
                //rdbms.setJdbcConnectionStringFormat("jdbc:mysql://localhost:3306/
                //databasename?
                //user=databaseuser&password=databasepassword&useInformationSchema=true&useUnicode=true&characterEncoding=UTF8&useServerPrepStmts=true");
                //rdbms.setDatabase("databsename");
                //rdbms.setPassword("databasepassword");
                //rdbms.setUser("databaseuser");
                //mysqlhelper = new LocalServiceTestHelper(rdbms);
                //mysqlhelper.setUp();


                DriverManager.registerDriver(new AppEngineDriver());

                //        Caused by: java.lang.IllegalStateException: java.lang.ClassNotFoundException: com.mysql.jdbc.Driver
                //at com.google.appengine.api.rdbms.dev.LocalRdbmsServiceLocalDriver.registerDriver(LocalRdbmsServiceLocalDriver.java:95)
                // https://groups.google.com/forum/?fromgroups=#!topic/google-cloud-sql-discuss/gTTWzmxqzAs
                // "Google App Engine doesn't allow you to open Sockets. When you try to 
                //load the JDBC driver, it makes a socket connection in a static block. 
                //An exception in the static block leads to a ClassNotFoundException, 
                //which is what you are seeing..." 


                //        Caused by: java.lang.ArrayIndexOutOfBoundsException: 1
                //at com.google.cloud.sql.jdbc.internal.Url.parseInstanceDatabase(Url.java:156)
                //at com.google.cloud.sql.jdbc.internal.Url.create(Url.java:84)
                //at com.google.cloud.sql.jdbc.Driver.connect(Driver.java:53)

                var c = DriverManager.getConnection("jdbc:google:rdbms://instance_name", "root", "");

                // http://www.tutorialspoint.com/jdbc/jdbc-create-database.htm

                var s = c.createStatement();

                // java.lang.RuntimeException: Can't create database 'guestbook00'; database exists
                // X:\jsc.svn\core\ScriptCoreLib\PHP\BCLImplementation\System\Data\SQLite\SQLiteConnection.cs
                //s.executeUpdate("CREATE DATABASE guestbook00");
                s.executeUpdate("CREATE DATABASE IF NOT EXISTS `guestbook00`");

                s.executeUpdate("USE guestbook00");

                // X:\jsc.svn\examples\php\PHPWiki\PHPWiki\ApplicationWebService.cs
                s.executeUpdate("create table if not exists MY_TABLEXX (XKey text not null, Content text not null)");

                s.executeUpdate("insert into MY_TABLEXX (XKey, Content) values ('" + Key.Replace("'", "\\'") + "', '" + Content.Replace("'", "\\'") + "')");

                //new SQLiteCommand("insert into MY_TABLEXX (XKey, Content) values ('" + Key.Replace("'", "\\'") + "', '" + e.Replace("'", "\\'") + "')", c).ExecuteNonQuery();

                var countr = s.executeQuery("select count(*) from MY_TABLEXX");

                if (countr.next())
                {
                    // java.lang.RuntimeException: ResultSet does not have a column 0
                    var value = countr.getInt(1);

                    y("count: " + value);
                }



            }
            catch
            {
                //        Caused by: java.lang.IllegalStateException: System property rdbms.driver must be set.
                //at com.google.appengine.api.rdbms.dev.LocalRdbmsServiceLocalDriver.registerDriver(LocalRdbmsServiceLocalDriver.java:80)

                throw;
            }

        }


        public void EnumerateItems(string Key, Action<string, string> y)
        {
            try
            {
                var c = DriverManager.getConnection("jdbc:google:rdbms://instance_name", "root", "");
                var s = c.createStatement();

                // java.lang.RuntimeException: You have an error in your SQL syntax; check the manual that corresponds to your MySQL server version for the right syntax to use near 'Key, Content from MY_TABLEXX' at line 1

                s.executeUpdate("USE guestbook00");

        //        Caused by: java.sql.SQLException: You have an error in your SQL syntax; check the manual that corresponds to your MySQL server version for the right syntax to use near 'Key, Content from MY_TABLEXX' at line 1
        //at com.google.cloud.sql.jdbc.internal.Exceptions.newSqlException(Exceptions.java:215)

                var r = s.executeQuery("select XKey, Content from MY_TABLEXX");

                while (r.next())
                {
                    y(
                        r.getString(1),
                        r.getString(2)
                    );
                }
            }
            catch
            {
                throw;
            }

        }

    }
}
