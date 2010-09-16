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
                    @base = "base",
                    @global = "global",
					@open = "open",
                    @type = "type",
                    @internal = "internal",
					@typeof = "typeof",
                    @do = "do",
                    @let = "let",
                    @ignore = "ignore",
                    @inherit = "inherit",
					@new = "new",
					@fun = "fun",
                    @member = "member",
                    @mutable = "mutable",
                    @as = "as",
                    @assembly = "assembly",
                    @module = "module",
                    @if = "if",
                    @then = "then",
                    @else = "else",
					@namespace = "namespace"
				;
		}
	}
}
