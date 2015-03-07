using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace xchrome.BCLImplementation.System.Net.Sockets
{
	[Script(Implements = typeof(global::System.Net.Sockets.UdpReceiveResult))]
	public class __UdpReceiveResult
	{
		public __UdpReceiveResult(byte[] buffer, IPEndPoint remoteEndPoint)
		{
			this.Buffer = buffer;
			this.RemoteEndPoint = remoteEndPoint;
        }

		// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150306/udp
		public IPEndPoint RemoteEndPoint { get; set; }
		public byte[] Buffer { get; set; }
	}
}
