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

            try
            {
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

                        let gkey = new { x.u, x.n.Description, key }
                        //let gvalue = new { key }

                        // Caused by: java.lang.RuntimeException: Implement IComparable for __AnonymousTypes__JVMCLRPrivateAddress__i__d_jvm.__f__AnonymousType_115__82__110_d_1 vs __AnonymousTypes__JVMCLRPrivateAddress__i__d_jvm.__f__AnonymousType_115__82__110_d_1

                        group gkey by key;


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


            }
            catch (Exception ex)
            {
                //                java.lang.Object, rt
                //{ Message = , StackTrace = java.lang.RuntimeException
                //        at ScriptCoreLib.Shared.BCLImplementation.System.Linq.__Enumerable.GroupBy(__Enumerable.java:248)
                //        at JVMCLRPrivateAddress.Program.main(Program.java:342)
                // }
                //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089


                Console.WriteLine(
                    new { ex.Message, ex.StackTrace }
                    );
            }

            // X:\jsc.svn\examples\javascript\android\AndroidPrivateAddress\AndroidPrivateAddress\ApplicationWebService.cs


            //{ Key = true, gi = 0 }
            //{ Address = 192.168.43.252, Description = Atheros AR9485WB-EG Wireless Network Adapter }
            //{ Key = false, gi = 1 }
            //{ Address = 127.0.0.1, Description = Software Loopback Interface 1 }
            //{ Address = 0:0:0:0:0:0:0:1, Description = Software Loopback Interface 1 }
            //{ Address = fe80:0:0:0:55cc:63eb:5b4:60b4%11, Description = Atheros AR9485WB-EG Wireless Network Adapter }
            //{ Address = fe80:0:0:0:81b:4a67:d4b1:2d08%15, Description = TAP-Win32 Adapter V9 }
            //{ Address = 2001:0:9d38:6ab8:1ca7:8b5:3f57:d403, Description = Teredo Tunneling Pseudo-Interface }
            //{ Address = fe80:0:0:0:1ca7:8b5:3f57:d403%22, Description = Teredo Tunneling Pseudo-Interface }
            //{ Address = fe80:0:0:0:0:5efe:c0a8:2bfc%48, Description = Microsoft ISATAP Adapter #6 }
            //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089



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
