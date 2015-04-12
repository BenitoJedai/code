using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using ScriptCoreLibJava.BCLImplementation.System.Threading.Tasks;
using System.Diagnostics;

namespace ScriptCoreLibJava.BCLImplementation.System.Net.Sockets
{
    // http://referencesource.microsoft.com/#System/net/System/Net/Sockets/TcpClient.cs
    // https://github.com/mono/mono/blob/master/mcs/class/System/System.Net.Sockets/TcpClient.cs
    // x:\jsc.svn\core\scriptcorelibjava\bclimplementation\system\net\sockets\tcpclient.cs
    // X:\jsc.svn\market\synergy\javascript\chrome\chrome\BCLImplementation\System\Net\Sockets\TcpClient.cs

    [Script(Implements = typeof(global::System.Net.Sockets.TcpClient))]
    internal class __TcpClient : IDisposable
    {
        // what comes after tcp?
        // what about async API ?

        java.net.Socket InternalSocket
        {
            get
            {
                return ((__Socket)(object)Client).InternalSocket;
            }
        }

        public Socket Client { get; set; }

        public __TcpClient()
        {
            Client = (Socket)(object)new __Socket { InternalSocket = new java.net.Socket() };
        }

        public __TcpClient(Socket s)
        {
            Client = s;
        }


        #region Connect
        public Task ConnectAsync(string host, int port)
        {
            // X:\jsc.svn\examples\java\android\forms\InteractivePortForwarding\InteractivePortForwarding\UserControl1.cs
            var c = new TaskCompletionSource<object>();

            __Task.Run(
                delegate
                {

                    var sw = Stopwatch.StartNew();

                    try
                    {
                        //Caused by: java.net.SocketTimeoutException: Connection timed out
                        //       at org.apache.harmony.luni.platform.OSNetworkSystem.connect(Native Method)
                        //       at dalvik.system.BlockGuard$WrappedNetworkSystem.connect(BlockGuard.java:357)
                        //       at org.apache.harmony.luni.net.PlainSocketImpl.connect(PlainSocketImpl.java:204)
                        //       at org.apache.harmony.luni.net.PlainSocketImpl.connect(PlainSocketImpl.java:437)
                        //       at java.net.Socket.connect(Socket.java:1002)
                        //       at java.net.Socket.connect(Socket.java:945)
                        //       at ScriptCoreLibJava.BCLImplementation.System.Net.Sockets.__TcpClient.Connect(__TcpClient.java:96)

                        InternalSocket.setSoTimeout(150000);

                        InternalSocket.connect(new java.net.InetSocketAddress(host, port));

                        c.SetResult(null);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Connect fault " + new { host, port, sw.ElapsedMilliseconds, ex.Message, ex.StackTrace });

                        //throw;
                    }
                }
            );

            return c.Task;
        }

        public void Connect(IPAddress hostname, int port)
        {
            var sw = Stopwatch.StartNew();

            try
            {
                //Caused by: java.net.SocketTimeoutException: Connection timed out
                //       at org.apache.harmony.luni.platform.OSNetworkSystem.connect(Native Method)
                //       at dalvik.system.BlockGuard$WrappedNetworkSystem.connect(BlockGuard.java:357)
                //       at org.apache.harmony.luni.net.PlainSocketImpl.connect(PlainSocketImpl.java:204)
                //       at org.apache.harmony.luni.net.PlainSocketImpl.connect(PlainSocketImpl.java:437)
                //       at java.net.Socket.connect(Socket.java:1002)
                //       at java.net.Socket.connect(Socket.java:945)
                //       at ScriptCoreLibJava.BCLImplementation.System.Net.Sockets.__TcpClient.Connect(__TcpClient.java:96)

                InternalSocket.setSoTimeout(150000);

                InternalSocket.connect(new java.net.InetSocketAddress(((__IPAddress)(object)hostname).InternalAddress, port));
            }
            catch
            {
                Console.WriteLine("Connect fault " + new { hostname, port, sw.ElapsedMilliseconds });

                throw;
            }
        }

        public void Connect(string hostname, int port)
        {
            try
            {
                InternalSocket.connect(new java.net.InetSocketAddress(hostname, port));
            }
            catch
            {
                throw;
            }
        }
        #endregion



        NetworkStream CachedGetStream;
        public NetworkStream GetStream()
        {
            if (CachedGetStream == null)
                InternalGetNetworkStream();

            return CachedGetStream;
        }

        private void InternalGetNetworkStream()
        {
            try
            {
                // http://stackoverflow.com/questions/7297173/android-outputstreamwriter-didnt-send-the-data-after-flush-socket



                CachedGetStream = (NetworkStream)(object)new __NetworkStream
                {
                    InternalInputStream = InternalSocket.getInputStream(),
                    InternalOutputStream = InternalSocket.getOutputStream()
                };
            }
            catch
            {
                throw;
            }
        }

        public void Close()
        {
            try
            {
                InternalSocket.close();
            }
            catch
            {
                throw new InvalidOperationException();
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.Close();
        }

        #endregion
    }
}
