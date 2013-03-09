using com.mikandi.android.kandibilling;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;

namespace MikandiExample
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        private static int appId = 1452;
        private static string appSecret = "18c79088-08c2-11e0-80ac-12313d061066";

        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // do we need the activity instead?
            var context = ThreadLocalContextReference.CurrentContext;

            KandiBillingClient.init(context, appId, appSecret);

            // Send it back to the caller.
            y(e);
        }

    }
}
