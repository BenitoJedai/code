using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace ScriptCoreLib.CompilerServices
{
	internal static class MyExtensions
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
			}
			catch
			{
				return;
			}
			h();
		}

		public static Thread ToListener(this int port, Action<Stream> handler)
		{
			var thread = new Thread(
				delegate()
				{
					var r = new TcpListener(IPAddress.Loopback, port);

					r.TryStart(
						delegate
						{

							r.Start();

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
			};
			
			
			thread.Start();

			return thread;
		}
	}
}
