using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace javax.servlet.http
{
	// http://java.sun.com/webservices/docs/1.6/api/javax/servlet/http/HttpServletRequest.html
	[Script(IsNative = true)]
	public interface HttpServletRequest
	{
		/// <summary>
		/// Returns any extra path information associated with the URL the client sent when it made this request.
		/// </summary>
		/// <returns></returns>
		string getPathInfo();


		/// <summary>
		/// Returns the query string that is contained in the request URL after the path.
		/// </summary>
		/// <returns></returns>
		string getQueryString();
          

		/// <summary>
		/// Returns the part of this request's URL that calls the servlet.
		/// </summary>
		/// <returns></returns>
		string getServletPath();

		/// <summary>
		/// Returns the part of this request's URL from the protocol name up to the query string in the first line of the HTTP request.
		/// </summary>
		/// <returns></returns>
		string getRequestURI();
          
		/// <summary>
		/// Reconstructs the URL the client used to make the request.
		/// </summary>
		/// <returns></returns>
		java.lang.StringBuffer getRequestURL();
          
	}
}
