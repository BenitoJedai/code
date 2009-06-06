using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace javax.servlet.http
{
	// http://java.sun.com/webservices/docs/1.6/api/javax/servlet/http/HttpServlet.html
	[Script(IsNative = true)]
	public class HttpServlet
	{
		/// <summary>
		/// Called by the server (via the service method) to allow a servlet to handle a GET request.
		/// </summary>
		/// <param name="req"></param>
		/// <param name="resp"></param>
		protected virtual void doGet(HttpServletRequest req, HttpServletResponse resp)
		{
		}

}
}
