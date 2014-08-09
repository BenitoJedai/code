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

namespace JVMCLRNIC
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // jsc needs to see args to make Main into main for javac..


            // see also>
            // X:\jsc.svn\examples\javascript\android\AndroidBroadcastLogger\AndroidBroadcastLogger\ApplicationWebService.cs

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );

            System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces().WithEach(
             n =>
             {
                 // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Net\NetworkInformation\NetworkInterface.cs

                 var IPProperties = n.GetIPProperties();
                 var PhysicalAddress = n.GetPhysicalAddress();

                 var InetAddressesString = "";


                 foreach (var ip in IPProperties.UnicastAddresses)
                 {
                     //if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                     //{
                     //Console.WriteLine(ip.Address.ToString());
                     InetAddressesString += ", " + ip.Address;
                     //}
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

            Console.WriteLine("--");


            //java.lang.Object, rt
            //{ NetworkInterfaceName = lo, supportsMulticast = true, isUp = true, isVirtual = false, DisplayName = Software Loopback Interface 1, InetAddressesString = , /127.0.0.1, /0:0:0:0:0:0:0:1 }
            //{ NetworkInterfaceName = net0, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = WAN Miniport (SSTP), InetAddressesString =  }
            //{ NetworkInterfaceName = net1, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = WAN Miniport (L2TP), InetAddressesString =  }
            //{ NetworkInterfaceName = net2, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = WAN Miniport (PPTP), InetAddressesString =  }
            //{ NetworkInterfaceName = ppp0, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = WAN Miniport (PPPOE), InetAddressesString =  }
            //{ NetworkInterfaceName = eth0, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = WAN Miniport (IPv6), InetAddressesString =  }
            //{ NetworkInterfaceName = eth1, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = WAN Miniport (Network Monitor), InetAddressesString =  }
            //{ NetworkInterfaceName = eth2, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = WAN Miniport (IP), InetAddressesString =  }
            //{ NetworkInterfaceName = ppp1, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = RAS Async Adapter, InetAddressesString =  }
            //{ NetworkInterfaceName = net3, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = WAN Miniport (IKEv2), InetAddressesString =  }
            //selecting: /192.168.43.252
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
            //{ NetworkInterfaceName = net12, supportsMulticast = false, isUp = true, isVirtual = false, DisplayName = Teredo Tunneling Pseudo-Interface, InetAddressesString = , /2001:0:9d38:6ab8:3c8c:2cb9:3f57:d403, /fe80:0:0:0:3c8c:2cb9:3f57:d403%22 }
            //{ NetworkInterfaceName = net13, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = Microsoft ISATAP Adapter #3, InetAddressesString =  }
            //{ NetworkInterfaceName = net14, supportsMulticast = false, isUp = false, isVirtual = false, DisplayName = Microsoft ISATAP Adapter #2, InetAddressesString =  }
            //{ NetworkInterfaceName = net15, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = Microsoft ISATAP Adapter #4, InetAddressesString =  }
            //{ NetworkInterfaceName = net16, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = Microsoft 6to4 Adapter-Netmon Lightweight Filter Driver-0000, InetAddressesString =  }
            //{ NetworkInterfaceName = net17, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = Microsoft ISATAP Adapter #2-Netmon Lightweight Filter Driver-0000, InetAddressesString =  }
            //{ NetworkInterfaceName = net18, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = Microsoft ISATAP Adapter #3-Netmon Lightweight Filter Driver-0000, InetAddressesString =  }
            //{ NetworkInterfaceName = net19, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = Microsoft ISATAP Adapter #4-Netmon Lightweight Filter Driver-0000, InetAddressesString =  }
            //{ NetworkInterfaceName = net20, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = Microsoft ISATAP Adapter #5-Netmon Lightweight Filter Driver-0000, InetAddressesString =  }
            //{ NetworkInterfaceName = net21, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = Teredo Tunneling Pseudo-Interface-Netmon Lightweight Filter Driver-0000, InetAddressesString =  }
            //{ NetworkInterfaceName = net22, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = Atheros AR9485WB-EG Wireless Network Adapter-Native WiFi Filter Driver-0000, InetAddressesString =  }
            //{ NetworkInterfaceName = net23, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = Atheros AR9485WB-EG Wireless Network Adapter-QoS Packet Scheduler-0000, InetAddressesString =  }
            //{ NetworkInterfaceName = net24, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = Atheros AR9485WB-EG Wireless Network Adapter-WFP LightWeight Filter-0000, InetAddressesString =  }
            //{ NetworkInterfaceName = eth6, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = WAN Miniport (Network Monitor)-Netmon Lightweight Filter Driver-0000, InetAddressesString =  }
            //{ NetworkInterfaceName = eth7, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = WAN Miniport (Network Monitor)-QoS Packet Scheduler-0000, InetAddressesString =  }
            //{ NetworkInterfaceName = eth8, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = WAN Miniport (IP)-QoS Packet Scheduler-0000, InetAddressesString =  }
            //{ NetworkInterfaceName = eth9, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = WAN Miniport (IPv6)-QoS Packet Scheduler-0000, InetAddressesString =  }
            //{ NetworkInterfaceName = eth10, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = TAP-Win32 Adapter V9-Netmon Lightweight Filter Driver-0000, InetAddressesString =  }
            //{ NetworkInterfaceName = eth11, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = TAP-Win32 Adapter V9-QoS Packet Scheduler-0000, InetAddressesString =  }
            //{ NetworkInterfaceName = eth12, supp'JVMCLRNIC.exe' (CLR v4.0.30319: JVMCLRNIC.exe): Loaded 'X:\jsc.svn\examples\java\JVMCLRNIC\JVMCLRNIC\bin\Release\JVMCLRNIC.exports'. Module was built without symbols.
            //'JVMCLRNIC.exe' (CLR v4.0.30319: JVMCLRNIC.exe): Loaded 'C:\Windows\Microsoft.Net\assembly\GAC_MSIL\System.Windows.Forms\v4.0_4.0.0.0__b77a5c561934e089\System.Windows.Forms.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.
            //'JVMCLRNIC.exe' (CLR v4.0.30319: JVMCLRNIC.exe): Loaded 'C:\Windows\Microsoft.Net\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.
            //ortsMulticast = true, isUp = false, isVirtual = false, DisplayName = TAP-Win32 Adapter V9-WFP LightWeight Filter-0000, InetAddressesString =  }
            //{ NetworkInterfaceName = net25, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = Atheros AR9485WB-EG Wireless Network Adapter-Netmon Lightweight Filter Driver-0000, InetAddressesString =  }
            //{ NetworkInterfaceName = net26, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = Atheros AR9485WB-EG Wireless Network Adapter-Virtual WiFi Filter Driver-0000, InetAddressesString =  }
            //{ NetworkInterfaceName = net27, supportsMulticast = false, isUp = false, isVirtual = false, DisplayName = Microsoft ISATAP Adapter #6, InetAddressesString = , /fe80:0:0:0:0:5efe:c0a8:2bfc%48 }
            //{ NetworkInterfaceName = net28, supportsMulticast = true, isUp = false, isVirtual = false, DisplayName = Microsoft ISATAP Adapter #6-Netmon Lightweight Filter Driver-0000, InetAddressesString =  }

#if !DEBUG
            try
            {
                //  [ScriptMethodThrows(typeof(SocketException))]
                // jsc should inject try rethrow to please javac
                // or should redefine extension methods that do it?
                var NetworkInterfaces = java.net.NetworkInterface.getNetworkInterfaces();

                var wlan = default(java.net.NetworkInterface);

                //- javac
                //"C:\Program Files (x86)\Java\jdk1.6.0_35\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\JVMCLRNIC\Program.java
                //java\JVMCLRNIC\Program.java:41: unreported exception java.net.SocketException; must be caught or declared to be thrown
                //        enumeration0 = NetworkInterface.getNetworkInterfaces();
                //                                                            ^
                //java\JVMCLRNIC\Program.java:46: unreported exception java.net.SocketException; must be caught or declared to be thrown
                //            flag3 = interface2.isUp();
                //                                   ^
                //java\JVMCLRNIC\Program.java:47: unreported exception java.net.SocketException; must be caught or declared to be thrown
                //            flag4 = interface2.supportsMulticast();
                //                                                ^


                while (NetworkInterfaces.hasMoreElements())
                {
                    var xNetworkInterface = (java.net.NetworkInterface)NetworkInterfaces.nextElement();

                    var isUp = xNetworkInterface.isUp();
                    var supportsMulticast = xNetworkInterface.supportsMulticast();
                    var isVirtual = xNetworkInterface.isVirtual();
                    var DisplayName = xNetworkInterface.getDisplayName();
                    var NetworkInterfaceName = xNetworkInterface.getName();

                    //xNetworkInterface.getHardwareAddress

                    var InetAddresses = xNetworkInterface.getInetAddresses();
                    var InetAddressesString = "";
                    while (InetAddresses.hasMoreElements())
                    {
                        var xInetAddress = (java.net.InetAddress)InetAddresses.nextElement();

                        InetAddressesString += ", " + xInetAddress;

                        //if (!xInetAddress.isLoopbackAddress())
                        //    if (xInetAddress is java.net.Inet4Address)
                        //    {


                        //        wlan = xNetworkInterface;

                        //        Console.WriteLine("selecting: " + xInetAddress);
                        //    }
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

            }
            catch
            {

                throw;
            }
#endif

            CLRProgram.CLRMain();
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
