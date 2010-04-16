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

		public int StatusCode
		{
			get
			{
				return 200;
			}
			set
			{
			}
		}

		public string ContentType
		{
			get
			{
				return "";
			}

			set
			{
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

	}
}
