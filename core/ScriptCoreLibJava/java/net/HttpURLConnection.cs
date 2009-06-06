using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace java.net
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/net/HttpURLConnection.html
	[Script(IsNative = true)]
	public class HttpURLConnection : URLConnection
	{
		/// <summary>
		/// Sets whether HTTP redirects (requests with response code 3xx) should be automatically followed by this HttpURLConnection instance.
		/// </summary>
		/// <param name="followRedirects"></param>
		public void setInstanceFollowRedirects(bool followRedirects)
		{
		}

		/// <summary>
		/// Set the method for the URL request, one of: GET POST HEAD OPTIONS PUT DELETE TRACE are legal, subject to protocol restrictions.
		/// </summary>
		/// <param name="method"></param>
		public void setRequestMethod(string method)
		{
		}
          
	}
}
