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
		// virtual is the new abstract


		public virtual int ReadTimeout { get; set; }

		public virtual long Seek(long offset, SeekOrigin origin) { return default(long); }

		public virtual void SetLength(long value) { }

		public virtual long Length { get; set; }

		public virtual long Position { get; set; }

		public virtual void Flush() { }

		public virtual void Close()
		{
			this.Flush();
		}




		#region Read
		public Func<Byte[], int, int, Task<int>> VirtualReadAsync;

		public virtual Task<int> ReadAsync(byte[] buffer, int offset, int count)
		{
			// X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerAsync\ChromeTCPServerAsync\Application.cs
			// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150304
			return VirtualReadAsync(buffer, offset, count);
		}

		public virtual int Read(byte[] buffer, int offset, int count) { return default(int); }

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
		public Func<byte[], int, int, Task> VirtualWriteAsync;

		public virtual Task WriteAsync(byte[] buffer, int offset, int count)
		{
			// x:\jsc.svn\market\synergy\javascript\chrome\chrome\bclimplementation\system\net\sockets\tcplistener.cs
			return VirtualWriteAsync(buffer, offset, count);
		}

		public virtual void Write(byte[] buffer, int offset, int count) { }

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
