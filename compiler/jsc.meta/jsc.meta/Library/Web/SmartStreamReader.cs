using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace jsc.meta.Library.Web
{
	internal class SmartStreamReader : Stream
	{
		public readonly Stream BaseStream;




		public SmartStreamReader(Stream BaseStream)
		{
			this.BaseStream = BaseStream;
		}

		public override bool CanRead
		{
			get { return true; }
		}

		public override bool CanSeek
		{
			get { return false; }
		}

		public override bool CanWrite
		{
			get { return false; }
		}

		public override void Flush()
		{
			throw new NotImplementedException("");
		}

		public override long Length
		{
			get { throw new NotImplementedException(""); }
		}

		public override long Position
		{
			get
			{
				throw new NotImplementedException("");
			}
			set
			{
				throw new NotImplementedException("");
			}
		}



		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException("");
		}

		public override void SetLength(long value)
		{
			throw new NotImplementedException("");
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException("");
		}

		const int InternalBufferCapacity = 0x1000;
		byte[] InternalBuffer = new byte[InternalBufferCapacity];
		int InternalBufferCount = 0;

		public override int Read(byte[] buffer, int offset, int count)
		{
			// buffer + stream
			var value = 0;

			var InternalBufferCountToBeRead = InternalBufferCount;

			if (InternalBufferCountToBeRead > count)
				InternalBufferCountToBeRead = count;

			for (int i = 0; i < InternalBufferCountToBeRead; i++)
			{
				buffer[offset + i] = InternalBuffer[i];
			}

			value += InternalBufferCountToBeRead;
			offset += InternalBufferCountToBeRead;
			count -= InternalBufferCountToBeRead;

			DiscardBuffer(InternalBufferCountToBeRead);


			// The total number of bytes read into the buffer. 
			// This can be less than the number of bytes requested 
			// if that many bytes are not currently available, 
			// or zero (0) if the end of the stream has been reached. 

			while (count > 0)
			{
				var i = this.BaseStream.Read(buffer, offset, count);

				if (i > 0)
				{
					value += i;
					offset += i;
					count -= i;


				}
				else
				{
					// no more data, we must return
					count = 0;
				}

			}

			return value;
		}

		public string ReadToEnd()
		{
			var a = new StringBuilder();

			while (true)
			{
				for (int i = 0; i < this.InternalBufferCount; i++)
				{
					a.Append((char)this.InternalBuffer[i]);
				}

				this.InternalBufferCount = this.BaseStream.Read(this.InternalBuffer, 0, InternalBufferCapacity);

				if (this.InternalBufferCount > 0)
					continue;

				return a.ToString();
			}
		}

		void DiscardBuffer(int bytes)
		{
			if (bytes < 1)
				return;

			for (int i = bytes; i < this.InternalBufferCount; i++)
			{
				this.InternalBuffer[i - bytes] = this.InternalBuffer[i];
			}

			this.InternalBufferCount -= bytes;
		}

		public string ReadLine()
		{
			var a = new StringBuilder();

			var LineFeedExcpected = false;

			while (true)
			{
				for (int i = 0; i < this.InternalBufferCount; i++)
				{
					var c = (char)this.InternalBuffer[i];

					if (c == '\n')
					{
						DiscardBuffer(i + 1);
						return a.ToString();
					}
					else if (LineFeedExcpected)
					{
						DiscardBuffer(i);

						return a.ToString();
					}

					if (c == '\r')
					{
						LineFeedExcpected = true;
						continue;
					}

					a.Append(c);
				}

				this.InternalBufferCount = this.BaseStream.Read(this.InternalBuffer, 0, InternalBufferCapacity);

				if (this.InternalBufferCount > 0)
					continue;

				return a.ToString();
			}
		}

		public void ReadBlockTo(int length, StringBuilder w)
		{
			var bytes = new byte[length];
			this.Read(bytes, 0, length);

			for (int i = 0; i < length; i++)
			{
				w.Append((char)bytes[i]);
			}
		}
	}

}
