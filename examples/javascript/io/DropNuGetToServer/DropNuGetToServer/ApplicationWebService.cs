using DropNuGetToServer.Schema;
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
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace DropNuGetToServer
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



        public void InternalHandler(WebServiceHandler h)
        {
            if (h.IsDefaultPath)
                return;


            #region /upload
            if (h.Context.Request.Path == "/upload")
            {
                Console.WriteLine("enter upload");


                var TextContent = h.Context.Request.Form["TextContent"];

                //Console.WriteLine(TextContent);


                var ok = new XElement("ok");

                var Files = h.Context.Request.Files.AllKeys.Select(k => h.Context.Request.Files[k]);
                foreach (HttpPostedFile item in Files)
                {
                    //var bytes = item.InputStream.ReadToEnd();
                    var bytes = item.InputStream.ToBytes();
                    var FileName = item.FileName;

                    Console.WriteLine(
                        new { item.ContentType, FileName, item.ContentLength, bytes.Length }
                    );

                    // http://code.activestate.com/recipes/252531-storing-binary-data-in-sqlite/


                    var t = new Table1();

                    // jsc cannot handle method call parameter with initializer and a delegate?
                    var __value = new Table1Queries.Insert
                        {
                            //ContentValue = item.FileName,
                            ContentValue = FileName,
                            ContentBytes = bytes
                        };

                    //enter upload
                    //{ ContentType = application/octet-stream, FileName = GravatarExperiment.1.0.0.0.nupkg, ContentLength = 9981, Length = 9981 }
                    //before insert { ManagedThreadId = 19 }
                    //after insert { LastInsertRowId = 1 }

                    Console.WriteLine("before insert " + new { Thread.CurrentThread.ManagedThreadId });

                    t.Insert(
                        value: __value,
                        yield:
                        LastInsertRowId =>
                        {
                            Console.WriteLine("after insert " + new { LastInsertRowId });
                            
                            ok.Add(new XElement("ContentKey", "" + LastInsertRowId));
                        }
                    );

                }


                h.Context.Response.ContentType = "text/xml";

                h.Context.Response.Write(ok);

                // close
                h.CompleteRequest();
                return;
            }
            #endregion
        }

    }
}
