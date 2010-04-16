using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ScriptCoreLib.Ultra.WebService
{
	/// <summary>
	/// This type is used to serve custom content from the web server.
	/// </summary>
	public class WebServiceHandler
	{
		public HttpContext Context;

		public Action CompleteRequest;


		public Action Diagnostics;
		public Action Default;
		public Action Redirect;

		public WebServiceScriptApplication[] Applications;

		public bool IsDefaultPath
		{
			get
			{
				var e = this.Context.Request.Path;

				return InternalIsDefaultPath(e);
			}
		}

		internal static bool InternalIsDefaultPath(string e)
		{
			if (e == "/")
				return true;

			if (e == "/default.htm")
				return true;

			if (e == "/default.aspx")
				return true;

			return false;
		}

	}
}
