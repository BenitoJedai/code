using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Net;
using System.Net.Sockets;

namespace ScriptCoreLibJava.BCLImplementation.System.Net.Sockets
{
	[Script(Implements = typeof(global::System.Net.Sockets.TcpClient))]
	internal class __TcpClient : IDisposable
	{
		java.net.Socket InternalSocket
		{
			get
			{
				return ((__Socket)(object)Client).InternalSocket;
			}
		}

		public Socket Client { get; set; }

		public __TcpClient()
		{
			Client = (Socket)(object)new __Socket { InternalSocket = new java.net.Socket() };

			//InternalSocket = new java.net.Socket();
		}

		public __TcpClient(Socket s)
		{
			Client = s;

			//InternalSocket = ((__Socket)(object)s).InternalSocket;
		}

		public void Connect(IPAddress hostname, int port)
		{
			try
			{
				InternalSocket.connect(new java.net.InetSocketAddress(((__IPAddress)(object)hostname).InternalAddress, port));
			}
			catch (csharp.ThrowableException t)
			{
				//((java.lang.Throwable)(object)t).printStackTrace();

				throw new InvalidOperationException();
			}
		}

		public void Connect(string hostname, int port)
		{
			try
			{
				InternalSocket.connect(new java.net.InetSocketAddress(hostname, port));
			}
			catch (csharp.ThrowableException t)
			{
				//((java.lang.Throwable)(object)t).printStackTrace();

				throw new InvalidOperationException();
			}
		}

		NetworkStream CachedGetStream;
		public NetworkStream GetStream()
		{
			if (CachedGetStream == null)
				InternalGetNetworkStream();

			return CachedGetStream;
		}

		private void InternalGetNetworkStream()
		{
			try
			{
				CachedGetStream = (NetworkStream)(object)new __NetworkStream
				{
					InternalInputStream = InternalSocket.getInputStream(),
					InternalOutputStream = InternalSocket.getOutputStream()
				};
			}
			catch
			{
				throw new InvalidOperationException();
			}
		}

		public void Close()
		{
			try
			{
				InternalSocket.close();
			}
			catch
			{
				throw new InvalidOperationException();
			}
		}

		#region IDisposable Members

		public void Dispose()
		{
			this.Close();
		}

		#endregion
	}
}
