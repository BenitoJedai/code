using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibJava.BCLImplementation.System.ComponentModel;
using System.Web;

namespace ScriptCoreLibJava.BCLImplementation.System.Web
{
	[Script(Implements = typeof(global::System.Web.HttpApplication))]
	internal class __HttpApplication : __IHttpAsyncHandler, __IHttpHandler, __IComponent, IDisposable
	{
		public HttpRequest Request { get; set; }
		public HttpResponse Response { get; set; }

		HttpContext _Context;
		public HttpContext Context
		{
			get
			{
				if (_Context == null)
					_Context = (HttpContext)(object)new __HttpContext { Request = this.Request, Response = this.Response };

				return _Context;
			}
		}

		public void CompleteRequest()
		{
			try
			{
				((__HttpResponse)(object)this.Response).InternalContext.getWriter().flush();
			}
			catch
			{
				throw new InvalidOperationException();
			}
		}

		#region __IHttpHandler Members

		public bool IsReusable
		{
			get { throw new NotImplementedException(); }
		}

		public void ProcessRequest(global::System.Web.HttpContext context)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region __IComponent Members

		public event EventHandler Disposed;

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
