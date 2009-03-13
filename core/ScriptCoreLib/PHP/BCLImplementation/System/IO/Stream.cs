using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScriptCoreLib.PHP.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.Stream))]
	internal abstract class __Stream : __IDisposable
	{
		#region __IDisposable Members

		public void Dispose()
		{
			this.Close();
		}

		#endregion

		public virtual void Close()
		{
	
		}

 

 


		public abstract long Seek(long offset, SeekOrigin origin);

		public abstract void SetLength(long value);

		public abstract int Read(byte[] buffer, int offset, int count);

		public virtual int ReadByte()
		{
			var buffer = new byte[1];

			var x = Read(buffer, 0, 1);

			if (x != 1)
				return -1;

			return (int) buffer[0] & 0xff;
		}

		public abstract void Write(byte[] buffer, int offset, int count);

		public virtual void WriteByte(byte value)
		{
			var v = (byte)(value & 0xff);

			this.Write(new[] { v }, 0, 1);
		}

		public abstract long Length { get; }

		public abstract long Position { get; set; }

	}
}
