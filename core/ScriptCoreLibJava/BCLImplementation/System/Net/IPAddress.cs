﻿using System;
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
        public global::java.net.InetAddress InternalAddress;

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

       //         Caused by: android.os.NetworkOnMainThreadException
       //at android.os.StrictMode$AndroidBlockGuardPolicy.onNetwork(StrictMode.java:1117)
       //at java.net.InetAddress.lookupHostByName(InetAddress.java:385)
       //at java.net.InetAddress.getLocalHost(InetAddress.java:365)
       //at ScriptCoreLibJava.BCLImplementation.System.Net.__IPAddress.InitializeLoopback(__IPAddress.java:36)
       //... 18 more

                Loopback = new __IPAddress { InternalAddress = global::java.net.InetAddress.getLocalHost() };
			}
			catch
			{
				throw;
			}

			return (IPAddress)(object)Loopback;
		}

		public override string ToString()
		{
			// http://www.devx.com/tips/Tip/24147
			return this.InternalAddress.getHostAddress();
		}
	}
}
