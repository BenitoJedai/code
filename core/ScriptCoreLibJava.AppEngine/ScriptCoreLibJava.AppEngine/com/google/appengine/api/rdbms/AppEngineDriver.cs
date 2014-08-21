using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.google.appengine.api.rdbms
{
    // https://developers.google.com/appengine/docs/java/cloud-sql/developers-guide
    [Script(IsNative = true)]
    public class AppEngineDriver : java.sql.Driver
    {
        // Caused by: java.lang.ClassNotFoundException: com.mysql.jdbc.Driver
        // "C:\util\appengine-java-sdk-1.8.2\lib\impl\mysql-connector-java-5.1.22-bin.jar"
        // "C:\util\xampp-win32-1.8.0-VC9\xampp\mysql_start.bat"

        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestAppEngineOrderByThenGroupBy\ApplicationWebService.cs

        public bool acceptsURL(string url)
        {
            throw new NotImplementedException();
        }

        public java.sql.Connection connect(string url, java.util.Properties info)
        {
            throw new NotImplementedException();
        }

        public int getMajorVersion()
        {
            throw new NotImplementedException();
        }

        public int getMinorVersion()
        {
            throw new NotImplementedException();
        }

        public java.sql.DriverPropertyInfo[] getPropertyInfo(string url, java.util.Properties info)
        {
            throw new NotImplementedException();
        }

        public bool jdbcCompliant()
        {
            throw new NotImplementedException();
        }

        // new indexer 177?
        public java.util.logging.Logger getParentLogger()
        {
            throw new NotImplementedException();
        }
    }
}
