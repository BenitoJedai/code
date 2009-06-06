using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace javax.servlet.http
{
	// http://java.sun.com/webservices/docs/1.6/api/javax/servlet/http/HttpServletResponse.html
	[Script(IsNative = true)]
	public interface HttpServletResponse : ServletResponse
	{
		/// <summary>
		/// Sends a temporary redirect response to the client using the specified redirect location URL.
		/// </summary>
		/// <param name="location"></param>
		void sendRedirect(string location);
          
	}
}
