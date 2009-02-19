using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.Char))]
	internal class __Char
	{
		public static bool IsNumber(string c, int i)
		{
			return IsNumber(c[i]);
		}

		public static bool IsNumber(char c)
		{
			if (c == '0') return true;
			if (c == '1') return true;
			if (c == '2') return true;
			if (c == '3') return true;
			if (c == '4') return true;
			if (c == '5') return true;
			if (c == '6') return true;
			if (c == '7') return true;
			if (c == '8') return true;
			if (c == '9') return true;

			return false;
		}
	}
}
