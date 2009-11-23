// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.servlet.http.HttpServlet

using ScriptCoreLib;
using javax.servlet;

namespace javax.servlet.http
{
	// http://java.sun.com/webservices/docs/1.6/api/javax/servlet/http/HttpServlet.html
	[Script(IsNative = true)]
	public abstract class HttpServlet : GenericServlet
	{
		/// <summary>
		/// Does nothing, because this is an abstract class.
		/// </summary>
		public HttpServlet()
		{
		}

		/// <summary>
		/// Called by the server (via the <code>service</code> method)
		/// to allow a servlet to handle a DELETE request.
		/// </summary>
		protected void doDelete(HttpServletRequest @req, HttpServletResponse @resp)
		{
		}

		/// <summary>
		/// Called by the server (via the <code>service</code> method) to
		/// allow a servlet to handle a GET request.
		/// </summary>
		protected virtual void doGet(HttpServletRequest @req, HttpServletResponse @resp)
		{
		}

		/// <summary>
		/// Receives an HTTP HEAD request from the protected
		/// <code>service</code> method and handles the
		/// request.
		/// </summary>
		protected virtual void doHead(HttpServletRequest @req, HttpServletResponse @resp)
		{
		}

		/// <summary>
		/// Called by the server (via the <code>service</code> method)
		/// to allow a servlet to handle a OPTIONS request.
		/// </summary>
		protected void doOptions(HttpServletRequest @req, HttpServletResponse @resp)
		{
		}

		/// <summary>
		/// Called by the server (via the <code>service</code> method)
		/// to allow a servlet to handle a POST request.
		/// </summary>
		protected virtual void doPost(HttpServletRequest @req, HttpServletResponse @resp)
		{
		}

		/// <summary>
		/// Called by the server (via the <code>service</code> method)
		/// to allow a servlet to handle a PUT request.
		/// </summary>
		protected void doPut(HttpServletRequest @req, HttpServletResponse @resp)
		{
		}

		/// <summary>
		/// Called by the server (via the <code>service</code> method)
		/// to allow a servlet to handle a TRACE request.
		/// </summary>
		protected void doTrace(HttpServletRequest @req, HttpServletResponse @resp)
		{
		}

		/// <summary>
		/// Returns the time the <code>HttpServletRequest</code>
		/// object was last modified,
		/// in milliseconds since midnight January 1, 1970 GMT.
		/// </summary>
		protected long getLastModified(HttpServletRequest @req)
		{
			return default(long);
		}

		/// <summary>
		/// Receives standard HTTP requests from the public
		/// <code>service</code> method and dispatches
		/// them to the <code>do</code><i>XXX</i> methods defined in
		/// this class.
		/// </summary>
		protected void service(HttpServletRequest @req, HttpServletResponse @resp)
		{
		}

		/// <summary>
		/// Dispatches client requests to the protected
		/// <code>service</code> method.
		/// </summary>
		public override void service(ServletRequest @req, ServletResponse @res)
		{
		}

	}
}
