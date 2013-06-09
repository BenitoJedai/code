using android.content;
using android.net.wifi;
using android.os;
using java.net;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;

namespace WithClickOnceLANLauncher
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
        public void WebMethod2(string e, Action<string> y)
        {
            // Send it back to the caller.
            y(e);
        }

        public void Vibrate(string e)
        { 
#if !DEBUG
            var vibrator = (Vibrator)
            ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext.getSystemService(Context.VIBRATOR_SERVICE);

            vibrator.vibrate(600);

#endif
        }

        public void DownloadSDK(WebServiceHandler h)
        {
            var HostUri = new
            {
                Host = h.Context.Request.Headers["Host"].TakeUntilIfAny(":"),
                Port = int.Parse(h.Context.Request.Headers["Host"].SkipUntilIfAny(":"))
            };


#if DEBUG
            if (InternalMulticast == null)
                InternalMulticast = new ApplicationWebServiceMulticast
                {
                    Host = HostUri.Host,
                    Port = HostUri.Port,

                };
#else
            if (InternalMulticast == null)
                InternalMulticast = new AndroidApplicationWebServiceMulticast
                {
                    Host = HostUri.Host,
                    Port = HostUri.Port,

                };
#endif

            DownloadSDKFunction.DownloadSDK(h);

        }

#if DEBUG
        static ApplicationWebServiceMulticast InternalMulticast;
#else
        static AndroidApplicationWebServiceMulticast InternalMulticast;

#endif

    }

    class AndroidApplicationWebServiceMulticast : Component
    {
        WifiManager wifi;
        WifiManager.MulticastLock multicastLock;

        public event Action<string> AtData;

        public AndroidApplicationWebServiceMulticast()
        {
            AtData += AndroidApplicationWebServiceMulticast_AtData;

            new Thread(
                delegate()
                {
                    // http://stackoverflow.com/questions/12610415/multicast-receiver-malfunction
                    // http://answers.unity3d.com/questions/250732/android-build-is-not-receiving-udp-broadcasts.html

                    // Acquire multicast lock
                    wifi = (WifiManager)
                        ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext.getSystemService(Context.WIFI_SERVICE);
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



                            //dgram.setLength(b.Length); // must reset length field!s

                            if (AtData != null)
                                AtData(listen);

                        }
                    }
                    catch
                    {
                        System.Console.WriteLine("client error");
                    }
                }
            )
            {

                Name = "client"
            }.Start();


        }

        void AndroidApplicationWebServiceMulticast_AtData(string listen)
        {
            Console.WriteLine(

               new { server = new { listen } }
               );

            try
            {
                var xml = XElement.Parse(listen);

                if (xml.Value.StartsWith("Where are you?"))
                {
                    this.Send(
                        "Visit me at " + this.Host + ":" + this.Port
                    );

                }
            }
            catch
            {

            }


        }

        int c;
        void Send(string data)
        {
            /// http://www.daniweb.com/software-development/java/threads/424998/udp-client-server-in-java

            c++;

            //var n = c + " hello world";
            var n =
                new XElement("string",
                    new XAttribute("c", "" + c),
                    data
                ).ToString();

            new Thread(
                delegate()
                {
                    try
                    {
                        DatagramSocket socket = new DatagramSocket(); //construct a datagram socket and binds it to the available port and the localhos
                        byte[] b = Encoding.UTF8.GetBytes(n.ToString());    //creates a variable b of type byte
                        DatagramPacket dgram;
                        dgram = new DatagramPacket((sbyte[])(object)b, b.Length, InetAddress.getByName("239.1.2.3"), 40404);//sends the packet details, length of the packet,destination address and the port number as parameters to the DatagramPacket  

                        socket.send(dgram); //send the datagram packet from this port
                    }
                    catch
                    {
                        System.Console.WriteLine("server error");
                    }
                }
            )
            {

                Name = "server"
            }.Start();
        }

        public int Port { get; set; }
        public string Host { get; set; }

    }
}
