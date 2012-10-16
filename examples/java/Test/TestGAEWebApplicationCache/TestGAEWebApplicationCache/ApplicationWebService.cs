using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;

namespace TestGAEWebApplicationCache
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
            var PreviousFoo = ApplicationWebServiceCache.Value.Foo;

            ApplicationWebServiceCache.Value.Foo = e;

            // Send it back to the caller.
            y(PreviousFoo);
        }

    }

    public class ApplicationWebServiceCache
    {
        #region Value synchronized by lock
        static object InternalSyncLock = new object();

        static ApplicationWebServiceCache InternalValue;

        public static ApplicationWebServiceCache Value
        {
            get
            {
                if (InternalValue == null)
                    lock (InternalSyncLock)
                    {
                        if (InternalValue == null)
                            InternalValue = new ApplicationWebServiceCache();
                    }

                return InternalValue;
            }
        }
        #endregion

        private ApplicationWebServiceCache()
        {
            Console.WriteLine("ApplicationWebServiceCache loaded for current CLR AppDomain/JVM Classloader context");

        }

        public string Foo = "bar";
    }
}
