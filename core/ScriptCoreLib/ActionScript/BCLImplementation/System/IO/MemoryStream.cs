using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.Extensions;
using System.IO;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.MemoryStream))]
	internal class __MemoryStream : __Stream
	{
		internal ByteArray Buffer = new ByteArray();

		public __MemoryStream() : this(null)
		{

		}

		public __MemoryStream(byte[] buffer)
		{
			if (buffer != null)
			{
				this.Write(buffer, 0, buffer.Length);

				Position = 0;
			}
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			// http://livedocs.adobe.com/flex/2/langref/flash/utils/ByteArray.html#readBytes()
			var o = new ByteArray();

			Buffer.readBytes(o, (uint)offset, (uint)count);

			o.position = 0;

			for (int i = 0; i < count; i++)
			{
				buffer[i] = (byte)((byte)(o.readByte()) & 0xff);
			}

			return buffer.Length;
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			// http://livedocs.adobe.com/flex/2/langref/flash/utils/ByteArray.html#writeBytes()
			var o = new ByteArray();

			for (int i = 0; i < count; i++)
			{
				o.writeByte(buffer[offset + i] & 0xff);
			}

			Buffer.writeBytes(o);
		}

		public virtual byte[] ToArray()
		{
			return this.Buffer.ToArray();

			//var o = new byte[this.Buffer.length];

			//for (int i = 0; i < o.Length; i++)
			//{
			//    var b = (byte)(this.Buffer.readByte());

			//    o[i] = (byte)(b & 0xff);
			//}

			//return o;
		}

		public override long Length
		{
			get { return Buffer.length;  }
		}

		public override long Position
		{
			get
			{
				return Buffer.position;
			}
			set
			{
				Buffer.position = (uint)value;
			}
		}

		public void WriteTo(Stream s)
		{
			var b = new byte[s.Length];

			s.Read(b, 0, b.Length);
			
			this.Write(b, 0, b.Length);
		}
	}
}
