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

		public void Application_BeginRequest(object sender, EventArgs e)
		{
			if (this.Request.Path.EndsWith(".ico"))
				return;

			if (this.Request.Path.EndsWith(".png"))
				return;

			if (this.Request.Path.EndsWith(".swf"))
				return;

			if (this.Request.Path.EndsWith(".asmx"))
				return;

			if (this.Request.Path.EndsWith(".htm"))
				return;

			if (this.Request.Path.EndsWith(".ashx"))
				return;

			if (this.Request.Path == "/examples")
			{
				this.Response.Redirect("/examples.htm");
				return;
			}

		

			this.Response.StatusCode = 200;
			this.Response.ContentType = "text/html; charset=utf-8";


			this.Response.Write(Pages.jsc_solutions.Static.DocumentHTML.Replace(
				"<code></code>", 
				"<code>" + this.Request.Path + "</code>"
				)
			);

			//this.Response.Write("<a href='/jsc'>jsc</a> howdy! @ " + this.Request.Path);
			//this.Response.Write("<a href='/jsc'>jsc</a> howdy! @ " + this.Request.Path + "?" + this.Request.QueryString);

			this.CompleteRequest();
		}
	}
}