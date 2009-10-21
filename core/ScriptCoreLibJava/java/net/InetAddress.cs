// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/java.net.InetAddress

using ScriptCoreLib;
using java.lang;

namespace java.net
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/net/InetAddress.html
	[Script(IsNative = true)]
	public class InetAddress
	{
		/// <summary>
		/// Compares this object against the specified object.
		/// </summary>
		public override bool Equals(object @obj)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the raw IP address of this <code>InetAddress</code>
		/// object.
		/// </summary>
		public sbyte[] getAddress()
		{
			return default(sbyte[]);
		}

		/// <summary>
		/// Given the name of a host, returns an array of its IP addresses,
		/// based on the configured name service on the system.
		/// </summary>
		static public InetAddress[] getAllByName(string @host)
		{
			return default(InetAddress[]);
		}

		/// <summary>
		/// Returns an <code>InetAddress</code> object given the raw IP address .
		/// </summary>
		static public InetAddress getByAddress(sbyte[] @addr)
		{
			return default(InetAddress);
		}

		/// <summary>
		/// Create an InetAddress based on the provided host name and IP address
		/// No name service is checked for the validity of the address.
		/// </summary>
		static public InetAddress getByAddress(string @host, sbyte[] @addr)
		{
			return default(InetAddress);
		}

		/// <summary>
		/// Determines the IP address of a host, given the host's name.
		/// </summary>
		static public InetAddress getByName(string @host)
		{
			return default(InetAddress);
		}

		/// <summary>
		/// Gets the fully qualified domain name for this IP address.
		/// </summary>
		public string getCanonicalHostName()
		{
			return default(string);
		}

		/// <summary>
		/// Returns the IP address string in textual presentation.
		/// </summary>
		public string getHostAddress()
		{
			return default(string);
		}

		/// <summary>
		/// Gets the host name for this IP address.
		/// </summary>
		public string getHostName()
		{
			return default(string);
		}

		/// <summary>
		/// Returns the local host.
		/// </summary>
		static public InetAddress getLocalHost()
		{
			return default(InetAddress);
		}

		/// <summary>
		/// Returns a hashcode for this IP address.
		/// </summary>
		public override int GetHashCode()
		{
			return default(int);
		}

		/// <summary>
		/// Utility routine to check if the InetAddress in a wildcard address.
		/// </summary>
		public bool isAnyLocalAddress()
		{
			return default(bool);
		}

		/// <summary>
		/// Utility routine to check if the InetAddress is an link local address.
		/// </summary>
		public bool isLinkLocalAddress()
		{
			return default(bool);
		}

		/// <summary>
		/// Utility routine to check if the InetAddress is a loopback address.
		/// </summary>
		public bool isLoopbackAddress()
		{
			return default(bool);
		}

		/// <summary>
		/// Utility routine to check if the multicast address has global scope.
		/// </summary>
		public bool isMCGlobal()
		{
			return default(bool);
		}

		/// <summary>
		/// Utility routine to check if the multicast address has link scope.
		/// </summary>
		public bool isMCLinkLocal()
		{
			return default(bool);
		}

		/// <summary>
		/// Utility routine to check if the multicast address has node scope.
		/// </summary>
		public bool isMCNodeLocal()
		{
			return default(bool);
		}

		/// <summary>
		/// Utility routine to check if the multicast address has organization scope.
		/// </summary>
		public bool isMCOrgLocal()
		{
			return default(bool);
		}

		/// <summary>
		/// Utility routine to check if the multicast address has site scope.
		/// </summary>
		public bool isMCSiteLocal()
		{
			return default(bool);
		}

		/// <summary>
		/// Utility routine to check if the InetAddress is an
		/// IP multicast address.
		/// </summary>
		public bool isMulticastAddress()
		{
			return default(bool);
		}

		/// <summary>
		/// Utility routine to check if the InetAddress is a site local address.
		/// </summary>
		public bool isSiteLocalAddress()
		{
			return default(bool);
		}

		/// <summary>
		/// Converts this IP address to a <code>String</code>.
		/// </summary>
		public override string ToString()
		{
			return default(string);
		}

	}
}
