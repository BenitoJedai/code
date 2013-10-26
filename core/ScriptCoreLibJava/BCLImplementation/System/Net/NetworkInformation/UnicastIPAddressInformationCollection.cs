﻿using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;
using System.Net.NetworkInformation;

namespace ScriptCoreLibJava.BCLImplementation.System.Net.NetworkInformation
{
    [Script(Implements = typeof(global::System.Net.NetworkInformation.UnicastIPAddressInformationCollection))]
    internal class __UnicastIPAddressInformationCollection : IEnumerable<__UnicastIPAddressInformation>
    {
        public IEnumerable<__UnicastIPAddressInformation> InternalValue;

        public static implicit operator global::System.Net.NetworkInformation.UnicastIPAddressInformationCollection(__UnicastIPAddressInformationCollection i)
        {
            return (global::System.Net.NetworkInformation.UnicastIPAddressInformationCollection)(object)i;
        }

        public IEnumerator<__UnicastIPAddressInformation> GetEnumerator()
        {
            return InternalValue.GetEnumerator();
        }

        global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
        {
            return InternalValue.GetEnumerator();
        }
    }
}
