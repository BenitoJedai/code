using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Net;
using System.Net.Sockets;
using java.lang;

namespace ScriptCoreLibJava.BCLImplementation.System.Net.Sockets
{
	[Script(Implements = typeof(global::System.Net.Sockets.TcpListener))]
	internal class __TcpListener
	{
		public java.net.ServerSocket InternalSocket;
		public __IPAddress localaddr;
		public int port;

		public __TcpListener(IPAddress localaddr, int port)
		{
			this.localaddr = (__IPAddress)(object)localaddr;
			this.port = port;
		}

		public void Start()
		{
			this.Start(0x7fffffff);
		}

		public Socket Server
		{
			get;
			set;
		}

		public void Start(int backlog)
		{
			// already started...
			if (this.InternalSocket != null)
				return;

			try
			{
				this.InternalSocket = new java.net.ServerSocket(this.port, backlog, this.localaddr.InternalAddress);
			}
			catch
				//(csharp.ThrowableException t)
			{
				//Console.WriteLine(t.Message);
				//((Throwable)(object)t).printStackTrace();

				throw new InvalidOperationException();
			}

			this.Server = (Socket)(object) new __Socket { InternalServerSocket = this.InternalSocket };
		}

		public void Stop()
		{
			try
			{
				this.InternalSocket.close();
				this.InternalSocket = null;
			}
			catch
			{
				throw new InvalidOperationException();
			}
		}

		public Socket AcceptSocket()
		{
			if (InternalSocket == null)
				throw new InvalidOperationException(
					//"Not listening. You must call the Start() method before calling this method."
				);

			var r = default(__Socket);

			try
			{
				r = new __Socket { InternalSocket = this.InternalSocket.accept() };
			}
			catch //(csharp.ThrowableException t)
			{
				//((Throwable)(object)t).printStackTrace();

				throw new InvalidOperationException();
			}

			return (Socket)(object)r;
		}

		public TcpClient AcceptTcpClient()
		{
			var s = AcceptSocket();
			var r = new __TcpClient(s);

			return (TcpClient)(object)r;
		}
	}
}
