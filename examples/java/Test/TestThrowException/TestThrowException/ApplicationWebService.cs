using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;

namespace TestThrowException
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
            try
            {
                dothrow();

                // Send it back to the caller.
                y("ok");
            }
            catch (Exception ex)
            {
                y("catch " + ex.Message);
            }
        }

        static void dothrow()
        {
            // PHP can currentyl only throw Exception
            // as multiconstructor is broken in this release
            // yet java will disallow us to thro Throwable 
            // lets implement a workaround for java
            // to throw RuntimeException instead.
            // later lets fix PHP.

            throw new Exception("dothrow");
        }
    }
}
