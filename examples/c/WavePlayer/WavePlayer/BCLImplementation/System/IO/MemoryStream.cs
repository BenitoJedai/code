using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;

namespace WavePlayer.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.MemoryStream))]
	internal class __MemoryStream : __Stream
	{
		public __MemoryStream() 
		{

		
			// we are doing manual virtual calls.. feel the pain yet?
			this.__Stream_get_Length = k => ((__MemoryStream)k).__Length;
			this.__Stream_Write = (k, buffer, offset, count) => ((__MemoryStream)k).__Write(buffer, offset, count);
			this.__Stream_Seek = (k, offset, o) => ((__MemoryStream)k).__Seek(offset, o);
			this.__Stream_Read = (k, offset, o, c) => ((__MemoryStream)k).__Read(offset, o, c);

			
			this.InternalLength = 0;
			this.InternalBufferCapacity = 0x7FFF;
			this.InternalBuffer = new byte[this.InternalBufferCapacity];
		}

		private long __Length
		{
			get
			{
				return this.InternalLength;
			}
		}

		public byte[] InternalBuffer;
		public int InternalBufferCapacity;
		public int InternalLength;
		public int InternalPosition;

		public byte[] ToArray()
		{
			var a = new byte[this.InternalLength];

			for (int i = 0; i < this.InternalLength; i++)
			{
				var x = this.InternalBuffer[i];




				a[i] = x;
			}


			return a;
		}

		public void EnsureCapacity(int c)
		{
			if (c < InternalBufferCapacity)
				return;

			

			InternalBufferCapacity = c;
			InternalBuffer = (byte[])stdlib_h.realloc(InternalBuffer, c);
		}

		public void __Write(byte[] buffer, int offset, int count)
		{
		

			EnsureCapacity(InternalPosition + count);

			for (int i = 0; i < count; i++)
			{
				var x = buffer[offset + i];

				InternalBuffer[InternalPosition + i] = x;
			}

			InternalPosition += count;
			InternalLength += count;
		}

		public long __Seek(long offset, SeekOrigin loc)
		{
			this.InternalPosition = (int)offset;

			return offset;
		}

		public int __Read(byte[] buffer, int offset, int count)
		{
			for (int i = 0; i < count; i++)
			{
				buffer[i + offset] = this.InternalBuffer[InternalPosition + i];
			}

			return count;
		}
	}
}
