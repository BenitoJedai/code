using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Threading;

namespace System_Net_Sockets_TcpClient
{

	[Script]
	public static class Extensions
	{
		public static void AtInterval(this int e, Action h)
		{
			e.AtDelay(
				delegate
				{
					h();

					AtInterval(e, h);
				}
			);
		}

		public static void AtDelay(this int e, Action h)
		{
			new Thread(
				delegate()
				{
					if (e > 0)
						Thread.Sleep(e);
					h();
				}
			) { IsBackground = true }.Start();
		}

		public static void TryStart(this TcpListener l, Action h)
		{
			try
			{
				l.Start();
				h();
			}
			catch
			{
			}
			
		}

		[Script]
		public delegate void StreamAction(Stream s);

		public static void ToListener(this int port, StreamAction handler)
		{
			new Thread(
				delegate()
				{
					var r = new TcpListener(IPAddress.Loopback, port);

					r.TryStart(
						delegate
						{


							while (true)
							{
								var c = r.AcceptTcpClient();

								var s = c.GetStream();

								new Thread(
									delegate()
									{
										handler(s);
									}
								)
								{
									IsBackground = true,
								}.Start();
							}
						}
					);
				}
			)
			{
				IsBackground = true,
			}.Start();

		}

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
