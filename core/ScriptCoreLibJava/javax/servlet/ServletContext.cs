// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.servlet.ServletContext

using ScriptCoreLib;
using javax.servlet;

namespace javax.servlet
{
	// http://java.sun.com/webservices/docs/1.6/api/javax/servlet/ServletContext.html
	[Script(IsNative = true)]
	public interface ServletContext
	{
		/// <summary>
		/// Returns the servlet container attribute with the given name,
		/// or <code>null</code> if there is no attribute by that name.
		/// </summary>
		object getAttribute(string @name);

		/// <summary>
		/// Returns an <code>Enumeration</code> containing the
		/// attribute names available
		/// within this servlet context.
		/// </summary>
		java.util.Enumeration getAttributeNames();

		/// <summary>
		/// Returns a <code>ServletContext</code> object that
		/// corresponds to a specified URL on the server.
		/// </summary>
		ServletContext getContext(string @uripath);

		/// <summary>
		/// Returns a <code>String</code> containing the value of the named
		/// context-wide initialization parameter, or <code>null</code> if the
		/// parameter does not exist.
		/// </summary>
		string getInitParameter(string @name);

		/// <summary>
		/// Returns the names of the context's initialization parameters as an
		/// <code>Enumeration</code> of <code>String</code> objects, or an
		/// empty <code>Enumeration</code> if the context has no initialization
		/// parameters.
		/// </summary>
		java.util.Enumeration getInitParameterNames();

		/// <summary>
		/// Returns the major version of the Java Servlet API that this
		/// servlet container supports.
		/// </summary>
		int getMajorVersion();

		/// <summary>
		/// Returns the MIME type of the specified file, or <code>null</code> if
		/// the MIME type is not known.
		/// </summary>
		string getMimeType(string @file);

		/// <summary>
		/// Returns the minor version of the Servlet API that this
		/// servlet container supports.
		/// </summary>
		int getMinorVersion();

		/// <summary>
		/// Returns a <A HREF="../../javax/servlet/RequestDispatcher.html" title="interface in javax.servlet"><CODE>RequestDispatcher</CODE></A> object that acts
		/// as a wrapper for the named servlet.
		/// </summary>
		RequestDispatcher getNamedDispatcher(string @name);

		/// <summary>
		/// Returns a <code>String</code> containing the real path
		/// for a given virtual path.
		/// </summary>
		string getRealPath(string @path);

		/// <summary>
		/// Returns a <A HREF="../../javax/servlet/RequestDispatcher.html" title="interface in javax.servlet"><CODE>RequestDispatcher</CODE></A> object that acts
		/// as a wrapper for the resource located at the given path.
		/// </summary>
		RequestDispatcher getRequestDispatcher(string @path);

		/// <summary>
		/// Returns a URL to the resource that is mapped to a specified
		/// path.
		/// </summary>
		java.net.URL getResource(string @path);

		/// <summary>
		/// Returns the resource located at the named path as
		/// an <code>InputStream</code> object.
		/// </summary>
		java.io.InputStream getResourceAsStream(string @path);

		/// <summary>
		/// Returns a directory-like listing of all the paths to resources within the web application whose longest sub-path
		/// matches the supplied path argument.
		/// </summary>
		java.util.Set getResourcePaths(string @path);

		/// <summary>
		/// Returns the name and version of the servlet container on which
		/// the servlet is running.
		/// </summary>
		string getServerInfo();

		/// <summary>
		/// <B>Deprecated.</B> <I>As of Java Servlet API 2.1, with no direct replacement.
		/// 
		/// <p>This method was originally defined to retrieve a servlet
		/// from a <code>ServletContext</code>. In this version, this method
		/// always returns <code>null</code> and remains only to preserve
		/// binary compatibility. This method will be permanently removed
		/// in a future version of the Java Servlet API.
		/// 
		/// <p>In lieu of this method, servlets can share information using the
		/// <code>ServletContext</code> class and can perform shared business logic
		/// by invoking methods on common non-servlet classes.</I>
		/// </summary>
		Servlet getServlet(string @name);

		/// <summary>
		/// Returns the name of this web application correponding to this ServletContext as specified in the deployment
		/// descriptor for this web application by the display-name element.
		/// </summary>
		string getServletContextName();

		/// <summary>
		/// <B>Deprecated.</B> <I>As of Java Servlet API 2.1, with no replacement.
		/// 
		/// <p>This method was originally defined to return an
		/// <code>Enumeration</code>
		/// of all the servlet names known to this context. In this version,
		/// this method always returns an empty <code>Enumeration</code> and
		/// remains only to preserve binary compatibility. This method will
		/// be permanently removed in a future version of the Java Servlet API.</I>
		/// </summary>
		java.util.Enumeration getServletNames();

		/// <summary>
		/// <B>Deprecated.</B> <I>As of Java Servlet API 2.0, with no replacement.
		/// 
		/// <p>This method was originally defined to return an <code>Enumeration</code>
		/// of all the servlets known to this servlet context. In this
		/// version, this method always returns an empty enumeration and
		/// remains only to preserve binary compatibility. This method
		/// will be permanently removed in a future version of the Java
		/// Servlet API.</I>
		/// </summary>
		java.util.Enumeration getServlets();

		/// <summary>
		/// <B>Deprecated.</B> <I>As of Java Servlet API 2.1, use
		/// <A HREF="../../javax/servlet/ServletContext.html#log(java.lang.String, java.lang.Throwable)"><CODE>log(String message, Throwable throwable)</CODE></A>
		/// instead.
		/// 
		/// <p>This method was originally defined to write an
		/// exception's stack trace and an explanatory error message
		/// to the servlet log file.</I>
		/// </summary>
		void log(java.lang.Exception @exception, string @msg);

		/// <summary>
		/// Writes the specified message to a servlet log file, usually
		/// an event log.
		/// </summary>
		void log(string @msg);

		/// <summary>
		/// Writes an explanatory message and a stack trace
		/// for a given <code>Throwable</code> exception
		/// to the servlet log file.
		/// </summary>
		void log(string @message, java.lang.Throwable @throwable);

		/// <summary>
		/// Removes the attribute with the given name from
		/// the servlet context.
		/// </summary>
		void removeAttribute(string @name);

		/// <summary>
		/// Binds an object to a given attribute name in this servlet context.
		/// </summary>
		void setAttribute(string @name, object @object);

	}
}
