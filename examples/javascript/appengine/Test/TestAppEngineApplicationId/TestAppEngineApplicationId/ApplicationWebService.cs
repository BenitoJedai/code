using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestAppEngineApplicationId
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {

        public XElement title;

        public Task yield()
        {
            title.Value = "hi";

            // https://code.google.com/p/googleappengine/source/browse/trunk/java/src/main/com/google/appengine/api/utils/SystemProperty.java?r=219
            // https://developers.google.com/appengine/docs/adminconsole/performancesettings

#if !DEBUG
            //var environment = com.google.appengine.api.utils.SystemProperty.environment.value().value();

            var applicationId = com.google.appengine.api.utils.SystemProperty.applicationId.get();
            var applicationVersion = com.google.appengine.api.utils.SystemProperty.applicationVersion.get();


            title.Value = new
            {
                applicationId,
                applicationVersion
                //, environment 
            }.ToString();
#endif

            return "".AsResult();
        }
    }
}
