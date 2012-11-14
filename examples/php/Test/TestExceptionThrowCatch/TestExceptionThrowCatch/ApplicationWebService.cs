using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;

namespace TestExceptionThrowCatch
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
                X.ThrowFoo();

                y(e);

            }
            catch (Exception ex)
            {
                y("error:\r\n  " + ex.Message + "\r\n  " + ex.StackTrace.TakeUntilOrEmpty("\n"));
            }
            // Send it back to the caller.
        }

    }

    static class X
    {
        public static void ThrowFoo()
        {
            throw new Exception("Foo");
        }
    }
}
