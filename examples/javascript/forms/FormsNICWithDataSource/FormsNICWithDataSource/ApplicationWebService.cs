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
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140423
        // X:\jsc.svn\examples\javascript\test\TestWebMethodTaskOfIEnumerable\TestWebMethodTaskOfIEnumerable\ApplicationWebService.cs

        // http://stackoverflow.com/questions/5061761/is-it-possible-to-await-yield-return-dosomethingasync
        // X:\jsc.svn\examples\javascript\forms\FormsNIC\FormsNIC\ApplicationWebService.cs


        [Obsolete("jsc either buffers all or could send in a hint, like take 5 or skip 6")]
        public async Task<IEnumerable<Data.NICDataGetInterfacesRow>> GetInterfaces()
        {
            return
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

                select z;
        }
    }
}
