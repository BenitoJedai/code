using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Abstractatech.JavaScript.FileStorage
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


        public void InternalHandler(WebServiceHandler h)
        {
            //var WebServiceMethod = h.Context.Request.Headers["WebServiceMethod"];

            if (h.Context.Request.HttpMethod == "POST")
                if (h.Context.Request.Path == "/FileStorageUpload")
                {
                    var Files = h.Context.Request.Files.AllKeys.Select(k => h.Context.Request.Files[k]);
                    foreach (HttpPostedFile item in Files)
                    {
                        //var bytes = item.InputStream.ReadToEnd();
                        var bytes = item.InputStream.ToBytes();
                        var FileName = item.FileName;

                        Console.WriteLine("FileStorageUpload: " +
                            new { item.ContentType, FileName, item.ContentLength, bytes.Length }
                        );
                    }

                    h.Context.Response.StatusCode = 204;
                    h.CompleteRequest();
                }
        }

    }
}
