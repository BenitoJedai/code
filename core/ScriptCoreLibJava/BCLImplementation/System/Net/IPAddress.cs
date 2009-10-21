using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Net;

namespace ScriptCoreLibJava.BCLImplementation.System.Net
{
	[Script(Implements = typeof(global::System.Net.IPAddress))]
	internal class __IPAddress
	{
		public java.net.InetAddress InternalAddress;

		public static readonly IPAddress Loopback;

		static __IPAddress()
		{
			// fixme: jsc is too agressive here to inline the static initializer
			// and causes a fault here
			__IPAddress.Loopback = InitializeLoopback();
		}

		private static IPAddress InitializeLoopback()
		{

			var Loopback = default(__IPAddress);

			try
			{
				Loopback = new __IPAddress { InternalAddress = java.net.InetAddress.getLocalHost() };
			}
			catch
			{
				throw new InvalidOperationException();
			}

			return (IPAddress)(object)Loopback;
		}

		public override string ToString()
		{
			return this.InternalAddress.ToString();
		}
	}
}
