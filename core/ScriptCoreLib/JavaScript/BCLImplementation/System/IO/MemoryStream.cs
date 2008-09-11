using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.Extensions;
using System.IO;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.MemoryStream))]
	internal class __MemoryStream : __Stream
	{
		internal string Buffer = "";

		public __MemoryStream()
			: this(null)
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

		public override int ReadByte()
		{
			if (this.Position < 0)
				return -1;

			if (this.Position >= this.Length)
				return -1;

			var x = (byte)(this.Buffer[(int)this.Position] & 0xff);

			this.Position++;

			return x;
		}

		public override void WriteByte(byte value)
		{
			if (this.Position < this.Length)
				throw new NotImplementedException();


			this.Buffer += __String.FromCharCode(value & 0xff);
			this.Position++;

		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			var c = 0;
			var p = (int)this.Position;

			for (int i = 0; i < count; i++)
			{
				if (i >= this.Length)
					break;

				buffer[i + offset] = (byte)(this.Buffer[i + p] & 0xff);

				c++;
			}

			this.Position += c;

			return c;
		}


		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.Position < this.Length)
				throw new NotImplementedException();

			for (int i = 0; i < count; i++)
			{
				this.Buffer += __String.FromCharCode(buffer[offset + i]);
			}

			this.Position += count;
		}

		public virtual byte[] ToArray()
		{
			var a = new byte[this.Length];

			for (int i = 0; i < this.Length; i++)
			{
				a[i] = (byte)(this.Buffer[i] & 0xff);
			}

			return a;
		}

		public override long Length
		{
			get { return Buffer.Length; }
		}

		public override long Position { get; set; }

		public void WriteTo(Stream s)
		{
			//var b = new byte[s.Length];

			//s.Read(b, 0, b.Length);

			//this.Write(b, 0, b.Length);

			throw new NotSupportedException();

		}
	}
}
