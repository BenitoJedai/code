using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace java.sql
{
    // http://docs.oracle.com/javase/1.4.2/docs/api/java/sql/DriverManager.html
    [Script(IsNative = true)]
    public class DriverManager
    {
        public static void registerDriver(Driver driver)
        {
        }

        public static Connection getConnection(string value)
        {
            return default(Connection);
        }

        public static Connection getConnection(string value, string user, string password)
        {
            return default(Connection);
        }
    }
}
