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
		Stream InternalStream;

		public __StreamWriter(Stream stream)
		{
			InternalStream = stream;
		}

		public override void Flush()
		{
			// flush if any
		}

		public override void WriteLine()
		{
			InternalStream.WriteByte((byte)'\r');
			InternalStream.WriteByte((byte)'\n');
		}

		public override void WriteLine(string value)
		{
			foreach (char c in value)
			{
				InternalStream.WriteByte((byte)c);
			}

			InternalStream.WriteByte((byte)'\r');
			InternalStream.WriteByte((byte)'\n');

		}
	}
}
