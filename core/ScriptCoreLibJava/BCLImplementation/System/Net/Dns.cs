using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Net;
using java.net;

namespace ScriptCoreLibJava.BCLImplementation.System.Net
{
    // http://referencesource.microsoft.com/#System/net/System/Net/DNS.cs
    // https://github.com/mono/mono/blob/master/mcs/class/System/System.Net/Dns.cs

	[Script(Implements = typeof(global::System.Net.Dns))]
	internal class __Dns
	{
        // what about a dns server?

        public static IPAddress[] GetHostAddresses(string hostNameOrAddress)
        {
            return GetHostEntry(hostNameOrAddress).AddressList;
        }

		public static IPHostEntry GetHostEntry(string hostNameOrAddress)
		{
			var a = default(InetAddress[]);

			try
			{
				a = InetAddress.getAllByName(hostNameOrAddress);
			}
			catch
			{
				throw new InvalidOperationException();
			}

			var z = new __IPAddress[a.Length];

			for (int j = 0; j < a.Length; j++)
			{

				z[j] = new __IPAddress { InternalAddress = a[j] };
			}

			var i = new __IPHostEntry();

			i.AddressList = (IPAddress[])(object)z;

			return (IPHostEntry)(object)i;
		}
	}
}
