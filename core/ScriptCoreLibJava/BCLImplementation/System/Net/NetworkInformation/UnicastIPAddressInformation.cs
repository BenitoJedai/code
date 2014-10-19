using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;
using System.Net.NetworkInformation;

namespace ScriptCoreLibJava.BCLImplementation.System.Net.NetworkInformation
{
    // http://referencesource.microsoft.com/#System/net/System/Net/NetworkInformation/UnicastIPAddressInformation.cs
    // https://github.com/mono/mono/blob/master/mcs/class/System/System.Net.NetworkInformation/UnicastIPAddressInformation.cs

    [Script(Implements = typeof(global::System.Net.NetworkInformation.UnicastIPAddressInformation))]
    internal class __UnicastIPAddressInformation : __IPAddressInformation
    {
        public static implicit operator global::System.Net.NetworkInformation.UnicastIPAddressInformation(__UnicastIPAddressInformation i)
        {
            return (global::System.Net.NetworkInformation.UnicastIPAddressInformation)(object)i;
        }
    }
}
