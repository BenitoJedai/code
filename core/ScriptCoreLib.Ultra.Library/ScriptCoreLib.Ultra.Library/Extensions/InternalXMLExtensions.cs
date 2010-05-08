using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Extensions
{
	internal static class InternalXMLExtensions
	{
		// once all platforms implement xml then we may
		// skip these methods...

		public static string ToXMLString(this string xml)
		{
			return xml
				.Replace("&", "&amp;")
				.Replace("<", "&lt;")
				.Replace(">", "&gt;")
				.Replace("\"", "&quot;")
				.Replace("'", "&apos;")
			;
		}

		public static string FromXMLString(this string xml)
		{
			return xml
				.Replace("&apos;", "'")
				.Replace("&quot;", "\"")
				.Replace("&gt;", ">")
				.Replace("&lt;", "<")
				.Replace("&amp;", "&")
			;
		}
	}
}
