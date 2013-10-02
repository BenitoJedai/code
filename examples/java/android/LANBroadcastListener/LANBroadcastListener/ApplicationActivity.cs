using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLib.Android.Manifest;
using System.Threading;
using java.net;
using System.IO;
using android.content;
using android.net.wifi;

namespace LANBroadcastListener.Activities
{
    public class ApplicationActivity : Activity
    {
        WifiManager wifi;
        WifiManager.MulticastLock multicastLock;

        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var b2 = new Button(this);
            {
                var ll = new LinearLayout(this);
                //ll.setOrientation(LinearLayout.VERTICAL);
                sv.addView(ll);

                var b1 = new Button(this).AttachTo(ll);



                b1.WithText("LANBroadcastListener createMulticastLock");


                var c = 0;

                b1.AtClick(
                    v =>
                    {
                        //                 server error { Message = , StackTrace = android.os.NetworkOnMainThreadException
                        //at android.os.StrictMode$AndroidBlockGuardPolicy.onNetwork(StrictMode.java:1117)
                        //at libcore.io.BlockGuardOs.sendto(BlockGuardOs.java:175)
                        //at libcore.io.IoBridge.sendto(IoBridge.java:473)
                        //at java.net.PlainDatagramSocketImpl.send(PlainDatagramSocketImpl.java:182)
                        //at java.net.DatagramSocket.send(DatagramSocket.java:284)

                        new Thread(
                            delegate()
                            {

                                try
                                {
                                    var socket = new DatagramSocket(); //construct a datagram socket and binds it to the available port and the localhos

                                    c++;

                                    var b = Encoding.UTF8.GetBytes(c + " hi from jvm!");    //creates a variable b of type byte
                                    var dgram = new DatagramPacket((sbyte[])(object)b, b.Length, InetAddress.getByName("239.1.2.3"), 40404);//sends the packet details, length of the packet,destination address and the port number as parameters to the DatagramPacket  
                                    //dgram.setData(b);
                                    //System.Console.WriteLine(
                                    //    "Sending " + b.Length + " bytes to " + dgram.getAddress() + ":" + dgram.getPort());//standard error output stream
                                    socket.send(dgram); //send the datagram packet from this port
                                }
                                catch (Exception ex)
                                {
                                    System.Console.WriteLine("server error " + new { ex.Message, ex.StackTrace });
                                }

                            }
                                )
                               {

                                   Name = "sender"
                               }.Start();
                    }
                );

                b2.setText("The other button!");
                ll.addView(b2);

                this.setContentView(sv);
            }


            // http://www.zzzxo.com/q/answers-android-device-not-receiving-multicast-package-13221736.html



            new Thread(
                delegate()
                {
                    // http://stackoverflow.com/questions/12610415/multicast-receiver-malfunction
                    // http://answers.unity3d.com/questions/250732/android-build-is-not-receiving-udp-broadcasts.html

                    // Acquire multicast lock
                    wifi = (WifiManager)getSystemService(Context.WIFI_SERVICE);
                    multicastLock = wifi.createMulticastLock("multicastLock");
                    //multicastLock.setReferenceCounted(true);
                    multicastLock.acquire();

                    System.Console.WriteLine("LANBroadcastListener ready...");
                    try
                    {
                        byte[] b = new byte[0x100];

                        // https://code.google.com/p/android/issues/detail?id=40003

                        var port = 40404;

                        MulticastSocket socket = new MulticastSocket(port); // must bind receive side
                        socket.setBroadcast(true);
                        socket.setReuseAddress(true);
                        socket.setTimeToLive(30);
                        socket.setReceiveBufferSize(0x100);
                        
                        // https://code.google.com/p/android/issues/detail?id=40003
                        // http://stackoverflow.com/questions/6550618/multicast-support-on-android-in-hotspot-tethering-mode
                        // http://www.massapi.com/class/java/net/InetSocketAddress.java.html
                        // http://www.javadocexamples.com/java/net/MulticastSocket/joinGroup(SocketAddress%20mcastaddr,NetworkInterface%20netIf).html
                        // http://grokbase.com/t/hadoop/common-issues/117jsjk8d7/jira-created-hadoop-7472-rpc-client-should-deal-with-the-ip-address-changes

                        var group = InetAddress.getByName("239.1.2.3");
                        var groupSockAddr = new InetSocketAddress(group, port);

                        // what lan interfaces do we have?
                        socket.joinGroup(groupSockAddr,
                            NetworkInterface.getByName("wlan0")
                        );

                        System.Console.WriteLine("LANBroadcastListener joinGroup...");
                        while (true)
                        {
                            DatagramPacket dgram = new DatagramPacket((sbyte[])(object)b, b.Length);
                            socket.receive(dgram); // blocks until a datagram is received

                            var bytes = new MemoryStream((byte[])(object)dgram.getData(), 0, dgram.getLength());


                            var listen = Encoding.UTF8.GetString(bytes.ToArray());

                            System.Console.WriteLine("Received "
                                + dgram.getLength()
                                + " bytes from " + dgram.getAddress());
                            //dgram.setLength(b.Length); // must reset length field!s



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


    }


}
