using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.Stream))]
	internal abstract class __Stream
	{
		public abstract long Length { get; }

		public abstract long Position { get; set; }


		public abstract int Read(byte[] buffer, int offset, int count);

		public virtual int ReadByte()
		{
			var buffer = new byte[1];
			var i = Read(buffer, 0, 1);

			if (i < 0)
				return i;

			return buffer[0];
		}

		public abstract void Write(byte[] buffer, int offset, int count);

		public virtual void WriteByte(byte value)
		{
			this.Write(new[] { value }, 0, 1);
		}

	}
}
