// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/java.net.NetworkInterface

using ScriptCoreLib;
using java.lang;
using java.util;
using System.Net.Sockets;

namespace java.net
{
    // http://docs.oracle.com/javase/1.5.0/docs/api/java/net/NetworkInterface.html
    // http://developer.android.com/reference/java/net/NetworkInterface.html
    [Script(IsNative = true)]
    public sealed class NetworkInterface
    {
        // X:\jsc.svn\examples\c\android\Test\TestHybridOVR\TestHybridOVR\OVRJVM\ApplicationActivity.cs
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Net\NetworkInformation\NetworkInterface.cs
        // Added in API level 9
        [ScriptMethodThrows(typeof(SocketException))]
        public sbyte[] getHardwareAddress()
        {
            return null;
        }

        /// <summary>
        /// Compares this object against the specified object.
        /// </summary>
        public override bool Equals(object @obj)
        {
            return default(bool);
        }

        /// <summary>
        /// Convenience method to search for a network interface that
        /// has the specified Internet Protocol (IP) address bound to
        /// it.
        /// </summary>
        static public NetworkInterface getByInetAddress(InetAddress @addr)
        {
            return default(NetworkInterface);
        }

        /// <summary>
        /// Searches for the network interface with the specified name.
        /// </summary>
        static public NetworkInterface getByName(string @name)
        {
            return default(NetworkInterface);
        }

        /// <summary>
        /// Get the display name of this network interface.
        /// </summary>
        public string getDisplayName()
        {
            return default(string);
        }

        /// <summary>
        /// Convenience method to return an Enumeration with all or a
        /// subset of the InetAddresses bound to this network interface.
        /// </summary>
        public Enumeration getInetAddresses()
        {
            return default(Enumeration);
        }

        /// <summary>
        /// Get the name of this network interface.
        /// </summary>
        public string getName()
        {
            return default(string);
        }

        /// <summary>
        /// Returns all the interfaces on this machine.
        /// </summary>
        static public Enumeration getNetworkInterfaces()
        {
            return default(Enumeration);
        }

        /// <summary>
        /// Returns a hash code value for the object.
        /// </summary>
        public override int GetHashCode()
        {
            return default(int);
        }

        /// <summary>
        /// Returns a string representation of the object.
        /// </summary>
        public override string ToString()
        {
            return default(string);
        }


        [ScriptMethodThrows(typeof(SocketException))]
        public bool isUp()
        {
            return default(bool);
        }

        [ScriptMethodThrows(typeof(SocketException))]
        public bool isVirtual()
        {
            return default(bool);
        }

        [ScriptMethodThrows(typeof(SocketException))]
        public bool supportsMulticast()
        {
            return default(bool);
        }
    }
}
