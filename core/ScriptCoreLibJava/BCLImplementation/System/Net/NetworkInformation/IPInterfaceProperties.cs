using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;
using System.Net.NetworkInformation;

namespace ScriptCoreLibJava.BCLImplementation.System.Net.NetworkInformation
{
    // http://referencesource.microsoft.com/#System/net/System/Net/NetworkInformation/IPInterfaceProperties.cs

    [Script(Implements = typeof(global::System.Net.NetworkInformation.IPInterfaceProperties))]
    internal class __IPInterfaceProperties
    {
        public __NetworkInterface InternalValue;

        public virtual UnicastIPAddressInformationCollection UnicastAddresses
        {
            get
            {
                var a = new List<__UnicastIPAddressInformation>();


                var InetAddresses = this.InternalValue.InternalValue.getInetAddresses();

                while (InetAddresses.hasMoreElements())
                {
                    var xInetAddress = (java.net.InetAddress)InetAddresses.nextElement();

                    var Address = new __IPAddress { InternalAddress = xInetAddress };

                    if (xInetAddress is java.net.Inet4Address)
                    {
                        Address.AddressFamily = global::System.Net.Sockets.AddressFamily.InterNetwork;
                    }
                    else
                    {
                        Address.AddressFamily = global::System.Net.Sockets.AddressFamily.InterNetworkV6;
                    }


                    a.Add(
                        new __UnicastIPAddressInformation
                        {
                            Address = Address
                        }
                    );
                }

                return new __UnicastIPAddressInformationCollection
                {
                    InternalValue = a
                };
            }
        }

        public static implicit operator global::System.Net.NetworkInformation.IPInterfaceProperties(__IPInterfaceProperties i)
        {
            return (global::System.Net.NetworkInformation.IPInterfaceProperties)(object)i;
        }
    }
}
