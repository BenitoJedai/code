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
using System.Xml.Linq;

namespace FormsNICWithDataSource
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class ApplicationWebService : Component
    {

        // X:\jsc.svn\examples\javascript\forms\FormsNIC\FormsNIC\ApplicationWebService.cs


        public async Task<Data.NICDataGetInterfacesRow[]> GetInterfaces()
        {
            //return null;

            // when can we send back Task of IEnumerable?
            return Enumerable.ToArray(
                from n in NetworkInterface.GetAllNetworkInterfaces()
                let Name = n.Name
                        + " | " + n.Description

                let SupportsMulticast = Convert.ToString(n.SupportsMulticast)
                let IPProperties = n.GetIPProperties()
                let GatewayAddresses = IPProperties.UnicastAddresses.Aggregate(
                    "", (state, item) => state + "; " + item.Address
                )

                let z = new Data.NICDataGetInterfacesRow
                {
                    Name = Name,
                    SupportsMulticast = SupportsMulticast,
                    GatewayAddresses = GatewayAddresses
                }

                orderby IPProperties.UnicastAddresses.Count > 0

                select z
            );
        }
    }
}
