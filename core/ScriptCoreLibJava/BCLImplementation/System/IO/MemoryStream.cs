using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.MemoryStream))]
	internal class __MemoryStream : __Stream
	{
		// ByteArrayInputStream 
		// http://www.koders.com/java/fid654B227C95A99C7C2ACA686E7BC6BA584491A6B7.aspx

		// InputStream
		// http://www.koders.com/java/fidF990D954151F15A618183172871A1403F719D971.aspx
		byte[] InternalBuffer;
		long InternalPosition;
		long InternalLength;

		public __MemoryStream()
			: this(null)
		{

		}

		public __MemoryStream(byte[] buffer)
		{
			if (buffer != null)
			{
				this.Write(buffer, 0, buffer.Length);

				this.InternalPosition = 0;
			}
		}

		public override long Length
		{
			get
			{
				return InternalLength;
			}
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			var a = this.Length - this.InternalPosition;

			if (count > a)
			{
				if (a < 0)
					return -1;

				count = (int)a;
			}

			Array.Copy(this.InternalBuffer, (int)this.InternalPosition, buffer, offset, count);

			this.InternalPosition += count;

			return count;
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			InternalEnsureCapacity(Capacity + count);

			// java does not support long array copy?

			Array.Copy(buffer, offset, InternalBuffer, (int)InternalPosition, count);
			InternalPosition += count;
			InternalLength += count;
		}

		public override void WriteByte(byte value)
		{
			InternalEnsureCapacity(Capacity + 1);

			InternalBuffer[InternalPosition] = value;
			InternalPosition++;
			InternalLength++;
		}

		void InternalEnsureCapacity(long TargetCapacity)
		{
			if (Capacity < TargetCapacity)
				Capacity = (int)(TargetCapacity + 8);
		}

		public virtual int Capacity
		{
			get
			{
				if (InternalBuffer == null)
					Capacity = 0x1000;

				return InternalBuffer.Length;

			}
			set
			{
				var x = InternalBuffer;

				// shall preserve current buffer
				InternalBuffer = new byte[value];

				if (x != null)
				{
					var y = x.Length;

					if (InternalBuffer.Length < y)
						y = InternalBuffer.Length;

					Array.Copy(x, InternalBuffer, y);
				}
			}
		}

		public virtual byte[] ToArray()
		{
			var x = new byte[InternalLength];

			Array.Copy(InternalBuffer, x, (int)InternalLength);

			return x;
		}


		public override long Position
		{
			get
			{
				return this.InternalPosition;
			}
			set
			{
				this.InternalPosition = value;
			}
		}
	}
}
