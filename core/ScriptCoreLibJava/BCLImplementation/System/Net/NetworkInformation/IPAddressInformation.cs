﻿using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;
using System.Net.NetworkInformation;
using System.Net;

namespace ScriptCoreLibJava.BCLImplementation.System.Net.NetworkInformation
{
    [Script(Implements = typeof(global::System.Net.NetworkInformation.IPAddressInformation))]
    internal class __IPAddressInformation
    {
        public IPAddress Address { get; set; }



        public static implicit operator global::System.Net.NetworkInformation.IPAddressInformation(__IPAddressInformation i)
        {
            return (global::System.Net.NetworkInformation.IPAddressInformation)(object)i;
        }
    }
}
