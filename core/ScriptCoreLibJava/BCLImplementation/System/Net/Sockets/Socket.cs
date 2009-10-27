using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Net;

namespace ScriptCoreLibJava.BCLImplementation.System.Net.Sockets
{
	[Script(Implements = typeof(global::System.Net.Sockets.Socket))]
	internal class __Socket : IDisposable
	{
		public java.net.ServerSocket InternalServerSocket;
		public java.net.Socket InternalSocket;

		public EndPoint LocalEndPoint
		{
			get
			{
				var e = new __IPEndPoint(
					(IPAddress)(object)new __IPAddress { 
						InternalAddress = InternalSocket.getLocalAddress()
					},

						InternalSocket.getLocalPort()
				);

				return (IPEndPoint)(object)e;
			}
		}

		public void Close()
		{
			if (this.InternalServerSocket != null)
			{
				try
				{
					this.InternalServerSocket.close();
				}
				catch
				{
					throw new InvalidOperationException();
				}
				return;
			}

			throw new InvalidOperationException();
		}

		#region IDisposable Members

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
