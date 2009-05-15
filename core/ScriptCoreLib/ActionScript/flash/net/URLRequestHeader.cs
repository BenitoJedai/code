using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.net
{
	// http://livedocs.adobe.com/flex/3/langref/flash/net/URLRequestHeader.html
	[Script(IsNative = true)]
	public class URLRequestHeader
	{
		#region Properties
		/// <summary>
		/// An HTTP request header name (such as Content-Type or SOAPAction).
		/// </summary>
		public string name { get; set; }

		/// <summary>
		/// The value associated with the name property (such as text/plain).
		/// </summary>
		public string value { get; set; }

		#endregion


		#region Methods
		#endregion

		#region Constructors
		/// <summary>
		/// Creates a new URLRequestHeader object that encapsulates a single HTTP request header.
		/// </summary>
		public URLRequestHeader(string name, string value)
		{
		}

		/// <summary>
		/// Creates a new URLRequestHeader object that encapsulates a single HTTP request header.
		/// </summary>
		public URLRequestHeader(string name)
		{
		}

		/// <summary>
		/// Creates a new URLRequestHeader object that encapsulates a single HTTP request header.
		/// </summary>
		public URLRequestHeader()
		{
		}

		#endregion

	}
}
