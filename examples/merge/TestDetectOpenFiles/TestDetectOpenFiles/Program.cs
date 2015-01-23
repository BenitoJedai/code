using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestDetectOpenFiles
{
    public static class x
    {
        public static IEnumerable<FileInfo> GetOpenFiles(this Process p) =>
            from x in p.GetOpenFileSystemInfos()
            let y = x as FileInfo
            where y != null
            select y;


        public static IEnumerable<FileSystemInfo> GetOpenFileSystemInfos(this Process p)
        {

            var f = VmcController.Services.DetectOpenFiles.GetOpenFilesEnumerator(p.Id);


            while (f.MoveNext())
            {
                var c = f.Current;

                yield return c;
            }
        }
    }

    class Program
    {
        // X:\jsc.svn\examples\merge\Test\TestYouTubeExtractor\TestYouTubeExtractor\Program.cs

        static void Main(string[] args)
        {
            // http://msdn.microsoft.com/en-us/library/bb432383%28v=vs.85%29.aspx
            // An optional pointer to a location where the function writes the actual size of the information requested. If that size is less than or equal to the ObjectInformationLength parameter, the function copies the information into the ObjectInformation buffer; otherwise, it returns an NTSTATUS error code and returns in ReturnLength the size of the buffer required to receive the requested information.

            var Host = "youtube.com";
            var PublicPort = 80;


            #region advertise
            Action<string> advertise = nmessage =>
            {
                // tested by
                // X:\jsc.svn\examples\javascript\Test\TestCLRBroadcast\TestCLRBroadcast\ApplicationWebService.cs
                // X:\jsc.svn\market\Abstractatech.Multicast\Abstractatech.Multicast\Library\MulticastListener.cs
                // X:\jsc.svn\examples\java\ConsoleMulticastExperiment\ConsoleMulticastExperiment\Program.cs
                // X:\jsc.svn\examples\javascript\chrome\apps\MulticastListenExperiment\MulticastListenExperiment\Application.cs

                #region  NIC
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
                                                        //Console.WriteLine("get_IsPrivate " + new { SupportsMulticast, n.Description, u.Address, IPv4 });

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


                var g = from x in data

                        let get_key = new Func<bool>(
                            delegate
                            {
                                //Console.WriteLine("get_key " + new { x.IsPrivate, x.IsLoopback, x.SupportsMulticast, x.IPv4 });



                                return x.IsPrivate && !x.IsLoopback && x.SupportsMulticast && x.IPv4;
                            }
                        )

                        let key = get_key()

                        // group by missing from scriptcorelib?

                        let gkey = new { x.u, x.n.Description, key }
                        //let gvalue = new { key }

                        // Caused by: java.lang.RuntimeException: Implement IComparable for __AnonymousTypes__JVMCLRPrivateAddress__i__d_jvm.__f__AnonymousType_115__82__110_d_1 vs __AnonymousTypes__JVMCLRPrivateAddress__i__d_jvm.__f__AnonymousType_115__82__110_d_1

                        group gkey by key;


                // X:\jsc.svn\examples\java\hybrid\JVMCLRPrivateAddress\JVMCLRPrivateAddress\Program.cs

                //{ Key = True, gi = 0 }
                //{ Address = 192.168.43.12, Description = TP - LINK Wireless USB Adapter }
                //{ Address = 192.168.136.1, Description = VMware Virtual Ethernet Adapter for VMnet1 }
                //{ Address = 192.168.81.1, Description = VMware Virtual Ethernet Adapter for VMnet8 }

                //g.Reverse().WithEachIndex(
                //      (gx, gi) =>
                //      {

                //          Console.WriteLine(new { gx.Key, gi });

                //          gx.WithEach(
                //                x =>
                //                {
                //                    Console.WriteLine(new { x.u.Address, x.Description });

                //                }
                //            );

                //      }
                //  );
                #endregion


                var message =
                   new XElement("string",
                       new XAttribute("c", "" + 1),
                       new XAttribute("n", nmessage),
                         "Visit me at " + Host + ":" + PublicPort
                   ).ToString();


                Console.WriteLine(new { message });


                var port = new Random().Next(16000, 40000);

                var socket = new UdpClient();


                socket.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

                socket.ExclusiveAddressUse = false;
                socket.EnableBroadcast = true;

                // 192.168.43.12
                var aa = g.Single(x => x.Key).First().u.Address;

                var loc = new IPEndPoint(aa
                   ,
                    port);

                socket.Client.Bind(loc);


                var mdata = Encoding.UTF8.GetBytes(message.ToString());    //creates a variable b of type byte

                // X:\jsc.svn\examples\javascript\chrome\apps\ChromeUDPNotification\ChromeUDPNotification\Application.cs
                var s = socket.Send(mdata, mdata.Length, "239.1.2.3", 40804);
                //socket.SendAsync
                // s = 49

                socket.Close();
            };

            #endregion


            //var p = Process.GetProcesses().FirstOrDefault(x => x.ProcessName == "vlc");

            var mp4 = default(FileInfo);

            // haha. if boot disk. the ssd cable gets disconnected the machine will drop.

            goto retry;
        next:


            // X:\opensource\github\taglib-sharp\src\TagLib\Mpeg4\AppleTag.cs
            // mp4 = {X:\media\Kryon - The Biology of Co Creation.mp4}

            var m = Path.GetFileNameWithoutExtension(mp4.Name);



            var total = mp4.Directory.GetFiles("*.mp4").Length;
            // will it affect youtube search on our peer, chrome extension and android app?

            // android does not know how to scroll long titles
            // nor can it find a video if we add total to it
            m += " of " + total;

            Console.Title = m;

            // done. wait for a new 
            Console.WriteLine(m);

            advertise(m);

        retry:
            var mp4next = Process.GetProcesses().FirstOrDefault(x => x.ProcessName == "vlc")?.GetOpenFiles().ToArray().FirstOrDefault(f => f.Extension == ".mp4");

            if (mp4next == null)
            {
                mp4 = null;

                //Console.Write(".");
                Thread.Sleep(1000);
                goto retry;
            }

            if (mp4 != null)
                if (mp4next.FullName == mp4.FullName)
                {
                    //Console.Write(".");
                    Thread.Sleep(1000);
                    goto retry;
                }

            mp4 = mp4next;
            goto next;

            Debugger.Break();
        }
    }
}
