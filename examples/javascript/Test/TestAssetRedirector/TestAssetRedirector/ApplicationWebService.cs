using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestAssetRedirector
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
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

        static int counter = 0;

        #region InternalHandler
        [Obsolete("experimental")]
        public /* will not be part of web service itself */ void InternalHandler(WebServiceHandler h)
        {
            //Console.WriteLine("enter InternalHandler");

            var HeadersHost = h.Context.Request.Headers["Host"];

            //Console.WriteLine("enter InternalHandler " + new { HeadersHost });

            var HostUri = new
            {
                Host = HeadersHost.TakeUntilIfAny(":"),
                Port = HeadersHost.SkipUntilOrEmpty(":")
            };

            #region path
            var path = h.Context.Request.Path;

            // cassini fix
            if (path == "/default.htm")
                path = "/";
            #endregion


            //var adressbardomain = "192.168.43.252";
            var adressbardomain = "192.168.1.75";
            //http://192.168.1.75/
            // XMLHttpRequest cannot load http://127.0.0.3:28071/xml. No 'Access-Control-Allow-Origin' header is present on the requested resource. Origin 'http://192.168.43.252:28071' is therefore not allowed access.

            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/04-monese/2014/201401/20140106-dev-day/quota

            // cannot change the root url as we would change the visible address then
            if (path != "/")
                // the io also has to stary on the main
                if (path != "/xml")
                    // the rest however, can be channeled via cloudflare or coralcache
                    if (HostUri.Host == adressbardomain)
                    {
                        h.Context.Response.Redirect(
                            "http://127.0.0." + (1 + counter++ % 254) + ":" + HostUri.Port + path
                        );


                        h.CompleteRequest();
                        return;
                    }

            Console.WriteLine("InternalHandler " + new { path });
        }
        #endregion

    }
}
