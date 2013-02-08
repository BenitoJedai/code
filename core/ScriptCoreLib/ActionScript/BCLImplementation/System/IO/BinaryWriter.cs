using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Shared.BCLImplementation.System;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.BinaryWriter))]
	internal class __BinaryWriter : __IDisposable
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
			OutStream.ToByteArray().writeShort(value);
		}

		public virtual void Write(int value)
		{
			OutStream.ToByteArray().writeInt(value);
		}

		public virtual void Write(uint value)
		{
			OutStream.ToByteArray().writeUnsignedInt(value);
		}

		public virtual void Write(byte value)
		{
			this.OutStream.WriteByte(value);
		}

		public virtual void Write(byte[] value)
		{
			this.OutStream.Write(value, 0, value.Length);
		}

        public virtual void Write(float value)
        {
            OutStream.ToByteArray().writeFloat(value);
        }

		public virtual void Write(double value)
		{
			OutStream.ToByteArray().writeDouble(value);
		}

		public virtual void Write(string value)
		{
			Write7BitEncodedInt(GetByteCount(value));
			OutStream.ToByteArray().writeUTFBytes(value);
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
