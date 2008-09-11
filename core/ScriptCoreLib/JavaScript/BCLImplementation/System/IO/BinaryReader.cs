using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.BinaryReader))]
	internal class __BinaryReader
	{
		private Stream m_stream;

		private byte[] m_buffer;


		public virtual Stream BaseStream
		{
			get
			{
				return m_stream;
			}
		}


		public __BinaryReader(Stream input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			this.m_stream = input;
			this.m_buffer = new byte[0x10];

		}


		public virtual uint ReadUInt32()
		{
			FillBuffer(4);

			uint v = 0;

			v += (uint)(this.m_buffer[0] << (0 * 8));
			v += (uint)(this.m_buffer[1] << (1 * 8));
			v += (uint)(this.m_buffer[2] << (2 * 8));
			v += (uint)(this.m_buffer[3] << (3 * 8));

			return v;
		}

		public virtual byte[] ReadBytes(int length)
		{
			var u = new byte[length];

			this.m_stream.Read(u, 0, length);

			return u;
		}

		public virtual int ReadInt32()
		{
			FillBuffer(4);

			int v = 0;

			v += this.m_buffer[0] << (0 * 8);
			v += this.m_buffer[1] << (1 * 8);
			v += this.m_buffer[2] << (2 * 8);
			v += this.m_buffer[3] << (3 * 8);

			return v;
		}

		public virtual short ReadInt16()
		{
			FillBuffer(2);

			short v = 0;

			v += (short)(this.m_buffer[0] << (0 * 8));
			v += (short)(this.m_buffer[1] << (1 * 8));

			return v;
		}

		private void FillBuffer(int p)
		{
			m_stream.Read(m_buffer, 0, p);
		}


		public virtual byte ReadByte()
		{
			if (this.m_stream == null)
			{
				throw new Exception("FileNotOpen");
			}
			int num = this.m_stream.ReadByte();
			if (num == -1)
			{
				var ms = this.m_stream as MemoryStream;


				if (ms != null)
				{

					throw new Exception("MemoryStreamEndOfFile: " + new { this.m_stream.Position, this.m_stream.Length, num, value = ms.ToArray() }.ToString());
				}
				else
					throw new Exception("EndOfFile: " + new { this.m_stream.Position, this.m_stream.Length, num }.ToString());
			}
			return (byte)num;
		}

		public virtual double ReadDouble()
		{
			throw new NotSupportedException();
		}

		public virtual string ReadString()
		{
			var length = Read7BitEncodedInt();
			var bytes = ReadBytes(length);
			var i = 0;
			var a = new IArray<int>();

			while (i < bytes.Length)
			{
				int c = bytes[i];

				if (c < 128)
				{
					a.push(c);
					i++;
				}
				else
				{
					var gt_191 = c > 191;
					var lt_224 = c < 224;

					if (gt_191 && lt_224)
					{
						int c2 = bytes[i + 1];
						a.push(((c & 31) << 6) | (c2 & 63));
						i += 2;
					}
					else
					{
						int c2 = bytes[i + 1];
						int c3 = bytes[i + 2];
						a.push(((c & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63));
						i += 3;
					}
				}

			}


			return String_fromCharCode(a);
		}

		[Script(OptimizedCode = "return String.fromCharCode.apply(null, e);")]
		static string String_fromCharCode(int[] e)
		{
			return default(string);
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
