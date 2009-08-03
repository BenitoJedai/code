using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Net.Sockets;
using System.IO;

namespace System_Net_Sockets_TcpClient
{

	[Script]
	public static class Extensions
	{
		public static void ToConsole(this string text)
		{
			Console.WriteLine(text);
		}

		public static byte[] GetBytesFromPort(this string host, int port, int count)
		{
			var bytes = default(byte[]);

			using (var tcp = new TcpClient())
			{
				tcp.Connect(host, port);
				var r = new BinaryReader(tcp.GetStream());
				bytes = r.ReadBytes(count);
				tcp.Close();
			}

			return bytes;
		}
	}
}
