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


		public override void Write(string value)
		{
			var x = value.Length;
			var buffer = new byte[x];

			for (int i = 0; i < x; i++)
			{
				buffer[i] = (byte)value[i];
			}

			InternalStream.Write(buffer, 0, buffer.Length);
		}

		public override void WriteLine()
		{
			Write("\r\n");
		}

		public override void WriteLine(string value)
		{
			Write(value + "\r\n");
		}
	}
}
