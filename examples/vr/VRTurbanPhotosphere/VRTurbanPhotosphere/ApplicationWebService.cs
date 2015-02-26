using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VRTurbanPhotosphere
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // Uncaught URIError: URI malformed

        /////// <summary>
        /////// The static content defined in the HTML file will be update to the dynamic content once application is running.
        /////// </summary>
        //public XElement Header = new XElement(@"h1", @"JSC - The .NET crosscompiler for web platforms. ready.");
        public XElement Header = new XElement(@"h1", @"JSC - The .NET crosscompiler for web platforms. ready.");
        // "%1B%19%13%CA%D1hJ%E9%14%1B-%F3%DA%D1%AC%5E%09%B2%1EX%B0%1BI%B2%5E%E1%E5%5B%98%EF%0F"
        //  // ScriptCoreLib.JavaScript.BCLImplementation.System.Text.__UTF8Encoding.GetString
        // "PGgxPkpTQyAtIFRoZSAuTkVUIGNyb3NzY29tcGlsZXIgZm9yIHdlYiBwbGF0Zm9ybXMuIHJlYWR5LjwvaDE+"
        // "PGgxPkpTQyAtIFRoZSAuTkVUIGNyb3NzY29tcGlsZXIgZm9yIHdlYiBwbGF0Zm9ybXMuIHJlYWR5LjwvaDE+"

        //  // ScriptCoreLib.Library.StringConversions.UTF8FromBase64StringOrDefault
        //   // ScriptCoreLib.JavaScript.Remoting.InternalWebMethodRequest.GetInternalFieldValue

        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // Send it back to the caller.
            y(e);
        }

    }
}
