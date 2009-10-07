using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;

namespace WavePlayer.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.MemoryStream))]
	internal class __MemoryStream
	{
		public __MemoryStream() : this(null)
		{

		}

		public __MemoryStream(Stream s)
		{

		}
	}
}
