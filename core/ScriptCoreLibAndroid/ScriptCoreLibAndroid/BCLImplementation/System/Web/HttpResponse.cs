using android.content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using java.io;
using System.Collections.Specialized;
using System.Web;
using System.IO;
namespace ScriptCoreLib.Android.BCLImplementation.System.Web
{
    // http://referencesource.microsoft.com/#System.Web/xsp/system/Web/HttpResponse.cs

    [Script(Implements = typeof(global::System.Web.HttpResponse))]
    public sealed class __HttpResponse
    {
        // X:\jsc.svn\examples\java\android\ApplicationWebService\ApplicationWebService\ApplicationActivity.cs

        public ContextWrapper InternalContext;
        public NetworkStream InternalStream;

        public int StatusCode { get; set; }
        public string ContentType { get; set; }

        public __HttpCachePolicy Cache
        {
            get
            {
                return new __HttpCachePolicy { InternalResponse = this };
            }
        }



        public __HttpResponse()
        {
            //ContentType = "application/octet-stream";
            ContentType = "text/html";
            StatusCode = 200;
            Headers = new NameValueCollection();
            IsClientConnected = true;
        }

        public NameValueCollection Headers { get; set; }

        public void AddHeader(string name, string value)
        {
            // http://stackoverflow.com/questions/14315224/disable-chunked-encoding-for-http-server-responses



            Headers[name] = value;
        }

        public void Redirect(string url)
        {
            // http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html
            StatusCode = 302;
            AddHeader("Location", url);

            InternalWriteHeaders();
        }

        bool InternalWriteHeadersDone;

        // what will enabling this break?
        bool InternalIsTransferEncodingChunked;

        void InternalWriteHeaders()
        {
            if (InternalWriteHeadersDone)
                return;

            InternalWriteHeadersDone = true;

            Action<string> WriteLine =
                x =>
                {
                    var HeaderStringBytes = Encoding.UTF8.GetBytes(x + "\r\n");

                    InternalStream.Write(HeaderStringBytes, 0, HeaderStringBytes.Length);
                };

            #region StatusCode
            if (StatusCode == 200)
            {
                WriteLine("HTTP/1.1 " + StatusCode + " 200");
            }
            else
            {
                WriteLine("HTTP/1.1 " + StatusCode);
            }
            #endregion


            // = "text/html; charset=utf-8";

            if (ContentType == "text/html")
                this.Headers["Content-Type"] = "text/html; charset=utf-8";


            this.Headers["Content-Type"] = ContentType;
            this.Headers["Connection"] = "close";

            foreach (var item in this.Headers.AllKeys)
            {
                //android.util.Log.wtf("InternalWriteHeaders", item);

                var h = item + ": " + this.Headers[item];
                WriteLine(h);

                //Console.WriteLine(h);
            }

            WriteLine("");
            //InternalStream.Flush();

        }

        public void Close()
        {

            this.Flush();

            if (InternalIsTransferEncodingChunked)
            {
                //Console.WriteLine("Close InternalIsTransferEncodingChunked");
                try
                {
                    var ChunkedLengthString = "0\r\n\r\n";
                    var ChunkedLengthStringBytes = Encoding.UTF8.GetBytes(ChunkedLengthString);

                    InternalStream.Write(ChunkedLengthStringBytes, 0, ChunkedLengthStringBytes.Length);

                    InternalStream.Flush();
                }
                catch
                {
                    // why?
                    //Console.WriteLine("failed to close chunk");
                }
            }

            IsClientConnected = false;
            InternalStream.Close();
        }

        public void Write(object s)
        {
            Write(s.ToString());
        }

        public StringBuilder WriteChuncks = new StringBuilder();

        public void Write(string s)
        {
            if (!InternalWriteHeadersDone)
            {
                if (this.Headers.AllKeys.Contains("Content-Length"))
                {
                    // cannot be chunked!
                }
                else
                {

                    // needs more testing! breaks xml webmethod calls.
                    InternalIsTransferEncodingChunked = true;


                    this.Headers["Transfer-Encoding"] = "chunked";
                }

                //         Caused by: java.lang.RuntimeException: sendto failed: EPIPE (Broken pipe)
                //at ScriptCoreLibJava.BCLImplementation.System.Net.Sockets.__NetworkStream.Write(__NetworkStream.java:115)
                //at ScriptCoreLibJava.BCLImplementation.System.IO.__Stream.CopyTo(__Stream.java:83)
                //at ScriptCoreLib.Android.BCLImplementation.System.Web.__HttpResponse.WriteFile(__HttpResponse.java:217)

            }

            InternalWriteHeaders();

            try
            {
                var buffer = Encoding.UTF8.GetBytes(s);

                if (InternalIsTransferEncodingChunked)
                {
                    WriteChuncks.Append(s);


                }
                else
                {
                    InternalStream.Write(buffer, 0, buffer.Length);
                }

            }
            catch
            {
                IsClientConnected = false;

                Console.WriteLine("failed to write " + new { s.Length });
            }
        }

