using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio.Languages
{
	public partial class VisualBasicLanguage
	{
        // http://www.codeproject.com/KB/dotnet/vbnet_c__difference.aspx

		public static class Keywords
		{
			public static Keyword
				@GetType = "GetType",
				@End = "End",
				@Sub = "Sub",
				@Function = "Function",
                @Imports = "Imports",
                @Inherits = "Inherits",
				@Assembly = "Assembly",
				@Of = "Of",
                @As = "As",
                @AddHandler = "AddHandler",
				@Public = "Public",
                @Private = "Private",
                @ReadOnly = "ReadOnly",
                @Overrides = "Overrides",
                @New = "New",
                @Me = "Me",
                @MyBase = "MyBase",
				@Shared = "Shared",
				@NotInheritable = "NotInheritable",
				@Class = "Class",
				@Module = "Module",
                @ByVal = "ByVal",
                @If = "If",
                @Then = "Then",
                @Else = "Else",
                @Return = "Return",
                @Partial = "Partial",
                @Protected = "Protected",
				@Namespace = "Namespace"
			;
		}
	}
}
