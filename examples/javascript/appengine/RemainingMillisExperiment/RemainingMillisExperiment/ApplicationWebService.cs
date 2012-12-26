using com.google.apphosting.api;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;

namespace RemainingMillisExperiment
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
        public void getRemainingMillis(string e, Action<string> y)
        {
            // http://stackoverflow.com/questions/13351563/how-to-impose-google-app-engines-deadlineexceededexception

            var en = ApiProxy.getCurrentEnvironment();

            var RemainingMillis = en.getRemainingMillis();

            // Send it back to the caller.
            y(new { RemainingMillis }.ToString());
        }

    }
}
