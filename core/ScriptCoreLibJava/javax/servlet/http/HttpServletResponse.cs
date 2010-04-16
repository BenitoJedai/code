// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.servlet.http.HttpServletResponse

using ScriptCoreLib;
using javax.servlet;

namespace javax.servlet.http
{
	// http://java.sun.com/webservices/docs/1.6/api/javax/servlet/http/HttpServletResponse.html
	[Script(IsNative = true)]
	public interface HttpServletResponse : ServletResponse
	{
		/// <summary>
		/// Adds the specified cookie to the response.
		/// </summary>
		void addCookie(Cookie @cookie);

		/// <summary>
		/// Adds a response header with the given name and
		/// date-value.
		/// </summary>
		void addDateHeader(string @name, long @date);

		/// <summary>
		/// Adds a response header with the given name and value.
		/// </summary>
		void addHeader(string @name, string @value);

		/// <summary>
		/// Adds a response header with the given name and
		/// integer value.
		/// </summary>
		void addIntHeader(string @name, int @value);

		/// <summary>
		/// Returns a boolean indicating whether the named response header
		/// has already been set.
		/// </summary>
		bool containsHeader(string @name);

		/// <summary>
		/// <B>Deprecated.</B> <I>As of version 2.1, use
		/// encodeRedirectURL(String url) instead</I>
		/// </summary>
		string encodeRedirectUrl(string @url);

		/// <summary>
		/// Encodes the specified URL for use in the
		/// <code>sendRedirect</code> method or, if encoding is not needed,
		/// returns the URL unchanged.
		/// </summary>
		string encodeRedirectURL(string @url);

		/// <summary>
		/// <B>Deprecated.</B> <I>As of version 2.1, use encodeURL(String url) instead</I>
		/// </summary>
		string encodeUrl(string @url);

		/// <summary>
		/// Encodes the specified URL by including the session ID in it,
		/// or, if encoding is not needed, returns the URL unchanged.
		/// </summary>
		string encodeURL(string @url);

		/// <summary>
		/// Sends an error response to the client using the specified status
		/// code and clearing the buffer.
		/// </summary>
		void sendError(int @sc);

		/// <summary>
		/// Sends an error response to the client using the specified
		/// status.
		/// </summary>
		void sendError(int @sc, string @msg);

		/// <summary>
		/// Sends a temporary redirect response to the client using the
		/// specified redirect location URL.
		/// </summary>
		void sendRedirect(string @location);

		/// <summary>
		/// Sets a response header with the given name and
		/// date-value.
		/// </summary>
		void setDateHeader(string @name, long @date);

		/// <summary>
		/// Sets a response header with the given name and value.
		/// </summary>
		void setHeader(string @name, string @value);

		/// <summary>
		/// Sets a response header with the given name and
		/// integer value.
		/// </summary>
		void setIntHeader(string @name, int @value);

		/// <summary>
		/// Sets the status code for this response.
		/// </summary>
		void setStatus(int @sc);

		/// <summary>
		/// <B>Deprecated.</B> <I>As of version 2.1, due to ambiguous meaning of the
		/// message parameter. To set a status code
		/// use <code>setStatus(int)</code>, to send an error with a description
		/// use <code>sendError(int, String)</code>.
		/// 
		/// Sets the status code and message for this response.</I>
		/// </summary>
		void setStatus(int @sc, string @sm);

	}
}
