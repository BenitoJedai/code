using System.Net.Sockets;
using System;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Net.Sockets
{
	[Script(Implements = typeof(global::System.Net.Sockets.TcpClient))]
	internal class __TcpClient
	{
		public __TcpClient()
		{

		}

		public Socket Client { get; set; }


		public void Connect(string hostname, int port)
		{
			// http://bugs.php.net/bug.php?id=44335
			if (hostname == "localhost")
				hostname = "127.0.0.1";

			var Client = new __Socket
			{
				InternalHandler = Native.API.fsockopen(hostname, port)
			};

			if (Client.InternalHandler == null)
				throw new Exception("Native.API.fsockopen failed");

			this.Client = (Socket)(object)Client;
		}

		public void Close()
		{
			var Client = (__Socket)(object)this.Client;

			Native.API.fclose(Client.InternalHandler);
			Client.InternalHandler = null;
		}

		NetworkStream InternalStream;

		public NetworkStream GetStream()
		{
			if (InternalStream == null)
			{
				InternalStream = new NetworkStream(this.Client);
			}

			return InternalStream;
		}
	}
}
