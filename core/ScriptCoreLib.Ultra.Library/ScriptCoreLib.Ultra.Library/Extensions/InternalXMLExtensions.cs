using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Library.Extensions
{
	internal static class InternalXMLExtensions
	{
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
