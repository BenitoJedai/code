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
			throw new global::System.NotImplementedException("");
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new global::System.NotImplementedException("");
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
				throw new global::System.NotImplementedException("");
			}
		}
	}
}
