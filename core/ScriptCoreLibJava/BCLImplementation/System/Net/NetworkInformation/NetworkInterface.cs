using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Collections;

namespace ScriptCoreLibJava.BCLImplementation.System.Net.NetworkInformation
{
	[Script(Implements = typeof(global::System.Net.NetworkInformation.NetworkInterface))]
	internal class __NetworkInterface
	{
		public java.net.NetworkInterface InternalValue;

		public string Description
		{
			get
			{
				return InternalValue.getDisplayName();
			}
		}

		public string Name
		{
			get
			{
				return InternalValue.getName();
			}
		}

		public static global::System.Net.NetworkInformation.NetworkInterface[] GetAllNetworkInterfaces()
		{
			var i = default(java.util.Enumeration);

			try
			{
				i = java.net.NetworkInterface.getNetworkInterfaces();
			}
			catch
			{
				throw new InvalidOperationException();
			}

			var a = new ArrayList();

			while (i.hasMoreElements())
			{
				var current = (java.net.NetworkInterface)i.nextElement();

				a.Add(
					new __NetworkInterface
					{
						InternalValue = current
					}
				);
			}

			return (global::System.Net.NetworkInformation.NetworkInterface[])a.ToArray(typeof(global::System.Net.NetworkInformation.NetworkInterface));

		}
	}
}
