using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Net;
using java.net;
using System.Net.Sockets;

namespace ScriptCoreLibJava.BCLImplementation.System.Net.Sockets
{
    // https://github.com/mono/mono/blob/effa4c07ba850bedbe1ff54b2a5df281c058ebcb/mcs/class/System/System.Net.Sockets/Socket.cs
    // http://referencesource.microsoft.com/#System/net/System/Net/Sockets/Socket.cs

    [Script(Implements = typeof(global::System.Net.Sockets.Socket))]
    internal class __Socket : IDisposable
    {
        public global::java.net.ServerSocket InternalServerSocket;
        public global::java.net.Socket InternalSocket;

        public void SetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, bool optionValue)
        {
      
            if (optionName == SocketOptionName.ReuseAddress)
            {
                if (this.InternalServerSocket != null)
                {
                    try
                    {
                        //Console.WriteLine("setReuseAddress... " + new { optionValue });

                        this.InternalServerSocket.setReuseAddress(optionValue);
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }

        public EndPoint RemoteEndPoint
        {
            get
            {
                // http://stackoverflow.com/questions/15130132/getlocalsocketaddress-and-getremotesocketaddress-not-returning-correct-value

                var RemoteSocketAddress = default(InetAddress);
                try
                {
                    //             Caused by: java.net.UnknownHostException: Unable to resolve host "/192.168.1.100:57999": No address associated with hostname
                    //at java.net.InetAddress.lookupHostByName(InetAddress.java:424)
                    //at java.net.InetAddress.getAllByNameImpl(InetAddress.java:236)
                    //at java.net.InetAddress.getByName(InetAddress.java:289)


                    var a = InternalSocket.getRemoteSocketAddress().ToString();

                    if (a.StartsWith("/"))
                        a = a.Substring(1);

                    {
                        var i = a.IndexOf(":");
                        if (i > 0)
                            a = a.Substring(0, i);
                    }
                    //               Caused by: java.net.UnknownHostException: 192.168.1.103/192.168.1.103
                    //      at java.net.InetAddress.lookupHostByName(InetAddress.java:506)
                    //      at java.net.InetAddress.getAllByNameImpl(InetAddress.java:294)
                    //      at java.net.InetAddress.getByName(InetAddress.java:325)
                    //      at ScriptCoreLibJava.BCLImplementation.System.Net.Sockets.__Socket.get_RemoteEndPoint(__Socket.java:53)
                    //      ... 20 more
                    //}

                    {
                        var i = a.IndexOf("/");
                        if (i > 0)
                            a = a.Substring(0, i);
                    }

                    RemoteSocketAddress = InetAddress.getByName(a);

                }
                catch
                {
                    throw;

                }
                //                [javac] V:\src\ScriptCoreLibJava\BCLImplementation\System\Net\Sockets\__Socket.java:33: unreported exception java.net.UnknownHostException; must be caught or declared to be thrown
                //[javac]         address0 = InetAddress.getByName(this.InternalSocket.getRemoteSocketAddress().toString());
                //[javac]                                         ^


                var e = new __IPEndPoint(
                    (IPAddress)(object)new __IPAddress
                    {
                        InternalAddress = RemoteSocketAddress
                    },

                        InternalSocket.getPort()
                );

                return (IPEndPoint)(object)e;
            }
        }

        public EndPoint LocalEndPoint
        {
            get
            {
                var e = new __IPEndPoint(
                    (IPAddress)(object)new __IPAddress
                    {
                        InternalAddress = InternalSocket.getLocalAddress()
                    },

                        InternalSocket.getLocalPort()
                );

                return (IPEndPoint)(object)e;
            }
        }

        public void Close()
        {
            if (this.InternalServerSocket != null)
            {
                try
                {
                    this.InternalServerSocket.close();
                }
                catch
                {
                    throw new InvalidOperationException();
                }
                return;
            }

            throw new InvalidOperationException();
        }

        #region IDisposable Members

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
