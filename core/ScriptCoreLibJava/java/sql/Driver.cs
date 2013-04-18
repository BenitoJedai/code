using java.util;
using ScriptCoreLib;


namespace java.sql
{
    // http://docs.oracle.com/javase/1.4.2/docs/api/java/sql/Driver.html
    [Script(IsNative = true)]
    public interface Driver
    {
        bool acceptsURL(string url);

        Connection connect(string url, Properties info);

        int getMajorVersion();

        int getMinorVersion();

        DriverPropertyInfo[] getPropertyInfo(string url, Properties info);


        bool jdbcCompliant();

        java.util.logging.Logger getParentLogger();

    }
}
