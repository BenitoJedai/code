using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScriptCoreLib.PHP.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.StreamWriter))]
	internal class __StreamWriter : __TextWriter
	{
		public __StreamWriter(Stream stream)
		{
		}
	}
}
