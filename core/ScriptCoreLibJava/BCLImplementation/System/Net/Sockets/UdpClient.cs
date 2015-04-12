using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using ScriptCoreLib.Shared.BCLImplementation.System.Net.Sockets;
using ScriptCoreLibJava.BCLImplementation.System.Threading.Tasks;

namespace ScriptCoreLibJava.BCLImplementation.System.Net.Sockets
{
    // http://referencesource.microsoft.com/#System/net/System/Net/Sockets/UdpClient.cs
    // https://github.com/mono/mono/blob/master/mcs/class/System/System.Net.Sockets/UdpClient.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Net\Sockets\UdpClient.cs
    // X:\jsc.svn\market\synergy\javascript\chrome\chrome\BCLImplementation\System\Net\Sockets\UdpClient.cs

    [Script(Implements = typeof(global::System.Net.Sockets.UdpClient))]
    internal class __UdpClient
    {
        // what comes after tcp?
        // what about async API ?

        // X:\jsc.svn\examples\java\hybrid\JVMCLRUDPReceiveAsync\JVMCLRUDPReceiveAsync\Program.cs
        // X:\jsc.svn\examples\java\hybrid\JVMCLRUDPSendAsync\JVMCLRUDPSendAsync\Program.cs
        // X:\jsc.svn\examples\java\ConsoleMulticastExperiment\ConsoleMulticastExperiment\Program.cs
        // X:\jsc.svn\examples\java\android\AndroidServiceUDPNotification\AndroidServiceUDPNotification\ApplicationActivity.cs
        // X:\jsc.svn\examples\java\android\LANBroadcastListener\LANBroadcastListener\ApplicationActivity.cs

        // X:\jsc.svn\core\ScriptCoreLibAndroidNDK\ScriptCoreLibAndroidNDK\SystemHeaders\sys\socket.cs

        static java.net.DatagramSocket try_new_DatagramSocket()
        {
            #region datagramSocket
            var datagramSocket = default(java.net.DatagramSocket);

            try
            {
                // http://developer.android.com/reference/java/net/DatagramSocket.html
                // Constructs a UDP datagram socket which is bound to any available port on the localhost.
                datagramSocket = new java.net.DatagramSocket();
            }
            catch
            {
                throw;
            }
            #endregion

            return datagramSocket;
        }

        static java.net.DatagramSocket try_new_DatagramSocket(int port)
        {
            #region datagramSocket
            var datagramSocket = default(java.net.DatagramSocket);

            try
            {
                // http://developer.android.com/reference/java/net/DatagramSocket.html
                // Constructs a UDP datagram socket which is bound to the specific port aPort on the localhost. Valid values for aPort are between 0 and 65535 inclusive.
                datagramSocket = new java.net.DatagramSocket(port);
            }
            catch
            {
                throw;
            }
            #endregion

            return datagramSocket;
        }



        public __UdpClient(java.net.DatagramSocket datagramSocket)
        {
            //var buffer = new sbyte[0x10000];
            var buffer = new sbyte[0x1000];

            //E/dalvikvm-heap(14366): Out of memory on a 1048592-byte allocation.
            //I/dalvikvm(14366): "Thread-4310" prio=5 tid=827 RUNNABLE


            #region vReceiveAsync
            this.vReceiveAsync = delegate
            {
                var c = new TaskCompletionSource<__UdpReceiveResult>();

                __Task.Run(
                    delegate
                    {
                        // http://stackoverflow.com/questions/10808512/datagrampacket-equivalent
                        // http://tutorials.jenkov.com/java-networking/udp-datagram-sockets.html


 
                        var packet = new java.net.DatagramPacket(buffer, buffer.Length);

                        try
                        {
                            datagramSocket.receive(packet);


                            var xbuffer = new byte[packet.getLength()];


                            Array.Copy(
                                buffer, xbuffer,
                                xbuffer.Length
                            );

                            var x = new __UdpReceiveResult(
                                buffer:
                                    xbuffer,

                                remoteEndPoint:
                                    new __IPEndPoint(
                                        new __IPAddress { InternalAddress = packet.getAddress() },
                                        port: packet.getPort()
                                    )
                            );

                            c.SetResult(x);
                        }
                        catch
                        {
                            // fault? 
                        }
                    }
                );


                return c.Task;
            };
            #endregion

            #region vSendAsync
            this.vSendAsync = (byte[] datagram, int bytes, string hostname, int port) =>
            {
                var c = new TaskCompletionSource<int>();
                __Task.Run(
                    delegate
                    {
                        try
                        {
                            var a = global::java.net.InetAddress.getByName(hostname);
                            var packet = new java.net.DatagramPacket(
                                (sbyte[])(object)datagram,
                                datagram.Length, a, port
                            );
                            datagramSocket.send(packet);
                            // retval tested?
                            c.SetResult(
                                packet.getLength()
                            );
                        }
                        catch
                        {
                            throw;
                        }
                    }
                );
                return c.Task;
            };
            #endregion

            #region vSendAsync2
            this.vSendAsync2 = (byte[] datagram, int bytes, IPEndPoint endPoint) =>
            {
                var c = new TaskCompletionSource<int>();
                __Task.Run(
                    delegate
                    {
                        try
                        {
                            var packet = new java.net.DatagramPacket(
                                (sbyte[])(object)datagram,
                                datagram.Length, (__IPAddress)endPoint.Address, endPoint.Port
                            );
                            datagramSocket.send(packet);
                            // retval tested?
                            c.SetResult(
                                packet.getLength()
                            );
                        }
                        catch
                        {
                            throw;
                        }
                    }
                );
                return c.Task;
            };
            #endregion

            #region vClose
            this.vClose = delegate
            {
                try
                {
                    datagramSocket.close();
                }
                catch
                {
                }
            };
            #endregion

        }

        public __UdpClient()
            : this(try_new_DatagramSocket())
        {

        }

        public __UdpClient(int port)
            : this(try_new_DatagramSocket(port))
        {


        }

        public Socket Client { get; set; }


        public Action vClose;
        public void Close() { vClose(); }


        // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Net\Sockets\UdpReceiveResult.cs
        public Func<Task<__UdpReceiveResult>> vReceiveAsync;
        public Task<__UdpReceiveResult> ReceiveAsync() { return vReceiveAsync(); }





        public SendAsyncDelegate vSendAsync;
        [Script]
        public delegate Task<int> SendAsyncDelegate(byte[] datagram, int bytes, string hostname, int port);
        public Task<int> SendAsync(byte[] datagram, int bytes, string hostname, int port) { return vSendAsync(datagram, bytes, hostname, port); }


        public SendAsyncDelegate2 vSendAsync2;
        [Script]
        public delegate Task<int> SendAsyncDelegate2(byte[] datagram, int bytes, IPEndPoint endPoint);
        public Task<int> SendAsync(byte[] datagram, int bytes, IPEndPoint endPoint) { return vSendAsync2(datagram, bytes, endPoint); }
    }
}
