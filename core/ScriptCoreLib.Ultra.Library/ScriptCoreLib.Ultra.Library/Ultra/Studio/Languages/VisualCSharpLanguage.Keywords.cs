using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio.Languages
{
	public partial class VisualCSharpLanguage
	{
		public static class Keywords
		{
            // jsc needs to support enum ToString!

			public static Keyword 
				@assembly = "assembly",
				@using = "using",
				@class = "class",
				@partial = "partial",
				@interface = "interface",
				@public = "public",
				@private = "private",
                @protected = "protected",
                @readonly = "readonly",
				@sealed = "sealed",
                @static = "static",
				@override = "override",
				@typeof = "typeof",
				@void = "void",
				@get = "get",
				@set = "set",
				@new = "new",
				@delegate = "delegate",
                @bool = "bool",
                @string = "string",
                @if = "if",
                @else = "else",
				@this = "this",
				@base = "base",
				@namespace = "namespace"
			;
		}
	}
}
