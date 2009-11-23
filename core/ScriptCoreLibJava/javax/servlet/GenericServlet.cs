// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.servlet.GenericServlet

using ScriptCoreLib;

namespace javax.servlet
{
	// http://java.sun.com/webservices/docs/1.6/api/javax/servlet/GenericServlet.html
	[Script(IsNative = true)]
	public abstract class GenericServlet
	{
		/// <summary>
		/// Does nothing.
		/// </summary>
		public GenericServlet()
		{
		}

		/// <summary>
		/// Called by the servlet container to indicate to a servlet that the
		/// servlet is being taken out of service.
		/// </summary>
		public void destroy()
		{
		}

		/// <summary>
		/// Returns a <code>String</code> containing the value of the named
		/// initialization parameter, or <code>null</code> if the parameter does
		/// not exist.
		/// </summary>
		public string getInitParameter(string @name)
		{
			return default(string);
		}

		/// <summary>
		/// Returns the names of the servlet's initialization parameters
		/// as an <code>Enumeration</code> of <code>String</code> objects,
		/// or an empty <code>Enumeration</code> if the servlet has no
		/// initialization parameters.
		/// </summary>
		public java.util.Enumeration getInitParameterNames()
		{
			return default(java.util.Enumeration);
		}

		/// <summary>
		/// Returns this servlet's <A HREF="../../javax/servlet/ServletConfig.html" title="interface in javax.servlet"><CODE>ServletConfig</CODE></A> object.
		/// </summary>
		public ServletConfig getServletConfig()
		{
			return default(ServletConfig);
		}

		/// <summary>
		/// Returns a reference to the <A HREF="../../javax/servlet/ServletContext.html" title="interface in javax.servlet"><CODE>ServletContext</CODE></A> in which this servlet
		/// is running.
		/// </summary>
		public ServletContext getServletContext()
		{
			return default(ServletContext);
		}

		/// <summary>
		/// Returns information about the servlet, such as
		/// author, version, and copyright.
		/// </summary>
		public string getServletInfo()
		{
			return default(string);
		}

		/// <summary>
		/// Returns the name of this servlet instance.
		/// </summary>
		public string getServletName()
		{
			return default(string);
		}

		/// <summary>
		/// A convenience method which can be overridden so that there's no need
		/// to call <code>super.init(config)</code>.
		/// </summary>
		public void init()
		{
		}

		/// <summary>
		/// Called by the servlet container to indicate to a servlet that the
		/// servlet is being placed into service.
		/// </summary>
		public void init(ServletConfig @config)
		{
		}

		/// <summary>
		/// Writes the specified message to a servlet log file, prepended by the
		/// servlet's name.
		/// </summary>
		public void log(string @msg)
		{
		}

		/// <summary>
		/// Writes an explanatory message and a stack trace
		/// for a given <code>Throwable</code> exception
		/// to the servlet log file, prepended by the servlet's name.
		/// </summary>
		public void log(string @message, java.lang.Throwable @t)
		{
		}

		/// <summary>
		/// Called by the servlet container to allow the servlet to respond to
		/// a request.
		/// </summary>
		abstract public void service(ServletRequest @req, ServletResponse @res);

	}
}
