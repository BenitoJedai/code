using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.Convert))]
	internal class __Convert
	{
		public static string ToString(char value)
		{
			return Native.API.chr(value);
		}

	}
}
