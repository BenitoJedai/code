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
		readonly java.net.Socket InternalSocket;

		public __TcpClient()
		{
			InternalSocket = new java.net.Socket();
		}


		public void Connect(string hostname, int port)
		{
			try
			{
				InternalSocket.connect(new java.net.InetSocketAddress(hostname, port));
			}
			catch
			{
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
