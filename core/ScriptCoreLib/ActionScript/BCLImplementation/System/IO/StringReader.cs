using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.StringReader))]
	internal class __StringReader : __TextReader
	{
		readonly string InputString;

		public int Position;

		public __StringReader(string InputString)
		{
			this.InputString = InputString;
		}

		public override string ReadLine()
		{
			if (Position < InputString.Length)
			{
				const string rn = "\r\n";
				const string n = "\n";

				// we need to support win32 and unix newlines

				var i = this.InputString.IndexOf(rn, Position);
				var j = this.InputString.IndexOf(n, Position);

				var x = rn.Length;

				var swap = false;

				if (i < 0)
					swap = true;

				if (j < i)
					swap = true;

				if (swap)
				{
					i = j;
					x = n.Length;
				}

				var p = Position;

				// if newline not found, take it all
				if (i < 0)
				{
					i = InputString.Length;
					Position = i;
				}
				else
				{
					Position = i + x;
				}

				return this.InputString.Substring(p, i - p);
			}

			return null;
		}
	}
}
