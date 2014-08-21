using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.google.apphosting.api
{
    // https://developers.google.com/appengine/docs/java/javadoc/com/google/apphosting/api/ApiProxy.Environment#getRemainingMillis()

    [Script(IsNative = true)]
    public class ApiProxy
    {
        // tested by ?

        public static ApiProxy.Environment getCurrentEnvironment()
        {
            return default(ApiProxy.Environment);
        }

        [Script(IsNative = true)]
        public interface Environment
        {
            long getRemainingMillis();
        }
    }
}
