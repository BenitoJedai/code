using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace PortCloner.Library
{
	public static class MyExtensions
	{
		public static void Sleep(this int delay)
		{
			Thread.Sleep(delay);

		}

		public delegate void StreamAction(Stream s);

		public static Thread ToListener(this int port, StreamAction handler)
		{
			var t = new Thread(
				delegate()
				{
					var r = new TcpListener(IPAddress.Loopback, port);

					r.TryStart(
						delegate
						{


							while (true)
							{
								// http://stackoverflow.com/questions/365370/proper-way-to-stop-tcplistener
								var c = r.AcceptTcpClient();

								// we wont be able to stop
								// this loop with current implementation

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

			t.Start();
			return t;
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

				// lets be informational
				Console.WriteLine("Oops! TryStart failed");
			}
		}

		public static Thread TryInvokeInBackground(this Action e)
		{
			var t = new Thread(
				delegate()
				{
					try
					{
						e();
					}
					catch 
					{
						Console.WriteLine("Oops! TryInvokeInBackground failed");
					}
				}
			)
			{
				IsBackground = true
			};

			t.Start();

			return t;
		}


	}
}
