using System.Net.Sockets;
using ScriptCoreLib.PHP.BCLImplementation.System.IO;

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

			for (int i = 0; i < count; i++)
			{
				buffer[i] = (byte)
					Native.API.ord(
						Native.API.fread(InternalSocket.InternalHandler, 1)
					);
			}

			return count;
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			var InternalSocket = (__Socket)(object)this.InternalSocket;

			foreach (byte x in buffer)
			{
				Native.API.fwrite(
					InternalSocket.InternalHandler,
					Native.API.chr(x)
				);
			}

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
	}
}
