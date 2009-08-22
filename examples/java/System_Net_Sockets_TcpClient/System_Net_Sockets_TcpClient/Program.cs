using System.Threading;
using System;

using ScriptCoreLib;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Text;
using System.Collections;


namespace System_Net_Sockets_TcpClient
{

	[Script]
	public class Program
	{
		public static void Main(string[] args)
		{
			// Use Release Build to use jsc to generate java program
			// Use Debug Build to develop on .net

			const int port = 33333;

			Console.WriteLine("server at " + port);

			var Clients = new ArrayList();

			port.ToListener(
				s =>
				{
					Console.WriteLine("connected at " + port);

					Clients.Add(s);

					var x = s.ReadByte();

					while (x >= 0)
					{
						foreach (Stream item in Clients)
						{
							if (item != s)
								item.WriteByte((byte)x);
						}

						x = s.ReadByte();
					}
				}
			);

		

			//foreach (var b in "127.0.0.1".GetBytesFromPort(30123, 0x40))
			//{
			//    Console.Write(" " + b.ToString("x2"));
			//}

			Console.WriteLine("connecting...");

			var c = new TcpClient();

			c.Connect(IPAddress.Loopback, port);

			var cs = c.GetStream();

			0.AtDelay(
				delegate
				{
					var x = cs.ReadByte();

					while (x >= 0)
					{
						Console.Write((char)x);

						x = cs.ReadByte();
					}
				}
			);

			#region Console to net
			var cr = Console.ReadLine();

			while (cr != "")
			{
				// telnet needs r and n!

				var data = Encoding.ASCII.GetBytes(cr + "\r\n");

				cs.Write(data, 0, data.Length);

				cr = Console.ReadLine();
			}
			#endregion

		}

	


	}


}
