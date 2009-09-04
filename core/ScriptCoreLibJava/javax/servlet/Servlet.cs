// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.servlet.Servlet

using ScriptCoreLib;
using javax.servlet;

namespace javax.servlet
{
	// http://java.sun.com/webservices/docs/1.6/api/javax/servlet/Servlet.html
	[Script(IsNative = true)]
	public interface Servlet
	{
		/// <summary>
		/// Called by the servlet container to indicate to a servlet that the
		/// servlet is being taken out of service.
		/// </summary>
		void destroy();

		/// <summary>
		/// Returns a <A HREF="../../javax/servlet/ServletConfig.html" title="interface in javax.servlet"><CODE>ServletConfig</CODE></A> object, which contains
		/// initialization and startup parameters for this servlet.
		/// </summary>
		ServletConfig getServletConfig();

		/// <summary>
		/// Returns information about the servlet, such
		/// as author, version, and copyright.
		/// </summary>
		string getServletInfo();

		/// <summary>
		/// Called by the servlet container to indicate to a servlet that the
		/// servlet is being placed into service.
		/// </summary>
		void init(ServletConfig @config);

		/// <summary>
		/// Called by the servlet container to allow the servlet to respond to
		/// a request.
		/// </summary>
		void service(ServletRequest @req, ServletResponse @res);

	}
}
