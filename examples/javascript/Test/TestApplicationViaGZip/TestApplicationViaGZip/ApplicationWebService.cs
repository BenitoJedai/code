using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace TestApplicationViaGZip
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


        public void Handler(WebServiceHandler h)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201301/20130103

            if (h.Context.Request.Path == "/view-source")
            {
                var Response = h.Context.Response;

                Console.WriteLine();

                h.Context.Request.Headers.AllKeys.WithEach(
                    Header =>
                    {
                        Console.WriteLine(Header + ": " + h.Context.Request.Headers[Header]);

                    }
                );

                // http://code.google.com/p/googleappengine/issues/detail?id=1804
                // http://stackoverflow.com/questions/9772061/app-engine-accept-encoding
                // https://developers.google.com/appengine/docs/python/runtime#Responses
                var AcceptEncoding = h.Context.Request.Headers["Accept-Encoding"];

                // http://stackoverflow.com/questions/6891034/sending-a-pre-gzipped-javascript-to-the-web-browser
                // appengine does not allow this for now.

                //Accept-Encoding:gzip,deflate,sdch

                if (!string.IsNullOrEmpty(AcceptEncoding))
                    if (AcceptEncoding.Contains("gzip"))
                    {
                        // http://www.dotnetperls.com/gzip-aspnet

                        //Response.ContentType = "text/javascript";
                        Response.ContentType = "application/x-gzip";
                        
                        var app = h.Applications[0];

                        Response.AddHeader("Content-Encoding", "gzip");
                        Response.AddHeader("X-GZipAssemblyFile", app.GZipAssemblyFile);

#if !flag1
                        Response.WriteFile("/" + app.GZipAssemblyFile);
#else


                    var m = new MemoryStream();


                    var mgz = new System.IO.Compression.GZipStream(m, System.IO.Compression.CompressionMode.Compress);

                    new StreamWriter(mgz) { AutoFlush = true }.WriteLine("\n/* gzip start */\n");
                    var size = 0;

                    app.References.WithEach(
                        f =>
                        {
                            var Name = f.AssemblyFile;

                            var bytes = File.ReadAllBytes(Name + ".js");
                            size += bytes.Length;
                            Response.AddHeader("X-Assembly", "" + new { Name, bytes.Length });

                            mgz.Write(bytes, 0, bytes.Length);
                        }
                    );

                    new StreamWriter(mgz) { AutoFlush = true }.WriteLine("\n/* gzip end */\n");


                    //mgz.Flush();
                    // http://code.google.com/p/tar-cs/issues/detail?id=14
                    mgz.Close();


                    //Response.AddHeader("Content-Length", "" + m.Length);
                    // Cannot access a closed Stream.
                    //m.Position = 0;
                    // 
                    var gzbytes = m.ToArray();

                    Response.AddHeader("X-GZip-Ratio", "" + (int)(gzbytes.Length * 100 / size) + "% of " + size + " bytes");

                    File.WriteAllBytes(app.References.Last().AssemblyFile + ".js.gz", gzbytes);

                    Response.OutputStream.Write(gzbytes, 0, gzbytes.Length);
#endif

                        h.CompleteRequest();
                    }
            }
        }
    }
}
