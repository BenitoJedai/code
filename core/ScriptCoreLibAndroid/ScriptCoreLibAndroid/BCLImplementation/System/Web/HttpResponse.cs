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

            WriteLine("HTTP/1.1 " + StatusCode);
            WriteLine("Content-Type: " + ContentType);

            // :ERR_INVALID_CHUNKED_ENCODING
            if (InternalIsTransferEncodingChunked)
                this.Headers["Transfer-Encoding"] = "chunked";

            this.Headers["Connection"] = "close";

            foreach (var item in this.Headers.AllKeys)
            {
                //android.util.Log.wtf("InternalWriteHeaders", item);

                WriteLine(item + ": " + this.Headers[item]);
            }

            WriteLine("");
            //InternalStream.Flush();

        }

        public void Close()
        {
            if (InternalIsTransferEncodingChunked)
            {
                var ChunkedLengthString = "0\r\n\r\n";
                var ChunkedLengthStringBytes = Encoding.UTF8.GetBytes(ChunkedLengthString);

                InternalStream.Write(ChunkedLengthStringBytes, 0, ChunkedLengthStringBytes.Length);

                Flush();
            }

            IsClientConnected = false;
            InternalStream.Close();
        }

        public void Write(object s)
        {
            Write(s.ToString());
        }

        public void Write(string s)
        {
            if (!InternalWriteHeadersDone)
            {
                // needs more testing! breaks xml webmethod calls.
                //InternalIsTransferEncodingChunked = true;

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
                    // Y:\jsc.community\zmovies\MovieAgent\MovieAgentCore\Server\Library\BasicWebCrawler.cs
                    // http://www.httpwatch.com/httpgallery/chunked/
                    // http://zoompf.com/2012/05/too-chunky
                    // http://tools.ietf.org/html/rfc2616#section-3.6.1
                    // http://www.jmarshall.com/easy/http/
                    // http://code.google.com/p/chrome-browser/source/browse/trunk/src/net/http/http_chunked_decoder.cc
                    // http://golang.org/src/pkg/net/http/chunked.go

                    var ChunkedLengthString = buffer.Length.ToString("x8") + "; jsc-chunck\r\n";
                    var ChunkedLengthStringBytes = Encoding.UTF8.GetBytes(ChunkedLengthString);

                    InternalStream.Write(ChunkedLengthStringBytes, 0, ChunkedLengthStringBytes.Length);

                    Flush();

                    buffer = Encoding.UTF8.GetBytes(s + "\r\n");
                }

                InternalStream.Write(buffer, 0, buffer.Length);
            }
            catch
            {
                IsClientConnected = false;
            }
        }

        public void WriteFile(string filename)
        {
            // we only work with absolute paths anyway
            if (filename.StartsWith("/"))
                filename = filename.Substring(1);

            InternalWriteHeaders();

            try
            {
                // assets only?
                var assets = InternalContext.getResources().getAssets();


                var s = assets.open(filename).ToNetworkStream();

                s.CopyTo(InternalStream);

                Flush();
            }
            catch
            {
                throw;
            }
        }

        public void Flush()
        {
            try
            {
                InternalStream.Flush();
            }
            catch
            {
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

        }
    }
}
