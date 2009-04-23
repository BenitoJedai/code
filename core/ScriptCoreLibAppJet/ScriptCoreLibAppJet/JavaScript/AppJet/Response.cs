using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibAppJet.JavaScript.AppJet
{
	// http://appjet.com/docs/librefbrowser?page=response
	[Script(HasNoPrototype = true)]
	public class Response
	{
		/// <summary>
		/// Sets the status code in the HTTP response.
		/// </summary>
		/// <param name="newCode"></param>
		public void setStatusCode(int newCode)
		{
		}

		/// <summary>
		/// Sets the Content-Type header of the response. If the content-type includes a charset, that charset is used to send the response.
		/// </summary>
		/// <param name="e"></param>
		public void setContentType(string contentType)
		{
		}

		/// <summary>
		/// Tells the client to cache the page. By default, clients are told to not never cache pages. (To send no caching-related headers at all, pass undefined.)
		/// </summary>
		/// <param name="cacheable"></param>
		public void setCacheable(bool cacheable)
		{
		}

		public void redirect(string e)
		{

		}

		/// <summary>
		/// Low-level hook for writing raw byte data to the response. Especially useful for writing the result of a wget of image data, or writing an uploaded file.
		/// </summary>
		/// <param name="data"></param>
		public void writeBytes(string data)
		{

		}
	}
}
