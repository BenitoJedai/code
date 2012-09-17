using AndroidNuGetSQLiteActivity;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;

namespace AndroidNuGetSQLiteWebActivity
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
            __SQLiteConnectionHack.Context = ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext;

            MyDatabase.Write();


            var contentRead = "-";

            contentRead = MyDatabase.Read(contentRead);

            // Send it back to the caller.
            y(contentRead);
        }

    }
}
