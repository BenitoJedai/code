using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;
using ScriptCoreLibJava.BCLImplementation.System.IO;
using ScriptCoreLibJava.BCLImplementation.System.Net.Sockets;
using System.Net.Sockets;

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

		public void Write(string s)
		{
			try
			{
				this.InternalContext.getWriter().print(s);
			}
			catch
			{
				throw new InvalidOperationException();
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
				throw new InvalidOperationException();
			}
		}

		public NetworkStream InternalOutputStream;
		public Stream OutputStream
		{
			get
			{
				if (InternalOutputStream == null)
					try
					{
						InternalOutputStream = (NetworkStream)(object)new __NetworkStream
						{
							InternalOutputStream = this.InternalContext.getOutputStream()
						};

					}
					catch
					{
						throw new NotSupportedException();
					}

				return InternalOutputStream;
			}
		}

		public void AddHeader(string name, string value)
		{
			this.InternalContext.addHeader(name, value);
		}

        public void WriteFile(string filename)
        {
            var bytes = File.ReadAllBytes(filename);

            this.OutputStream.Write(bytes, 0, bytes.Length);
        }
	}
}
