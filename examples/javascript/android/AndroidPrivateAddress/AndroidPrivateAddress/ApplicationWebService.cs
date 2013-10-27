using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AndroidPrivateAddress
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class ApplicationWebService : Component
    {
        public Task<DataTable> GetInterfaces()
        {
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



            // X:\jsc.svn\examples\javascript\forms\Test\TestDataTableToJavascript\TestDataTableToJavascript\ApplicationWebService.cs

            var table = new DataTable
            {

                TableName = "GetInterfaces"
            };




            var cAddress = new DataColumn
            {
                ColumnName = "Address",
                ReadOnly = true

            };

            table.Columns.Add(cAddress);

            var cComment = new DataColumn
            {
                ColumnName = "Comment",
                ReadOnly = true

            };

            table.Columns.Add(cComment);

            g.OrderByDescending(k => k.Key).WithEachIndex(
                (gx, gi) =>
                {

                    if (gi > 0)
                    {
                        var row = table.NewRow();

                        row[cAddress] = "";
                        row[cComment] = "";

                        table.Rows.Add(row);
                    }

                    gx.WithEach(
                          x =>
                          {
                              var row = table.NewRow();

                              row[cAddress] = x.u.Address.ToString();
                              row[cComment] = new { x.key, x.Description }.ToString();

                              table.Rows.Add(row);
                          }
                      );

                }
            );






            return table.ToTaskResult();

        }


    }
}
