using android.content;
using android.net.wifi;
using java.net;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;

namespace com.abstractatech.scholar
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService :
        // can we do explicit implementations too?
        Abstractatech.JavaScript.FileStorage.IApplicationWebService
    {
        // jsc does not yet look deep enough
        Type ref0 = typeof(System.Data.SQLite.SQLiteCommand);
        Type ref1 = typeof(ScriptCoreLib.Shared.Data.DynamicDataReader);

        // { Message = Could not load file or assembly 'ScriptCoreLib.Extensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified., StackTrace =    at Abstractatech.JavaScript.FileStorage.Schema.XX.WithEach(SQLiteDataReader reader, Action`1 y)







        #region service
        public Abstractatech.JavaScript.FileStorage.ApplicationWebService service = new Abstractatech.JavaScript.FileStorage.ApplicationWebService();



        public void DeleteAsync(string Key, Action done = null)
        {
            service.DeleteAsync(Key, done);

        }

        public void EnumerateFilesAsync(Abstractatech.JavaScript.FileStorage.AtFile y, Action<string> done = null)
        {
            service.EnumerateFilesAsync(y, done);
        }

        public void GetTransactionKeyAsync(Action<string> done = null)
        {
            service.GetTransactionKeyAsync(done);
        }

        public void UpdateAsync(string Key, string Value, Action done = null)
        {
            service.UpdateAsync(Key, Value, done);
        }
        #endregion


        public void InternalHandler(WebServiceHandler h)
        {
            // HTTP routing? how to do this more elegantly?
            service.InternalHandler(h);
        }


        public void DownloadSDK(WebServiceHandler h)
        {
            var HostUri = new
            {
                Host = h.Context.Request.Headers["Host"].TakeUntilIfAny(":"),
                Port = int.Parse(h.Context.Request.Headers["Host"].SkipUntilIfAny(":"))
            };


            //#if DEBUG
            //            if (InternalMulticast == null)
            //                InternalMulticast = new WithClickOnceLANLauncher.ApplicationWebServiceMulticast
            //                {
            //                    Host = HostUri.Host,
            //                    Port = HostUri.Port,

            //                };
            //#else
            //            if (InternalMulticast == null)
            //                InternalMulticast = new AndroidApplicationWebServiceMulticast
            //                {
            //                    Host = HostUri.Host,
            //                    Port = HostUri.Port,

            //                };
            //#endif

            DownloadSDKFunction.DownloadSDK(h);

        }

        //#if DEBUG
        //        static WithClickOnceLANLauncher.ApplicationWebServiceMulticast InternalMulticast;
        //#else
        //        static AndroidApplicationWebServiceMulticast InternalMulticast;

        //#endif


    }

    class AndroidApplicationWebServiceMulticast : System.ComponentModel.Component
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
            System.Console.WriteLine(

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
