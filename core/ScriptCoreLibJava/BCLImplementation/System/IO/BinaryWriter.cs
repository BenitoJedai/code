﻿using System;
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

        public virtual void Write(long value)
        {
            this._buffer[0] = (byte)value;
            this._buffer[1] = (byte)(value >> 8);
            this._buffer[2] = (byte)(value >> (8 * 2));
            this._buffer[3] = (byte)(value >> (8 * 3));
            this._buffer[4] = (byte)(value >> (8 * 4));
            this._buffer[5] = (byte)(value >> (8 * 5));
            this._buffer[6] = (byte)(value >> (8 * 6));
            this._buffer[7] = (byte)(value >> (8 * 7));
            this.InternalStream.Write(this._buffer, 0, 8);
        }

		//public virtual void Write(uint value)
		//{

		//}

		public virtual void Write(byte[] buffer)
		{
			this.BaseStream.Write(buffer, 0, buffer.Length);
		}

		public virtual void Write(string value)
		{
			var bytes = Encoding.UTF8.GetBytes(value); ;

			this.Write7BitEncodedInt(bytes.Length);
			this.Write(bytes);
		}

	

		protected void Write7BitEncodedInt(int value)
		{
			uint num = (uint)value;
			while (num >= 0x80)
			{
				this.Write((byte)(num | 0x80));
				num = num >> 7;
			}
			this.Write((byte)num);
		}
	}

}
