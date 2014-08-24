using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Net;
using System.Net.Sockets;
using java.lang;
using System.Threading.Tasks;

namespace ScriptCoreLibJava.BCLImplementation.System.Net.Sockets
{
    // http://referencesource.microsoft.com/#System/net/System/Net/Sockets/TCPListener.cs
    // https://github.com/mono/mono/blob/effa4c07ba850bedbe1ff54b2a5df281c058ebcb/mcs/class/System/System.Net.Sockets/TcpListener.cs

    [Script(Implements = typeof(global::System.Net.Sockets.TcpListener))]
    internal class __TcpListener
    {
        // tested by ?
        // when can we do Android, CLR and Chrome webservers via SSL ?


        public global::java.net.ServerSocket InternalSocket;
        public __IPAddress localaddr;
        public int port;

        public __TcpListener(IPAddress localaddr, int port)
        {
            this.localaddr = (__IPAddress)(object)localaddr;
            this.port = port;



            try
            {
                // http://stackoverflow.com/questions/6090891/what-is-socket-bind-and-how-to-bind-an-address
                this.InternalSocket = new global::java.net.ServerSocket();
            }
            catch
            {
                throw;
            }

            this.Server = (Socket)(object)new __Socket { InternalServerSocket = this.InternalSocket };

        }



        public Socket Server
        {
            get;
            set;
        }

        #region Start
        public void Start()
        {
            this.Start(0x7fffffff);
        }

        public void Start(int backlog)
        {
            try
            {
                //this.InternalSocket = new global::java.net.ServerSocket(this.port, backlog, this.localaddr.InternalAddress);

                //         Caused by: java.net.SocketException: Socket is not bound
                //at java.net.ServerSocket.accept(ServerSocket.java:122)

                // http://stackoverflow.com/questions/10516030/java-server-socket-doesnt-reuse-address
                this.InternalSocket.bind(
                    new java.net.InetSocketAddress(
                        this.localaddr.InternalAddress,
                        port
                    ),

                    backlog
                );

            }
            catch
            {
                throw;
            }

        }
        #endregion

        public void Stop()
        {
            try
            {
                this.InternalSocket.close();
                this.InternalSocket = null;
            }
            catch
            {
                throw;
            }
        }




        // NET45
        public Task<Socket> AcceptSocketAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TcpClient> AcceptTcpClientAsync()
        {
            throw new NotImplementedException();
        }

        public Socket AcceptSocket()
        {
            if (InternalSocket == null)
                throw new InvalidOperationException(
                    //"Not listening. You must call the Start() method before calling this method."
                );

            var r = default(__Socket);

            try
            {
                r = new __Socket { InternalSocket = this.InternalSocket.accept() };
            }
            catch
            {
                throw;
            }

            return (Socket)(object)r;
        }

        public TcpClient AcceptTcpClient()
        {
            var s = AcceptSocket();
            var r = new __TcpClient(s);

            return (TcpClient)(object)r;
        }
    }
}
