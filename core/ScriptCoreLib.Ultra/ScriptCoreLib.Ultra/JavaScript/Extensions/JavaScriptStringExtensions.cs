using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.Extensions
{
	public static class JavaScriptStringExtensions
	{
		public static string ToDocumentTitle(this string Text)
		{
			Native.Document.title = Text;
			return Text;
		}
	}
}
