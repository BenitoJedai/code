using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript;
using Ultra1.Common;

namespace Ultra1.Inline
{
	public class Class2
	{
		// types from this assembly will be merged at rewrite
		// this assembly should only be referenced by the rewritee or other inlined assemblies

		// at this time this assembly must also be not optimized
		// the rewriter is not yet up for simplification phase

		public static string ToString(string x, string y)
		{
			return "[ " + x + "= " + y + " ]";
		}
	}

	public static class js_extensions
	{
		public static void MessageStatusOnClick(this IUltraPolyglot e)
		{
			e.Clicked +=
				delegate
				{
					Native.Window.alert("MessageStatusOnClick: " + e.Status);
				};
		}
	}
}
