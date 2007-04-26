using ScriptCoreLib;

using java.lang;

namespace java.net
{
    [Script(IsNative=true)]
    public class InetSocketAddress:SocketAddress
    {
        public InetSocketAddress(string hostname, int port)
        {
        }

        #region methods
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
        /// Checks wether the address has been resolved or not.
        /// </summary>
        public bool isUnresolved()
        {
            return default(bool);
        }

        #endregion

    }
}
