// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.servlet.RequestDispatcher

using ScriptCoreLib;
using javax.servlet;

namespace javax.servlet
{
	// http://java.sun.com/webservices/docs/1.6/api/javax/servlet/RequestDispatcher.html
	[Script(IsNative = true)]
	public interface RequestDispatcher
	{
		/// <summary>
		/// Forwards a request from
		/// a servlet to another resource (servlet, JSP file, or
		/// HTML file) on the server.
		/// </summary>
		void forward(ServletRequest @request, ServletResponse @response);

		/// <summary>
		/// Includes the content of a resource (servlet, JSP page,
		/// HTML file) in the response.
		/// </summary>
		void include(ServletRequest @request, ServletResponse @response);

	}
}
