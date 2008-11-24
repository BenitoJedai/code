using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.StringWriter))]
	internal class __StringWriter : __TextWriter
	{
		readonly StringBuilder StringBuilder = new StringBuilder();

		public override void WriteLine(string value)
		{
			StringBuilder.AppendLine(value);
		}

		public override string ToString()
		{
			return StringBuilder.ToString();
		}
	}
}
