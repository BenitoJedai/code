using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Net;
using ScriptCoreLib.Shared.BCLImplementation.System.Net;

namespace ScriptCoreLibJava.BCLImplementation.System.Net
{
	// https://github.com/mono/mono/blob/master/mcs/class/System/System.Net/IPEndPoint.cs
	// X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Net\IPEndPoint.cs
	// X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Net\IPEndPoint.cs

	[Script(Implements = typeof(global::System.Net.IPEndPoint))]
	internal class __IPEndPoint : __EndPoint
	{
		public __IPEndPoint(IPAddress address, int port)
		{
			this.Address = address;
			this.Port = port;
		}

		public IPAddress Address { get; set; }
		public int Port { get; set; }

		public override string ToString()
		{
			return this.Address + ":" + this.Port;
		}
	}
}
