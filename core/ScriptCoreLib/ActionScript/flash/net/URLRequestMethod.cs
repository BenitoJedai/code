using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.net
{
	// http://livedocs.adobe.com/flex/3/langref/flash/net/URLRequestMethod.html
	[Script(IsNative = true)]
	public class URLRequestMethod
	{
		#region Constants
		/// <summary>
		/// [static] Specifies that the URLRequest object is a GET.
		/// </summary>
		public static readonly string GET = "GET";

		/// <summary>
		/// [static] Specifies that the URLRequest object is a POST.
		/// </summary>
		public static readonly string POST = "POST";

		#endregion

	}
}
