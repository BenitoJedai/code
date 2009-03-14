using System.Net.Sockets;
using ScriptCoreLib.PHP.BCLImplementation.System.IO;
using System;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Net.Sockets
{
	[Script(Implements = typeof(global::System.Net.Sockets.NetworkStream))]
	internal class __NetworkStream : __Stream
	{
		internal Socket InternalSocket;

		public __NetworkStream(Socket Socket)
		{
			InternalSocket = Socket;
		}



		public override int Read(byte[] buffer, int offset, int count)
		{
			var InternalSocket = (__Socket)(object)this.InternalSocket;

			if (Native.API.feof(InternalSocket.InternalHandler))
				return -1;

			var x = Native.API.fread(InternalSocket.InternalHandler, count);

			var bytes = __File.ToBytes(x);

			for (int i = 0; i < bytes.Length; i++)
			{
				var v = bytes[i];
				//Console.WriteLine("__NetworkStream.Read: " + new { i, v });

				buffer[i] = v;
			}

			// old implementation:
			//for (int i = 0; i < x.Length; i++)
			//{
			//    buffer[i] = (byte)x[i];
			//}

			return x.Length;
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			var InternalSocket = (__Socket)(object)this.InternalSocket;

			var data = "";

			for (int i = offset; i < count; i++)
			{
				data += Native.API.chr(buffer[i]);
			}


			Native.API.fwrite(InternalSocket.InternalHandler, data);

		}

		public override long Length
		{
			get { throw new global::System.NotImplementedException(""); }
		}

		public override long Position
		{
			get
			{
				throw new global::System.NotImplementedException("");
			}
			set
			{
				// Note: Note that fread() reads from the current position of the 
				// file pointer. Use ftell() to find the current position of the 
				// pointer and rewind() to rewind the pointer position. 

				throw new global::System.NotImplementedException("");
			}
		}

		public override void SetLength(long value)
		{
			throw new global::System.NotImplementedException("");
		}

		public override long Seek(long offset, global::System.IO.SeekOrigin origin)
		{
			throw new global::System.NotImplementedException("");
		}
	}
}
