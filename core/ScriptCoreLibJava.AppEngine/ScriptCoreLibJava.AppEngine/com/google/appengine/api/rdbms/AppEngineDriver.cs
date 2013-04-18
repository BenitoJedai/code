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
