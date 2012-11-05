using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript
{
    [Script(IsStringEnum = true)]
    public enum SocketType
    {
        tcp,
        udp
    }

    public static partial class chrome
    {
        // http://developer.chrome.com/trunk/apps/socket.html#method-listen
        public static partial class socket
        {
            public static void create(SocketType type, CreateOptions options, Action<CreateInfo> CreateCallback)
            {
            }

            public static void listen(int socketId, string address, int port, int backlog, Action<int> ListenCallback)
            {

            }

            public static void accept(int socketId, Action<AcceptInfo> AcceptCallback)
            {
            }
        }
    }
}
