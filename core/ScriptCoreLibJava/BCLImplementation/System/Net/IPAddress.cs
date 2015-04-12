using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Net;
using System.Net.Sockets;
using ScriptCoreLibJava.BCLImplementation.System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace ScriptCoreLibJava.BCLImplementation.System.Net
{
    // http://referencesource.microsoft.com/#System/net/System/Net/IPAddress.cs
    // https://github.com/mono/mono/blob/master/mcs/class/System/System.Net/IPAddress.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Net\IPAddress.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Net\IPAddress.cs

    [Script(Implements = typeof(global::System.Net.IPAddress))]
    public class __IPAddress
    {
        public AddressFamily AddressFamily { get; set; }

        public string InternalAddressString;
        public global::java.net.InetAddress InternalAddress;

        public static readonly IPAddress Loopback;
        public static readonly IPAddress Any;


        public byte[] GetAddressBytes()
        {
            return (byte[])(object)this.InternalAddress.getAddress();
        }



        public static bool IsLoopback(IPAddress address)
        {
            __IPAddress a = address;

            return a.InternalAddress.isLoopbackAddress();
        }

        static __IPAddress()
        {
            //I/System.Console( 7307): enter __IPAddress
            //W/dalvikvm( 7307): Exception Ljava/lang/RuntimeException; thrown while initializing LScriptCoreLibJava/BCLImplementation/System/Net/__IPAddress;
            //D/AndroidRuntime( 7307): Shutting down VM
            //W/dalvikvm( 7307): threadid=1: thread exiting with uncaught exception (group=0x41d0e700)

            // X:\jsc.svn\examples\java\android\forms\InteractivePortForwarding\InteractivePortForwarding\UserControl1.cs

            var sw = Stopwatch.StartNew();
            Console.WriteLine("enter __IPAddress");

            //I/System.Console( 7846): Caused by: android.os.NetworkOnMainThreadException
            //I/System.Console( 7846):        at android.os.StrictMode$AndroidBlockGuardPolicy.onNetwork(StrictMode.java:1133)
            //I/System.Console( 7846):        at java.net.InetAddress.lookupHostByName(InetAddress.java:385)
            //I/System.Console( 7846):        at java.net.InetAddress.getLocalHost(InetAddress.java:365)
            //I/System.Console( 7846):        at ScriptCoreLibJava.BCLImplementation.System.Net.__IPAddress.InitializeLoopback(__IPAddress.java:83)
            //I/System.Console( 7846):        ... 56 more
            //I/System.Console( 7846):  }
            //I/System.Console( 7846): exit __IPAddress
            //I/System.Console( 7846): UserControl1_Load { xUnicastAddresses = ScriptCoreLibJava.BCLImplementation.System.Net.NetworkInformation.__UnicastIPAddressInformationCollection@425ee6e0 }


            //I/System.Console( 8716): enter __IPAddress
            //W/ActivityManager(  375): Launch timeout has expired, giving up wake lock!
            //W/ActivityManager(  375): Activity idle timeout for ActivityRecord{42e056a8 u0 InteractivePortForwarding.Activities/.ApplicationActivity}

            // this is stupid!

            var a = new AutoResetEvent(false);

            __IPAddress.Loopback = new __IPAddress { };
            __IPAddress.Any = new __IPAddress { };

            new Thread(
                 delegate()
                 {
                     Console.WriteLine("enter __IPAddress worker");

                     a.Set();

                     //        I/System.Console(10144): enter __IPAddress
                     //I/System.Console(10144): enter __IPAddress worker
                     //I/System.Console(10144): exit __IPAddress { ElapsedMilliseconds = 13 }
                     //I/System.Console(10144): exit __IPAddress worker { ElapsedMilliseconds = 15 }

                     try
                     {
                         // fixme: jsc is too agressive here to inline the static initializer
                         // and causes a fault here
                         //__IPAddress.Loopback = new __IPAddress { InternalAddress = global::java.net.InetAddress.getLocalHost() };


                         ((__IPAddress)__IPAddress.Loopback).InternalAddress = global::java.net.InetAddress.getByName("127.0.0.1");
                         ((__IPAddress)__IPAddress.Any).InternalAddress = global::java.net.InetAddress.getByName("0.0.0.0");

                         // http://msdn.microsoft.com/en-us/library/system.net.ipaddress.any.aspx


                     }
                     catch (Exception ex)
                     {
                         Console.WriteLine(new { ex.Message, ex.StackTrace });

                         //throw;
                     }
                     Console.WriteLine("exit __IPAddress worker " + new { sw.ElapsedMilliseconds });

                 }
             ).Start();

            //I/System.Console( 9988): enter __IPAddress
            //I/System.Console( 9988): enter __IPAddress worker
            //I/System.Console( 9988): exit __IPAddress { ElapsedMilliseconds = 2 }
            //I/System.Console( 9988): UserControl1_Load { xUnicastAddresses = ScriptCoreLibJava.BCLImplementation.System.Net.NetworkInformation.__UnicastIPAddressInformationCollection@42629c40 }
            //I/System.Console( 9988): exit __IPAddress worker { ElapsedMilliseconds = 4 }

            a.WaitOne();
            Thread.Sleep(1);

            Console.WriteLine("exit __IPAddress " + new { sw.ElapsedMilliseconds });
        }



        public override string ToString()
        {
            if (this.InternalAddressString != null)
                return this.InternalAddressString;

            // http://www.devx.com/tips/Tip/24147
            return this.InternalAddress.getHostAddress();
        }




        public static implicit operator global::java.net.InetAddress(__IPAddress i)
        {
            return i.InternalAddress;
        }

        public static implicit operator global::System.Net.IPAddress(__IPAddress i)
        {
            return (global::System.Net.IPAddress)(object)i;
        }

        public static implicit operator __IPAddress(global::System.Net.IPAddress i)
        {
            return (__IPAddress)(object)i;
        }
    }
}
