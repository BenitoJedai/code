using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Net
{
	// http://referencesource.microsoft.com/#System/net/System/Net/IPAddress.cs
	// https://github.com/mono/mono/blob/master/mcs/class/System/System.Net/IPAddress.cs
	// X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Net\IPAddress.cs
	// X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Net\IPAddress.cs

	[Script(Implements = typeof(global::System.Net.IPAddress))]
	public class __IPAddress
	{
		// x:\jsc.svn\market\synergy\javascript\chrome\chrome\bclimplementation\system\net\sockets\tcplistener.cs

		public static readonly IPAddress Any = new __IPAddress();

		public string ipString;

		// X:\jsc.svn\examples\javascript\chrome\apps\ChromeUDPReceiveAsync\ChromeUDPReceiveAsync\Application.cs
		public static IPAddress Parse(string ipString)
		{
			return new __IPAddress { ipString = ipString };
		}



		public static implicit operator global::System.Net.IPAddress(__IPAddress i)
		{
			return (global::System.Net.IPAddress)(object)i;
		}

		public static implicit operator __IPAddress(global::System.Net.IPAddress i)
		{
			return (__IPAddress)(object)i;
		}
	}
}
