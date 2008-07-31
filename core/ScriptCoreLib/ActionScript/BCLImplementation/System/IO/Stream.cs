using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.Stream))]
	internal abstract class __Stream : __IDisposable
	{
		#region __IDisposable Members

		public void Dispose()
		{

		}

		#endregion

		public abstract int Read(byte[] buffer, int offset, int count);

		public virtual int ReadByte()
		{
			var buffer = new byte[1];

			Read(buffer, 0, 1);

			return buffer[0];
		}

		public abstract void Write(byte[] buffer, int offset, int count);

		public virtual void WriteByte(byte value)
		{
			this.Write(new[] { value }, 0, 1);
		}

		public abstract long Length { get; }

		public abstract long Position { get; set; }

	}
}
