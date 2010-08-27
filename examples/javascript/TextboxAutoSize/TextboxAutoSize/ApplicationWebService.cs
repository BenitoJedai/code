// For more information visit:
// http://studio.jsc-solutions.net/

using System;
using System.Linq;
using System.Xml.Linq;
using ScriptCoreLib;
using ScriptCoreLib.Extensions;

namespace TextboxAutoSize
{
    /// <summary>
    /// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript</param>
        /// <param name="y">A callback to javascript</param>
        public void WebMethod2(XElement e, Action<XElement> y)
        {
            // Send it to the caller.
            y(e);
        }

    }
}
