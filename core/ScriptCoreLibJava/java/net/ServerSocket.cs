// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;
using java.net;
using java.nio.channels;

namespace java.net
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/net/ServerSocket.html
	[Script(IsNative = true)]
	public class ServerSocket
	{
		/// <summary>
		/// Creates an unbound server socket.
		/// </summary>
		public ServerSocket()
		{
		}

		/// <summary>
		/// Creates a server socket, bound to the specified port.
		/// </summary>
		public ServerSocket(int @port)
		{
		}

		/// <summary>
		/// Creates a server socket and binds it to the specified local port
		/// number, with the specified backlog.
		/// </summary>
		public ServerSocket(int @port, int @backlog)
		{
		}

		/// <summary>
		/// Create a server with the specified port, listen backlog, and
		/// local IP address to bind to.
		/// </summary>
		public ServerSocket(int @port, int @backlog, InetAddress @bindAddr)
		{
		}

		/// <summary>
		/// Listens for a connection to be made to this socket and accepts
		/// it.
		/// </summary>
		public Socket accept()
		{
			return default(Socket);
		}

		/// <summary>
		/// Binds the <code>ServerSocket</code> to a specific address
		/// (IP address and port number).
		/// </summary>
		public void bind(SocketAddress @endpoint)
		{
		}

		/// <summary>
		/// Binds the <code>ServerSocket</code> to a specific address
		/// (IP address and port number).
		/// </summary>
		public void bind(SocketAddress @endpoint, int @backlog)
		{
		}

		/// <summary>
		/// Closes this socket.
		/// </summary>
		public void close()
		{
		}

		/// <summary>
		/// Returns the unique <A HREF="../../java/nio/channels/ServerSocketChannel.html" title="class in java.nio.channels"><CODE>ServerSocketChannel</CODE></A> object
		/// associated with this socket, if any.
		/// </summary>
		public ServerSocketChannel getChannel()
		{
			return default(ServerSocketChannel);
		}

		/// <summary>
		/// Returns the local address of this server socket.
		/// </summary>
		public InetAddress getInetAddress()
		{
			return default(InetAddress);
		}

		/// <summary>
		/// Returns the port on which this socket is listening.
		/// </summary>
		public int getLocalPort()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the address of the endpoint this socket is bound to, or
		/// <code>null</code> if it is not bound yet.
		/// </summary>
		public SocketAddress getLocalSocketAddress()
		{
			return default(SocketAddress);
		}

		/// <summary>
		/// Gets the value of the SO_RCVBUF option for this <tt>ServerSocket</tt>,
		/// that is the proposed buffer size that will be used for Sockets accepted
		/// from this <tt>ServerSocket</tt>.
		/// </summary>
		public int getReceiveBufferSize()
		{
			return default(int);
		}

		/// <summary>
		/// Tests if SO_REUSEADDR is enabled.
		/// </summary>
		public bool getReuseAddress()
		{
			return default(bool);
		}

		/// <summary>
		/// Retrive setting for SO_TIMEOUT.
		/// </summary>
		public int getSoTimeout()
		{
			return default(int);
		}

		/// <summary>
		/// Subclasses of ServerSocket use this method to override accept()
		/// to return their own subclass of socket.
		/// </summary>
		protected void implAccept(Socket @s)
		{
		}

		/// <summary>
		/// Returns the binding state of the ServerSocket.
		/// </summary>
		public bool isBound()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the closed state of the ServerSocket.
		/// </summary>
		public bool isClosed()
		{
			return default(bool);
		}

		/// <summary>
		/// Sets a default proposed value for the SO_RCVBUF option for sockets
		/// accepted from this <tt>ServerSocket</tt>.
		/// </summary>
		public void setReceiveBufferSize(int @size)
		{
		}

		/// <summary>
		/// Enable/disable the SO_REUSEADDR socket option.
		/// </summary>
		public void setReuseAddress(bool @on)
		{
		}

		/// <summary>
		/// Sets the server socket implementation factory for the
		/// application.
		/// </summary>
		static public void setSocketFactory(SocketImplFactory @fac)
		{
		}

		/// <summary>
		/// Enable/disable SO_TIMEOUT with the specified timeout, in
		/// milliseconds.
		/// </summary>
		public void setSoTimeout(int @timeout)
		{
		}

		/// <summary>
		/// Returns the implementation address and implementation port of
		/// this socket as a <code>String</code>.
		/// </summary>
		public override string ToString()
		{
			return default(string);
		}

	}
}

