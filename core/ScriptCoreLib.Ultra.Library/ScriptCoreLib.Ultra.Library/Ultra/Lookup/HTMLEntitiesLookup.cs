using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Lookup
{
	public static class HTMLEntitiesLookup
	{
		// http://www.w3.org/TR/REC-html40/sgml/entities.html
		public static readonly Dictionary<string, string> Lookup = new Dictionary<string, string>
		{
			{"&nbsp;", "&#160;"},
			{"&ndash;", "&#8211;"},
			{"&laquo;", "&#171;"},
			{"&raquo;", "&#187;"},
		};
	}
}
