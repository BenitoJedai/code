using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Resolvers;

namespace zproxy.wordpress.com
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

            var x = new WebClient();

            var source = x.DownloadString("http://zproxy.wordpress.com");


            // XElement cant work with that just yet

            Console.WriteLine(new { source.Length });

            // Send it back to the caller.
            y(source);
        }

    }
}
