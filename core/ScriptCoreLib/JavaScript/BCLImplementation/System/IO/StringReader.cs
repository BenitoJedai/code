using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.IO
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
				var n = Environment.NewLine;
				var i = this.InputString.IndexOf(n, Position);

				var p = Position;

				// if newline not found, take it all
				if (i < 0)
				{
					Position = InputString.Length;
				}
				else
				{
					Position = i + n.Length;
				}

				return this.InputString.Substring(p, Position - p);
			}

			return null;
		}
	}
}
