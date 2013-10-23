using java.net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JVMCLRBroadcastLogger
{

    public class __AndroidMulticast
    {
        public static __AndroidMulticast value;



        //WifiManager wifi;
        //WifiManager.MulticastLock multicastLock;

        public __AndroidMulticast(

            Action<string> AtData

            )
        {


            new Thread(
                    delegate()
                    {
                        InitializeThread(AtData);
                    }
                )
            {

                Name = "client"
            }.Start();
        }

        private void InitializeThread(Action<string> AtData)
        {
            var buffer = new byte[0xffff];

            // http://stackoverflow.com/questions/12610415/multicast-receiver-malfunction
            // http://answers.unity3d.com/questions/250732/android-build-is-not-receiving-udp-broadcasts.html

            // Acquire multicast lock
            //wifi = (WifiManager)
            //    ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext.getSystemService(Context.WIFI_SERVICE);
            //multicastLock = wifi.createMulticastLock("multicastLock");
            ////multicastLock.setReferenceCounted(true);
            //multicastLock.acquire();

            System.Console.WriteLine("LANBroadcastListener ready...");
            try
            {
                //ApplicationWebService cctor: { value = <string reason="shake" c="5" preview="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAGAAAABgCAIAAABt+uBvAAAACXBIWXMAAA7EAAAOxAGVKw4bAAAUBklEQVR4nN2dz5Mkx1XHv+9lVlX/mJ0fuzuS1kZCksMmrPCBCAEHMBEcIYAjEQQHgiB84z/hxh3wCQIHdoAPcMIYcAAH47BDQsi2tLJWsqwf+2tmdrq7qrIy }
                //[Fatal Error] :1:257: XML document structures must start and end within the same entity.
                //ApplicationWebService cctor error { Message = , StackTrace = java.lang.RuntimeException
                //    at ScriptCoreLibJava.BCLImplementation.System.Xml.Linq.__XDocument.Parse(__XDocument.java:59)
                //    at ScriptCoreLibJava.BCLImplementation.System.Xml.Linq.__XElement.Parse(__XElement.java:177)
                //    at JVMCLRBroadcastLogger.Program._Main_b__0(Program.java:64)

                // tweet sized incoming broadcasts!

                // https://code.google.com/p/android/issues/detail?id=40003



                // http://stackoverflow.com/questions/3947555/java-net-socketexception-unrecognized-windows-sockets-error-0-jvm-bind-jboss
                // https://forums.oracle.com/message/11027069

                var socket = new MulticastSocket(40404); // must bind receive side
                socket.setBroadcast(true);
                socket.setReuseAddress(true);
                socket.setTimeToLive(30);
                socket.setReceiveBufferSize(0x100);


                var NetworkInterfaces = NetworkInterface.getNetworkInterfaces();

                var wlan = default(NetworkInterface);

                //I/System.Console( 6922): { NetworkInterfaceName = dummy0 }
                //I/System.Console( 6922): { NetworkInterfaceName = sit0 }
                //I/System.Console( 6922): { NetworkInterfaceName = ip6tnl0 }
                //I/System.Console( 6922): { NetworkInterfaceName = p2p0 }
                //I/System.Console( 6922): { NetworkInterfaceName = wlan0 }
                //I/System.Console( 6922): LANBroadcastListener joinGroup...
                //I/Web Console( 7351): DataGridView ready
                //I/Web Console( 7351):  at http://192.168.43.7:1440/view-source:28790
                //I/Web Console( 7351): Time to load fields from the cookie, were they even sent?
                //I/Web Console( 7351):  at http://192.168.43.7:1440/view-source:28790
                //E/Web Console( 7351): Uncaught Error: SYNTAX_ERR: DOM Exception 12 at http://192.168.43.7:1440/view-source:11710
                //I/System.Console( 7351): #4 GET /assets/ScriptCoreLib.Windows.Forms/DataGridNewRow.png HTTP/1.1
                //D/TilesManager( 7351): Starting TG #0, 0x63c079d0
                //D/TilesManager( 7351): new EGLContext from framework: 62720e40
                //D/GLWebViewState( 7351): Reinit shader
                //D/dalvikvm( 7351): GC_CONCURRENT freed 505K, 7% free 7984K/8552K, paused 2ms+4ms, total 34ms
                //D/GLWebViewState( 7351): Reinit transferQueue
                //E/Web Console( 7351): Uncaught TypeError: Cannot call method 'nhwABlgwczGFxMM_a_a0rwGA' of null at http://192.168.43.7:1440/view-source:46281
                //E/Web Console( 7351): Uncaught TypeError: Cannot call method 'nhwABlgwczGFxMM_a_a0rwGA' of null at http://192.168.43.7:1440/view-source:46281
                //E/Web Console( 7351): Uncaught TypeError: Cannot call method 'nhwABlgwczGFxMM_a_a0rwGA' of null at http://192.168.43.7:1440/view-source:46281

                while (NetworkInterfaces.hasMoreElements())
                {
                    var xNetworkInterface = (NetworkInterface)NetworkInterfaces.nextElement();

                    var isUp = xNetworkInterface.isUp();
                    var supportsMulticast = xNetworkInterface.supportsMulticast();
                    var isVirtual = xNetworkInterface.isVirtual();
                    var DisplayName = xNetworkInterface.getDisplayName();
                    var NetworkInterfaceName = xNetworkInterface.getName();


                    var InetAddresses = xNetworkInterface.getInetAddresses();
                    var InetAddressesString = "";
                    while (InetAddresses.hasMoreElements())
                    {
                        var xInetAddress = (InetAddress)InetAddresses.nextElement();


                        //xInetAddress.metr
                        InetAddressesString += ", " + xInetAddress;

                        if (!xInetAddress.isLoopbackAddress())
                            if (xInetAddress is Inet4Address)
                            {


                                wlan = xNetworkInterface;

                                Console.WriteLine("selecting: " + xInetAddress);
                            }
                    }

                    Console.WriteLine(new
                    {
                        NetworkInterfaceName,
                        supportsMulticast,
                        isUp,
                        isVirtual,
                        DisplayName,
                        InetAddressesString
                    });
                }


                //               { NetworkInterfaceName = lo, supportsMulticast = true, isUp = true, isVirtual = false, DisplayName = Software Loopback Interface 1, InetAddressesString = , /127.0.0.1, /0:0:0:0:0:0:0:1 }
                //{ NetworkInterfaceName = net0, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = WAN Miniport (SSTP), InetAddressesString =  }
                //{ NetworkInterfaceName = net1, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = WAN Miniport (L2TP), InetAddressesString =  }
                //{ NetworkInterfaceName = net2, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = WAN Miniport (PPTP), InetAddressesString =  }
                //{ NetworkInterfaceName = ppp0, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = WAN Miniport (PPPOE), InetAddressesString =  }
                //{ NetworkInterfaceName = eth0, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = WAN Miniport (IPv6), InetAddressesString =  }
                //{ NetworkInterfaceName = eth1, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = WAN Miniport (Network Monitor), InetAddressesString =  }
                //{ NetworkInterfaceName = eth2, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = WAN Miniport (IP), InetAddressesString =  }
                //{ NetworkInterfaceName = ppp1, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = RAS Async Adapter, InetAddressesString =  }
                //{ NetworkInterfaceName = net3, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = WAN Miniport (IKEv2), InetAddressesString =  }

                //{ NetworkInterfaceName = net4, supportsMulticast = true, isUp = true, isVirtual = false, DisplayName = Atheros AR9485WB-EG Wireless Network Adapter, InetAddressesString = , /192.168.43.252, /fe80:0:0:0:55cc:63eb:5b4:60b4%11 }

                //{ NetworkInterfaceName = net5, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = Bluetooth Device (RFCOMM Protocol TDI), InetAddressesString =  }

                //{ NetworkInterfaceName = eth3, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = Bluetooth Device (Personal Area Network), InetAddressesString =  }
                //{ NetworkInterfaceName = net6, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = Microsoft Virtual WiFi Miniport Adapter, InetAddressesString =  }
                //{ NetworkInterfaceName = eth4, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = TAP-Win32 Adapter V9, InetAddressesString = , /fe80:0:0:0:81b:4a67:d4b1:2d08%15 }
                //{ NetworkInterfaceName = net7, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = HUAWEI Mobile Connect - 3G Network Card, InetAddressesString =  }
                //{ NetworkInterfaceName = eth5, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = ASIX AX88772B USB2.0 to Fast Ethernet Adapter, InetAddressesString =  }
                //{ NetworkInterfaceName = net8, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = HUAWEI Mobile Connect - 3G Network Card #2, InetAddressesString =  }
                //{ NetworkInterfaceName = net9, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = Microsoft 6to4 Adapter, InetAddressesString =  }
                //{ NetworkInterfaceName = net10, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = , InetAddressesString =  }
                //{ NetworkInterfaceName = net11, supportsMulticast = false, isUp = false, isVirtual = false, DisplayName = Microsoft ISATAP Adapter #5, InetAddressesString =  }
                //{ NetworkInterfaceName = net12, supportsMulticast = false, isUp = true, isVirtual = false, DisplayName = Teredo Tunneling Pseudo-Interface, InetAddressesString = , /2001:0:9d38:90d7:419:3c33:3f57:d403, /fe80:0:0:0:419:3c33:3f57:d403%22 }
                //{ NetworkInterfaceName = net13, supportsMulticast = false, isUp = false, isVirtual = false, DisplayName = Microsoft ISATAP Adapter #3, InetAddressesString =  }
                //{ NetworkInterfaceName = net14, supportsMulticast = false, isUp = false, isVirtual = false, DisplayName = Microsoft ISATAP Adapter #2, InetAddressesString = , /fe80:0:0:0:0:5efe:c0a8:2bfc%24 }
                //{ NetworkInterfaceName = net15, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = Microsoft 6to4 Adapter-Netmon Lightweight Filter Driver-0000, InetAddressesString =  }
                //{ NetworkInterfaceName = net16, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = Microsoft ISATAP Adapter #2-Netmon Lightweight Filter Driver-0000, InetAddressesString =  }
                //{ NetworkInterfaceName = net17, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = Microsoft ISATAP Adapter #3-Netmon Lightweight Filter Driver-0000, InetAddressesString =  }
                //{ NetworkInterfaceName = net18, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = Microsoft ISATAP Adapter #5-Netmon Lightweight Filter Driver-0000, InetAddressesString =  }
                //{ NetworkInterfaceName = net19, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = Teredo Tunneling Pseudo-Interface-Netmon Lightweight Filter Driver-0000, InetAddressesString =  }
                //{ NetworkInterfaceName = net20, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = Atheros AR9485WB-EG Wireless Network Adapter-Native WiFi Filter Driver-0000, InetAddressesString =  }
                //{ NetworkInterfaceName = net21, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = Atheros AR9485WB-EG Wireless Network Adapter-QoS Packet Scheduler-0000, InetAddressesString =  }
                //{ NetworkInterfaceName = net22, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = Atheros AR9485WB-EG Wireless Network Adapter-WFP LightWeight Filter-0000, InetAddressesString =  }
                //{ NetworkInterfaceName = eth6, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = WAN Miniport (Network Monitor)-Netmon Lightweight Filter Driver-0000, InetAddressesString =  }
                //{ NetworkInterfaceName = eth7, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = WAN Miniport (Network Monitor)-QoS Packet Scheduler-0000, InetAddressesString =  }
                //{ NetworkInterfaceName = eth8, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = WAN Miniport (IP)-QoS Packet Scheduler-0000, InetAddressesString =  }
                //{ NetworkInterfaceName = eth9, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = WAN Miniport (IPv6)-QoS Packet Scheduler-0000, InetAddressesString =  }
                //{ NetworkInterfaceName = eth10, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = TAP-Win32 Adapter V9-Netmon Lightweight Filter Driver-0000, InetAddressesString =  }
                //{ NetworkInterfaceName = eth11, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = TAP-Win32 Adapter V9-QoS Packet Scheduler-0000, InetAddressesString =  }
                //{ NetworkInterfaceName = eth12, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = TAP-Win32 Adapter V9-WFP LightWeight Filter-0000, InetAddressesString =  }
                //{ NetworkInterfaceName = net23, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = Atheros AR9485WB-EG Wireless Network Adapter-Netmon Lightweight Filter Driver-0000, InetAddressesString =  }
                //{ NetworkInterfaceName = net24, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = Atheros AR9485WB-EG Wireless Network Adapter-Virtual WiFi Filter Driver-0000, InetAddressesString =  }
                //{ NetworkInterfaceName = net25, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = Microsoft ISATAP Adapter #4, InetAddressesString =  }
                //{ NetworkInterfaceName = net26, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = Microsoft ISATAP Adapter #4-Netmon Lightweight Filter Driver-0000, InetAddressesString =  }
                //socket.joinGroup(InetAddress.getByName("239.1.2.3"), wlan);
                socket.joinGroup(InetAddress.getByName("239.1.2.3"));

                System.Console.WriteLine("LANBroadcastListener joinGroup...");

                // workaround
                var forever = true;
                while (forever)
                {
                    DatagramPacket dgram = new DatagramPacket((sbyte[])(object)buffer, buffer.Length);
                    socket.receive(dgram); // blocks until a datagram is received

                    var bytes = new MemoryStream((byte[])(object)dgram.getData(), 0, dgram.getLength());


                    var listen = Encoding.UTF8.GetString(bytes.ToArray());



                    //dgram.setLength(b.Length); // must reset length field!s

                    if (AtData != null)
                        AtData(listen);

                }
            }
            catch (Exception ex)
            {
                // client error
                System.Console.WriteLine("client error " + new { ex.Message, ex.StackTrace });

                //client error { Message = Unrecognized Windows Sockets error: 0: Cannot bind, StackTrace = java.net.SocketException: Unrecognized Windows Sockets error: 0: Cannot bind
                //    at java.net.TwoStacksPlainDatagramSocketImpl.bind0(Native Method)
                //    at java.net.TwoStacksPlainDatagramSocketImpl.bind0(Unknown Source)
                //    at java.net.AbstractPlainDatagramSocketImpl.bind(Unknown Source)
                //    at java.net.TwoStacksPlainDatagramSocketImpl.bind(Unknown Source)
                //    at java.net.DatagramSocket.bind(Unknown Source)
                //    at java.net.MulticastSocket.<init>(Unknown Source)
                //    at java.net.MulticastSocket.<init>(Unknown Source)
                //    at JVMCLRBroadcastLogger.__AndroidMulticast.InitializeThread(__AndroidMulticast.java:80)
                //    at JVMCLRBroadcastLogger.__AndroidMulticast___c__DisplayClass3.__ctor_b__1(__AndroidMulticast___c__DisplayClass3.java:24)
                //    at sun.reflect.NativeMethodAccessorImpl.invoke0(Native Method)
                //    at sun.reflect.NativeMethodAccessorImpl.invoke(Unknown Source)
                //    at sun.reflect.DelegatingMethodAccessorImpl.invoke(Unknown Source)
                //    at java.lang.reflect.Method.invoke(Unknown Source)
                //    at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodInfo.InternalInvoke(__MethodInfo.java:88)
                //    at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodBase.Invoke(__MethodBase.java:70)
                //    at ScriptCoreLib.Shared.BCLImplementation.System.Threading.__ThreadStart.Invoke(__ThreadStart.java:27)
                //    at ScriptCoreLibJava.BCLImplementation.System.Threading.__Thread___c__DisplayClass3.__ctor_b__1(__Thread___c__DisplayClass3.java:20)
                //    at sun.reflect.NativeMethodAccessorImpl.invoke0(Native Method)
                //    at sun.reflect.NativeMethodAccessorImpl.invoke(Unknown Source)
                //    at sun.reflect.DelegatingMethodAccessorImpl.invoke(Unknown Source)
                //    at java.lang.reflect.Method.invoke(Unknown Source)
                //    at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodInfo.InternalInvoke(__MethodInfo.java:88)
                //    at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodBase.Invoke(__MethodBase.java:70)
                //    at ScriptCoreLib.Shared.BCLImplementation.System.__Action.Invoke(__Action.java:27)
                //    at ScriptCoreLibJava.BCLImplementation.System.Threading.__Thread_RunnableHandler.run(__Thread_RunnableHandler.java:20)
                //    at java.lang.Thread.run(Unknown Source)

            }

            //           script: error JSC1000: return from within try block not yet supported:
            //assembly: W:\staging\clr\AndroidBroadcastLogger.ApplicationWebService.AndroidActivity.dll
            //type: AndroidBroadcastLogger.__AndroidMulticast, AndroidBroadcastLogger.ApplicationWebService.AndroidActivity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            //offset: 0x015c
            // method:Void InitializeThread(System.Action`1[System.String])


            var __workaround = new object();
        }
    }

}
