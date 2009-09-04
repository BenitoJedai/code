// This source code was generated for ScriptCoreLib
// javax.servlet.http.HttpSession

using ScriptCoreLib;
using javax.servlet;
using javax.servlet.http;

namespace javax.servlet.http
{
	// http://java.sun.com/webservices/docs/1.6/api/javax/servlet/http/HttpSession.html
	[Script(IsNative = true)]
	public interface HttpSession
	{
		/// <summary>
		/// Returns the object bound with the specified name in this session, or
		/// <code>null</code> if no object is bound under the name.
		/// </summary>
		object getAttribute(string @name);

		/// <summary>
		/// Returns an <code>Enumeration</code> of <code>String</code> objects
		/// containing the names of all the objects bound to this session.
		/// </summary>
		java.util.Enumeration getAttributeNames();

		/// <summary>
		/// Returns the time when this session was created, measured
		/// in milliseconds since midnight January 1, 1970 GMT.
		/// </summary>
		long getCreationTime();

		/// <summary>
		/// Returns a string containing the unique identifier assigned
		/// to this session.
		/// </summary>
		string getId();

		/// <summary>
		/// Returns the last time the client sent a request associated with
		/// this session, as the number of milliseconds since midnight
		/// January 1, 1970 GMT, and marked by the time the container received the request.
		/// </summary>
		long getLastAccessedTime();

		/// <summary>
		/// Returns the maximum time interval, in seconds, that
		/// the servlet container will keep this session open between
		/// client accesses.
		/// </summary>
		int getMaxInactiveInterval();

		/// <summary>
		/// Returns the ServletContext to which this session belongs.
		/// </summary>
		ServletContext getServletContext();

		/// <summary>
		/// <B>Deprecated.</B> <I>As of Version 2.1, this method is
		/// deprecated and has no replacement.
		/// It will be removed in a future
		/// version of the Java Servlet API.</I>
		/// </summary>
		HttpSessionContext getSessionContext();

		/// <summary>
		/// <B>Deprecated.</B> <I>As of Version 2.2, this method is
		/// replaced by <A HREF="../../../javax/servlet/http/HttpSession.html#getAttribute(java.lang.String)"><CODE>getAttribute(java.lang.String)</CODE></A>.</I>
		/// </summary>
		object getValue(string @name);

		/// <summary>
		/// <B>Deprecated.</B> <I>As of Version 2.2, this method is
		/// replaced by <A HREF="../../../javax/servlet/http/HttpSession.html#getAttributeNames()"><CODE>getAttributeNames()</CODE></A></I>
		/// </summary>
		java.lang.String[] getValueNames();

		/// <summary>
		/// Invalidates this session then unbinds any objects bound
		/// to it.
		/// </summary>
		void invalidate();

		/// <summary>
		/// Returns <code>true</code> if the client does not yet know about the
		/// session or if the client chooses not to join the session.
		/// </summary>
		bool isNew();

		/// <summary>
		/// <B>Deprecated.</B> <I>As of Version 2.2, this method is
		/// replaced by <A HREF="../../../javax/servlet/http/HttpSession.html#setAttribute(java.lang.String, java.lang.Object)"><CODE>setAttribute(java.lang.String, java.lang.Object)</CODE></A></I>
		/// </summary>
		void putValue(string @name, object @value);

		/// <summary>
		/// Removes the object bound with the specified name from
		/// this session.
		/// </summary>
		void removeAttribute(string @name);

		/// <summary>
		/// <B>Deprecated.</B> <I>As of Version 2.2, this method is
		/// replaced by <A HREF="../../../javax/servlet/http/HttpSession.html#removeAttribute(java.lang.String)"><CODE>removeAttribute(java.lang.String)</CODE></A></I>
		/// </summary>
		void removeValue(string @name);

		/// <summary>
		/// Binds an object to this session, using the name specified.
		/// </summary>
		void setAttribute(string @name, object @value);

		/// <summary>
		/// Specifies the time, in seconds, between client requests before the
		/// servlet container will invalidate this session.
		/// </summary>
		void setMaxInactiveInterval(int @interval);

	}
}
