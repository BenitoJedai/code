using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace OrcasMetaWebApplication1
{
	public class Global : System.Web.HttpApplication
	{
		// later we may want to expose Ultra Documents
		// including webservice proxy

		public void Application_BeginRequest(object sender, EventArgs e)
		{
			this.Response.ContentType = "text/plain";
			this.Response.Write("Hello World");
			this.CompleteRequest();
		}
	}
}