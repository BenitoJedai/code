using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;
using ScriptCoreLibJava.BCLImplementation.System.IO;
using ScriptCoreLibJava.BCLImplementation.System.Net.Sockets;
using System.Net.Sockets;
using System.Web;

namespace ScriptCoreLibJava.BCLImplementation.System.Web
{
    [Script(Implements = typeof(global::System.Web.HttpResponse))]
    internal class __HttpResponse
    {
        public javax.servlet.http.HttpServletResponse InternalContext;

        internal int InternalStatusCode;

        public int StatusCode
        {
            get
            {
                return InternalStatusCode;
            }
            set
            {
                InternalStatusCode = value;
                this.InternalContext.setStatus(value);
            }
        }

        #region ContentType
        internal string InternalContentType;
        public string ContentType
        {
            get
            {
                return InternalContentType;
            }

            set
            {
                InternalContentType = value;
                this.InternalContext.setContentType(value);
            }
        }
        #endregion


        public void Write(string s)
        {
            try
            {
                //this.InternalContext.getWriter().print(s);

                var bytes = Encoding.UTF8.GetBytes(s);

                this.OutputStream.Write(bytes, 0, bytes.Length);
            }
            catch
            {
                throw;
            }
        }

        public void Redirect(string url)
        {
            try
            {
                this.InternalContext.sendRedirect(url);
            }
            catch
            {
                throw;
            }
        }

        #region OutputStream
        public NetworkStream InternalOutputStream;
        public Stream OutputStream
        {
            get
            {
                if (this.InternalOutputStream == null)
                    try
                    {
                        this.InternalOutputStream = (NetworkStream)(object)new __NetworkStream
                        {
                            InternalOutputStream = this.InternalContext.getOutputStream()
                        };

                    }
                    catch
                    {
                        throw;
                    }

                return InternalOutputStream;
            }
        }
        #endregion


        public void AddHeader(string name, string value)
        {
            this.InternalContext.addHeader(name, value);
        }

        public void WriteFile(string filename)
        {
            // we only work with absolute paths anyway
            if (filename.StartsWith("/"))
                filename = filename.Substring(1);

            var bytes = File.ReadAllBytes(filename);

            this.OutputStream.Write(bytes, 0, bytes.Length);
        }

        public __HttpCachePolicy Cache
        {
            get
            {
                return new __HttpCachePolicy { };
            }
        }

        public void Flush()
        {
            try
            {
                this.OutputStream.Flush();
            }
            catch
            {
                //IsClientConnected = false;
            }
        }

        public HttpCookieCollection Cookies { get; set; }

        public __HttpResponse()
        {
            Cookies = new HttpCookieCollection();
        }

        public void SetCookie(HttpCookie e)
        {
            // http://en.wikipedia.org/wiki/HTTP_cookie

            // Set-Cookie:session="eyB0aWNrcyA9IDYzNDkzNzg5MDQyMzM5MDAwMCwgYWNjb3VudCA9IDEsIGNvbW1lbnQgPSB3ZSBzaGFsbCBTSEExIHRoaXMhIH0="
            // Set-Cookie:session=eyB0aWNrcyA9IDYzNDkzNzk2NTU3NzczMjI5MiwgYWNjb3VudCA9IDIsIGNvbW1lbnQgPSB3ZSBzaGFsbCBTSEExIHRoaXMhIH0=; path=/

            this.AddHeader("Set-Cookie",
                e.Name + "=" + e.Value + ";  path=/");

        }
    }
}
