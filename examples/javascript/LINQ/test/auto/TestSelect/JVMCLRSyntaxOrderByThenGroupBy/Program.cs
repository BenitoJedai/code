using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using ScriptCoreLib.Query.Experimental;

namespace JVMCLRSyntaxOrderByThenGroupBy
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var f = (
                   from x in new xTable()

                   orderby x.field1 ascending

                   group x by 1 into gg

                   select new
                   {
                       gg.Last().Tag
                   }

               ).FirstOrDefault();

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

            // X:\jsc.svn\examples\javascript\forms\FormsNIC\FormsNIC\ApplicationWebService.cs

            //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
            //{ Name = Local Area Connection, Description = TAP-Win32 Adapter V9, SupportsMulticast = True, NetworkInterfaceType = Ethernet, InetAddressesString =  }
            //{ Name = Wireless Network Connection, Description = Atheros AR9485WB-EG Wireless Network Adapter, SupportsMulticast = True, NetworkInterfaceType = Wireless80211, InetAddressesString = , 192.168.43.1 }
            //{ Name = Loopback Pseudo-Interface 1, Description = Software Loopback Interface 1, SupportsMulticast = True, NetworkInterfaceType = Loopback, InetAddressesString =  }
            //{ Name = Teredo Tunneling Pseudo-Interface, Description = Teredo Tunneling Pseudo-Interface, SupportsMulticast = False, NetworkInterfaceType = Tunnel, InetAddressesString = , :: }
            //{ Name = isatap.{84D54A45-2ACC-42B2-A59B-E01D43897D2D}, Description = Microsoft ISATAP Adapter #2, SupportsMulticast = False, NetworkInterfaceType = Tunnel, InetAddressesString =  }
            //{ Name = isatap.{CE7A76DF-BCB0-4C3B-8466-D712A03F10A0}, Description = Microsoft ISATAP Adapter #5, SupportsMulticast = False, NetworkInterfaceType = Tunnel, InetAddressesString =  }
            //{ Name = Reusable ISATAP Interface {D17267A3-075D-49F6-83A4-BF4ADA717ADE}, Description = Microsoft ISATAP Adapter #6, SupportsMulticast = False, NetworkInterfaceType = Tunnel, InetAddressesString =  }

            //{ Name = Local Area Connection, Description = TAP-Win32 Adapter V9, SupportsMulticast = True, NetworkInterfaceType = Ethernet, InetAddressesString = , 169.254.45.8 }
            //{ Name = Wireless Network Connection, Description = Atheros AR9485WB-EG Wireless Network Adapter, SupportsMulticast = True, NetworkInterfaceType = Wireless80211, InetAddressesString = , 192.168.43.252 }
            //{ Name = Loopback Pseudo-Interface 1, Description = Software Loopback Interface 1, SupportsMulticast = True, NetworkInterfaceType = Loopback, InetAddressesString = , 127.0.0.1 }
            //{ Name = Teredo Tunneling Pseudo-Interface, Description = Teredo Tunneling Pseudo-Interface, SupportsMulticast = False, NetworkInterfaceType = Tunnel, InetAddressesString =  }
            //{ Name = isatap.{84D54A45-2ACC-42B2-A59B-E01D43897D2D}, Description = Microsoft ISATAP Adapter #2, SupportsMulticast = False, NetworkInterfaceType = Tunnel, InetAddressesString =  }
            //{ Name = isatap.{CE7A76DF-BCB0-4C3B-8466-D712A03F10A0}, Description = Microsoft ISATAP Adapter #5, SupportsMulticast = False, NetworkInterfaceType = Tunnel, InetAddressesString =  }
            //{ Name = Reusable ISATAP Interface {D17267A3-075D-49F6-83A4-BF4ADA717ADE}, Description = Microsoft ISATAP Adapter #6, SupportsMulticast = False, NetworkInterfaceType = Tunnel, InetAddressesString =  }

            //{ OperationalStatus = Down, Name = Local Area Connection, Description = TAP-Win32 Adapter V9, SupportsMulticast = True, NetworkInterfaceType = Ethernet, InetAddressesString = , 169.254.45.8 }
            //{ OperationalStatus = Up, Name = Wireless Network Connection, Description = Atheros AR9485WB-EG Wireless Network Adapter, SupportsMulticast = True, NetworkInterfaceType = Wireless80211, InetAddressesString = , 192.168.43.252 }
            //{ OperationalStatus = Up, Name = Loopback Pseudo-Interface 1, Description = Software Loopback Interface 1, SupportsMulticast = True, NetworkInterfaceType = Loopback, InetAddressesString = , 127.0.0.1 }
            //{ OperationalStatus = Up, Name = Teredo Tunneling Pseudo-Interface, Description = Teredo Tunneling Pseudo-Interface, SupportsMulticast = False, NetworkInterfaceType = Tunnel, InetAddressesString =  }
            //{ OperationalStatus = Down, Name = isatap.{84D54A45-2ACC-42B2-A59B-E01D43897D2D}, Description = Microsoft ISATAP Adapter #2, SupportsMulticast = False, NetworkInterfaceType = Tunnel, InetAddressesString =  }
            //{ OperationalStatus = Down, Name = isatap.{CE7A76DF-BCB0-4C3B-8466-D712A03F10A0}, Description = Microsoft ISATAP Adapter #5, SupportsMulticast = False, NetworkInterfaceType = Tunnel, InetAddressesString =  }
            //{ OperationalStatus = Down, Name = Reusable ISATAP Interface {D17267A3-075D-49F6-83A4-BF4ADA717ADE}, Description = Microsoft ISATAP Adapter #6, SupportsMulticast = False, NetworkInterfaceType = Tunnel, InetAddressesString =  }

            //{ OperationalStatus = 1, Name = lo, Description = Software Loopback Interface 1, SupportsMulticast = true, InetAddressesString = , 127.0.0.1, 0:0:0:0:0:0:0:1 }
            //{ OperationalStatus = 2, Name = net0, Description = WAN Miniport (SSTP), SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = net1, Description = WAN Miniport (L2TP), SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = net2, Description = WAN Miniport (PPTP), SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = ppp0, Description = WAN Miniport (PPPOE), SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = eth0, Description = WAN Miniport (IPv6), SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = eth1, Description = WAN Miniport (Network Monitor), SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = eth2, Description = WAN Miniport (IP), SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = ppp1, Description = RAS Async Adapter, SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = net3, Description = WAN Miniport (IKEv2), SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 1, Name = net4, Description = Atheros AR9485WB-EG Wireless Network Adapter, SupportsMulticast = true, InetAddressesString = , 192.168.43.252, fe80:0:0:0:55cc:63eb:5b4:60b4%11 }
            //{ OperationalStatus = 2, Name = net5, Description = Bluetooth Device (RFCOMM Protocol TDI), SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = eth3, Description = Bluetooth Device (Personal Area Network), SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = net6, Description = Microsoft Virtual WiFi Miniport Adapter, SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = eth4, Description = TAP-Win32 Adapter V9, SupportsMulticast = true, InetAddressesString = , fe80:0:0:0:81b:4a67:d4b1:2d08%15 }
            //{ OperationalStatus = 2, Name = net7, Description = HUAWEI Mobile Connect - 3G Network Card, SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = eth5, Description = ASIX AX88772B USB2.0 to Fast Ethernet Adapter, SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = net8, Description = HUAWEI Mobile Connect - 3G Network Card #2, SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = net9, Description = Microsoft 6to4 Adapter, SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = net10, Description = , SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = net11, Description = Microsoft ISATAP Adapter #5, SupportsMulticast = false, InetAddressesString =  }
            //{ OperationalStatus = 1, Name = net12, Description = Teredo Tunneling Pseudo-Interface, SupportsMulticast = false, InetAddressesString = , 2001:0:9d38:6ab8:3c8c:2cb9:3f57:d403, fe80:0:0:0:3c8c:2cb9:3f57:d403%22 }
            //{ OperationalStatus = 2, Name = net13, Description = Microsoft ISATAP Adapter #3, SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = net14, Description = Microsoft ISATAP Adapter #2, SupportsMulticast = false, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = net15, Description = Microsoft ISATAP Adapter #4, SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = net16, Description = Microsoft 6to4 Adapter-Netmon Lightweight Filter Driver-0000, SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = net17, Description = Microsoft ISATAP Adapter #2-Netmon Lightweight Filter Driver-0000, SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = net18, Description = Microsoft ISATAP Adapter #3-Netmon Lightweight Filter Driver-0000, SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = net19, Description = Microsoft ISATAP Adapter #4-Netmon Lightweight Filter Driver-0000, SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = net20, Description = Microsoft ISATAP Adapter #5-Netmon Lightweight Filter Driver-0000, SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = net21, Description = Teredo Tunneling Pseudo-Interface-Netmon Lightweight Filter Driver-0000, SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = net22, Description = Atheros AR9485WB-EG Wireless Network Adapter-Native WiFi Filter Driver-0000, SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = net23, Description = Atheros AR9485WB-EG Wireless Network Adapter-QoS Packet Scheduler-0000, SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = net24, Description = Atheros AR9485WB-EG Wireless Network Adapter-WFP LightWeight Filter-0000, SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = eth6, Description = WAN Miniport (Network Monitor)-Netmon Lightweight Filter Driver-0000, SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = eth7, Description = WAN Miniport (Network Monitor)-QoS Packet Scheduler-0000, SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = eth8, Description = WAN Miniport (IP)-QoS Packet Scheduler-0000, SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = eth9, Description = WAN Miniport (IPv6)-QoS Packet Scheduler-0000, SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = eth10, Description = TAP-Win32 Adapter V9-Netmon Lightweight Filter Driver-0000, SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = eth11, Description = TAP-Win32 Adapter V9-QoS Packet Scheduler-0000, SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = eth12, Description = TAP-Win32 Adapter V9-WFP LightWeight Filter-0000, SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = net25, Description = Atheros AR9485WB-EG Wireless Network Adapter-Netmon Lightweight Filter Driver-0000, SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = net26, Description = Atheros AR9485WB-EG Wireless Network Adapter-Virtual WiFi Filter Driver-0000, SupportsMulticast = true, InetAddressesString =  }
            //{ OperationalStatus = 2, Name = net27, Description = Microsoft ISATAP Adapter #6, SupportsMulticast = false, InetAddressesString = , fe80:0:0:0:0:5efe:c0a8:2bfc%48 }
            //{ OperationalStatus = 2, Name = net28, Description = Microsoft ISATAP Adapter #6-Netmon Lightweight Filter Driver-0000, SupportsMulticast = true, InetAddressesString =  }
            //--



            System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces().WithEach(
                n =>
                {
                    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Net\NetworkInformation\NetworkInterface.cs

                    var IPProperties = n.GetIPProperties();
                    var PhysicalAddress = n.GetPhysicalAddress();

                    var InetAddressesString = "";


                    foreach (var ip in IPProperties.UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            Console.WriteLine(ip.Address.ToString());
                            InetAddressesString += ", " + ip.Address;
                        }
                    }

                    // Address = {192.168.43.1}
                    //IPProperties.GatewayAddresses.WithEach(
                    //    g =>
                    //    {
                    //        InetAddressesString += ", " + g.Address;
                    //    }
                    //);


                    Console.WriteLine(new
                    {
                        n.OperationalStatus,

                        n.Name,
                        n.Description,
                        n.SupportsMulticast,
                        //n.NetworkInterfaceType,

                        InetAddressesString
                    });
                }
            );

            MessageBox.Show("click to close");

        }
    }


}
