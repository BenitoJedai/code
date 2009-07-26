using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.utils;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.BinaryReader))]
	internal class __BinaryReader
	{
		private Stream InternalStream;

		private byte[] m_buffer;


		public virtual Stream BaseStream
		{
			get
			{
				return InternalStream;
			}
		}


		public __BinaryReader(Stream input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			this.InternalStream = input;
			this.m_buffer = new byte[0x10];

		}


		public virtual uint ReadUInt32()
		{
			return InternalStream.ToByteArray().readUnsignedInt();
		}

		public virtual byte[] ReadBytes(int length)
		{
			var k = new ByteArray();
			var s = InternalStream.ToByteArray();

			s.readBytes(k, 0, (uint)length);

			return k.ToArray();
		}

		public virtual int Read(byte[] buffer, int index, int count)
		{
			return this.InternalStream.Read(buffer, index, count);
		}

		public virtual int ReadInt32()
		{
			return InternalStream.ToByteArray().readInt();
		}

		public virtual short ReadInt16()
		{
			return InternalStream.ToByteArray().readShort();
		}

		private void FillBuffer(int p)
		{
			InternalStream.Read(m_buffer, 0, p);
		}


		public virtual byte ReadByte()
		{
			if (this.InternalStream == null)
			{
				throw new Exception("FileNotOpen");
			}
			int num = this.InternalStream.ReadByte();
			if (num == -1)
			{
				var ms = this.InternalStream as MemoryStream;


				if (ms != null)
				{

					throw new Exception("MemoryStreamEndOfFile: " + new { this.InternalStream.Position, this.InternalStream.Length, num, value = ms.ToArray() }.ToString());
				}
				else
					throw new Exception("EndOfFile: " + new { this.InternalStream.Position, this.InternalStream.Length, num }.ToString());
			}
			return (byte)num;
		}

		public virtual double ReadDouble()
		{
			return InternalStream.ToByteArray().readDouble();

		}

		public virtual string ReadString()
		{
			var length = Read7BitEncodedInt();
			//var bytes = ReadBytes(length);

			return InternalStream.ToByteArray().readUTFBytes((uint)length);
		}

		protected internal int Read7BitEncodedInt()
		{
			byte num3;
			int num = 0;
			int num2 = 0;
			bool loop = true;
			while (loop)
			{
				if (num2 == 0x23)
				{
					throw new Exception("Format_Bad7BitInt32");
				}
				num3 = this.ReadByte();
				num |= (num3 & 0x7f) << num2;
				num2 += 7;

				loop = ((num3 & 0x80) != 0);
			}

			return num;
		}

		public static implicit operator __BinaryReader(BinaryReader r)
		{
			return (__BinaryReader)(object)r;
		}

	}
}
