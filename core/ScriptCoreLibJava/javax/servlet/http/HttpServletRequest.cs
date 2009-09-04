// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.servlet.http.HttpServletRequest

using ScriptCoreLib;
using javax.servlet;
using javax.servlet.http;

namespace javax.servlet.http
{
	// http://java.sun.com/webservices/docs/1.6/api/javax/servlet/http/HttpServletRequest.html
	[Script(IsNative = true)]
	public interface HttpServletRequest : ServletRequest
	{
		/// <summary>
		/// Returns the name of the authentication scheme used to protect
		/// the servlet.
		/// </summary>
		string getAuthType();

		/// <summary>
		/// Returns the portion of the request URI that indicates the context
		/// of the request.
		/// </summary>
		string getContextPath();

		/// <summary>
		/// Returns an array containing all of the <code>Cookie</code>
		/// objects the client sent with this request.
		/// </summary>
		Cookie[] getCookies();

		/// <summary>
		/// Returns the value of the specified request header
		/// as a <code>long</code> value that represents a
		/// <code>Date</code> object.
		/// </summary>
		long getDateHeader(string @name);

		/// <summary>
		/// Returns the value of the specified request header
		/// as a <code>String</code>.
		/// </summary>
		string getHeader(string @name);

		/// <summary>
		/// Returns an enumeration of all the header names
		/// this request contains.
		/// </summary>
		java.util.Enumeration getHeaderNames();

		/// <summary>
		/// Returns all the values of the specified request header
		/// as an <code>Enumeration</code> of <code>String</code> objects.
		/// </summary>
		java.util.Enumeration getHeaders(string @name);

		/// <summary>
		/// Returns the value of the specified request header
		/// as an <code>int</code>.
		/// </summary>
		int getIntHeader(string @name);

		/// <summary>
		/// Returns the name of the HTTP method with which this
		/// request was made, for example, GET, POST, or PUT.
		/// </summary>
		string getMethod();

		/// <summary>
		/// Returns any extra path information associated with
		/// the URL the client sent when it made this request.
		/// </summary>
		string getPathInfo();

		/// <summary>
		/// Returns any extra path information after the servlet name
		/// but before the query string, and translates it to a real
		/// path.
		/// </summary>
		string getPathTranslated();

		/// <summary>
		/// Returns the query string that is contained in the request
		/// URL after the path.
		/// </summary>
		string getQueryString();

		/// <summary>
		/// Returns the login of the user making this request, if the
		/// user has been authenticated, or <code>null</code> if the user
		/// has not been authenticated.
		/// </summary>
		string getRemoteUser();

		/// <summary>
		/// Returns the session ID specified by the client.
		/// </summary>
		string getRequestedSessionId();

		/// <summary>
		/// Returns the part of this request's URL from the protocol
		/// name up to the query string in the first line of the HTTP request.
		/// </summary>
		string getRequestURI();

		/// <summary>
		/// Reconstructs the URL the client used to make the request.
		/// </summary>
		java.lang.StringBuffer getRequestURL();

		/// <summary>
		/// Returns the part of this request's URL that calls
		/// the servlet.
		/// </summary>
		string getServletPath();

		/// <summary>
		/// Returns the current session associated with this request,
		/// or if the request does not have a session, creates one.
		/// </summary>
		HttpSession getSession();

		/// <summary>
		/// Returns the current <code>HttpSession</code>
		/// associated with this request or, if there is no
		/// current session and <code>create</code> is true, returns
		/// a new session.
		/// </summary>
		HttpSession getSession(bool @create);

		/// <summary>
		/// Returns a <code>java.security.Principal</code> object containing
		/// the name of the current authenticated user.
		/// </summary>
		java.security.Principal getUserPrincipal();

		/// <summary>
		/// Checks whether the requested session ID came in as a cookie.
		/// </summary>
		bool isRequestedSessionIdFromCookie();

		/// <summary>
		/// <B>Deprecated.</B> <I>As of Version 2.1 of the Java Servlet
		/// API, use <A HREF="../../../javax/servlet/http/HttpServletRequest.html#isRequestedSessionIdFromURL()"><CODE>isRequestedSessionIdFromURL()</CODE></A>
		/// instead.</I>
		/// </summary>
		bool isRequestedSessionIdFromUrl();

		/// <summary>
		/// Checks whether the requested session ID came in as part of the
		/// request URL.
		/// </summary>
		bool isRequestedSessionIdFromURL();

		/// <summary>
		/// Checks whether the requested session ID is still valid.
		/// </summary>
		bool isRequestedSessionIdValid();

		/// <summary>
		/// Returns a boolean indicating whether the authenticated user is included
		/// in the specified logical "role".
		/// </summary>
		bool isUserInRole(string @role);

	}
}
