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

            g.Reverse().WithEachIndex(
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
