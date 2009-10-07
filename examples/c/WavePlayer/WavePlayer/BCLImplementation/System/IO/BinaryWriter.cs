using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;

namespace WavePlayer.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.BinaryWriter))]
	internal class __BinaryWriter
	{
		Stream InternalStream;

		public Stream BaseStream
		{
			get
			{
				return InternalStream;
			}
		}

		public __BinaryWriter(Stream s)
		{
			InternalStream = s;
		}

		public void Write(char[] e)
		{

		}

		public void Write(uint e)
		{

		}

		public void Write(short e)
		{

		}

		public void Write(ushort e)
		{

		}

		public long Seek(int offset, SeekOrigin origin)
		{
			return 0;
		}
	}
}
