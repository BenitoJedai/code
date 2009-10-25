using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Net;

namespace ScriptCoreLibJava.BCLImplementation.System.Net
{
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
