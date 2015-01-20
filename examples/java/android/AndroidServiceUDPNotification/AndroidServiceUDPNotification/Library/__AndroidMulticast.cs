using ScriptCoreLibJava.BCLImplementation.System.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.BCLImplementation.System.Net;

namespace JVMCLRBroadcastLogger
{

    public class __AndroidMulticast
    {
        public static __AndroidMulticast value;
        private object AtData;




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

        public __AndroidMulticast(object AtData)
        {
            // TODO: Complete member initialization
            this.AtData = AtData;
        }

        private void InitializeThread(Action<string> AtData)
        {
            // X:\jsc.svn\examples\java\android\LANBroadcastListener\LANBroadcastListener\ApplicationActivity.cs



            var buffer = new byte[0xffff];

            // http://stackoverflow.com/questions/12610415/multicast-receiver-malfunction
            // http://answers.unity3d.com/questions/250732/android-build-is-not-receiving-udp-broadcasts.html



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




                #region NetworkInterfaces
                var data =
                          from n in NetworkInterface.GetAllNetworkInterfaces()

                          let SupportsMulticast = n.SupportsMulticast

                          from u in n.GetIPProperties().UnicastAddresses

                          let IsLoopback = IPAddress.IsLoopback(u.Address)

                          let IPv4 = u.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork

                          // http://compnetworking.about.com/od/workingwithipaddresses/l/aa042400b.htm
                          // http://ipaddressextensions.codeplex.com/SourceControl/latest#WorldDomination.Net/IPAddressExtensions.cs


                          //- javac
                          //"C:\Program Files (x86)\Java\jdk1.6.0_35\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\JVMCLRPrivateAddress\Program.java
                          //java\JVMCLRPrivateAddress\Program.java:435: <T>Of(T[]) in ScriptCoreLib.Shared.BCLImplementation.System.__SZArrayEnumerator_1 
                          // cannot be applied to <java.lang.Integer>(int[])
                          //        return  new __AnonymousTypes__JVMCLRPrivateAddress__i__d_jvm.__f__AnonymousType_82__51__79_6_2<__AnonymousTypes__JVMCLRPrivateAddress__i__d_jvm.__f__AnonymousType_76__48__76_5_2<__AnonymousTypes__JVMCLRPrivateAddress__i__d_jvm.__f__AnonymousType_69__45__73_4_2<__AnonymousTypes__JVMCLRPrivateAddress__i__d_jvm.__f__AnonymousType_64__42__70_3_2<__AnonymousTypes__JVMCLRPrivateAddress__i__d_jvm.__f__AnonymousType_59__39__67_2_2<__AnonymousTypes__JVMCLRPrivateAddress__i__d_jvm.__f__AnonymousType_54__36__64_1_2<__AnonymousTypes__JVMCLRPrivateAddress__i__d_jvm.__f__AnonymousType_11__28__30_0_2<__NetworkInterface, Boolean>, __UnicastIPAddressInformation>, Boolean>, Boolean>, byte[]>, int[]>, Boolean>(__h__TransparentIdentifier5, __Enumerable.<Integer>Contains(__SZArrayEnumerator_1.<Integer>Of(__h__TransparentIdentifier5.get_PrivateAddresses()), (short)(__h__TransparentIdentifier5.get___h__TransparentIdentifier4().get_AddressBytes()[0] & 0xff)));
                          //                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      ^
                          //Note: Some input files use unchecked or unsafe operations.
                          //Note: Recompile with -Xlint:unchecked for details.

                          let get_IsPrivate = new Func<bool>(
                           delegate
                           {
                               Console.WriteLine("get_IsPrivate " + new { SupportsMulticast, n.Description, u.Address, IPv4 });

                               var AddressBytes = u.Address.GetAddressBytes();

                               // should do a full mask check?
                               // http://en.wikipedia.org/wiki/IP_address
                               //var PrivateAddresses = new[] { 10, 172, 192 };

                               //- javac
                               //"C:\Program Files (x86)\Java\jdk1.6.0_35\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\JVMCLRPrivateAddress\Program.java
                               //Y:\staging\web\java\JVMCLRPrivateAddress\Program___c__DisplayClass2b.java:36: <T>Of(T[]) in ScriptCoreLib.Shared.BCLImplementation.System.__SZArrayEnumerator_1 cannot be applied to <java.lang.Integer>(int[])
                               //        enumerable_11 = __Enumerable.<Integer>AsEnumerable(__SZArrayEnumerator_1.<Integer>Of(new int[] {
                               //                                                                                ^
                               //Y:\staging\web\java\JVMCLRPrivateAddress\Program___c__DisplayClass2b.java:38: <TSource>Contains(ScriptCoreLib.Shared.BCLImplementation.System.Collections.Generic.__IEnumerable_1<TSource>,TSource) in ScriptCoreLib.
                               //        return  __Enumerable.<Integer>Contains(enumerable_11, (short)(byteArray0[0] & 0xff));
                               //                            ^


                               //return PrivateAddresses.Contains(AddressBytes[0]);

                               if (AddressBytes[0] == 10)
                                   return true;

                               if (AddressBytes[0] == 172)
                                   return true;

                               if (AddressBytes[0] == 192)
                                   return true;

                               return false;

                           }
                          )


                          let IsPrivate = get_IsPrivate()

                          select new
                          {
                              IsPrivate,
                              IsLoopback,
                              SupportsMulticast,
                              IPv4,
                              u,
                              n
                          };
                #endregion


                Console.WriteLine("new MulticastSocket..");

                // http://stackoverflow.com/questions/8692399/finding-a-bug-hidden-causing-a-java-net-socketexception-unrecognized-windows-so

                var localport = new Random().Next(10000, 30000);

                var socket = new java.net.MulticastSocket(
                    //localport
                    40804
                    //40404
                    ); // must bind receive side
                // X:\jsc.internal.svn\examples\javascript\chrome\ChromeMyJscSolutionsNet\ChromeMyJscSolutionsNet\Application.cs

                Console.WriteLine("new MulticastSocket.. done");


                socket.setBroadcast(true);
                socket.setReuseAddress(true);

                // http://www.oser.org/~hp/ds/node27.html
                socket.setTimeToLive(30);
                socket.setReceiveBufferSize(buffer.Length);

                //socket.bind(
                //    new java.net.InetSocketAddress(
                //            java.net.Inet4Address.getByName("0.0.0.0"),
                //            40404
                //    )
                //);


                #region g
                var g = from x in data

                        let get_key = new Func<bool>(
                            delegate
                            {
                                Console.WriteLine("get_key " + new { x.IsPrivate, x.IsLoopback, x.SupportsMulticast, x.IPv4 });



                                return x.IsPrivate && !x.IsLoopback && x.SupportsMulticast && x.IPv4;
                            }
                        )

                        let key = get_key()

                        // group by missing from scriptcorelib?

                        let gkey = new { x.u, x.n.Description, x.SupportsMulticast, key }
                        //let gvalue = new { key }

                        // Caused by: java.lang.RuntimeException: Implement IComparable for __AnonymousTypes__JVMCLRPrivateAddress__i__d_jvm.__f__AnonymousType_115__82__110_d_1 vs __AnonymousTypes__JVMCLRPrivateAddress__i__d_jvm.__f__AnonymousType_115__82__110_d_1

                        group gkey by key;
                #endregion



                //socket.joinGroup(InetAddress.getByName("239.1.2.3"), wlan);

                data.FirstOrDefault(
                    x =>
                    {
                        return x.IsPrivate && !x.IsLoopback && x.SupportsMulticast && x.IPv4;
                    }
                ).With(
                    x =>
                    {
                        Console.WriteLine("");
                        Console.WriteLine("");
                        Console.WriteLine("joinGroup " + new { x.u.Address });

                        //                        pile:
                        //[javac] Compiling 545 source files to V:\bin\classes
                        //[javac] V:\src\JVMCLRBroadcastLogger\__AndroidMulticast___c__DisplayClass2d.java:30: unreported exception java.net.SocketException; must be caught or declared to be thrown
                        //[javac]         this.socket.setNetworkInterface(__NetworkInterface.ToNetworkInterface(__NetworkInterface.Of(x.get_n())));
                        //[javac]                                        ^
                        //[javac] Note: Some input files use or override a deprecated API.


                        try
                        {

                            //socket.setNetworkInterface(
                            //      (__NetworkInterface)x.n
                            //);

                            // var value_bind = await chrome.socket.bind(socketId, "0.0.0.0", 40404);


                            // http://stackoverflow.com/questions/14699249/java-socket-binding-to-local-port

                            Console.WriteLine("joinGroup");

                            socket.joinGroup(

                                new java.net.InetSocketAddress(
                                    java.net.InetAddress.getByName("239.1.2.3"),
                                    40804
                                    //40404
                                ),

                                (__NetworkInterface)
                                x.n

                            );



                        }
                        catch
                        {
                            throw;
                        }
                    }
                );








                System.Console.WriteLine("LANBroadcastListener joinGroup... awaiting DatagramPacket");

                // workaround
                var forever = true;
                while (forever)
                {
                    var dgram = new java.net.DatagramPacket((sbyte[])(object)buffer, buffer.Length);
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

                // client error { Message = Unrecognized Windows Sockets error: 0: Cannot bind, StackTrace = java.net.SocketException: Unrecognized Windows Sockets error: 0: Cannot bind

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
