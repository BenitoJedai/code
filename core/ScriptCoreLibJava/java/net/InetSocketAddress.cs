// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;
using java.net;

namespace java.net
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/net/InetSocketAddress.html
	[Script(IsNative = true)]
	public class InetSocketAddress : SocketAddress
	{
		/// <summary>
		/// Creates a socket address from an IP address and a port number.
		/// </summary>
		public InetSocketAddress(InetAddress @addr, int @port)
		{
		}

		/// <summary>
		/// Creates a socket address where the IP address is the wildcard address
		/// and the port number a specified value.
		/// </summary>
		public InetSocketAddress(int @port)
		{
		}

		/// <summary>
		/// Creates a socket address from a hostname and a port number.
		/// </summary>
		public InetSocketAddress(string @hostname, int @port)
		{
		}

		/// <summary>
		/// Compares this object against the specified object.
		/// </summary>
		public bool equals(Object @obj)
		{
			return default(bool);
		}

		/// <summary>
		/// Gets the <code>InetAddress</code>.
		/// </summary>
		public InetAddress getAddress()
		{
			return default(InetAddress);
		}

		/// <summary>
		/// Gets the <code>hostname</code>.
		/// </summary>
		public string getHostName()
		{
			return default(string);
		}

		/// <summary>
		/// Gets the port number.
		/// </summary>
		public int getPort()
		{
			return default(int);
		}

		/// <summary>
		/// Returns a hashcode for this socket address.
		/// </summary>
		public override int GetHashCode()
		{
			return default(int);
		}

		/// <summary>
		/// Checks wether the address has been resolved or not.
		/// </summary>
		public bool isUnresolved()
		{
			return default(bool);
		}

		/// <summary>
		/// Constructs a string representation of this InetSocketAddress.
		/// </summary>
		public override string ToString()
		{
			return default(string);
		}

	}
}

