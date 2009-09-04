// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.servlet.ServletRequest

using ScriptCoreLib;
using javax.servlet;

namespace javax.servlet
{
	// http://java.sun.com/webservices/docs/1.6/api/javax/servlet/ServletRequest.html
	[Script(IsNative = true)]
	public interface ServletRequest
	{
		/// <summary>
		/// Returns the value of the named attribute as an <code>Object</code>,
		/// or <code>null</code> if no attribute of the given name exists.
		/// </summary>
		object getAttribute(string @name);

		/// <summary>
		/// Returns an <code>Enumeration</code> containing the
		/// names of the attributes available to this request.
		/// </summary>
		java.util.Enumeration getAttributeNames();

		/// <summary>
		/// Returns the name of the character encoding used in the body of this
		/// request.
		/// </summary>
		string getCharacterEncoding();

		/// <summary>
		/// Returns the length, in bytes, of the request body
		/// and made available by the input stream, or -1 if the
		/// length is not known.
		/// </summary>
		int getContentLength();

		/// <summary>
		/// Returns the MIME type of the body of the request, or
		/// <code>null</code> if the type is not known.
		/// </summary>
		string getContentType();

		/// <summary>
		/// Retrieves the body of the request as binary data using
		/// a <A HREF="../../javax/servlet/ServletInputStream.html" title="class in javax.servlet"><CODE>ServletInputStream</CODE></A>.
		/// </summary>
		ServletInputStream getInputStream();

		/// <summary>
		/// Returns the Internet Protocol (IP) address of the interface on
		/// which the request  was received.
		/// </summary>
		string getLocalAddr();

		/// <summary>
		/// Returns the preferred <code>Locale</code> that the client will
		/// accept content in, based on the Accept-Language header.
		/// </summary>
		java.util.Locale getLocale();

		/// <summary>
		/// Returns an <code>Enumeration</code> of <code>Locale</code> objects
		/// indicating, in decreasing order starting with the preferred locale, the
		/// locales that are acceptable to the client based on the Accept-Language
		/// header.
		/// </summary>
		java.util.Enumeration getLocales();

		/// <summary>
		/// Returns the host name of the Internet Protocol (IP) interface on
		/// which the request was received.
		/// </summary>
		string getLocalName();

		/// <summary>
		/// Returns the Internet Protocol (IP) port number of the interface
		/// on which the request was received.
		/// </summary>
		int getLocalPort();

		/// <summary>
		/// Returns the value of a request parameter as a <code>String</code>,
		/// or <code>null</code> if the parameter does not exist.
		/// </summary>
		string getParameter(string @name);

		/// <summary>
		/// Returns a java.util.Map of the parameters of this request.
		/// </summary>
		java.util.Map getParameterMap();

		/// <summary>
		/// Returns an <code>Enumeration</code> of <code>String</code>
		/// objects containing the names of the parameters contained
		/// in this request.
		/// </summary>
		java.util.Enumeration getParameterNames();

		/// <summary>
		/// Returns an array of <code>String</code> objects containing
		/// all of the values the given request parameter has, or
		/// <code>null</code> if the parameter does not exist.
		/// </summary>
		java.lang.String[] getParameterValues(string @name);

		/// <summary>
		/// Returns the name and version of the protocol the request uses
		/// in the form <i>protocol/majorVersion.minorVersion</i>, for
		/// example, HTTP/1.1.
		/// </summary>
		string getProtocol();

		/// <summary>
		/// Retrieves the body of the request as character data using
		/// a <code>BufferedReader</code>.
		/// </summary>
		java.io.BufferedReader getReader();

		/// <summary>
		/// <B>Deprecated.</B> <I>As of Version 2.1 of the Java Servlet API,
		/// use <A HREF="../../javax/servlet/ServletContext.html#getRealPath(java.lang.String)"><CODE>ServletContext.getRealPath(java.lang.String)</CODE></A> instead.</I>
		/// </summary>
		string getRealPath(string @path);

		/// <summary>
		/// Returns the Internet Protocol (IP) address of the client
		/// or last proxy that sent the request.
		/// </summary>
		string getRemoteAddr();

		/// <summary>
		/// Returns the fully qualified name of the client
		/// or the last proxy that sent the request.
		/// </summary>
		string getRemoteHost();

		/// <summary>
		/// Returns the Internet Protocol (IP) source port of the client
		/// or last proxy that sent the request.
		/// </summary>
		int getRemotePort();

		/// <summary>
		/// Returns a <A HREF="../../javax/servlet/RequestDispatcher.html" title="interface in javax.servlet"><CODE>RequestDispatcher</CODE></A> object that acts as a wrapper for
		/// the resource located at the given path.
		/// </summary>
		RequestDispatcher getRequestDispatcher(string @path);

		/// <summary>
		/// Returns the name of the scheme used to make this request,
		/// for example,
		/// <code>http</code>, <code>https</code>, or <code>ftp</code>.
		/// </summary>
		string getScheme();

		/// <summary>
		/// Returns the host name of the server to which the request was sent.
		/// </summary>
		string getServerName();

		/// <summary>
		/// Returns the port number to which the request was sent.
		/// </summary>
		int getServerPort();

		/// <summary>
		/// Returns a boolean indicating whether this request was made using a
		/// secure channel, such as HTTPS.
		/// </summary>
		bool isSecure();

		/// <summary>
		/// Removes an attribute from this request.
		/// </summary>
		void removeAttribute(string @name);

		/// <summary>
		/// Stores an attribute in this request.
		/// </summary>
		void setAttribute(string @name, object @o);

		/// <summary>
		/// Overrides the name of the character encoding used in the body of this
		/// request.
		/// </summary>
		void setCharacterEncoding(string @env);

	}
}
