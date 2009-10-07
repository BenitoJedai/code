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
		Stream OutStream;

		private byte[] _buffer;

		public Stream BaseStream
		{
			get
			{
				return OutStream;
			}
		}

		public __BinaryWriter(Stream s)
		{
			this.OutStream = s;
			this._buffer = new byte[0x10];
		}

	

		public void Write(uint value)
		{
			this._buffer[0] = (byte)((value >> 8 * 0) & 0xff);
			this._buffer[1] = (byte)((value >> 8 * 1) & 0xff);
			this._buffer[2] = (byte)((value >> 8 * 2) & 0xff);
			this._buffer[3] = (byte)((value >> 8 * 3) & 0xff);

			OutStream.Write(this._buffer, 0, 4);
		}

		public void Write(short value)
		{
			this._buffer[0] = (byte)((value >> 8 * 0) & 0xff);
			this._buffer[1] = (byte)((value >> 8 * 1) & 0xff);

			OutStream.Write(this._buffer, 0, 2);
		}

		public void Write(ushort value)
		{
			this._buffer[0] = (byte)((value >> 8 * 0) & 0xff);
			this._buffer[1] = (byte)((value >> 8 * 1) & 0xff);
	
			OutStream.Write(this._buffer, 0, 2);
		}

		public long Seek(int offset, SeekOrigin origin)
		{
			return offset;
		}
	}
}
