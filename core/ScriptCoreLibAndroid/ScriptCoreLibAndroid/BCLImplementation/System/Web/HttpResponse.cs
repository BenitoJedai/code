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
    internal sealed class __HttpResponse
    {
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

        public void Close()
        {
            InternalStream.Close();
        }

        public __HttpResponse()
        {
            ContentType = "application/octet-stream";
            StatusCode = 200;
            Headers = new NameValueCollection();
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

        void InternalWriteHeaders()
        {
            if (InternalWriteHeadersDone)
                return;

            InternalWriteHeadersDone = true;

            Action<string> WriteLine = x => Write(x + "\r\n");

            WriteLine("HTTP/1.1 " + StatusCode);
            WriteLine("Content-Type: " + ContentType);

            foreach (var item in this.Headers.AllKeys)
            {
                //android.util.Log.wtf("InternalWriteHeaders", item);

                WriteLine(item + ": " + this.Headers[item]);
            }

            WriteLine("Connection: close");
            WriteLine("");
            //InternalStream.Flush();

        }

        public void Write(string s)
        {
            InternalWriteHeaders();

            var buffer = Encoding.UTF8.GetBytes(s);
            InternalStream.Write(buffer, 0, buffer.Length);
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
            }
            catch
            {
                throw;
            }

            InternalStream.Flush();
        }

        public Stream OutputStream
        {
            get
            {
                InternalWriteHeaders();

                return this.InternalStream;
            }
        }
    }
}
