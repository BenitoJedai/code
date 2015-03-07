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
	// http://referencesource.microsoft.com/#System/net/System/Net/Sockets/TcpClient.cs
	// https://github.com/mono/mono/blob/master/mcs/class/System/System.Net.Sockets/TcpClient.cs
	// x:\jsc.svn\core\scriptcorelibjava\bclimplementation\system\net\sockets\tcpclient.cs
	// X:\jsc.svn\market\synergy\javascript\chrome\chrome\BCLImplementation\System\Net\Sockets\TcpClient.cs

	[Script(Implements = typeof(global::System.Net.Sockets.TcpClient))]
	internal class __TcpClient : IDisposable
	{
		// X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Net\IPAddress.cs
		public void Dispose()
		{
		}

		public static implicit operator global::System.Net.Sockets.TcpClient(__TcpClient i) => (global::System.Net.Sockets.TcpClient)(object)i;
		public static implicit operator __TcpClient(global::System.Net.Sockets.TcpClient i) => (__TcpClient)(object)i;

		// X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerAsync\ChromeTCPServerAsync\Application.cs
		// object not ready to be inspected yet?
		public Func<string> VirtualToString = () => nameof(global::System.Net.Sockets.TcpClient);
		public override string ToString() => VirtualToString();

		public Func<NetworkStream> VirtualGetStream;
		public NetworkStream GetStream() => VirtualGetStream();


		public Action VirtualClose;
		public void Close() => VirtualClose();
    }
}
