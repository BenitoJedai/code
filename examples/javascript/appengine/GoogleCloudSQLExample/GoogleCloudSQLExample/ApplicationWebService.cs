using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;
using com.google.appengine.api.capabilities;

namespace GoogleCloudSQLExample
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
        public void WebMethod2(string e, Action<string> y)
        {
            // see also: https://developers.google.com/cloud-sql/docs/developers_guide_java
            // http://www.javacodegeeks.com/2011/04/app-engine-capabilities-namespaces-api.html

            // why is that api missing? did we exclue it during build?
            var s = com.google.appengine.api.capabilities.CapabilitiesServiceFactory.getCapabilitiesService();

            Action<Capability> ff =
                value =>
                {
                    var cs = s.getStatus(value);

                    e += value.getPackageName() + ": ";
                    e += value.getName() + ": ";

                    e += cs.getStatus().ToString() + "; \n";
                };

            ff(Capability.BLOBSTORE);
            ff(Capability.DATASTORE);
            ff(Capability.DATASTORE_WRITE);
            ff(Capability.IMAGES);
            ff(Capability.MAIL);
            ff(Capability.MEMCACHE);
            ff(Capability.PROSPECTIVE_SEARCH);
            ff(Capability.TASKQUEUE);
            ff(Capability.URL_FETCH);
            ff(Capability.XMPP);

            // Send it back to the caller.
            y(e);
        }


        
    }
}
