// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.servlet.ServletConfig

using ScriptCoreLib;
using javax.servlet;

namespace javax.servlet
{
	// http://java.sun.com/webservices/docs/1.6/api/javax/servlet/ServletConfig.html
	[Script(IsNative = true)]
	public interface ServletConfig
	{
		/// <summary>
		/// Returns a <code>String</code> containing the value of the
		/// named initialization parameter, or <code>null</code> if
		/// the parameter does not exist.
		/// </summary>
		string getInitParameter(string @name);

		/// <summary>
		/// Returns the names of the servlet's initialization parameters
		/// as an <code>Enumeration</code> of <code>String</code> objects,
		/// or an empty <code>Enumeration</code> if the servlet has
		/// no initialization parameters.
		/// </summary>
		java.util.Enumeration getInitParameterNames();

		/// <summary>
		/// Returns a reference to the <A HREF="../../javax/servlet/ServletContext.html" title="interface in javax.servlet"><CODE>ServletContext</CODE></A> in which the caller
		/// is executing.
		/// </summary>
		ServletContext getServletContext();

		/// <summary>
		/// Returns the name of this servlet instance.
		/// </summary>
		string getServletName();

	}
}
