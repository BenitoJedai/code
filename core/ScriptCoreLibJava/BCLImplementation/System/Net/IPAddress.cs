using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Net;
using System.Net.Sockets;

namespace ScriptCoreLibJava.BCLImplementation.System.Net
{
    // http://referencesource.microsoft.com/#System/net/System/Net/IPAddress.cs

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
            // fixme: jsc is too agressive here to inline the static initializer
            // and causes a fault here
            __IPAddress.Loopback = InitializeLoopback();


            // http://msdn.microsoft.com/en-us/library/system.net.ipaddress.any.aspx

            try
            {
                __IPAddress.Any = (IPAddress)(object)new __IPAddress { InternalAddress = global::java.net.InetAddress.getByName("0.0.0.0") };
            }
            catch
            {
                throw;
            }

        }

        private static IPAddress InitializeLoopback()
        {

            var Loopback = default(__IPAddress);

            try
            {

                //         Caused by: android.os.NetworkOnMainThreadException
                //at android.os.StrictMode$AndroidBlockGuardPolicy.onNetwork(StrictMode.java:1117)
                //at java.net.InetAddress.lookupHostByName(InetAddress.java:385)
                //at java.net.InetAddress.getLocalHost(InetAddress.java:365)
                //at ScriptCoreLibJava.BCLImplementation.System.Net.__IPAddress.InitializeLoopback(__IPAddress.java:36)
                //... 18 more

                Loopback = new __IPAddress { InternalAddress = global::java.net.InetAddress.getLocalHost() };
            }
            catch
            {
                throw;
            }

            return (IPAddress)(object)Loopback;
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
