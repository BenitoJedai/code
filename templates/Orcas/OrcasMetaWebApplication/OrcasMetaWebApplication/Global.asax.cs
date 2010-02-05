using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace OrcasMetaWebApplication
{
	public class Global : System.Web.HttpApplication
	{
		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			this.Response.Write("hi@ " + this.Request.Path + "?" + this.Request.QueryString);
			this.Response.StatusCode = 200;
			this.Response.ContentType = "text/plain";

			this.Response.End();
		}
	}
}