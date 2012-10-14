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
        public void WebMethod2(string fname, string content, Action<string> y)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20121001-solutionbuilderv1/20121014-gae-data

            try
            {
                DriverManager.registerDriver(new AppEngineDriver());
                var c = DriverManager.getConnection("jdbc:google:rdbms://instance_name/guestbook");
                //Connection c = DriverManager.getConnection("jdbc:google:rdbms://instance_name/database", "user", "password");

                var statement = "INSERT INTO entries (guestName, content) VALUES( ? , ? )";
                PreparedStatement stmt = c.prepareStatement(statement);
                stmt.setString(1, fname);
                stmt.setString(2, content);
                int success = 2;
                success = stmt.executeUpdate();
                if (success == 1)
                {
                    y("Success!");
                }
                else if (success == 0)
                {
                    y("Failure! Please try again!");
                }
            }
            catch
            {
        //        Caused by: java.lang.IllegalStateException: System property rdbms.driver must be set.
        //at com.google.appengine.api.rdbms.dev.LocalRdbmsServiceLocalDriver.registerDriver(LocalRdbmsServiceLocalDriver.java:80)

                throw;
            }

        }

    }
}
