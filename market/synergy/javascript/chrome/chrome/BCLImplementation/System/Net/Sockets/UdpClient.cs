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
using ScriptCoreLib.JavaScript.BCLImplementation.System.Net;

namespace xchrome.BCLImplementation.System.Net.Sockets
{
	// http://referencesource.microsoft.com/#System/net/System/Net/Sockets/UdpClient.cs
	// https://github.com/mono/mono/blob/master/mcs/class/System/System.Net.Sockets/UdpClient.cs
	// X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Net\Sockets\UdpClient.cs
	// X:\jsc.svn\market\synergy\javascript\chrome\chrome\BCLImplementation\System\Net\Sockets\UdpClient.cs


	[Script(Implements = typeof(global::System.Net.Sockets.UdpClient))]
	public class __UdpClient
	{
		// https://software.intel.com/en-us/blogs/2012/10/16/developing-for-intel-smart-connect-technology
		//Unattended Active State(S0ISCT).  Similar to S0(standard power on) except that the system looks and sound like it is still in standby – display off, audio off, fans off or muted, etc.. The user will not be interacting with the platform during this state.The platform will periodically enter this state and remain there for a specific duration determined by the ISCT service. There is full network connectivity (if available) in this state. The service will put the system back to sleep within a predefined interval. The exceptions to this activity are when the ISCT agent decides to disable the service and remain in standby (S3) in order to protect the platform from overheating or running low on battery. Should this occur, the platform will not wake up from this state until the user presses the power button.

		// Intel® Smart ConnectTechnology
		// https://software.intel.com/en-us/tags/41560
		// http://www.intel.com/content/www/us/en/architecture-and-technology/smart-connect-technology-compatible-apps-brief.html

		private const int MaxUDPSize = 0x10000;

		// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150306/udp

		public __UdpClient()
		{
			// x:\jsc.svn\examples\javascript\chrome\apps\chromeudpsendasync\chromeudpsendasync\application.cs

			// async ctors?
			var isocket_after_create = socket.create("udp", new object());
			var afterbind = new TaskCompletionSource<int>();


			#region vJoinMulticastGroup
			this.vJoinMulticastGroup = async (IPAddress multicastAddr) =>
			{
				__IPAddress a = multicastAddr;

				var isocket = await isocket_after_create;

				var value_joinGroup = await isocket.socketId.joinGroup(a.ipString);

				Console.WriteLine("UdpClient.vJoinMulticastGroup " + new { value_joinGroup });
			};
			#endregion


			#region vReceiveAsync
			this.vReceiveAsync = async delegate
			{
				var isocket = await isocket_after_create;

				var result = await isocket.socketId.recvFrom(1048576);

				byte[] buffer = new ScriptCoreLib.JavaScript.WebGL.Uint8ClampedArray(result.data);

				return new UdpReceiveResult(
					buffer,
					remoteEndPoint: default(IPEndPoint)
				);
			};

			#endregion

			#region Client
			this.Client = new __Socket
			{
				#region vBind
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

						Console.WriteLine("UdpClient.Client.vBind " + new
						{
							bind
						});

						afterbind.SetResult(bind);
					}
				}
				#endregion

			};
			#endregion


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

		// X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Net\Sockets\UdpReceiveResult.cs
		public Func<Task<UdpReceiveResult>> vReceiveAsync;
		public Task<UdpReceiveResult> ReceiveAsync() => vReceiveAsync();


		public Action<IPAddress> vJoinMulticastGroup;
		public void JoinMulticastGroup(IPAddress multicastAddr) => vJoinMulticastGroup(multicastAddr);


		public SendAsyncDelegate vSendAsync;
		public delegate Task<int> SendAsyncDelegate(byte[] datagram, int bytes, string hostname, int port);
		public Task<int> SendAsync(byte[] datagram, int bytes, string hostname, int port) => vSendAsync(datagram, bytes, hostname, port);

		public Task<int> SendAsync(byte[] datagram, int bytes)
		{
			return null;
		}
	}
}
