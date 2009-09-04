// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.servlet.http.HttpSessionContext

using ScriptCoreLib;
using javax.servlet.http;

namespace javax.servlet.http
{
	// http://java.sun.com/webservices/docs/1.6/api/javax/servlet/http/HttpSessionContext.html
	[Script(IsNative = true)]
	public interface HttpSessionContext
	{
		/// <summary>
		/// <B>Deprecated.</B> <I>As of Java Servlet API 2.1 with
		/// no replacement. This method must return
		/// an empty <code>Enumeration</code> and will be removed
		/// in a future version of this API.</I>
		/// </summary>
		java.util.Enumeration getIds();

		/// <summary>
		/// <B>Deprecated.</B> <I>As of Java Servlet API 2.1 with
		/// no replacement. This method must
		/// return null and will be removed in
		/// a future version of this API.</I>
		/// </summary>
		HttpSession getSession(string @sessionId);

	}
}
