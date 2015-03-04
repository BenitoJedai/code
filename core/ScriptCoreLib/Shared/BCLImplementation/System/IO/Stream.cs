using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;
using System.IO;
using ScriptCoreLib.Shared.BCLImplementation.System;
using System.Threading.Tasks;

namespace ScriptCoreLib.Shared.BCLImplementation.System.IO
{
	// http://referencesource.microsoft.com/#mscorlib/system/io/stream.cs
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/IO/Stream.cs
	// https://github.com/mono/mono/blob/master/mcs/class/corlib/System.IO/Stream.cs

	// NEW_EXPERIMENTAL_ASYNC_IO
	[Script(Implements = typeof(global::System.IO.Stream))]
	public abstract class __Stream : __MarshalByRefObject, IDisposable
	{
		public virtual int ReadTimeout { get; set; }


		public abstract long Seek(long offset, SeekOrigin origin);

		public abstract void SetLength(long value);

		public abstract long Length { get; }

		public abstract long Position { get; set; }

		public abstract void Flush();

		public virtual void Close()
		{
			this.Flush();
		}




		#region Read
		public virtual Task<int> ReadAsync(Byte[] buffer, int offset, int count)
		{
			// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150304
			throw new NotImplementedException();
		}

		public abstract int Read(byte[] buffer, int offset, int count);

		public virtual int ReadByte()
		{
			var buffer = new byte[1];
			var i = Read(buffer, 0, 1);

			if (i < 0)
				return i;

			return buffer[0];
		}
		#endregion

		#region Write
		public Task WriteAsync(Byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		public abstract void Write(byte[] buffer, int offset, int count);

		public virtual void WriteByte(byte value)
		{
			this.Write(new[] { value }, 0, 1);
		}
		#endregion


		#region CopyTo
		public virtual Task CopyToAsync(Stream destination)
		{
			//return CopyToAsync(destination, _DefaultCopyBufferSize);
			// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150304
			throw new NotImplementedException();
		}

		public void CopyTo(Stream destination)
		{
			//Console.WriteLine("__Stream.CopyTo");

			var buffer = new byte[0x4000];

			var flag = true;
			while (flag)
			{
				flag = false;
				var c = this.Read(buffer, 0, buffer.Length);

				//Console.WriteLine("__Stream.CopyTo " + new { c });

				if (c > 0)
				{
					destination.Write(buffer, 0, c);
					flag = true;
				}
			}
		}
		#endregion


		public void Dispose()
		{
			//            Implementation not found for type import :
			//type: System.IO.Stream
			//method: Void Dispose()
			//Did you forget to add the [Script] attribute?
			//Please double check the signature!

			this.Close();
		}
	}
}
