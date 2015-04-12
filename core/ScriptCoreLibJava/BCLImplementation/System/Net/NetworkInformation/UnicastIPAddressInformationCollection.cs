using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;
using System.Net.NetworkInformation;

namespace ScriptCoreLibJava.BCLImplementation.System.Net.NetworkInformation
{
    // http://referencesource.microsoft.com/#System/net/System/Net/NetworkInformation/UnicastIPAddressInformationCollection.cs

    [Script(Implements = typeof(global::System.Net.NetworkInformation.UnicastIPAddressInformationCollection))]
    internal class __UnicastIPAddressInformationCollection : IEnumerable<__UnicastIPAddressInformation>
    {
        public List<__UnicastIPAddressInformation> InternalValue;

        public virtual int Count
        {
            get
            {
                return InternalValue.Count;
            }
        }

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

        public override string ToString()
        {
            return new { InternalValue.Count }.ToString();
        }
    }
}
