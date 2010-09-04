using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio.Languages
{
	partial class VisualFSharpLanguage
	{
		public static class Keywords
		{
			// keywords vs operators? :)
			// http://msdn.microsoft.com/en-us/library/ee353754.aspx

			public static Keyword
					@global = "global",
					@open = "open",
					@type = "type",
					@typeof = "typeof",
                    @do = "do",
                    @let = "let",
					@ignore = "ignore",
					@new = "new",
					@fun = "fun",
                    @member = "member",
                    @mutable = "mutable",
					@assembly = "assembly",
					@module = "module",
					@namespace = "namespace"
				;
		}
	}
}
