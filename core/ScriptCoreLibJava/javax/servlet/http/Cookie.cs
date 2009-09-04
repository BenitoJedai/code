// This source code was generated for ScriptCoreLib
// javax.servlet.http.Cookie

using ScriptCoreLib;

namespace javax.servlet.http
{
	// http://java.sun.com/webservices/docs/1.6/api/javax/servlet/http/Cookie.html
	[Script(IsNative = true)]
	public class Cookie
	{
		/// <summary>
		/// Constructs a cookie with a specified name and value.
		/// </summary>
		public Cookie(string @name, string @value)
		{
		}

		/// <summary>
		/// Overrides the standard <code>java.lang.Object.clone</code>
		/// method to return a copy of this cookie.
		/// </summary>
		public object clone()
		{
			return default(object);
		}

		/// <summary>
		/// Returns the comment describing the purpose of this cookie, or
		/// <code>null</code> if the cookie has no comment.
		/// </summary>
		public string getComment()
		{
			return default(string);
		}

		/// <summary>
		/// Returns the domain name set for this cookie.
		/// </summary>
		public string getDomain()
		{
			return default(string);
		}

		/// <summary>
		/// Returns the maximum age of the cookie, specified in seconds,
		/// By default, <code>-1</code> indicating the cookie will persist
		/// until browser shutdown.
		/// </summary>
		public int getMaxAge()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the name of the cookie.
		/// </summary>
		public string getName()
		{
			return default(string);
		}

		/// <summary>
		/// Returns the path on the server
		/// to which the browser returns this cookie.
		/// </summary>
		public string getPath()
		{
			return default(string);
		}

		/// <summary>
		/// Returns <code>true</code> if the browser is sending cookies
		/// only over a secure protocol, or <code>false</code> if the
		/// browser can send cookies using any protocol.
		/// </summary>
		public bool getSecure()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the value of the cookie.
		/// </summary>
		public string getValue()
		{
			return default(string);
		}

		/// <summary>
		/// Returns the version of the protocol this cookie complies
		/// with.
		/// </summary>
		public int getVersion()
		{
			return default(int);
		}

		/// <summary>
		/// Specifies a comment that describes a cookie's purpose.
		/// </summary>
		public void setComment(string @purpose)
		{
		}

		/// <summary>
		/// Specifies the domain within which this cookie should be presented.
		/// </summary>
		public void setDomain(string @pattern)
		{
		}

		/// <summary>
		/// Sets the maximum age of the cookie in seconds.
		/// </summary>
		public void setMaxAge(int @expiry)
		{
		}

		/// <summary>
		/// Specifies a path for the cookie
		/// to which the client should return the cookie.
		/// </summary>
		public void setPath(string @uri)
		{
		}

		/// <summary>
		/// Indicates to the browser whether the cookie should only be sent
		/// using a secure protocol, such as HTTPS or SSL.
		/// </summary>
		public void setSecure(bool @flag)
		{
		}

		/// <summary>
		/// Assigns a new value to a cookie after the cookie is created.
		/// </summary>
		public void setValue(string @newValue)
		{
		}

		/// <summary>
		/// Sets the version of the cookie protocol this cookie complies
		/// with.
		/// </summary>
		public void setVersion(int @v)
		{
		}

	}
}
