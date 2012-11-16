using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace DropFileIntoSQLite
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
            // Send it back to the caller.
            y(e);
        }

#if FUTURE
        public void XUpload(Blob f, Action<string> y)
        {
            // this is how a recieving of a file should look like.
        }
#endif

        public void InternalHandler(WebServiceHandler h)
        {
            if (h.IsDefaultPath)
                return;


            if (h.Context.Request.Path == "/upload")
            {

                var c = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;

                var TextContent = h.Context.Request.Form["TextContent"];

                Console.WriteLine(TextContent);



                foreach (HttpPostedFile item in h.Context.Request.Files.AllKeys.Select(k => h.Context.Request.Files[k]))
                {
                    //var bytes = item.InputStream.ReadToEnd();
                    var bytes = item.InputStream.ToBytes();

                    Console.WriteLine(
                        new { item.ContentType, item.FileName, item.ContentLength, bytes.Length }
                    );
                }


                Console.ForegroundColor = c;

                // close
                h.Context.Response.StatusCode = 204;
                h.CompleteRequest();
                return;
            }
        }
    }
}
