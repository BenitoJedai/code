extern alias xglobal;
using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.Extensions;
using xglobal::chrome;
using System.Net;

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

		public __UdpClient()
		{
			// x:\jsc.svn\examples\javascript\chrome\apps\chromeudpsendasync\chromeudpsendasync\application.cs

			// async ctors?
			var isocket_after_create = socket.create("udp", new object());
			var afterbind = new TaskCompletionSource<int>();


			this.Client = new __Socket
			{
				vBind = async (EndPoint localEP) =>
				{
					var isocket = await isocket_after_create;

					var v4 = localEP as IPEndPoint;
					if (v4 != null)
					{
						var bind = await isocket.socketId.bind(
							address: "0.0.0.0",
							port: v4.Port
						);

						Console.WriteLine("UdpClient.Client.vBind " + new { bind });

						afterbind.SetResult(bind);
					}
				}
			};

			#region vClose
			this.vClose = async delegate
			{
				var isocket = await isocket_after_create;

				isocket.socketId.disconnect();
				isocket.socketId.destroy();
			};
			#endregion

			#region vSendAsync
			this.vSendAsync = async (byte[] datagram, int bytes, string hostname, int port) =>
			{
				// now we need it
				var isocket = await isocket_after_create;

				// are we bound?
				await afterbind.Task;

				var data = new ScriptCoreLib.JavaScript.WebGL.Uint8Array(datagram);

				var result = await isocket.socketId.sendTo(
						 data.buffer,
						 hostname,
						 port
					 );

				// sent: -15 no bind?
				Console.WriteLine("UdpClient.vSendAsync " + new { result.bytesWritten });

				return result.bytesWritten;
			};
			#endregion



		}



		public Socket Client { get; set; }


		public Action vClose;
		public void Close() => vClose();

		public Task<UdpReceiveResult> ReceiveAsync()
		{
			return null;
		}

		public SendAsyncDelegate vSendAsync;
		public delegate Task<int> SendAsyncDelegate(byte[] datagram, int bytes, string hostname, int port);
		public Task<int> SendAsync(byte[] datagram, int bytes, string hostname, int port) => vSendAsync(datagram, bytes, hostname, port);

		public Task<int> SendAsync(byte[] datagram, int bytes)
		{
			return null;
		}
	}
}
