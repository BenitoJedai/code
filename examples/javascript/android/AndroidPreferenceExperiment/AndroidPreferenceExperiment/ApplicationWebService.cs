using android.content;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;

namespace AndroidPreferenceExperiment
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
            var p = ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext.getSharedPreferences(
                "ThreadLocalContextReference", Context.MODE_PRIVATE
            );

            var port = p.getInt("port", 0);

            if (port == 0)
            {
                port = int.Parse(e);
            }

            p.edit().putInt("port", port).commit();

            // Send it back to the caller.
            y(port + "");


        }

    }
}
