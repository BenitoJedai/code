using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.BinaryWriter))]
	internal class __BinaryWriter : IDisposable
	{
		protected Stream OutStream;
		private byte[] _buffer;


		public virtual Stream BaseStream
		{
			get
			{
				return OutStream;
			}
		}



		public __BinaryWriter(Stream output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			this.OutStream = output;
			this._buffer = new byte[0x10];

		}

		#region __IDisposable Members

		public void Dispose()
		{
		}

		#endregion

		public virtual void Write(short value)
		{
			this._buffer[0] = (byte)((value >> 8 * 0) & 0xff);
			this._buffer[1] = (byte)((value >> 8 * 1) & 0xff);

			OutStream.Write(this._buffer, 0, 2);
		}

		public virtual void Write(int value)
		{
			this._buffer[0] = (byte)((value >> 8 * 0) & 0xff);
			this._buffer[1] = (byte)((value >> 8 * 1) & 0xff);
			this._buffer[2] = (byte)((value >> 8 * 2) & 0xff);
			this._buffer[3] = (byte)((value >> 8 * 3) & 0xff);

			OutStream.Write(this._buffer, 0, 4);
		}

		public virtual void Write(uint value)
		{
			this._buffer[0] = (byte)((value >> 8 * 0) & 0xff);
			this._buffer[1] = (byte)((value >> 8 * 1) & 0xff);
			this._buffer[2] = (byte)((value >> 8 * 2) & 0xff);
			this._buffer[3] = (byte)((value >> 8 * 3) & 0xff);

			OutStream.Write(this._buffer, 0, 4);
		}

		public virtual void Write(byte value)
		{
			this.OutStream.WriteByte(value);
		}

		public virtual void Write(byte[] value)
		{
			this.OutStream.Write(value, 0, value.Length);
		}

		public virtual void Write(double value)
		{
			throw new NotSupportedException();
		}

		public virtual void Write(string value)
		{
			Write7BitEncodedInt(GetByteCount(value));

			// http://www.webtoolkit.info/javascript-utf8.html

			foreach (var c in value)
			{
				if (c < 128)
				{
					BaseStream.WriteByte((byte)c);
				}
				else if (c < 2048)
				{
					BaseStream.WriteByte((byte)((c >> 6) | 192));
					BaseStream.WriteByte((byte)((c & 63) | 128));
				}
				else
				{
					BaseStream.WriteByte((byte)((c >> 12) | 224));
					BaseStream.WriteByte((byte)(((c >> 6) & 63) | 128));
					BaseStream.WriteByte((byte)((c & 63) | 128));
				}

			}
		}

		public int GetByteCount(string value)
		{
			int u = 0;

			foreach (var c in value)
			{
				u++;

				if (c > 0x7F)
					u++;

				if (c > 0x7FF)
					u++;
			}

			return u;
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
