using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JVMCLRTCPServerAsync
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );


            // first are we able to run async?

            var s = new SemaphoreSlim(0);

            //java.lang.Object, rt
            //enter async { ManagedThreadId = 1 }
            //awaiting for SemaphoreSlim{ ManagedThreadId = 1 }
            //after delay{ ManagedThreadId = 8 }
            //http://127.0.0.1:8080
            //{ fileName = http://127.0.0.1:8080 }
            //enter catch { mname = <0032> nop.try } ClauseCatchLocal:
            //{ Message = , StackTrace = java.lang.RuntimeException
            //        at ScriptCoreLibJava.BCLImplementation.System.Net.Sockets.__TcpListener.AcceptTcpClientAsync(__TcpListener.java:131)

            new { }.With(
                async delegate
                {
                    //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
                    //enter async { ManagedThreadId = 1 }
                    //awaiting for SemaphoreSlim{ ManagedThreadId = 1 }
                    //after delay{ ManagedThreadId = 4 }
                    //http://127.0.0.1:8080
                    //awaiting for SemaphoreSlim. done.{ ManagedThreadId = 1 }
                    //--
                    //accept { c = System.Net.Sockets.TcpClient, ManagedThreadId = 6 }
                    //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
                    //accept { c = System.Net.Sockets.TcpClient, ManagedThreadId = 8 }
                    //{ ManagedThreadId = 6, input = GET / HTTP/1.1


                    Console.WriteLine("enter async " + new { Thread.CurrentThread.ManagedThreadId });

                    // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerAsync\ChromeTCPServerAsync\Application.cs
                    await Task.Delay(100);

                    Console.WriteLine("after delay" + new { Thread.CurrentThread.ManagedThreadId });

                    // Additional information: Only one usage of each socket address (protocol/network address/port) is normally permitted
                    // close the other server!
                    var l = new TcpListener(IPAddress.Any, 8080);

                    l.Start();


                    var href =
                        "http://127.0.0.1:8080";

                    Console.WriteLine(
                        href
                    );

                    Process.Start(
                        href
                    );


                    new { }.With(
                        async delegate
                        {
                            while (true)
                            {
                                var c = await l.AcceptTcpClientAsync();

                                Console.WriteLine("accept " + new { c, Thread.CurrentThread.ManagedThreadId });

                                yield(c);
                            }
                        }
                    );

                    // jump back to main thread..
                    s.Release();
                }
            );

            Console.WriteLine("awaiting for SemaphoreSlim" + new { Thread.CurrentThread.ManagedThreadId });
            s.Wait();
            Console.WriteLine("awaiting for SemaphoreSlim. done." + new { Thread.CurrentThread.ManagedThreadId });

            //System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces().WithEach(
            // n =>
            // {
            //	 // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Net\NetworkInformation\NetworkInterface.cs

            //	 var IPProperties = n.GetIPProperties();
            //	 var PhysicalAddress = n.GetPhysicalAddress();

            //	 var InetAddressesString = "";


            //	 foreach (var ip in IPProperties.UnicastAddresses)
            //	 {
            //		 //if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            //		 //{
            //		 //Console.WriteLine(ip.Address.ToString());
            //		 InetAddressesString += ", " + ip.Address;
            //		 //}
            //	 }

            //	 // Address = {192.168.43.1}
            //	 //IPProperties.GatewayAddresses.WithEach(
            //	 //    g =>
            //	 //    {
            //	 //        InetAddressesString += ", " + g.Address;
            //	 //    }
            //	 //);


            //	 Console.WriteLine(new
            //	 {
            //		 n.OperationalStatus,

            //		 n.Name,
            //		 n.Description,
            //		 n.SupportsMulticast,
            //		 //n.NetworkInterfaceType,

            //		 InetAddressesString
            //	 });
            // }
            //);

            Console.WriteLine("--");

            CLRProgram.CLRMain();
        }

        static async void yield(TcpClient c)
        {
            var s = c.GetStream();

            // could we switch into a worker thread?
            // jsc would need to split the stream object tho

            var buffer = new byte[1024];
            // why no implict buffer?
            var count = await s.ReadAsync(buffer, 0, buffer.Length);

            var input = Encoding.UTF8.GetString(buffer, 0, count);

            //new IHTMLPre { new { input } }.AttachToDocument();
            Console.WriteLine(new { Thread.CurrentThread.ManagedThreadId, input });

            var outputString = "HTTP/1.0 200 OK \r\nConnection: close\r\n\r\nhello world. jvm clr android async tcp? udp?\r\n";
            var obuffer = Encoding.UTF8.GetBytes(outputString);

            await s.WriteAsync(obuffer, 0, obuffer.Length);

            c.Close();
        }
    }


    public delegate XElement XElementFunc();

    [SwitchToCLRContext]
    static class CLRProgram
    {
        public static XElement XML { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void CLRMain()
        {
            System.Console.WriteLine(
                typeof(object).AssemblyQualifiedName
            );

            MessageBox.Show("click to close");

        }
    }


}



//- javac
//"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\JVMCLRTCPServerAsync\Program.java
//Y:\staging\web\java\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\__Task_1.java:147: error: _1_GetAwaiter_public_ldftn_0017() in __Task_1 cannot override _1_GetAwaiter_public_ldftn_0017() in __Task
//    public final  boolean _1_GetAwaiter_public_ldftn_0017()
//                          ^
//  overridden method is final
//Y:\staging\web\java\JVMCLRTCPServerAsync__i__d\Internal\Library\StringConversions.java:207: error: incompatible types
//        for (num5 = 0; num5; num5++)
//                       ^
//  required: boolean
//  found:    int

//- javac
//"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\JVMCLRTCPServerAsync\Program.java
//Y:\staging\web\java\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\__Task_1.java:147: error: _1_GetAwaiter_public_ldftn_0017() in __Task_1 cannot override _1_GetAwaiter_public_ldftn_0017() in __Task
//    public final  boolean _1_GetAwaiter_public_ldftn_0017()
//                          ^
//  overridden method is final