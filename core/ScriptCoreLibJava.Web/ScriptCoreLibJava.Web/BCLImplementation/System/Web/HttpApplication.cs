using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibJava.BCLImplementation.System.ComponentModel;

namespace ScriptCoreLibJava.BCLImplementation.System.Web
{
	[Script(Implements = typeof(global::System.Web.HttpApplication))]
	internal class __HttpApplication : __IHttpAsyncHandler, __IHttpHandler, __IComponent, __IDisposable
	{
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
