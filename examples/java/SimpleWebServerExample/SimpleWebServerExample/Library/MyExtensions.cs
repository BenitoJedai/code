using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace SimpleWebServerExample.Library
{
	public static class MyExtensions
	{
		public static void WriteBytes(this MemoryStream m, byte[] data)
		{
			m.Write(data, 0, data.Length);
		}

		public static void WriteLineASCII(this MemoryStream m, string e)
		{
			var x = Encoding.ASCII.GetBytes(e + "\r\n");

			m.Write(x, 0, x.Length);
		}

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
	}
}