        public void WriteFile(string filename)
        {
            // X:\jsc.smokescreen.svn\core\javascript\com.abstractatech.analytics\com.abstractatech.analytics\ApplicationWebService.cs

            // we only work with absolute paths anyway
            if (filename.StartsWith("/"))
                filename = filename.Substring(1);

            InternalWriteHeaders();

            try
            {
                // assets only?
                var assets = InternalContext.getResources().getAssets();


                var s = assets.open(filename).ToNetworkStream();
                // should we report the size?
                s.CopyTo(InternalStream);

                InternalStream.Flush();
            }
            catch
            {
                throw;
            }
        }

        public void Flush()
        {
            if (!InternalWriteHeadersDone)
                InternalWriteHeaders();


            var s = "";

            try
            {
                if (InternalIsTransferEncodingChunked)
                {
                    if (WriteChuncks.Length > 0)
                    {
                        //Console.WriteLine("Flush InternalIsTransferEncodingChunked" + new { WriteChuncks.Length });


                        s = WriteChuncks.ToString();
                        WriteChuncks.Clear();

                        var buffer = Encoding.UTF8.GetBytes(s);


                        // why wont this work?

                        // http://web.mit.edu/javadev/packages/Acme/Serve/servlet/http/ChunkedOutputStream.java
                        // Y:\jsc.community\zmovies\MovieAgent\MovieAgentCore\Server\Library\BasicWebCrawler.cs
                        // http://www.httpwatch.com/httpgallery/chunked/
                        // http://zoompf.com/2012/05/too-chunky
                        // http://tools.ietf.org/html/rfc2616#section-3.6.1
                        // http://www.jmarshall.com/easy/http/
                        // http://code.google.com/p/chrome-browser/source/browse/trunk/src/net/http/http_chunked_decoder.cc
                        // http://golang.org/src/pkg/net/http/chunked.go
                        // http://www.java2s.com/Open-Source/Android/Game/mages/org/aksonov/tools/ChunkedClient.java.htm

                        //var ChunkedLengthString = buffer.Length.ToString("x8") + "; jsc-chunck\r\n";

                        // Unknown chromium error: -324
                        var m = new MemoryStream();

                        #region ChunkedLengthString
                        // https://code.google.com/p/chromium/issues/detail?id=39206

                        // this was expensive fix
                        var ChunkedLengthStringWithPadding = (buffer.Length).ToString("x8");

                        while (ChunkedLengthStringWithPadding[0] == '0')
                        {
                            if (ChunkedLengthStringWithPadding.Length == 1)
                                break;

                            ChunkedLengthStringWithPadding = ChunkedLengthStringWithPadding.Substring(1);
                        }


                        var ChunkedLengthString = ChunkedLengthStringWithPadding + "\r\n";
                        var ChunkedLengthStringBytes = Encoding.UTF8.GetBytes(ChunkedLengthString);


                        m.Write(ChunkedLengthStringBytes, 0, ChunkedLengthStringBytes.Length);
                        #endregion

                        // http://stackoverflow.com/questions/5142649/how-to-send-http-reply-using-chunked-encoding

                        // Unknown chromium error: -321
                        buffer = Encoding.UTF8.GetBytes(s + "\r\n");
                        m.Write(buffer, 0, buffer.Length);

                        buffer = m.ToArray();


                        //var DebugTransferEncodingChunked = Encoding.UTF8.GetString(buffer);

                        //Console.WriteLine(new { DebugTransferEncodingChunked });

                        InternalStream.Write(buffer, 0, buffer.Length);
                        //InternalStream.Flush();



                        //var ChunkedLengthString = "0\r\n\r\n";
                        //var ChunkedLengthStringBytes = Encoding.UTF8.GetBytes(ChunkedLengthString);

                        //InternalStream.Write(ChunkedLengthStringBytes, 0, ChunkedLengthStringBytes.Length);
                    }
                }

                InternalStream.Flush();
            }
            catch
            {
                Console.WriteLine("failed to flush " + new { s.Length });

                IsClientConnected = false;
            }
        }

        public Stream OutputStream
        {
            get
            {
                InternalWriteHeaders();

                return this.InternalStream;
            }
        }

        public bool IsClientConnected
        {
            get;
            set;
        }

        public void SetCookie(HttpCookie cookie)
        {
            // http://en.wikipedia.org/wiki/HTTP_cookie

            // Set-Cookie:session="eyB0aWNrcyA9IDYzNDkzNzg5MDQyMzM5MDAwMCwgYWNjb3VudCA9IDEsIGNvbW1lbnQgPSB3ZSBzaGFsbCBTSEExIHRoaXMhIH0="
            // Set-Cookie:session=eyB0aWNrcyA9IDYzNDkzNzk2NTU3NzczMjI5MiwgYWNjb3VudCA9IDIsIGNvbW1lbnQgPSB3ZSBzaGFsbCBTSEExIHRoaXMhIH0=; path=/

            this.AddHeader("Set-Cookie",
                cookie.Name + "=" + cookie.Value + ";  path=/");
        }
    }
}
