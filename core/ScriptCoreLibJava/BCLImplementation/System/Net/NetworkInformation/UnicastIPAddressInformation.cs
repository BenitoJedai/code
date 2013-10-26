using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;
using System.Net.NetworkInformation;

namespace ScriptCoreLibJava.BCLImplementation.System.Net.NetworkInformation
{
    [Script(Implements = typeof(global::System.Net.NetworkInformation.UnicastIPAddressInformation))]
    internal class __UnicastIPAddressInformation : __IPAddressInformation
    {
        public static implicit operator global::System.Net.NetworkInformation.UnicastIPAddressInformation(__UnicastIPAddressInformation i)
        {
            return (global::System.Net.NetworkInformation.UnicastIPAddressInformation)(object)i;
        }
    }
}
