using ScriptCoreLib.ActionScript.flash.events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.net
{
    // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/net/ServerSocket.html
    [Script(IsNative = true)]
    public class ServerSocket : EventDispatcher
    {
        // http://lucamezzalira.com/2013/05/10/adobe-air-3-8-introduces-socket-server-on-ios-and-android/
        // X:\jsc.svn\examples\actionscript\air\AIRServerSocketExperiment\AIRServerSocketExperiment\ApplicationSprite.cs

        public void bind(int localPort)
        {
        }

        public void listen(int backlog)
        {
        }

        public void close()
        {
        }

        // tcp async server?
        // chrome app can create a server, now air can too.
        // can ios device know its ip in the lan?

        // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/net/ServerSocket.html#event:connect
        [method: Script(NotImplementedHere = true)]
        public event Action<ServerSocketConnectEvent> connect;

    }
}

namespace ScriptCoreLib.ActionScript.Extensions.flash.net
{

    [Script(Implements = typeof(ScriptCoreLib.ActionScript.flash.net.ServerSocket))]
    internal static class __ServerSocket
    {


        #region Implementation for methods marked with [Script(NotImplementedHere = true)]
        #region timer
        public static void add_connect(ScriptCoreLib.ActionScript.flash.net.ServerSocket that, Action<ServerSocketConnectEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, ServerSocketConnectEvent.CONNECT);
        }

        public static void remove_connect(ScriptCoreLib.ActionScript.flash.net.ServerSocket that, Action<ServerSocketConnectEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, ServerSocketConnectEvent.CONNECT);
        }
        #endregion


        #endregion
    }
}

