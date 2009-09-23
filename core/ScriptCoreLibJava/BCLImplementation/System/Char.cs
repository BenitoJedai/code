using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.Char)
		, ImplementationType = typeof(java.lang.Character)
		)]
	internal class __Char
	{
		public static bool IsWhiteSpace(char c)
		{
			return java.lang.Character.isWhitespace(c);
		}

		public static bool IsWhiteSpace(string e, int i)
		{
			var c = e[i];

			return char.IsWhiteSpace(c);
		}

	
	}
}
