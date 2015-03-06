using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace xchrome.BCLImplementation.System.Net.Sockets
{
	// http://referencesource.microsoft.com/#System/net/System/Net/Sockets/UdpClient.cs
	// https://github.com/mono/mono/blob/master/mcs/class/System/System.Net.Sockets/UdpClient.cs
	// X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Net\Sockets\UdpClient.cs
	// X:\jsc.svn\market\synergy\javascript\chrome\chrome\BCLImplementation\System\Net\Sockets\UdpClient.cs


	[Script(Implements = typeof(global::System.Net.Sockets.UdpClient))]
	public class __UdpClient
	{
		private const int MaxUDPSize = 0x10000;

		// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150306/udp

		public Task<UdpReceiveResult> ReceiveAsync()
		{
			return null;
		}

		public Task<int> SendAsync(byte[] datagram, int bytes)
		{
			return null;
		}
	}
}
