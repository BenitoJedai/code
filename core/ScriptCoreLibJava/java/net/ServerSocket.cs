using ScriptCoreLib;

namespace java.net
{
    /// <summary>
    /// http://java.sun.com/j2se/1.4.2/docs/api/java/net/ServerSocket.html
    /// </summary>
    [Script(IsNative = true)]
    public class ServerSocket
    {
        public ServerSocket(int port)
        {

        }

        public Socket accept()
        {
            return default(Socket);
        }
    }
}
