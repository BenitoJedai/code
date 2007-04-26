using ScriptCoreLib;

using java.io;

namespace java.net
{
    /// <summary>
    /// http://java.sun.com/j2se/1.4.2/docs/api/java/net/Socket.html
    /// </summary>
    [Script(IsNative = true)]
    public class Socket
    {
        public Socket()
        {
        }

        public Socket(string host, int port)
        {
        }

        #region methods
        /// <summary>
        /// Binds the socket to a local address.
        /// </summary>
        public void bind(SocketAddress bindpoint)
        {
        }

        /// <summary>
        /// Closes this socket.
        /// </summary>
        public void close()
        {
        }

        /// <summary>
        /// Connects this socket to the server.
        /// </summary>
        public void connect(SocketAddress endpoint)
        {
        }

        /// <summary>
        /// Connects this socket to the server with a specified timeout value.
        /// </summary>
        public void connect(SocketAddress endpoint, int timeout)
        {
        }

        /// <summary>
        /// Returns the unique <A HREF="../../java/nio/channels/SocketChannel.html" title="class in java.nio.channels"><CODE>SocketChannel</CODE></A> object associated with this socket, if any.
        /// </summary>
        public SocketChannel getChannel()
        {
            return default(SocketChannel);
        }

        /// <summary>
        /// Returns the address to which the socket is connected.
        /// </summary>
        public InetAddress getInetAddress()
        {
            return default(InetAddress);
        }

        /// <summary>
        /// Returns an input stream for this socket.
        /// </summary>
        public InputStream getInputStream()
        {
            return default(InputStream);
        }

        /// <summary>
        /// Tests if SO_KEEPALIVE is enabled.
        /// </summary>
        public bool getKeepAlive()
        {
            return default(bool);
        }

        /// <summary>
        /// Gets the local address to which the socket is bound.
        /// </summary>
        public InetAddress getLocalAddress()
        {
            return default(InetAddress);
        }

        /// <summary>
        /// Returns the local port to which this socket is bound.
        /// </summary>
        public int getLocalPort()
        {
            return default(int);
        }

        /// <summary>
        /// Returns the address of the endpoint this socket is bound to, or <code>null</code> if it is not bound yet.
        /// </summary>
        public SocketAddress getLocalSocketAddress()
        {
            return default(SocketAddress);
        }

        /// <summary>
        /// Tests if OOBINLINE is enabled.
        /// </summary>
        public bool getOOBInline()
        {
            return default(bool);
        }

        /// <summary>
        /// Returns an output stream for this socket.
        /// </summary>
        public OutputStream getOutputStream()
        {
            return default(OutputStream);
        }

        /// <summary>
        /// Returns the remote port to which this socket is connected.
        /// </summary>
        public int getPort()
        {
            return default(int);
        }

        /// <summary>
        /// Gets the value of the SO_RCVBUF option for this <tt>Socket</tt>,  that is the buffer size used by the platform for  input on this <tt>Socket</tt>.
        /// </summary>
        public int getReceiveBufferSize()
        {
            return default(int);
        }

        /// <summary>
        /// Returns the address of the endpoint this socket is connected to, or <code>null</code> if it is unconnected.
        /// </summary>
        public SocketAddress getRemoteSocketAddress()
        {
            return default(SocketAddress);
        }

        /// <summary>
        /// Tests if SO_REUSEADDR is enabled.
        /// </summary>
        public bool getReuseAddress()
        {
            return default(bool);
        }

        /// <summary>
        /// Get value of the SO_SNDBUF option for this <tt>Socket</tt>,  that is the buffer size used by the platform  for output on this <tt>Socket</tt>.
        /// </summary>
        public int getSendBufferSize()
        {
            return default(int);
        }

        /// <summary>
        /// Returns setting for SO_LINGER.
        /// </summary>
        public int getSoLinger()
        {
            return default(int);
        }

        /// <summary>
        /// Returns setting for SO_TIMEOUT.
        /// </summary>
        public int getSoTimeout()
        {
            return default(int);
        }

        /// <summary>
        /// Tests if TCP_NODELAY is enabled.
        /// </summary>
        public bool getTcpNoDelay()
        {
            return default(bool);
        }

        /// <summary>
        /// Gets traffic class or type-of-service in the IP header for packets sent from this Socket
        /// </summary>
        public int getTrafficClass()
        {
            return default(int);
        }

        /// <summary>
        /// Returns the binding state of the socket.
        /// </summary>
        public bool isBound()
        {
            return default(bool);
        }

        /// <summary>
        /// Returns the closed state of the socket.
        /// </summary>
        public bool isClosed()
        {
            return default(bool);
        }

        /// <summary>
        /// Returns the connection state of the socket.
        /// </summary>
        public bool isConnected()
        {
            return default(bool);
        }

        /// <summary>
        /// Returns wether the read-half of the socket connection is closed.
        /// </summary>
        public bool isInputShutdown()
        {
            return default(bool);
        }

        /// <summary>
        /// Returns wether the write-half of the socket connection is closed.
        /// </summary>
        public bool isOutputShutdown()
        {
            return default(bool);
        }

        /// <summary>
        /// Send one byte of urgent data on the socket.
        /// </summary>
        public void sendUrgentData(int data)
        {
        }

        /// <summary>
        /// Enable/disable SO_KEEPALIVE.
        /// </summary>
        public void setKeepAlive(bool on)
        {
        }

        /// <summary>
        /// Enable/disable OOBINLINE (receipt of TCP urgent data) By default, this option is disabled and TCP urgent data received on a  socket is silently discarded.
        /// </summary>
        public void setOOBInline(bool on)
        {
        }

        /// <summary>
        /// Sets the SO_RCVBUF option to the specified value for this <tt>Socket</tt>.
        /// </summary>
        public void setReceiveBufferSize(int size)
        {
        }

        /// <summary>
        /// Enable/disable the SO_REUSEADDR socket option.
        /// </summary>
        public void setReuseAddress(bool on)
        {
        }

        /// <summary>
        /// Sets the SO_SNDBUF option to the specified value for this <tt>Socket</tt>.
        /// </summary>
        public void setSendBufferSize(int size)
        {
        }

        /// <summary>
        /// Sets the client socket implementation factory for the application.
        /// </summary>
        public static void setSocketImplFactory(SocketImplFactory fac)
        {
        }

        /// <summary>
        /// Enable/disable SO_LINGER with the specified linger time in seconds.
        /// </summary>
        public void setSoLinger(bool on, int linger)
        {
        }

        /// <summary>
        /// Enable/disable SO_TIMEOUT with the specified timeout, in  milliseconds.
        /// </summary>
        public void setSoTimeout(int timeout)
        {
        }

        /// <summary>
        /// Enable/disable TCP_NODELAY (disable/enable Nagle's algorithm).
        /// </summary>
        public void setTcpNoDelay(bool on)
        {
        }

        /// <summary>
        /// Sets traffic class or type-of-service octet in the IP header for packets sent from this Socket.
        /// </summary>
        public void setTrafficClass(int tc)
        {
        }

        /// <summary>
        /// Places the input stream for this socket at "end of stream".
        /// </summary>
        public void shutdownInput()
        {
        }

        /// <summary>
        /// Disables the output stream for this socket.
        /// </summary>
        public void shutdownOutput()
        {
        }

        #endregion

    }
}
