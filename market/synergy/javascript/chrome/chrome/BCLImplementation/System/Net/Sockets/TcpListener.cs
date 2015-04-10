extern alias xglobal;

using ScriptCoreLib;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Net.Sockets;
using ScriptCoreLib.JavaScript.WebGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using xglobal::chrome;

namespace xchrome.BCLImplementation.System.Net.Sockets
{
	// http://referencesource.microsoft.com/#System/net/System/Net/Sockets/TCPListener.cs
	// https://github.com/mono/mono/tree/master/mcs/class/System/System.Net.Sockets/TcpListener.cs
	// X:\jsc.svn\market\synergy\javascript\chrome\chrome\BCLImplementation\System\Net\Sockets\TcpListener.cs
	// x:\jsc.svn\core\scriptcorelibjava\bclimplementation\system\net\sockets\tcplistener.cs
	// "R:\opensource\unmonitored\seclib-1.0\seclib-1.0.0\seclib\Security\Ssl\SecureTcpListener.cs"

	[Script(Implements = typeof(global::System.Net.Sockets.TcpListener))]
	public class __TcpListener
	{
		// X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Net\IPAddress.cs
		// X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerAsync\ChromeTCPServerAsync\Application.cs

		public __TcpListener(IPAddress localaddr, int port)
		{
			var host = "0.0.0.0";

			var isocket_after_listen = new TaskCompletionSource<socketId>();

			// we have to set it ahead of time!
			this.VirtualAcceptTcpClientAsync = async delegate
			{
				Console.WriteLine("at VirtualAcceptTcpClientAsync");
				var iisocket = await isocket_after_listen.Task;

				Console.WriteLine("at VirtualAcceptTcpClientAsync accept... ");
				var accept = await iisocket.accept();

				return new __TcpClient
				{
					VirtualClose = delegate
					{
						accept.socketId.disconnect();
						accept.socketId.destroy();
					},

					VirtualGetStream = () => new __NetworkStream
					{
						VirtualWriteAsync = async (byte[] buffer, int offset, int count) =>
						{
							var copy = new byte[count];

							Array.Copy(
								buffer,
								offset,
								copy,
								0,
								count
							);


							var xx = new Uint8ClampedArray(copy);

							var write = await accept.socketId.write(xx.buffer);

							// report error?
						},

						VirtualReadAsync = async (byte[] buffer, int offset, int count) =>
						{
							var read = await accept.socketId.read(bufferSize: count);


							// whats the best way to copy buffer to buffer?
							byte[] sourceArray = new Uint8ClampedArray(read.data, 0, (uint)read.data.byteLength);

							Array.Copy(
								sourceArray,
								sourceIndex: 0,

								destinationArray: buffer,
								destinationIndex: offset,

								length: sourceArray.Length
							);

							return sourceArray.Length;
						}
					},

					VirtualToString = () => new { accept.resultCode, accept.socketId }.ToString()
				};
			};

			#region VirtualStart
			this.VirtualStart =
				async backlog =>
				{
					var ix = await socket.create("tcp", null);
					var isocket = ix.socketId;
					var listen = await isocket.listen(host, port, backlog: backlog);


					isocket_after_listen.SetResult(isocket);
				};
			#endregion


		}



		// when would jsc allow to implement via delegates?
		Action<int> VirtualStart;

		public void Start() => this.Start(0x7fffffff);
		public void Start(int backlog) => VirtualStart(backlog);


		// ?
		Func<Task<TcpClient>> VirtualAcceptTcpClientAsync;
		public Task<TcpClient> AcceptTcpClientAsync() => VirtualAcceptTcpClientAsync();


	}
}
