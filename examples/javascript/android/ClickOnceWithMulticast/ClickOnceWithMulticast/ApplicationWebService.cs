using android.content;
using android.net.wifi;
using java.net;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Xml.Linq;

namespace ClickOnceWithMulticast
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y, Action done)
        {

            data.WithEach(y);


            done();
        }

        public void DownloadSDK(WebServiceHandler h)
        {
            DownloadSDKFunction.DownloadSDK(h);

        }

        static List<string> data = new List<string>();



#if DEBUG
           static ApplicationWebService()
        {

            Console.WriteLine("MulticastListener " + MulticastSettings.testSettings.Address);
            var receiver = new MulticastListener(MulticastSettings.testSettings);
            {

                receiver.StartListening(
                    bytes =>
                    {
                        var listen = Encoding.UTF8.GetString(bytes);


                        Console.WriteLine(new { listen });
                            data.AddDistinct(listen);


                    }
                );


                // await next click?
            }
            }
#else

        static WifiManager wifi;
        static WifiManager.MulticastLock multicastLock;

        static ApplicationWebService()
        {
            // http://www.zzzxo.com/q/answers-android-device-not-receiving-multicast-package-13221736.html



            new Thread(
                delegate()
                {
                    // http://stackoverflow.com/questions/12610415/multicast-receiver-malfunction
                    // http://answers.unity3d.com/questions/250732/android-build-is-not-receiving-udp-broadcasts.html

                    // Acquire multicast lock
                    wifi = (WifiManager)ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext.getSystemService(Context.WIFI_SERVICE);
                    multicastLock = wifi.createMulticastLock("multicastLock");
                    //multicastLock.setReferenceCounted(true);
                    multicastLock.acquire();

                    System.Console.WriteLine("LANBroadcastListener ready...");
                    try
                    {
                        byte[] b = new byte[0x100];

                        // https://code.google.com/p/android/issues/detail?id=40003

                        MulticastSocket socket = new MulticastSocket(40404); // must bind receive side
                        socket.setBroadcast(true);
                        socket.setReuseAddress(true);
                        socket.setTimeToLive(30);
                        socket.setReceiveBufferSize(0x100);


                        socket.joinGroup(InetAddress.getByName("239.1.2.3"));
                        System.Console.WriteLine("LANBroadcastListener joinGroup...");
                        while (true)
                        {
                            DatagramPacket dgram = new DatagramPacket((sbyte[])(object)b, b.Length);
                            socket.receive(dgram); // blocks until a datagram is received

                            var bytes = new MemoryStream((byte[])(object)dgram.getData(), 0, dgram.getLength());


                            var listen = Encoding.UTF8.GetString(bytes.ToArray());

                            Console.WriteLine(new { listen });

                            var skip = false;

                            foreach (var item in data)
                            {
                                if (item == listen)
                                    skip = true;

                            }

                            if (!skip)
                                data.Add(listen);

                            //                            { listen = http://www.fun2code.de/articles/multicast_images_java/multicast_images_java.html }
                            //client error, { Message = , StackTrace = java.lang.RuntimeException
                            //       at ScriptCoreLibJava.BCLImplementation.System.Collections.Generic.__List_1.Contains(__List_1.java:102)
                            //       at ScriptCoreLib.Extensions.ListExtensions.AddDistinct(ListExtensions.java:17)

                            //System.Console.WriteLine("Received "
                            //    + listen
                            //    + " bytes from " + dgram.getAddress());
                            //dgram.setLength(b.Length); // must reset length field!s



                        }
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine("client error, " + new { ex.Message, ex.StackTrace });
                    }
                }
            )
            {

                Name = "client"
            }.Start();
        }
#endif



    }



#if DEBUG

        // https://code.google.com/p/multicastdotnet/source/browse/#svn%2Ftrunk%2FMulticast%2FMulticast

    public class MulticastSettings
    {
//        Implementation not found for type import :
//type: System.Net.IPAddress
//method: System.Net.IPAddress Parse(System.String)
//Did you forget to add the [Script] attribute?

        public static MulticastSettings testSettings = new MulticastSettings()
        {
            Address = IPAddress.Parse("239.1.2.3"),
            Port = 40404,
            TimeToLive = 30
        };


        public IPAddress Address { get; set; }
        public int Port { get; set; }
        public int TimeToLive { get; set; }
    }

    public class MulticastListener : IDisposable
    {
        public event Action<byte[]> OnReceive;

        public MulticastSettings Settings { get; protected set; }

        public bool IsBound
        {
            get
            {
                return UdpClient.Client != null
                        ? UdpClient.Client.IsBound
                        : false;
            }
        }

        private UdpClient udpClient;
        public UdpClient UdpClient
        {
            get { return udpClient ?? (udpClient = new UdpClient()); }
        }

        public IPEndPoint LocalIPEndPoint { get; protected set; }

        public MulticastListener(MulticastSettings settings)
            : this(settings, true)
        { }

        public MulticastListener(MulticastSettings settings, bool autoBindJoinConnect)
        {
            if (settings == null) throw new ArgumentNullException("settings");

            Settings = settings;

            if (autoBindJoinConnect) BindAndJoin();
        }

        private void BindAndJoin()
        {
            try
            {
                LocalIPEndPoint = new IPEndPoint(IPAddress.Any, Settings.Port);
                UdpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                UdpClient.ExclusiveAddressUse = false;
                UdpClient.EnableBroadcast = true;

                UdpClient.Client.Bind(LocalIPEndPoint);
                UdpClient.JoinMulticastGroup(Settings.Address, Settings.TimeToLive);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void StartListening(Action<byte[]> handler)
        {
            try
            {
                if (!IsBound) BindAndJoin();

                OnReceive += handler;
                AsyncCallback receiveCallback = new AsyncCallback(ReceiveCallback);
                UdpClient.BeginReceive(receiveCallback, this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void StopListening()
        {
            OnReceive = null;
            if (IsBound) UnbindAndLeave();
        }

        private void UnbindAndLeave()
        {
            try
            {
                UdpClient.DropMulticastGroup(Settings.Address);
                UdpClient.Close();
            }
            catch (ObjectDisposedException)
            {
                // expected exception fired when we close - swallow it up
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                MulticastListener receiver = (MulticastListener)(ar.AsyncState);

                UdpClient udpClient = receiver.UdpClient;
                IPEndPoint ipEndPoint = receiver.LocalIPEndPoint;

                byte[] receiveBytes = udpClient.EndReceive(ar, ref ipEndPoint);
                OnReceive(receiveBytes);

                AsyncCallback receiveCallback = new AsyncCallback(ReceiveCallback);
                UdpClient.BeginReceive(receiveCallback, this);
            }
            catch (ObjectDisposedException)
            {
                // expected exception fired when we close - swallow it up
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            if (IsBound) UnbindAndLeave();
        }
    }
#endif

}
