using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;
using ScriptCoreLib.Ultra.WebService;
using System.IO;
using System.Net;

namespace LoadExternalFlashComponent
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript. JSC supports string data type for all platforms.</param>
        /// <param name="y">A callback to javascript. In the future all platforms will allow Action&lt;XElementConvertable&gt; delegates.</param>
        public void WebMethod2(string e, StringAction y)
        {
            // Send it back to the caller.
            y(e);
        }

        public void Proxy(WebServiceHandler h)
        {
            var r = h.Context.Request.Path;

            Console.WriteLine("get: " + r);


            if (h.IsDefaultPath)
            {
                h.Default();
                return;
            }

            var p = "/proxy/";

            if (r == "/images/eesti.jpg")
                r = "/proxy/www.regio.ee" + r;

            if (r.StartsWith(p))
            {
                var f = "http://" + r.SkipUntilIfAny(p);
                Console.WriteLine("download: " + f);

                var bytes = new WebClient().DownloadData(f);

                h.Context.Response.OutputStream.Write(bytes, 0, bytes.Length);
                h.CompleteRequest();

                return;
            }

        }

    }
}
