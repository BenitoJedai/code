using ScriptCoreLib.Shared.BCLImplementation.System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Net
{
	// http://referencesource.microsoft.com/#System/net/System/Net/IPEndPoint.cs
	// https://github.com/mono/mono/blob/master/mcs/class/System/System.Net/IPEndPoint.cs
	// X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Net\IPEndPoint.cs
	// X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Net\IPEndPoint.cs

	[Script(Implements = typeof(global::System.Net.IPEndPoint))]
	internal class __IPEndPoint : __EndPoint
	{
		// x:\jsc.svn\market\synergy\javascript\chrome\chrome\bclimplementation\system\net\sockets\tcplistener.cs

		public IPAddress Address { get; set; }
		public int Port { get; set; }

		public __IPEndPoint(IPAddress address, int port)
		{
			this.Address = address;
			this.Port = port;
		}





		public static implicit operator global::System.Net.IPEndPoint(__IPEndPoint i)
		{
			return (global::System.Net.IPEndPoint)(object)i;
		}

		public static implicit operator __IPEndPoint(global::System.Net.IPEndPoint i)
		{
			return (__IPEndPoint)(object)i;
		}
	}
}
