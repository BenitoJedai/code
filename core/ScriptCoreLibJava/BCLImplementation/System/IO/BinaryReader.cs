using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;

namespace ScriptCoreLibJava.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.BinaryReader))]
	internal class __BinaryReader : __IDisposable
	{
		internal Stream InternalStream;

		public virtual Stream BaseStream { get { return this.InternalStream; } }

		public __BinaryReader(Stream input)
		{
			this.InternalStream = input;
		}

		#region __IDisposable Members

		public void Dispose()
		{

		}

		#endregion

		public virtual byte ReadByte()
		{
			var i = this.InternalStream.ReadByte();

			// yay, we are throwing the wrong exception
			if (i < 0)
				throw new InvalidOperationException();

			return (byte)i;
		}

		public virtual byte[] ReadBytes(int count)
		{
			var buffer = new byte[count];
			var c = this.InternalStream.Read(buffer, 0, count);
			if (c != count)
				throw new InvalidOperationException();

			return buffer;
		}

		public virtual int ReadInt32()
		{
			var i = ReadBytes(4);

			return
				(i[3] << (8 * 3)) +
				(i[2] << (8 * 2)) +
				(i[1] << (8 * 1)) +
				(i[0] << (8 * 0));
		}

		public virtual uint ReadUInt32()
		{
			var i = ReadBytes(4);

			return (uint)(
				(i[3] << (8 * 3)) +
				(i[2] << (8 * 2)) +
				(i[1] << (8 * 1)) +
				(i[0] << (8 * 0))
			);
		}

		public virtual short ReadInt16()
		{
			var i = ReadBytes(2);

			return (short)(
				(i[1] << (8 * 1)) +
				(i[0] << (8 * 0))
			);
		}

		public virtual int Read(byte[] buffer, int index, int count)
		{
			return this.InternalStream.Read(buffer, index, count);
		}
	}
}
