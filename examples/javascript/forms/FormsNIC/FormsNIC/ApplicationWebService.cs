using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FormsNIC
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class ApplicationWebService : Component
    {
        public Task<DataTable> GetInterfaces()
        {

            // X:\jsc.svn\examples\javascript\forms\Test\TestDataTableToJavascript\TestDataTableToJavascript\ApplicationWebService.cs

            var table = new DataTable
            {

                TableName = "GetInterfaces"
            };


            var column = new DataColumn();
            column.ColumnName = "Name";
            column.ReadOnly = true;

            // An exception of type 'System.Data.DuplicateNameException' occurred in System.Data.dll but was not handled in user code
            //Additional information: A column named 'Column 2' already belongs to this DataTable.


            table.Columns.Add(column);


            var cSupportsMulticast = new DataColumn
            {
                ColumnName = "SupportsMulticast",
                ReadOnly = true

            };

            table.Columns.Add(cSupportsMulticast);

            var cGatewayAddresses = new DataColumn
            {
                ColumnName = "GatewayAddresses",
                ReadOnly = true

            };

            table.Columns.Add(cGatewayAddresses);



            NetworkInterface.GetAllNetworkInterfaces().WithEach(
                n =>
                {
                    var row = table.NewRow();

                    //row[column] = n.Id + " | " + n.Name;
                    row[column] = n.Name
                        + " | " + n.Description
                        + " | " + n.GetPhysicalAddress().GetAddressBytes().ToHexString();


                    //     //{ NetworkInterfaceName = net4, supportsMulticast = true, isUp = true, isVirtual = false, 
                    // DisplayName = Atheros AR9485WB-EG Wireless Network Adapter, 
                    // InetAddressesString = , /192.168.43.252, /fe80:0:0:0:55cc:63eb:5b4:60b4%11 }

                    row[cSupportsMulticast] = Convert.ToString(n.SupportsMulticast);

                    var IPProperties = n.GetIPProperties();
                    //var PhysicalAddress = n.GetPhysicalAddress();

                    var InetAddressesString = "";

                    // Address = {192.168.43.1}
                    IPProperties.GatewayAddresses.WithEach(
                        g =>
                        {
                            InetAddressesString += ", " + g.Address;
                        }
                    );
                    row[cGatewayAddresses] = InetAddressesString;

                    table.Rows.Add(row);

                }
            );


            return table.ToTaskResult();

        }
    }
}
