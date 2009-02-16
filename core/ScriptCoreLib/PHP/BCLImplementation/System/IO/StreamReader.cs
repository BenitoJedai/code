using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScriptCoreLib.PHP.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.StreamReader))]
	internal class __StreamReader : __TextReader
	{
		public __StreamReader(Stream stream)
		{

		}
	}
}
