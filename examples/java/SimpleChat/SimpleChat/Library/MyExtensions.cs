using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace SimpleChat.Library
{
	static class MyExtensions
	{
		public static void AppendTextLine(this TextBox t, string e)
		{
			t.AppendText(e + Environment.NewLine);
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

			t.Start();
			return t;
		}

		public static int ParseToInt32OrDefault(this string e, int d)
		{
			if (string.IsNullOrEmpty(e))
				return d;
			if (e.IsNumeric())
				return int.Parse(e);

			return d;
		}

		static Random InternalRandomLine = new Random();

		public static string RandomLine(this string e)
		{
			var a = e.Split('\n');

			return a[Convert.ToInt32(InternalRandomLine.NextDouble() * a.Length)].Trim();

		}

		public static bool IsNumeric(this string e)
		{
			var n = "0123456789";
			var r = true;
			foreach (var i in e)
			{
				if (!n.Contains(new string(new[] { i })))
				{
					r = false;
					break;
				}
			}
			return r;
		}
	}
}
