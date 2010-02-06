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
		// later we may want to expose Ultra Documents
		// including webservice proxy

		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			if (this.Request.Path != "/default.aspx")
			{
				if (this.Request.Path.EndsWith(".asmx"))
					return;

				if (this.Request.Path.EndsWith(".htm"))
					return;

				if (this.Request.Path.EndsWith(".ashx"))
					return;
			}

			
			this.Response.Write("<a href='/jsc'>jsc</a> howdy! @ " + this.Request.Path + "?" + this.Request.QueryString);
			this.Response.StatusCode = 200;
			this.Response.ContentType = "text/html";

			this.CompleteRequest();
		}
	}
}