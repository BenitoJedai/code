// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.net;
using java.nio.channels;

namespace java.nio.channels
{
    // http://docs.oracle.com/javase/1.5.0/docs/api/java/nio/channels/ServerSocketChannel.html
    // http://developer.android.com/reference/java/nio/channels/ServerSocketChannel.html
	[Script(IsNative = true)]
	public abstract class ServerSocketChannel
	{
		/// <summary>
		/// Accepts a connection made to this channel's socket.
		/// </summary>
		public SocketChannel accept()
		{
			return default(SocketChannel);
		}

		/// <summary>
		/// Opens a server-socket channel.
		/// </summary>
		public ServerSocketChannel open()
		{
			return default(ServerSocketChannel);
		}

		/// <summary>
		/// Retrieves a server socket associated with this channel.
		/// </summary>
		public ServerSocket socket()
		{
			return default(ServerSocket);
		}

		/// <summary>
		/// Returns an operation set identifying this channel's supported
		/// operations.
		/// </summary>
		public int validOps()
		{
			return default(int);
		}

	}
}

