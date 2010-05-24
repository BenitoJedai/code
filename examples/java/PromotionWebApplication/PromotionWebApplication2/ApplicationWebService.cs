// For more information visit:
// http://studio.jsc-solutions.net/

// View as Visual Basic project
// http://do.jsc-solutions.net/View-as-Visual-Basic-project

// View as Visual FSharp project
// http://do.jsc-solutions.net/View-as-Visual-FSharp-project

using System;
using System.Linq;
using System.Xml.Linq;
using ScriptCoreLib;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.Library.Delegates;
using ScriptCoreLib.Ultra.WebService;
using System.IO;

namespace PromotionWebApplication2
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
        public void WebMethod2(XElement e, XElementAction y)
        {
            // Send something back from WebMethod2
            // http://do.jsc-solutions.net/Send-something-back-from-WebMethod2

            e.Element(@"Data").ReplaceAll(@"Data from the web server");
            // Send it to the caller.
            y(e);
        }

        public void DownloadSDK(WebServiceHandler h)
        {
            DownloadSDKFunction.DownloadSDK(h);
        }
    }

    public static class DownloadSDKFunction
    {
        public static void DownloadSDK(WebServiceHandler h)
        {
            const string _download = "/download/";
            const string a = @"assets/PromotionWebApplicationAssets";

            var path = h.Context.Request.Path;

            if (path == "/download")
            {
                h.Context.Response.Redirect(_download);
                h.CompleteRequest();
                return;
            }

            if (path == "/download/")
                path = "/download/publish.htm";


            if (path.StartsWith(_download))
            {
                var f = a + "/" + path.Substring(_download.Length).Replace(" ", "_");


                if (File.Exists(f))
                {

                    var data = File.ReadAllBytes(f);


                    var ext = "." + f.SkipUntilLastOrEmpty(".").ToLower();

                    // http://en.wikipedia.org/wiki/Mime_type
                    // http://msdn.microsoft.com/en-us/library/ms228998.aspx

                    var ContentType = "application/octet-stream";

                    if (ext == ".application")
                    {
                        ContentType = "application/x-ms-application";
                    }
                    else if (ext == ".manifest")
                    {
                        ContentType = "application/x-ms-manifest";
                    }
                    else if (ext == ".htm")
                    {
                        ContentType = "text/html";
                    }

                    h.Context.Response.ContentType = ContentType;

                    Console.WriteLine("length: " + data.Length + " " + ContentType + " " + f);

                    h.Context.Response.OutputStream.Write(data, 0, data.Length);
                }
                else
                {
                    Console.WriteLine("missing " + f);

                    h.Context.Response.StatusCode = 404;
                }


                //h.Context.Response.Redirect(r);
                h.CompleteRequest();

                return;
            }
        }
    }
}
