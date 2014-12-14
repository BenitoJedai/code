using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;
using ScriptCoreLibNative.SystemHeaders;

namespace ScriptCoreLibNative.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.MemoryStream))]
	internal class __MemoryStream : __Stream
	{
        public static int override_Stream_Read(object k, byte[] buffer, int offset, int count)
        {
            return  ((__MemoryStream)k).__Read(buffer, offset, count);
        }

        public static long override_Stream_Seek(object k, long offest, SeekOrigin so)
        {
            return ((__MemoryStream)k).__Seek(offest, so);
        }

        public static void override_Stream_Write(object k, byte[] buffer, int offset, int count)
        {
            ((__MemoryStream)k).__Write(buffer, offset, count);
        }

        public static void override_Stream_Close(object k)
        {
            //((__MemoryStream)k).__Close();
        }

        public static long override_Stream_get_Length(object k)
        {
            return ((__MemoryStream)k).__Length;
        }

        public __MemoryStream()
		{
            this.__Stream_Write = override_Stream_Write;
            this.__Stream_Close = override_Stream_Close;
            this.__Stream_Read = override_Stream_Read;
            this.__Stream_Seek = override_Stream_Seek;
            this.__Stream_get_Length = override_Stream_get_Length;

            // we are doing manual virtual calls.. feel the pain yet?


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
