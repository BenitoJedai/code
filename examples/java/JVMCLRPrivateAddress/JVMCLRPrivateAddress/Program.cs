using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JVMCLRPrivateAddress
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

            //            java.lang.Object, rt
            //'JVMCLRPrivateAddress.exe' (CLR v4.0.30319: JVMCLRPrivateAddress.exe): Loaded 'X:\jsc.svn\examples\java\JVMCLRPrivateAddress\JVMCLRPrivateAddress\bin\Release\JVMCLRPrivateAddress.exports'. Module was built without symbols.
            //The program '[12004] JVMCLRPrivateAddress.exe' has exited with code 0 (0x0).
            //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   

            var data =
                   from n in NetworkInterface.GetAllNetworkInterfaces()

                   let SupportsMulticast = n.SupportsMulticast

                   from u in n.GetIPProperties().UnicastAddresses

                   let IsLoopback = IPAddress.IsLoopback(u.Address)

                   let IPv4 = u.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork

                   // http://compnetworking.about.com/od/workingwithipaddresses/l/aa042400b.htm
                   // http://ipaddressextensions.codeplex.com/SourceControl/latest#WorldDomination.Net/IPAddressExtensions.cs

                   let AddressBytes = u.Address.GetAddressBytes()

                   // should do a full mask check?
                   // http://en.wikipedia.org/wiki/IP_address
                   let PrivateAddresses = new[] { 10, 172, 192 }

                   let IsPrivate = PrivateAddresses.Contains(AddressBytes[0])

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

                    let key = x.IsPrivate && !x.IsLoopback && x.SupportsMulticast && x.IPv4

                    // group by missing from scriptcorelib?
                    group new { x.u, x.n.Description, key } by new { key };


            g.Reverse().WithEachIndex(
                  (gx, gi) =>
                  {

                      Console.WriteLine(new { gx.Key, gi });

                      gx.WithEach(
                            x =>
                            {
                                Console.WriteLine(new { x.u.Address, x.Description });

                            }
                        );

                  }
              );

            // X:\jsc.svn\examples\javascript\android\AndroidPrivateAddress\AndroidPrivateAddress\ApplicationWebService.cs


            //Implementation not found for type import :
            //type: System.Linq.Enumerable
            //method: System.Collections.Generic.IEnumerable`1[System.Linq.IGrouping`2[<>f__AnonymousType$100$$63$$91$9`1[System.Boolean],<>f__AnonymousType$104$$65$$93$a`3[System.Net.NetworkInformation.UnicastIPAddressInformation,System.String,System.Boolean]]] GroupBy[<>f__AnonymousType$95$$60$$88$8`2,<>f__AnonymousType$100$$63$$91$9`1,<>f__AnonymousType$104$$65$$93$a`3](System.Collections.Generic.IEnumerable`1[<>f__AnonymousType$95$$60$$88$8`2[<>f__AnonymousType$86$$53$$81$7`6[System.Boolean,System.Boolean,System.Boolean,System.Boolean,System.Net.NetworkInformation.UnicastIPAddressInformation,System.Net.NetworkInformation.NetworkInterface],System.Boolean]], System.Func`2[<>f__AnonymousType$95$$60$$88$8`2[<>f__AnonymousType$86$$53$$81$7`6[System.Boolean,System.Boolean,System.Boolean,System.Boolean,System.Net.NetworkInformation.UnicastIPAddressInformation,System.Net.NetworkInformation.NetworkInterface],System.Boolean],<>f__AnonymousType$100$$63$$91$9`1[System.Boolean]], System.Func`2[<>f__AnonymousType$95$$60$$88$8`2[<>f__AnonymousType$86$$53$$81$7`6[System.Boolean,System.Boolean,System.Boolean,System.Boolean,System.Net.NetworkInformation.UnicastIPAddressInformation,System.Net.NetworkInformation.NetworkInterface],System.Boolean],<>f__AnonymousType$104$$65$$93$a`3[System.Net.NetworkInformation.UnicastIPAddressInformation,System.String,System.Boolean]])
            //Did you forget to add the [Script] attribute?
            //Please double check the signature!

            //assembly: Y:\staging\JVMCLRPrivateAddress__i__d.jvm.exe
            //type: JVMCLRPrivateAddress.Program, JVMCLRPrivateAddress__i__d.jvm, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            //offset: 0x025a
            // method:Void Main(System.String[])

            //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
            //{ Key = { key = True }, gi = 0 }
            //{ Address = 192.168.43.252, Description = Atheros AR9485WB-EG Wireless Network Adapter }
            //{ Key = { key = False }, gi = 1 }
            //{ Address = fe80::81b:4a67:d4b1:2d08%15, Description = TAP-Win32 Adapter V9 }
            //{ Address = 169.254.45.8, Description = TAP-Win32 Adapter V9 }
            //{ Address = fe80::55cc:63eb:5b4:60b4%11, Description = Atheros AR9485WB-EG Wireless Network Adapter }
            //{ Address = ::1, Description = Software Loopback Interface 1 }
            //{ Address = 127.0.0.1, Description = Software Loopback Interface 1 }
            //{ Address = 2001:0:9d38:6ab8:1ca7:8b5:3f57:d403, Description = Teredo Tunneling Pseudo-Interface }
            //{ Address = fe80::1ca7:8b5:3f57:d403%22, Description = Teredo Tunneling Pseudo-Interface }
            //{ Address = fe80::5efe:192.168.43.252%48, Description = Microsoft ISATAP Adapter #6 }
            //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089




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

            MessageBox.Show("click to close");

        }
    }


}
