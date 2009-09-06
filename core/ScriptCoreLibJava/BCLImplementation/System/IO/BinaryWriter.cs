using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;

namespace ScriptCoreLibJava.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.BinaryWriter))]
	internal class __BinaryWriter : IDisposable
	{
		internal Stream InternalStream;

		public virtual Stream BaseStream { get { return this.InternalStream; } }

		public __BinaryWriter(Stream input)
		{
			this.InternalStream = input;
		}

		#region IDisposable Members

		public void Dispose()
		{

		}

		#endregion

		readonly byte[] _buffer = new byte[16];

		public virtual void Write(byte value)
		{
			InternalStream.WriteByte(value);
		}

		public virtual void Write(char value)
		{
			this._buffer[0] = (byte)(value & 0xff);
			this._buffer[1] = (byte)(value >> 8);
			this.InternalStream.Write(this._buffer, 0, 2);
		}

		// we will implement only CLSCompliant? Write versions as jsc cannot do the disambiguation on its own at this time...

		public virtual void Write(short value)
		{
			this._buffer[0] = (byte)value;
			this._buffer[1] = (byte)(value >> 8);
			this.InternalStream.Write(this._buffer, 0, 2);
		}

		//public virtual void Write(ushort value)
		//{

		//}

		public virtual void Write(int value)
		{
			this._buffer[0] = (byte)value;
			this._buffer[1] = (byte)(value >> 8);
			this._buffer[2] = (byte)(value >> 0x10);
			this._buffer[3] = (byte)(value >> 0x18);
			this.InternalStream.Write(this._buffer, 0, 4);

		}

		//public virtual void Write(uint value)
		//{

		//}

		public virtual void Write(byte[] buffer)
		{
			this.BaseStream.Write(buffer, 0, buffer.Length);
		}
	}

}
