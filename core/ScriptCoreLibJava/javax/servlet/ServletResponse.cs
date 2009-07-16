using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace javax.servlet
{
	// http://java.sun.com/webservices/docs/1.6/api/javax/servlet/ServletResponse.html
	[Script(IsNative = true)]
	public interface ServletResponse
	{
		/// <summary>
		/// Sets the content type of the response being sent to the client, if the response has not been committed yet.
		/// </summary>
		/// <param name="type"></param>
		void setContentType(string type);


		/// <summary>
		/// Returns a PrintWriter object that can send character text to the client.
		/// </summary>
		/// <returns></returns>
		java.io.PrintWriter getWriter();
          
		/// <summary>
		/// Returns a ServletOutputStream suitable for writing binary data in the response.
		/// </summary>
		/// <returns></returns>
		 ServletOutputStream 	getOutputStream();
          
	}
}
